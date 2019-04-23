namespace WindowsFormsApp1
{
    partial class GameForm
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
            this._damageHeroButton = new System.Windows.Forms.Button();
            this._heroHealthTextBox = new System.Windows.Forms.TextBox();
            this._heroHealthLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _damageHeroButton
            // 
            this._damageHeroButton.Location = new System.Drawing.Point(227, 94);
            this._damageHeroButton.Name = "_damageHeroButton";
            this._damageHeroButton.Size = new System.Drawing.Size(75, 23);
            this._damageHeroButton.TabIndex = 0;
            this._damageHeroButton.Text = "button1";
            this._damageHeroButton.UseVisualStyleBackColor = true;
            this._damageHeroButton.Click += new System.EventHandler(this._damageHeroButton_Click_1);
            // 
            // _heroHealthTextBox
            // 
            this._heroHealthTextBox.Location = new System.Drawing.Point(388, 46);
            this._heroHealthTextBox.Name = "_heroHealthTextBox";
            this._heroHealthTextBox.Size = new System.Drawing.Size(100, 22);
            this._heroHealthTextBox.TabIndex = 1;
            this._heroHealthTextBox.Text = "90";
            // 
            // _heroHealthLabel
            // 
            this._heroHealthLabel.AutoSize = true;
            this._heroHealthLabel.Location = new System.Drawing.Point(224, 30);
            this._heroHealthLabel.Name = "_heroHealthLabel";
            this._heroHealthLabel.Size = new System.Drawing.Size(32, 17);
            this._heroHealthLabel.TabIndex = 2;
            this._heroHealthLabel.Text = "100";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(679, 185);
            this.Controls.Add(this._heroHealthLabel);
            this.Controls.Add(this._heroHealthTextBox);
            this.Controls.Add(this._damageHeroButton);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GameForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _damageHeroButton;
        private System.Windows.Forms.TextBox _heroHealthTextBox;
        private System.Windows.Forms.Label _heroHealthLabel;
    }
}

