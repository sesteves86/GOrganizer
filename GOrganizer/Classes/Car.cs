using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOrganizer.Classes
{
    public class Car:ICloneable
    {
        public int Id { get; set; }

        public int ChassisLevel { get; set; }
        public int ChassisWear { get; set; }
        public int EngineLevel { get; set; }
        public int EngineWear { get; set; }
        public int FWingLevel { get; set; }
        public int FWingWear { get; set; }
        public int RWingLevel { get; set; }
        public int RWingWear { get; set; }
        public int UnderbodyLevel { get; set; }
        public int UnderbodyWear { get; set; }
        public int SidepodsLevel { get; set; }
        public int SidepodsWear { get; set; }
        public int CoolingLevel { get; set; }
        public int CoolingWear { get; set; }
        public int GearboxLevel { get; set; }
        public int GearboxWear { get; set; }
        public int BrakesLevel { get; set; }
        public int BrakesWear { get; set; }
        public int SuspensionLevel { get; set; }
        public int SuspensionWear { get; set; }
        public int ElectronicsLevel { get; set; }
        public int ElectronicsWear { get; set; }

        //Going to be passed to a Testing Class
        public float TestPoints { get; set; }
        public float RDPoints { get; set; }
        public float EngineeringPoints { get; set; }
        public float CCPoints { get; set; }

        //Methods
        public void UpdateCarWearAfterRace(Track track, Driver driver, int CTRiskPc)
        {
            ChassisWear += CarPartWearAfterRace(track.ChassisWearConstant, ChassisLevel, driver, CTRiskPc);
            EngineWear += CarPartWearAfterRace(track.EngineWearConstant, EngineLevel, driver, CTRiskPc);
            FWingWear += CarPartWearAfterRace(track.FWingWearConstant, FWingLevel, driver, CTRiskPc);
            RWingWear += CarPartWearAfterRace(track.RWingWearConstant, RWingLevel, driver, CTRiskPc);
            UnderbodyWear += CarPartWearAfterRace(track.UnderbodyWearConstant, UnderbodyLevel, driver, CTRiskPc);
            SidepodsWear += CarPartWearAfterRace(track.SidepodsWearConstant, SidepodsLevel, driver, CTRiskPc);
            CoolingWear += CarPartWearAfterRace(track.CoolingWearConstant, CoolingLevel, driver, CTRiskPc);
            GearboxWear += CarPartWearAfterRace(track.GearboxWearConstant, GearboxLevel, driver, CTRiskPc);
            BrakesWear += CarPartWearAfterRace(track.BrakesWearConstant, BrakesLevel, driver, CTRiskPc);
            SuspensionWear += CarPartWearAfterRace(track.SuspensionWearConstant, SuspensionLevel, driver, CTRiskPc);
            ElectronicsWear += CarPartWearAfterRace(track.ElectronicsWearConstant, ElectronicsLevel, driver, CTRiskPc);
        }
        internal void UpdateCarWearAndLevelAfterCarUpdate(int[] carPartsBought)
        {
            for (int i = 0; i < 11; i++)//foreach car part
            {
                int newCarPart = carPartsBought[i];

                if (newCarPart > 0) //New Car Part
                {
                    switch (i)
                    {
                        case 0:
                            ChassisLevel = newCarPart > ChassisLevel - 1 ? newCarPart : ChassisLevel + 1;
                            ChassisWear = 0;
                            break;
                        case 1:
                            EngineLevel = newCarPart > EngineLevel - 1 ? newCarPart : EngineLevel + 1;
                            EngineWear = 0;
                            break;
                        case 2:
                            FWingLevel = newCarPart > FWingLevel - 1 ? newCarPart : FWingLevel + 1;
                            FWingWear = 0;
                            break;
                        case 3:
                            RWingLevel = newCarPart > RWingLevel - 1 ? newCarPart : RWingLevel + 1;
                            RWingWear = 0;
                            break;
                        case 4:
                            UnderbodyLevel = newCarPart > UnderbodyLevel - 1 ? newCarPart : UnderbodyLevel + 1;
                            UnderbodyWear = 0;
                            break;
                        case 5:
                            SidepodsLevel = newCarPart > SidepodsLevel - 1 ? newCarPart : SidepodsLevel + 1;
                            SidepodsWear = 0;
                            break;
                        case 6:
                            CoolingLevel = newCarPart > CoolingLevel - 1 ? newCarPart : CoolingLevel + 1;
                            CoolingWear = 0;
                            break;
                        case 7:
                            GearboxLevel = newCarPart > GearboxLevel - 1 ? newCarPart : GearboxLevel + 1;
                            GearboxWear = 0;
                            break;
                        case 8:
                            BrakesLevel = newCarPart > BrakesLevel - 1 ? newCarPart : BrakesLevel + 1;
                            BrakesWear = 0;
                            break;
                        case 9:
                            SuspensionLevel = newCarPart > SuspensionLevel - 1 ? newCarPart : SuspensionLevel + 1;
                            SuspensionWear = 0;
                            break;
                        case 10:
                            ElectronicsLevel = newCarPart > ElectronicsLevel - 1 ? newCarPart : ElectronicsLevel + 1;
                            ElectronicsWear = 0;
                            break;
                    }
                }
                else if (newCarPart < 0) //Downgrade
                {
                    switch (i)
                    {
                        case 0:
                            ChassisLevel += newCarPart;
                            ChassisWear += newCarPart * 15;
                            break;
                        case 1:
                            EngineLevel += newCarPart;
                            EngineWear += newCarPart * 15;
                            break;
                        case 2:
                            FWingLevel += newCarPart;
                            FWingWear += newCarPart * 15;
                            break;
                        case 3:
                            RWingLevel += newCarPart;
                            RWingWear += newCarPart * 15;
                            break;
                        case 4:
                            UnderbodyLevel += newCarPart;
                            UnderbodyWear += newCarPart * 15;
                            break;
                        case 5:
                            SidepodsLevel += newCarPart;
                            SidepodsWear += newCarPart * 15;
                            break;
                        case 6:
                            CoolingLevel += newCarPart;
                            CoolingWear += newCarPart * 15;
                            break;
                        case 7:
                            GearboxLevel += newCarPart;
                            GearboxWear += newCarPart * 15;
                            break;
                        case 8:
                            BrakesLevel += newCarPart;
                            BrakesWear += newCarPart * 15;
                            break;
                        case 9:
                            SuspensionLevel += newCarPart;
                            SuspensionWear += newCarPart * 15;
                            break;
                        case 10:
                            ElectronicsLevel += newCarPart;
                            ElectronicsWear += newCarPart * 15;
                            break;

                    }
                }
            }
        }
        internal void DoTesting(Classes.TechnicalDirector td, Classes.Track track, Classes.StaffFacilities sf)
        {
            UpdateCarWearAfterTesting(td, sf);

            CCPoints = CCPoints * 0.95f + EngineeringPoints * ( 2.1f );
            EngineeringPoints = RDPoints * (73f + 0.03f * sf.Technicalskill + 0.02f * sf.Efficiency + 0.02f * sf.RDworkshop + 0.02f * sf.Engineeringworkshop + 0.05f * sf.Alloyandchemicallab);
            RDPoints = TestPoints * (80 + 0.7f + td.Experience * .01f + td.RDaerodynamics * 0.01f + td.RDelectronics * 0.01f + td.RDmechanics * 0.01f +
                sf.Technicalskill * 0.2f + 0.1f * sf.Efficiency + 0.2f * sf.Windtunnel);
            TestPoints = 0;
        }
        internal float GetCarLevel()
        {

            float sumPHA = 0;
            for (int i = 0; i < 3; i++)
            {
                sumPHA += Constants.GetChassisPHA(ChassisLevel)[i] + Constants.GetEnginePHA(EngineLevel)[i] + Constants.GetFWingPHA(FWingLevel)[i] + Constants.GetRWingPHA(RWingLevel)[i];
                sumPHA += Constants.GetUnderbodyPHA(UnderbodyLevel)[i] + Constants.GetSidepodsPHA(SidepodsLevel)[i] + Constants.GetCoolingPHA(CoolingLevel)[i] + Constants.GetGearboxPHA(GearboxLevel)[i];
                sumPHA += Constants.GetBrakesPHA(BrakesLevel)[i] + Constants.GetSuspensionPHA(SuspensionLevel)[i] + Constants.GetElectronicsPHA(ElectronicsLevel)[i];
            }

            float carLevel = (sumPHA + CCPoints) / 40;

            return carLevel;
        }
        internal int GetCarUpgradesCost(int[] boughtCarPartsLevels)
        {
            int cost = 0;

            for (int i = 0; i < 11; i++)
            {
                if (boughtCarPartsLevels[i] > 0) //if a new car part was bought
                {
                    cost += (int)(Constants.GetLvl1CarPartCost((CarPart)i) * Math.Pow(1.2385, (boughtCarPartsLevels[i] - 1)));
                }
            }

            return cost;
        }
        internal int[] GetWears()
        {
            int[] wears = new int[11];

            wears[0] = ChassisWear;
            wears[1] = EngineWear;
            wears[2] = FWingWear;
            wears[3] = RWingWear;
            wears[4] = UnderbodyWear;
            wears[5] = SidepodsWear;
            wears[6] = CoolingWear;
            wears[7] = GearboxWear;
            wears[8] = BrakesWear;
            wears[9] = SuspensionWear;
            wears[10] = ElectronicsWear;

            return wears;
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        //Private Methods
        private int CarPartWearAfterRace(float carPartWearConstant, int carPartLvl, Driver driver, int CTRiskPc)
        {
            float raceWear = carPartWearConstant *
                    (1 + 0.0008f * driver.Aggressiveness) *
                    (1 + CTRiskPc * Constants.CarLevelWearConstants(carPartLvl));

            float r2 = (1 + 0.0008f * driver.Concentration) *
                    (1 + 0.0004f * driver.Talent) *
                    (1 + 0.0006f * driver.Experience) *
                    (1 + 0.0002f * driver.Stamina);

            float r3 = raceWear / r2;

            return (int)Math.Round(r3,0)+1;
        }
        //Inaccurate values here, wear should be a function of the track
        private void UpdateCarWearAfterTesting(Classes.TechnicalDirector td, Classes.StaffFacilities sf)
        {
            ChassisWear += 10;
            EngineWear += 10;
            FWingWear += 10;
            RWingWear += 10;
            UnderbodyWear += 10;
            SidepodsWear += 10;
            CoolingWear += 10;
            GearboxWear += 10;
            BrakesWear += 10;
            SuspensionWear += 10;
            ElectronicsWear += 10;
        }
    }

    public enum CarPart
    {
        Chassis,
        Engine,
        FWing,
        RWing,
        Underbody,
        Sidepods,
        Cooling,
        Gearbox,
        Brakes,
        Suspension,
        Electronics
    }
}
