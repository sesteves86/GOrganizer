using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.DB
{
    public static class ActiveTyreSupplier
    {
        public static void UpdateTyreSupplier(int tyreSupplierId)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = string.Format(
                    "UPDATE ActiveTyreSupplier SET SupplierId = {0}",
                    tyreSupplierId
                    );

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static int GetTyreSupplierId()
        {
            string connectionString = Constants.connectionString;
            int supplierId = 1;
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT * FROM ActiveTyreSupplier";
                
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                supplierId = int.Parse(reader[1].ToString());

                conn.Close();
            }

            return supplierId;
        }
    }
}
