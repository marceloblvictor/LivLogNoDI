using LivlogNoDI.Enums;
using LivlogNoDI.Models.DTO;

namespace LivlogNoDI.Validators
{
    public class BookRentalValidator
    {
        public void ValidateCustomerIsInDebt(
            IList<FineDTO> allFines, 
            CustomerDTO customer)
        {
            var isInDebt = allFines
                .Where(f => f.CustomerId == customer.Id &&
                            f.Status == FineStatus.Active)
                .Any();

            if (isInDebt)
            {
                throw new Exception("Este usuário não pode alugar livros pois possui multas não pagas");
            }
        }

        public void ValidateCustomerRentalsLimit(
            IList<CustomerBookDTO> activeCustomerBooks, 
            IList<BookDTO> requestedBooks, 
            int customerMaxLimit)
        {
            var requestedAmount = requestedBooks.Count;

            var remainingAmount = customerMaxLimit - activeCustomerBooks.Count;

            if (requestedAmount > remainingAmount)
                throw new Exception($"O cliente não pode utrapassar o limite de {customerMaxLimit} empréstimos");
        }

        public void ValidateBookAvailability(
            IList<BookDTO> requestedBooks, 
            IList<CustomerBookDTO> allBooks)
        {
            foreach (var book in requestedBooks)
            {
                // Conta quantos exemplares do livro requisitado estão alugados
                int booksTakenCount = allBooks
                    .Where(b => b.BookId == book.Id && 
                                b.Status == BookRentalStatus.Active)
                    .Count();

                if (booksTakenCount >= book.Quantity)
                    throw new Exception($"Não existem mais exemplares disponíveis para empréstimo do livro '{book.Title}'");
            }
        }

        public void ValidateReturnedBooksSameCustomer(
            IList<CustomerBookDTO> returnedBooks)
        {
            bool isSameCustomer = returnedBooks
                .Select(cb => cb.CustomerId)
                .Distinct()
                .Count() == 1;

            if (!isSameCustomer)
            {
                throw new Exception("O retorno de livros só pode ser feito em relação a um usuário por vez");
            }
        }

        public void ValidateReturnedBookStatus(CustomerBookDTO returnedBook)
        {
            if (returnedBook.Status == BookRentalStatus.WaitingQueue)
            {
                throw new Exception("Livros em espera não podem ser devolvidos");
            }

            if (returnedBook.Status == BookRentalStatus.Returned)
            {
                throw new Exception("Este livro já foi devolvido");
            }
        }
    }
}
