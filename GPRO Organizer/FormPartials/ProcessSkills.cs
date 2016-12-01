using GOrganizer.Classes;
using GOrganizer.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOrganizer
{
    public partial class Form1 : Form
    {
        void InitiateSkillsTab()
        {
            Classes.Driver driver = DB.Driver.ReadDriverFromDB();
            FillDriver(driver);
            Classes.Car car = DB.Car.ReadCarFromDB();
            FillCar(car);
            Classes.TechnicalDirector td = DB.TechnicalDirector.ReadTdFromDB();
            FillTd(td);
            Classes.StaffFacilities sf = DB.StaffFacilities.ReadStaffFacilitiesFromDB();
            FillStaffFacilities(sf);
            UpdateStaffAndFacilitiesCost(sf);
            Classes.Weather w = DB.Weather.ReadWeatherFromDB();
            FillWeather(w);
            int activeTyreSupplierId = DB.ActiveTyreSupplier.GetTyreSupplierId();
            Classes.TyresSupplier tyre = DB.Tyres.ReadTyreFromSupplierDB(activeTyreSupplierId);
            FillActiveTyreSupplier(tyre);
        }
        void FillDriver(Classes.Driver driver)
        {
            //Pass driver to Skills tab
            txtDriverEnergy.Text = (driver.Energy).ToString();

            txtDriverConcentration.Text = (driver.Concentration).ToString();
            txtDriverTalent.Text = (driver.Talent).ToString();
            txtDriverAgg.Text = (driver.Aggressiveness).ToString();
            txtDriverExperience.Text = (driver.Experience).ToString();
            txtDriverTI.Text = (driver.TechnicalInsight).ToString();
            txtDriverStamina.Text = (driver.Stamina).ToString();
            txtDriverCharisma.Text = (driver.Charisma).ToString();
            txtDriverMotivation.Text = (driver.Motivation).ToString();

            txtDriverWeight.Text = (driver.Weight).ToString();
            txtDriverAge.Text = (driver.Age).ToString();
            txtDriverSalary.Text = (driver.Salary).ToString();
            txtDriverRacesLeft.Text = (driver.RacesLeft).ToString();
            txtDriverReputation.Text = (driver.Reputation).ToString();

            CalculateDriverOA(driver);
        }

        void FillCar(Classes.Car car)
        {
            txtChassisLevel.Text = car.ChassisLevel.ToString();
            txtChassisWear.Text = car.ChassisWear.ToString();
            txtEngineLevel.Text = car.EngineLevel.ToString();
            txtEngineWear.Text = car.EngineWear.ToString();
            txtFWingLevel.Text = car.FWingLevel.ToString();
            txtFWingWear.Text = car.FWingWear.ToString();
            txtRWingLevel.Text = car.RWingLevel.ToString();
            txtRWingWear.Text = car.RWingWear.ToString();
            txtUnderbodyLevel.Text = car.UnderbodyLevel.ToString();
            txtUnderbodyWear.Text = car.UnderbodyWear.ToString();
            txtSidepodsLevel.Text = car.SidepodsLevel.ToString();
            txtSidepodsWear.Text = car.SidepodsWear.ToString();
            txtCoolingLevel.Text = car.CoolingLevel.ToString();
            txtCoolingWear.Text = car.CoolingWear.ToString();
            txtGearboxLevel.Text = car.GearboxLevel.ToString();
            txtGearboxWear.Text = car.GearboxWear.ToString();
            txtBrakesLevel.Text = car.BrakesLevel.ToString();
            txtBrakesWear.Text = car.BrakesWear.ToString();
            txtSuspensionLevel.Text = car.SuspensionLevel.ToString();
            txtSuspensionWear.Text = car.SuspensionWear.ToString();
            txtElectronicsLevel.Text = car.ElectronicsLevel.ToString();
            txtElectronicsWear.Text = car.ElectronicsWear.ToString();
        }
        void FillTd(Classes.TechnicalDirector td)
        {
            txtTDLeadership.Text = td.Leadership.ToString();
            txtTDRDMechanics.Text = td.RDmechanics.ToString();
            txtTDRDElectronics.Text = td.RDelectronics.ToString();
            txtTDRDAerodynamics.Text = td.RDaerodynamics.ToString();
            txtTDExperience.Text = td.Experience.ToString();
            txtTDPitCoordination.Text = td.Pitcoordination.ToString();
            txtTDMotivation.Text = td.Motivation.ToString();
            txtTDAge.Text = td.Age.ToString();
            txtTDSalary.Text = td.Salary.ToString();
            txtTDRaces.Text = td.RacesLeft.ToString();
        }
        void FillStaffFacilities(Classes.StaffFacilities sf)
        {
            txtStaffExperience.Text = sf.Experience.ToString();
            txtStaffMotivation.Text = sf.Motivation.ToString();
            txtStaffTSkills.Text = sf.Technicalskill.ToString();
            txtStaffStressHandling.Text = sf.Stresshandling.ToString();
            txtStaffConcentration.Text = sf.Concentration.ToString();
            txtStaffEfficiency.Text = sf.Efficiency.ToString();
            txtFacilitiesWindtunnel.Text = sf.Windtunnel.ToString();
            txtFacilitiesPitstop.Text = sf.Pitstoptrainingcenter.ToString();
            txtFacilitiesRDWorkshop.Text = sf.RDworkshop.ToString();
            txtFacilitiesRDDesign.Text = sf.RDdesigncenter.ToString();
            txtFacilitiesEngineering.Text = sf.Engineeringworkshop.ToString();
            txtFacilitiesAlloy.Text = sf.Alloyandchemicallab.ToString();
            txtFacilitiesCommercial.Text = sf.Commercial.ToString();

        }
        void FillWeather(Classes.Weather weather)
        {
            txtWeatherQ1Temp.Text = weather.Q1[(int)Classes.WeatherEnum2.Temp].ToString();
            txtWeatherQ1Hum.Text = weather.Q1[(int)Classes.WeatherEnum2.Hum].ToString();
            txtWeatherQ1Rain.Text = weather.Q1[(int)Classes.WeatherEnum2.Rain].ToString();
            txtWeatherQ2Temp.Text = weather.Q2[(int)Classes.WeatherEnum2.Temp].ToString();
            txtWeatherQ2Hum.Text = weather.Q2[(int)Classes.WeatherEnum2.Hum].ToString();
            txtWeatherQ2Rain.Text = weather.Q2[(int)Classes.WeatherEnum2.Rain].ToString();

            txtWeatherR1Temp.Text = weather.R1[(int)Classes.WeatherEnum2.Temp].ToString();
            txtWeatherR1Hum.Text = weather.R1[(int)Classes.WeatherEnum2.Hum].ToString();
            txtWeatherR1Rain.Text = weather.R1[(int)Classes.WeatherEnum2.Rain].ToString();
            txtWeatherR2Temp.Text = weather.R2[(int)Classes.WeatherEnum2.Temp].ToString();
            txtWeatherR2Hum.Text = weather.R2[(int)Classes.WeatherEnum2.Hum].ToString();
            txtWeatherR2Rain.Text = weather.R2[(int)Classes.WeatherEnum2.Rain].ToString();
            txtWeatherR3Temp.Text = weather.R3[(int)Classes.WeatherEnum2.Temp].ToString();
            txtWeatherR3Hum.Text = weather.R3[(int)Classes.WeatherEnum2.Hum].ToString();
            txtWeatherR3Rain.Text = weather.R3[(int)Classes.WeatherEnum2.Rain].ToString();
            txtWeatherR4Temp.Text = weather.R4[(int)Classes.WeatherEnum2.Temp].ToString();
            txtWeatherR4Hum.Text = weather.R4[(int)Classes.WeatherEnum2.Hum].ToString();
            txtWeatherR4Rain.Text = weather.R4[(int)Classes.WeatherEnum2.Rain].ToString();

            UpdateRaceTabWeather(weather);
        }
        void FillActiveTyreSupplier(Classes.TyresSupplier tyre)
        {
            //int tyreSupplierId = DB.ActiveTyreSupplier.GetTyreSupplierId();
            cBoxSkillsTyresSupplier.Text = tyre.GetActiveSupplierName();
        }

        void SaveSkills()//testing car
        {
            //Read values from form
            Classes.Car car = ReadCarFromForm();
            Classes.Driver driver = ReadDriverFromForm();
            Classes.TechnicalDirector td = ReadTdFromForm();
            Classes.StaffFacilities sf = ReadSfFromForm();
            Classes.Weather weather = ReadWeatherFromForm();
            int activeTyreCode = GetTyreCodeFromSkillTab();

            //Call the Save methods
            DB.Car.UpdateCarToDb(car);
            DB.Driver.UpdateDriverToDb( driver);
            DB.TechnicalDirector.UpdateTdToDb( td);
            DB.StaffFacilities.UpdateStaffFacilitiesToDb( sf);
            DB.Weather.UpdateWeatherToDb( weather);
            DB.Tyres.UpdateTyreToDb( activeTyreCode);
        }
        
        public Classes.Car ReadCarFromForm()
        {
            Classes.Car car = new Classes.Car();
            
            car.BrakesLevel = int.Parse(txtBrakesLevel.Text);
            car.BrakesWear = int.Parse(txtBrakesWear.Text); ;
            car.ChassisLevel = int.Parse(txtChassisLevel.Text);
            car.ChassisWear = int.Parse(txtChassisWear.Text);
            car.CoolingLevel = int.Parse(txtCoolingLevel.Text);
            car.CoolingWear = int.Parse(txtCoolingWear.Text);
            car.ElectronicsLevel = int.Parse(txtElectronicsLevel.Text);
            car.ElectronicsWear = int.Parse(txtElectronicsWear.Text);
            car.EngineLevel = int.Parse(txtEngineLevel.Text);
            car.EngineWear = int.Parse(txtEngineWear.Text);
            car.FWingLevel = int.Parse(txtFWingLevel.Text);
            car.FWingWear = int.Parse(txtFWingWear.Text);
            car.GearboxLevel = int.Parse(txtGearboxLevel.Text);
            car.GearboxWear = int.Parse(txtGearboxWear.Text);
            car.RWingLevel = int.Parse(txtRWingLevel.Text);
            car.RWingWear = int.Parse(txtRWingWear.Text);
            car.SidepodsLevel = int.Parse(txtSidepodsLevel.Text);
            car.SidepodsWear = int.Parse(txtSidepodsWear.Text);
            car.SuspensionLevel = int.Parse(txtSuspensionLevel.Text);
            car.SuspensionWear = int.Parse(txtSuspensionWear.Text);
            car.UnderbodyLevel = int.Parse(txtUnderbodyLevel.Text);
            car.UnderbodyWear = int.Parse(txtUnderbodyWear.Text);

            car.Id = 1;

            return car;
        }
        Classes.Driver ReadDriverFromForm()
        {
            Classes.Driver driver = new Classes.Driver();

            driver.Age = int.Parse(txtDriverAge.Text);
            driver.Aggressiveness = int.Parse(txtDriverAgg.Text);
            driver.Charisma = int.Parse(txtDriverCharisma.Text);
            driver.Concentration = int.Parse(txtDriverConcentration.Text);
            driver.Energy = int.Parse(txtDriverEnergy.Text);
            driver.Experience = int.Parse(txtDriverExperience.Text);
            driver.Motivation = int.Parse(txtDriverMotivation.Text);
            driver.RacesLeft = int.Parse(txtDriverRacesLeft.Text);
            driver.Reputation = int.Parse(txtDriverReputation.Text);

            decimal salary = 0;
            if(decimal.TryParse(txtDriverSalary.Text, out salary))
            {
                driver.Salary = (int)salary; ;
            }
            else
            {
                MessageBox.Show("Could not parse {0}", txtDriverSalary.Text);
            }
            
            driver.Stamina = int.Parse(txtDriverStamina.Text);
            driver.Talent = int.Parse(txtDriverTalent.Text);
            driver.TechnicalInsight = int.Parse(txtDriverTI.Text);
            driver.Weight = int.Parse(txtDriverWeight.Text);
            
            driver.Id = 1;

            return driver;
        }
        Classes.TechnicalDirector ReadTdFromForm()
        {
            Classes.TechnicalDirector td = new Classes.TechnicalDirector();

            td.Age = int.Parse(txtTDAge.Text);
            td.Experience = int.Parse(txtTDExperience.Text);
            td.Leadership = int.Parse(txtTDLeadership.Text);
            td.Motivation = int.Parse(txtTDMotivation.Text);
            td.Pitcoordination = int.Parse(txtTDPitCoordination.Text);
            td.RacesLeft = int.Parse(txtTDRaces.Text);
            td.RDaerodynamics = int.Parse(txtTDRDAerodynamics.Text);
            td.RDelectronics = int.Parse(txtTDRDElectronics.Text);
            td.RDmechanics = int.Parse(txtTDRDMechanics.Text);
            td.Salary = int.Parse(txtTDSalary.Text);

            td.Id = 1;

            return td;

        }
        Classes.StaffFacilities ReadSfFromForm()
        {
            Classes.StaffFacilities sf = new Classes.StaffFacilities();

            
            sf.Experience = int.Parse(txtStaffExperience.Text);
            sf.Motivation = int.Parse(txtStaffMotivation.Text);
            sf.Technicalskill = int.Parse(txtStaffTSkills.Text);
            sf.Stresshandling = int.Parse(txtStaffStressHandling.Text);
            sf.Concentration = int.Parse(txtStaffConcentration.Text);
            sf.Efficiency = int.Parse(txtStaffEfficiency.Text);
            
            sf.Windtunnel = int.Parse(txtFacilitiesWindtunnel.Text);
            sf.Pitstoptrainingcenter = int.Parse(txtFacilitiesPitstop.Text);
            sf.RDworkshop = int.Parse(txtFacilitiesRDWorkshop.Text);
            sf.RDdesigncenter = int.Parse(txtFacilitiesRDDesign.Text);
            sf.Engineeringworkshop = int.Parse(txtFacilitiesEngineering.Text);
            sf.Alloyandchemicallab = int.Parse(txtFacilitiesAlloy.Text);
            sf.Commercial = int.Parse(txtFacilitiesCommercial.Text);
            
            sf.Id = 1;

            return sf;
        }
        Classes.Weather ReadWeatherFromForm()
        {
            Classes.Weather weather = new Classes.Weather();

            weather.Q1[(int)Classes.WeatherEnum2.Temp] = int.Parse(txtWeatherQ1Temp.Text);
            weather.Q1[(int)Classes.WeatherEnum2.Hum] = int.Parse(txtWeatherQ1Hum.Text);
            weather.Q1[(int)Classes.WeatherEnum2.Rain] = int.Parse(txtWeatherQ1Rain.Text);
            weather.Q2[(int)Classes.WeatherEnum2.Temp] = int.Parse(txtWeatherQ2Temp.Text);
            weather.Q2[(int)Classes.WeatherEnum2.Hum] = int.Parse(txtWeatherQ2Hum.Text);
            weather.Q2[(int)Classes.WeatherEnum2.Rain] = int.Parse(txtWeatherQ2Rain.Text);
            weather.R1[(int)Classes.WeatherEnum2.Temp] = int.Parse(txtWeatherR1Temp.Text);
            weather.R1[(int)Classes.WeatherEnum2.Hum] = int.Parse(txtWeatherR1Hum.Text);
            weather.R1[(int)Classes.WeatherEnum2.Rain] = int.Parse(txtWeatherR1Rain.Text);
            weather.R2[(int)Classes.WeatherEnum2.Temp] = int.Parse(txtWeatherR2Temp.Text);
            weather.R2[(int)Classes.WeatherEnum2.Hum] = int.Parse(txtWeatherR2Hum.Text);
            weather.R2[(int)Classes.WeatherEnum2.Rain] = int.Parse(txtWeatherR2Rain.Text);
            weather.R3[(int)Classes.WeatherEnum2.Temp] = int.Parse(txtWeatherR3Temp.Text);
            weather.R3[(int)Classes.WeatherEnum2.Hum] = int.Parse(txtWeatherR3Hum.Text);
            weather.R3[(int)Classes.WeatherEnum2.Rain] = int.Parse(txtWeatherR3Rain.Text);
            weather.R4[(int)Classes.WeatherEnum2.Temp] = int.Parse(txtWeatherR4Temp.Text);
            weather.R4[(int)Classes.WeatherEnum2.Hum] = int.Parse(txtWeatherR4Hum.Text);
            weather.R4[(int)Classes.WeatherEnum2.Rain] = int.Parse(txtWeatherR4Rain.Text);
            
            weather.Id = 1;

            return weather;
        }
        //Classes.TyresSupplier ReadTyreFromUpdateTab()
        //{
        //    Classes.TyresSupplier tyre = new Classes.TyresSupplier();

        //    tyre.Supplier = (Classes.TyreSuppliers)tyre.GetSupplierCodeFromName(cBoxTyresSuppliers.Text);

        //    return tyre;
        //}
        int GetTyreCodeFromSkillTab()
        {
            Classes.TyresSupplier tyre = ReadTyreFromSkillTab();

            //int tyreCode = tyre.GetSupplierCodeFromName(cBoxTyresSuppliers.Text);
            int tyreCode = tyre.GetSupplierCodeFromName(tyre.Supplier.ToString());

            return tyreCode;
        }
        Classes.TyresSupplier ReadTyreFromSkillTab()
        {
            Classes.TyresSupplier tyre = new Classes.TyresSupplier();

            string tyreSupplier = cBoxSkillsTyresSupplier.Text;
            tyre = DB.Tyres.ReadTyreFromDB(tyreSupplier);

            return tyre;
        }

        void CalculateDriverOA(Classes.Driver driver)
        {
            int OA = (int)Math.Round(driver.Concentration / 6 +
                        driver.Talent / 4 +
                        driver.Aggressiveness / 6.86 +
                        driver.Experience / 12 +
                        driver.TechnicalInsight / 8 +
                        driver.Stamina / 6.86 + 
                        driver.Charisma / 12 + 
                        driver.Motivation / 12 -
                        driver.Weight / 12
                        ,0);

            lblDriverOA.Text = OA.ToString();
        }
        
        void UpdateStaffAndFacilitiesCost(Classes.StaffFacilities sf)
        {
            lblStaffSalary.Text = GetStaffCost(sf).ToString();
            lblFacilitiesMaintenance.Text = GetFacilitiesCost(sf).ToString();
        }
        int GetStaffCost(Classes.StaffFacilities sf)
        {
            int costs = 11 * sf.Stresshandling + 6 * sf.Concentration + 18 * sf.Efficiency;
            return costs * 1000;
        }
        int GetFacilitiesCost(Classes.StaffFacilities sf)
        {
            int costs = 5000 * (sf.Windtunnel + sf.Pitstoptrainingcenter + sf.RDdesigncenter + sf.RDworkshop + sf.Engineeringworkshop + sf.Alloyandchemicallab + sf.Commercial);
            return costs;
        }

        //From objects
        private void bttSkillsSave_Click(object sender, EventArgs e)
        {
            SaveSkills();
            UpdateStaffAndFacilitiesCost(ReadSfFromForm());
            MessageBox.Show("Skills saved!");
        }
        private void cBoxTyresSuppliers_TabIndexChanged(object sender, EventArgs e)
        {
            if (finishedLoading)
            {
                int index = cBoxTyresSuppliers.SelectedIndex;
                DB.ActiveTyreSupplier.UpdateTyreSupplier(index);
            }
        }
        private void bttSkillsHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Constants.skillsHelpMessage, "Skills help message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
