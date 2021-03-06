﻿namespace RE4
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lvData = new System.Windows.Forms.ListView();
            this.columnDesc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListviewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.adjustDifficultyScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageHack = new System.Windows.Forms.ImageList(this.components);
            this.timerMemory = new System.Windows.Forms.Timer(this.components);
            this.ListviewMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvData
            // 
            this.lvData.BackColor = System.Drawing.SystemColors.WindowText;
            this.lvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDesc,
            this.columnValue});
            this.lvData.ContextMenuStrip = this.ListviewMenu;
            this.lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvData.ForeColor = System.Drawing.SystemColors.Window;
            this.lvData.FullRowSelect = true;
            this.lvData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvData.Location = new System.Drawing.Point(0, 0);
            this.lvData.Margin = new System.Windows.Forms.Padding(0);
            this.lvData.Name = "lvData";
            this.lvData.Size = new System.Drawing.Size(273, 421);
            this.lvData.StateImageList = this.imageHack;
            this.lvData.TabIndex = 0;
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.View = System.Windows.Forms.View.Details;
            // 
            // columnDesc
            // 
            this.columnDesc.Text = " Description";
            this.columnDesc.Width = 150;
            // 
            // columnValue
            // 
            this.columnValue.Text = "Value";
            this.columnValue.Width = 120;
            // 
            // ListviewMenu
            // 
            this.ListviewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adjustDifficultyScaleToolStripMenuItem});
            this.ListviewMenu.Name = "ListviewMenu";
            this.ListviewMenu.Size = new System.Drawing.Size(176, 48);
            this.ListviewMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ListviewMenu_Opening);
            // 
            // adjustDifficultyScaleToolStripMenuItem
            // 
            this.adjustDifficultyScaleToolStripMenuItem.Name = "adjustDifficultyScaleToolStripMenuItem";
            this.adjustDifficultyScaleToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.adjustDifficultyScaleToolStripMenuItem.Text = "Edit Difficulty Scale";
            this.adjustDifficultyScaleToolStripMenuItem.Click += new System.EventHandler(this.adjustDifficultyScaleToolStripMenuItem_Click);
            // 
            // imageHack
            // 
            this.imageHack.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageHack.ImageSize = new System.Drawing.Size(1, 24);
            this.imageHack.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // timerMemory
            // 
            this.timerMemory.Enabled = true;
            this.timerMemory.Interval = 200;
            this.timerMemory.Tick += new System.EventHandler(this.timerMemory_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 421);
            this.Controls.Add(this.lvData);
            this.Name = "frmMain";
            this.Text = "Resident Evil 4 Memory";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ListviewMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvData;
        private System.Windows.Forms.ColumnHeader columnDesc;
        private System.Windows.Forms.ColumnHeader columnValue;
        private System.Windows.Forms.ContextMenuStrip ListviewMenu;
        private System.Windows.Forms.Timer timerMemory;
        private System.Windows.Forms.ImageList imageHack;
        private System.Windows.Forms.ToolStripMenuItem adjustDifficultyScaleToolStripMenuItem;

    }
}

