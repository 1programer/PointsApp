using Microsoft.EntityFrameworkCore;
using PointAppWithCleanArchitecture.Data;
using PointAppWithCleanArchitecture.Domain.Models;
using PointAppWithCleanArchitecture.Interfaces;

namespace PointAppWithCleanArchitecture.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            if (entity is Base date)
            {
                date.DateOfCreate = DateTime.Now;
            }
            SaveChanges();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

            if (entity is Base date)
            {
                date.DateOfCreate = DateTime.Now;
            }

            SaveChangesAsync();
        }

        public void Delete(Guid id)
        {
            T entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
            SaveChanges();
        }

        public async Task DeleteAsync(Guid id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsyncWithString(string id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                if (entity is User userEntity)
                {
                    var pointsToDelete = _context.Point.Where(b => b.UserId.ToString() == userEntity.Id);
                    _context.Point.RemoveRange(pointsToDelete);
                }

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return [.. _dbSet];
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public T GetById(Guid id)
        {
            T entity = _dbSet.Find(id);
            return entity;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            T entity = await _dbSet.FindAsync(id);
            return entity;
        }
        public async Task<T> GetByIdAsyncWithString(string id)
        {
            T entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity).State = EntityState.Modified;
            if (entity is Base date)
            {
                date.DateOfUpdate = DateTime.Now;
            }
            SaveChanges();
        }

        public async Task UpdateAsync(Guid id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Update(entity).State = EntityState.Modified;
            if (entity is Base date)
            {
                date.DateOfUpdate = DateTime.Now;
            }
            SaveChanges();
        }
        public async Task UpdateAsyncWithString(string id)
        {
            T entity = await _dbSet.FindAsync(id);
            if (entity != null)
                _dbSet.Update(entity).State = EntityState.Modified;
            if (entity is Base date)
            {
                date.DateOfUpdate = DateTime.Now;
            }
            SaveChanges();
        }
        private void SaveChanges()
        {
            _context.SaveChanges();
        }

        private async void SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}