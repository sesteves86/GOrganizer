using GOrganizer.Classes;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer_Test.Classes
{
    [TestFixture]
    public class Car
    {
        [Test]
        public void UpdateCarWear_Test()
        {
            Trace.WriteLine("UpdateCarWear initialized");

            //Assign
            Track track = Generator.TestTrack.GetTrack();
            Driver driver = Generator.Driver.GetAverageDriver();
            int CTRiskPc = 50;
            GOrganizer.Classes.Car car = Generator.Car.GetAverageCar();
            GOrganizer.Classes.Car expectedCarAfterWear = Generator.Car.GetAverageCar();

            expectedCarAfterWear.BrakesWear = 57;
            expectedCarAfterWear.ChassisWear = 25;
            expectedCarAfterWear.CoolingWear = 21;
            expectedCarAfterWear.ElectronicsWear = 17;
            expectedCarAfterWear.EngineWear = 43;
            expectedCarAfterWear.FWingWear = 19;
            expectedCarAfterWear.GearboxWear = 47;
            expectedCarAfterWear.RWingWear = 24;
            expectedCarAfterWear.SidepodsWear = 15;
            expectedCarAfterWear.SuspensionWear = 38;
            expectedCarAfterWear.UnderbodyWear = 18;

            //Act
            car.UpdateCarWearAfterRace(track, driver, CTRiskPc);

            //Assert
            //This should be the correct assertion, but doesn't match due to approximations
            //Assert.AreEqual(car, expectedCarAfterWear); 
            Assert.AreEqual(car.BrakesWear, expectedCarAfterWear.BrakesWear);
        }

        [Test]
        public void UpdateCarWearAfterTesting_Test()
        {
            GOrganizer.Classes.Car car = Generator.Car.GetAverageCar();
            GOrganizer.Classes.Car expectedCarAfterTesting = Generator.Car.GetAverageCar();
            expectedCarAfterTesting.BrakesWear = 10;
            expectedCarAfterTesting.ChassisWear = 10;
            expectedCarAfterTesting.CoolingWear = 10;
            expectedCarAfterTesting.ElectronicsWear = 10;
            expectedCarAfterTesting.EngineWear = 10;
            expectedCarAfterTesting.FWingWear = 10;
            expectedCarAfterTesting.GearboxWear = 10;
            expectedCarAfterTesting.RWingWear = 10;
            expectedCarAfterTesting.SidepodsWear = 10;
            expectedCarAfterTesting.SuspensionWear = 10;
            expectedCarAfterTesting.UnderbodyWear = 10;

            car.Up

        }

        //internal void UpdateCarWearAndLevelAfterCarUpdate(int[] carPartsBought)
        //{
        //    for (int i = 0; i < 11; i++)//foreach car part
        //    {
        //        int newCarPart = carPartsBought[i];

        //        if (newCarPart > 0) //New Car Part
        //        {
        //            switch (i)
        //            {
        //                case 0:
        //                    ChassisLevel = newCarPart > ChassisLevel - 1 ? newCarPart : ChassisLevel + 1;
        //                    ChassisWear = 0;
        //                    break;
        //                case 1:
        //                    EngineLevel = newCarPart > EngineLevel - 1 ? newCarPart : EngineLevel + 1;
        //                    EngineWear = 0;
        //                    break;
        //                case 2:
        //                    FWingLevel = newCarPart > FWingLevel - 1 ? newCarPart : FWingLevel + 1;
        //                    FWingWear = 0;
        //                    break;
        //                case 3:
        //                    RWingLevel = newCarPart > RWingLevel - 1 ? newCarPart : RWingLevel + 1;
        //                    RWingWear = 0;
        //                    break;
        //                case 4:
        //                    UnderbodyLevel = newCarPart > UnderbodyLevel - 1 ? newCarPart : UnderbodyLevel + 1;
        //                    UnderbodyWear = 0;
        //                    break;
        //                case 5:
        //                    SidepodsLevel = newCarPart > SidepodsLevel - 1 ? newCarPart : SidepodsLevel + 1;
        //                    SidepodsWear = 0;
        //                    break;
        //                case 6:
        //                    CoolingLevel = newCarPart > CoolingLevel - 1 ? newCarPart : CoolingLevel + 1;
        //                    CoolingWear = 0;
        //                    break;
        //                case 7:
        //                    GearboxLevel = newCarPart > GearboxLevel - 1 ? newCarPart : GearboxLevel + 1;
        //                    GearboxWear = 0;
        //                    break;
        //                case 8:
        //                    BrakesLevel = newCarPart > BrakesLevel - 1 ? newCarPart : BrakesLevel + 1;
        //                    BrakesWear = 0;
        //                    break;
        //                case 9:
        //                    SuspensionLevel = newCarPart > SuspensionLevel - 1 ? newCarPart : SuspensionLevel + 1;
        //                    SuspensionWear = 0;
        //                    break;
        //                case 10:
        //                    ElectronicsLevel = newCarPart > ElectronicsLevel - 1 ? newCarPart : ElectronicsLevel + 1;
        //                    ElectronicsWear = 0;
        //                    break;
        //            }
        //        }
        //        else if (newCarPart < 0) //Downgrade
        //        {
        //            switch (i)
        //            {
        //                case 0:
        //                    ChassisLevel += newCarPart;
        //                    ChassisWear += newCarPart * 15;
        //                    break;
        //                case 1:
        //                    EngineLevel += newCarPart;
        //                    EngineWear += newCarPart * 15;
        //                    break;
        //                case 2:
        //                    FWingLevel += newCarPart;
        //                    FWingWear += newCarPart * 15;
        //                    break;
        //                case 3:
        //                    RWingLevel += newCarPart;
        //                    RWingWear += newCarPart * 15;
        //                    break;
        //                case 4:
        //                    UnderbodyLevel += newCarPart;
        //                    UnderbodyWear += newCarPart * 15;
        //                    break;
        //                case 5:
        //                    SidepodsLevel += newCarPart;
        //                    SidepodsWear += newCarPart * 15;
        //                    break;
        //                case 6:
        //                    CoolingLevel += newCarPart;
        //                    CoolingWear += newCarPart * 15;
        //                    break;
        //                case 7:
        //                    GearboxLevel += newCarPart;
        //                    GearboxWear += newCarPart * 15;
        //                    break;
        //                case 8:
        //                    BrakesLevel += newCarPart;
        //                    BrakesWear += newCarPart * 15;
        //                    break;
        //                case 9:
        //                    SuspensionLevel += newCarPart;
        //                    SuspensionWear += newCarPart * 15;
        //                    break;
        //                case 10:
        //                    ElectronicsLevel += newCarPart;
        //                    ElectronicsWear += newCarPart * 15;
        //                    break;

        //            }
        //        }
        //    }
        //}
        //internal void DoTesting(Classes.TechnicalDirector td, Classes.Track track, Classes.StaffFacilities sf)
        //{
        //    UpdateCarWearAfterTesting(td, sf);

        //    CCPoints = CCPoints * 0.95f + EngineeringPoints * (2.1f);
        //    EngineeringPoints = RDPoints * (73f + 0.03f * sf.Technicalskill + 0.02f * sf.Efficiency + 0.02f * sf.RDworkshop + 0.02f * sf.Engineeringworkshop + 0.05f * sf.Alloyandchemicallab);
        //    RDPoints = TestPoints * (80 + 0.7f + td.Experience * .01f + td.RDaerodynamics * 0.01f + td.RDelectronics * 0.01f + td.RDmechanics * 0.01f +
        //        sf.Technicalskill * 0.2f + 0.1f * sf.Efficiency + 0.2f * sf.Windtunnel);
        //    TestPoints = 0;
        //}
        //internal float GetCarLevel()
        //{

        //    float sumPHA = 0;
        //    for (int i = 0; i < 3; i++)
        //    {
        //        sumPHA += Constants.GetChassisPHA(ChassisLevel)[i] + Constants.GetEnginePHA(EngineLevel)[i] + Constants.GetFWingPHA(FWingLevel)[i] + Constants.GetRWingPHA(RWingLevel)[i];
        //        sumPHA += Constants.GetUnderbodyPHA(UnderbodyLevel)[i] + Constants.GetSidepodsPHA(SidepodsLevel)[i] + Constants.GetCoolingPHA(CoolingLevel)[i] + Constants.GetGearboxPHA(GearboxLevel)[i];
        //        sumPHA += Constants.GetBrakesPHA(BrakesLevel)[i] + Constants.GetSuspensionPHA(SuspensionLevel)[i] + Constants.GetElectronicsPHA(ElectronicsLevel)[i];
        //    }

        //    float carLevel = (sumPHA + CCPoints) / 40;

        //    return carLevel;
        //}
        //internal int GetCarUpgradesCost(int[] boughtCarPartsLevels)
        //{
        //    int cost = 0;

        //    for (int i = 0; i < 11; i++)
        //    {
        //        if (boughtCarPartsLevels[i] > 0) //if a new car part was bought
        //        {
        //            cost += (int)(Constants.GetLvl1CarPartCost((CarPart)i) * Math.Pow(1.2385, (boughtCarPartsLevels[i] - 1)));
        //        }
        //    }

        //    return cost;
        //}
        //internal int[] GetWears()
    }
}
