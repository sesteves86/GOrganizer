using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOrganizer
{
    public partial class Processing : Form
    {
        private int currentItems;
        private int maxItems;
        private bool wasCancelled;

        public Processing(int _maxItems)
        {
            maxItems = _maxItems;
            currentItems = 0;
            wasCancelled = false;

            InitializeComponent();
        }

        public void IncreaseItems(int increment=1)
        {
            currentItems += increment;

            progressBar1.Value = currentItems*100/maxItems;

            //if (currentItems >= maxItems)
            //    this.Close();
        }

        private void bttProcessingCancel_Click(object sender, EventArgs e)
        {
            wasCancelled = true;
            currentItems = 0;
            Close();
        }

        public bool WasCancelled()
        {
            return wasCancelled;
        }
    }
}
