using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.Data;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Services
{
    public class DoprinosiService
    {
        private readonly DoprinosRepository _repository;

        public DoprinosiService(DoprinosRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<DoprinosiDTO> GetAllDoprinosi()
        {
            return _repository.GetAllDoprinosi();
        }
    }
}
