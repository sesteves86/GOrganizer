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
        void UpdateUpdate()
        {
            string car = txtCarUpdate.Text.Trim();
            string driver = txtDriver.Text.Trim();
            string td = txtTdUpdate.Text.Trim();
            string weather = txtWeather.Text.Trim();
            //string sponsors = txtSponsors.Text.Trim();
            string staffFacilities = txtStaffFacilities.Text.Trim();

            if (car != "") UpdateCar(car);
            if (driver != "") UpdateDriver(driver);
            if (td != "") UpdateTd(td);
            if (weather != "") UpdateWeather(weather);
            //if (sponsors != "") UpdateSponsors(sponsors);
            if (staffFacilities != "") UpdateStaffFacilities(staffFacilities);

            //Signal that the Update has been concluded
            CleanUpdateBoxes();
            MessageBox.Show("Update Complete");
            //Update on the Update2 tab button
        }

        void UpdateCar(string text)
        {
            string lastCarPart = "";
            int level = 1;
            int wear = 0;

            string[] lines = text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                int index = lines[i].IndexOf(':');
                if (index != -1) //Odd line will have the name and level of the part
                {
                    lastCarPart = lines[i].Substring(0, index);
                    level = int.Parse(lines[i].Substring(index + 1).Trim());
                }
                else //following pair line will have the wear of the part
                {
                    int index2 = lines[i].IndexOf('%');
                    if (index2 != -1)
                    {
                        wear = int.Parse(lines[i].Substring(0, index2).Trim());

                        //Update car on Update2 tab
                        switch (lastCarPart)
                        {
                            case "Chassis":
                                txtChassisLevel.Text = level.ToString();
                                txtChassisWear.Text = wear.ToString();
                                break;
                            case "Engine":
                                txtEngineLevel.Text = level.ToString();
                                txtEngineWear.Text = wear.ToString();
                                break;
                            case "Front wing":
                                txtFWingLevel.Text = level.ToString();
                                txtFWingWear.Text = wear.ToString();
                                break;
                            case "Rear wing":
                                txtRWingLevel.Text = level.ToString();
                                txtRWingWear.Text = wear.ToString();
                                break;
                            case "Underbody":
                                txtUnderbodyLevel.Text = level.ToString();
                                txtUnderbodyWear.Text = wear.ToString();
                                break;
                            case "Sidepods":
                                txtSidepodsLevel.Text = level.ToString();
                                txtSidepodsWear.Text = wear.ToString();
                                break;
                            case "Cooling":
                                txtCoolingLevel.Text = level.ToString();
                                txtCoolingWear.Text = wear.ToString();
                                break;
                            case "Gearbox":
                                txtGearboxLevel.Text = level.ToString();
                                txtGearboxWear.Text = wear.ToString();
                                break;
                            case "Brakes":
                                txtBrakesLevel.Text = level.ToString();
                                txtBrakesWear.Text = wear.ToString();
                                break;
                            case "Suspension":
                                txtSuspensionLevel.Text = level.ToString();
                                txtSuspensionWear.Text = wear.ToString();
                                break;
                            case "Electronics":
                                txtElectronicsLevel.Text = level.ToString();
                                txtElectronicsWear.Text = wear.ToString();
                                break;
                        }
                    }
                }
                
            }
        }
        void UpdateDriver(string text)
        {
            string[] lines = text.Split('\n');
            int energy = 0;
            string skillName = "";
            int skillLevel = 0;

            foreach (string line in lines)
            {
                int index = line.IndexOf(':');
                if (index != -1) //Overall to Age
                {
                    skillName = line.Substring(0,index);
                    skillLevel = int.Parse(line.Substring(index + 2));

                    FillDriverBoxes(skillName, skillLevel);
                }
                else
                {
                    index = line.IndexOf('%'); //Energy
                    if (index != -1)
                    {
                        energy = int.Parse(line.Substring(0, index));
                        FillDriverBoxes("Energy", energy);
                    }
                }
            }

            txtDriver.Clear();
        }
        void UpdateTd(string text)
        {
            string[] lines = text.Split('\n');
            string skillName = "";
            int skillLevel = 0;

            foreach (string line in lines)
            {
                int index = line.IndexOf(':');
                if (index != -1) //if : is found
                {
                    skillName = line.Substring(0, index);
                    skillLevel = int.Parse(line.Substring(index + 2));

                    skillName = skillName.Replace(" ","");
                    skillName = skillName.Replace("&", "");
                    FillTDBoxes(skillName, skillLevel);
                }
            }

            txtDriver.Clear();
        }
        void UpdateWeather(string text)
        {
            char[] separatingCharacters = { '\n', '\t' };
            string[] lines = text.Split(separatingCharacters); 
            string hum, rain, temp;
            int startingLine = 0;
            int activeLine = 0;
            bool active = false;

            for (int i = 0; i < lines.Count(); i++)
            {
                if (!active)
                {
                    if (lines[i].Contains("Practice / Qualify 1"))
                    {
                        active = true;
                        startingLine = i+1; 
                        continue;
                    }
                }
                else //if active
                {
                    activeLine = i - startingLine;
                    switch (activeLine)
                    {
                        case 1: // Q1 Rain
                            if (lines[i].Contains("Rain")) 
                                txtWeatherQ1Rain.Text = "100";
                            else
                                txtWeatherQ1Rain.Text = "0";
                            break;
                        case 2: // Q1 Temp
                            temp = lines[i].Replace("°C", "");
                            temp = temp.Replace("Temp: ", "");
                            txtWeatherQ1Temp.Text = temp;
                            break;
                        case 3: //Q1 Hum
                            hum = lines[i];
                            hum = FormatHumidity(hum);
                            txtWeatherQ1Hum.Text = hum;
                            
                            break;
                        case 4://Q2 Rain
                            rain = lines[i];
                            if (rain.Contains("Rain"))
                                txtWeatherQ2Rain.Text = "100";
                            else
                                txtWeatherQ2Rain.Text = "0";

                            break;
                        case 5: //Q2 temp
                            temp = lines[i].Replace("°C", "");
                            temp = temp.Replace("Temp: ", "");
                            txtWeatherQ2Temp.Text = temp;
                            break;
                        case 6: //Q2 Hum
                            hum = lines[i];
                            hum = FormatHumidity(hum);
                            txtWeatherQ2Hum.Text = hum;
                            break;
                        case 10: //R1 Temp
                            temp = lines[i];
                            temp = FormatTemperature(temp);
                            temp = GetAverage(temp);
                            txtWeatherR1Temp.Text = temp;
                            break;
                        case 11: //R1 Hum
                            hum = lines[i];
                            hum = FormatHumidity(hum);
                            hum = GetAverage(hum);
                            txtWeatherR1Hum.Text = hum;
                            break;
                        case 12: //R1 Rain 
                            rain = lines[i];
                            rain = FormatRain(rain);
                            rain = GetAverage(rain);
                            txtWeatherR1Rain.Text = rain;
                            break;
                        case 13: //R2 Temp
                            temp = lines[i];
                            temp = FormatTemperature(temp);
                            temp = GetAverage(temp);
                            txtWeatherR2Temp.Text = temp;
                            break;

                        case 14: //R2 Hum
                            hum = lines[i];
                            hum = FormatHumidity(hum);
                            hum = GetAverage(hum);
                            txtWeatherR2Hum.Text = hum;
                            
                            break;
                        case 15: //R2 Rain
                            rain = lines[i];
                            rain = FormatRain(rain);
                            rain = GetAverage(rain);
                            txtWeatherR2Rain.Text = rain;

                            break;
                        case 18: //R3 Temp
                            temp = lines[i];
                            temp = FormatTemperature(temp);
                            temp = GetAverage(temp);
                            txtWeatherR3Temp.Text = temp;

                            break;
                        case 19: //R3 Hum
                            hum = lines[i];
                            hum = FormatHumidity(hum);
                            hum = GetAverage(hum);
                            txtWeatherR3Hum.Text = hum;

                            break;
                        case 20: //R3 Rain
                            rain = lines[i];
                            rain = FormatRain(rain);
                            rain = GetAverage(rain);
                            txtWeatherR3Rain.Text = rain;

                            break;
                        case 21: //R4 Temp
                            temp = lines[i];
                            temp = FormatTemperature(temp);
                            temp = GetAverage(temp);
                            txtWeatherR4Temp.Text = temp;

                            break;
                        case 22: //R4 Hum
                            hum = lines[i];
                            hum = FormatHumidity(hum);
                            hum = GetAverage(hum);
                            txtWeatherR4Hum.Text = hum;

                            break;
                        case 23: //R4 Rain
                            rain = lines[i];
                            rain = FormatRain(rain);
                            rain = GetAverage(rain);
                            txtWeatherR4Rain.Text = rain;

                            break;
                        default:
                            break;
                    }
                }
            }

            /*
            foreach (string line in lines)
            {
                
            }*/

            txtDriver.Clear();
        }
        void UpdateSponsors(string text)
        {

        }//ToDo
        void UpdateStaffFacilities(string text)
        {
            string[] lines = text.Split('\n');
            string skillName = "";
            int skillLevel = 0;

            foreach (string line in lines)
            {
                int index = line.IndexOf(':');
                if (index != -1) //if : is found
                {
                    skillName = line.Substring(0, index);
                    skillLevel = int.Parse(line.Substring(index + 2));

                    skillName = skillName.Replace(" ", "");
                    skillName = skillName.Replace("&", "");
                    FillStaffFacilitiesBoxes(skillName, skillLevel);
                }
            }

            txtDriver.Clear();
        }

        void FillDriverBoxes(string skillName, int skillValue)
        {
            try
            {
                Classes.DriverSkills skill = (Classes.DriverSkills)Enum.Parse(
                    typeof(Classes.DriverSkills), skillName
                );

                switch (skill)
                {
                    case Classes.DriverSkills.Energy:
                        txtDriverEnergy.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.Concentration:
                        txtDriverConcentration.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.Talent:
                        txtDriverTalent.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.Aggressiveness:
                        txtDriverAgg.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.Experience:
                        txtDriverExperience.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.TI:
                        txtDriverTI.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.Stamina:
                        txtDriverStamina.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.Charisma:
                        txtDriverCharisma.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.Motivation:
                        txtDriverMotivation.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.Reputation:
                        txtDriverReputation.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.Weight:
                        txtDriverWeight.Text = skillValue.ToString();
                        break;
                    case Classes.DriverSkills.Age:
                        txtDriverAge.Text = skillValue.ToString();
                        break;
                }
            }
            catch { }
            
        }
        void FillTDBoxes(string skillName, int skillValue)
        {
            try
            {
                Classes.TDSkills skill = 
                    (Classes.TDSkills)Enum.Parse(
                        typeof(Classes.TDSkills), skillName
                    );

                switch (skill)
                {
                    case Classes.TDSkills.Leadership:
                        txtTDLeadership.Text = skillValue.ToString();
                        break;
                    case Classes.TDSkills.RDmechanics:
                        txtTDRDMechanics.Text = skillValue.ToString();
                        break;
                    case Classes.TDSkills.RDelectronics:
                        txtTDRDElectronics.Text = skillValue.ToString();
                        break;
                    case Classes.TDSkills.RDaerodynamics:
                        txtTDRDAerodynamics.Text = skillValue.ToString();
                        break;
                    case Classes.TDSkills.Experience:
                        txtTDExperience.Text = skillValue.ToString();
                        break;
                    case Classes.TDSkills.Pitcoordination:
                        txtTDPitCoordination.Text = skillValue.ToString();
                        break;
                    case Classes.TDSkills.Motivation:
                        txtTDMotivation.Text = skillValue.ToString();
                        break;
                    /*case Classes.TDSkills.Age:
                        txtTDAge.Text = skillValue.ToString();
                        break;*/
                    case Classes.TDSkills.Salary:
                        txtTDSalary.Text = skillValue.ToString();
                        break;
                    case Classes.TDSkills.RacesLeft:
                        txtTDRaces.Text = skillValue.ToString();
                        break;
                    default:
                        break;
                }
            }
            catch { }
        }
        void FillStaffFacilitiesBoxes(string skillName, int skillValue)
        {
            try
            {
                Classes.StaffFacilitiesNames skill =
                    (Classes.StaffFacilitiesNames)Enum.Parse(
                        typeof(Classes.StaffFacilitiesNames), skillName
                    );

                switch (skill)
                {
                    case Classes.StaffFacilitiesNames.Experience:
                        txtStaffExperience.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.Motivation:
                        txtStaffMotivation.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.Technicalskill:
                        txtStaffTSkills.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.Stresshandling:
                        txtStaffStressHandling.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.Concentration:
                        txtStaffConcentration.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.Efficiency:
                        txtStaffEfficiency.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.Windtunnel:
                        txtFacilitiesWindtunnel.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.Pitstoptrainingcenter:
                        txtFacilitiesPitstop.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.RDworkshop:
                        txtFacilitiesRDWorkshop.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.RDdesigncenter:
                        txtFacilitiesRDDesign.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.Engineeringworkshop:
                        txtFacilitiesEngineering.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.Alloyandchemicallab:
                        txtFacilitiesAlloy.Text = skillValue.ToString();
                        break;
                    case Classes.StaffFacilitiesNames.Commercial:
                        txtFacilitiesCommercial.Text = skillValue.ToString();
                        break;
                    default:
                        break;
                }
            }
            catch { }
        }

        string GetAverage(string s)
        {
            int index = s.IndexOf('-');
            if (index != -1)
            {
                int s1 = int.Parse(s.Substring(0, index).Trim());
                int s2 = int.Parse(s.Substring(index + 1).Trim());
                s = ((s1 + s2) / 2).ToString();
            }
            else
            {
                s.Trim();
            }

            return s;
        }
        string FormatTemperature(string temp)
        {
            temp = temp.Replace("Temp: ", "");
            temp = temp.Replace("°", "");
            return temp;
        }
        string FormatRain(string rain)
        {
            rain = rain.Replace("Rain probability: ", "");
            rain = rain.Replace("%", "");

            return rain;
        }
        string FormatHumidity(string hum)
        {
            hum = hum.Replace("Humidity: ", "");
            hum = hum.Replace("%", ""); //76-78

            return hum;
        }

        void CleanUpdateBoxes()
        {
            txtCarUpdate.Clear();
            txtDriver.Clear();
            txtTdUpdate.Clear();
            txtWeather.Clear();
            //txtSponsors.Clear();
            txtStaffFacilities.Clear();
        }

        //Buttons
        private void bttUpdate_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 3;
            UpdateUpdate();
        }
        private void bttUpdateHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Constants.updateHelpMessage, "Update help message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
