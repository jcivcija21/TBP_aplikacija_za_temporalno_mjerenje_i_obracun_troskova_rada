using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.DTOs
{
    public class RadniSatiDTO
    {
        public int RadniSatiId { get; set; }
        public int ZaposlenikId { get; set; }
        public DateTime Datum { get; set; }
        public decimal Sati { get; set; }
    }

}
