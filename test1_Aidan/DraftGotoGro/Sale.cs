using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DraftGotoGro
{
    public class Sale
    {
        public ObjectId _id { get; set; }
        public int MemberID { get; set; }
        public int OrderNumber { get; set; }
        public List<Item> Items { get; set; }
        public DateTime SaleDate { get; set; }

        public int ItemCount { get { return Items.Count; } }
    }
}
