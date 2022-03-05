using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SortVisualizer
{
    public partial class Form1 : Form
    {
        //int[] panelArray;
        List<int> panelArray;
        Graphics g;
        BackgroundWorker bgw = null;
        bool Paused = false;
        int value = 0;
        Brush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        Brush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        public Form1()
        {
            InitializeComponent();
            DropDownMenu();
            this.Resize += Form1_Resize;
        }

        void Form1_Resize(object sender, EventArgs e)
        {
            btnShuffle_Click(null, null);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void DropDownMenu()
        {
            //get all the class that is implement with interface
            List<string> ClassList = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                 .Where(x => typeof(ISortVisualizer).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                 .Select(x => x.Name).ToList();
            ClassList.Sort();
            foreach (string entry in ClassList)
            {
                comboBox1.Items.Add(entry);
            }
            comboBox1.SelectedIndex = 0;
        }
        private bool isSorted()
        {
            for (int i = 0; i < panelArray.Count()-1;i++)
            {
                if (panelArray[i] > panelArray[i+1])
                {
                    return false;
                }
            }
            return true;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            /*
             * Case: click on start button before initlize and draw array on the panel
             */
            if (panelArray == null)
            {
                btnShuffle_Click(null, null);
            }
            /*
             * Case: where user click on the Start button after finish sorting instead of Shuffle
             */
            if (isSorted())
            {
                btnShuffle_Click(null, null);
            }
            bgw = new BackgroundWorker();
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerAsync(argument: comboBox1.SelectedItem);

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (!Paused)
            {
                bgw.CancelAsync();
                Paused = true;
            }
            else
            {
                if (bgw.IsBusy) return;
                int numEntries = visualpnl.Width;
                int MaxVal = visualpnl.Height;
                Paused = false;
                for (int i = 0; i < numEntries; i++)
                {
                    g.FillRectangle(BlackBrush, i, 0, 1, MaxVal);
                    g.FillRectangle(WhiteBrush, i, MaxVal - panelArray[i], 1, MaxVal);
                }
                bgw.RunWorkerAsync(argument: comboBox1.SelectedItem);
            }
        }
        
        private void btnShuffle_Click(object sender, EventArgs e)
        {
            g = visualpnl.CreateGraphics();
            int numEntries = visualpnl.Width;
            int MaxVal = visualpnl.Height;
            panelArray = new List<int>();
            Random rand = new Random();
            if (Sliderbar.Value == 100)
            {
                value = 1;
            }
            else { value = 100 - Sliderbar.Value; }
            /*
             *  Populate array with random number
             */
            for (int i = 0; i < numEntries - value;)
            {
                panelArray.Add(rand.Next(value, MaxVal));
                i += value + 1;
            }
            
            g.FillRectangle(BlackBrush, 0, 0, numEntries, MaxVal);
            /*
             * Draw the value in the array on the panel
             */
            int k = 0;
            foreach (int i in panelArray)
            {
                g.FillRectangle(WhiteBrush, k, MaxVal - i, value, MaxVal);
                k += value + 1;
            }
        }

        #region BackGroundWorkPlace
        public void bgw_DoWork(Object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker; //explicit casting sender as BackgroundWorker
            string SortAlgorithm = (string)e.Argument;
            Type type = Type.GetType("SortVisualizer." + SortAlgorithm);
            var ctors = type.GetConstructors(); //get contrucstor of that type
            try
            {
                ISortVisualizer sv = (ISortVisualizer)ctors[0].Invoke(new object[] { panelArray.ToArray(), g, visualpnl.Height,value });
                while (!sv.IsSorted()&&!bgw.CancellationPending)
                {
                    sv.NextSort();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0} Exception caught.", ex);
            }
        }
        #endregion

        private void Sliderbar_ValueChanged(object sender, EventArgs e)
        {
            SliderSize.Text = "Bar Width: " + Sliderbar.Value.ToString();
            btnShuffle_Click(null, null);
        }
    }
}
