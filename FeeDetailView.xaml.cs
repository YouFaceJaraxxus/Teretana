using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Teretana.Data;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class FeeDetailView : Page
    {
        int id;
        MembershipFee fee;
        public FeeDetailView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.id = Int32.Parse((string)e.Parameter);
            using (var db = new AppDBContext())
            {
                fee = db.MembershipFees.Include(f=>f.member).Where(f => f.Id == this.id).FirstOrDefault();
                idValueBlock.Text = id.ToString();
                memberIdValueBlock.Text = fee.member.Id.ToString();
                nameValueBlock.Text = fee.member.name;
                lastNameValueBlock.Text = fee.member.lastName;
                priceValueBlock.Text = fee.realPrice.ToString("#.00");
                actualPriceValueBlock.Text = fee.price.ToString("#.00");
                discountValueBlock.Text = fee.discount.ToString("#.00");
                dateValueBlock.Text = fee.dateAsString;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(SalesView));
        }
    }
}
