using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Teretana.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public bool type { get; set; } //true==equipment, false==supplement
        public int state { get; set; }

        public byte[] productImage { get; set; }


        [NotMapped]
        public string typeAsString
        {
            get
            {
                if (type) return Application.Current.Resources["Equipment"] as string;
                else return Application.Current.Resources["Supplement"] as string;
            }
        }
    }
}
