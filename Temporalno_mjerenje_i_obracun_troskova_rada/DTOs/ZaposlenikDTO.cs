using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.DTOs
{
    public class ZaposlenikDTO
    {
        public int ZaposlenikId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Oib { get; set; }
        public DateTime DatumZaposlenja { get; set; }
    }
}
