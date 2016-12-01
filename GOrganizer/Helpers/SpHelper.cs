using GOrganizer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOrganizer.Helpers
{
    static public class SpHelper
    {
        static internal int GetQualPosition(Classes.Driver driver, Classes.Car car, Classes.Divisions division)
        {
            int position = 1;
            float sd = Constants.sd;

            float qPerformance = 80 - 0.03f * driver.GetOA() - 0.18f * car.GetCarLevel();
            float qOpPerformance = 80 - 0.03f * SpHelper.GetAvgOA(division) - 0.18f * SpHelper.GetAvgCarLevel(division);
            position = (int)(40 - 39 * Classes.Maths.NormalDistribution.GetCumulativeDistribution(qPerformance, qOpPerformance, sd));

            position = AssurePositionWithinLimits(position);

            return position;
        }
        static internal int GetRacePosition(Driver driver, Car car, Divisions division, int ct)
        {
            int position = 1;
            float sd = Constants.sd;

            float rPerformance = (80 - 0.03f * driver.GetOA() - 0.18f * car.GetCarLevel())
                / (1f + 0.033f * ct / 100);
            float rOpPerformance = (80 - 0.03f * SpHelper.GetAvgOA(division) - 0.18f * SpHelper.GetAvgCarLevel(division))
                / (1f + 0.033f * SpHelper.GetAvgCT(division) / 100);
            position = (int)(40 - 39 * Classes.Maths.NormalDistribution.GetCumulativeDistribution(rPerformance, rOpPerformance, sd));

            position = AssurePositionWithinLimits(position);

            return position;
        }
        static internal int GetPoints(int racePosition)
        {
            int points = 0;

            switch (racePosition)
            {
                case 1: points = 10;
                    break;
                case 2: points = 8;
                    break;
                case 3: points = 6;
                    break;
                case 4: points = 5;
                    break;
                case 5: points = 4;
                    break;
                case 6: points = 3;
                    break;
                case 7: points = 2;
                    break;
                case 8: points = 1;
                    break;
            }

            return points;
        }
        static internal int GetRaceWinnings(Divisions division, int qualPos, int racePos)
        {
            int qualEarnings = 0;
            int raceEarnings = 0;

            qualEarnings = (int)(Constants.QualMaxEarning(division) * (1 + (1 / 40f)) - qualPos * Constants.QualMaxEarning(division) / 40);
            raceEarnings = (int)(Constants.RaceMaxEarning(division) * (1f + (1 - racePos) / 78f));

            return qualEarnings + raceEarnings;
        }
        static internal int GetAvgCT(Divisions division)
        {
            int ct = 50;
            switch (division)
            {
                case Divisions.Elite: ct = 90;
                    break;
                case Divisions.Master: ct = 80;
                    break;
                case Divisions.Pro: ct = 70;
                    break;
                case Divisions.Amateur: ct = 50;
                    break;
                case Divisions.Rookie: ct = 20;
                    break;
            }
            return ct;
        }
        static internal int GetAvgOA(Divisions division)
        {
            int oa;

            switch (division)
            {
                case Divisions.Elite: oa = 210;
                    break;
                case Divisions.Master: oa = 170;
                    break;
                case Divisions.Pro: oa = 145;
                    break;
                case Divisions.Amateur: oa = 110;
                    break;
                case Divisions.Rookie: oa = 85;
                    break;
                default: oa = 100;
                    break;
            }

            return oa;
        }
        static internal float GetAvgCarLevel(Divisions division)
        {
            float carLvl;

            switch (division)
            {
                case Divisions.Elite: carLvl = 9;
                    break;
                case Divisions.Master: carLvl = 8;
                    break;
                case Divisions.Pro: carLvl = 7;
                    break;
                case Divisions.Amateur: carLvl = 5.5f;
                    break;
                case Divisions.Rookie: carLvl = 2;
                    break;
                default: carLvl = 5;
                    break;
            }

            return carLvl;
        }
        
        static internal object[] ConvertSpTableToRow(Classes.SeasonPlannerForDataTable spTable, SeasonPlannerDecision spDecisions)
        {
            object[] row = new object[32];

            row[0] = spTable.SeasonRaceNumber;

            int trackId = DB.SeasonTrack.GetTrackId(spTable.SeasonRaceNumber);

            row[1] = DB.Track.ReadTrackFromDB(trackId).Name;
            row[2] = spTable.QualPosition;
            row[3] = spTable.RacePosition;
            row[4] = spDecisions.Training;
            row[5] = spDecisions.Testing;
            row[6] = spDecisions.TargetCarLevelEngBra;
            row[7] = spDecisions.TargetCarLevelOthers;
            row[8] = spDecisions.CT;
            row[9] = Math.Round(spTable.BalanceAfterRaceM,2);
            //Car Parts Change
            row[10] = spTable.CarPartsChanged[0];
            row[11] = spTable.CarPartsChanged[1];
            row[12] = spTable.CarPartsChanged[2];
            row[13] = spTable.CarPartsChanged[3];
            row[14] = spTable.CarPartsChanged[4];
            row[15] = spTable.CarPartsChanged[5];
            row[16] = spTable.CarPartsChanged[6];
            row[17] = spTable.CarPartsChanged[7];
            row[18] = spTable.CarPartsChanged[8];
            row[19] = spTable.CarPartsChanged[9];
            row[20] = spTable.CarPartsChanged[10];
            //Car Parts Wear
            row[21] = spTable.CarWearAfterRace[0];
            row[22] = spTable.CarWearAfterRace[1];
            row[23] = spTable.CarWearAfterRace[2];
            row[24] = spTable.CarWearAfterRace[3];
            row[25] = spTable.CarWearAfterRace[4];
            row[26] = spTable.CarWearAfterRace[5];
            row[27] = spTable.CarWearAfterRace[6];
            row[28] = spTable.CarWearAfterRace[7];
            row[29] = spTable.CarWearAfterRace[8];
            row[30] = spTable.CarWearAfterRace[9];
            row[31] = spTable.CarWearAfterRace[10];

            return row;
        }

        static internal SeasonPlannerDecision GenerateRandomDecisionValues(Random r)
        {
            SeasonPlannerDecision spDecision = new SeasonPlannerDecision();

            spDecision.Training = (DriverTrainning)(r.Next(7));
            spDecision.Testing = Convert.ToBoolean(r.Next(2));
            spDecision.TargetCarLevelEngBra = r.Next(7) + 2;
            spDecision.TargetCarLevelOthers = r.Next(7) + 2;
            spDecision.CT = r.Next(100);

            return spDecision;
        }
        static internal SeasonPlannerFullLine ProcessSeasonPlanner(SeasonPlannerFullLine spFull)
        {
            #region  Initial Values
            Classes.RaceTab rt = spFull.spOptimizer.raceTab;
            Classes.SeasonPlannerForDataTable spForDataTable = spFull.spTable; //null. Filled later
            Classes.TechnicalDirector td = spFull.spOptimizer.technicalDirector;
            Classes.Car car = (Classes.Car)spFull.spOptimizer.car.Clone();
            Classes.Car carAfterRace = (Classes.Car)car.Clone();
            Classes.Driver driver = spFull.spOptimizer.driver;
            int trackId = DB.SeasonTrack.GetTrackId(spFull.spTable.SeasonRaceNumber);
            Classes.Track track = DB.Track.ReadTrackFromDB(trackId);
            Classes.StaffFacilities sf = spFull.spOptimizer.staffFacilities;
            Classes.TyresSupplier tyre = DB.Tyres.ReadActiveTyreSupplierFromDB();

            int seasonRaceNumber = rt.SeasonTrackIndex;
            //float balanceM = spFull.spTab.StartingBalanceM;
            float balanceM = spFull.spTable.BalanceAfterRaceM;
            int[] carPartsBought = new int[11];

            Divisions division = (Divisions)spFull.spTab.Division;

            int trainning = (int)spFull.spDecisions.Training;
            bool testing = spFull.spDecisions.Testing;
            int targetCarLevel = spFull.spDecisions.TargetCarLevelEngBra;
            int targetCarLevel2 = spFull.spDecisions.TargetCarLevelOthers;
            int ct = spFull.spDecisions.CT;
            #endregion

            #region Process
            carAfterRace.UpdateCarWearAfterRace(track, driver, ct);

            if (testing)
                carAfterRace.DoTesting(td, track, sf);
            carPartsBought = GetWhichCarPartsChange(car, carAfterRace, targetCarLevel, targetCarLevel2);

            carAfterRace = (Classes.Car)car.Clone();
            carAfterRace = GetCarAfterPartsChangeAndWear(car, testing, track, driver, td, sf, ct, carPartsBought);

            int qualPosition = SpHelper.GetQualPosition(driver, car, division);
            int racePosition = SpHelper.GetRacePosition(driver, car, division, ct);
            int racePoints = SpHelper.GetPoints(racePosition);

            //Expenses processing
            float balanceAfterRaceM = ((balanceM * 1000000) + GetAfterRaceBalance(driver, td, sf, testing, tyre, car, division, qualPosition, racePosition, trainning, carPartsBought))/1000000f;
            #endregion

            #region Output
            SeasonPlannerDecision spDecisionsNext = new SeasonPlannerDecision();
            spDecisionsNext = (Classes.SeasonPlannerDecision)spFull.spDecisions.Clone();

            SeasonPlannerForDataTable spTableNext = new SeasonPlannerForDataTable();
            spTableNext.SeasonRaceNumber = spFull.spTable.SeasonRaceNumber + 1;
            spTableNext.BalanceAfterRaceM = balanceAfterRaceM;
            spTableNext.CarPartsChanged = carPartsBought;
            spTableNext.CarWearAfterRace = carAfterRace.GetWears();
            spTableNext.QualPosition = qualPosition;
            spTableNext.RacePosition = racePosition;

            SeasonPlannerForOptimizer spOptimizerNext = new SeasonPlannerForOptimizer();
            spOptimizerNext.car = (Classes.Car)carAfterRace.Clone();

            driver.DoTrainning((DriverTrainning)trainning);
            driver.DriverUpdateAfterRace(racePosition);
            spOptimizerNext.driver = driver;
            spOptimizerNext.Id = spFull.spOptimizer.Id + 1;
            spOptimizerNext.raceTab = rt; //How to update trackId?
            spOptimizerNext.staffFacilities = sf; //To Do: Update sf after races (convert levels to float type)
            spOptimizerNext.technicalDirector = td;

            SeasonPlannerTab spTabNext = new SeasonPlannerTab();
            spTabNext.CurrentPoints = spFull.spTab.CurrentPoints + racePoints;
            spTabNext.Division = spFull.spTab.Division;
            spTabNext.StartingBalanceM = balanceAfterRaceM;
            spTabNext.TargetPoints = spFull.spTab.TargetPoints;

            SeasonPlannerFullLine spFullNext = new SeasonPlannerFullLine(spDecisionsNext, spTableNext, spOptimizerNext, spTabNext);
            
            return spFullNext;
            #endregion
        }

        static internal int[] GetWhichCarPartsChange(Car car, Car carAfterRace, int targetCarPartLevelEngBra, int targetCarPartLevel)
        {
            int[] carPartsChanged = new int[11];

            carPartsChanged[0] = GetNewCarPart(car.ChassisLevel, carAfterRace.ChassisWear, targetCarPartLevel);
            carPartsChanged[1] = GetNewCarPart(car.EngineLevel, carAfterRace.EngineWear, targetCarPartLevelEngBra);
            carPartsChanged[2] = GetNewCarPart(car.FWingLevel, carAfterRace.FWingWear, targetCarPartLevel);
            carPartsChanged[3] = GetNewCarPart(car.RWingLevel, carAfterRace.RWingWear, targetCarPartLevel);
            carPartsChanged[4] = GetNewCarPart(car.UnderbodyLevel, carAfterRace.UnderbodyWear, targetCarPartLevel);
            carPartsChanged[5] = GetNewCarPart(car.SidepodsLevel, carAfterRace.SidepodsWear, targetCarPartLevel);
            carPartsChanged[6] = GetNewCarPart(car.CoolingLevel, carAfterRace.CoolingWear, targetCarPartLevel);
            carPartsChanged[7] = GetNewCarPart(car.GearboxLevel, carAfterRace.GearboxWear, targetCarPartLevel);
            carPartsChanged[8] = GetNewCarPart(car.BrakesLevel, carAfterRace.BrakesWear, targetCarPartLevelEngBra);
            carPartsChanged[9] = GetNewCarPart(car.SuspensionLevel, carAfterRace.SuspensionWear, targetCarPartLevel);
            carPartsChanged[10] = GetNewCarPart(car.ElectronicsLevel, carAfterRace.ElectronicsWear, targetCarPartLevel);

            return carPartsChanged;
        }
        static private int GetNewCarPart(int carLevel, int afterRaceWear, int targetCarPartLevel)
        {
            int code = 0;

            if (afterRaceWear > 100)
            {
                if (afterRaceWear < 115 && carLevel >= targetCarPartLevel)
                {
                    code = -1; //downgrade 1 level
                }
                else if (afterRaceWear < 130 && carLevel >= targetCarPartLevel + 1)
                {
                    code = -2;
                }
                else if (afterRaceWear < 145 && carLevel >= targetCarPartLevel + 2)
                {
                    code = -3;
                }
                else //buy new car Part
                {
                    if (carLevel <= targetCarPartLevel - 1)
                    {
                        code = carLevel + 1;
                    }
                    else
                    {
                        code = targetCarPartLevel;
                    }
                }
            }

            return code;
        }

        static Classes.Car GetCarAfterPartsChangeAndWear(Classes.Car car, bool testing, Classes.Track track,
            Classes.Driver driver, Classes.TechnicalDirector td, Classes.StaffFacilities sf, int ct, int[] carPartsBought)
        {
            Classes.Car carAfterWear = (Classes.Car)car.Clone(); //all properties are value type

            carAfterWear.UpdateCarWearAndLevelAfterCarUpdate(carPartsBought);

            carAfterWear.UpdateCarWearAfterRace(track, driver, ct);
            if (testing)
                carAfterWear.DoTesting(td, track, sf);

            return carAfterWear;
        }

        static private int AssurePositionWithinLimits(int position)
        {
            int finalPosition=position;

            if (position < 1)
                finalPosition = 1;
            else if (position > 40)
                finalPosition = 40;

            return finalPosition;
        }
        static private int GetAfterRaceBalance(Driver driver, TechnicalDirector td, StaffFacilities sf, bool testing, TyresSupplier tyre, Car car, Divisions division, int qualPosition, int racePosition, int trainning, int[] carPartsBought)
        {
            int qualifyingCost = Constants.qualCost;

            int totalCosts = driver.Salary + driver.TrainningCost((DriverTrainning)trainning) +
                    td.Salary +
                    sf.GetExpenses() +
                    (testing ? 1000000 : 0) +
                    qualifyingCost +
                    tyre.GetActiveSupplierCost() +
                    car.GetCarUpgradesCost(carPartsBought);

            int raceWinnings = SpHelper.GetRaceWinnings(division, qualPosition, racePosition);

            //int balanceAfterRace = (int)(balanceM * 1000000f - totalCosts + raceWinnings);
            int balanceAfterRace = (int)(raceWinnings  - totalCosts );

            return balanceAfterRace;
        }
    }
}
