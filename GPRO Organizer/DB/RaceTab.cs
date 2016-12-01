using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.DB
{
    static class RaceTab
    {
        public static void SaveRaceTabToDb(Classes.RaceTab rt)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = 
                        "INSERT INTO RaceTab VALUES (" +
                            "@TrackId, @CustomLaps1, @CustomLaps2, @Compound, @CT, @Temp, @Hum, @Rain " +
                        ")";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@TrackId", rt.SeasonTrackIndex);
                    command.Parameters.AddWithValue("@CustomLaps1", rt.CustomLap1);
                    command.Parameters.AddWithValue("@CustomLaps2", rt.CustomLap2);
                    command.Parameters.AddWithValue("@Compound", rt.Compound);
                    command.Parameters.AddWithValue("@CT", rt.CT);

                    command.Parameters.AddWithValue("@Temp", rt.Temp);
                    command.Parameters.AddWithValue("@Hum", rt.Hum);
                    command.Parameters.AddWithValue("@Rain", rt.Rain);


                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }
        public static void UpdateRaceTabToDb(Classes.RaceTab rt)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    //command.CommandText = "UPDATE Cars SET ChassisLevel=4, ChassisWear=44 WHERE Id=1";

                    command.CommandText = 
                        "UPDATE RaceTab SET " +
                        "TrackId = @TrackId, CustomLaps1 = @CustomLaps1, CustomLaps2 = @CustomLaps2, " +
                        "Compound = @Compound, CT = @CT, " +
                        "Temp = @Temp, Hum = @Hum, Rain = @Rain " +
                        "WHERE Id = @Id";
                    
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@TrackId", rt.SeasonTrackIndex);
                    command.Parameters.AddWithValue("@CustomLaps1", rt.CustomLap1);
                    command.Parameters.AddWithValue("@CustomLaps2", rt.CustomLap2);
                    command.Parameters.AddWithValue("@Compound", rt.Compound);
                    command.Parameters.AddWithValue("@CT", rt.CT);

                    command.Parameters.AddWithValue("@Temp", rt.Temp);
                    command.Parameters.AddWithValue("@Hum", rt.Hum);
                    command.Parameters.AddWithValue("@Rain", rt.Rain);

                    command.Parameters.AddWithValue("@Id", 1);

                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
        public static Classes.RaceTab ReadRaceTabFromDB()
        {
            string connectionString = Constants.connectionString;
            Classes.RaceTab rt = new Classes.RaceTab();

            string query = "SELECT * FROM RaceTab WHERE Id=1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        rt.Id = int.Parse(reader[0].ToString());

                        rt.SeasonTrackIndex = int.Parse(reader[1].ToString());
                        rt.CustomLap1 = int.Parse(reader[2].ToString());
                        rt.CustomLap2 = int.Parse(reader[3].ToString());
                        rt.Compound = int.Parse(reader[4].ToString());
                        rt.CT = int.Parse(reader[5].ToString());

                        rt.Temp = int.Parse(reader[6].ToString());
                        rt.Hum = int.Parse(reader[7].ToString());
                        rt.Rain = bool.Parse(reader[8].ToString());
                    }

                    conn.Close();
                }
            }

            return rt;
        }
    }
}
