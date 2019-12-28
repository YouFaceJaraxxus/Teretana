using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teretana.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Teretana
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SessionDetailView : Page
    {
        Training training;
        Member member;
        int id;
        public SessionDetailView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.id = Int32.Parse((string)e.Parameter);
            using (var db = new AppDBContext())
            {
                training = db.Trainings.Include(tr => tr.Member).Where(tr => tr.Id == this.id).FirstOrDefault();
                member = db.Members.Include(m => m.membershipType).Where(m => m.Id == this.id).FirstOrDefault();
                idValueBlock.Text = id.ToString();
                nameValueBlock.Text = member.name;
                lastNameValueBlock.Text = member.lastName;
                string membershipTypeString = member.membershipTypeAsString;
                if (membershipTypeString == null || membershipTypeString.Equals("none"))
                {
                    membershipTypeValueBlock.Text = Application.Current.Resources["None"] as string;
                }
                else membershipTypeValueBlock.Text = membershipTypeString;
                dateValueBlock.Text = training.trainingDateAsString + "  " + training.TrainingDate.Hour.ToString()+"h " + training.TrainingDate.Minute.ToString()+"m";
                untilDateValueBlock.Text = training.endingDateAsString + "  " + training.EndingDate.Hour.ToString() + "h " + training.EndingDate.Minute.ToString() + "m"; ;
                durationValueBlock.Text = training.durationAsString;
                loadImage();
            }
        }
        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(TrainingSessionsView));
        }

        public async void loadImage()
        {

            var data = member.imgBytes;

            IRandomAccessStream stream = await Util.ConvertTo(data);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap softwareBitmap = await decoder.GetSoftwareBitmapAsync();



            SoftwareBitmap softwareBitmapBGR8 = SoftwareBitmap.Convert(softwareBitmap,
            BitmapPixelFormat.Bgra8,
            BitmapAlphaMode.Premultiplied);

            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                SoftwareBitmapSource bitmapSource = new SoftwareBitmapSource();
                bitmapSource.SetBitmapAsync(softwareBitmapBGR8);
                memberImage.Source = bitmapSource;
            });
        }

    }
}
