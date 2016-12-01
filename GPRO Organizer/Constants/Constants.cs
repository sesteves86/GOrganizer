using GOrganizer.Classes;
using System;
using System.Collections.Generic;
using System.IO;

namespace GOrganizer
{
    static public class Constants
    {
        static internal string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""" + Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..")) + @"\DBGOrganizer.mdf"";Integrated Security=True";

        //Season Planner Constants
        static internal float sd = 0.9f; //2f; //0.9f; //Standard deviation for Normal Distribution to guess opponents performance
        static internal int qualCost = 200000; 
        static internal int RaceMaxEarning(Divisions division)
        {
            int maxEarning = 0;

            switch (division)
            {
                case Divisions.Elite:
                    maxEarning = 22000000;
                    break;
                case Divisions.Master:
                    maxEarning = 18000000;
                    break;
                case Divisions.Pro:
                    maxEarning = 15000000;
                    break;
                case Divisions.Amateur:
                    maxEarning = 12000000;
                    break;
                case Divisions.Rookie:
                    maxEarning = 8000000;
                    break;
            }

            return maxEarning;
        }
        static internal int QualMaxEarning(Divisions division)
        {
            int maxEarning = 0;

            switch (division)
            {
                case Divisions.Elite:
                    maxEarning = 2500000;
                    break;
                case Divisions.Master:
                    maxEarning = 2000000;
                    break;
                case Divisions.Pro:
                    maxEarning = 1500000;
                    break;
                case Divisions.Amateur:
                    maxEarning = 1000000;
                    break;
                case Divisions.Rookie:
                    maxEarning = 500000;
                    break;
            }

            return maxEarning;
        }
        static internal List<string> GetDriverTrainningEnumList()
        {
            List<string> trainningList = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                var j = Enum.GetName(typeof(DriverTrainning), i);
                trainningList.Add(j);
            }

            return trainningList;
        }

        //Car
        static internal int GetLvl1CarPartCost(CarPart carPart)
        {
            int cost = 0;

            switch (carPart)
            {
                case CarPart.Chassis:
                    cost = 1292539;
                    break;
                case CarPart.Engine:
                    cost = 3311737;
                    break;
                case CarPart.FWing:
                    cost = 1551354;
                    break;
                case CarPart.RWing:
                    cost = 1504126;
                    break;
                case CarPart.Underbody:
                    cost = 510128;
                    break;
                case CarPart.Sidepods:
                    cost = 459831;
                    break;
                case CarPart.Cooling:
                    cost = 454545;
                    break;
                case CarPart.Gearbox:
                    cost = 3098104;
                    break;
                case CarPart.Brakes:
                    cost = 697674;
                    break;
                case CarPart.Suspension:
                    cost = 1181545;
                    break;
                case CarPart.Electronics:
                    cost = 938416;
                    break;
            }

            return cost;
        }
        static internal float[] GetChassisPHA(int ChassisLevel)
        {
            float p = 0.333f * ChassisLevel;
            float h = 1.667f * ChassisLevel;
            float a = 1.667f * ChassisLevel;

            return new float[3] { p, h, a };
        }
        static internal float[] GetEnginePHA(int EngineLevel)
        {
            float p = 6f * EngineLevel;
            float h = 0.667f * EngineLevel;
            float a = 2.333f * EngineLevel;

            return new float[3] { p, h, a };
        }
        static internal float[] GetFWingPHA(int FWingLevel)
        {
            float p = 0.333f * FWingLevel;
            float h = 3.667f * FWingLevel;
            float a = 0;

            return new float[3] { p, h, a };
        }
        static internal float[] GetRWingPHA(int RWingLevel)
        {
            float p = 0;
            float h = 1.333f * RWingLevel;
            float a = 2.667f * RWingLevel;

            return new float[3] { p, h, a };
        }
        static internal float[] GetUnderbodyPHA(int UnderbodyLevel)
        {
            float p = 0;
            float h = 1 * UnderbodyLevel;
            float a = 0.333f * UnderbodyLevel;

            return new float[3] { p, h, a };
        }
        static internal float[] GetSidepodsPHA(int SidepodsLevel)
        {
            float p = 1f * SidepodsLevel;
            float h = 0;
            float a = 0;

            return new float[3] { p, h, a };
        }
        static internal float[] GetCoolingPHA(int CoolingLevel)
        {
            float p = 1f * CoolingLevel;
            float h = 0;
            float a = 0;

            return new float[3] { p, h, a };
        }
        static internal float[] GetGearboxPHA(int GearboxLevel)
        {
            float p = 3f * GearboxLevel;
            float h = 1f * GearboxLevel;
            float a = 4.5f * GearboxLevel;

            return new float[3] { p, h, a };
        }
        static internal float[] GetBrakesPHA(int BrakesLevel)
        {
            float p = 0;
            float h = 2f * BrakesLevel;
            float a = 0;

            return new float[3] { p, h, a };
        }
        static internal float[] GetSuspensionPHA(int SuspensionLevel)
        {
            float p = 0;
            float h = 2f * SuspensionLevel;
            float a = 1.333f * SuspensionLevel;

            return new float[3] { p, h, a };
        }
        static internal float[] GetElectronicsPHA(int ElectronicsLevel)
        {
            float p = 1f * ElectronicsLevel;
            float h = 0;
            float a = 1.5f * ElectronicsLevel;

            return new float[3] { p, h, a };
        }

        static internal float CarLevelWearConstants(int carLevel)
        {
            float constant = 0;
            switch (carLevel)
            {
                case 1:
                    constant = 0.0225f;
                    break;
                case 2:
                    constant = 0.012f;
                    break;
                case 3:
                    constant = 0.008f;
                    break;
                case 4:
                    constant = 0.006f;
                    break;
                case 5:
                    constant = 0.005f;
                    break;
                case 6:
                    constant = 0.004f;
                    break;
                case 7:
                    constant = 0.005f;
                    break;
                case 8:
                    constant = 0.012f;
                    break;
                case 9:
                    constant = 0.025f;
                    break;
                default:
                    //MessageBox.Show("Incorrect Car Level detected. \n" +
                    //    "It should be an int [1-9] \n" +
                    //    "Using Car level = " + carLevel.ToString());
                    break;
            }

            return constant;
        }

        //Tyre Supplier
        static internal int GetSupplierCost(TyreSuppliers supplier) 
        {
            int cost;
            switch (supplier)
            {
                case TyreSuppliers.Pipirelli:
                    cost = 250000;
                    break;
                case TyreSuppliers.Avonn:
                    cost = 1200000;
                    break;
                case TyreSuppliers.Yokomama:
                    cost = 1500000;
                    break;
                case TyreSuppliers.Dunnolop:
                    cost = 2200000;
                    break;
                case TyreSuppliers.Contimental:
                    cost = 2400000;
                    break;
                case TyreSuppliers.Badyear:
                    cost = 3400000;
                    break;
                case TyreSuppliers.Hancock:
                    cost = 5600000;
                    break;
                case TyreSuppliers.Michelini:
                    cost = 6000000;
                    break;
                case TyreSuppliers.Bridgerock:
                    cost = 7500000;
                    break;
                default:
                    throw new Exception("Error on TyreSupplier.GetSupplierCost(): Invalid argument");
            }

            return cost;
        }

        //Messages
        static internal string qualifyingHelpMessage = "This tab will help you get the best setup for the qualifyings and the race. \n\n"+
            "1- Choose which setup values your using for the practice lap. \n" + 
            "2- Copy-paste the driver's feedback onto the Driver's feedback box. \n" +
            "3- Click on Process practice lap for the program to give a new practice lap setup \n" +
            "\t and suggest the setup to be used on the qualifyings and the race";
        static internal string raceHelpMessage = "This tab will help you to choose the best strategy for the race. \n\n" +
            "1- Select the current track of the season. If the season tracks are wrong, they can be corrected on the SeasonPlanner tab. \n" +
            "2- Choose the tyre coumpound / number combination that best suits the race.\n"+
                "\t Greater shades of red means higher tyre wear (>80%)"+
                "\t The numbers indicate the expected race time in seconds"+
            "3- Use the Personalized Stints box to get fuel and tyre wear of custom stints";
        static internal string updateHelpMessage = "In this tab you can copy-paste your data from the website.\n"+
            "Alternatively, you can adjust the values on the skills tab.";
        static internal string skillsHelpMessage = "In this tab you can adjust the variables from your data. \n" +
            "After making the changes, click Save to save the changes.\n" +
            "To copy-paste entire pages and have the program do the hard work for you, you can use the update tab.";
        static internal string spHelpMessage = "In this tab you can: \n" +
            "1- Select what tracks are present on the present and on the next season.\n" + 
            "2- Have the program to choose the best decisions to be made using the input parameters: \n" +
                "\t Starting money, target number of points and the number of iterations.\n" + 
            "To select the first track, use the dropdown list from the race tab.";
        static internal string trackHelpMessage = "In this tab you can create, update characteristics, read and delete tracks from the Database.";
    }
}
