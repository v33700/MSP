using System.Windows;
using DraftGotoGro;
namespace TestProject1
{
    public class UnitTest
    {
        private SalesInput Item;
        MainWindow myParent = new MainWindow();
        [SetUp]
        public void Setup()
        {

            Item = new SalesInput(myParent);
        }

        [Test]
        public void Test1() //This backlog is tested by adding 10 sales data and measure how long it takes for the data to be saved in the database on average. 
        {
            for (int i = 1; i <= 10; i++)
            {
                var member = new Item
                {
                    memberID = "EEEE",
                    orderNumber = $"{i}",
                    itemID = $"{i}",
                    itemQuantity = $"{i}"

                };
                Item.SaveOrUpdateItem(member);
                Assert.IsTrue(SaveOrUpdateItem(member));
            }
        }
        [Test]
        public void Test2()
        {

        }
    }
}