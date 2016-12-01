using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.DB
{
    static class Car
    {
        public static void SaveCarToDb(Classes.Car car)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Cars VALUES (" +
                        "@ChassisLevel, @ChassisWear, @EngineLevel, @EngineWear, " +
                        "@FWingLevel, @FWingWear, @RWingLevel, @RWingWear, " +
                        "@UnderbodyLevel, @UnderbodyWear, @SidepodsLevel, @SidepodsWear, " +
                        "@CoolingLevel, @CoolingWear, @GearboxLevel, @GearboxWear, " +
                        "@BrakesLevel, @BrakesWear, @SuspensionLevel, @SuspensionWear " +
                        "@ElectronicsLevel, @ElectronicsWear " +
                        ")";

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@ChassisLevel", car.ChassisLevel);
                    command.Parameters.AddWithValue("@ChassisWear", car.ChassisWear);
                    command.Parameters.AddWithValue("@EngineLevel", car.EngineLevel);
                    command.Parameters.AddWithValue("@EngineWear", car.EngineWear);
                    command.Parameters.AddWithValue("@FWingLevel", car.FWingLevel);
                    command.Parameters.AddWithValue("@FWingWear", car.FWingWear);
                    command.Parameters.AddWithValue("@RWingLevel", car.RWingLevel);
                    command.Parameters.AddWithValue("@RWingWear", car.RWingWear);
                    command.Parameters.AddWithValue("@UnderbodyLevel", car.UnderbodyLevel);
                    command.Parameters.AddWithValue("@UnderbodyWear", car.UnderbodyWear);
                    command.Parameters.AddWithValue("@SidepodsLevel", car.SidepodsLevel);
                    command.Parameters.AddWithValue("@SidepodsWear", car.SidepodsWear);
                    command.Parameters.AddWithValue("@CoolingLevel", car.CoolingLevel);
                    command.Parameters.AddWithValue("@CoolingWear", car.CoolingWear);
                    command.Parameters.AddWithValue("@GearboxLevel", car.GearboxLevel);
                    command.Parameters.AddWithValue("@GearboxWear", car.GearboxWear);
                    command.Parameters.AddWithValue("@BrakesLevel", car.BrakesLevel);
                    command.Parameters.AddWithValue("@BrakesWear", car.BrakesWear);
                    command.Parameters.AddWithValue("@SuspensionLevel", car.SuspensionLevel);
                    command.Parameters.AddWithValue("@SuspensionWear", car.SuspensionWear);
                    command.Parameters.AddWithValue("@ElectronicsLevel", car.ElectronicsLevel);
                    command.Parameters.AddWithValue("@ElectronicsWear", car.ElectronicsWear);


                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }

        }
        public static void UpdateCarToDb( Classes.Car car)
        {
            string connectionString = Constants.connectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = conn.CreateCommand())
                {
                    //command.CommandText = "UPDATE Cars SET ChassisLevel=4, ChassisWear=44 WHERE Id=1";

                    command.CommandText = "UPDATE Cars SET " +
                    "ChassisLevel = @ChassisLevel, ChassisWear = @ChassisWear, " +
                    "EngineLevel = @EngineLevel, EngineWear = @EngineWear, " +
                    "FWingLevel = @FWingLevel, FWingWear = @FWingWear, " +
                    "RWingLevel = @RWingLevel, RWingWear = @RWingWear, " +
                    "UnderbodyLevel = @UnderbodyLevel, UnderbodyWear = @UnderbodyWear, " +
                    "SidepodsLevel = @SidepodsLevel, SidepodsWear = @SidepodsWear, " +
                    "CoolingLevel = @CoolingLevel, CoolingWear = @CoolingWear, " +
                    "GearboxLevel = @GearboxLevel, GearboxWear = @GearboxWear, " +
                    "BrakesLevel = @BrakesLevel, BrakesWear = @BrakesWear, " +
                    "SuspensionLevel = @SuspensionLevel, SuspensionWear = @SuspensionWear, " +
                    "ElectronicsLevel = @ElectronicsLevel, ElectronicsWear = @ElectronicsWear " +

                    "WHERE Id = @Id";
                     

                    command.Parameters.Clear();

                    command.Parameters.AddWithValue("@ChassisLevel", car.ChassisLevel);
                    command.Parameters.AddWithValue("@ChassisWear", car.ChassisWear);
                    command.Parameters.AddWithValue("@EngineLevel", car.EngineLevel);
                    command.Parameters.AddWithValue("@EngineWear", car.EngineWear);
                    command.Parameters.AddWithValue("@FWingLevel", car.FWingLevel);
                    command.Parameters.AddWithValue("@FWingWear", car.FWingWear);
                    command.Parameters.AddWithValue("@RWingLevel", car.RWingLevel);
                    command.Parameters.AddWithValue("@RWingWear", car.RWingWear);
                    command.Parameters.AddWithValue("@UnderbodyLevel", car.UnderbodyLevel);
                    command.Parameters.AddWithValue("@UnderbodyWear", car.UnderbodyWear);
                    command.Parameters.AddWithValue("@SidepodsLevel", car.SidepodsLevel);
                    command.Parameters.AddWithValue("@SidepodsWear", car.SidepodsWear);
                    command.Parameters.AddWithValue("@CoolingLevel", car.CoolingLevel);
                    command.Parameters.AddWithValue("@CoolingWear", car.CoolingWear);
                    command.Parameters.AddWithValue("@GearboxLevel", car.GearboxLevel);
                    command.Parameters.AddWithValue("@GearboxWear", car.GearboxWear);
                    command.Parameters.AddWithValue("@BrakesLevel", car.BrakesLevel);
                    command.Parameters.AddWithValue("@BrakesWear", car.BrakesWear);
                    command.Parameters.AddWithValue("@SuspensionLevel", car.SuspensionLevel);
                    command.Parameters.AddWithValue("@SuspensionWear", car.SuspensionWear);
                    command.Parameters.AddWithValue("@ElectronicsLevel", car.ElectronicsLevel);
                    command.Parameters.AddWithValue("@ElectronicsWear", car.ElectronicsWear);

                    command.Parameters.AddWithValue("@Id", car.Id);
                    
                    conn.Open();

                    command.ExecuteNonQuery();

                    conn.Close();
                }
            }
        }
        public static Classes.Car ReadCarFromDB()
        {
            string connectionString = Constants.connectionString;
            Classes.Car car = new Classes.Car();

            string query = "SELECT * FROM Cars WHERE Id=1";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        car.Id = int.Parse(reader[0].ToString());

                        car.ChassisLevel = int.Parse(reader[1].ToString());
                        car.ChassisWear = int.Parse(reader[2].ToString());
                        car.EngineLevel = int.Parse(reader[3].ToString());
                        car.EngineWear = int.Parse(reader[4].ToString());
                        car.FWingLevel = int.Parse(reader[5].ToString());
                        car.FWingWear = int.Parse(reader[6].ToString());
                        car.RWingLevel = int.Parse(reader[7].ToString());
                        car.RWingWear = int.Parse(reader[8].ToString());
                        car.UnderbodyLevel = int.Parse(reader[9].ToString());
                        car.UnderbodyWear = int.Parse(reader[10].ToString());
                        car.SidepodsLevel = int.Parse(reader[11].ToString());
                        car.SidepodsWear = int.Parse(reader[12].ToString());
                        car.CoolingLevel = int.Parse(reader[13].ToString());
                        car.CoolingWear = int.Parse(reader[14].ToString());
                        car.GearboxLevel = int.Parse(reader[15].ToString());
                        car.GearboxWear = int.Parse(reader[16].ToString());
                        car.BrakesLevel = int.Parse(reader[17].ToString());
                        car.BrakesWear = int.Parse(reader[18].ToString());
                        car.SuspensionLevel = int.Parse(reader[19].ToString());
                        car.SuspensionWear = int.Parse(reader[20].ToString());
                        car.ElectronicsLevel = int.Parse(reader[21].ToString());
                        car.ElectronicsWear = int.Parse(reader[22].ToString());
                    }

                    conn.Close();
                }
            }

            return car;
        }
    }
}
