namespace Minesweeper.Gui
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.panelInfo = new System.Windows.Forms.Panel();
            this.tableLayoutPanelInfo = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxMinesCount = new System.Windows.Forms.PictureBox();
            this.pictureBoxSmileButton = new System.Windows.Forms.PictureBox();
            this.pictureBoxTime = new System.Windows.Forms.PictureBox();
            this.panelGame = new System.Windows.Forms.Panel();
            this.timerGame = new System.Windows.Forms.Timer(this.components);
            this.menuStripGame = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemMain = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHighScore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.panelInfo.SuspendLayout();
            this.tableLayoutPanelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinesCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSmileButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTime)).BeginInit();
            this.menuStripGame.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelInfo
            // 
            this.panelInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelInfo.Controls.Add(this.tableLayoutPanelInfo);
            this.panelInfo.Location = new System.Drawing.Point(6, 31);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(227, 60);
            this.panelInfo.TabIndex = 8;
            // 
            // tableLayoutPanelInfo
            // 
            this.tableLayoutPanelInfo.ColumnCount = 3;
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfo.Controls.Add(this.pictureBoxMinesCount, 0, 0);
            this.tableLayoutPanelInfo.Controls.Add(this.pictureBoxSmileButton, 1, 0);
            this.tableLayoutPanelInfo.Controls.Add(this.pictureBoxTime, 2, 0);
            this.tableLayoutPanelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelInfo.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelInfo.Name = "tableLayoutPanelInfo";
            this.tableLayoutPanelInfo.RowCount = 1;
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelInfo.Size = new System.Drawing.Size(223, 56);
            this.tableLayoutPanelInfo.TabIndex = 0;
            // 
            // pictureBoxMinesCount
            // 
            this.pictureBoxMinesCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBoxMinesCount.Image = global::Minesweeper.Properties.Resources.clock000;
            this.pictureBoxMinesCount.Location = new System.Drawing.Point(3, 3);
            this.pictureBoxMinesCount.Name = "pictureBoxMinesCount";
            this.pictureBoxMinesCount.Size = new System.Drawing.Size(74, 50);
            this.pictureBoxMinesCount.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxMinesCount.TabIndex = 0;
            this.pictureBoxMinesCount.TabStop = false;
            // 
            // pictureBoxSmileButton
            // 
            this.pictureBoxSmileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxSmileButton.Image = global::Minesweeper.Properties.Resources.smileButton;
            this.pictureBoxSmileButton.Location = new System.Drawing.Point(89, 3);
            this.pictureBoxSmileButton.Name = "pictureBoxSmileButton";
            this.pictureBoxSmileButton.Size = new System.Drawing.Size(44, 50);
            this.pictureBoxSmileButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxSmileButton.TabIndex = 1;
            this.pictureBoxSmileButton.TabStop = false;
            // 
            // pictureBoxTime
            // 
            this.pictureBoxTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBoxTime.Image = global::Minesweeper.Properties.Resources.clock000;
            this.pictureBoxTime.Location = new System.Drawing.Point(146, 3);
            this.pictureBoxTime.Name = "pictureBoxTime";
            this.pictureBoxTime.Size = new System.Drawing.Size(74, 50);
            this.pictureBoxTime.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxTime.TabIndex = 2;
            this.pictureBoxTime.TabStop = false;
            // 
            // panelGame
            // 
            this.panelGame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelGame.Location = new System.Drawing.Point(6, 100);
            this.panelGame.Name = "panelGame";
            this.panelGame.Size = new System.Drawing.Size(227, 211);
            this.panelGame.TabIndex = 9;
            // 
            // timerGame
            // 
            this.timerGame.Interval = 1000;
            // 
            // menuStripGame
            // 
            this.menuStripGame.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStripGame.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMain,
            this.toolStripMenuItemInfo});
            this.menuStripGame.Location = new System.Drawing.Point(0, 0);
            this.menuStripGame.Name = "menuStripGame";
            this.menuStripGame.Size = new System.Drawing.Size(238, 28);
            this.menuStripGame.TabIndex = 10;
            this.menuStripGame.Text = "menuStrip1";
            // 
            // toolStripMenuItemMain
            // 
            this.toolStripMenuItemMain.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNew,
            this.toolStripSeparator1,
            this.toolStripMenuItemExit});
            this.toolStripMenuItemMain.Name = "toolStripMenuItemMain";
            this.toolStripMenuItemMain.Size = new System.Drawing.Size(55, 24);
            this.toolStripMenuItemMain.Text = "Игра";
            // 
            // toolStripMenuItemNew
            // 
            this.toolStripMenuItemNew.Name = "toolStripMenuItemNew";
            this.toolStripMenuItemNew.Size = new System.Drawing.Size(216, 26);
            this.toolStripMenuItemNew.Text = "Новая";
            this.toolStripMenuItemNew.Click += new System.EventHandler(this.toolStripMenuItemNew_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(213, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(216, 26);
            this.toolStripMenuItemExit.Text = "Выход";
            // 
            // toolStripMenuItemInfo
            // 
            this.toolStripMenuItemInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemHighScore,
            this.toolStripMenuItemAbout});
            this.toolStripMenuItemInfo.Name = "toolStripMenuItemInfo";
            this.toolStripMenuItemInfo.Size = new System.Drawing.Size(114, 24);
            this.toolStripMenuItemInfo.Text = "Информация";
            // 
            // toolStripMenuItemHighScore
            // 
            this.toolStripMenuItemHighScore.Name = "toolStripMenuItemHighScore";
            this.toolStripMenuItemHighScore.Size = new System.Drawing.Size(216, 26);
            this.toolStripMenuItemHighScore.Text = "Таблица рекордов";
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(216, 26);
            this.toolStripMenuItemAbout.Text = "Об игре";
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(238, 327);
            this.Controls.Add(this.panelGame);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.menuStripGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripGame;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(256, 374);
            this.Name = "mainForm";
            this.Text = " Сапёр";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelInfo.ResumeLayout(false);
            this.tableLayoutPanelInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMinesCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSmileButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTime)).EndInit();
            this.menuStripGame.ResumeLayout(false);
            this.menuStripGame.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panelInfo;
        private System.Windows.Forms.Panel panelGame;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInfo;
        private System.Windows.Forms.PictureBox pictureBoxMinesCount;
        private System.Windows.Forms.PictureBox pictureBoxSmileButton;
        private System.Windows.Forms.PictureBox pictureBoxTime;
        private System.Windows.Forms.Timer timerGame;
        private System.Windows.Forms.MenuStrip menuStripGame;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInfo;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHighScore;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
    }
}

