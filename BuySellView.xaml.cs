using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class BuySellView : Page
    {
        bool buy;
        Purchase purchase;
        List<Product> products;
        ObservableCollection<PurchaseItem> items;
        //ObservableCollection<PurchaseItem> itemsCollection;
        public BuySellView()
        {
            this.InitializeComponent();
            items = new ObservableCollection<PurchaseItem>();
            purchaseItemsList.ItemsSource = items;
            //itemsCollection = new ObservableCollection<PurchaseItem>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            buy = (bool)e.Parameter;

            purchase = new Purchase()
            {
                type = buy
            };
            if (buy)
            {
                titleBlock.Text = Application.Current.Resources["Buy product"] as string;
            }
            else
            {
                titleBlock.Text = Application.Current.Resources["Sell product"] as string;
            }
            using (var db = new AppDBContext())
            {
                products = db.Products.ToList();
                productsList.ItemsSource = products;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            TrainerMenu.trainerMenuFrame.Navigate(typeof(SalesView));
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if(items.Any())
            {
                purchase.date = DateTime.Now;
                double price = 0;
                using(AppDBContext db=new AppDBContext())
                {
                    foreach(PurchaseItem item in items)
                    {
                        price += item.total;
                    }
                    purchase.total = price;
                    db.Purchases.Add(purchase);
                    db.SaveChanges();
                    foreach (PurchaseItem item in items)
                    {
                        Product product = db.Products.Where(p => p.Id == item.productId).FirstOrDefault();
                        item.purchaseId = purchase.Id;
                        item.productId = product.Id;
                        item.purchase = purchase;
                        item.product = product;
                        db.PurchaseItems.Add(item);
                        db.SaveChanges();
                        int amount = buy ? item.count : (-item.count);
                        product.state += amount;
                        db.Products.Update(product);
                        db.SaveChanges();
                    }
                    okButton.IsEnabled = false;
                    setButton.IsEnabled = false;
                    clearButton.IsEnabled = false;
                    removeButton.IsEnabled = false;
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Operation successful"] as string), (Application.Current.Resources["Notification"] as string));
                    messageDialog.ShowAsync();
                    TrainerMenu.trainerMenuFrame.Navigate(typeof(SalesView));
                    return;
                }
            }
            else
            {
                MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["No items were added to the list"] as string), (Application.Current.Resources["Error"] as string));
                messageDialog.ShowAsync();
                return;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {

            TrainerMenu.trainerMenuFrame.Navigate(typeof(SalesView));
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            PurchaseItem selectedItem = (PurchaseItem)purchaseItemsList.SelectedItem;
            if(selectedItem!=null)
            {
                items.Remove(selectedItem);
                refreshItems();
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            items.Clear();
            refreshItems();
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            Product selectedProduct = (Product)productsList.SelectedItem;
            if(selectedProduct!=null)
            {
                string amountString = amountBox.Text;
                try
                {
                    int amount = Int32.Parse(amountString);
                    if (amount <= 0) throw new FormatException();
                    int remove = -1;
                    int refresh = -1;
                    if (!buy && (amount > selectedProduct.state)) throw new Exception();
                    foreach (PurchaseItem item in items)
                    {
                        if (item.productId == selectedProduct.Id)
                        {
                            if(amount==0)
                            {
                                remove = items.IndexOf(item);
                                break;
                            }
                            else
                            {
                                item.count = amount;
                                refresh = items.IndexOf(item);
                                break;
                            }
                        }
                    }
                    if(remove!=-1)
                    {
                        items.RemoveAt(remove);
                    }
                    else if(refresh!=-1)
                    {
                        PurchaseItem item = items[refresh];
                        items.RemoveAt(refresh);
                        refreshItems();
                        items.Add(item);
                    }
                    else
                    {
                        PurchaseItem item = new PurchaseItem()
                        {
                            productId = selectedProduct.Id,
                            purchaseId = purchase.Id,
                            count = amount,
                            product = selectedProduct,
                            purchase = purchase
                        };
                        items.Add(item);
                    }
                    refreshItems();
                }
                catch(FormatException ex)
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["Invalid number input"] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
                catch(Exception ex)
                {
                    MessageDialog messageDialog = new MessageDialog((Application.Current.Resources["You do not posess as many items of that kind as you entered"] as string), (Application.Current.Resources["Error"] as string));
                    messageDialog.ShowAsync();
                    return;
                }
            }
        }

        private void StackPanel_GotFocus(object sender, RoutedEventArgs e)
        {
            StackPanel actualSender = (StackPanel)sender;
            currentAmountBlock.Text = ((TextBlock)actualSender.Children[4]).Text;
        }

        private void refreshItems()
        {
            purchaseItemsList.ItemsSource = items;
        }
    }
}
