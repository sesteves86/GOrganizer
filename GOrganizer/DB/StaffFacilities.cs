using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.DB
{
    static class StaffFacilities
    {
        public static void SaveStaffFacilitiesToDb(Classes.StaffFacilities sf)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO StaffFacilities VALUES (" +
                        "@Experience, @Motivation, @Technicalskill, @Stresshandling, @Concentration, " +
                        "@Efficiency, @Windtunnel,	@Pitstoptrainingcentre, @RDWorkshop, @RDdesigncenter " +
                        "@Engineeringworkshop, @Alloyandchemicallab, @Commercial" + 
                        ")";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@", sf.Experience);
                    command.Parameters.AddWithValue("@", sf.Motivation);
                    command.Parameters.AddWithValue("@", sf.Technicalskill);
                    command.Parameters.AddWithValue("@", sf.Stresshandling);
                    command.Parameters.AddWithValue("@", sf.Concentration);
                    command.Parameters.AddWithValue("@", sf.Efficiency);
                    command.Parameters.AddWithValue("@", sf.Windtunnel);
                    command.Parameters.AddWithValue("@", sf.Pitstoptrainingcenter);
                    command.Parameters.AddWithValue("@", sf.RDworkshop);
                    command.Parameters.AddWithValue("@", sf.RDdesigncenter);
                    command.Parameters.AddWithValue("@", sf.Engineeringworkshop);
                    command.Parameters.AddWithValue("@", sf.Alloyandchemicallab);
                    command.Parameters.AddWithValue("@", sf.Commercial);


                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }
        public static void UpdateStaffFacilitiesToDb(Classes.StaffFacilities sf)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "UPDATE StaffFacilities SET " +
                        "Experience = @Experience, " +
                        "Motivation = @Motivation, " +
                        "Technicalskill = @Technicalskill, " +
                        "Stresshandling = @Stresshandling, " +
                        "Concentration = @Concentration, " +
                        "Efficiency = @Efficiency, " +
                        "Windtunnel = @Windtunnel, " +
                        "Pitstoptrainingcenter = @Pitstoptrainingcenter, " +
                        "RDworkshop = @RDworkshop, " +
                        "RDdesigncenter = @RDdesigncenter, " +
                        "Engineeringworkshop = @Engineeringworkshop, " +
                        "Alloyandchemicallab = @Alloyandchemicallab, " +
                        "Commercial = @Commercial " +

                        "WHERE Id = @Id";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@Experience", sf.Experience);
                    command.Parameters.AddWithValue("@Motivation", sf.Motivation);
                    command.Parameters.AddWithValue("@Technicalskill", sf.Technicalskill);
                    command.Parameters.AddWithValue("@Stresshandling", sf.Stresshandling);
                    command.Parameters.AddWithValue("@Concentration", sf.Concentration);
                    command.Parameters.AddWithValue("@Efficiency", sf.Efficiency);
                    command.Parameters.AddWithValue("@Windtunnel", sf.Windtunnel);
                    command.Parameters.AddWithValue("@Pitstoptrainingcenter", sf.Pitstoptrainingcenter);
                    command.Parameters.AddWithValue("@RDworkshop", sf.RDworkshop);
                    command.Parameters.AddWithValue("@RDdesigncenter", sf.RDdesigncenter);
                    command.Parameters.AddWithValue("@Engineeringworkshop", sf.Engineeringworkshop);
                    command.Parameters.AddWithValue("@Alloyandchemicallab", sf.Alloyandchemicallab);
                    command.Parameters.AddWithValue("@Commercial", sf.Commercial);
                    
                    command.Parameters.AddWithValue("@Id", sf.Id);

                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
        public static Classes.StaffFacilities ReadStaffFacilitiesFromDB()
        {
            string connectionString = Constants.connectionString;
            Classes.StaffFacilities sf = new Classes.StaffFacilities();

            string query = "SELECT * FROM StaffFacilities WHERE Id=1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        sf.Id = int.Parse(reader[0].ToString());

                        sf.Experience = int.Parse(reader[1].ToString());
                        sf.Motivation = int.Parse(reader[2].ToString());
                        sf.Technicalskill = int.Parse(reader[3].ToString());
                        sf.Stresshandling = int.Parse(reader[4].ToString());
                        sf.Concentration = int.Parse(reader[5].ToString());
                        sf.Efficiency = int.Parse(reader[6].ToString());
                        sf.Windtunnel = int.Parse(reader[7].ToString());
                        sf.Pitstoptrainingcenter = int.Parse(reader[8].ToString());
                        sf.RDworkshop = int.Parse(reader[9].ToString());
                        sf.RDdesigncenter = int.Parse(reader[10].ToString());
                        sf.Engineeringworkshop = int.Parse(reader[11].ToString());
                        sf.Alloyandchemicallab = int.Parse(reader[12].ToString());
                        sf.Commercial = int.Parse(reader[13].ToString());
                    }

                    conn.Close();
                }
            }

            return sf;
        }
    }
}
