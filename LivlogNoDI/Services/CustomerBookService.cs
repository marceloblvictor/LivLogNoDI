using LivlogNoDI.Data.Repositories;
using LivlogNoDI.Enums;
using LivlogNoDI.Models.DTO;
using LivlogNoDI.Models.Entities;
using LivlogNoDI.Validators;
using static LivlogNoDI.Enums.CustomerCategory;

namespace LivlogNoDI.Services
{
    public class CustomerBookService
    {
        private readonly CustomerBookRepository _repo;

        private readonly CustomerService _customerService;
        private readonly BookService _bookService;
        private readonly FineService _fineService;

        private readonly BookRentalValidator _rentalValidator;
        
        public CustomerBookService()
        {
            _repo = new CustomerBookRepository();

            _customerService = new CustomerService();
            _bookService = new BookService();
            _fineService = new FineService();

            _rentalValidator = new BookRentalValidator();
        }

        public CustomerBookDTO Get(int id)
        {
            var customerBook = _repo.Get(id);

            return CreateDTO(customerBook);
        }

        public IList<CustomerBookDTO> GetAll()
        {
            var customerBooks = _repo.GetAll();

            return CreateDTOs(customerBooks);
        }

        public IList<CustomerBookDTO> GetByCustomer(int customerId)
        {
            var allCustomerBooks = GetAll();
            var customerBooks = FilterByCustomer(allCustomerBooks, customerId);

            return customerBooks;
        }        

        public CustomerBookDTO Update(int customerBookId, CustomerBookDTO customerBookDTO)
        {
            var customerBook = _repo.Get(customerBookId);

            customerBook.StartDate = customerBookDTO.StartDate;
            customerBook.DueDate = customerBookDTO.DueDate;
            customerBook.Status = customerBookDTO.Status;

            var updatedBook =
                _repo.Update(customerBook);

            return CreateDTO(updatedBook);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);
        }

        public IList<CustomerBookDTO> RentBooks(CustomerBooksRequestDTO request)
        {
            // Obter cliente 
            var customer = _customerService.Get(request.CustomerId);

            // Verifica se o usuário possui alguma dívida em aberto
            var allFines = _fineService
                .GetAll()
                .ToList();

            _rentalValidator.ValidateCustomerIsInDebt(
                allFines, 
                customer);

            // Obter os livros requisitados
            var requestedBooks = _bookService.FilterByIds(
                _bookService.GetAll(), 
                request.BookIds);

            // Obter todos os livros de todos os clientes
            var allCustomerBooks = GetAll();

            // Filtrar os livros de clientes pertencentes ao cliente que fez o request
            var customerBooks = FilterByCustomer(
                allCustomerBooks, customer.Id);

            // Filtrar os livros do cliente que tenham o status "Ativo"
            var activeCustomerBooks = FilterByStatus(
                customerBooks, BookRentalStatus.Active);

            // Validar se o cliente pode alugar mais livros
            _rentalValidator.ValidateCustomerRentalsLimit(
                activeCustomerBooks,
                requestedBooks,
                GetCustomerRentalsMaxLimit(customer.Category));

            // Validar se existem livros suficientes no estoque
            _rentalValidator.ValidateBookAvailability(
                requestedBooks,
                allCustomerBooks);

            DateTime currentDateTime = DateTime.Now;

            // Calcula o prazo do empréstimo do cliente baseado em sua categoria
            var customerDueDate = CalculateDueDate(customer, currentDateTime);

            var customerBooksCreated = new List<CustomerBook>();

            // Cria uma nova entidade CustomerBook para cada livro alugado
            foreach (int bookId in request.BookIds)
            {
                customerBooksCreated.Add(
                    _repo.Add(CreateEntity(request.CustomerId, bookId, customerDueDate)));
            }

            // Retorna uma lista com informações dos livros alugados pelo cliente
            var rentedBookDtos = CreateDTOs(customerBooksCreated);

            return rentedBookDtos;
        }

        public IList<CustomerBookDTO> ReturnBooks(IList<int> customerBookIds)
        {
            var returnedCustomerBooks = FilterByIds(
                GetAll(), 
                customerBookIds);

            _rentalValidator.ValidateReturnedBooksSameCustomer(
                returnedCustomerBooks);

            DateTime currentDateTime = DateTime.Now;

            // TODO: apagar próxima linha! SOMENTE PARA TESTE DE LIVROS ATRASADOS!!
            // currentDateTime = currentDateTime.AddDays(20);

            // Verifica se tem algum livro em atraso e cria multa, caso exista
            foreach (var returnedBook in returnedCustomerBooks)
            {
                if (IsReturnedBookOverdue(returnedBook, currentDateTime))
                {
                    var overdueDays = GetOverdueDays(returnedBook.DueDate, currentDateTime);

                    _fineService.FineCustomer(
                        returnedBook.CustomerId,
                        overdueDays);
                }
            }

            returnedCustomerBooks = SetCustomerBookStatusToReturned(
                returnedCustomerBooks);

            foreach (var returnedBook in returnedCustomerBooks)
            {
                Update(returnedBook.Id, returnedBook);
            }

            // SendReturnalNotification();

            return returnedCustomerBooks;
        }

        public bool SendReminderToCustomer(int customerId)
        {
            // _messager.SendEmail();

            return true;
        }

        #region Helper Methods

        public IList<CustomerBookDTO> SetCustomerBookStatusToReturned(IList<CustomerBookDTO> returnedCustomerBooks)
        {
            foreach (var returnedBook in returnedCustomerBooks)
            {
                _rentalValidator.ValidateReturnedBookStatus(returnedBook);

                returnedBook.Status = BookRentalStatus.Returned;
            }

            return returnedCustomerBooks;
        }

        public int GetOverdueDays(DateTime dueDate, DateTime returnDate)
        {
            return (returnDate - dueDate).Days;
        }

        public bool IsReturnedBookOverdue(
            CustomerBookDTO customerBook,
            DateTime returnDate)
        {
            return returnDate > customerBook.DueDate;
        }

        public DateTime CalculateDueDate(CustomerDTO customer, DateTime startTime)
        {
            return startTime.AddDays(
                GetCustomerCategoryDaysDuration(customer.Category));
        }

        public int GetCustomerCategoryDaysDuration(CustomerCategory category)
            => category switch
            {
                Top => 15,
                Medium => 10,
                Low => 5,
                _ => throw new ArgumentException()
            };

        public int GetCustomerRentalsMaxLimit(CustomerCategory category)
            => category switch
            {
                Top => 5,
                Medium => 3,
                Low => 1,
                _ => throw new ArgumentException()
            };

        public IList<CustomerBookDTO> FilterByCustomer(
            IList<CustomerBookDTO> dtos,
            int customerId)
        {
            return dtos
                .Where(dto => dto.CustomerId == customerId)
                .ToList();
        }

        public IList<CustomerBookDTO> FilterByCustomerAndBook(
            IList<CustomerBookDTO> dtos,
            int customerId,
            IList<int> bookIds)
        {
            return dtos
                .Where(dto => dto.CustomerId == customerId &&
                              bookIds.Contains(dto.BookId))
                .ToList();
        }

        public IList<CustomerBookDTO> FilterByStatus(
            IList<CustomerBookDTO> dtos,
            BookRentalStatus status)
        {
            return dtos
                .Where(dto => dto.Status == status)
                .ToList();
        }

        public IList<CustomerBookDTO> FilterByIds(
            IEnumerable<CustomerBookDTO> customerBooks, 
            IList<int> ids)
        {
            return customerBooks
                .Where(cb => ids.Contains(cb.Id))
                .ToList();
        }

        public CustomerBookDTO CreateDTO(CustomerBook customerBook)
        {
            return new CustomerBookDTO
            {
                Id = customerBook.Id,
                BookId = customerBook.BookId,
                BookTitle = customerBook.Book.Title,
                CustomerId = customerBook.CustomerId,
                CustomerName = customerBook.Customer.Name,
                StartDate = customerBook.StartDate,
                DueDate = customerBook.DueDate,
                Status = customerBook.Status
            };
        }

        public IList<CustomerBookDTO> CreateDTOs(IList<CustomerBook> customerBooks)
        {
            var customerBooksDtos = new List<CustomerBookDTO>();

            foreach (var customerBook in customerBooks)
            {
                customerBooksDtos.Add(CreateDTO(customerBook));
            }

            return customerBooksDtos;
        }

        public CustomerBook CreateEntity(
            int customerId, 
            int bookId, 
            DateTime dueDate)
        {
            return new CustomerBook
            {
                BookId = bookId,
                CustomerId = customerId,
                StartDate = DateTime.Now,
                DueDate = dueDate,
                Status = BookRentalStatus.Active
            };
        }

        #endregion
    }
}
