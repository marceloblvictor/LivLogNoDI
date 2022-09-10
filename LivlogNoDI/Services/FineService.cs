using LivlogNoDI.Data.Repositories;
using LivlogNoDI.Enums;
using LivlogNoDI.Models.DTO;
using LivlogNoDI.Models.Entities;
using LivlogNoDI.Validators;
using static LivlogNoDI.Enums.CustomerCategory;
using static LivlogNoDI.Enums.FineStatus;

namespace LivlogNoDI.Services
{
    public class FineService
    {
        private readonly FineRepository _repo;
        private readonly FineValidator _validator;
        private readonly CustomerService _customerService;

        public FineService()
        {
            _repo = new FineRepository();
            _validator = new FineValidator();
            _customerService = new CustomerService();
        }

        public FineDTO Get(int fineid)
        {
            var fine = _repo.Get(fineid);

            return CreateDTO(fine);
        }

        public IEnumerable<FineDTO> GetAll()
        {
            var fines = _repo.GetAll();

            return CreateDTOs(fines);
        }

        public decimal GetCustomerDebts(int customerId)
        {
            var finesDto = GetAll();

            var customerFines = FilterByCustomer(finesDto, customerId);

            var customerActiveFines = FilterByStatus(customerFines, Active);

            var amountOwed = customerActiveFines.Sum(f => f.Amount);

            return amountOwed;
        }

        public FineDTO Create(FineDTO fineDTO)
        {
            var fine = CreateEntity(fineDTO);

            fine = _repo.Add(fine);

            return CreateDTO(fine);
        }

        public FineDTO FineCustomer(int customerId, int daysOverdue)
        {
            var customerCategory = _customerService
                .GetCustomerCategory(customerId);

            var fineAmount = CalculateFineAmount(
                customerCategory,
                daysOverdue);

            var fineDto = new FineDTO()
            {
                Id = 0,
                Amount = fineAmount,
                Status = Active,
                CustomerId = customerId,
                CustomerName = null
            };

            fineDto = Create(fineDto);

            // SendFineNotification();

            return fineDto;
        }

        public FineDTO UpdateFineToPaid(int fineId, decimal amountPaid)
        {
            var fineDto = Get(fineId);

            _validator.ValidateFineToBePaid(fineDto, amountPaid);

            fineDto = SetFineStatusToPaid(fineDto);

            var fine = _repo.Get(fineId);
            fine.Status = fineDto.Status;

            fineDto = CreateDTO(_repo.Update(fine));

            return fineDto;
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);            
        }

        #region Helper Methods

        public FineDTO CreateDTO(Fine fine)
        {
            return new FineDTO
            {
                Id = fine.Id,
                Amount = fine.Amount,
                Status = fine.Status,
                CustomerName = fine.Customer.Name,
                CustomerId = fine.CustomerId,
            };
        }

        public decimal CalculateFineAmount(CustomerCategory category, int overdueDays)
        {
            var categoryRate = GetCategoryFineRate(category);

            return categoryRate * overdueDays;
        }

        public decimal GetCategoryFineRate(CustomerCategory category)
            => category switch
            {
                Top => 1.25m,
                Medium => 1.50m,
                Low => 2m,

                _ => throw new ArgumentException()
            };

        public IEnumerable<FineDTO> CreateDTOs(IEnumerable<Fine> fines)
        {
            var finesDtos = new List<FineDTO>();

            foreach (var fine in fines)
            {
                finesDtos.Add(CreateDTO(fine));
            }

            return finesDtos;
        }

        public Fine CreateEntity(FineDTO dto)
        {
            return new Fine
            {
                Id = dto.Id,
                Amount = dto.Amount,
                Status = dto.Status,                
                CustomerId = dto.CustomerId
            };
        }

        public IList<FineDTO> FilterByIds(IEnumerable<FineDTO> fines, IList<int> ids)
        {
            return fines
                .Where(f => ids.Contains(f.Id))
                .ToList();
        }

        public IList<FineDTO> FilterByCustomer(IEnumerable<FineDTO> fines, int customerId)
        {
            return fines
                .Where(f => f.CustomerId == customerId)
                .ToList();
        }

        public IList<FineDTO> FilterByStatus(IEnumerable<FineDTO> fines, FineStatus status)
        {
            return fines
                .Where(f => f.Status == status)
                .ToList();
        }

        public FineDTO SetFineStatusToPaid(FineDTO fineDto)
        {            
            fineDto.Status = Paid;

            return fineDto;
        }

        #endregion

    }
}
