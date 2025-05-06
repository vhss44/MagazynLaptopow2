using Dapper;
using System.Collections.Generic;
using System;
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
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Laptop>("SELECT * FROM Laptopy", new DynamicParameters());
                return output.ToList();
            }
        }

        public static void ZapiszLaptop(Laptop laptop)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("INSERT INTO Laptopy (NumerSeryjny, Marka, Model, SystemOperacyjny, IloscSztuk) VALUES (@NumerSeryjny, @Marka, @Model, @SystemOperacyjny, @IloscSztuk)", laptop);
            }

        }

        public static void UsunLaptop(string NumerSeryjny)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM Laptopy WHERE NumerSeryjny = @NumerSeryjny", new { NumerSeryjny });
            }
        }


        private static String LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }

}


