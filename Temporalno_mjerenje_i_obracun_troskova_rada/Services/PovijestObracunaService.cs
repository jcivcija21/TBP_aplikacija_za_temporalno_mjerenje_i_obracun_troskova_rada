using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Temporalno_mjerenje_i_obracun_troskova_rada.Data;
using Temporalno_mjerenje_i_obracun_troskova_rada.DTOs;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.Services
{
    public class PovijestObracunaService
    {
        private readonly PovijestObracunaRepository _povijestObracunaRepository;

        public PovijestObracunaService(PovijestObracunaRepository povijestObracunaRepository)
        {
            _povijestObracunaRepository = povijestObracunaRepository;
        }

        public List<PovijestObracunaDTO> GetLogsForEmployee(int zaposlenikId)
        {
            return _povijestObracunaRepository.GetLogsForEmployee(zaposlenikId);
        }
    }
}
