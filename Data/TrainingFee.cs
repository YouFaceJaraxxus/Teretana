using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teretana.Data
{
    public class TrainingFee
    {
        public int Id { get; set; }
        public virtual Training training { get; set; }
        public double price { get; set; }
    }
}
