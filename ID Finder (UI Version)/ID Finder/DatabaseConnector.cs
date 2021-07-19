using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ID_Finder
{
    class DatabaseConnector
    {

        public static SqliteConnection connection()
        {
            return new SqliteConnection("Data Source=horarios.db");
        }

    }
}
