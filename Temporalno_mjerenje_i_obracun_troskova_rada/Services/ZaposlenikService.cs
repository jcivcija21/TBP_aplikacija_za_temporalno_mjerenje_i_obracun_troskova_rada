using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.Data;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Services
{
    public class ZaposlenikService
    {
        private readonly ZaposlenikRepository _repository;

        public ZaposlenikService(ZaposlenikRepository repository)
        {
            _repository = repository;
        }

        public List<ZaposlenikDTO> GetAllZaposlenici()
        {
            return _repository.GetAllZaposlenici();
        }

        public void AddZaposlenik(ZaposlenikDTO zaposlenik)
        {
            _repository.AddZaposlenik(zaposlenik);
        }

        public void UpdateZaposlenik(ZaposlenikDTO zaposlenik)
        {
            _repository.UpdateZaposlenik(zaposlenik);
        }

    }
}
