namespace MemoryGame
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        //start game
        private void buttonStart_Click(object sender, EventArgs e)
        {
            PlayingForm playingForm = new PlayingForm(this, int.Parse(comboBoxDifficulty.SelectedItem.ToString()), comboBoxTopic.SelectedItem.ToString());
            playingForm.Show();
            this.Enabled = false;
            this.Hide();
        }

        //initiate topics list and best time
        private void MenuForm_Load(object sender, EventArgs e)
        {
            DirectoryInfo imagesChecker = new DirectoryInfo("Images");
            DirectoryInfo[] list = imagesChecker.GetDirectories();
            foreach (DirectoryInfo directory in list)
            {
                if (directory.GetFiles().Where(file => file.Extension == ".png").Count() >= 18)
                    comboBoxTopic.Items.Add(directory.Name);
            }
            comboBoxDifficulty.SelectedIndex = 0;
            comboBoxTopic.SelectedIndex = 0;
            updateBest();
        }

        //update best time
        public void updateBest()
        {
            using (FileStream fs = new FileStream("best.txt", FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string best = sr.ReadToEnd();
                    string[] time = best.Split(':');
                    if (time.Length < 2)
                    {
                        labelRecord.Text = "Best time: None";
                        return;
                    }
                    labelRecord.Text = $"Best time: {time[0]}:{time[1]}";
                }
            }
        }

        //exit the program
        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("Are you sure?", "Check", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}