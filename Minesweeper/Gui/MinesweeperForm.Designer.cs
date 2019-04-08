namespace Minesweeper.Gui
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beginerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.averageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.professionalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameInfoTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.mineCountPictureBox = new System.Windows.Forms.PictureBox();
            this.newGameButtonPictureBox = new System.Windows.Forms.PictureBox();
            this.timePictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.GameInfoTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mineCountPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.newGameButtonPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(414, 28);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beginerToolStripMenuItem,
            this.averageToolStripMenuItem,
            this.professionalToolStripMenuItem,
            this.userToolStripMenuItem});
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.newToolStripMenuItem.Text = "Новая игра";
            // 
            // beginerToolStripMenuItem
            // 
            this.beginerToolStripMenuItem.Name = "beginerToolStripMenuItem";
            this.beginerToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.beginerToolStripMenuItem.Text = "Новичок";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.GameInfoTableLayoutPanel);
            this.panel1.Location = new System.Drawing.Point(6, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 72);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Location = new System.Drawing.Point(6, 109);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(402, 211);
            this.panel2.TabIndex = 9;
            // 
            // averageToolStripMenuItem
            // 
            this.averageToolStripMenuItem.Name = "averageToolStripMenuItem";
            this.averageToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.averageToolStripMenuItem.Text = "Средний";
            // 
            // professionalToolStripMenuItem
            // 
            this.professionalToolStripMenuItem.Name = "professionalToolStripMenuItem";
            this.professionalToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.professionalToolStripMenuItem.Text = "Профессионал";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(216, 26);
            this.userToolStripMenuItem.Text = "Пользовательский";
            // 
            // GameInfoTableLayoutPanel
            // 
            this.GameInfoTableLayoutPanel.ColumnCount = 3;
            this.GameInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GameInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.GameInfoTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.GameInfoTableLayoutPanel.Controls.Add(this.mineCountPictureBox, 0, 0);
            this.GameInfoTableLayoutPanel.Controls.Add(this.newGameButtonPictureBox, 1, 0);
            this.GameInfoTableLayoutPanel.Controls.Add(this.timePictureBox, 2, 0);
            this.GameInfoTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameInfoTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.GameInfoTableLayoutPanel.Name = "GameInfoTableLayoutPanel";
            this.GameInfoTableLayoutPanel.RowCount = 1;
            this.GameInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.GameInfoTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.GameInfoTableLayoutPanel.Size = new System.Drawing.Size(398, 68);
            this.GameInfoTableLayoutPanel.TabIndex = 0;
            // 
            // mineCountPictureBox
            // 
            this.mineCountPictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            this.mineCountPictureBox.Location = new System.Drawing.Point(3, 3);
            this.mineCountPictureBox.Name = "mineCountPictureBox";
            this.mineCountPictureBox.Size = new System.Drawing.Size(100, 62);
            this.mineCountPictureBox.TabIndex = 0;
            this.mineCountPictureBox.TabStop = false;
            // 
            // newGameButtonPictureBox
            // 
            this.newGameButtonPictureBox.Location = new System.Drawing.Point(177, 3);
            this.newGameButtonPictureBox.Name = "newGameButtonPictureBox";
            this.newGameButtonPictureBox.Size = new System.Drawing.Size(44, 50);
            this.newGameButtonPictureBox.TabIndex = 1;
            this.newGameButtonPictureBox.TabStop = false;
            // 
            // timePictureBox
            // 
            this.timePictureBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.timePictureBox.Location = new System.Drawing.Point(295, 3);
            this.timePictureBox.Name = "timePictureBox";
            this.timePictureBox.Size = new System.Drawing.Size(100, 62);
            this.timePictureBox.TabIndex = 2;
            this.timePictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(414, 394);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.GameInfoTableLayoutPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mineCountPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.newGameButtonPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timePictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beginerToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripMenuItem averageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem professionalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel GameInfoTableLayoutPanel;
        private System.Windows.Forms.PictureBox mineCountPictureBox;
        private System.Windows.Forms.PictureBox newGameButtonPictureBox;
        private System.Windows.Forms.PictureBox timePictureBox;
    }
}

