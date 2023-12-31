using ContactAppXamarin.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ContactAppXamarin.Database
{
    public class ContactoDatabase
    {
        readonly SQLiteAsyncConnection database;

        public ContactoDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ContactoModel>().Wait();
        }

        public AsyncTableQuery<ContactoModel> AsQueryable()
        {
            return database.Table<ContactoModel>();
        }

        public async Task<List<ContactoModel>> GetAllAsync(Expression<Func<ContactoModel, bool>> expression = null)
        {
            AsyncTableQuery<ContactoModel> query = AsQueryable();

            if (expression != null)
                query = query.Where(expression);

           return await query.ToListAsync();

        }
        public async Task<ContactoModel> FirstOrDefaultAsync(Expression<Func<ContactoModel, bool>> expression)
        {
            AsyncTableQuery<ContactoModel> query = AsQueryable();
            return await query.FirstOrDefaultAsync(expression);
        }

        public async Task<int> InsertAsync(ContactoModel entity)
        {
            return await database.InsertAsync(entity);
        }
        public async Task<int> InsertAllAsync(List<ContactoModel> entity)
        {
            return await database.InsertAllAsync(entity);
        }
        public async Task<int> UpdateAsync(ContactoModel entity)
        {
            return await database.UpdateAsync(entity);
        }       

        public async Task<int> DeleteAsync(Guid Id)
        {
            var registro = await FirstOrDefaultAsync(x=>x.Id == Id);
            registro.Deleted = true;
            registro.DeletedAt = DateTime.Now;
            return await database.UpdateAsync(registro);
        }
    }
}
