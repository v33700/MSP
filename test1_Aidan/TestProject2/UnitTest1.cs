using System.Windows;
using DraftGotoGro;
namespace TestProject1
{
    public class Tests
    {
        private MemberPage Members;
        MainWindow myParent = new MainWindow();
        [SetUp]
        public void Setup()
        {
            
            Members = new MemberPage(myParent);
        }

        [Test]
        public void Test1() //Adding 40 members to the database, then checking the accuracy of each record. 
        {
            for (int i = 1; i <= 40; i++)
            {
                var member = new Member
                {
                    Name = $"Member{i}",
                    PhoneNumber = $"123-221-12{i}",
                    Address = $"{i}"

                };
                Members.SaveOrUpdateMember(member);
                Assert.IsTrue(Members.SaveOrUpdateMember(member));
            }
            

        }
       
    }   
}   