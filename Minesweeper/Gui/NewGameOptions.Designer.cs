namespace Minesweeper.Gui
{
    partial class FormNewGameOptions
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
            this.buttonNewGame = new System.Windows.Forms.Button();
            this.radioButtonLow = new System.Windows.Forms.RadioButton();
            this.radioButtonMedium = new System.Windows.Forms.RadioButton();
            this.radioButtonHigh = new System.Windows.Forms.RadioButton();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.textBoxMinesCount = new System.Windows.Forms.TextBox();
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.labelRowCountCaption = new System.Windows.Forms.Label();
            this.labelColumnCountCaption = new System.Windows.Forms.Label();
            this.labelMinesCountCaption = new System.Windows.Forms.Label();
            this.labelMinesCountLow = new System.Windows.Forms.Label();
            this.labelColumnCountLow = new System.Windows.Forms.Label();
            this.labelRowCountLow = new System.Windows.Forms.Label();
            this.labelMinesCountMedium = new System.Windows.Forms.Label();
            this.labelColumnCountMedium = new System.Windows.Forms.Label();
            this.labelRowCountMedium = new System.Windows.Forms.Label();
            this.labelMinesCountHigh = new System.Windows.Forms.Label();
            this.labelColumnCountHigh = new System.Windows.Forms.Label();
            this.labelRowCountHigh = new System.Windows.Forms.Label();
            this.checkBoxMark = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Location = new System.Drawing.Point(134, 209);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(75, 23);
            this.buttonNewGame.TabIndex = 1;
            this.buttonNewGame.Text = "OK";
            this.buttonNewGame.UseVisualStyleBackColor = true;
            this.buttonNewGame.Click += new System.EventHandler(this.buttonNewGame_Click);
            // 
            // radioButtonLow
            // 
            this.radioButtonLow.AutoSize = true;
            this.radioButtonLow.Checked = true;
            this.radioButtonLow.Location = new System.Drawing.Point(11, 30);
            this.radioButtonLow.Name = "radioButtonLow";
            this.radioButtonLow.Size = new System.Drawing.Size(85, 21);
            this.radioButtonLow.TabIndex = 0;
            this.radioButtonLow.TabStop = true;
            this.radioButtonLow.Text = "Новичок";
            this.radioButtonLow.UseVisualStyleBackColor = true;
            // 
            // radioButtonMedium
            // 
            this.radioButtonMedium.AutoSize = true;
            this.radioButtonMedium.Location = new System.Drawing.Point(11, 58);
            this.radioButtonMedium.Name = "radioButtonMedium";
            this.radioButtonMedium.Size = new System.Drawing.Size(95, 21);
            this.radioButtonMedium.TabIndex = 1;
            this.radioButtonMedium.Text = "Любитель";
            this.radioButtonMedium.UseVisualStyleBackColor = true;
            // 
            // radioButtonHigh
            // 
            this.radioButtonHigh.AutoSize = true;
            this.radioButtonHigh.Location = new System.Drawing.Point(11, 85);
            this.radioButtonHigh.Name = "radioButtonHigh";
            this.radioButtonHigh.Size = new System.Drawing.Size(128, 21);
            this.radioButtonHigh.TabIndex = 2;
            this.radioButtonHigh.Text = "Профессионал";
            this.radioButtonHigh.UseVisualStyleBackColor = true;
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(139, 123);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.Size = new System.Drawing.Size(55, 22);
            this.textBoxHeight.TabIndex = 3;
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(206, 123);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(55, 22);
            this.textBoxWidth.TabIndex = 4;
            // 
            // textBoxMinesCount
            // 
            this.textBoxMinesCount.Location = new System.Drawing.Point(274, 123);
            this.textBoxMinesCount.Name = "textBoxMinesCount";
            this.textBoxMinesCount.Size = new System.Drawing.Size(55, 22);
            this.textBoxMinesCount.TabIndex = 5;
            // 
            // radioButtonCustom
            // 
            this.radioButtonCustom.AutoSize = true;
            this.radioButtonCustom.Location = new System.Drawing.Point(11, 124);
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.radioButtonCustom.Size = new System.Drawing.Size(81, 21);
            this.radioButtonCustom.TabIndex = 6;
            this.radioButtonCustom.Text = "Особый";
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            // 
            // labelRowCountCaption
            // 
            this.labelRowCountCaption.AutoSize = true;
            this.labelRowCountCaption.Location = new System.Drawing.Point(138, 8);
            this.labelRowCountCaption.Name = "labelRowCountCaption";
            this.labelRowCountCaption.Size = new System.Drawing.Size(57, 17);
            this.labelRowCountCaption.TabIndex = 7;
            this.labelRowCountCaption.Text = "Высота";
            // 
            // labelColumnCountCaption
            // 
            this.labelColumnCountCaption.AutoSize = true;
            this.labelColumnCountCaption.Location = new System.Drawing.Point(204, 8);
            this.labelColumnCountCaption.Name = "labelColumnCountCaption";
            this.labelColumnCountCaption.Size = new System.Drawing.Size(59, 17);
            this.labelColumnCountCaption.TabIndex = 8;
            this.labelColumnCountCaption.Text = "Ширина";
            // 
            // labelMinesCountCaption
            // 
            this.labelMinesCountCaption.AutoSize = true;
            this.labelMinesCountCaption.Location = new System.Drawing.Point(279, 8);
            this.labelMinesCountCaption.Name = "labelMinesCountCaption";
            this.labelMinesCountCaption.Size = new System.Drawing.Size(45, 17);
            this.labelMinesCountCaption.TabIndex = 9;
            this.labelMinesCountCaption.Text = "Мины";
            // 
            // labelMinesCountLow
            // 
            this.labelMinesCountLow.AutoSize = true;
            this.labelMinesCountLow.Location = new System.Drawing.Point(289, 32);
            this.labelMinesCountLow.Name = "labelMinesCountLow";
            this.labelMinesCountLow.Size = new System.Drawing.Size(24, 17);
            this.labelMinesCountLow.TabIndex = 12;
            this.labelMinesCountLow.Text = GameOptionsConstants.LowLevelMinesCount.ToString();// "10";
            // 
            // labelColumnCountLow
            // 
            this.labelColumnCountLow.AutoSize = true;
            this.labelColumnCountLow.Location = new System.Drawing.Point(225, 32);
            this.labelColumnCountLow.Name = "labelColumnCountLow";
            this.labelColumnCountLow.Size = new System.Drawing.Size(16, 17);
            this.labelColumnCountLow.TabIndex = 11;
            this.labelColumnCountLow.Text = GameOptionsConstants.LowLevelColumnCount.ToString();
            // 
            // labelRowCountLow
            // 
            this.labelRowCountLow.AutoSize = true;
            this.labelRowCountLow.Location = new System.Drawing.Point(158, 32);
            this.labelRowCountLow.Name = "labelRowCountLow";
            this.labelRowCountLow.Size = new System.Drawing.Size(16, 17);
            this.labelRowCountLow.TabIndex = 10;
            this.labelRowCountLow.Text = GameOptionsConstants.LowLevelRowCount.ToString();
            // 
            // labelMinesCountMedium
            // 
            this.labelMinesCountMedium.AutoSize = true;
            this.labelMinesCountMedium.Location = new System.Drawing.Point(289, 60);
            this.labelMinesCountMedium.Name = "labelMinesCountMedium";
            this.labelMinesCountMedium.Size = new System.Drawing.Size(24, 17);
            this.labelMinesCountMedium.TabIndex = 15;
            this.labelMinesCountMedium.Text = GameOptionsConstants.MediumLevelMinesCount.ToString();
            // 
            // labelColumnCountMedium
            // 
            this.labelColumnCountMedium.AutoSize = true;
            this.labelColumnCountMedium.Location = new System.Drawing.Point(221, 60);
            this.labelColumnCountMedium.Name = "labelColumnCountMedium";
            this.labelColumnCountMedium.Size = new System.Drawing.Size(24, 17);
            this.labelColumnCountMedium.TabIndex = 14;
            this.labelColumnCountMedium.Text = GameOptionsConstants.MediumLevelColumnCount.ToString();
            // 
            // labelRowCountMedium
            // 
            this.labelRowCountMedium.AutoSize = true;
            this.labelRowCountMedium.Location = new System.Drawing.Point(154, 60);
            this.labelRowCountMedium.Name = "labelRowCountMedium";
            this.labelRowCountMedium.Size = new System.Drawing.Size(24, 17);
            this.labelRowCountMedium.TabIndex = 13;
            this.labelRowCountMedium.Text = GameOptionsConstants.MediumLevelRowCount.ToString();
            // 
            // labelMinesCountHigh
            // 
            this.labelMinesCountHigh.AutoSize = true;
            this.labelMinesCountHigh.Location = new System.Drawing.Point(289, 87);
            this.labelMinesCountHigh.Name = "labelMinesCountHigh";
            this.labelMinesCountHigh.Size = new System.Drawing.Size(24, 17);
            this.labelMinesCountHigh.TabIndex = 18;
            this.labelMinesCountHigh.Text = GameOptionsConstants.HighLevelMinesCount.ToString();
            // 
            // labelColumnCountHigh
            // 
            this.labelColumnCountHigh.AutoSize = true;
            this.labelColumnCountHigh.Location = new System.Drawing.Point(221, 87);
            this.labelColumnCountHigh.Name = "labelColumnCountHigh";
            this.labelColumnCountHigh.Size = new System.Drawing.Size(24, 17);
            this.labelColumnCountHigh.TabIndex = 17;
            this.labelColumnCountHigh.Text = GameOptionsConstants.HighLevelColumnCount.ToString();
            // 
            // labelRowCountHigh
            // 
            this.labelRowCountHigh.AutoSize = true;
            this.labelRowCountHigh.Location = new System.Drawing.Point(154, 87);
            this.labelRowCountHigh.Name = "labelRowCountHigh";
            this.labelRowCountHigh.Size = new System.Drawing.Size(24, 17);
            this.labelRowCountHigh.TabIndex = 16;
            this.labelRowCountHigh.Text = GameOptionsConstants.HighLevelRowCount.ToString();
            // 
            // checkBoxMark
            // 
            this.checkBoxMark.AutoSize = true;
            this.checkBoxMark.Checked = true;
            this.checkBoxMark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMark.Location = new System.Drawing.Point(11, 166);
            this.checkBoxMark.Name = "checkBoxMark";
            this.checkBoxMark.Size = new System.Drawing.Size(116, 21);
            this.checkBoxMark.TabIndex = 19;
            this.checkBoxMark.Text = "Включить (?)";
            this.checkBoxMark.UseVisualStyleBackColor = true;
            // 
            // FormNewGameOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 244);
            this.Controls.Add(this.checkBoxMark);
            this.Controls.Add(this.labelMinesCountHigh);
            this.Controls.Add(this.labelColumnCountHigh);
            this.Controls.Add(this.labelRowCountHigh);
            this.Controls.Add(this.labelMinesCountMedium);
            this.Controls.Add(this.labelColumnCountMedium);
            this.Controls.Add(this.labelRowCountMedium);
            this.Controls.Add(this.labelMinesCountLow);
            this.Controls.Add(this.labelColumnCountLow);
            this.Controls.Add(this.labelRowCountLow);
            this.Controls.Add(this.labelMinesCountCaption);
            this.Controls.Add(this.labelColumnCountCaption);
            this.Controls.Add(this.labelRowCountCaption);
            this.Controls.Add(this.radioButtonCustom);
            this.Controls.Add(this.textBoxMinesCount);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.buttonNewGame);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.radioButtonHigh);
            this.Controls.Add(this.radioButtonLow);
            this.Controls.Add(this.radioButtonMedium);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormNewGameOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новая игра";
            this.Load += new System.EventHandler(this.FormNewGameOptions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.RadioButton radioButtonLow;
        private System.Windows.Forms.RadioButton radioButtonMedium;
        private System.Windows.Forms.RadioButton radioButtonHigh;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.TextBox textBoxMinesCount;
        private System.Windows.Forms.RadioButton radioButtonCustom;
        private System.Windows.Forms.Label labelRowCountCaption;
        private System.Windows.Forms.Label labelColumnCountCaption;
        private System.Windows.Forms.Label labelMinesCountCaption;
        private System.Windows.Forms.Label labelMinesCountLow;
        private System.Windows.Forms.Label labelColumnCountLow;
        private System.Windows.Forms.Label labelRowCountLow;
        private System.Windows.Forms.Label labelMinesCountMedium;
        private System.Windows.Forms.Label labelColumnCountMedium;
        private System.Windows.Forms.Label labelRowCountMedium;
        private System.Windows.Forms.Label labelMinesCountHigh;
        private System.Windows.Forms.Label labelColumnCountHigh;
        private System.Windows.Forms.Label labelRowCountHigh;
        private System.Windows.Forms.CheckBox checkBoxMark;
    }
}