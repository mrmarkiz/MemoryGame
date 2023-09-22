namespace MemoryGame
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            PlayingForm playingForm = new PlayingForm(this, int.Parse(comboBoxDifficulty.SelectedItem.ToString()), comboBoxTopic.SelectedItem.ToString());
            playingForm.Show();
            this.Enabled = false;
            this.Hide();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            DirectoryInfo imagesChecker = new DirectoryInfo("Images");
            DirectoryInfo[] list = imagesChecker.GetDirectories();
            foreach(DirectoryInfo directory in list)
            {
                if (directory.GetFiles().Where(file => file.Extension == ".png").Count() >= 18)
                    comboBoxTopic.Items.Add(directory.Name);
            }
            comboBoxDifficulty.SelectedIndex = 0;
            comboBoxTopic.SelectedIndex = 0;
        }
    }
}