using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace DraftGotoGro
{
    public class Sale
    {
        /*[JsonIgnore]*/
        public ObjectId _id { get; set; }
        public int MemberID { get; set; }
        public int OrderNumber { get; set; }
        public List<Item> Items { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
