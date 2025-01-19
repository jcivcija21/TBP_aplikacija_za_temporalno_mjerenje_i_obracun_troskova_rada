using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.Data;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Services
{
    public class PoreziService
    {
        private readonly PoreziRepository _repository;

        public PoreziService(PoreziRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<PorezDTO> GetAllPorezi()
        {
            return _repository.GetAllPorezi();
        }
    }
}
