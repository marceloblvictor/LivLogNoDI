using LivlogNoDI.Enums;
using LivlogNoDI.Models.DTO;

namespace LivlogNoDI.Validators
{
    public class FineValidator
    {
        public void ValidateFineToBePaid(FineDTO fineDto, decimal amountPaid)
        {
            if (fineDto.Status != FineStatus.Active)
            {
                throw new Exception("A multa já está paga");
            }

            if (amountPaid < fineDto.Amount)
            {
                throw new Exception($"O valor pago não é suficiente para quitar a dívida. Faltam R$ { fineDto.Amount - amountPaid }");
            }
        }
    }
}
