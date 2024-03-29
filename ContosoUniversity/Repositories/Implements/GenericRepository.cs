﻿using ContosoUniversity.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Repositories.Implementation
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {

        private readonly SchoolContext schoolContext;

        public GenericRepository(SchoolContext schoolContext)
        {
           this.schoolContext = schoolContext;
        }

        public async  Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null)
                throw new Exception("The entity is null");
            schoolContext.Set<TEntity>().Remove(entity);

              await schoolContext.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await schoolContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await  schoolContext.Set<TEntity>().FindAsync(id);
        }
    

        public async  Task<TEntity> Insert(TEntity entity)
        {
            await schoolContext.Set<TEntity>().AddAsync(entity);
            await schoolContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
             schoolContext.Set<TEntity>().Update(entity);
            await schoolContext.SaveChangesAsync();
            return entity;
        }
    }
}
