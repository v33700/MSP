using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;
using System.Text.Json;
using System.Reflection;

namespace DraftGotoGro
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        private ObservableCollection<Member> MemberResult = new ObservableCollection<Member>();
        private ObservableCollection<Sale> SaleResult = new ObservableCollection<Sale>();

        private IMongoDatabase _database;
        private IMongoCollection<Member> _memberCollection;

        private IMongoCollection<Sale> _saleCollection; // Add this to the class definition

        public Report()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb+srv://SWECLASS:IXo4LdFQqKUdJXIr@tomstestcluster.unrd1c2.mongodb.net/"); // TODO: Secure the connection string!
            _database = client.GetDatabase("SWE");
            _memberCollection = _database.GetCollection<Member>("Members");
            _saleCollection = _database.GetCollection<Sale>("Sales"); // Initialize the sale collection
        }

        private void MemberReportBtn_Click(object sender, RoutedEventArgs e)
        {
            List<Member> membersFromSales = null;
            List<object> combinedData = new List<object>(); // List to store combined member and sales data

            FilterDefinition<Sale> filter = null;

            if (WeekRadio.IsChecked == true)
            {
                DateTime oneWeekAgo = DateTime.UtcNow.AddDays(-7);
                filter = Builders<Sale>.Filter.Gte(s => s.SaleDate, oneWeekAgo);
            }
            else if (MonthRadio.IsChecked == true)
            {
                DateTime oneMonthAgo = DateTime.UtcNow.AddDays(-30);
                filter = Builders<Sale>.Filter.Gte(s => s.SaleDate, oneMonthAgo);
            }
            else if (AllRadio.IsChecked == true)
            {
                filter = Builders<Sale>.Filter.Empty; // This will match all documents in the collection
            }

            if (filter != null)
            {
                var sales = _saleCollection.Find(filter).ToList();
                var uniqueMemberIds = sales.Select(s => s.MemberID).Distinct().ToList();
                var memberFilter = Builders<Member>.Filter.In(m => m._id, uniqueMemberIds);
                membersFromSales = _memberCollection.Find(memberFilter).ToList();

                foreach (var member in membersFromSales)
                {
                    var memberSales = sales.Where(s => s.MemberID == member._id).ToList();
                    combinedData.Add(new
                    {
                        Member = member,
                        Sales = memberSales
                    });
                }
                string jsonData = JsonSerializer.Serialize(combinedData);
                SaveToFile(jsonData, true);  // Pass true for combined data

            }
        }


        private void SaleReportBtn_Click(object sender, RoutedEventArgs e)
        {
            if (FromDate.SelectedDate.HasValue && ToDate.SelectedDate.HasValue)
            {
                DateTime startDate = FromDate.SelectedDate.Value;
                DateTime endDate = ToDate.SelectedDate.Value;

                if (startDate > endDate)
                {
                    MessageBox.Show("Start date should be earlier than end date.");
                    return;
                }

                var defaultFilter = Builders<Sale>.Filter.And(
                    Builders<Sale>.Filter.Ne(s => s.MemberID, 0),
                    Builders<Sale>.Filter.Ne(s => s.SaleDate, new DateTime(0001, 01, 01))
                );

                var dateFilter = Builders<Sale>.Filter.And(
                    Builders<Sale>.Filter.Gte(s => s.SaleDate, startDate),
                    Builders<Sale>.Filter.Lte(s => s.SaleDate, endDate)
                );

                var combinedFilter = Builders<Sale>.Filter.And(defaultFilter, dateFilter);

                var sales = _saleCollection.Find(combinedFilter).ToList();
                sales.RemoveAll(s => s.MemberID == 0 || s.SaleDate == new DateTime(0001, 01, 01));

                string jsonData = JsonSerializer.Serialize(sales);
                SaveToFile(jsonData, false);  // Pass false for only sales data
            }
            else
            {
                MessageBox.Show("Please select both start and end dates.");
            }
        }



        private string AskForFileName()
        {
            return CsvFileNameTextBox.Text;
        }

        private void SaveToFile(string jsonData, bool isCombined = false)
        {
            string filename = AskForFileName();
            if (string.IsNullOrEmpty(filename))
            {
                MessageBox.Show("Please enter a valid filename.");
                return;
            }

            while (File.Exists(filename))
            {
                MessageBoxResult result = MessageBox.Show($"File {filename} already exists. Would you like to choose another name?", "File Exists", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
                    dlg.FileName = "Document";
                    dlg.DefaultExt = ".csv";
                    dlg.Filter = "CSV documents (.csv)|*.csv";
                    bool? dialogResult = dlg.ShowDialog();

                    if (dialogResult == true)
                    {
                        filename = dlg.FileName;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            CSVGEN csvGenerator = new CSVGEN(jsonData, isCombined);
            csvGenerator.ToCsv(filename);
            MessageBox.Show("CSV has been successfully saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

}

