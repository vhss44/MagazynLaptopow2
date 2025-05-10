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
        public static void SprawdzICzyUzupelnijBaze()
        {
            using (var conn = new SQLiteConnection(LoadConnectionString()))
            {
                conn.Open();

              
                var tableExists = conn.ExecuteScalar<int>(
                    "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='Laptopy'") > 0;

                if (tableExists)
                {
                    
                    var isEmpty = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Laptopy") == 0;

                    if (isEmpty)
                    {
                        
                        using (var transaction = conn.BeginTransaction())
                        {
                            try
                            {
                                conn.Execute(@"
                            INSERT INTO Laptopy (NumerSeryjny, Marka, Model, SystemOperacyjny, IloscSztuk)
                            VALUES 
                                ('SN-001', 'Dell', 'XPS 15', 'Windows', 5),
                                ('SN-002', 'HP', 'ProBook 450', 'Windows', 3),
                                ('SN-003', 'Lenovo', 'ThinkPad T490', 'Windows', 2),
                                ('ADC-20251127', 'HP', 'Omen 16', 'Windows', 1),
                                ('ADC-202511270FC', 'MSI', 'Katana 17', 'Windows', 10),
                                ('MSC-20241045FX', 'Apple', 'MacBook Pro M4 Pro', 'macOS', 16)",
                                    transaction: transaction);

                                transaction.Commit();
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                }
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

        public static void AktualizujLaptop(string staryNumerSeryjny, Laptop nowyLaptop)
        {
            using (var conn = new SQLiteConnection(LoadConnectionString()))
            {
                conn.Open();
                var cmd = new SQLiteCommand(
                    "UPDATE Laptopy SET NumerSeryjny=@NowyNumer, Marka=@Marka, Model=@Model, SystemOperacyjny=@OS, IloscSztuk=@Ilosc WHERE NumerSeryjny=@StaryNumer",
                    conn);

                cmd.Parameters.AddWithValue("@NowyNumer", nowyLaptop.NumerSeryjny);
                cmd.Parameters.AddWithValue("@Marka", nowyLaptop.Marka);
                cmd.Parameters.AddWithValue("@Model", nowyLaptop.Model);
                cmd.Parameters.AddWithValue("@OS", nowyLaptop.SystemOperacyjny);
                cmd.Parameters.AddWithValue("@Ilosc", nowyLaptop.IloscSztuk);
                cmd.Parameters.AddWithValue("@StaryNumer", staryNumerSeryjny);

                cmd.ExecuteNonQuery();
            }
        }
    }

}

