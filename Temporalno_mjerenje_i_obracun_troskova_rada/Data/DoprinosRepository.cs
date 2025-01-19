using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Data
{
    public class DoprinosRepository
    {
        private readonly DatabaseContext _context;

        public DoprinosRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<DoprinosiDTO> GetAllDoprinosi()
        {
            var doprinosi = new List<DoprinosiDTO>();

            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT * FROM doprinosi", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        doprinosi.Add(new DoprinosiDTO
                        {
                            DoprinosId = reader.GetInt32(0),
                            Naziv = reader.GetString(1),
                            Stopa = reader.GetDecimal(2)
                        });
                    }
                }
            }

            return doprinosi;
        }
    }
}
