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
    public sealed partial class SalesView : Page
    {
        string query;
        List<Product> products;
        List<MembershipFee> fees;
        List<Purchase> purchases;
        public SalesView()
        {
            this.InitializeComponent();
            refillLists();
        }

        private void feeDetailButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            TrainerMenu.trainerMenuFrame.Navigate(typeof(FeeDetailView), ((TextBlock)realSender.Children[1]).Text);
        }

        private void productDetailButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            TrainerMenu.trainerMenuFrame.Navigate(typeof(ProductDetailView), ((TextBlock)realSender.Children[1]).Text);
        }

        private void purchaseDetailButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanel realSender = (StackPanel)((Button)sender).Parent;
            TrainerMenu.trainerMenuFrame.Navigate(typeof(PurchaseDetailView), ((TextBlock)realSender.Children[2]).Text);
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            query = searchBox.Text;
            query = query.ToLower();
            refillLists();
        }

        private void addProductButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(AddProductMenu));
        }

        private void editProductButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = (Product)productsList.SelectedItem;
            if (product != null)
            {
                TrainerMenu.trainerMenuFrame.Navigate(typeof(EditProductMenu), product.Id.ToString());
            }
        }

        private void buyProductButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(BuySellView), true);
        }

        private void sellProductButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(BuySellView), false);
        }

        private void refillLists()
        {
            using (AppDBContext db = new AppDBContext())
            {
                if (String.IsNullOrEmpty(query))
                {
                    products = db.Products.ToList();
                    fees = db.MembershipFees.ToList();
                    purchases = db.Purchases.ToList();
                }
                else
                {
                    products = db.Products.Where(p => p.Id.ToString().Contains(query) || p.name.Contains(query)).ToList();
                    fees = db.MembershipFees.Include(f => f.member).Where(f => f.Id.ToString().Contains(query) || f.member.name.Contains(query) || f.member.lastName.Contains(query)).ToList();
                    purchases = db.Purchases.Where(p => p.Id.ToString().Contains(query)).ToList();
                }
            }
            productsList.ItemsSource = products;
            feesList.ItemsSource = fees;
            purchasesList.ItemsSource = purchases;
        }

    }
}
