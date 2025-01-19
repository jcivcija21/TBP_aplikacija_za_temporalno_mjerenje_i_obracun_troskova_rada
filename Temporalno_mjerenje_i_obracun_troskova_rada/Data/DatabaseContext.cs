using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Data
{
    public class DatabaseContext
    {
        private readonly string _connectionString;

        public DatabaseContext()
        {
            _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=12341234;Database=obracun_placa;CommandTimeout=60;Timeout=60;";
        }

        public NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
