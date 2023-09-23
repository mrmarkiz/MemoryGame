namespace MemoryGame
{
    partial class PlayingForm
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
            components = new System.ComponentModel.Container();
            unmatchTimer = new System.Windows.Forms.Timer(components);
            timer1 = new System.Windows.Forms.Timer(components);
            labelTime = new Label();
            SuspendLayout();
            // 
            // unmatchTimer
            // 
            unmatchTimer.Interval = 750;
            unmatchTimer.Tick += unmatchTimer_Tick;
            // 
            // timer1
            // 
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // labelTime
            // 
            labelTime.AutoSize = true;
            labelTime.Location = new Point(2, 2);
            labelTime.Name = "labelTime";
            labelTime.Size = new Size(80, 20);
            labelTime.TabIndex = 0;
            labelTime.Text = "Time:00:00";
            // 
            // PlayingForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(702, 673);
            Controls.Add(labelTime);
            Name = "PlayingForm";
            Text = "Memory Game";
            FormClosed += PlayingForm_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer unmatchTimer;
        private System.Windows.Forms.Timer timer1;
        private Label labelTime;
    }
}