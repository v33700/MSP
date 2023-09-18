using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftGotoGro
{
    public class Database
    {
        private string _connectionString;

        public string Conn { get { return _connectionString; } }

        Database(string connection) 
        {
            _connectionString = connection;
        }

    }
}
