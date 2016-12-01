using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.DB
{
    static class Tyres
    {
        //ToDo: Include new Tyre properties to SaveTyre and UpdateTyre methods
        public static void SaveTyreToDb(Classes.TyresSupplier tyre)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Active TyreSupplier VALUES (@Supplier)";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@Supplier", tyre.Supplier);

                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }
        /// <summary>
        /// Stores which tyreSuppliedId is being used
        /// </summary>
        /// <param name="tyre"></param>
        public static void UpdateTyreToDb(int tyreCode)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "UPDATE ActiveTyreSupplier SET " +
                    "SupplierId = @SupplierId " +

                    "WHERE Id = 1";


                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@SupplierId", tyreCode);

                    //command.Parameters.AddWithValue("@Id", tyre.Id);

                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
        public static Classes.TyresSupplier ReadTyreFromSupplierDB(int supplierId)
        {
            string connectionString = Constants.connectionString;
            Classes.TyresSupplier tyre = new Classes.TyresSupplier();

            string query = "SELECT * FROM TyresSupplier WHERE Id=" + supplierId.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        tyre.Id = int.Parse(reader[0].ToString());
                        tyre.Supplier = (Classes.TyreSuppliers)int.Parse(reader[0].ToString()); //reader[1].ToString();
                        tyre.DryPerformance = int.Parse(reader[2].ToString());
                        tyre.WetPerformance = int.Parse(reader[3].ToString());
                        tyre.PeakTemperature = int.Parse(reader[4].ToString());
                        tyre.Durability = int.Parse(reader[5].ToString());
                        tyre.WarmUpDistance = int.Parse(reader[6].ToString());
                        tyre.CostPerRace = int.Parse(reader[7].ToString());
                        tyre.TdcVariable = float.Parse(reader[8].ToString());
                    }

                    conn.Close();
                }
            }

            return tyre;
        }
        public static Classes.TyresSupplier ReadTyreFromDB(string supplier)
        {
            string connectionString = Constants.connectionString;
            Classes.TyresSupplier tyre = new Classes.TyresSupplier();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM TyresSupplier WHERE Supplier = @Supplier";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Supplier", supplier);

                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    {
                        tyre.Id = int.Parse(reader[0].ToString());
                        tyre.Supplier = (Classes.TyreSuppliers)(tyre.Id-1); 
                        tyre.DryPerformance = int.Parse(reader[2].ToString());
                        tyre.WetPerformance = int.Parse(reader[3].ToString());
                        tyre.PeakTemperature = int.Parse(reader[4].ToString());
                        tyre.Durability = int.Parse(reader[5].ToString());
                        tyre.WarmUpDistance = int.Parse(reader[6].ToString());
                        tyre.CostPerRace = int.Parse(reader[7].ToString());
                        tyre.TdcVariable = float.Parse(reader[8].ToString());
                    }

                    conn.Close();
                }
            }

            return tyre;
        }
        public static Classes.TyresSupplier ReadActiveTyreSupplierFromDB()
        {
            Classes.TyresSupplier tyreSupplier = new Classes.TyresSupplier();
            string connectionString = Constants.connectionString;

            int supplierId;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM ActiveTyreSupplier WHERE Id = 1";

                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    reader.Read();
                    supplierId = int.Parse(reader[1].ToString());
                    conn.Close();
                }
            }

            //use Id to get tyreSupplier
            tyreSupplier = ReadTyreFromSupplierDB(supplierId);

            return tyreSupplier;
        }
    }
}
