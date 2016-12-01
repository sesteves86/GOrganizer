using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using GOrganizer.Classes;
using System.Windows.Forms;

namespace GOrganizer.DB
{
    public static class Track
    {
        public static void SaveTrackToDb( Classes.Track track)
        {
            string connectionString = Constants.connectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using(SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "INSERT INTO Tracks VALUES (@Name, @DistanceKm, @Laps, @P, @H, @A, " + 
                    "@FuelConsumption, @FuelConstant, @TyresWear, @PitStopTime, @Downforce, " +
                    "@Overtake, @Suspension, @GripLevel, @WS, @TDCConst, " + 
                    "@ChassisWC, @EngineWC, @FWingWC, @RWingWC, @UnderbodyWC, @SidepodsWC, @CoolingWC, " +
                    "@GearboxWC, @BrakesWC, @SuspensionWC, @ElectronicsWC, @BaseTime )";

                command.Parameters.Clear();

                command.Parameters.AddWithValue("@Name", track.Name);
                command.Parameters.AddWithValue("@DistanceKm", track.DistanceKm);
                command.Parameters.AddWithValue("@Laps", track.Laps);
                command.Parameters.AddWithValue("@P", track.Power);
                command.Parameters.AddWithValue("@H", track.Handling);
                command.Parameters.AddWithValue("@A", track.Acceleration);
                command.Parameters.AddWithValue("@FuelConsumption", (int)track.FuelConsumption);
                command.Parameters.AddWithValue("@FuelConstant", track.FuelConstant);
                command.Parameters.AddWithValue("@TyresWear", (int)track.TyresWear);
                command.Parameters.AddWithValue("@PitStopTime", track.PitStopTime);
                command.Parameters.AddWithValue("@Downforce", (int)track.Downforce);
                command.Parameters.AddWithValue("@Overtake", (int)track.Overtake);
                command.Parameters.AddWithValue("@Suspension", (int)track.Suspension);
                command.Parameters.AddWithValue("@GripLevel", (int)track.GripLevel);
                command.Parameters.AddWithValue("@WS", track.NormalWingSplit);
                command.Parameters.AddWithValue("@TDCConst", track.TDCConstant);
                command.Parameters.AddWithValue("@ChassisWC", track.ChassisWearConstant);
                command.Parameters.AddWithValue("@EngineWC", track.EngineWearConstant);
                command.Parameters.AddWithValue("@FWingWC", track.FWingWearConstant);
                command.Parameters.AddWithValue("@RWingWC", track.RWingWearConstant);
                command.Parameters.AddWithValue("@UnderbodyWC", track.UnderbodyWearConstant);
                command.Parameters.AddWithValue("@SidepodsWC", track.SidepodsWearConstant);
                command.Parameters.AddWithValue("@CoolingWC", track.CoolingWearConstant);
                command.Parameters.AddWithValue("@GearboxWC", track.GearboxWearConstant);
                command.Parameters.AddWithValue("@BrakesWC", track.BrakesWearConstant);
                command.Parameters.AddWithValue("@SuspensionWC", track.Suspension);
                command.Parameters.AddWithValue("@ElectronicsWC", track.ElectronicsWearConstant);
                command.Parameters.AddWithValue("@BaseTime", track.baseTime);

                conn.Open();

                command.ExecuteNonQuery();

                conn.Close();
            }

        }
        public static void UpdateTrackToDb( Classes.Track track)
        {
            string connectionString = Constants.connectionString;
            if (track.Id==0)
            {
                MessageBox.Show("Invalid track selected for update. \nPlease first select a track from the grid to update");
                return;
            }

            string query = "UPDATE Tracks ";
            query += string.Format(
                "SET Name = '{0}', DistanceKm = {1}, Laps = {2}, Power = {3}, Handling = {4}, Acceleration={5}, " +
                "FuelConsumption = {6}, FuelConstant = {7}, TyresWear = {8}, PitStopTime = {9}, " +
                "Downforce = {10}, Overtake = {11}, Suspension = {12}, GripLevel = {13}, NormalWingSplit = '{14}'," +
                "TDCConstant = {15}, ",
                track.Name, track.DistanceKm, track.Laps, track.Power, track.Handling, track.Acceleration,
                (int)track.FuelConsumption, track.FuelConstant, (int)track.TyresWear, track.PitStopTime,
                (int)track.Downforce, (int)track.Overtake, (int)track.Suspension, (int)track.GripLevel, track.NormalWingSplit,
                track.TDCConstant);
            query += string.Format(
                "ChassisWearConstant = {0}, EngineWearConstant = {1}, FWingWearConstant = {2}, " +
                "RWingWearConstant = {3}, UnderbodyWearConstant = {4}, " +
                "SidepodsWearConstant = {5}, CoolingWearConstant = {6}, GearboxWearConstant = {7}, " +
                "BrakesWearConstant = {8}, SuspensionWearConstant = {9}, ElectronicsWearConstant = {10}, ",
                track.ChassisWearConstant, track.EngineWearConstant, track.FWingWearConstant,
                track.RWingWearConstant, track.UnderbodyWearConstant,
                track.SidepodsWearConstant, track.CoolingWearConstant, track.GearboxWearConstant,
                track.BrakesWearConstant, track.SuspensionWearConstant, track.ElectronicsWearConstant);
            query += string.Format("BaseTime = {0} ", track.baseTime);
            query += string.Format(
                "WHERE Id = {0}",
                track.Id
                );

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
        public static List<Classes.Track> ReadAllTracksFromDB()
        {
            string connectionString = Constants.connectionString;
            List<Classes.Track> tracksList = new List<Classes.Track>();

            string query = "SELECT * FROM Tracks";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = int.Parse(reader[0].ToString());
                        string name = reader[1].ToString();
                        int distanceKm = int.Parse(reader[2].ToString());
                        int laps = int.Parse(reader[3].ToString());
                        int power = int.Parse(reader[4].ToString());
                        int handling = int.Parse(reader[5].ToString());
                        int acceleration = int.Parse(reader[6].ToString());
                        int fuelConsumption = int.Parse(reader[7].ToString());
                        float fuelConstant = 1;
                            float.TryParse(reader["FuelConstant"].ToString(), out fuelConstant);
                        int tyreWear = int.Parse(reader[9].ToString());
                        Single pitStopTime = Single.Parse(reader[10].ToString());
                        int downforce = int.Parse(reader[11].ToString());
                        int overtake = int.Parse(reader[12].ToString());
                        int suspension = int.Parse(reader[13].ToString());
                        int gripLevel = int.Parse(reader[14].ToString());
                        string WS = reader[15].ToString();
                        float tdcConstant = 0;
                            float.TryParse(reader["TDCConstant"].ToString(), out tdcConstant);

                        float chassisWearConstant = 0;
                            float.TryParse(reader[17].ToString(), out chassisWearConstant);
                        float engineWearConstant = 0;
                            float.TryParse(reader[18].ToString(), out engineWearConstant);
                        float fWingWearConstant = 0;
                            float.TryParse(reader[19].ToString(), out fWingWearConstant);
                        float rWingWearConstant = 0;
                            float.TryParse(reader[20].ToString(), out rWingWearConstant);
                        float underbodyWearConstant = 0;
                            float.TryParse(reader[21].ToString(), out underbodyWearConstant);
                        float sidepodsWearConstant = 0;
                            float.TryParse(reader[22].ToString(), out sidepodsWearConstant);
                        float coolingWearConstant = 0;
                            float.TryParse(reader[23].ToString(), out coolingWearConstant);
                        float gearboxWearConstant = 0;
                            float.TryParse(reader[24].ToString(), out gearboxWearConstant);
                        float brakesWearConstant = 0;
                            float.TryParse(reader[25].ToString(), out brakesWearConstant);
                        float suspensionWearConstant = 0;
                            float.TryParse(reader[26].ToString(), out suspensionWearConstant);
                        float electronicsWearConstant = 0;
                            float.TryParse(reader[27].ToString(), out electronicsWearConstant);

                        float baseTime = 90;
                            float.TryParse(reader["BaseTime"].ToString(), out baseTime);

                        Classes.Track track = new Classes.Track(
                            id, name, distanceKm, laps, power, handling, acceleration,
                            (Levels.HighLow)fuelConsumption, fuelConstant, pitStopTime, (Levels.HighLow)tyreWear, (Levels.HighLow)downforce,
                            (Levels.EasyHard)overtake, (Levels.HighLow)suspension, (Levels.HighLow)gripLevel,
                            WS, tdcConstant);

                        track.SetWearConstants(chassisWearConstant, engineWearConstant, fWingWearConstant,
                            rWingWearConstant, underbodyWearConstant, sidepodsWearConstant, coolingWearConstant,
                            gearboxWearConstant, brakesWearConstant, suspensionWearConstant, electronicsWearConstant);

                        track.baseTime = baseTime;

                        tracksList.Add(track);
                    }

                    conn.Close();
                }
            }

            return tracksList;
        }
        public static Classes.Track ReadTrackFromDB(int trackId)
        {
            string connectionString = Constants.connectionString;
            Classes.Track track = new Classes.Track();

            string query = string.Format("SELECT * FROM Tracks WHERE Id = {0}", trackId);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = int.Parse(reader[0].ToString());
                        string name = reader[1].ToString();
                        int distanceKm = int.Parse(reader[2].ToString());
                        int laps = int.Parse(reader[3].ToString());
                        int power = int.Parse(reader[4].ToString());
                        int handling = int.Parse(reader[5].ToString());
                        int acceleration = int.Parse(reader[6].ToString());
                        int fuelConsumption = int.Parse(reader[7].ToString());
                        float fuelConstant = 0;
                            float.TryParse(reader[8].ToString(), out fuelConstant);
                        int tyreWear = int.Parse(reader[9].ToString());
                        Single pitStopTime = Single.Parse(reader[10].ToString());
                        int downforce = int.Parse(reader[11].ToString());
                        int overtake = int.Parse(reader[12].ToString());
                        int suspension = int.Parse(reader[13].ToString());
                        int gripLevel = int.Parse(reader[14].ToString());
                        string WS = reader[15].ToString();
                        float tdcConstant = 0;
                            float.TryParse(reader[16].ToString(), out tdcConstant);

                        float chassisWearConstant = 0;
                            float.TryParse(reader[17].ToString(), out chassisWearConstant);
                        float engineWearConstant = 0;
                            float.TryParse(reader[18].ToString(), out engineWearConstant);
                        float fWingWearConstant = 0;
                            float.TryParse(reader[19].ToString(), out fWingWearConstant);
                        float rWingWearConstant = 0;
                            float.TryParse(reader[20].ToString(), out rWingWearConstant);
                        float underbodyWearConstant = 0;
                            float.TryParse(reader[21].ToString(), out underbodyWearConstant);
                        float sidepodsWearConstant = 0;
                            float.TryParse(reader[22].ToString(), out sidepodsWearConstant);
                        float coolingWearConstant = 0;
                            float.TryParse(reader[23].ToString(), out coolingWearConstant);
                        float gearboxWearConstant = 0;
                            float.TryParse(reader[24].ToString(), out gearboxWearConstant);
                        float brakesWearConstant = 0;
                            float.TryParse(reader[25].ToString(), out brakesWearConstant);
                        float suspensionWearConstant = 0;
                            float.TryParse(reader[26].ToString(), out suspensionWearConstant);
                        float electronicsWearConstant = 0;
                            float.TryParse(reader[27].ToString(), out electronicsWearConstant);

                        float baseTime = 0;
                            float.TryParse(reader[28].ToString(), out baseTime);

                        track = new Classes.Track(
                            id, name, distanceKm, laps, power, handling, acceleration,
                            (Levels.HighLow)fuelConsumption, fuelConstant, pitStopTime, (Levels.HighLow)tyreWear, (Levels.HighLow)downforce,
                            (Levels.EasyHard)overtake, (Levels.HighLow)suspension, (Levels.HighLow)gripLevel,
                            WS, tdcConstant);

                        track.SetWearConstants(chassisWearConstant, engineWearConstant, fWingWearConstant,
                            rWingWearConstant, underbodyWearConstant, sidepodsWearConstant, coolingWearConstant,
                            gearboxWearConstant, brakesWearConstant, suspensionWearConstant, electronicsWearConstant);

                        track.baseTime = baseTime;
                    }

                    conn.Close();
                }
            }

            return track;
        }
        public static void DeleteTrackFromDB(int trackId)
        {
            string connectionString = Constants.connectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = "DELETE FROM Tracks WHERE Id = @Id";

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Id", trackId);

                conn.Open();

                command.ExecuteNonQuery();

                conn.Close();
            }
        }
    }

    static class SeasonTrack
    {
        public static void UpdateTrackListToDb( List<Classes.Track> tracksList)
        {
            string connectionString = Constants.connectionString;

            for (int i = 0; i < tracksList.Count; i++)
            {
                //Update the track in the DB
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = conn.CreateCommand())
                    {
                        conn.Open();

                        command.CommandText =
                            "UPDATE SeasonTracks " +
                            "SET TrackId = @TrackId " +
                            "WHERE Id = @Id";

                        command.Parameters.Clear();

                        command.Parameters.AddWithValue("@TrackId", tracksList[i].Id == 0 ? 1 : tracksList[i].Id);
                        command.Parameters.AddWithValue("@Id", i+1);

                        command.ExecuteNonQuery();

                        conn.Close();
                    }
                }
            }
        }
        public static List<Classes.Track> ReadTracksListFromDB()
        {
            string connectionString = Constants.connectionString;
            List<Classes.Track> tracksList = new List<Classes.Track>();

            string query = "SELECT * FROM SeasonTracks ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = int.Parse(reader[0].ToString());
                        int trackId = int.Parse(reader[1].ToString());

                        var track = DB.Track.ReadTrackFromDB(trackId);

                        tracksList.Add(track);
                    }

                    conn.Close();
                }
            }

            return tracksList;
        }
        public static int GetTrackId(int id)
        {
            string connectionString = Constants.connectionString;
            int trackId = 1;
            Classes.Track track = new Classes.Track();

            string query = string.Format("SELECT * FROM SeasonTracks WHERE Id = {0}", id);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    trackId = int.Parse(reader[1].ToString());

                    conn.Close();
                }
            }

            return trackId;
        }
    }
}
