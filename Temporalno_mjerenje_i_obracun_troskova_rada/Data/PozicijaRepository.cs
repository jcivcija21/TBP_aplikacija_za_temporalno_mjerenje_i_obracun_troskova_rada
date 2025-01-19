using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Data
{
    public class PozicijaRepository
    {
        private readonly DatabaseContext _context;

        public PozicijaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<PozicijaDTO> GetAllPozicije()
        {
            var pozicije = new List<PozicijaDTO>();

            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT * FROM pozicije", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        pozicije.Add(new PozicijaDTO
                        {
                            PozicijaId = reader.GetInt32(0),
                            Naziv = reader.GetString(1),
                            Opis = reader.IsDBNull(2) ? null : reader.GetString(2),
                            OsnovnaSatnica = reader.GetDecimal(3)
                        });
                    }
                }
            }

            return pozicije;
        }

        public void AddPozicija(PozicijaDTO pozicija)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("INSERT INTO pozicije (naziv, opis, osnovna_satnica) " +
                                                "VALUES (@Naziv, @Opis, @OsnovnaSatnica)", connection);

                command.Parameters.AddWithValue("@Naziv", pozicija.Naziv);
                command.Parameters.AddWithValue("@Opis", pozicija.Opis ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@OsnovnaSatnica", pozicija.OsnovnaSatnica);

                command.ExecuteNonQuery();
            }
        }

        public void UpdatePozicija(PozicijaDTO pozicija)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("UPDATE pozicije SET naziv = @Naziv, opis = @Opis, " +
                                                "osnovna_satnica = @OsnovnaSatnica WHERE pozicija_id = @PozicijaId", connection);

                command.Parameters.AddWithValue("@Naziv", pozicija.Naziv);
                command.Parameters.AddWithValue("@Opis", pozicija.Opis ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@OsnovnaSatnica", pozicija.OsnovnaSatnica);
                command.Parameters.AddWithValue("@PozicijaId", pozicija.PozicijaId);

                command.ExecuteNonQuery();
            }
        }

        public void DeletePozicija(int pozicijaId)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("DELETE FROM pozicije WHERE pozicija_id = @PozicijaId", connection);

                command.Parameters.AddWithValue("@PozicijaId", pozicijaId);

                command.ExecuteNonQuery();
            }
        }
    }
}
