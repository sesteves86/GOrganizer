using GOrganizer.Classes;
using GOrganizer.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOrganizer
{
    public partial class Form1 : Form
    {
        #region Initiate Season Planner Tab
        void InitiateSeasonPlannerTab()
        {
            InitiateTracksComboBoxes();
            SetInitialValuesTracksComboBoxes();

            FillSpTab();

            InitiateDtSeasonPlanner();
            InitiateDgvSp();
        }

        void InitiateTracksComboBoxes()
        {
            //Populte with all existing tracks
            List<string> trackList = new List<string>();
            foreach (DataRow row in dtTracks.Rows)
            {
                trackList.Add(row.ItemArray[1].ToString());
            }

            cBoxSpTrack1.DataSource = new List<string>(trackList);
            cBoxSpTrack2.DataSource = new List<string>(trackList);
            cBoxSpTrack3.DataSource = new List<string>(trackList);
            cBoxSpTrack4.DataSource = new List<string>(trackList);
            cBoxSpTrack5.DataSource = new List<string>(trackList);
            cBoxSpTrack6.DataSource = new List<string>(trackList);
            cBoxSpTrack7.DataSource = new List<string>(trackList);
            cBoxSpTrack8.DataSource = new List<string>(trackList);
            cBoxSpTrack9.DataSource = new List<string>(trackList);
            cBoxSpTrack10.DataSource = new List<string>(trackList);
            cBoxSpTrack11.DataSource = new List<string>(trackList);
            cBoxSpTrack12.DataSource = new List<string>(trackList);
            cBoxSpTrack13.DataSource = new List<string>(trackList);
            cBoxSpTrack14.DataSource = new List<string>(trackList);
            cBoxSpTrack15.DataSource = new List<string>(trackList);
            cBoxSpTrack16.DataSource = new List<string>(trackList);
            cBoxSpTrack17.DataSource = new List<string>(trackList);

            cBoxSpTrackNext1.DataSource = new List<string>(trackList);
            cBoxSpTrackNext2.DataSource = new List<string>(trackList);
            cBoxSpTrackNext3.DataSource = new List<string>(trackList);
            cBoxSpTrackNext4.DataSource = new List<string>(trackList);
            cBoxSpTrackNext5.DataSource = new List<string>(trackList);
            cBoxSpTrackNext6.DataSource = new List<string>(trackList);
            cBoxSpTrackNext7.DataSource = new List<string>(trackList);
            cBoxSpTrackNext8.DataSource = new List<string>(trackList);
            cBoxSpTrackNext9.DataSource = new List<string>(trackList);
            cBoxSpTrackNext10.DataSource = new List<string>(trackList);
            cBoxSpTrackNext11.DataSource = new List<string>(trackList);
            cBoxSpTrackNext12.DataSource = new List<string>(trackList);
            cBoxSpTrackNext13.DataSource = new List<string>(trackList);
            cBoxSpTrackNext14.DataSource = new List<string>(trackList);
            cBoxSpTrackNext15.DataSource = new List<string>(trackList);
            cBoxSpTrackNext16.DataSource = new List<string>(trackList);
            cBoxSpTrackNext17.DataSource = new List<string>(trackList);
        }
        void SetInitialValuesTracksComboBoxes()
        {
            List<Classes.Track> seasonTrackList =
                DB.SeasonTrack.ReadTracksListFromDB();

            cBoxSpTrack1.SelectedIndex = seasonTrackList[0].Id - 1;
            cBoxSpTrack2.SelectedIndex = seasonTrackList[1].Id - 1;
            cBoxSpTrack3.SelectedIndex = seasonTrackList[2].Id - 1;
            cBoxSpTrack4.SelectedIndex = seasonTrackList[3].Id - 1;
            cBoxSpTrack5.SelectedIndex = seasonTrackList[4].Id - 1;
            cBoxSpTrack6.SelectedIndex = seasonTrackList[5].Id - 1;
            cBoxSpTrack7.SelectedIndex = seasonTrackList[6].Id - 1;
            cBoxSpTrack8.SelectedIndex = seasonTrackList[7].Id - 1;
            cBoxSpTrack9.SelectedIndex = seasonTrackList[8].Id - 1;
            cBoxSpTrack10.SelectedIndex = seasonTrackList[9].Id - 1;
            cBoxSpTrack11.SelectedIndex = seasonTrackList[10].Id - 1;
            cBoxSpTrack12.SelectedIndex = seasonTrackList[11].Id - 1;
            cBoxSpTrack13.SelectedIndex = seasonTrackList[12].Id - 1;
            cBoxSpTrack14.SelectedIndex = seasonTrackList[13].Id - 1;
            cBoxSpTrack15.SelectedIndex = seasonTrackList[14].Id - 1;
            cBoxSpTrack16.SelectedIndex = seasonTrackList[15].Id - 1;
            cBoxSpTrack17.SelectedIndex = seasonTrackList[16].Id - 1;

            cBoxSpTrackNext1.SelectedIndex = seasonTrackList[17].Id - 1;
            cBoxSpTrackNext2.SelectedIndex = seasonTrackList[18].Id - 1;
            cBoxSpTrackNext3.SelectedIndex = seasonTrackList[19].Id - 1;
            cBoxSpTrackNext4.SelectedIndex = seasonTrackList[20].Id - 1;
            cBoxSpTrackNext5.SelectedIndex = seasonTrackList[21].Id - 1;
            cBoxSpTrackNext6.SelectedIndex = seasonTrackList[22].Id - 1;
            cBoxSpTrackNext7.SelectedIndex = seasonTrackList[23].Id - 1;
            cBoxSpTrackNext8.SelectedIndex = seasonTrackList[24].Id - 1;
            cBoxSpTrackNext9.SelectedIndex = seasonTrackList[25].Id - 1;
            cBoxSpTrackNext10.SelectedIndex = seasonTrackList[26].Id - 1;
            cBoxSpTrackNext11.SelectedIndex = seasonTrackList[27].Id - 1;
            cBoxSpTrackNext12.SelectedIndex = seasonTrackList[28].Id - 1;
            cBoxSpTrackNext13.SelectedIndex = seasonTrackList[29].Id - 1;
            cBoxSpTrackNext14.SelectedIndex = seasonTrackList[30].Id - 1;
            cBoxSpTrackNext15.SelectedIndex = seasonTrackList[31].Id - 1;
            cBoxSpTrackNext16.SelectedIndex = seasonTrackList[32].Id - 1;
            cBoxSpTrackNext17.SelectedIndex = seasonTrackList[33].Id - 1;
        }
        void FillSpTab()
        {
            SeasonPlannerTab spTab = DB.SeasonPlannerTab.ReadSeasonPlannerTabFromDB();
            txtSpNumberTries.Text = spTab.nRuns.ToString();
            txtSpTargetPoints.Text = spTab.TargetPoints.ToString();
            txtSpStartingMoney.Text = spTab.StartingBalanceM.ToString();
            cBoxSpDivision.SelectedIndex = spTab.Division;
        }
        void InitiateDtSeasonPlanner()
        {
            //Only has what appears on dgvSeasonPlanner
            dtSeasonPlanner = new DataTable();
            dtSeasonPlanner.Columns.Add("Race Number");
            dtSeasonPlanner.Columns.Add("Track Name");
            dtSeasonPlanner.Columns.Add("Qual Pos");
            dtSeasonPlanner.Columns.Add("Race Pos");
            //Editables
            dtSeasonPlanner.Columns.Add("Train", typeof(Classes.DriverTrainning));
            dtSeasonPlanner.Columns.Add("Test", typeof(Boolean));
            dtSeasonPlanner.Columns.Add("TargetCarLvl EngBra");
            dtSeasonPlanner.Columns.Add("TargetCarLvl Others");
            dtSeasonPlanner.Columns.Add("CT");
            dtSeasonPlanner.Columns.Add("Balance After Race/M");
            //Car Parts Change
            dtSeasonPlanner.Columns.Add("Chassis");
            dtSeasonPlanner.Columns.Add("Engine");
            dtSeasonPlanner.Columns.Add("FWing");
            dtSeasonPlanner.Columns.Add("RWing");
            dtSeasonPlanner.Columns.Add("Underbody");
            dtSeasonPlanner.Columns.Add("Sidepods");
            dtSeasonPlanner.Columns.Add("Cooling");
            dtSeasonPlanner.Columns.Add("Gearbox");
            dtSeasonPlanner.Columns.Add("Brakes");
            dtSeasonPlanner.Columns.Add("Suspension");
            dtSeasonPlanner.Columns.Add("Electronics");
            //Car Parts Wear
            dtSeasonPlanner.Columns.Add("ChassisWear");
            dtSeasonPlanner.Columns.Add("EngineWear");
            dtSeasonPlanner.Columns.Add("FWingWear");
            dtSeasonPlanner.Columns.Add("RWingWear");
            dtSeasonPlanner.Columns.Add("UnderbodyWear");
            dtSeasonPlanner.Columns.Add("SidepodsWear");
            dtSeasonPlanner.Columns.Add("CoolingWear");
            dtSeasonPlanner.Columns.Add("GearboxWear");
            dtSeasonPlanner.Columns.Add("BrakesWear");
            dtSeasonPlanner.Columns.Add("SuspensionWear");
            dtSeasonPlanner.Columns.Add("ElectronicsWear");
        }
        void InitiateDgvSp()
        {
            dgvSp.DataSource = dtSeasonPlanner;

            foreach (DataGridViewColumn col in dgvSp.Columns)
            {
                if (col.Name != "CT" && col.Name != "Test" && col.Name != "Train" && col.Name != "TargetCarLvl EngBra" && col.Name != "TargetCarLvl Others")
                    col.ReadOnly = true;
                else
                    col.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(200, 255, 200);

                col.Width = 50;
            }

            FillDgvSp();

            //Make header bold
            dgvSp.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(DataGridView.DefaultFont, System.Drawing.FontStyle.Bold);
            //dgvSp.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.Red; //System.Drawing.Color.FromArgb(0, 0, 255);
        }
        void FillDgvSp()
        {
            int initialRaceId = DB.RaceTab.ReadRaceTabFromDB().SeasonTrackIndex; //cBoxNextRacingTrack.SelectedIndex; //1; //Is it included on SpFull?
            SeasonPlannerDecision[] spDecisions = DB.SeasonPlannerTab.ReadSpDecisionsFromDB(initialRaceId - 1);
            SeasonPlannerFullLine spFull = GetInitialSpFull();
            ProcessDecisionsIntoDgv(spDecisions, initialRaceId, spFull);
        }
        #endregion

        //Form1 objects
        void bttSpOptimize_Click(object sender, EventArgs e)
        {
            finishedLoading = false;
            Random random = new Random();

            int nRuns = int.Parse(txtSpNumberTries.Text);
            int targetPoints = int.Parse(txtSpTargetPoints.Text);

            //Get Initial Values / SP Line
            SeasonPlannerForOptimizer spOptimizer = new SeasonPlannerForOptimizer();
            spOptimizer = GetInitialSeasonPlannerOptimizer();
            SeasonPlannerForDataTable spTable = new SeasonPlannerForDataTable();
            spTable.SeasonRaceNumber = cBoxNextRacingTrack.SelectedIndex;
            spTable.TrackId = spOptimizer.raceTab.SeasonTrackIndex; //-1?
            //SeasonPlannerTab spTab = new SeasonPlannerTab();
            //spTab = ReadSeasonPlannerTabFromForm();
            int initialRaceId = spTable.SeasonRaceNumber;

            SeasonPlannerFullLine spFull = GetInitialSpFull();
            //spFull2.spTable = spTable;
            //SeasonPlannerFullLine spFull = new SeasonPlannerFullLine(new SeasonPlannerDecision(), spTable, spOptimizer, spTab);
            
            //Process
            processingForm = new Processing(nRuns);
            processingForm.Show();
            //spDecisions = GetBestDecisions(nRuns, spTable.SeasonRaceNumber, random, spFull, targetPoints, maxBalance);
            
            GetBestDecisionsAsync(nRuns, spTable.SeasonRaceNumber, spFull, targetPoints);
            //SeasonPlannerDecision[] spDecisions = ReadSpDecisionsFromDgv();

            //finishedLoading = true;

            ////Update Values when process completes
            //dgvSp_CellValueChanged(null, null);
        }
        void bttSpGenerateNextSeason_Click(object sender, EventArgs e)
        {
            List<Classes.Track> trackList = DB.Track.ReadAllTracksFromDB();
            Classes.Track[] nextSeasonTracks = new Classes.Track[17];
            int nTracks = trackList.Count;

            Random r = new Random();

            for (int raceNumber = 0; raceNumber < 17; raceNumber++)
            {
                int trackId = r.Next(nTracks);
                nextSeasonTracks[raceNumber] = DB.Track.ReadTrackFromDB(trackId);
            }

            FillNextSeasonTracksTextBoxes(nextSeasonTracks);
        }
        void bttSpHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Constants.spHelpMessage, "Season Planner help message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void txtSpChanged(object sender, EventArgs e)
        {
            if (finishedLoading)
            {
                SeasonPlannerTab spTab = ReadSeasonPlannerTabFromForm();
                DB.SeasonPlannerTab.UpdateSeasonPlannerTab(spTab);
            }
        }
        void dgvSp_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (finishedLoading && !isProcessing)
            {
                isProcessing = true;

                int nRaces = dgvSp.Rows.Count;
                SeasonPlannerDecision[] spDecisions = new SeasonPlannerDecision[nRaces];
                for (int line = 0; line < nRaces; line++)
                {
                    spDecisions[line] = ReadSpDecisionFromDgv(line);
                }
                ProcessDecisionsIntoDgv(spDecisions, 17 - nRaces, GetInitialSpFull());

                isProcessing = false;
            }
        }

        //Others
        void WriteSpFullToDgv(SeasonPlannerFullLine spFull)
        {
            object[] rowObject = SpHelper.ConvertSpTableToRow(spFull.spTable, spFull.spDecisions);
            dtSeasonPlanner.Rows.Add(rowObject);
        }
        void ProcessDecisionsIntoDgv(SeasonPlannerDecision[] spDecisions, int _initialRaceId, SeasonPlannerFullLine spFull)
        {
            dtSeasonPlanner.Clear();

            int initialRaceId = _initialRaceId;

            spDecisions = spDecisions.Where(x => x != null).ToArray();

            for (int raceIndex = initialRaceId; raceIndex < 17; raceIndex++)
            {
                spFull.spDecisions = spDecisions[raceIndex-initialRaceId]; //spDecisions[j - initialRaceId];
                spFull.spTable.SeasonRaceNumber = raceIndex;
                spFull = SpHelper.ProcessSeasonPlanner(spFull);

                WriteSpFullToDgv(spFull);
            }
        }
        void FillNextSeasonTracksTextBoxes(Classes.Track[] trackList)
        {
            cBoxSpTrackNext1.Text = trackList[0].Name;
            cBoxSpTrackNext2.Text = trackList[1].Name;
            cBoxSpTrackNext3.Text = trackList[2].Name;
            cBoxSpTrackNext4.Text = trackList[3].Name;
            cBoxSpTrackNext5.Text = trackList[4].Name;
            cBoxSpTrackNext6.Text = trackList[5].Name;
            cBoxSpTrackNext7.Text = trackList[6].Name;
            cBoxSpTrackNext8.Text = trackList[7].Name;
            cBoxSpTrackNext9.Text = trackList[8].Name;
            cBoxSpTrackNext10.Text = trackList[9].Name;
            cBoxSpTrackNext11.Text = trackList[10].Name;
            cBoxSpTrackNext12.Text = trackList[11].Name;
            cBoxSpTrackNext13.Text = trackList[12].Name;
            cBoxSpTrackNext14.Text = trackList[13].Name;
            cBoxSpTrackNext15.Text = trackList[14].Name;
            cBoxSpTrackNext16.Text = trackList[15].Name;
            cBoxSpTrackNext17.Text = trackList[16].Name;
        }
        void SaveDgvSpToDB()
        {
            //DB.SeasonPlannerDecisions.ClearDB();

            int nRows = dgvSp.Rows.Count;

            for (int line = 17 - nRows; line < 17; line++)
            {
                SeasonPlannerDecision spDecisions = ReadSpDecisionFromDgv(line);
                DB.SeasonPlannerTab.UpdateSpDecision(spDecisions);
            }
        }

        SeasonPlannerForOptimizer GetInitialSeasonPlannerOptimizer()
        {
            SeasonPlannerForOptimizer spOptimizer = new SeasonPlannerForOptimizer();

            spOptimizer.Id = 1;
            spOptimizer.car = ReadCarFromForm();
            spOptimizer.driver = ReadDriverFromForm();
            spOptimizer.raceTab = ReadRaceTabFromForm();
            spOptimizer.staffFacilities = ReadSfFromForm();
            spOptimizer.technicalDirector = ReadTdFromForm();

            return spOptimizer;
        }
        SeasonPlannerFullLine GetInitialSpFull()
        {
            SeasonPlannerFullLine spFull = new SeasonPlannerFullLine();
            
            SeasonPlannerDecision[] spDecisions = DB.SeasonPlannerTab.ReadSpDecisionsFromDB(0);

            SeasonPlannerForOptimizer spOptimizer = new SeasonPlannerForOptimizer();
            RaceTab rTab = DB.RaceTab.ReadRaceTabFromDB();
            spOptimizer.raceTab = rTab;
            SeasonPlannerTab spTab = DB.SeasonPlannerTab.ReadSeasonPlannerTabFromDB();
            spOptimizer.spTab = spTab;
            StaffFacilities sf = DB.StaffFacilities.ReadStaffFacilitiesFromDB();
            spOptimizer.staffFacilities = sf;
            TechnicalDirector td = DB.TechnicalDirector.ReadTdFromDB();
            spOptimizer.technicalDirector = td;
            Driver driver = DB.Driver.ReadDriverFromDB();
            spOptimizer.driver = driver;
            Car car = DB.Car.ReadCarFromDB();
            spOptimizer.car = car;

            SeasonPlannerForDataTable spTable = new SeasonPlannerForDataTable();
            spTable.BalanceAfterRaceM = spTab.StartingBalanceM;

            spFull.spTable = spTable;
            spFull.spOptimizer = spOptimizer;
            spFull.spTab = spTab;
            spFull.spDecisions = spDecisions[0];

            return spFull;
        }
        SeasonPlannerDecision ReadSpDecisionFromDgv(int line)
        {
            int nRows = dgvSp.Rows.Count;

            SeasonPlannerDecision spDecisions = new SeasonPlannerDecision();

            if (line < nRows)
            {
                string trainning = dgvSp.Rows[line].Cells["Train"].Value.ToString();//Should be #4. It appears on 33
                string testing = dgvSp.Rows[line].Cells["Test"].Value.ToString();
                string targetCarLevelEngBra = dgvSp.Rows[line].Cells["TargetCarLvl EngBra"].Value.ToString();
                string targetCarLevelOthers = dgvSp.Rows[line].Cells["TargetCarLvl Others"].Value.ToString();
                string ct = dgvSp.Rows[line].Cells["CT"].Value.ToString();

                spDecisions.Training = (Classes.DriverTrainning)Enum.Parse(typeof(Classes.DriverTrainning), trainning);
                spDecisions.Testing = bool.Parse(testing);
                spDecisions.TargetCarLevelEngBra = int.Parse(targetCarLevelEngBra);
                spDecisions.TargetCarLevelOthers = int.Parse(targetCarLevelOthers);
                spDecisions.CT = int.Parse(ct);
                spDecisions.SeasonRace = int.Parse(dgvSp.Rows[line].Cells[0].Value.ToString());
            }
            return spDecisions;
        }
        SeasonPlannerTab ReadSeasonPlannerTabFromForm()
        {
            SeasonPlannerTab spTab = new SeasonPlannerTab();

            spTab.TargetPoints = int.Parse(txtSpTargetPoints.Text.ToString());
            spTab.Division = (short)(cBoxSpDivision.SelectedIndex);
            int nRuns = 0;
                int.TryParse(txtSpNumberTries.Text.ToString(), out nRuns);
            spTab.nRuns = nRuns;
            spTab.StartingBalanceM = int.Parse(txtSpStartingMoney.Text.ToString());

            return spTab;
        }
        List<Classes.Track> ReadSeasonsTracksFromForm()
        {
            List<Classes.Track> trackList = new List<Track>();

            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack1.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack2.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack3.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack4.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack5.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack6.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack7.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack8.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack9.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack10.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack11.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack12.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack13.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack14.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack15.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack16.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrack17.SelectedIndex + 1));

            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext1.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext2.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext3.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext4.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext5.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext6.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext7.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext8.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext9.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext10.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext11.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext12.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext13.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext14.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext15.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext16.SelectedIndex + 1));
            trackList.Add(DB.Track.ReadTrackFromDB(cBoxSpTrackNext17.SelectedIndex + 1));


            return trackList;
        }
        SeasonPlannerDecision[] ReadSpDecisionsFromDgv()
        {
            int dgvSize = dgvSp.Rows.Count;
            SeasonPlannerDecision[] spDecisions = new SeasonPlannerDecision[dgvSize];

            for (int line = 0; line < dgvSize; line++)
            {
                spDecisions[line] = ReadSpDecisionFromDgv(line);
            }

            return spDecisions;
        }
        float ReadFinalBalanceFromDgvSp()
        {
            float balance = 0;
            int dgvSize = dgvSp.Rows.Count;

            object oBalance = dgvSp.Rows[dgvSize - 1].Cells["Balance After Race/M"].Value;
            balance = float.Parse(oBalance.ToString());

            return balance;
        }

        #region Button press async area
        async void GetBestDecisionsAsync(int nRuns, int _initialRaceId, SeasonPlannerFullLine spFull,
                int targetPoints)
        {
            isSpProcessing = true;
            SeasonPlannerDecision[] bestSpDecisions = ReadSpDecisionsFromDgv();
            float maxBalance = ReadFinalBalanceFromDgvSp();
            Random random = new Random();
            SeasonPlannerResult spResult;
            spFull.spTable.BalanceAfterRaceM = spFull.spTab.StartingBalanceM;
            spFull.spTab.Division = cBoxSpDivision.SelectedIndex;

            for (int run = 0; run < nRuns; run++)
            {
                spResult = await GetBestDecisionThread2( _initialRaceId, spFull, random);
                
                if ( spResult.finalPoints >= targetPoints && spResult.finalBalanceM > maxBalance)
                {
                    maxBalance = spResult.finalBalanceM;
                    bestSpDecisions = (SeasonPlannerDecision[])spResult.spDecisions.Clone();
                }

                if (processingForm.WasCancelled())
                {
                    run = nRuns;
                    break;
                }

                processingForm.IncreaseItems();
                random = spResult.random;

            }
            
            processingForm.Close();
            
            bestSpDecisions = bestSpDecisions.Where(x => x != null).ToArray();
            ProcessDecisionsIntoDgv(bestSpDecisions, _initialRaceId, spFull);
            isSpProcessing = false;

            finishedLoading = true;

            //Update Values when process completes
            dgvSp_CellValueChanged(null, null);

            MessageBox.Show("Optimization completed!", "Optimization");
        }

        
        Task<SeasonPlannerResult> GetBestDecisionThread2(int initialRaceId, SeasonPlannerFullLine spFull, Random random)
        {
            return Task.Factory.StartNew(() => GetBestDecisionThread3( initialRaceId, spFull, random));   
        }
        SeasonPlannerResult GetBestDecisionThread3(int initialRaceId, SeasonPlannerFullLine spFull, Random random)
        {
            SeasonPlannerDecision[] spDecisions = new SeasonPlannerDecision[17];
            SeasonPlannerDecision[] bestSpDecisions = new SeasonPlannerDecision[17];

            //Generate and test spDecision
            for (int j = initialRaceId; j < 17; j++)
            {
                spDecisions[j - initialRaceId] = SpHelper.GenerateRandomDecisionValues(random);
                spFull.spDecisions = spDecisions[j - initialRaceId];

                spFull.spTable.SeasonRaceNumber = j;
                spFull = SpHelper.ProcessSeasonPlanner(spFull);
            }

            SeasonPlannerResult spDecBal = new SeasonPlannerResult();
            spDecBal.spDecisions = (SeasonPlannerDecision[])spDecisions.Clone();
            spDecBal.random = random;
            spDecBal.finalPoints = spFull.spTab.CurrentPoints;
            spDecBal.finalBalanceM = spFull.spTable.BalanceAfterRaceM;

            return spDecBal;
        }
        #endregion
    }
}
