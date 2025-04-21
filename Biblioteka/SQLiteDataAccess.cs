using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class SQLiteDataAccess
    {
        public static List<Laptop> ZaladujLaptopy()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString())) {
                var output = cnn.Query<Laptop>("select * from Laptopy", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void zapiszLaptop(Laptop laptop)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Laptopy (numSer, marka, model, systemOperacyjny, iloscSztuk) values (@numSer, @marka, @model, @systemOperacyjny, @iloscSztuk)", laptop);
            }
            
        }


        private static String LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
    
}
