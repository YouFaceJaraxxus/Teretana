using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teretana.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Teretana
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TrainingSessionsView : Page
    {
        string query;
        List<Training> allTrainings;
        public TrainingSessionsView()
        {
            this.InitializeComponent();
            fillTrainingsList();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            TrainerMenu.trainerMenuFrame.Navigate(typeof(SessionDetailView), ((TextBlock)realSender.Children[1]).Text);
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            query = searchBox.Text;
            query = query.ToLower();
            fillTrainingsList();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            fillTrainingsList();
        }

        private void endSessionButton_Click(object sender, RoutedEventArgs e)
        {
            Training training = (Training)trainingsList.SelectedItem;
            if (training != null)
            {
                using (AppDBContext db = new AppDBContext())
                {
                    if (training.EndingDate == DateTime.MinValue)
                    {
                        training.EndingDate = DateTime.Now;
                        using (var trx = db.Database.BeginTransaction())
                        {
                            db.Trainings.Update(training);
                            db.SaveChanges();
                            trx.Commit();
                        }
                        fillTrainingsList();
                    }
                    else
                    {
                        MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["This session has already been logged"] as string), (Application.Current.Resources["Error"] as string));
                        messageDialog.ShowAsync();
                        return;
                    }
                }
            }
        }

        private void fillTrainingsList()
        {
            using (AppDBContext db = new AppDBContext())
            {
                if (String.IsNullOrEmpty(query))
                {
                    allTrainings = db.Trainings.Include(tr=>tr.Member).ToList();
                    trainingsList.ItemsSource = allTrainings;
                }
                else
                {
                    allTrainings = db.Trainings.Include(tr => tr.Member).Where(tr => tr.Id.ToString().Equals(query) || tr.Member.name.ToLower().Contains(query) || tr.Member.lastName.ToLower().Contains(query)).ToList();
                    trainingsList.ItemsSource = allTrainings;
                }
            }
        }
    }
}
