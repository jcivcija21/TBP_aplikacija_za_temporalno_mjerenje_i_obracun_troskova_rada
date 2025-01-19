using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Data
{
    public class RadniSatiRepository
    {
        private readonly DatabaseContext _context;

        public RadniSatiRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<RadniSatiDTO> GetAllRadniSati()
        {
            var radniSati = new List<RadniSatiDTO>();
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT radni_sati_id, zaposlenik_id, datum, sati FROM radni_sati", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        radniSati.Add(new RadniSatiDTO
                        {
                            RadniSatiId = reader.GetInt32(0),
                            ZaposlenikId = reader.GetInt32(1),
                            Datum = reader.GetDateTime(2),
                            Sati = reader.GetDecimal(3)
                        });
                    }
                }
            }
            return radniSati;
        }

        public void AddRadniSati(RadniSatiDTO radniSati)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand(
                    "INSERT INTO radni_sati (zaposlenik_id, datum, sati) VALUES (@ZaposlenikId, @Datum, @Sati)", connection);

                command.Parameters.AddWithValue("@ZaposlenikId", radniSati.ZaposlenikId);
                command.Parameters.AddWithValue("@Datum", radniSati.Datum);
                command.Parameters.AddWithValue("@Sati", radniSati.Sati);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateRadniSati(RadniSatiDTO radniSati)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand(
                    "UPDATE radni_sati SET sati = @Sati WHERE radni_sati_id = @RadniSatiId", connection);

                command.Parameters.AddWithValue("@Sati", radniSati.Sati);
                command.Parameters.AddWithValue("@RadniSatiId", radniSati.RadniSatiId);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteRadniSati(int radniSatiId)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("DELETE FROM radni_sati WHERE radni_sati_id = @RadniSatiId", connection);
                command.Parameters.AddWithValue("@RadniSatiId", radniSatiId);
                command.ExecuteNonQuery();
            }
        }

        public decimal GetTotalWorkHoursForEmployee(int employeeId, int month, int year)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand(
                    "SELECT SUM(sati) FROM radni_sati WHERE zaposlenik_id = @EmployeeId AND EXTRACT(MONTH FROM datum) = @Month AND EXTRACT(YEAR FROM datum) = @Year",
                    connection
                );
                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                command.Parameters.AddWithValue("@Month", month);
                command.Parameters.AddWithValue("@Year", year);

                var result = command.ExecuteScalar();
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
        }

    }
}
