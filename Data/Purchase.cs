using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Teretana.Data
{
    public class Purchase
    {
        public int Id { get; set; }
        public double total { get; set; }
        public DateTime date { get; set; }
        public bool type { get; set; } //true==buy, false==sell

        [NotMapped]
        public string typeAsString
        {
            get
            {
                if (type) return Application.Current.Resources["Buy"] as string;
                else return Application.Current.Resources["Sell"] as string;
            }
        }

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
