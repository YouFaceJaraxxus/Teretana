using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Teretana.Data
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public bool type { get; set; }  //false==admin, true==trainer
        public bool active { get; set; }

        public bool logged { get; set; }
        public bool master { get; set; }

        [NotMapped]
        public string typeAsString { get
            {
                if (type) return Application.Current.Resources["Trainer"] as string;
                else return Application.Current.Resources["Admin"] as string;
            } }

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
