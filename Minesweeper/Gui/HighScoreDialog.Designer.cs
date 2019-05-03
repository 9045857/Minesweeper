namespace Minesweeper.Gui
{
    partial class HighScoreDialog
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
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.labelCongratulation = new System.Windows.Forms.Label();
            this.labelTimeCaption = new System.Windows.Forms.Label();
            this.labelTimeResult = new System.Windows.Forms.Label();
            this.labelNameCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(112, 113);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(125, 22);
            this.textBoxUserName.TabIndex = 0;
            this.textBoxUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxUserName_KeyDown);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(28, 165);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 27);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(162, 165);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 27);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // labelCongratulation
            // 
            this.labelCongratulation.AutoSize = true;
            this.labelCongratulation.Location = new System.Drawing.Point(24, 27);
            this.labelCongratulation.Name = "labelCongratulation";
            this.labelCongratulation.Size = new System.Drawing.Size(46, 17);
            this.labelCongratulation.TabIndex = 3;
            this.labelCongratulation.Text = "label1";
            // 
            // labelTimeCaption
            // 
            this.labelTimeCaption.AutoSize = true;
            this.labelTimeCaption.Location = new System.Drawing.Point(28, 84);
            this.labelTimeCaption.Name = "labelTimeCaption";
            this.labelTimeCaption.Size = new System.Drawing.Size(54, 17);
            this.labelTimeCaption.TabIndex = 4;
            this.labelTimeCaption.Text = "Время:";
            // 
            // labelTimeResult
            // 
            this.labelTimeResult.AutoSize = true;
            this.labelTimeResult.Location = new System.Drawing.Point(109, 84);
            this.labelTimeResult.Name = "labelTimeResult";
            this.labelTimeResult.Size = new System.Drawing.Size(46, 17);
            this.labelTimeResult.TabIndex = 5;
            this.labelTimeResult.Text = "label3";
            // 
            // labelNameCaption
            // 
            this.labelNameCaption.AutoSize = true;
            this.labelNameCaption.Location = new System.Drawing.Point(28, 116);
            this.labelNameCaption.Name = "labelNameCaption";
            this.labelNameCaption.Size = new System.Drawing.Size(39, 17);
            this.labelNameCaption.TabIndex = 6;
            this.labelNameCaption.Text = "Имя:";
            // 
            // HighScoreDialog
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(277, 225);
            this.Controls.Add(this.labelNameCaption);
            this.Controls.Add(this.labelTimeResult);
            this.Controls.Add(this.labelTimeCaption);
            this.Controls.Add(this.labelCongratulation);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxUserName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HighScoreDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Новый рекорд";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.HighScoreDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label labelCongratulation;
        private System.Windows.Forms.Label labelTimeCaption;
        private System.Windows.Forms.Label labelTimeResult;
        private System.Windows.Forms.Label labelNameCaption;
    }
}