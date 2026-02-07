
using CinemaWebAppOriginal.Data;
using CinemaWebAppOriginal.Infrastructure.Repositories.Contracts;

using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CinemaWebAppOriginal.Infrastructure.Repositories
{
    public class BaseRepository<TType, TId> : IRepository<TType, TId> where TType : class
    {

        private readonly AppDbContext context;
        private readonly DbSet<TType> dbSet;

        public BaseRepository(AppDbContext _context)
        {
            this.context = _context;
            this.dbSet = this.context.Set<TType>(); 
        }


        public void Add(TType entity)
        {
            this.dbSet.Add(entity);
            this.context.SaveChanges();
        }

        public async Task AddAndSaveAsync(TType entity)
        {
            await this.dbSet.AddAsync(entity);
            await this.context.SaveChangesAsync();
        }

        public bool Delete(TId id)
        {
            TType entity = this.GetById(id);

            if (entity == null) {
                return false;
            }
            this.dbSet.Remove(entity);
            this.context?.SaveChanges();

            return true;    
        }

        public async Task DeleteAsync(TId id)
        {
            TType entity = await this.GetByIdAsync(id);

            if(entity != null)
            {
                this.dbSet.Remove(entity);
                await this.context.SaveChangesAsync();
            }
        }
        public async Task RemoveAsync(TType entity)
        {
            this.dbSet.Remove(entity);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteRangeAndSaveChangesAsync(List<TType> entities)
        {
            this.dbSet.RemoveRange(entities);
            await this.context.SaveChangesAsync();
        }

        public IEnumerable<TType> GetAll()
        {
            return this.dbSet.ToArray();
        }

        public async Task<ICollection<TType>> GetAllAsync()
        {
            return await this.dbSet.AsTracking().ToListAsync();
        }

        public IQueryable<TType> GetAllAttached()
        {
            return this.dbSet.AsQueryable();
        }

        public TType GetById(TId id)
        {
            TType ?entity = this.dbSet.Find(id);
            return entity;
        }

        public async Task<TType> GetByIdAsync(TId id)
        {
            TType entity = await this.dbSet.FindAsync(id);
            return entity;
        }

       
        public void Update(TType entity)
        {
            this.dbSet.Attach(entity);
            this.context.Entry(entity).State = EntityState.Modified;    
            this.context.SaveChanges();
        }

        public async Task UpdateAsync(TType entity)
        {
            this.dbSet.Attach(entity);
            this.dbSet.Entry(entity).State = EntityState.Modified;
            await this.context.SaveChangesAsync();
        }
    }
}
