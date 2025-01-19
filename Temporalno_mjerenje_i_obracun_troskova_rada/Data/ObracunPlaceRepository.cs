using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Data
{
    public class ObracunPlaceRepository
    {
        private readonly DatabaseContext _context;

        public ObracunPlaceRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void AddObracunPlace(ObracunPlaceDTO obracun)
        {
            var obracuni = new List<ObracunPlaceDTO>();

            using (var connection = _context.GetConnection())
            {
                connection.Open();

                var command = new NpgsqlCommand("SELECT * FROM obracun_place WHERE zaposlenik_id = @ZaposlenikId", connection);
                command.Parameters.AddWithValue("@ZaposlenikId", obracun.ZaposlenikId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        obracuni.Add(new ObracunPlaceDTO
                        {
                            ObracunId = reader.GetInt32(0),
                            ZaposlenikId = reader.GetInt32(1),
                            Bruto = reader.GetDecimal(2),
                            Doprinosi = reader.GetDecimal(3),
                            Porez = reader.GetDecimal(4),
                            Prirez = reader.GetDecimal(5),
                            Neto = reader.GetDecimal(6),
                            DatumObracuna = reader.GetDateTime(7)
                        });
                    }
                }
            }

            using (var connection = _context.GetConnection())
            {
                connection.Open();

                if (obracuni.Count == 0)
                {
                    var insertCommand = new NpgsqlCommand(
                        "INSERT INTO obracun_place (zaposlenik_id, bruto, doprinosi, porez, prirez, neto, datum_obracuna) " +
                        "VALUES (@ZaposlenikId, @Bruto, @Doprinosi, @Porez, @Prirez, @Neto, @DatumObracuna)", connection);

                    insertCommand.Parameters.AddWithValue("@ZaposlenikId", obracun.ZaposlenikId);
                    insertCommand.Parameters.AddWithValue("@Bruto", obracun.Bruto);
                    insertCommand.Parameters.AddWithValue("@Doprinosi", obracun.Doprinosi);
                    insertCommand.Parameters.AddWithValue("@Porez", obracun.Porez);
                    insertCommand.Parameters.AddWithValue("@Prirez", obracun.Prirez);
                    insertCommand.Parameters.AddWithValue("@Neto", obracun.Neto);
                    insertCommand.Parameters.AddWithValue("@DatumObracuna", obracun.DatumObracuna);

                    insertCommand.ExecuteNonQuery();
                    Console.WriteLine("New payroll record inserted.");
                } else
                {
                    var updateCommand = new NpgsqlCommand(
                        "UPDATE obracun_place SET bruto = @Bruto, doprinosi = @Doprinosi, porez = @Porez, prirez = @Prirez, neto = @Neto " +
                        "WHERE zaposlenik_id = @ZaposlenikId", connection);

                    updateCommand.Parameters.AddWithValue("@Bruto", obracun.Bruto);
                    updateCommand.Parameters.AddWithValue("@Doprinosi", obracun.Doprinosi);
                    updateCommand.Parameters.AddWithValue("@Porez", obracun.Porez);
                    updateCommand.Parameters.AddWithValue("@Prirez", obracun.Prirez);
                    updateCommand.Parameters.AddWithValue("@Neto", obracun.Neto);
                    updateCommand.Parameters.AddWithValue("@ZaposlenikId", obracun.ZaposlenikId);
                    updateCommand.Parameters.AddWithValue("@DatumObracuna", obracun.DatumObracuna);

                    updateCommand.ExecuteNonQuery();
                    Console.WriteLine("Payroll record updated.");
                }
            }
        }

        public IEnumerable<ObracunPlaceDTO> GetAllObracuni()
        {
            var obracuni = new List<ObracunPlaceDTO>();

            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT * FROM obracun_place", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        obracuni.Add(new ObracunPlaceDTO
                        {
                            ObracunId = reader.GetInt32(0),
                            ZaposlenikId = reader.GetInt32(1),
                            Bruto = reader.GetDecimal(2),
                            Doprinosi = reader.GetDecimal(3),
                            Porez = reader.GetDecimal(4),
                            Prirez = reader.GetDecimal(5),
                            Neto = reader.GetDecimal(6),
                            DatumObracuna = reader.GetDateTime(7)
                        });
                    }
                }
            }

            return obracuni;
        }
    }
}
