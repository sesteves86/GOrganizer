using GOrganizer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GOrganizer
{
    public partial class Form1 : Form
    {
        //Initiate
        void InitiateRaceTab()
        {
            PopulateNextTrackRaceComboBox();

            Classes.RaceTab rt = DB.RaceTab.ReadRaceTabFromDB();
            WriteRaceTabToForm(rt);

            PopulateDgvRaceTrack();
            CreateDtRacingTimes();
        }
        void InitiateRaceTab_LateLoading()
        {
            tabControl1.SelectedIndex = 1;
            txtRace_TextChanged(null, null);
        }
        void PopulateNextTrackRaceComboBox()
        {
            //Populate last cBox with chosen tracks
            List<Classes.Track> seasonTrackList =
                DB.SeasonTrack.ReadTracksListFromDB();
            List<string> trackSeasonListcBox = new List<string>();

            int raceNumber = 0;
            foreach (Classes.Track track in seasonTrackList)
            {
                raceNumber++;
                if (raceNumber > 17)
                    break;

                trackSeasonListcBox.Add(track.Name);
            }
            
            cBoxNextRacingTrack.DataSource = new List<string>(trackSeasonListcBox);

            //Populate cBox in RaceTab for Compounds
            List<string> compoundList = Enum.GetNames(typeof(Classes.Compound)).ToList();
            
            cBoxRaceCompound.DataSource = new List<string>(compoundList);
        }
        void PopulateDgvRaceTrack()
        {
            dtRaceTrack = dtTracks.Clone();

            int seasonTrackIndex = DB.RaceTab.ReadRaceTabFromDB().SeasonTrackIndex;
            int trackId = DB.SeasonTrack.ReadTracksListFromDB()[seasonTrackIndex].Id - 1;
            dtRaceTrack.ImportRow(dtTracks.Rows[trackId]);

            dgvRaceTrack.DataSource = dtRaceTrack;

            foreach (DataGridViewColumn col in dgvRaceTrack.Columns)
            {
                col.Width = 50;
                //col.Width = (int)((Form1.ActiveForm.Width-60f)/17f);
            }
            dgvRaceTrack.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(DataGridView.DefaultFont, System.Drawing.FontStyle.Bold);
        }
        void CreateDtRacingTimes()
        {
            dtRacingTimes = new DataTable();

            dtRacingTimes.Columns.Add("-");
            dtRacingTimes.Columns.Add("0");
            dtRacingTimes.Columns.Add("1");
            dtRacingTimes.Columns.Add("2");
            dtRacingTimes.Columns.Add("3");
            dtRacingTimes.Columns.Add("4");

            FillDgvRacingTimes();
        }
        void WriteRaceTabToForm(Classes.RaceTab rt)
        {
            cBoxNextRacingTrack.SelectedIndex = rt.SeasonTrackIndex;

            txtRaceLaps1.Text = rt.CustomLap1.ToString(); 
            txtRaceLaps2.Text = rt.CustomLap2.ToString();

            cBoxRaceCompound.SelectedIndex = rt.Compound;

            txtRaceCt.Text = rt.CT.ToString();
        }

        //Updates
        void UpdateDgvRaceTrack(string nextRaceName)
        {
            dtRaceTrack = new DataTable();
            dtRaceTrack = dtTracks.Clone();

            string expression = string.Format("Name LIKE '{0}'", nextRaceName);
            var r = dtTracks.Select(expression)[0];
            dtRaceTrack.ImportRow(r);
            dgvRaceTrack.DataSource = dtRaceTrack;
        }
        void UpdateRaceTrackWarnings(string nextRaceName)
        {
            string expression = string.Format("Name LIKE '{0}'", nextRaceName);
            var row = dtTracks.Select(expression)[0];

            //if(row.ItemArray[17].ToString() == "0")
            //{
            //    lblRaceWarningCarWearValuesMissing.Visible = true;
            //}
            //else
            //{
            //    lblRaceWarningCarWearValuesMissing.Visible = false;
            //}

            if (row.ItemArray[16].ToString() == "0")
            {
                lblRaceWarningsTdcValueMissing.Visible = true;
            }
            else
            {
                lblRaceWarningsTdcValueMissing.Visible = false;
            }

            if (row.ItemArray[8].ToString() == "0")
            {
                lblRaceWarningsFuelConstantMissing.Visible = true;
            }
            else
            {
                lblRaceWarningsFuelConstantMissing.Visible = false;
            }
        }
        void FillDgvRacingTimes()
        {
            //For Styling
            int[,] tyreWearArray = new int[5, 5]; //row, col
            
            Classes.RaceTab rt = ReadRaceTabFromForm();
            int trackId = DB.SeasonTrack.GetTrackId(rt.SeasonTrackIndex + 1);
            Classes.Track track = DB.Track.ReadTrackFromDB(trackId);

            int supplierId = DB.ActiveTyreSupplier.GetTyreSupplierId();
            Classes.TyresSupplier tyre = DB.Tyres.ReadTyreFromSupplierDB(supplierId);

            Classes.Car car = finishedLoading ? 
                                ReadCarFromForm() : 
                                DB.Car.ReadCarFromDB();
            Classes.Driver driver = finishedLoading ?
                                        ReadDriverFromForm() :
                                        DB.Driver.ReadDriverFromDB();

            float lapDistance = (float)track.DistanceKm / track.Laps;
            float totalFuel = GetFuelPerLap(track, car, driver, lapDistance) * track.Laps;

            float pitStopDriveTime = track.PitStopTime; 
            float baseTime = track.baseTime;
            float tdc = track.TDCConstant * rt.Temp + tyre.TdcVariable;
            
            string[] o = new string[6];
            for (int line = 0; line < 7; line++)
            {
                switch (line)
                {
                    case 0:
                        o[0] = "XSoft";
                        break;
                    case 1:
                        o[0] = "Soft";
                        break;
                    case 2:
                        o[0] = "Medium";
                        break;
                    case 3:
                        o[0] = "Hard";
                        break;
                    case 4:
                        o[0] = "Rain";
                        break;
                    case 5:
                        o[0] = "Fuel";
                        break;
                    case 6:
                        o[0] = "Laps/stop";
                        break;
                    default:
                        o[0] = "Error";
                        break;
                }

                for (int nStops = 0; nStops < 5; nStops++)
                {
                    float stintDistance = (float)track.DistanceKm / (nStops+1);
                    if (line < 4) //Race Time
                    {
                        string raceTime = "";
                        float tyreDistance = TyreDistance(track, tyre, driver, car, rt, line);
                        float tyreWear = stintDistance / tyreDistance*100;
                        if (tyreDistance > stintDistance)
                        {
                            int fuelPerStint = (int)(totalFuel / (1 + nStops));
                            raceTime = CalculateTotalRaceTime(nStops, fuelPerStint, track, tdc, tyre.WarmUpDistance,
                                line, rt.Rain, rt.CT).ToString("0");
                        }
                        tyreWearArray[line, nStops] = (int)tyreWear;
                        o[nStops + 1] = raceTime;
                    }
                    else if(line ==5 ) //Fuel
                    {
                        int fuelPerStint = (int)(totalFuel / (1 + nStops));
                        string fuel = string.Format("{0:###.#}", fuelPerStint);
                        o[nStops + 1] = fuel;
                    }
                    else if (line == 6) // Laps / Stint
                    {
                        string laps = string.Format("{0:##.#}",track.Laps / (nStops + 1));
                        o[nStops + 1] = laps;
                    }
                }
                dtRacingTimes.Rows.Add(o);
            }
            

            dgvRacingTimes.DataSource = dtRacingTimes;

            foreach (DataGridViewColumn col in dgvRacingTimes.Columns)
            {
                col.Width = 45;
            }

            PaintDgvRacingTimesCells(tyreWearArray);
        }
        void UpdateRaceTabWeather(Classes.Weather weather)
        {
            int raceTemp = (int)((weather.R1[(int)Classes.WeatherEnum2.Temp] + 
                weather.R2[(int)Classes.WeatherEnum2.Temp] + 
                weather.R3[(int)Classes.WeatherEnum2.Temp] + 
                weather.R4[(int)Classes.WeatherEnum2.Temp]) /4);
            int raceHum = (int)((weather.R1[(int)Classes.WeatherEnum2.Hum] + 
                weather.R2[(int)Classes.WeatherEnum2.Hum] + 
                weather.R3[(int)Classes.WeatherEnum2.Hum] + 
                weather.R4[(int)Classes.WeatherEnum2.Hum]) / 4);

            txtRaceTemp.Text = raceTemp.ToString();
            txtRaceHum.Text = raceHum.ToString();
        }

        //Others
        Classes.RaceTab ReadRaceTabFromForm()
        {
            Classes.RaceTab rt = new RaceTab();
            rt.SeasonTrackIndex = cBoxNextRacingTrack.SelectedIndex;

            int tempVar;

            if (!int.TryParse(txtRaceLaps1.Text.ToString(), out tempVar))
                tempVar = 0;
            rt.CustomLap1 = tempVar;

            if (!int.TryParse(txtRaceLaps2.Text.ToString(), out tempVar))
                tempVar = 0;
            rt.CustomLap2 = tempVar;
            
            rt.Compound = cBoxRaceCompound.SelectedIndex;
            
            if (!int.TryParse(txtRaceCt.Text.ToString(), out tempVar))
                tempVar = 0;
            rt.CT = tempVar;

            if (!int.TryParse(txtRaceTemp.Text.ToString(), out tempVar))
                tempVar = 0;
            rt.Temp = tempVar;

            if (!int.TryParse(txtRaceHum.Text.ToString(), out tempVar))
                tempVar = 0;
            rt.Hum = tempVar;

            rt.Rain = rBttRain.Checked;

            return rt;
        }
        float CalculateTotalRaceTime(int nStops, int fuelPerStint, Classes.Track track, float tdc, 
            int tyreWarmUpCode, int compoundCode, bool rain, int ct)
        {
            float pitStopRefuelling = 0.15f * fuelPerStint; //s
            float pitStopTyreChange = 15;
            float pitStopServiceTime = Math.Max(pitStopRefuelling, pitStopTyreChange); //Refuel + Tyre change
            float warmingTyres = tyreWarmUpCode * 1.8f; //s, depends on tyres

            float pitStopTotalTime = track.PitStopTime + pitStopServiceTime + warmingTyres;
            
            float fuelWeightImpact = 0.01f;
            float rainPaceDelay = 1.27f;

            float totalRaceTime = 
                (track.baseTime + fuelPerStint * fuelWeightImpact + (compoundCode - 1) * tdc) * track.Laps;
            totalRaceTime *= (rain ? rainPaceDelay : 1); //slow pace due to rain
            totalRaceTime /= (float)(1 + 0.032 * ct); //CT Risk pace change
            totalRaceTime += warmingTyres + nStops * pitStopTotalTime; //Include start up warming up and Pit Stops
            
            return totalRaceTime;
        }
        float TyreDistance(Classes.Track track, Classes.TyresSupplier tyre, Classes.Driver driver, Classes.Car car,
            Classes.RaceTab rt, int compoundCode)
        {
            rt.Compound = compoundCode;
            return TyreDistance(track, tyre, driver, car, rt);
        }
        float TyreDistance(Classes.Track track, Classes.TyresSupplier tyre, Classes.Driver driver, Classes.Car car,
            Classes.RaceTab rt)
        {
            double tyreDistance = 1 / (
                                0.008f +
                                (int)track.TyresWear * 0.0011357f +
                                rt.Temp * 0.000117f + 
                                tyre.Durability * -0.0003848391f +
                                rt.Compound * -0.0022681289f +
                                driver.Aggressiveness * 1.98713674683317E-06f +
                                driver.Experience * -1.98047770495874E-06f +
                                car.SuspensionLevel * -0.0001644978f +
                                rt.CT * 2.58125503524434E-05f
                            );
            return (float)tyreDistance;
        }
        float GetFuelPerLap(Classes.Track track, Classes.Car car, Classes.Driver driver, float lapDistance)
        {
            double fuelPerLap = lapDistance / (
                track.FuelConstant +
                car.EngineLevel * 0.028 +
                car.ElectronicsLevel * 0.01 +
                driver.Experience * 0.00025 +
                driver.TechnicalInsight * 0.0005 -
                driver.Aggressiveness * 0.00015
            ); //for dry races

            return (float)fuelPerLap;
        }
        void PaintDgvRacingTimesCells(int[,] tyreWearArray)
        {
            for(int col = 1; col<5; col++)
            {
                for (int row = 0; row < 5; row++)
                {
                    int tyreWear = tyreWearArray[row,col-1];
                    dgvRacingTimes.Rows[row].Cells[col].Style = CellStyles.GetRaceCellStyle(tyreWear);
                }
            }
        }

        
        //Form Objects (buttons, textBoxes, ...)
        private void cBoxNextRacingTrack_SelectedValueChanged(object sender, EventArgs e)
        {
            string raceName = ((ComboBox)sender).Text;
            UpdateDgvRaceTrack(raceName);

            //int index = cBoxNextRacingTrack.SelectedIndex;
            UpdateRaceTrackWarnings(raceName);
        }
        private void txtRaceLaps_TextChanged(object sender, EventArgs e)
        {
            if (finishedLoading)
            {
                int customLaps;

                if (!int.TryParse(((TextBox)sender).Text, out customLaps))
                    customLaps = 0;

                int fuelConsumption = 0;
                int tyreWear = 0;

                //Get Track details
                int distanceKm = int.Parse(dgvRaceTrack.Rows[0].Cells["DistanceKm"].Value.ToString());
                int trackLaps = int.Parse(dgvRaceTrack.Rows[0].Cells["Laps"].Value.ToString());
                float fuelConstant = float.Parse(dgvRaceTrack.Rows[0].Cells["Fuel Constant"].Value.ToString());

                float lapDistance = distanceKm / trackLaps;

                Classes.Driver driver = ReadDriverFromForm();
                Classes.Car car = ReadCarFromForm();
                Classes.RaceTab rt = ReadRaceTabFromForm();

                //If dry lap, perhaps adding a bool argument for rain in future
                fuelConsumption = (int)Classes.Fuel.ConsumptionDryLap(lapDistance, fuelConstant,
                    (byte)(car.EngineLevel), (byte)car.ElectronicsLevel,
                    (byte)driver.Experience, (byte)driver.TechnicalInsight, (byte)driver.Aggressiveness);

                int stintDistance = (int)(customLaps * lapDistance);
                float tyreDistance = TyreDistance(
                    DB.Track.ReadTrackFromDB(rt.SeasonTrackIndex),
                    ReadTyreFromSkillTab(), 
                    driver, 
                    car, 
                    rt);

                tyreWear = (int)(stintDistance / tyreDistance * 100);

                if (((TextBox)sender).Name == "txtRaceLaps1")
                {
                    lblRaceFuel1.Text = (fuelConsumption * customLaps).ToString();
                    lblRaceTyre1.Text = tyreWear.ToString()+"%";
                }
                else
                {
                    lblRaceFuel2.Text = (fuelConsumption * customLaps).ToString();
                    lblRaceTyre2.Text = tyreWear.ToString()+"%";
                }
                
            }
        }
        //private void txtRaceLaps2_TextChanged(object sender, EventArgs e)
        //{
        //    int fuelConsumption = 0;
        //    int tyreWear = 0;

        //    //Get Track details
        //    int distanceKm = int.Parse(dgvRaceTrack.Rows[0].Cells["DistanceKm"].Value.ToString());
        //    int trackLaps = int.Parse(dgvRaceTrack.Rows[0].Cells["Laps"].Value.ToString());
        //    int fuelConstant = int.Parse(dgvRaceTrack.Rows[0].Cells["Fuel Constant"].Value.ToString());

        //    float lapDistance = distanceKm / trackLaps;
        //    int customLaps = int.Parse(((TextBox)sender).Text);

        //    Classes.Driver driver = ReadDriverFromForm();
        //    Classes.Car car = ReadCarFromForm();

        //    //If dry lap
        //    fuelConsumption = (int)Classes.Fuel.ConsumptionDryLap(lapDistance, fuelConstant,
        //        (byte)(car.EngineLevel), (byte)car.ElectronicsLevel,
        //        (byte)driver.Experience, (byte)driver.TechnicalInsight, (byte)driver.Aggressiveness);

        //    lblRaceFuel2.Text = (fuelConsumption * customLaps).ToString();
        //    //lblRaceTyre1.Text = ...;

        //    //If wet lap
        //    //int fuelConsumption = (int)Classes.Fuel.ConsumpionWetLap( lapDistance, track.FuelConstant,
        //    //(byte)(car.EngineLevel), (byte)car.ElectronicsLevel,
        //    //    (byte)driver.Experience, (byte)driver.TechnicalInsight, (byte)driver.Aggressiveness);
        //}
        private void cBoxNextRacingTrack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (finishedLoading)
            {
                CreateDtRacingTimes();
                txtRace_TextChanged(this, null);
                txtRaceLaps_TextChanged(txtRaceLaps1, null);
                txtRaceLaps_TextChanged(txtRaceLaps2, null);
            }
        }
        private void txtRace_TextChanged(object sender, EventArgs e)
        {
            if (finishedLoading)
            {
                CreateDtRacingTimes();
                if (tabControl1.SelectedTab != tabControl1.TabPages[1]) //not in race tab
                {
                    Classes.Weather weather = ReadWeatherFromForm();
                    UpdateRaceTabWeather(weather);
                }

                //Update TDC
                float tdcConstant = float.Parse(dgvRaceTrack.Rows[0].Cells[16].Value.ToString());
                float raceTemp = 0;
                    float.TryParse(txtRaceTemp.Text, out raceTemp);
                float suppliersTdcConstant = ReadTyreFromSkillTab().TdcVariable;
                float tdc = (50f - raceTemp) * tdcConstant + suppliersTdcConstant;
                lblRaceTDC.Text = string.Format("{0:0.000} s", tdc); //tdc.ToString() + " s";

                txtRaceLaps_TextChanged(txtRaceLaps1, null);
                txtRaceLaps_TextChanged(txtRaceLaps2, null);
            }
        }
        private void cBoxRaceCompound_SelectedValueChanged(object sender, EventArgs e)
        {
            txtRaceLaps_TextChanged(txtRaceLaps1, null);
            txtRaceLaps_TextChanged(txtRaceLaps2, null);
        }
        private void bttRaceHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Constants.raceHelpMessage, "Race help message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    
    static class CellStyles
    {
        public static DataGridViewCellStyle GetRaceCellStyle(float tyreWear)//, float raceTime)
        {
            DataGridViewCellStyle style = new DataGridViewCellStyle();
            if (tyreWear > 100)
                style.BackColor = System.Drawing.Color.FromArgb(100, 0, 0);
            else if (tyreWear > 80)
                style.BackColor = System.Drawing.Color.FromArgb(230+(100-(int)tyreWear),(int)(1050-(10*tyreWear)), (int)(1050 - (10 * tyreWear)));
            else 
                style.BackColor = System.Drawing.Color.FromArgb(250, 250, 250);

            //style.BackColor = System.Drawing.Color.FromArgb(1, 1, 1); //black
            //style.ForeColor = System.Drawing.Color.Black;

            return style;
        }
    }
}
