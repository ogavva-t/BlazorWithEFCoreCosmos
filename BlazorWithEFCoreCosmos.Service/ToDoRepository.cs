using System;
using System.Collections.Generic;
using System.Linq;
using BlazorWithEFCoreCosmos.Entity;
using Microsoft.EntityFrameworkCore;
using BlazorWithEFCoreCosmos.Infrastructure.Cosmos;
using System.Threading.Tasks;


namespace BlazorWithEFCoreCosmos.Repository
{
    public interface IToDoRepository
    {
        Task<List<ToDo>> GetAsync();
        Task<ToDo> GetAsync(Guid id);
        Task<ToDo> AddAsync(ToDo todo);
        Task<ToDo> UpdateAsync(ToDo todo);
        Task<ToDo> DeleteAsync(Guid id);
    }

    public class ToDoRepository : IToDoRepository
    {
        private readonly CosmosDbConnectionFactory _dbDonnectionFactory;

        public ToDoRepository(CosmosDbConnectionFactory dbDonnectionFactory)
        {
            _dbDonnectionFactory = dbDonnectionFactory;
        }

        public async Task<List<ToDo>> GetAsync()
        {
            using (var context = _dbDonnectionFactory.CreateConnection())
            {
                var contracts = await context.ToDo
                    .ToListAsync();
                return contracts;
            }
        }
        public async Task<ToDo> GetAsync(Guid id)
        {
            using (var context = _dbDonnectionFactory.CreateConnection())
            {
                var contract = await context.ToDo
                    .FindAsync(id);
                return contract;
            }
        }

        public async Task<ToDo> AddAsync(ToDo todo)
        {
            using (var context = _dbDonnectionFactory.CreateConnection())
            {
                context.ToDo.Add(todo);
                await context.SaveChangesAsync();
                return todo;
            }
        }
        public async Task<ToDo> UpdateAsync(ToDo todo)
        {
            using (var context = _dbDonnectionFactory.CreateConnection())
            {
                var existingContract = await context.ToDo
                    .FindAsync(todo.Id);
                context.Entry(existingContract)?.CurrentValues.SetValues(todo);
                await context.SaveChangesAsync();
                return todo;
            }
        }

        public async Task<ToDo> DeleteAsync(Guid id)
        {
            using (var context = _dbDonnectionFactory.CreateConnection())
            {
                var contract = await context.ToDo
                    .FindAsync(id);

                if (contract != null)
                {
                    context.ToDo.Remove(contract);
                    await context.SaveChangesAsync();
                }
                return contract;
            }
        }
    }
}
