namespace Ex05_UIOthelo
{
    public partial class FormGameSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGameSetting));
            this.boardSizeButton = new System.Windows.Forms.Button();
            this.playAgainstComputerButton = new System.Windows.Forms.Button();
            this.playAgainstUserButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // boardSizeButton
            // 
            this.boardSizeButton.Location = new System.Drawing.Point(67, 30);
            this.boardSizeButton.Name = "boardSizeButton";
            this.boardSizeButton.Size = new System.Drawing.Size(358, 51);
            this.boardSizeButton.TabIndex = 0;
            this.boardSizeButton.Text = "Board size: 6x6 (click to increase)";
            this.boardSizeButton.UseVisualStyleBackColor = true;
            this.boardSizeButton.Click += new System.EventHandler(this.boardSizeButton_Click);
            // 
            // playAgainstComputerButton
            // 
            this.playAgainstComputerButton.Location = new System.Drawing.Point(67, 136);
            this.playAgainstComputerButton.Name = "playAgainstComputerButton";
            this.playAgainstComputerButton.Size = new System.Drawing.Size(171, 55);
            this.playAgainstComputerButton.TabIndex = 1;
            this.playAgainstComputerButton.Text = "Play against the computer";
            this.playAgainstComputerButton.UseVisualStyleBackColor = true;
            this.playAgainstComputerButton.Click += new System.EventHandler(this.playAgainstComputerButton_Click);
            // 
            // playAgainstUserButton
            // 
            this.playAgainstUserButton.Location = new System.Drawing.Point(254, 136);
            this.playAgainstUserButton.Name = "playAgainstUserButton";
            this.playAgainstUserButton.Size = new System.Drawing.Size(171, 55);
            this.playAgainstUserButton.TabIndex = 2;
            this.playAgainstUserButton.Text = "Play against your friend";
            this.playAgainstUserButton.UseVisualStyleBackColor = true;
            this.playAgainstUserButton.Click += new System.EventHandler(this.playAgainstUserButton_Click);
            // 
            // FormGameSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 232);
            this.Controls.Add(this.playAgainstUserButton);
            this.Controls.Add(this.playAgainstComputerButton);
            this.Controls.Add(this.boardSizeButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button boardSizeButton;
        private System.Windows.Forms.Button playAgainstComputerButton;
        private System.Windows.Forms.Button playAgainstUserButton;
    }
}