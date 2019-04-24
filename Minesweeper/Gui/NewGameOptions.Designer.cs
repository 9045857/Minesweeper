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
            this.textBoxRowCount = new System.Windows.Forms.TextBox();
            this.textBoxColumnCount = new System.Windows.Forms.TextBox();
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
            this.pictureBoxCellColor = new System.Windows.Forms.PictureBox();
            this.colorDialogCellColor = new System.Windows.Forms.ColorDialog();
            this.groupBoxCellColor = new System.Windows.Forms.GroupBox();
            this.pictureBoxStandartCellColor = new System.Windows.Forms.PictureBox();
            this.radioButtonStandartColor = new System.Windows.Forms.RadioButton();
            this.radioButtonCustomColor = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellColor)).BeginInit();
            this.groupBoxCellColor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStandartCellColor)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonNewGame
            // 
            this.buttonNewGame.Location = new System.Drawing.Point(11, 196);
            this.buttonNewGame.Name = "buttonNewGame";
            this.buttonNewGame.Size = new System.Drawing.Size(122, 36);
            this.buttonNewGame.TabIndex = 1;
            this.buttonNewGame.Text = "Начать";
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
            // textBoxRowCount
            // 
            this.textBoxRowCount.Location = new System.Drawing.Point(139, 123);
            this.textBoxRowCount.Name = "textBoxRowCount";
            this.textBoxRowCount.Size = new System.Drawing.Size(55, 22);
            this.textBoxRowCount.TabIndex = 3;
            // 
            // textBoxColumnCount
            // 
            this.textBoxColumnCount.Location = new System.Drawing.Point(206, 123);
            this.textBoxColumnCount.Name = "textBoxColumnCount";
            this.textBoxColumnCount.Size = new System.Drawing.Size(55, 22);
            this.textBoxColumnCount.TabIndex = 4;
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
            this.labelMinesCountLow.Text = "10";
            // 
            // labelColumnCountLow
            // 
            this.labelColumnCountLow.AutoSize = true;
            this.labelColumnCountLow.Location = new System.Drawing.Point(225, 32);
            this.labelColumnCountLow.Name = "labelColumnCountLow";
            this.labelColumnCountLow.Size = new System.Drawing.Size(16, 17);
            this.labelColumnCountLow.TabIndex = 11;
            this.labelColumnCountLow.Text = "9";
            // 
            // labelRowCountLow
            // 
            this.labelRowCountLow.AutoSize = true;
            this.labelRowCountLow.Location = new System.Drawing.Point(158, 32);
            this.labelRowCountLow.Name = "labelRowCountLow";
            this.labelRowCountLow.Size = new System.Drawing.Size(16, 17);
            this.labelRowCountLow.TabIndex = 10;
            this.labelRowCountLow.Text = "9";
            // 
            // labelMinesCountMedium
            // 
            this.labelMinesCountMedium.AutoSize = true;
            this.labelMinesCountMedium.Location = new System.Drawing.Point(289, 60);
            this.labelMinesCountMedium.Name = "labelMinesCountMedium";
            this.labelMinesCountMedium.Size = new System.Drawing.Size(24, 17);
            this.labelMinesCountMedium.TabIndex = 15;
            this.labelMinesCountMedium.Text = "40";
            // 
            // labelColumnCountMedium
            // 
            this.labelColumnCountMedium.AutoSize = true;
            this.labelColumnCountMedium.Location = new System.Drawing.Point(221, 60);
            this.labelColumnCountMedium.Name = "labelColumnCountMedium";
            this.labelColumnCountMedium.Size = new System.Drawing.Size(24, 17);
            this.labelColumnCountMedium.TabIndex = 14;
            this.labelColumnCountMedium.Text = "16";
            // 
            // labelRowCountMedium
            // 
            this.labelRowCountMedium.AutoSize = true;
            this.labelRowCountMedium.Location = new System.Drawing.Point(154, 60);
            this.labelRowCountMedium.Name = "labelRowCountMedium";
            this.labelRowCountMedium.Size = new System.Drawing.Size(24, 17);
            this.labelRowCountMedium.TabIndex = 13;
            this.labelRowCountMedium.Text = "16";
            // 
            // labelMinesCountHigh
            // 
            this.labelMinesCountHigh.AutoSize = true;
            this.labelMinesCountHigh.Location = new System.Drawing.Point(289, 87);
            this.labelMinesCountHigh.Name = "labelMinesCountHigh";
            this.labelMinesCountHigh.Size = new System.Drawing.Size(24, 17);
            this.labelMinesCountHigh.TabIndex = 18;
            this.labelMinesCountHigh.Text = "99";
            // 
            // labelColumnCountHigh
            // 
            this.labelColumnCountHigh.AutoSize = true;
            this.labelColumnCountHigh.Location = new System.Drawing.Point(221, 87);
            this.labelColumnCountHigh.Name = "labelColumnCountHigh";
            this.labelColumnCountHigh.Size = new System.Drawing.Size(24, 17);
            this.labelColumnCountHigh.TabIndex = 17;
            this.labelColumnCountHigh.Text = "30";
            // 
            // labelRowCountHigh
            // 
            this.labelRowCountHigh.AutoSize = true;
            this.labelRowCountHigh.Location = new System.Drawing.Point(154, 87);
            this.labelRowCountHigh.Name = "labelRowCountHigh";
            this.labelRowCountHigh.Size = new System.Drawing.Size(24, 17);
            this.labelRowCountHigh.TabIndex = 16;
            this.labelRowCountHigh.Text = "16";
            // 
            // checkBoxMark
            // 
            this.checkBoxMark.AutoSize = true;
            this.checkBoxMark.Checked = true;
            this.checkBoxMark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxMark.Location = new System.Drawing.Point(11, 161);
            this.checkBoxMark.Name = "checkBoxMark";
            this.checkBoxMark.Size = new System.Drawing.Size(116, 21);
            this.checkBoxMark.TabIndex = 19;
            this.checkBoxMark.Text = "Включить (?)";
            this.checkBoxMark.UseVisualStyleBackColor = true;
            // 
            // pictureBoxCellColor
            // 
            this.pictureBoxCellColor.Location = new System.Drawing.Point(125, 53);
            this.pictureBoxCellColor.Name = "pictureBoxCellColor";
            this.pictureBoxCellColor.Size = new System.Drawing.Size(22, 22);
            this.pictureBoxCellColor.TabIndex = 22;
            this.pictureBoxCellColor.TabStop = false;
            this.pictureBoxCellColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCellColor_MouseDown);
            this.pictureBoxCellColor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxCellColor_MouseUp);
            // 
            // groupBoxCellColor
            // 
            this.groupBoxCellColor.Controls.Add(this.radioButtonCustomColor);
            this.groupBoxCellColor.Controls.Add(this.radioButtonStandartColor);
            this.groupBoxCellColor.Controls.Add(this.pictureBoxStandartCellColor);
            this.groupBoxCellColor.Controls.Add(this.pictureBoxCellColor);
            this.groupBoxCellColor.Location = new System.Drawing.Point(178, 151);
            this.groupBoxCellColor.Name = "groupBoxCellColor";
            this.groupBoxCellColor.Size = new System.Drawing.Size(167, 88);
            this.groupBoxCellColor.TabIndex = 23;
            this.groupBoxCellColor.TabStop = false;
            this.groupBoxCellColor.Text = "Цвет ячейки";
            // 
            // pictureBoxStandartCellColor
            // 
            this.pictureBoxStandartCellColor.Location = new System.Drawing.Point(125, 22);
            this.pictureBoxStandartCellColor.Name = "pictureBoxStandartCellColor";
            this.pictureBoxStandartCellColor.Size = new System.Drawing.Size(22, 22);
            this.pictureBoxStandartCellColor.TabIndex = 23;
            this.pictureBoxStandartCellColor.TabStop = false;
            this.pictureBoxStandartCellColor.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxStandartCellColor_MouseDown);
            this.pictureBoxStandartCellColor.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // radioButtonStandartColor
            // 
            this.radioButtonStandartColor.AutoSize = true;
            this.radioButtonStandartColor.Checked = true;
            this.radioButtonStandartColor.Location = new System.Drawing.Point(19, 24);
            this.radioButtonStandartColor.Name = "radioButtonStandartColor";
            this.radioButtonStandartColor.Size = new System.Drawing.Size(90, 21);
            this.radioButtonStandartColor.TabIndex = 24;
            this.radioButtonStandartColor.TabStop = true;
            this.radioButtonStandartColor.Text = "стандарт";
            this.radioButtonStandartColor.UseVisualStyleBackColor = true;
            // 
            // radioButtonCustomColor
            // 
            this.radioButtonCustomColor.AutoSize = true;
            this.radioButtonCustomColor.Location = new System.Drawing.Point(19, 52);
            this.radioButtonCustomColor.Name = "radioButtonCustomColor";
            this.radioButtonCustomColor.Size = new System.Drawing.Size(73, 21);
            this.radioButtonCustomColor.TabIndex = 25;
            this.radioButtonCustomColor.Text = "другой";
            this.radioButtonCustomColor.UseVisualStyleBackColor = true;
            // 
            // FormNewGameOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 244);
            this.Controls.Add(this.groupBoxCellColor);
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
            this.Controls.Add(this.textBoxColumnCount);
            this.Controls.Add(this.buttonNewGame);
            this.Controls.Add(this.textBoxRowCount);
            this.Controls.Add(this.radioButtonHigh);
            this.Controls.Add(this.radioButtonLow);
            this.Controls.Add(this.radioButtonMedium);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormNewGameOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Новая игра";
            this.Load += new System.EventHandler(this.FormNewGameOptions_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxCellColor)).EndInit();
            this.groupBoxCellColor.ResumeLayout(false);
            this.groupBoxCellColor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxStandartCellColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonNewGame;
        private System.Windows.Forms.RadioButton radioButtonLow;
        private System.Windows.Forms.RadioButton radioButtonMedium;
        private System.Windows.Forms.RadioButton radioButtonHigh;
        private System.Windows.Forms.TextBox textBoxRowCount;
        private System.Windows.Forms.TextBox textBoxColumnCount;
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
        private System.Windows.Forms.PictureBox pictureBoxCellColor;
        private System.Windows.Forms.ColorDialog colorDialogCellColor;
        private System.Windows.Forms.GroupBox groupBoxCellColor;
        private System.Windows.Forms.RadioButton radioButtonCustomColor;
        private System.Windows.Forms.RadioButton radioButtonStandartColor;
        private System.Windows.Forms.PictureBox pictureBoxStandartCellColor;
    }
}