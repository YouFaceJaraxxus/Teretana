using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Teretana.Data
{
    public class MembershipType
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
        public double price { get; set; }

        [NotMapped]
        public string activeAsString
        {
            get
            {
                if (active) return Application.Current.Resources["Active"] as string;
                else return Application.Current.Resources["Inactive"] as string;
            }
        }
    }
}
