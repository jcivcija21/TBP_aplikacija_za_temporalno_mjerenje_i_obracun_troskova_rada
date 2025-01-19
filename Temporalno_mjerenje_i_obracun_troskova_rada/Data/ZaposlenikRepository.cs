using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Data
{
    public class ZaposlenikRepository
    {
        private readonly DatabaseContext _context;

        public ZaposlenikRepository(DatabaseContext context)
        {
            _context = context;
        }

        public List<ZaposlenikDTO> GetAllZaposlenici()
        {
            var zaposlenici = new List<ZaposlenikDTO>();

            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("SELECT * FROM zaposlenici", connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        zaposlenici.Add(new ZaposlenikDTO
                        {
                            ZaposlenikId = reader.GetInt32(0),
                            Ime = reader.GetString(1),
                            Prezime = reader.GetString(2),
                            Oib = reader.GetString(3),
                            DatumZaposlenja = reader.GetDateTime(4),
                            Aktivan = reader.GetBoolean(5),
                        });
                    }
                }
            }

            return zaposlenici;
        }

        public void AddZaposlenik(ZaposlenikDTO zaposlenik)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("INSERT INTO zaposlenici (Ime, Prezime, Oib, datum_zaposlenja, Aktivan) " +
                                                "VALUES (@Ime, @Prezime, @Oib, @DatumZaposlenja, @Aktivan)", connection);

                command.Parameters.AddWithValue("@Ime", zaposlenik.Ime);
                command.Parameters.AddWithValue("@Prezime", zaposlenik.Prezime);
                command.Parameters.AddWithValue("@Oib", zaposlenik.Oib);
                command.Parameters.AddWithValue("@DatumZaposlenja", zaposlenik.DatumZaposlenja);
                command.Parameters.AddWithValue("@Aktivan", zaposlenik.Aktivan);

                command.ExecuteNonQuery();
            }
        }
        public void UpdateZaposlenik(ZaposlenikDTO zaposlenik)
        {
            using (var connection = _context.GetConnection())
            {
                connection.Open();
                var command = new NpgsqlCommand("UPDATE zaposlenici SET Ime = @Ime, Prezime = @Prezime, Oib = @Oib, " +
                                                "datum_zaposlenja = @DatumZaposlenja, Aktivan = @Aktivan WHERE zaposlenik_id = @ZaposlenikId", connection);

                command.Parameters.AddWithValue("@Ime", zaposlenik.Ime);
                command.Parameters.AddWithValue("@Prezime", zaposlenik.Prezime);
                command.Parameters.AddWithValue("@Oib", zaposlenik.Oib);
                command.Parameters.AddWithValue("@DatumZaposlenja", zaposlenik.DatumZaposlenja);
                command.Parameters.AddWithValue("@Aktivan", zaposlenik.Aktivan);
                command.Parameters.AddWithValue("@ZaposlenikId", zaposlenik.ZaposlenikId);

                command.ExecuteNonQuery();
            }
        }
    }
}
