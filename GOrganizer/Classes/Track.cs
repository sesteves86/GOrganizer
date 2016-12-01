using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer.Classes
{
    public class Track
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DistanceKm { get; set; }
        public int Laps { get; set; }
        public int Power { get; set; }
        public int Handling { get; set; }
        public int Acceleration { get; set; }
        public Levels.HighLow FuelConsumption { get; set; }
        public float FuelConstant { get; set; }
        public Levels.HighLow TyresWear { get; set; }
        public float PitStopTime { get; set; }
        public Levels.HighLow Downforce { get; set; }
        public Levels.EasyHard Overtake { get; set; }
        public Levels.HighLow Suspension { get; set; }
        public Levels.HighLow GripLevel { get; set; }
        public string NormalWingSplit { get; set; }
        public float TDCConstant { get; set; }

        public float ChassisWearConstant { get; set; }
        public float EngineWearConstant { get; set; }
        public float FWingWearConstant { get; set; }
        public float RWingWearConstant { get; set; }
        public float UnderbodyWearConstant { get; set; }
        public float SidepodsWearConstant { get; set; }
        public float CoolingWearConstant { get; set; }
        public float GearboxWearConstant { get; set; }
        public float BrakesWearConstant { get; set; }
        public float SuspensionWearConstant { get; set; }
        public float ElectronicsWearConstant { get; set; }

        public float baseTime { get; set; }

        public Track() { }

        public Track(int _id, string _name, int _distanceKm, int _laps, int _power, int _handling, int _acceleration,
            Levels.HighLow _fuelConsumption, float _fuelConstant, float _pitStopTime, Levels.HighLow _tyresWear,
            Levels.HighLow _downforce, Levels.EasyHard _overtake, Levels.HighLow _suspension, 
            Levels.HighLow _gripLevel, string _ws, float _tdcConstant)
        {
            Id = _id;
            Name = _name;
            DistanceKm = _distanceKm;
            Laps = _laps;
            Power = _power;
            Handling = _handling;
            Acceleration = _acceleration;
            FuelConsumption = _fuelConsumption;
            FuelConstant = _fuelConstant;
            TyresWear = _tyresWear;
            PitStopTime = _pitStopTime;
            Downforce = _downforce;
            Overtake = _overtake;
            Suspension = _suspension;
            GripLevel = _gripLevel;
            NormalWingSplit = _ws;
            TDCConstant = _tdcConstant;
        }

        public void SetWearConstants(float _ChassisWearConstant,
            float _EngineWearConstant,
            float _FWingWearConstant,
            float _RWingWearConstant,
            float _UnderbodyWearConstant,
            float _SidepodsWearConstant,
            float _CoolingWearConstant,
            float _GearboxWearConstant,
            float _BrakesWearConstant,
            float _SuspensionWearConstant,
            float _ElectronicsWearConstant)
        {
            ChassisWearConstant = _ChassisWearConstant;
            EngineWearConstant = _EngineWearConstant;
            FWingWearConstant = _FWingWearConstant;
            RWingWearConstant = _RWingWearConstant;
            UnderbodyWearConstant = _UnderbodyWearConstant;
            SidepodsWearConstant = _SidepodsWearConstant;
            CoolingWearConstant = _CoolingWearConstant;
            GearboxWearConstant = _GearboxWearConstant;
            BrakesWearConstant = _BrakesWearConstant;
            SuspensionWearConstant = _SuspensionWearConstant;
            ElectronicsWearConstant = _ElectronicsWearConstant;
        }

        public Object[] ConvertTrackToObjectArrayForDataTableRace()
        {
            Object[] o = new Object[28];

            o[0] = Id;
            o[1] = Name ;
            o[2] = DistanceKm ;
            o[3] = Laps ;
            o[4] = Power ;
            o[5] = Handling ;
            o[6] = Acceleration ;
            o[7] = FuelConsumption;
            o[8] = FuelConstant;
            o[9] = TyresWear ;
            o[10] = PitStopTime ;
            o[11] = Downforce ;
            o[12] = Overtake;
            o[13] = Suspension;
            o[14] = GripLevel;
            o[15] = NormalWingSplit;
            o[16] = TDCConstant;

            o[17] = ChassisWearConstant;
            o[18] = EngineWearConstant;
            o[19] = FWingWearConstant;
            o[20] = RWingWearConstant;
            o[21] = UnderbodyWearConstant;
            o[22] = SidepodsWearConstant;
            o[23] = CoolingWearConstant;
            o[24] = GearboxWearConstant;
            o[25] = BrakesWearConstant;
            o[26] = SuspensionWearConstant;
            o[27] = ElectronicsWearConstant;

            //o[28] = baseTime;

            return o;
        }
    }
    
    public class Levels
    {
        public enum HighLow
        {
            Very_High,
            High,
            Normal,
            Low,
            Very_Low
        }
        public enum EasyHard
        {
            Very_Easy,
            Easy,
            Normal,
            Hard,
            Very_Hard
        }
    }
}
