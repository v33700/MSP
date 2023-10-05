using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftGotoGro
{
    public class Sale
    {
        public int MemberID { get; set; }
        public int OrderNumber { get; set; }
        public List<Item> Items { get; set; }
        public DateTime saleDate { get; set; }
    }

}
