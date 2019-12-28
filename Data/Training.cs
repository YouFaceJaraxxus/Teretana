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
    public class Training
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public DateTime TrainingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public virtual Member Member { get; set; }
        [NotMapped]
        public string trainingDateAsString { get { return TrainingDate.Day + "." + TrainingDate.Month + "." + TrainingDate.Year; } }
        [NotMapped]
        public string endingDateAsString { get { if(EndingDate==DateTime.MinValue) return Application.Current.Resources["In progress"] as string;
                else return EndingDate.Day + "." + EndingDate.Month + "." + EndingDate.Year; } }
        [NotMapped]
        public string durationAsString { get {
                TimeSpan span;
                if (EndingDate == DateTime.MinValue)span = DateTime.Now - TrainingDate;
                else span = EndingDate - TrainingDate;
                int hours = (int)span.TotalHours;
                int minutes = ((int)span.TotalMinutes)%60;
                return hours.ToString() + "h " + minutes.ToString() + "m";
            } }
        [NotMapped]
        public string currentlyActive
        {
            get
            {
                if (EndingDate == DateTime.MinValue) return Application.Current.Resources["In progress"] as string;
                else return Application.Current.Resources["Finished"] as string;
            }
        }
    }
}
