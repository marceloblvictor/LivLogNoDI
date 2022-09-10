using LivlogNoDI.Enums;
using LivlogNoDI.Models.DTO;

namespace LivlogNoDI.Validators
{
    public class BookRentalValidator
    {
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
    }
}
