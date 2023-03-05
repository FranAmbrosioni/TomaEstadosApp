using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using CRUDSQLite.Models;
using System.Threading.Tasks;

namespace CRUDSQLite.Data
{
    public class SQLiteHelper
    {

        public SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Usuario>().Wait();

        }

        public Task<int> GuardarUsuarioAsync(Usuario usuario)
        {
            if (usuario.Id != 0)
            {
                return db.UpdateAsync(usuario);

            }
            else
            {
                return db.InsertAsync(usuario);
            }
        }

        public Task<List<Usuario>> GetusuarioAsync()
        {
            return db.Table<Usuario>().ToListAsync();
        }

        public Task<Usuario> GetUsuarioById(int idUsuario)
        {
            return db.Table<Usuario>().Where(a => a.Id == idUsuario).FirstOrDefaultAsync();
        }
    
        public Task<int> DeleteUsuarioAsync(Usuario usuario)
        {
            return db.DeleteAsync(usuario);
        }
    }
}
