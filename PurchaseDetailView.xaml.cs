using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class PurchaseDetailView : Page
    {
        int id;
        ObservableCollection<PurchaseItemObservable> items;
        Purchase purchase;
        public PurchaseDetailView()
        {
            this.InitializeComponent();
            items = new ObservableCollection<PurchaseItemObservable>();
            itemsList.ItemsSource = items;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            id = Int32.Parse((string)e.Parameter);
            using(AppDBContext db = new AppDBContext())
            {
                purchase = db.Purchases.Where(p => p.Id == id).FirstOrDefault();
                List<PurchaseItem> purchaseItems = db.PurchaseItems.Include(pi => pi.product).Include(pi => pi.purchase).Where(pi => pi.purchaseId == id).ToList();
                foreach (PurchaseItem item in purchaseItems) items.Add(new PurchaseItemObservable() { name = item.product.name, price = item.product.price, productId = item.productId, quantity = item.count, total = item.total });
                itemsList.ItemsSource = items;
                titleBlock.Text = purchase.typeAsString;
                dateValueBlock.Text = purchase.dateAsString;
                idValueBlock.Text = purchase.Id.ToString();
                totalValueBlock.Text = purchase.total.ToString();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(SalesView));
        }
    }
}
