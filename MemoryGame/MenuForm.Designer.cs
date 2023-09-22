namespace MemoryGame
{
    partial class MenuForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            comboBoxDifficulty = new ComboBox();
            label2 = new Label();
            comboBoxTopic = new ComboBox();
            buttonStart = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(98, 9);
            label1.Name = "label1";
            label1.Size = new Size(123, 20);
            label1.TabIndex = 0;
            label1.Text = "Choose difficulty:";
            // 
            // comboBoxDifficulty
            // 
            comboBoxDifficulty.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxDifficulty.FormattingEnabled = true;
            comboBoxDifficulty.Items.AddRange(new object[] { "1", "2", "3" });
            comboBoxDifficulty.Location = new Point(76, 41);
            comboBoxDifficulty.Name = "comboBoxDifficulty";
            comboBoxDifficulty.Size = new Size(163, 28);
            comboBoxDifficulty.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(110, 105);
            label2.Name = "label2";
            label2.Size = new Size(99, 20);
            label2.TabIndex = 2;
            label2.Text = "Choose topic:";
            // 
            // comboBoxTopic
            // 
            comboBoxTopic.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTopic.FormattingEnabled = true;
            comboBoxTopic.Location = new Point(76, 137);
            comboBoxTopic.Name = "comboBoxTopic";
            comboBoxTopic.Size = new Size(163, 28);
            comboBoxTopic.TabIndex = 3;
            // 
            // buttonStart
            // 
            buttonStart.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point);
            buttonStart.Location = new Point(76, 198);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(163, 48);
            buttonStart.TabIndex = 4;
            buttonStart.Text = "Start Game";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // MenuForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(316, 270);
            Controls.Add(buttonStart);
            Controls.Add(comboBoxTopic);
            Controls.Add(label2);
            Controls.Add(comboBoxDifficulty);
            Controls.Add(label1);
            Name = "MenuForm";
            Text = "Menu";
            Load += MenuForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox comboBoxDifficulty;
        private Label label2;
        private ComboBox comboBoxTopic;
        private Button buttonStart;
    }
}