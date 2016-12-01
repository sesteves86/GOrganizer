using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOrganizer
{
    public partial class Form1:Form
    {
        //Initiate
        void InitiateQualifyingTab()
        {
            InitiatePracticeTable();
        }
        void InitiatePracticeTable()
        {
            //Bind dgvPractice to dtPractice
            dtPractice = new DataTable("practiceTable");
            dtPractice.Columns.Add("FWing");
            dtPractice.Columns.Add("RWing");
            dtPractice.Columns.Add("Engine");
            dtPractice.Columns.Add("Breaks");
            dtPractice.Columns.Add("Gearbox");
            dtPractice.Columns.Add("Suspension");
            dtPractice.Columns.Add("WingsFeedback");
            dtPractice.Columns.Add("EngineFeedback");
            dtPractice.Columns.Add("BreaksFeedback");
            dtPractice.Columns.Add("GearboxFeedback");
            dtPractice.Columns.Add("SuspensionFeedback");

            dgvPractice.DataSource = dtPractice;

            for (int i = 0; i < dgvPractice.Columns.Count; i++)
            {
                dgvPractice.Columns[i].Width = 61;
            }
            dgvPractice.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(DataGridView.DefaultFont, System.Drawing.FontStyle.Bold);

            //Populate with data from DB
            short[,] setups = DB.Practise.ReadPractisesSetups();
            int size = setups.GetLength(0);

            for (int line = 0; line < size; line++)
            {
                object[] o = new object[11];
                for (int i = 0; i < 11; i++)
                {
                    o[i] = setups[line, i];
                }
                dtPractice.Rows.Add(o);
            }

        }

        //Button
        private void bttProcessPracticeLap_Click(object sender, EventArgs e)
        {
            if (dtPractice.Rows.Count < 8)
            {
                sbyte[] feedbacks = ReadDriverFeedback();
                short[] setup = ReadUsedSetup();
                AddPracticeLapToTable(setup, feedbacks);
                GetNextPracticeSetup();

                //Update Q1, Q2 and race setup
                short[] q1Setup = GetQ1Setup();
                UpdateQ1Setup(q1Setup);
                UpdateQ2Setup(q1Setup);
                UpdateRaceSetup(q1Setup);
            }
            else
            {
                MessageBox.Show("The limit of 8 practice laps has been reached. \nTo add more laps, please reset the practise.");
            }
        }
        private void bttPracticeReset_Click(object sender, EventArgs e)
        {
            dtPractice.Clear();

            txtFWing1.Text = "500";
            txtRWing1.Text = "500";
            txtEngine1.Text = "500";
            txtBreaks1.Text = "500";
            txtGear1.Text = "500";
            txtSuspension1.Text = "500";
        }
        private void txtPractiseWS_TextChanged(object sender, EventArgs e)
        {
            short[] q1Setup = GetQ1Setup();
            UpdateQ1Setup(q1Setup);
            UpdateQ2Setup(q1Setup);
            UpdateRaceSetup(q1Setup);
        }
        private void bttQualifyingHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Constants.qualifyingHelpMessage, "Qualifying help message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Methods
        private short[] GetQ1Setup()
        {
            short[] upperSetup = ReadUsedSetup();
            short[] bestSetup = GetQ1Setup(upperSetup);

            return bestSetup;
        }
        private void UpdateQ1Setup(short[] q1Setup)
        {
            short wingSplit = ReadWS();

            lblQ1FWing.Text = (q1Setup[0] + wingSplit).ToString();
            lblQ1RWing.Text = (q1Setup[1] - wingSplit).ToString();
            lblQ1Engine.Text = q1Setup[2].ToString();
            lblQ1Breaks.Text = q1Setup[3].ToString();
            lblQ1Gear.Text = q1Setup[4].ToString();
            lblQ1Suspension.Text = q1Setup[5].ToString();
        }
        private void UpdateQ2Setup(short[] q1Setup)
        {
            Classes.Weather weather = ReadWeatherFromForm();

            short wingSplit = ReadWS();
            short[] q2Setup = GetQ2Setup(q1Setup, weather);

            lblQ2FWing.Text = (q2Setup[0] + wingSplit).ToString();
            lblQ2RWing.Text = (q2Setup[1] - wingSplit).ToString();
            lblQ2Engine.Text = q2Setup[2].ToString();
            lblQ2Breaks.Text = q2Setup[3].ToString();
            lblQ2Gear.Text = q2Setup[4].ToString();
            lblQ2Suspension.Text = q2Setup[5].ToString();
        }
        private void UpdateRaceSetup(short[] q1Setup)
        {
            Classes.Weather weather = ReadWeatherFromForm();
            short wingSplit = ReadWS();

            short[] raceSetup = GetRaceSetup(q1Setup, weather);

            lblRaceFWing.Text = (raceSetup[0] + wingSplit).ToString();
            lblRaceRWing.Text = (raceSetup[1] - wingSplit).ToString();
            lblRaceEngine.Text = raceSetup[2].ToString();
            lblRaceBreaks.Text = raceSetup[3].ToString();
            lblRaceGear.Text = raceSetup[4].ToString();
            lblRaceSuspension.Text = raceSetup[5].ToString();
        }

        sbyte[] ReadDriverFeedback()
        {
            SByte[] feedback = new SByte[5] { 0, 0, 0, 0, 0 };

            string sFeedback = txtDriverFeedback.Text;
            //string tempFeedback = sFeedback;

            char carriageChar = (char)13;
            int nLines = sFeedback.Count(v => v.Equals(carriageChar));

            string[] feedbackLines = sFeedback.Split(carriageChar); //new string[nLines];
            
            foreach (string line in feedbackLines)
            {
                try
                {
                    string str = line.Trim();

                    char spaceChar = (char)32;
                    int spacePos = str.IndexOf(spaceChar);

                    string carPart = str.Substring(0, spacePos-1).Trim();
                    string feedbackText = str.Substring(spacePos).Trim();

                    switch (carPart)
                    {
                        case "Wings":
                            feedback[0] = GetWingFeedbackCode(feedbackText);
                            break;
                        case "Engine":
                            feedback[1] = GetEngineFeedbackCode(feedbackText);
                            break;
                        case "Brakes":
                            feedback[2] = GetBrakesFeedbackCode(feedbackText);
                            break;
                        case "Gear":
                            feedback[3] = GetGearFeedbackCode(feedbackText);
                            break;
                        case "Suspension":
                            feedback[4] = GetSuspensionFeedbackCode(feedbackText);
                            break;
                        default:
                            MessageBox.Show("Unexpected error reading driver feedback");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    //MessageBox.Show("Error: " + ex.Message);
                }
            }

            return feedback;
        }
        short[] ReadUsedSetup()
        {
            short[] setups = new short[6] { 500, 500, 500, 500, 500, 500 };

            setups[0] = short.Parse(txtFWing1.Text);
            setups[1] = short.Parse(txtRWing1.Text);
            setups[2] = short.Parse(txtEngine1.Text);
            setups[3] = short.Parse(txtBreaks1.Text);
            setups[4] = short.Parse(txtGear1.Text);
            setups[5] = short.Parse(txtSuspension1.Text);

            return setups;
        }
        short ReadWS()
        {
            short ws = 0;

            short.TryParse(txtPractiseWS.Text, out ws);

            return ws;
        }
        void AddPracticeLapToTable(short[] setups, sbyte[] feedbackCodes)
        {
            object[] o = {
                setups[0], setups[1], setups[2], setups[3], setups[4], setups[5],
                feedbackCodes[0], feedbackCodes[1], feedbackCodes[2], feedbackCodes[3], feedbackCodes[4]
            };

            dtPractice.Rows.Add(o);
        }
        void GetNextPracticeSetup()
        {
            short[,] setups = new short[8, 11];

            int driverExp = int.Parse(txtDriverExperience.Text);
            int driverTI = int.Parse(txtDriverTI.Text);
            Classes.TechnicalDirector td = ReadTdFromForm();

            float HR = 135f - 0.1f * driverExp - 0.3f * driverTI;

            float SRWings = HR - 0.39f * td.RDaerodynamics - 0.04f * td.Experience;
            float SREngine = HR - 0.14f * td.RDmechanics - 0.2f * td.RDelectronics - 0.04f * td.Experience;
            float SRBrakes = HR - 0.24f * td.RDmechanics - 0.009f *td.RDaerodynamics - 0.06f * td.Experience - 0.08f * td.RDelectronics;
            float SRGear = HR - 0.29f * td.RDelectronics - 0.06f * td.Experience - 0.06f * td.RDmechanics;
            float SRSuspension = HR - 0.259f * td.RDmechanics - 0.07f * td.Experience - 0.09f * td.RDaerodynamics;

            //Get setup list
            int practiceLap = 0;
            foreach (DataRow row in dtPractice.Rows)
            {
                object[] setupArray = row.ItemArray;
                for (int i = 0; i < 11; i++)
                {
                    setups[practiceLap, i] = short.Parse(setupArray[i].ToString());
                }
                practiceLap++;
            }

            //Process
            int lastLapNumber = dtPractice.Rows.Count - 1; //Subtract 1 because arrays in c# starts counting on 0

            int wingSetup = (int)((setups[lastLapNumber, 0] + setups[lastLapNumber, 1]) / 2 - setups[lastLapNumber, 6] * HR);
            wingSetup += (int)(HR * Math.Pow(2, -lastLapNumber) + (SRWings - HR));

            int engSetup = (int)(setups[lastLapNumber, 2] - setups[lastLapNumber, 7] * HR);
            engSetup += (int)(HR * Math.Pow(2, -lastLapNumber) + (SREngine - HR));

            int brakesSetup = (int)(setups[lastLapNumber, 3] - setups[lastLapNumber, 8] * HR);
            brakesSetup += (int)(HR * Math.Pow(2, -lastLapNumber) + (SRBrakes - HR));

            int gearSetup = (int)(setups[lastLapNumber, 4] - setups[lastLapNumber, 9] * HR);
            gearSetup += (int)(HR * Math.Pow(2, -lastLapNumber) + (SRGear - HR));

            int suspSetup = (int)(setups[lastLapNumber, 5] - setups[lastLapNumber, 10] * HR);
            suspSetup += (int)(HR * Math.Pow(2, -lastLapNumber) + (SRSuspension - HR));

            //Write next setup to form
            txtFWing1.Text = wingSetup.ToString();
            txtRWing1.Text = wingSetup.ToString();
            txtEngine1.Text = engSetup.ToString();
            txtBreaks1.Text = brakesSetup.ToString();
            txtGear1.Text = gearSetup.ToString();
            txtSuspension1.Text = suspSetup.ToString();

        }
        short[] GetQ1Setup(short[] upperSetup)
        {
            short[] bestSetup = new short[6];

            int driverExp = int.Parse(txtDriverExperience.Text);
            int driverTI = int.Parse(txtDriverTI.Text);
            Classes.TechnicalDirector td = ReadTdFromForm();

            float HR = 135f - 0.1f * driverExp - 0.3f * driverTI;

            short SRWings = (short)(HR - 0.39f * td.RDaerodynamics - 0.04f * td.Experience);
            short SREngine = (short)(HR - 0.14f * td.RDmechanics - 0.2f * td.RDelectronics - 0.04f * td.Experience);
            short SRBrakes = (short)(HR - 0.24f * td.RDmechanics - 0.009f * td.RDaerodynamics - 0.06f * td.Experience - 0.08f * td.RDelectronics);
            short SRGear = (short)(HR - 0.29f * td.RDelectronics - 0.06f * td.Experience - 0.06f * td.RDmechanics);
            short SRSuspension = (short)(HR - 0.259f * td.RDmechanics - 0.07f * td.Experience - 0.09f * td.RDaerodynamics);

            bestSetup[0] = (short)(upperSetup[0] - SRWings);
            bestSetup[1] = (short)(upperSetup[1] - SRWings);
            bestSetup[2] = (short)(upperSetup[2] - SREngine);
            bestSetup[3] = (short)(upperSetup[3] - SRBrakes);
            bestSetup[4] = (short)(upperSetup[4] - SRGear);
            bestSetup[5] = (short)(upperSetup[5] - SRSuspension);

            return bestSetup;
        }
        short[] GetQ2Setup(short[] q1Setup, Classes.Weather weather)
        {
            short[] q2Setup = new short[6];

            int temp1 = weather.Q1[(int)Classes.WeatherEnum2.Temp];
            int temp2 = weather.Q2[(int)Classes.WeatherEnum2.Temp];
            int rain = weather.Q2[(int)Classes.WeatherEnum2.Rain] - 
                        weather.Q1[(int)Classes.WeatherEnum2.Rain];

            q2Setup[0] = (short)(q1Setup[0] + (272 - 4.05 * temp1) * rain + 3.6 * (temp2 - temp1) * (1 - rain));
            q2Setup[1] = (short)(q1Setup[1] + (254 - 5.95 * temp1) * rain + 4.36 * (temp2 - temp1) * (1 - rain));
            q2Setup[2] = (short)(q1Setup[2] + (-190 + 3.7 * temp1) * rain - 4.26 * (temp2 - temp1) * (1 - rain));
            q2Setup[3] = (short)(q1Setup[3] + (105 - 2 * temp1) * rain + 6 * (temp2 - temp1) * (1 - rain));
            q2Setup[4] = (short)(q1Setup[4] + (-4 - 4 * temp1) * rain - 4 * (temp2 - temp1) * (1 - rain));
            q2Setup[5] = (short)(q1Setup[5] + (-257 + 5 * temp1) * rain - 6 * (temp2 - temp1) * (1 - rain));

            return q2Setup;
        }
        short[] GetRaceSetup(short[] q1Setup, Classes.Weather weather)
        {
            short[] raceSetup = new short[6];
            //Not yet implemented
            float tuneRaceSetup = 0.5f; //Best setup on: 0-beginning of the race; 1-end of the race

            int temp1 = weather.Q1[(int)Classes.WeatherEnum2.Temp];
            int tempRace = weather.GetRaceAvgTemp();
            float rain = (weather.Q1[(int)Classes.WeatherEnum2.Rain] - weather.GetRaceAvgRain()) / 100;

            raceSetup[0] = (short)(q1Setup[0] + (272 - 4.05 * temp1) * rain + 3.6 * (tempRace - temp1) * (1 - rain));
            raceSetup[1] = (short)(q1Setup[1] + (254 - 5.95 * temp1) * rain + 4.36 * (tempRace - temp1) * (1 - rain));
            raceSetup[2] = (short)(q1Setup[2] + (-190 + 3.7 * temp1) * rain - 4.26 * (tempRace - temp1) * (1 - rain));
            raceSetup[3] = (short)(q1Setup[3] + (105 - 2 * temp1) * rain + 6 * (tempRace - temp1) * (1 - rain));
            raceSetup[4] = (short)(q1Setup[4] + (-4 - 4 * temp1) * rain - 4 * (tempRace - temp1) * (1 - rain));
            raceSetup[5] = (short)(q1Setup[5] + (-257 + 5 * temp1) * rain - 6 * (tempRace - temp1) * (1 - rain));

            return raceSetup;
        }

        void WriteSetupToBoxes(short[] setups)
        {
            txtFWing1.Text = setups[0].ToString();
            txtRWing1.Text = setups[1].ToString();
            txtEngine1.Text = setups[2].ToString();
            txtBreaks1.Text = setups[3].ToString();
            txtGear1.Text = setups[4].ToString();
            txtSuspension1.Text = setups[5].ToString();
        }

        short[,] GetSetupsFromDataTablePractise()
        {
            int nLines = dtPractice.Rows.Count;
            short[,] setups = new short[nLines, 11];

            for (int line = 0; line < nLines; line++)
            {
                var itemArray = dtPractice.Rows[line].ItemArray;
                for (int i = 0; i < 11; i++)
                {
                    setups[line, i] = short.Parse(itemArray[i].ToString());
                }
            }

            return setups;
        }

        //Convert Feedback to Code number
        sbyte GetWingFeedbackCode(string feedbackText)
        {
            sbyte wingCode = 0;

            if(feedbackText.StartsWith("I am really missing a lot of speed in straights"))
            {
                wingCode = 3;
            }
            else if (feedbackText.StartsWith("The car is lacking some speed in the straights"))
            {
                wingCode = 2;
            }
            else if (feedbackText.StartsWith("The car could have a bit more speed in the straights"))
            {
                wingCode = 1;
            }
            else if (feedbackText.StartsWith("Satisfied"))
            {
                wingCode = 0;
            }
            else if (feedbackText.StartsWith("I am missing a bit of grip in the curves"))
            {
                wingCode = -1;
            }
            else if (feedbackText.StartsWith("The car is very unstable in many corners"))
            {
                wingCode = -2;
            }
            else if (feedbackText.StartsWith("I cannot drive the car, there's no grip on it"))
            {
                wingCode = -3;
            }

            return wingCode;
        }
        sbyte GetEngineFeedbackCode(string feedbackText)
        {
            sbyte engineCode = 0;

            if (feedbackText.StartsWith("No, no, no!!! Favour a lot more the low revs!"))
            {
                engineCode = 3;
            }
            else if (feedbackText.StartsWith("The engine revs are too high"))
            {
                engineCode = 2;
            }
            else if (feedbackText.StartsWith("Try to favour a bit more the low revs"))
            {
                engineCode = 1;
            }
            else if (feedbackText.StartsWith("Satisfied"))
            {
                engineCode = 0;
            }
            else if (feedbackText.StartsWith("I feel that I do not have enough engine power in the straights"))
            {
                engineCode = -1;
            }
            else if (feedbackText.StartsWith("The engine power on the straights is not sufficient"))
            {
                engineCode = -2;
            }
            else if (feedbackText.StartsWith("You should try to favor a lot more the high revs"))
            {
                engineCode = -3;
            }

            return engineCode;
        }
        sbyte GetBrakesFeedbackCode(string feedbackText)
        {
            sbyte brakesCode = 0;

            if (feedbackText.StartsWith("Please, move the balance a lot more to the back"))
            {
                brakesCode = 3;
            }
            else if (feedbackText.StartsWith("I think the brakes effectiveness could be higher if we move the balance to the back"))
            {
                brakesCode = 2;
            }
            else if (feedbackText.StartsWith("Put the balance a bit more to the back"))
            {
                brakesCode = 1;
            }
            else if (feedbackText.StartsWith("Satisfied"))
            {
                brakesCode = 0;
            }
            else if (feedbackText.StartsWith("I would like to have the balance a bit more to the front"))
            {
                brakesCode = -1;
            }
            else if (feedbackText.StartsWith("I think the brakes effectiveness could be higher if we move the balance to the front"))
            {
                brakesCode = -2;
            }
            else if (feedbackText.StartsWith("I would feel a lot more comfortable to move the balance to the front"))
            {
                brakesCode = -3;
            }

            return brakesCode;
        }
        sbyte GetGearFeedbackCode(string feedbackText)
        {
            sbyte gearCode = 0;

            if (feedbackText.StartsWith("Please, put a lot lower ration between the gears"))
            {
                gearCode = 3;
            }
            else if (feedbackText.StartsWith("The gear ratio is too high"))
            {
                gearCode = 2;
            }
            else if (feedbackText.StartsWith("I cannot take advantage of the power of the engine. Put the gear ratio a bit lower"))
            {
                gearCode = 1;
            }
            else if (feedbackText.StartsWith("Satisfied"))
            {
                gearCode = 0;
            }
            else if (feedbackText.StartsWith("I am very often in the red. Put the gear ratio a bit higher"))
            {
                gearCode = -1;
            }
            else if (feedbackText.StartsWith("The gear ratio is too low"))
            {
                gearCode = -2;
            }
            else if (feedbackText.StartsWith("It feels like the engine is going to explode. Put a lot higher ratio between gears"))
            {
                gearCode = -3;
            }

            return gearCode;
        }
        sbyte GetSuspensionFeedbackCode(string feedbackText)
        {
            sbyte suspensionCode = 0;

            if (feedbackText.StartsWith("The car is far too righ. Lower a lot of rigidity"))
            {
                suspensionCode = 3;
            }
            else if (feedbackText.StartsWith("The suspension rigidity is too high"))
            {
                suspensionCode = 2;
            }
            else if (feedbackText.StartsWith("The car is too rigid. Lower a bit the rigidity"))
            {
                suspensionCode = 1;
            }
            else if (feedbackText.StartsWith("Satisfied"))
            {
                suspensionCode = 0;
            }
            else if (feedbackText.StartsWith("I think with a bit more rigid suspension I will be able to go faster"))
            {
                suspensionCode = -1;
            }
            else if (feedbackText.StartsWith("The suspension rigidity is too low"))
            {
                suspensionCode = -2;
            }
            else if (feedbackText.StartsWith("The suspension rigidity should be a lot higher"))
            {
                suspensionCode = -3;
            }

            return suspensionCode;
        }
    }
}
