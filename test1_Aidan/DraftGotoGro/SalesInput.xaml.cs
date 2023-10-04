using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;


namespace DraftGotoGro
{
    public partial class SalesInput : Page
    {
        List<Item> itemList = new List<Item>();  

        private IMongoDatabase _database;
        private IMongoCollection<Sale> _collection;
        private IMongoCollection<Member> _collectionMember;

        public SalesInput()
        {
            InitializeComponent();

            var client = new MongoClient("mongodb+srv://SWECLASS:IXo4LdFQqKUdJXIr@tomstestcluster.unrd1c2.mongodb.net/"); // MongoDB connection string will add to ppk or pem style key once we know its working
            _database = client.GetDatabase("SWE"); // database name
            _collection = _database.GetCollection<Sale>("Sales");
        }
        private void SubmitButtons(object sender, RoutedEventArgs e)
        {
            if (validatePage()) 
            {
                Sale newSale = new Sale();
                newSale.MemberID = MemberIDBox.Text;
                newSale.OrderNumber = int.Parse(OrderNumber.Text);
                newSale.Items = itemList;
                newSale.SaleDate = DateTime.Now;

                //update sales list in the member from the members table with a matching ID

                var memberFilter = Builders<Member>.Filter.Eq(m => m.Id.ToString(), newSale.MemberID);

                var memberUpdate = Builders<Member>.Update.Push(m => m.Sales, newSale);

                _collectionMember.UpdateOne(memberFilter, memberUpdate);

                _collection.InsertOne(newSale);

                MemberIDBox.Text = "";
                OrderNumber.Text = "";
                ItemID.Text = "";
                ItemQuantity.Text = ""; 

                itemList.Clear();
                ItemListView.Items.Clear(); 
            }
        }
        private void AddToOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (validatePage()) 
            {
                Item Newitem = new Item();
                Newitem.ItemID = int.Parse(ItemID.Text);
                Newitem.ItemQty = int.Parse(ItemQuantity.Text);

                itemList.Add(Newitem);
                ItemListView.Items.Add(Newitem);
            }

        }

        private bool validatePage() 
        {
            bool isValid = false;

            bool valid = int.TryParse(OrderNumber.Text, out _); 

            if (MemberIDBox.Text == "" || OrderNumber.Text == "" || ItemID.Text == "" || ItemQuantity.Text == "")
            {
                SaleErrorLabel.Content = "Please fill all the data.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return isValid;
            }
            if (!int.TryParse(OrderNumber.Text, out _)) 
            {
                SaleErrorLabel.Content = "Order number is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return isValid;
            }
            if (!int.TryParse(ItemID.Text, out _))
            {
                SaleErrorLabel.Content = "Item ID is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return isValid;
            }
            if (!int.TryParse(ItemQuantity.Text, out _))
            {
                SaleErrorLabel.Content = "Item Quantity is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return isValid;
            }

            return true;
        }

        private void regex() 
        {
            string pattern = "^[A-Z]{4}d{2}"; //Four capital letters and 2 digit numbers
            bool isMatch = Regex.IsMatch(MemberIDBox.Text, pattern);
            if (!isMatch)
            {
                SaleErrorLabel.Content = "Member ID is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            pattern = "^d{5}"; //5 numbers
            isMatch = Regex.IsMatch(OrderNumber.Text, pattern);
            if (!isMatch)
            {
                SaleErrorLabel.Content = "Order number is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            pattern = "^d{3}"; //3 numbers
            isMatch = Regex.IsMatch(ItemID.Text, pattern);
            if (!isMatch)
            {
                SaleErrorLabel.Content = "Item ID is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
            pattern = "^d{2}"; //2 numbers
            isMatch = Regex.IsMatch(ItemQuantity.Text, pattern);
            if (!isMatch)
            {
                SaleErrorLabel.Content = "Item Quantity is not of a valid format. Please try again.";
                SaleErrorLabel.Visibility = Visibility.Visible;
                return;
            }
        }
    }

}
