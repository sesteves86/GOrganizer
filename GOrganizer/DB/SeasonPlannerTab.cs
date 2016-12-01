using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.DB
{
    public static class SeasonPlannerTab
    {
        internal static void UpdateSeasonPlannerTab(Classes.SeasonPlannerTab spTab)
        {
            string connectionString = Constants.connectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "UPDATE SeasonPlannerTab " +
                    " SET Division = @Division, StartingMoneyM = @StartingMoneyM, " + 
                    " TargetPoints = @TargetPoints, nRuns = @nRuns";

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Division", spTab.Division);
                command.Parameters.AddWithValue("@StartingMoneyM", spTab.StartingBalanceM);
                command.Parameters.AddWithValue("@TargetPoints", spTab.TargetPoints);
                command.Parameters.AddWithValue("@nRuns", spTab.nRuns);

                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
            }
        }
        internal static Classes.SeasonPlannerTab ReadSeasonPlannerTabFromDB()
        {
            string connectionString = Constants.connectionString;

            Classes.SeasonPlannerTab spTab = new Classes.SeasonPlannerTab();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT * FROM SeasonPlannerTab";

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                reader.Read();

                spTab.Division = short.Parse(reader["Division"].ToString());
                spTab.StartingBalanceM = int.Parse(reader["StartingMoneyM"].ToString());
                spTab.TargetPoints = int.Parse(reader["TargetPoints"].ToString());
                spTab.nRuns = int.Parse(reader["nRuns"].ToString());

                conn.Close();
            }

            return spTab;
        }

        //Repeated code
        internal static void UpdateSpDecision(Classes.SeasonPlannerDecision spDecision)
        {
            if (!spDecision.IsNull())
            {
                string connectionString = Constants.connectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "UPDATE SeasonPlannerDecisions " +
                                "SET Trainning = @Trainning, Testing = @Testing, " +
                                "TargetCarLevelEngBra = @TargetCarLevelEngBra, " +
                                "TargetCarLevelOthers = @TargetCarLevelOthers, CT = @CT " +
                                "WHERE Id = @Id";

                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Id", spDecision.SeasonRace);
                    command.Parameters.AddWithValue("@Trainning", spDecision.Training);
                    command.Parameters.AddWithValue("@Testing", spDecision.Testing);
                    command.Parameters.AddWithValue("@TargetCarLevelEngBra", spDecision.TargetCarLevelEngBra);
                    command.Parameters.AddWithValue("@TargetCarLevelOthers", spDecision.TargetCarLevelOthers);
                    command.Parameters.AddWithValue("@CT", spDecision.CT);

                    conn.Open();
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        internal static void UpdateSeasonPlannerDecisions(Classes.SeasonPlannerDecision[] spDecisions)
        {
            string connectionString = Constants.connectionString;
            int length = spDecisions.Count();

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "UPDATE SeasonPlannerDecisions "+
                    "SET Trainning = @Trainning, Testing=@Testing, TargetCarLevelEngBra = @TargetCarLevelEngBra, " +
                    "TargetCarLevelOthers = @TargetCarLevelOthers, CT = @CT " + 
                    "WHERE Id = @Id";
                
                conn.Open();
                for (int i = 0; i < length; i++)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Trainning", spDecisions[i].Training);
                    command.Parameters.AddWithValue("@Testing", spDecisions[i].Testing);
                    command.Parameters.AddWithValue("@TargetCarLevelEngBra", spDecisions[i].TargetCarLevelEngBra);
                    command.Parameters.AddWithValue("@TargetCarLevelOthers", spDecisions[i].TargetCarLevelOthers);
                    command.Parameters.AddWithValue("@CT", spDecisions[i].CT);
                    command.Parameters.AddWithValue("@Id", spDecisions[i].SeasonRace );
                    command.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        internal static Classes.SeasonPlannerDecision[] ReadSpDecisionsFromDB(int initialRace = 0)
        {
            Classes.SeasonPlannerDecision[] spDecisions = new Classes.SeasonPlannerDecision[17];

            string connectionString = Constants.connectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using(SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "SELECT * FROM SeasonPlannerDecisions";

                conn.Open();

                SqlDataReader reader = command.ExecuteReader();

                for(int line=initialRace; line<17; line++) { 
                    reader.Read();
                    
                    spDecisions[line] = new Classes.SeasonPlannerDecision();
                    spDecisions[line].SeasonRace = int.Parse(reader["Id"].ToString());
                    spDecisions[line].Training = (Classes.DriverTrainning) Enum.Parse( typeof( Classes.DriverTrainning), reader["Trainning"].ToString());
                    spDecisions[line].Testing = Convert.ToBoolean(reader["Testing"].ToString());
                    spDecisions[line].TargetCarLevelEngBra = int.Parse(reader["TargetCarLevelEngBra"].ToString());
                    spDecisions[line].TargetCarLevelOthers = int.Parse(reader["TargetCarLevelOthers"].ToString());
                    spDecisions[line].CT = int.Parse(reader["CT"].ToString());
                    
                }

                conn.Close();
            }

            return spDecisions;
        }
        
    }
}
