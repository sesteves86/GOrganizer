using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.DB
{
    static class Weather
    {
        public static void SaveWeatherToDb(Classes.Weather weather)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Weather VALUES (" +
                        "@Q1Temp, @Q1Hum, @Q1Rain, " +
                        "@Q2Temp, @Q2Hum, @Q2Rain, " +
                        "@R1Temp, @R1Hum, @R1Rain, " +
                        "@R2Temp, @R2Hum, @R2Rain, " +
                        "@R3Temp, @R3Hum, @R3Rain, " +
                        "@R4Temp, @R4Hum, @R4Rain " +
                        ")";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@Q1Temp", weather.Q1[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@Q1Hum", weather.Q1[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@Q1Rain", weather.Q1[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@Q2Temp", weather.Q2[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@Q2Hum", weather.Q2[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@Q2Rain", weather.Q2[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R1Temp", weather.R1[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R1Hum", weather.R1[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R1Rain", weather.R1[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R2Temp", weather.R2[(int)Classes.WeatherEnum2.Temp]);
                    command.Parameters.AddWithValue("@R2Hum", weather.R2[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R2Rain", weather.R2[(int)Classes.WeatherEnum2.Rain]);
                    command.Parameters.AddWithValue("@R3Temp", weather.R3[(int)Classes.WeatherEnum2.Temp]);
                    command.Parameters.AddWithValue("@R3Hum", weather.R3[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R3Rain", weather.R3[(int)Classes.WeatherEnum2.Rain]);
                    command.Parameters.AddWithValue("@R4Temp", weather.R4[(int)Classes.WeatherEnum2.Temp]);
                    command.Parameters.AddWithValue("@R4Hum", weather.R4[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R4Rain", weather.R4[(int)Classes.WeatherEnum2.Rain]);
                    
                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }
        public static void UpdateWeatherToDb(Classes.Weather weather)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "UPDATE Weather SET " +
                    "Q1Temp = @Q1Temp, Q1Hum = @Q1Hum, Q1Rain = @Q1Rain, " +
                    "Q2Temp = @Q2Temp, Q2Hum = @Q2Hum, Q2Rain = @Q2Rain, " +
                    "R1Temp = @R1Temp, R1Hum = @R1Hum, R1Rain = @R1Rain, " +
                    "R2Temp = @R2Temp, R2Hum = @R2Hum, R2Rain = @R2Rain, " +
                    "R3Temp = @R3Temp, R3Hum = @R3Hum, R3Rain = @R3Rain, " +
                    "R4Temp = @R4Temp, R4Hum = @R4Hum, R4Rain = @R4Rain " +

                    "WHERE Id = @Id";


                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@Q1Temp", weather.Q1[(int)Classes.WeatherEnum2.Temp]);
                    command.Parameters.AddWithValue("@Q1Hum", weather.Q1[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@Q1Rain", weather.Q1[(int)Classes.WeatherEnum2.Rain]);
                    command.Parameters.AddWithValue("@Q2Temp", weather.Q2[(int)Classes.WeatherEnum2.Temp]);
                    command.Parameters.AddWithValue("@Q2Hum", weather.Q2[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@Q2Rain", weather.Q2[(int)Classes.WeatherEnum2.Rain]);
                    command.Parameters.AddWithValue("@R1Temp", weather.R1[(int)Classes.WeatherEnum2.Temp]);
                    command.Parameters.AddWithValue("@R1Hum", weather.R1[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R1Rain", weather.R1[(int)Classes.WeatherEnum2.Rain]);
                    command.Parameters.AddWithValue("@R2Temp", weather.R2[(int)Classes.WeatherEnum2.Temp]);
                    command.Parameters.AddWithValue("@R2Hum", weather.R2[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R2Rain", weather.R2[(int)Classes.WeatherEnum2.Rain]);
                    command.Parameters.AddWithValue("@R3Temp", weather.R3[(int)Classes.WeatherEnum2.Temp]);
                    command.Parameters.AddWithValue("@R3Hum", weather.R3[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R3Rain", weather.R3[(int)Classes.WeatherEnum2.Rain]);
                    command.Parameters.AddWithValue("@R4Temp", weather.R4[(int)Classes.WeatherEnum2.Temp]);
                    command.Parameters.AddWithValue("@R4Hum", weather.R4[(int)Classes.WeatherEnum2.Hum]);
                    command.Parameters.AddWithValue("@R4Rain", weather.R4[(int)Classes.WeatherEnum2.Rain]);

                    command.Parameters.AddWithValue("@Id", weather.Id);

                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
        public static Classes.Weather ReadWeatherFromDB()
        {
            string connectionString = Constants.connectionString;
            Classes.Weather weather = new Classes.Weather();

            string query = "SELECT * FROM Weather WHERE Id=1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        weather.Id = int.Parse(reader[0].ToString());
                        
                        weather.Q1[(int)Classes.WeatherEnum2.Temp] = int.Parse(reader[1].ToString());
                        weather.Q1[(int)Classes.WeatherEnum2.Hum] = int.Parse(reader[2].ToString());
                        weather.Q1[(int)Classes.WeatherEnum2.Rain] = int.Parse(reader[3].ToString());
                        weather.Q2[(int)Classes.WeatherEnum2.Temp] = int.Parse(reader[4].ToString());
                        weather.Q2[(int)Classes.WeatherEnum2.Hum] = int.Parse(reader[5].ToString());
                        weather.Q2[(int)Classes.WeatherEnum2.Rain] = int.Parse(reader[6].ToString());
                        weather.R1[(int)Classes.WeatherEnum2.Temp] = int.Parse(reader[7].ToString());
                        weather.R1[(int)Classes.WeatherEnum2.Hum] = int.Parse(reader[8].ToString());
                        weather.R1[(int)Classes.WeatherEnum2.Rain] = int.Parse(reader[9].ToString());
                        weather.R2[(int)Classes.WeatherEnum2.Temp] =  int.Parse(reader[10].ToString());
                        weather.R2[(int)Classes.WeatherEnum2.Hum] = int.Parse(reader[11].ToString());
                        weather.R2[(int)Classes.WeatherEnum2.Rain] = int.Parse(reader[12].ToString());
                        weather.R3[(int)Classes.WeatherEnum2.Temp] =  int.Parse(reader[13].ToString());
                        weather.R3[(int)Classes.WeatherEnum2.Hum] = int.Parse(reader[14].ToString());
                        weather.R3[(int)Classes.WeatherEnum2.Rain] = int.Parse(reader[15].ToString());
                        weather.R4[(int)Classes.WeatherEnum2.Temp] =  int.Parse(reader[16].ToString());
                        weather.R4[(int)Classes.WeatherEnum2.Hum] = int.Parse(reader[17].ToString());
                        weather.R4[(int)Classes.WeatherEnum2.Rain] = int.Parse(reader[18].ToString());

                    }

                    conn.Close();
                }
            }

            return weather;
        }
    }
}
