using LivlogNoDI.Data.Repositories;
using LivlogNoDI.Models.DTO;
using LivlogNoDI.Models.Entities;

namespace LivlogNoDI.Services
{
    public class CustomerBookService
    {
        private readonly CustomerBookRepository _repo;

        public CustomerBookService()
        {
            _repo = new CustomerBookRepository();
        }

        public CustomerBookDTO Get(int id)
        {
            var customerBook = _repo.Get(id);

            return CreateDTO(customerBook);
        }

        public IEnumerable<CustomerBookDTO> GetAll()
        {
            var customerBooks = _repo.GetAll();

            return CreateDTOs(customerBooks);
        }

        private CustomerBookDTO CreateDTO(CustomerBook customerBook)
        {
            return new CustomerBookDTO
            {
                Id = customerBook.Id,
                BookId = customerBook.BookId,
                CustomerId = customerBook.CustomerId,
                StartDate = customerBook.DueDate
            };
        }

        private IEnumerable<CustomerBookDTO> CreateDTOs(IEnumerable<CustomerBook> customerBooks)
        {
            var customerBooksDtos = new List<CustomerBookDTO>();

            foreach (var customerBook in customerBooks)
            {
                customerBooksDtos.Add(CreateDTO(customerBook));
            }

            return customerBooksDtos;
        }

        private CustomerBook CreateEntity(CustomerBookDTO dto)
        {
            return new CustomerBook
            {
                Id = dto.Id,
                BookId = dto.BookId,
                CustomerId = dto.CustomerId,
                StartDate = dto.DueDate
            };
        }
    }
}
