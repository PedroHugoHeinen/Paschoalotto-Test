using Microsoft.EntityFrameworkCore;
using paschoalotto_api.Data;
using paschoalotto_api.Repository.Interfaces;

namespace paschoalotto_api.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly DbSet<T> dbSet;

        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            this.dbSet = this.applicationDbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this.dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            await this.dbSet.AddAsync(entity);
            await this.applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            this.dbSet.Attach(entity);
            this.applicationDbContext.Entry(entity).State = EntityState.Modified;
            await this.applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != default)
            {
                this.dbSet.Remove(entity);
                await this.applicationDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
