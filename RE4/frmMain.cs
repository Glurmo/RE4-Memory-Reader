using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RE4
{
    public partial class frmMain : Form
    {
        private MemoryReader _memoryReader;
        private ResidentEvilMemory _residentEvilMemory;

        public frmMain()
        {
            InitializeComponent();
        }


        private void timerMemory_Tick(object sender, EventArgs e)
        {
            if (_residentEvilMemory == null)
            {
                _memoryReader = new MemoryReader("bio4");
                _residentEvilMemory = new ResidentEvilMemory();
            }
            _residentEvilMemory.Populate(_memoryReader);
            _populateListView();
        }

        private void _populateListView()
        {
            lvData.BeginUpdate();
            lvData.Items.Clear();
            var defaultColour = lvData.ForeColor;
            var r = _residentEvilMemory;

            var rows = new []
            {
                Tuple.Create("Dynamic Difficulty Level", r.DynamicDifficultyLevel.ToString(), defaultColour), 
                Tuple.Create("Dynamic Difficulty Scale", r.DynamicDifficultyScale.ToString(), defaultColour), 
                Tuple.Create(
                    "Difficulty Scale Delta", 
                    r.PreviousDynamicDifficultyScaleDelta.ToString(), 
                    (r.PreviousDynamicDifficultyScaleDelta > 0) ? Color.Green : Color.Red
                ),
                Tuple.Create("", "", defaultColour),
                Tuple.Create(
                    "Health", 
                    String.Format("{0}/{1} ({2}%)", r.HealthCurrent, r.HealthTotal, r.HealthPercentage), 
                    defaultColour
                ),
                Tuple.Create(
                    "Health Delta", 
                    r.PreviousHealthDelta.ToString(), 
                    (r.PreviousHealthDelta > 0) ? Color.Green : Color.Red
                ),
                Tuple.Create("", "", defaultColour),
                Tuple.Create("Kills (Chapter)", r.ChapterKills.ToString(), defaultColour),
                Tuple.Create(
                    "Accuracy (Chapter)", 
                    String.Format("{0}/{1} ({2}%)", r.ChapterShotsOnTarget, r.ChapterShots, r.ChapterAccuracy), 
                    defaultColour
                ),
                Tuple.Create("", "", defaultColour),
                Tuple.Create("Kills (Total)", r.TotalKills.ToString(), defaultColour),
                Tuple.Create(
                    "Accuracy (Total)", 
                    String.Format("{0}/{1} ({2}%)", r.TotalShotsOnTarget, r.TotalShots, r.TotalAccuracy), 
                    defaultColour
                ),
                Tuple.Create("", "", defaultColour),
                Tuple.Create("Deaths (Zone)", r.LoadingAreaDeaths.ToString(), defaultColour),
                Tuple.Create("Deaths (Chapter)", r.ChapterDeaths.ToString(), defaultColour),
                Tuple.Create("Deaths (Total)", r.TotalDeaths.ToString(), defaultColour)
            };

            foreach (var row in rows)
            {
                var item = new ListViewItem();
                item.Text = row.Item1;
                var subItem = new ListViewItem.ListViewSubItem()
                {
                    Text = row.Item2,
                    ForeColor = row.Item3
                };
                item.UseItemStyleForSubItems = false;
                item.SubItems.Add(subItem);
                lvData.Items.Add(item);
            }
            lvData.EndUpdate();
        }

        private void menuItemStart_Click(object sender, EventArgs e)
        {
            timerMemory.Start();
        }

        private void menuItemStop_Click(object sender, EventArgs e)
        {
            timerMemory.Stop();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lvData.DoubleBuffered(true);
        }
    }
}
