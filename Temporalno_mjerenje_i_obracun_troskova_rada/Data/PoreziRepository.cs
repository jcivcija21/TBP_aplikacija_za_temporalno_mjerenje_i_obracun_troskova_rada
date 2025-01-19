using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Data
{
    public class PoreziRepository
    {
        private readonly DatabaseContext _context;

        public PoreziRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<PorezDTO> GetAllPorezi()
        {
            var porezi = new List<PorezDTO>();

            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT * FROM porezi", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        porezi.Add(new PorezDTO
                        {
                            PorezId = reader.GetInt32(0),
                            Naziv = reader.GetString(1),
                            Stopa = reader.GetDecimal(2)
                        });
                    }
                }
            }

            return porezi;
        }
    }
}
