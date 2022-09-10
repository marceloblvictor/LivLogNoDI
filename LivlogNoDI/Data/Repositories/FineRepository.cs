using LivlogNoDI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LivlogNoDI.Data.Repositories
{
    public class FineRepository
    {
        private readonly LivlogNoDIContext _dbContext;

        public FineRepository()
        {
            _dbContext = new LivlogNoDIContext(
                new DbContextOptions<LivlogNoDIContext>());
        }

        public List<Fine> GetAll()
        {
            return _dbContext.Fines
                .Include(f => f.Customer)
                .AsNoTracking()
                .OrderByDescending(b => b.Id)
                .ToList();
        }

        public Fine Get(int fineId)
        {
            return _dbContext.Fines
                .Include(f => f.Customer)
                .AsNoTracking()
                .Where(f => f.Id == fineId)
                .SingleOrDefault()
                    ?? throw new ArgumentException();
        }

        public Fine Add(Fine fine)
        {
            _dbContext.Fines.Add(fine);
            _dbContext.SaveChanges();

            fine = Get(fine.Id);

            return fine;
        }

        public Fine Update(Fine fine)
        {
            _dbContext.Fines.Update(fine);
            _dbContext.SaveChanges();

            fine = Get(fine.Id);

            return fine;
        }

        public bool Delete(int fineId)
        {
            var fine = Get(fineId);

            if (fine is null)
            {
                throw new ArgumentException();
            }
            
            _dbContext.Remove(fine);
            _dbContext.SaveChanges();

            return true;
        }
    }
}
