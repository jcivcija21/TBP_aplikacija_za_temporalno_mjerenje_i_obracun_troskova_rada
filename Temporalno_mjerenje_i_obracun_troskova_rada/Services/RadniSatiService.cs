using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.Data;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Services
{
    public class RadniSatiService
    {
        private readonly RadniSatiRepository _repository;

        public RadniSatiService(RadniSatiRepository repository)
        {
            _repository = repository;
        }

        public List<RadniSatiDTO> GetAllRadniSati()
        {
            return _repository.GetAllRadniSati();
        }

        public void AddRadniSati(RadniSatiDTO radniSati)
        {
            _repository.AddRadniSati(radniSati);
        }

        public void UpdateRadniSati(RadniSatiDTO radniSati)
        {
            _repository.UpdateRadniSati(radniSati);
        }

        public void DeleteRadniSati(int radniSatiId)
        {
            _repository.DeleteRadniSati(radniSatiId);
        }

        public decimal GetTotalWorkHoursForEmployee(int employeeId, int month, int year)
        {
            return _repository.GetTotalWorkHoursForEmployee(employeeId, month, year);
        }

    }
}
