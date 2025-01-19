using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.Data;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Services
{
    public class ObracunPlaceService
    {
        private readonly ObracunPlaceRepository _repository;

        public ObracunPlaceService(ObracunPlaceRepository repository)
        {
            _repository = repository;
        }

        public void AddObracun(ObracunPlaceDTO obracun)
        {
            _repository.AddObracunPlace(obracun);
        }

        public IEnumerable<ObracunPlaceDTO> GetAllObracuni()
        {
            return _repository.GetAllObracuni();
        }
    }
}
