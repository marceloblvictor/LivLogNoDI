using LivlogNoDI.Data.Repositories;
using LivlogNoDI.Enums;
using LivlogNoDI.Models.DTO;
using LivlogNoDI.Models.Entities;

namespace LivlogNoDI.Services
{
    public class FineService
    {
        private readonly FineRepository _repo;

        public FineService()
        {
            _repo = new FineRepository();
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
        
        public FineDTO Create(FineDTO fineDTO)
        {
            var fine = CreateEntity(fineDTO);

            fine = _repo.Add(fine);

            return Get(fine.Id);
        }

        public FineDTO UpdateFineToPaid(int fineId)
        {
            var fineDto = Get(fineId);

            fineDto = SetFineStatusToPaid(fineDto);

            var fine = CreateEntity(fineDto);

            fine =_repo.Update(fine);

            return Get(fine.Id);
        }

        public bool Delete(int id)
        {
            return _repo.Delete(id);            
        }

        private FineDTO CreateDTO(Fine fine)
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

        #region Helper Methods
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
                .Where(b => ids.Contains(b.Id))
                .ToList();
        }

        public FineDTO SetFineStatusToPaid(FineDTO fineDto)
        {
            if (fineDto.Status == FineStatus.Paid)
            {
                throw new ArgumentException("A multa já se encontra paga");
            }

            fineDto.Status = FineStatus.Paid;

            return fineDto;
        }

        #endregion

    }
}
