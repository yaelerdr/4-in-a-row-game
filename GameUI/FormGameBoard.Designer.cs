
namespace GameUI
{
    partial class FormGameBoard
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
            this.player2Label = new System.Windows.Forms.Label();
            this.player1Label = new System.Windows.Forms.Label();
            this.player1Score = new System.Windows.Forms.Label();
            this.player2Score = new System.Windows.Forms.Label();
            this.quit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // player2Label
            // 
            this.player2Label.AutoSize = true;
            this.player2Label.Location = new System.Drawing.Point(31, 56);
            this.player2Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.player2Label.Name = "player2Label";
            this.player2Label.Size = new System.Drawing.Size(69, 20);
            this.player2Label.TabIndex = 0;
            this.player2Label.Text = "Player 2:";
            this.player2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // player1Label
            // 
            this.player1Label.AutoSize = true;
            this.player1Label.Location = new System.Drawing.Point(31, 24);
            this.player1Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.player1Label.Name = "player1Label";
            this.player1Label.Size = new System.Drawing.Size(69, 20);
            this.player1Label.TabIndex = 1;
            this.player1Label.Text = "Player 1:";
            this.player1Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // player1Score
            // 
            this.player1Score.AutoSize = true;
            this.player1Score.Location = new System.Drawing.Point(108, 24);
            this.player1Score.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.player1Score.Name = "player1Score";
            this.player1Score.Size = new System.Drawing.Size(18, 20);
            this.player1Score.TabIndex = 2;
            this.player1Score.Text = "0";
            this.player1Score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // player2Score
            // 
            this.player2Score.AccessibleName = "";
            this.player2Score.AutoSize = true;
            this.player2Score.Location = new System.Drawing.Point(108, 56);
            this.player2Score.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.player2Score.Name = "player2Score";
            this.player2Score.Size = new System.Drawing.Size(18, 20);
            this.player2Score.TabIndex = 3;
            this.player2Score.Text = "0";
            this.player2Score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // quit
            // 
            this.quit.Location = new System.Drawing.Point(306, 24);
            this.quit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.quit.Name = "quit";
            this.quit.Size = new System.Drawing.Size(126, 51);
            this.quit.TabIndex = 4;
            this.quit.Text = "quit";
            this.quit.UseVisualStyleBackColor = true;
            this.quit.Click += new System.EventHandler(this.quit_Click);
            // 
            // FormGameBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 548);
            this.Controls.Add(this.quit);
            this.Controls.Add(this.player2Score);
            this.Controls.Add(this.player1Score);
            this.Controls.Add(this.player1Label);
            this.Controls.Add(this.player2Label);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormGameBoard";
            this.Text = "4 In A Row !";
            this.Load += new System.EventHandler(this.FormGameBoard_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label player2Label;
        private System.Windows.Forms.Label player1Label;
        private System.Windows.Forms.Label player1Score;
        private System.Windows.Forms.Label player2Score;
        private System.Windows.Forms.Button quit;
    }
}