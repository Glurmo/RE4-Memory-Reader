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

        private const string PROCESS_NAME = "bio4";

        public frmMain()
        {
            InitializeComponent();
        }

        private void timerMemory_Tick(object sender, EventArgs e)
        {
            if (_memoryReader == null || _residentEvilMemory == null)
            {
                _memoryReader = new MemoryReader(PROCESS_NAME);
                _residentEvilMemory = new ResidentEvilMemory();
            }
            if (!_memoryReader.IsProcessOpen() && !_memoryReader.OpenProcess(PROCESS_NAME))
            {
                lvData.Items.Clear();
                lvData.Items.Add(String.Format("Waiting for {0}.exe", PROCESS_NAME));
                _residentEvilMemory.Reset();
            }
            else
            {
                _residentEvilMemory.Populate(_memoryReader);
                _populateListView();
            }
        }

        
        private void adjustDifficultyScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int16 newValue = 0;
            string newValueString = Microsoft.VisualBasic.Interaction.InputBox("New value:", "Edit Dyanmic Difficulty Scale (1000 - 10000)");
            if (Int16.TryParse(newValueString, out newValue))
            {
                var memoryWriter = new MemoryWriter("bio4");
                memoryWriter.WriteInt16(0x085BE74, newValue);
                _residentEvilMemory.Populate(_memoryReader);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lvData.DoubleBuffered(true);
        }
        private void _populateListView()
        {
            lvData.BeginUpdate();
            lvData.Items.Clear();
            var defaultColour = lvData.ForeColor;
            var cs = _residentEvilMemory.CurrentState;
            var pv = _residentEvilMemory.PreviousValues;

            int dynamicDifficultyScaleDelta = cs.DynamicDifficultyScale - pv.DynamicDifficultyScale;
            int healthDelta = cs.HealthRemaining - pv.HealthRemaining;

            var rows = new[]
            {
                Tuple.Create("Difficulty Level", cs.DynamicDifficultyLevel.ToString(), defaultColour), 
                Tuple.Create("Difficulty Scale", cs.DynamicDifficultyScale.ToString(), defaultColour), 
                Tuple.Create(
                    "Scale Delta", 
                    dynamicDifficultyScaleDelta.ToString(), 
                    (dynamicDifficultyScaleDelta > 0) ? Color.Green : Color.Red
                ),
                Tuple.Create("", "", defaultColour),
                Tuple.Create(
                    "Health", 
                    String.Format("{0}/{1} ({2}%)", cs.HealthRemaining, cs.HealthTotal, cs.HealthPercentage), 
                    defaultColour
                ),
                Tuple.Create(
                    "Health Delta", 
                    healthDelta.ToString(), 
                    (healthDelta > 0) ? Color.Green : Color.Red
                ),
                Tuple.Create("", "", defaultColour),
                Tuple.Create("Kills (Chapter)", cs.ChapterKills.ToString(), defaultColour),
                Tuple.Create(
                    "Accuracy (Chapter)", 
                    String.Format("{0}/{1} ({2}%)", cs.ChapterShotsOnTarget, cs.ChapterShots, cs.ChapterAccuracy), 
                    defaultColour
                ),
                Tuple.Create("", "", defaultColour),
                Tuple.Create("Kills (Total)", cs.TotalKills.ToString(), defaultColour),
                Tuple.Create(
                    "Accuracy (Total)", 
                    String.Format("{0}/{1} ({2}%)", cs.TotalShotsOnTarget, cs.TotalShots, cs.TotalAccuracy), 
                    defaultColour
                ),
                Tuple.Create("", "", defaultColour),
                Tuple.Create("Deaths (Zone)", cs.LoadingAreaDeaths.ToString(), defaultColour),
                Tuple.Create("Deaths (Chapter)", cs.ChapterDeaths.ToString(), defaultColour),
                Tuple.Create("Deaths (Total)", cs.TotalDeaths.ToString(), defaultColour)
            };

            foreach (var row in rows)
            {
                var item = new ListViewItem
                {
                    Text = row.Item1,
                    Font = new Font(lvData.Font, FontStyle.Bold),
                    UseItemStyleForSubItems = false
                };
                var subItem = new ListViewItem.ListViewSubItem()
                {
                    Text = row.Item2,
                    ForeColor = row.Item3
                };
                item.SubItems.Add(subItem);
                lvData.Items.Add(item);
            }
            lvData.EndUpdate();
        }

        private void ListviewMenu_Opening(object sender, CancelEventArgs e)
        {
            adjustDifficultyScaleToolStripMenuItem.Enabled = _memoryReader.IsProcessOpen();
        }
    }
}
