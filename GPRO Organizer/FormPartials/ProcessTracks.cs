using GOrganizer.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOrganizer
{
    public partial class Form1 : Form
    {
        void InitiateTrackTab()
        {
            InitiateTrackTable();
            List<Classes.Track> trackList = DB.Track.ReadAllTracksFromDB();
            PopulateTrackTable(trackList);
            PopulateTrackComboBox();
        }

        void InitiateTrackTable()
        {
            dtTracks = new DataTable();

            dtTracks.Columns.Add("Id");
            dtTracks.Columns.Add("Name");
            dtTracks.Columns.Add("DistanceKm");
            dtTracks.Columns.Add("Laps");
            dtTracks.Columns.Add("P");
            dtTracks.Columns.Add("H");
            dtTracks.Columns.Add("A");
            dtTracks.Columns.Add("Fuel Consumption");
            dtTracks.Columns.Add("Fuel Constant");
            dtTracks.Columns.Add("Tyre Wear");
            dtTracks.Columns.Add("Pitstop Time");
            dtTracks.Columns.Add("Downforce");
            dtTracks.Columns.Add("Overtake");
            dtTracks.Columns.Add("Suspension");
            dtTracks.Columns.Add("Grip Level");
            dtTracks.Columns.Add("WS");
            dtTracks.Columns.Add("TDC Const");

            dtTracks.Columns.Add("Chassis Wear Constant");
            dtTracks.Columns.Add("Engine Wear Constant");
            dtTracks.Columns.Add("FWing Wear Constant");
            dtTracks.Columns.Add("RWing Wear Constant");
            dtTracks.Columns.Add("Underbody Wear Constant");
            dtTracks.Columns.Add("Sidepods Wear Constant");
            dtTracks.Columns.Add("Cooling Wear Constant");
            dtTracks.Columns.Add("Gearbox Wear Constant");
            dtTracks.Columns.Add("Brakes Wear Constant");
            dtTracks.Columns.Add("Suspension Wear Constant");
            dtTracks.Columns.Add("Electronics Wear Constant");

            dgvTracks.DataSource = dtTracks;

            //int formWidth = Form1.ActiveForm.Width;
            int formWidth = Screen.FromControl(this).Bounds.Width;

            for (int i = 0; i < dgvTracks.Columns.Count; i++)
            {
                dgvTracks.Columns[i].Width = (int)((formWidth - 490)/17);
            }
            dgvTracks.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font(DataGridView.DefaultFont, System.Drawing.FontStyle.Bold);

        }
        void PopulateTrackTable(List<Classes.Track> tracks)
        {
            foreach (Classes.Track track in tracks)
            {
                Object[] formatedTrack = track.ConvertTrackToObjectArrayForDataTableRace();
                dtTracks.Rows.Add(formatedTrack);
            }
            
        }
        void PopulateTrackComboBox()
        {
            cBoxDownforceTrack.DataSource = Enum.GetNames(typeof(Levels.HighLow));
            cBoxFuelConsumption.DataSource = Enum.GetNames(typeof(Levels.HighLow));
            cBoxGripLevelTrack.DataSource = Enum.GetNames(typeof(Levels.HighLow));
            cBoxOvertakeTrack.DataSource = Enum.GetNames(typeof(Levels.EasyHard));
            cBoxSuspensionTrack.DataSource = Enum.GetNames(typeof(Levels.HighLow));
            cBoxTyreWearTrack.DataSource = Enum.GetNames(typeof(Levels.HighLow));
        }

        Classes.Track ReadTrackFromTrackTab()
        {
            int id = 0;
            if (txtIdTrack.Text != "")
            {
                id = int.Parse(txtIdTrack.Text);
            }
            string name = txtNameTrack.Text;
            int distanceKm = 1;
                int.TryParse(txtDistanceKmTrack.Text, out distanceKm);
            int laps = 1;
                int.TryParse(txtLapsTrack.Text, out laps);
            int power = 1;
                int.TryParse(txtPTrack.Text, out power);
            int handling = 1;
                int.TryParse(txtHTrack.Text, out handling);
            int acceleration = 1;
                int.TryParse(txtATrack.Text, out acceleration);
            int fuelConsumption = cBoxFuelConsumption.SelectedIndex;
            Single fuelConstant = 1;
                Single.TryParse(txtFuelConstantTrack.Text, out fuelConstant);
            int tyreWear = cBoxTyreWearTrack.SelectedIndex;
            Single pitStopTime = 1;
                Single.TryParse(txtPitStopTrack.Text, out pitStopTime);
            int downforce = cBoxDownforceTrack.SelectedIndex;
            int overtake = cBoxOvertakeTrack.SelectedIndex;
            int suspension = cBoxSuspensionTrack.SelectedIndex;
            int gripLevel = cBoxGripLevelTrack.SelectedIndex;
            string WS = txtWSTrack.Text;
            Single tdcConstant = 1;
                Single.TryParse(txtTdcConstant.Text, out tdcConstant);

            Classes.Track track = new Classes.Track(
                id, name, distanceKm, laps, power, handling, acceleration, (Levels.HighLow)fuelConsumption, 
                fuelConsumption, pitStopTime, (Levels.HighLow)tyreWear, (Levels.HighLow)downforce,
                (Levels.EasyHard)overtake, (Levels.HighLow)suspension, (Levels.HighLow)gripLevel,
                WS, tdcConstant
                );

            float chassisWearConstant = 0;
                float.TryParse(txtChassisWearConstant.Text, out chassisWearConstant);
            float engineWearConstant = 0;
                float.TryParse(txtEngineWearConstant.Text, out engineWearConstant);
            float fWingWearConstant = 0;
                float.TryParse(txtFWingWearConstant.Text, out fWingWearConstant);
            float rWingWearConstant = 0;
                float.TryParse(txtRWingWearConstant.Text, out rWingWearConstant);
            float underbodyWearConstant = 0;
                float.TryParse(txtUnderbodyWearConstant.Text, out underbodyWearConstant);
            float sidepodsWearConstant = 0;
                float.TryParse(txtSidepodsWearConstant.Text, out sidepodsWearConstant);
            float coolingWearConstant = 0;
                float.TryParse(txtCoolingWearConstant.Text, out coolingWearConstant);
            float gearboxWearConstant = 0;
                float.TryParse(txtGearboxWearConstant.Text, out gearboxWearConstant);
            float brakesWearConstant = 0;
                float.TryParse(txtBrakesWearConstant.Text, out brakesWearConstant);
            float suspensionWearConstant = 0;
                float.TryParse(txtSuspensionWearConstant.Text, out suspensionWearConstant);
            float electronicsWearConstant = 0;
                float.TryParse(txtElectronicsWearConstant.Text, out electronicsWearConstant);

            track.SetWearConstants(
                chassisWearConstant, engineWearConstant, fWingWearConstant,
                rWingWearConstant, underbodyWearConstant, sidepodsWearConstant, coolingWearConstant,
                gearboxWearConstant, brakesWearConstant, suspensionWearConstant, electronicsWearConstant
                );

            return track;
        }

        //CRUD
        void CreateTrack()
        {
            Classes.Track track = ReadTrackFromTrackTab();

            DB.Track.SaveTrackToDb(track);

            //AddTrackToTable
            Object[] formatedTrack = track.ConvertTrackToObjectArrayForDataTableRace();
            dtTracks.Rows.Add(formatedTrack);
        }
        void ReadTrack(int index)
        {
            int id = GetTrackId(index);
            string expression = "Id Like '" + id.ToString() + "'";
            DataRow row = dtTracks.Select(expression).First();

            txtIdTrack.Text = row["Id"].ToString();
            txtNameTrack.Text = row["Name"].ToString();
            txtDistanceKmTrack.Text = row["DistanceKm"].ToString();
            txtLapsTrack.Text = row["Laps"].ToString();
            txtPTrack.Text = row["P"].ToString();
            txtHTrack.Text = row["H"].ToString();
            txtATrack.Text = row["A"].ToString();
            cBoxFuelConsumption.Text = row["Fuel Consumption"].ToString();
            txtFuelConstantTrack.Text = row["Fuel Constant"].ToString();
            cBoxTyreWearTrack.Text = row["Tyre Wear"].ToString();
            txtPitStopTrack.Text = row["Pitstop Time"].ToString();
            cBoxDownforceTrack.Text = row["Downforce"].ToString();
            cBoxOvertakeTrack.Text = row["Overtake"].ToString();
            cBoxSuspensionTrack.Text = (row["Suspension"].ToString());
            cBoxGripLevelTrack.Text = (row["Grip Level"].ToString());
            txtWSTrack.Text = row["WS"].ToString();
            txtTdcConstant.Text = row["TDC Const"].ToString();

            txtChassisWearConstant.Text = row[17].ToString();
            txtEngineWearConstant.Text = row[18].ToString();
            txtFWingWearConstant.Text = row[19].ToString();
            txtRWingWearConstant.Text = row[20].ToString();
            txtUnderbodyWearConstant.Text = row[21].ToString();
            txtSidepodsWearConstant.Text = row[22].ToString();
            txtCoolingWearConstant.Text = row[23].ToString();
            txtGearboxWearConstant.Text = row[24].ToString();
            txtBrakesWearConstant.Text = row[25].ToString();
            txtSuspensionWearConstant.Text = row[26].ToString();
            txtElectronicsWearConstant.Text = row[27].ToString();
            
        }
        void UpdateTrack()
        {
            Classes.Track track = ReadTrackFromTrackTab();

            DB.Track.UpdateTrackToDb(track);

            Object[] formatedTrack = track.ConvertTrackToObjectArrayForDataTableRace();

            string expression = "Id Like '" + track.Id.ToString() + "'";
            int index = dtTracks.Rows.IndexOf(dtTracks.Select(expression).First());

            //DataRow row = dtTracks.Select("Id = " + track.Id).First();

            dtTracks.Rows[index].ItemArray = formatedTrack;
        }
        void DeleteTrack(int trackId, int rowIndex)
        {
            //Update DB
            DB.Track.DeleteTrackFromDB(trackId);

            //Remove from DGV
            dtTracks.Rows.RemoveAt(rowIndex);
        }

        //Buttons
        private void bttCreateTrack_Click(object sender, EventArgs e)
        {
            CreateTrack();
        }
        private void bttUpdateTrack_Click(object sender, EventArgs e)
        {
            UpdateTrack();
        }
        private void bttDeleteTrack_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvTracks.CurrentCell.RowIndex;
            int trackId = int.Parse(dtTracks.Rows[rowIndex].ItemArray[0].ToString());

            if(rowIndex>-1)
                DeleteTrack(trackId, rowIndex);
        }
        private void bttTracksHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Constants.trackHelpMessage, "Tracks help message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void dgvTracks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            ReadTrack(index);
        }

        //Others
        Classes.Track GetTrack(object[] itemArray)
        {
            Track track = new Track();

            try
            {
                int iTemp = 0;
                if (int.TryParse(itemArray[0].ToString(), out iTemp))
                    track.Id = iTemp;
                else
                    MessageBox.Show("Error parsing track.Id on ConvertObjectArrayToTrack()");

                track = GetTrack(track.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: {0}", ex.Message);
                return null;
            }

            return track;
        }
        Classes.Track GetTrack(int id)
        {
            Classes.Track track = new Track();

            //ToDo: Get the track from the dtTrack?
            track = DB.Track.ReadTrackFromDB(id);

            return track;
        }
        int GetTrackId(int dtTrackIndex)
        {
            int id = int.Parse(dgvTracks.Rows[dtTrackIndex].Cells["Id"].Value.ToString());

            return id;
        }
    }
}
