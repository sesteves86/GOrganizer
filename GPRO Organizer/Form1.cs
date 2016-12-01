using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace GOrganizer
{
    public partial class Form1 : Form
    {
        //To Do: Include TestingTab and RaceAnalysisTab
        //To Do: Must these DataTables be global variables?
        DataTable dtTracks, dtPractice, dtRaceTrack, dtRacingTimes, dtSeasonPlanner;
        bool finishedLoading = false;
        bool isProcessing = false;
        bool isSpProcessing = false;
        Processing processingForm;
        //Random random;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {   
            //Initiate tabs
            InitiateQualifyingTab();
            InitiateTrackTab();
            InitiateSeasonPlannerTab();
            InitiateSkillsTab();
            InitiateRaceTab();

            //Graphics
            InitiateImages();
            this.WindowState = FormWindowState.Maximized;
            ResizeDgvs();

            //Others
            finishedLoading = true;

            //Late Loadings
            InitiateRaceTab_LateLoading();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if(finishedLoading)
                ResizeDgvs();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Save RaceTab
            Classes.RaceTab rt = ReadRaceTabFromForm();
            DB.RaceTab.UpdateRaceTabToDb(rt);

            //Save Practice Laps
            DB.Practise.ClearPractise();
            DB.Practise.UpdatePractise(GetSetupsFromDataTablePractise());

            //Save SpTab
            List<Classes.Track> trackList = ReadSeasonsTracksFromForm();
            DB.SeasonTrack.UpdateTrackListToDb(trackList);
            SaveDgvSpToDB();

        }

        //Others
        private void txtBox_Int(object sender, KeyPressEventArgs e)
        {
            char keyPressed = e.KeyChar;

            if (!char.IsDigit(keyPressed) && keyPressed != '\b' && keyPressed != '\n' && keyPressed != '\t')
            {
                e.Handled = true;
            }
        }
        private void txtBox_Float(object sender, KeyPressEventArgs e)
        {
            char keyPressed = e.KeyChar;

            if (!char.IsDigit(keyPressed) && keyPressed != '.' &&
                    keyPressed != '\b' && keyPressed != '\n' && keyPressed != '\t')
            {
                e.Handled = true;
            }

            string text = ((TextBox)sender).Text;

            if(keyPressed == '.' && text.Contains('.'))
            {
                e.Handled = true;
            }
        }

        private void InitiateImages()
        {
            string path = Environment.CurrentDirectory;
            path = Path.GetFullPath(Path.Combine(path, @"..\.."));
            pictureUpdateCar.ImageLocation = path + @"\Images\Car_32.png";
            pictureUpdateDriver.ImageLocation = path + @"\Images\helmet_32.png";
            pictureUpdateWeather.ImageLocation = path + @"\Images\cloudy.gif";
            pictureUpdateSf.ImageLocation = path + @"\Images\td_32.png";
            pictureUpdateTd.ImageLocation = path + @"\Images\td_32.png";
            pictureUpdateTyre.ImageLocation = path + @"\Images\tyres_32.png";
        }
        private void ResizeDgvs()
        {
            int windowsWidth;
            int windowsHeight;

            try { 
                windowsWidth = Form1.ActiveForm.Width;
                windowsHeight = Form1.ActiveForm.Height;
            }
            catch   
            {
                windowsWidth = Screen.FromControl(this).Bounds.Width;
                windowsHeight = Screen.FromControl(this).Bounds.Height;
            }
            tabControl1.Height = windowsHeight - 60;
            tabControl1.Width = windowsWidth - 30;
            dgvRaceTrack.Width = windowsWidth - 60;
            dgvSp.Width = windowsWidth - 310;
            dgvTracks.Width = windowsWidth - 480;
        }
    }
}
