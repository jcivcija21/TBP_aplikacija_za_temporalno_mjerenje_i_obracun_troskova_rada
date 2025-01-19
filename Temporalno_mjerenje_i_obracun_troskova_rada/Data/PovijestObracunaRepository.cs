using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Data
{
    public class PovijestObracunaRepository
    {
        private readonly DatabaseContext _context;

        public PovijestObracunaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<PovijestObracunaDTO> GetLogsForEmployee(int zaposlenikId)
        {
            var logs = new List<PovijestObracunaDTO>();

            string query = @"
        SELECT povijest_id, obracun_id, datum_izmjene, staro_bruto, novo_bruto
        FROM povijest_obracuna 
        WHERE obracun_id IN 
        (SELECT obracun_id FROM obracun_place WHERE zaposlenik_id = @ZaposlenikId)";

            using (var connection = _context.GetConnection())
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ZaposlenikId", zaposlenikId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add(new PovijestObracunaDTO
                            {
                                PovijestId = reader.GetInt32(0),
                                ObracunId = reader.GetInt32(1),
                                DatumIzmjene = reader.GetDateTime(2),
                                StaroBruto = reader.GetDecimal(3),
                                NovoBruto = reader.GetDecimal(4)
                            });
                        }
                    }
                }
            }


            return logs;
        }
    }
}
