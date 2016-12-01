using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GOrganizer.DB
{
    static class TechnicalDirector
    {
        public static void SaveTdToDb(Classes.TechnicalDirector td)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO TechnicalDirectors VALUES (" +
                        "@Leadership, @RDmechanics, @RDelectronics, @RDaerodynamics, @Experience, " +
                        "@Pitcoordination, @Motivation,	@Age, @Salary, @RacesLeft " +
                        ")";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@Leadership", td.Leadership);
                    command.Parameters.AddWithValue("@RDmechanics", td.RDmechanics);
                    command.Parameters.AddWithValue("@RDelectronics", td.RDelectronics);
                    command.Parameters.AddWithValue("@RDaerodynamics", td.RDaerodynamics);
                    command.Parameters.AddWithValue("@Experience", td.Experience);
                    command.Parameters.AddWithValue("@Pitcoordination", td.Pitcoordination);
                    command.Parameters.AddWithValue("@Motivation", td.Motivation);
                    command.Parameters.AddWithValue("@Age", td.Age);
                    command.Parameters.AddWithValue("@Salary", td.Salary);
                    command.Parameters.AddWithValue("@RacesLeft", td.RacesLeft);
                    
                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }
        public static void UpdateTdToDb(Classes.TechnicalDirector td)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "UPDATE TechnicalDirectors SET " +
                        "Leadership = @Leadership, " +
                        "RDmechanics = @RDmechanics, " +
                        "RDelectronics = @RDelectronics, " +
                        "RDaerodynamics = @RDaerodynamics, " +
                        "Experience = @Experience, " +
                        "Pitcoordination = @Pitcoordination, " +
                        "Motivation = @Motivation, " +
                        "Age = @Age, " +
                        "Salary = @Salary, " +
                        "RacesLeft = @RacesLeft " +
                        "WHERE Id = @Id";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@Leadership", td.Leadership);
                    command.Parameters.AddWithValue("@RDmechanics", td.RDmechanics);
                    command.Parameters.AddWithValue("@RDelectronics", td.RDelectronics);
                    command.Parameters.AddWithValue("@RDaerodynamics", td.RDaerodynamics);
                    command.Parameters.AddWithValue("@Experience", td.Experience);
                    command.Parameters.AddWithValue("@Pitcoordination", td.Pitcoordination);
                    command.Parameters.AddWithValue("@Motivation", td.Motivation);
                    command.Parameters.AddWithValue("@Age", td.Age);
                    command.Parameters.AddWithValue("@Salary", td.Salary);
                    command.Parameters.AddWithValue("@RacesLeft", td.RacesLeft);

                    command.Parameters.AddWithValue("@Id", td.Id);

                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
        public static Classes.TechnicalDirector ReadTdFromDB()
        {
            string connectionString = Constants.connectionString;
            Classes.TechnicalDirector td = new Classes.TechnicalDirector();

            string query = "SELECT * FROM TechnicalDirectors WHERE Id=1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        td.Id = int.Parse(reader[0].ToString());

                        td.Leadership = int.Parse(reader[1].ToString());
                        td.RDmechanics = int.Parse(reader[2].ToString());
                        td.RDelectronics = int.Parse(reader[3].ToString());
                        td.RDaerodynamics = int.Parse(reader[4].ToString());
                        td.Experience = int.Parse(reader[5].ToString());
                        td.Pitcoordination = int.Parse(reader[6].ToString());
                        td.Motivation = int.Parse(reader[7].ToString());
                        td.Age = int.Parse(reader[8].ToString());
                        td.Salary = int.Parse(reader[9].ToString());
                        td.RacesLeft = int.Parse(reader[10].ToString());
                    }

                    conn.Close();
                }
            }

            return td;
        }
    }
}
