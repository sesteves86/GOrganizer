using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.DB
{
    static class Practise
    {
        static internal void ClearPractise()
        {
            string connectionString = Constants.connectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "DELETE FROM Practise";

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        static internal void UpdatePractise(short[,] setups)
        {
            string connectionString = Constants.connectionString;
            int nLines = setups.GetLength(0);

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "INSERT INTO Practise "+
                    "VALUES (@FWing, @RWing, @Engine, @Breaks, @Gear, @Suspension, " +
                    "@WingsFeedback, @EngineFeedback, @BreaksFeedback, @GearFeedback, @SuspensionFeedback)";
                
                conn.Open();

                for (int line = 0; line < nLines; line++)
                {
                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@FWing", setups[line, 0]);
                    command.Parameters.AddWithValue("@RWing", setups[line, 1]);
                    command.Parameters.AddWithValue("@Engine", setups[line, 2]);
                    command.Parameters.AddWithValue("@Breaks", setups[line, 3]);
                    command.Parameters.AddWithValue("@Gear", setups[line, 4]);
                    command.Parameters.AddWithValue("@Suspension", setups[line, 5]);
                    command.Parameters.AddWithValue("@WingsFeedback", setups[line, 6]);
                    command.Parameters.AddWithValue("@EngineFeedback", setups[line, 7]);
                    command.Parameters.AddWithValue("@BreaksFeedback", setups[line, 8]);
                    command.Parameters.AddWithValue("@GearFeedback", setups[line, 9]);
                    command.Parameters.AddWithValue("@SuspensionFeedback", setups[line, 10]);

                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }

        static internal short[,] ReadPractisesSetups()
        {
            string connectionString = Constants.connectionString;
            short[,] setups = new short[8, 11];
            int line = -1;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using(SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Practise";
                
                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                

                while (reader.Read() && line<8)
                {
                    line++;

                    setups[line, 0] = short.Parse(reader["FWing"].ToString());
                    setups[line, 1] = short.Parse(reader["RWing"].ToString());
                    setups[line, 2] = short.Parse(reader["Engine"].ToString());
                    setups[line, 3] = short.Parse(reader["Breaks"].ToString());
                    setups[line, 4] = short.Parse(reader["Gear"].ToString());
                    setups[line, 5] = short.Parse(reader["Suspension"].ToString());
                    setups[line, 6] = short.Parse(reader["WingsFeedback"].ToString());
                    setups[line, 7] = short.Parse(reader["EngineFeedback"].ToString());
                    setups[line, 8] = short.Parse(reader["BreaksFeedback"].ToString());
                    setups[line, 9] = short.Parse(reader["GearFeedback"].ToString());
                    setups[line, 10] = short.Parse(reader["SuspensionFeedback"].ToString());
                }
                

                conn.Close();
            }

            //Convert Setup to the right size
            short[,] correctedSetups = new short[line+1,11];

            for (int l = 0; l < line+1; l++)
            {
                for (int i = 0; i < 11; i++)
                {
                    correctedSetups[l, i] = setups[l, i];
                }
            }

            return correctedSetups;
        }
    }
}
