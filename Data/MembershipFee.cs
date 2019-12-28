using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teretana.Data
{
    public class MembershipFee
    {
        [Key]
        public int Id { get; set; }
        public double price { get; set; }
        public virtual Member member { get; set; }
        public virtual MembershipType membershipType { get; set; }
        public int numberOfMonths { get; set; }
        public double realPrice { get; set; }
        public double discount { get; set; }
        public DateTime date { get; set; }

        [NotMapped]
        public string dateAsString
        {
            get
            {
                return date.Day + "." + date.Month + "." + date.Year;
            }
        }
    }
}
