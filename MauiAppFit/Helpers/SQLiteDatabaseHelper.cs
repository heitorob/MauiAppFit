using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppFit.Models;
using SQLite;

namespace MauiAppFit.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection conectar;

        public SQLiteDatabaseHelper(string rota)
        {
            conectar = new SQLiteAsyncConnection(rota);
            conectar.CreateTableAsync<Atividade>().Wait();
        }

        public Task<int> Insert(Atividade a)
        {
            return conectar.InsertAsync(a);
        }

        public Task<List<Atividade>> Update(Atividade a)
        {
            string sql = "UPDATE Atividade SET Descricao=?, Data=?, Peso=?, Observacoes=? WHERE Id=?";
            return conectar.QueryAsync<Atividade>(sql, a.Descricao, a.Data, a.Peso, a.Observacoes, a.Id);
        }

        public Task<List<Atividade>> GetAll()
        {
            return conectar.Table<Atividade>().ToListAsync();
        }

        public Task GetById(int id)
        {
            return conectar.Table<Atividade>().FirstAsync(i => i.Id == id);
        }
    }
}
