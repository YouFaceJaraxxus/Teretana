using Teretana.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml;

namespace Teretana.Data
{
    public class Member
    {
        public int Id { get; set; }
        public string JMB { get; set; }
        public string name { get; set; }
        public string lastName { get; set; }
        public bool active { get; set; }
        public DateTime joinDate { get; set; }
        public DateTime untilDate { get; set; } = DateTime.Now;
        public DateTime birthDate { get; set; }
        public byte[] imgBytes { get; set; }
        public virtual MembershipType membershipType { get; set; }
        public bool gender { get; set; } = true; //true==male, false==female

        [NotMapped]
        public bool alreadyTrainedToday
        {
            get
            {
                bool alreadyTrained = false;
                using (var db = new AppDBContext())
                {
                    alreadyTrained = db.Trainings.Include(tr => tr.Member).Where(tr => (tr.Member.Id == Id && tr.TrainingDate.Date.Equals(DateTime.Now.Date))).Any();
                }
                return alreadyTrained;
            } 
        }

        [NotMapped]
        public string birthDateAsString
        {
            get
            {
                return birthDate.Day + "." + birthDate.Month + "." + birthDate.Year;
            }
        }

        [NotMapped]
        public string joinDateAsString
        {
            get
            {
                return joinDate.Day + "." + joinDate.Month + "." + joinDate.Year;
            }
        }

        [NotMapped]
        public string untilDateAsString
        {
            get
            {
                return untilDate.Day + "." + untilDate.Month + "." + untilDate.Year;
            }
        }

        [NotMapped]
        public string membershipTypeAsString
        {
            get
            {
                return membershipType.name;
            }
        }

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
