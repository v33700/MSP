using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftGotoGro
{
    public class Database
    {
        private string _connection;

        public string Conn { get { return _connection; } }

        Database(string connection) 
        {
            _connection = connection;
        }

    }
}
