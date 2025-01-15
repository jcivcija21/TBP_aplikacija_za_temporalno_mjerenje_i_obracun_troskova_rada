using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.DTOs
{
    public class ObracunPlaceDTO
    {
        public int ObracunId { get; set; }
        public decimal Bruto { get; set; }
        public decimal Doprinosi { get; set; }
        public decimal Porez { get; set; }
        public decimal Prirez { get; set; }
        public decimal Neto { get; set; }
        public DateTime DatumObracuna { get; set; }
    }
}
