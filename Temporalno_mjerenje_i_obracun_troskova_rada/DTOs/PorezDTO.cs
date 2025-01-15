using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temporalno_mjerenje_i_obracun_troskova_rada.DTOs
{
    public class PorezDTO
    {
        public int PorezId { get; set; }
        public string Naziv { get; set; }
        public decimal Stopa { get; set; }
    }

}
