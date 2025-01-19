using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.DTOs
{
    public class PovijestObracunaDTO
    {
        public int PovijestId { get; set; }
        public int ObracunId { get; set; }
        public DateTime DatumIzmjene { get; set; }
        public decimal StaroBruto { get; set; }
        public decimal NovoBruto { get; set; }
    }
}
