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
    public sealed partial class MembersView : Page
    {
        public List<Member> allMembers = null;
        string query;
        public MembersView()
        {
            this.InitializeComponent();
            fillMembersList();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            TrainerMenu.trainerMenuFrame.Navigate(typeof(MemberDetailView), ((TextBlock)realSender.Children[1]).Text);
        }


        private void fillMembersList()
        {
            using (AppDBContext db = new AppDBContext())
            {
                if (String.IsNullOrEmpty(query))
                {
                    allMembers = db.Members.Include(m => m.membershipType).ToList();
                    membersList.ItemsSource = allMembers;
                }
                else
                {
                    allMembers = db.Members.Include(m => m.membershipType).Where(m => m.name.ToLower().Contains(query) || m.lastName.ToLower().Contains(query) || m.JMB.Contains(query)).ToList();
                    membersList.ItemsSource = allMembers;
                }
            }
        }

        private async void AddMember_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(AddMemberMenu));
        }


        private void editMemberButton_Click(object sender, RoutedEventArgs e)
        {
            Member member = (Member)membersList.SelectedItem;
            if(member!=null)
            {
                TrainerMenu.trainerMenuFrame.Navigate(typeof(EditMemberMenu), member.Id);
            }
        }


        private void prolongMembershipButton_Click(object sender, RoutedEventArgs e)
        {
            Member member = (Member)membersList.SelectedItem;
            if (member != null)
            {
                if((member.untilDate-DateTime.Now).TotalDays>3)
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The membership can only be extended up to three days before expiration"] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
                TrainerMenu.trainerMenuFrame.Navigate(typeof(ProlongMembershipMenu), member.Id);
            }
        }

        private void activateButton_Click(object sender, RoutedEventArgs e)
        {
            Member member = (Member)membersList.SelectedItem;
            if (member != null)
            {
                if(!member.active)
                {
                    using (AppDBContext db = new AppDBContext())
                    {
                        member.active = true;
                        using (var trx = db.Database.BeginTransaction())
                        {
                            db.Members.Update(member);
                            db.SaveChanges();
                            trx.Commit();
                        }
                    }
                }
            }
            fillMembersList();
        }

        private void deactivateButton_Click(object sender, RoutedEventArgs e)
        {
            Member member = (Member)membersList.SelectedItem;
            if (member != null)
            {
                if (member.active)
                {
                    using (AppDBContext db = new AppDBContext())
                    {
                        member.active = false;
                        using (var trx = db.Database.BeginTransaction())
                        {
                            db.Members.Update(member);
                            db.SaveChanges();
                            trx.Commit();
                        }
                    }
                }
            }
            fillMembersList();
        }

        private void addSessionButton_Click(object sender, RoutedEventArgs e)
        {
            Member member = (Member)membersList.SelectedItem;
            if (member != null)
            {
                if (member.active)
                {
                    if(member.untilDate>DateTime.Now)
                    {
                        if(!member.alreadyTrainedToday)
                        {
                            using (AppDBContext db = new AppDBContext())
                            {
                                member = db.Members.Include(m => m.membershipType).Where(m => m.Id == member.Id).FirstOrDefault();
                                Training training = new Training()
                                {
                                    Id = member.Id,
                                    TrainingDate = DateTime.Now,
                                    Member = member,
                                    EndingDate = DateTime.MinValue
                                };
                                using (var trx = db.Database.BeginTransaction())
                                {
                                    db.Trainings.Add(training);
                                    db.SaveChanges();
                                    trx.Commit();
                                }
                                fillMembersList();
                            }
                        }
                        else
                        {
                            MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The member has already trained today"] as string), (Application.Current.Resources["Error"] as string));
                            messageDialog.ShowAsync();
                            return;
                        }
                    }
                    else
                    {
                        MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The member's membership has expired"] as string), (Application.Current.Resources["Error"] as string));
                        messageDialog.ShowAsync();
                        return;
                    }
                }
                else
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The member is inactive"] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
            }
        }

        private void endSessionButton_Click(object sender, RoutedEventArgs e)
        {
            Member member = (Member)membersList.SelectedItem;
            if (member != null)
            {
                using (AppDBContext db = new AppDBContext())
                {
                    Training training = db.Trainings.Include(tr => tr.Member).Where(tr => tr.Member.Id == member.Id && tr.TrainingDate.Date.Equals(DateTime.Now.Date)).FirstOrDefault();
                    if(training==null)
                    {
                        MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The member is not training at the moment"] as string), (Application.Current.Resources["Error"] as string));
                        messageDialog.ShowAsync();
                        return;
                    }
                    else
                    {
                        if(training.EndingDate==DateTime.MinValue)
                        {
                            training.EndingDate = DateTime.Now;
                            using (var trx = db.Database.BeginTransaction())
                            {
                                db.Trainings.Update(training);
                                db.SaveChanges();
                                trx.Commit();
                            }
                            fillMembersList();
                        }
                        else
                        {
                            MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["The today's session has already been logged"] as string), (Application.Current.Resources["Error"] as string));
                            messageDialog.ShowAsync();
                            return;
                        }
                    }
                }
            }
        }


        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            query = searchBox.Text;
            query = query.ToLower();
            fillMembersList();
        }
    }
}
