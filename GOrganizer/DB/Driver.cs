using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace GOrganizer.DB
{
    static class Driver
    {
        public static void SaveDriverToDb( Classes.Driver driver)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Drivers "+
                        "VALUES (@Concentration, @Talent, @Aggressiveness, @Experience, @TI, " +
                        "@Stamina, @Charisma, @Motivation, @Weight, @Age, @Salary, @RacesLeft, @Energy)";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@Concentration", driver.Concentration);
                    command.Parameters.AddWithValue("@Talent", driver.Talent);
                    command.Parameters.AddWithValue("@Aggressiveness", driver.Aggressiveness);
                    command.Parameters.AddWithValue("@Experience", driver.Experience);
                    command.Parameters.AddWithValue("@TI", driver.TechnicalInsight);
                    command.Parameters.AddWithValue("@Stamina", driver.Stamina);
                    command.Parameters.AddWithValue("@Charisma", driver.Charisma);
                    command.Parameters.AddWithValue("@Motivation", driver.Motivation);
                    command.Parameters.AddWithValue("@Weight", driver.Weight);
                    command.Parameters.AddWithValue("@Age", driver.Age);

                    command.Parameters.AddWithValue("@Salary", driver.Salary);
                    command.Parameters.AddWithValue("@RacesLeft", driver.RacesLeft);
                    command.Parameters.AddWithValue("@Energy", driver.Energy);

                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }
        public static void UpdateDriverToDb( Classes.Driver driver)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "UPDATE Drivers " +
                        "SET Concentration = @Concentration, Talent = @Talent, Aggressiveness = @Aggressiveness, " +
                        "Experience = @Experience, TechnicalInsight = @TI, " +
                        "Stamina = @Stamina, Charisma = @Charisma, Motivation = @Motivation, " + 
                        "Weight = @Weight, Age = @Age, Salary = @Salary, RacesLeft = @RacesLeft, Energy = @Energy " +
                        "WHERE Id = @Id";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@Concentration", driver.Concentration);
                    command.Parameters.AddWithValue("@Talent", driver.Talent);
                    command.Parameters.AddWithValue("@Aggressiveness", driver.Aggressiveness);
                    command.Parameters.AddWithValue("@Experience", driver.Experience);
                    command.Parameters.AddWithValue("@TI", driver.TechnicalInsight);
                    command.Parameters.AddWithValue("@Stamina", driver.Stamina);
                    command.Parameters.AddWithValue("@Charisma", driver.Charisma);
                    command.Parameters.AddWithValue("@Motivation", driver.Motivation);
                    command.Parameters.AddWithValue("@Weight", driver.Weight);
                    command.Parameters.AddWithValue("@Age", driver.Age);

                    command.Parameters.AddWithValue("@Salary", driver.Salary);
                    command.Parameters.AddWithValue("@RacesLeft", driver.RacesLeft);
                    command.Parameters.AddWithValue("@Energy", driver.Energy);

                    command.Parameters.AddWithValue("@Id", driver.Id);

                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
        public static Classes.Driver ReadDriverFromDB()
        {
            string connectionString = Constants.connectionString;
            Classes.Driver driver = new Classes.Driver();

            string query = "SELECT * FROM Drivers WHERE Id=1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        driver.Id = int.Parse(reader[0].ToString());
                        driver.Concentration = int.Parse(reader[1].ToString());
                        driver.Talent = int.Parse(reader[2].ToString());
                        driver.Aggressiveness = int.Parse(reader[3].ToString());
                        driver.Experience = int.Parse(reader[4].ToString());
                        driver.TechnicalInsight = int.Parse(reader[5].ToString());
                        driver.Stamina = int.Parse(reader[6].ToString());
                        driver.Charisma = int.Parse(reader[7].ToString());
                        driver.Motivation = int.Parse(reader[8].ToString());
                        driver.Weight = int.Parse(reader[9].ToString());
                        driver.Age = int.Parse(reader[10].ToString());
                        driver.Salary = int.Parse(reader[11].ToString());
                        driver.RacesLeft = int.Parse(reader[12].ToString());
                        driver.Energy = int.Parse(reader[13].ToString());
                    }

                    conn.Close();
                }
            }

            return driver;
        }
    }
}
