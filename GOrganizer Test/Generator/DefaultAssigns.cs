using GOrganizer.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOrganizer_Test.Generator
{
    static class Driver
    {
        public static GOrganizer.Classes.Driver GetAverageDriver() {
            GOrganizer.Classes.Driver driver = new GOrganizer.Classes.Driver();
            driver.Age = 25;
            driver.Aggressiveness = 125;
            driver.Charisma = 125;
            driver.Concentration = 125;
            driver.Energy = 125;
            driver.Experience = 125;
            driver.Motivation = 125;
            driver.RacesLeft = 125;
            driver.Reputation = 125;
            driver.Salary = 125;
            driver.Stamina = 125;
            driver.Talent = 125;
            driver.TechnicalInsight = 125;
            driver.Weight = 80;
            
            return driver;
        }
    }

    static class TestTrack
    {
        public static GOrganizer.Classes.Track GetTrack() //Buenos Aires
        {
            //return GOrganizer.DB.Track.ReadTrackFromDB(trackId);  //can't access DB
            GOrganizer.Classes.Track track = new GOrganizer.Classes.Track();
            track.Name = "Buenos Aires";

            track.DistanceKm = 306;
            track.Laps = 72;
            track.Power = 12;
            track.Handling = 11;
            track.Acceleration = 9;
            track.FuelConsumption = 0;
            track.FuelConstant = 1.31f;
            track.TyresWear = Levels.HighLow.High;
            track.PitStopTime = 19.5f;
            track.Downforce = Levels.HighLow.High;
            track.Overtake = Levels.EasyHard.Very_Hard;
            track.Suspension = Levels.HighLow.High;
            track.GripLevel = Levels.HighLow.Normal;
            track.NormalWingSplit = "+20/-20";
            track.TDCConstant = 0.01418f;
            track.ChassisWearConstant = 22.4f;
            track.EngineWearConstant = 39.1f;
            track.FWingWearConstant = 17.5f;
            track.RWingWearConstant = 22.0f;
            track.UnderbodyWearConstant = 16.0f;
            track.SidepodsWearConstant = 13.7f;
            track.CoolingWearConstant = 18.7f;
            track.GearboxWearConstant = 41.1f;
            track.BrakesWearConstant = 51.9f;
            track.SuspensionWearConstant = 36.6f;
            track.ElectronicsWearConstant = 15.1f;

            return track;
        }
    }

    static class Car
    {
        public static GOrganizer.Classes.Car GetAverageCar()
        {
            GOrganizer.Classes.Car car = new GOrganizer.Classes.Car();

            car.BrakesLevel = 5;
            car.ChassisLevel = 5;
            car.CoolingLevel = 5;
            car.ElectronicsLevel = 5;
            car.EngineLevel = 5;
            car.FWingLevel = 5;
            car.GearboxLevel = 5;
            car.RWingLevel = 5;
            car.SidepodsLevel = 5;
            car.SuspensionLevel = 5;
            car.UnderbodyLevel = 5;

            car.BrakesWear = 0;
            car.ChassisWear = 0;
            car.CoolingWear = 0;
            car.ElectronicsWear = 0;
            car.EngineWear = 0;
            car.FWingWear = 0;
            car.GearboxWear = 0;
            car.RWingWear = 0;
            car.SidepodsWear = 0;
            car.SuspensionWear = 0;
            car.UnderbodyWear = 0;

            car.CCPoints = 10;
            car.EngineeringPoints = 10;
            car.RDPoints = 10;
            car.TestPoints = 10;

            return car;
        }
    }
}
