using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teretana.Data
{
    public class PurchaseItem
    {
        [Key, Column(Order = 0)]
        public int productId { get; set; }
        [Key, Column(Order = 1)]
        public int purchaseId { get; set; }
        public virtual Product product { get; set; }
        public virtual Purchase purchase { get; set; }
        public int count { get; set; }

        [NotMapped]
        public double total
        {
            get
            {
                return count * product.price;
            }
        }
    }
}
