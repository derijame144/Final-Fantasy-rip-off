namespace Final_Fantasy_rip_off
{
    partial class Form1
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
            this.Gamer = new System.Windows.Forms.Timer(this.components);
            this.XcordLabel = new System.Windows.Forms.Label();
            this.YcordLabel = new System.Windows.Forms.Label();
            this.bmStatsLabel = new System.Windows.Forms.Label();
            this.dkStatsLabel = new System.Windows.Forms.Label();
            this.drStatsLabel = new System.Windows.Forms.Label();
            this.animationTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Gamer
            // 
            this.Gamer.Enabled = true;
            this.Gamer.Interval = 20;
            this.Gamer.Tick += new System.EventHandler(this.Gamer_Tick);
            // 
            // XcordLabel
            // 
            this.XcordLabel.AutoSize = true;
            this.XcordLabel.Location = new System.Drawing.Point(45, 28);
            this.XcordLabel.Name = "XcordLabel";
            this.XcordLabel.Size = new System.Drawing.Size(70, 25);
            this.XcordLabel.TabIndex = 0;
            this.XcordLabel.Text = "label1";
            // 
            // YcordLabel
            // 
            this.YcordLabel.AutoSize = true;
            this.YcordLabel.Location = new System.Drawing.Point(45, 69);
            this.YcordLabel.Name = "YcordLabel";
            this.YcordLabel.Size = new System.Drawing.Size(70, 25);
            this.YcordLabel.TabIndex = 1;
            this.YcordLabel.Text = "label1";
            // 
            // bmStatsLabel
            // 
            this.bmStatsLabel.BackColor = System.Drawing.Color.Transparent;
            this.bmStatsLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bmStatsLabel.ForeColor = System.Drawing.Color.White;
            this.bmStatsLabel.Location = new System.Drawing.Point(124, 695);
            this.bmStatsLabel.Name = "bmStatsLabel";
            this.bmStatsLabel.Size = new System.Drawing.Size(229, 145);
            this.bmStatsLabel.TabIndex = 2;
            this.bmStatsLabel.Text = "   Black Mage\r\n\r\nHP:";
            this.bmStatsLabel.Visible = false;
            // 
            // dkStatsLabel
            // 
            this.dkStatsLabel.BackColor = System.Drawing.Color.Transparent;
            this.dkStatsLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dkStatsLabel.ForeColor = System.Drawing.Color.White;
            this.dkStatsLabel.Location = new System.Drawing.Point(404, 695);
            this.dkStatsLabel.Name = "dkStatsLabel";
            this.dkStatsLabel.Size = new System.Drawing.Size(229, 145);
            this.dkStatsLabel.TabIndex = 3;
            this.dkStatsLabel.Text = "   Black Mage\r\n\r\nHP:";
            this.dkStatsLabel.Visible = false;
            // 
            // drStatsLabel
            // 
            this.drStatsLabel.BackColor = System.Drawing.Color.Transparent;
            this.drStatsLabel.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.drStatsLabel.ForeColor = System.Drawing.Color.White;
            this.drStatsLabel.Location = new System.Drawing.Point(692, 695);
            this.drStatsLabel.Name = "drStatsLabel";
            this.drStatsLabel.Size = new System.Drawing.Size(229, 145);
            this.drStatsLabel.TabIndex = 4;
            this.drStatsLabel.Text = "   Black Mage\r\n\r\nHP:";
            this.drStatsLabel.Visible = false;
            // 
            // animationTimer
            // 
            this.animationTimer.Enabled = true;
            this.animationTimer.Interval = 20;
            this.animationTimer.Tick += new System.EventHandler(this.Animation_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1457, 1051);
            this.Controls.Add(this.drStatsLabel);
            this.Controls.Add(this.dkStatsLabel);
            this.Controls.Add(this.bmStatsLabel);
            this.Controls.Add(this.YcordLabel);
            this.Controls.Add(this.XcordLabel);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Gamer;
        private System.Windows.Forms.Label XcordLabel;
        private System.Windows.Forms.Label YcordLabel;
        private System.Windows.Forms.Label bmStatsLabel;
        private System.Windows.Forms.Label dkStatsLabel;
        private System.Windows.Forms.Label drStatsLabel;
        private System.Windows.Forms.Timer animationTimer;
    }
}

