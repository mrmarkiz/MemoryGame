using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class PlayingForm : Form
    {
        private MenuForm menu;
        private int difficulty;
        private PictureBox firstClicked, firstClickedLayout, secondClicked, secondClickedLayout;
        private List<string> imagesNames;
        private string topic;
        private int row;
        private int col;
        private int tries;
        private long time;

        public PlayingForm(MenuForm menu, int difficulty, string topic)
        {
            InitializeComponent();

            initFormFields(menu, difficulty, topic);

            initImagesNames();

            initFormSize(row, col);
            initField(row, col);
            timer1.Start();
        }

        private void initFormFields(MenuForm menu, int difficulty, string topic)
        {
            this.menu = menu;
            this.difficulty = difficulty;
            this.topic = topic;
            firstClicked = null;
            secondClicked = null;
            firstClickedLayout = null;
            secondClickedLayout = null;
            tries = 0;
            time = 0;

            //count size depending on difficulty
            int row = 4, col = 4;
            switch (difficulty)
            {
                case 2:
                    col = 6;
                    break;
                case 3:
                    row = 6;
                    col = 6;
                    break;
            }
            this.row = row;
            this.col = col;
        }

        //save images locations
        private void initImagesNames()
        {
            imagesNames = new List<string>();
            DirectoryInfo imagesDir = new DirectoryInfo($"Images\\{topic}");
            foreach (FileInfo image in imagesDir.GetFiles())
            {
                if (image.Extension == ".png")
                {
                    imagesNames.Add($"Images\\{topic}\\{image.Name}");
                }
            }
        }

        //resize form deppending on columns and rows number
        private void initFormSize(int row, int col)
        {
            this.Width = col * 120 + 19;
            this.Height = row * 120 + 72;
        }

        //add to the form images pictureBoxes and layout pictureBoxes
        private void initField(int rowCount, int columnsCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnsCount; j++)
                {
                    PictureBox pictureBox = new PictureBox(), layout = new PictureBox();
                    initPictureBox(pictureBox, i, j, layout);
                    this.Controls.Add(pictureBox);
                    this.Controls.Add(layout);
                }
            }
            initImages();
        }

        //set images for image pictureBoxes
        private void initImages()
        {
            Random rnd = new Random();
            List<PictureBox> uninitialised = new List<PictureBox>();
            foreach (var control in this.Controls)
            {
                if (control is PictureBox && ((PictureBox)control).Tag.ToString().Contains("image"))
                    uninitialised.Add((PictureBox)control);
            }
            while (uninitialised.Count > 0)
            {
                int imgRnd = rnd.Next(0, imagesNames.Count), picBoxRnd = rnd.Next(0, uninitialised.Count);
                uninitialised[picBoxRnd].ImageLocation = imagesNames[imgRnd];
                uninitialised.RemoveAt(picBoxRnd);
                picBoxRnd = rnd.Next(0, uninitialised.Count);
                uninitialised[picBoxRnd].ImageLocation = imagesNames[imgRnd];
                uninitialised.RemoveAt(picBoxRnd);
                imagesNames.RemoveAt(imgRnd);
            }
        }

        private void initPictureBox(PictureBox pictureBox, int i, int j, PictureBox layout)
        {
            //initiate image pictureBox
            pictureBox.Width = 120;
            pictureBox.Height = 120;
            pictureBox.Top = i * 120 + 25;
            pictureBox.Left = j * 120;
            pictureBox.Tag = $"image/{i}/{j}";
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Visible = false;

            //initiate layout pictureBox
            layout.Width = 120;
            layout.Height = 120;
            layout.Top = i * 120 + 25;
            layout.Left = j * 120;
            layout.Tag = $"layout/{i}/{j}";
            layout.Click += PictureBox_Click;
            layout.BorderStyle = BorderStyle.FixedSingle;
        }

        //when layout is clicked
        private void PictureBox_Click(object? sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox == null)
                return;

            if (firstClicked != null && secondClicked != null)
                return;

            if (firstClicked == null)
            {
                firstClickedLayout = pictureBox;
                firstClicked = getRelativeLayout(pictureBox.Tag.ToString());
                firstClicked.Visible = true;
                return;
            }

            if (firstClicked != null)
            {
                tries++;
                secondClickedLayout = pictureBox;
                secondClicked = getRelativeLayout(pictureBox.Tag.ToString());
                secondClicked.Visible = true;
                if (firstClicked.ImageLocation != secondClicked.ImageLocation)
                {
                    unmatchTimer.Start();
                }
                else
                {
                    firstClicked = null;
                    firstClickedLayout = null;
                    secondClicked = null;
                    secondClickedLayout = null;
                    checkWinner();
                }
                return;
            }
        }

        //check whether there still are any hidden image pictureBoxes
        private void checkWinner()
        {
            List<PictureBox> visible = new List<PictureBox>();
            foreach (var control in this.Controls)
            {
                if (control is PictureBox && ((PictureBox)control).Tag.ToString().Contains("image") && ((PictureBox)control).Visible == true)
                    visible.Add((PictureBox)control);
            }
            if (visible.Count() < row * col)
                return;
            else
            {
                string result = $"Congratulations!!!\nYou needed {tries} tries\nTime spent: ";

                if (time / 60 < 10)
                    result += 0;
                result += $"{time / 60}:";
                if (time % 60 < 10)
                    result += 0;
                result += time % 60;

                timer1.Stop();
                MessageBox.Show(result, "You won", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tryUpdateBest();
                this.Close();
            }
        }

        //update best time if record is beaten
        private void tryUpdateBest()
        {
            int minutes, seconds;
            using (FileStream fs = new FileStream("best.txt", FileMode.OpenOrCreate))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    string best = sr.ReadToEnd();
                    string[] time = best.Split(':');
                    if (time.Length < 2)
                        time = new string[2] { "0", "0" };
                    int.TryParse(time[0], out minutes);
                    int.TryParse(time[1], out seconds);
                }
            }
            if (minutes * 60 + seconds < this.time && (minutes != 0 && seconds != 0))
                return;

            using (FileStream fs = new FileStream("best.txt", FileMode.Truncate))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    string result = "";
                    if (this.time / 60 < 10)
                        result += 0;
                    result += $"{this.time / 60}:";
                    if (this.time % 60 < 10)
                        result += 0;
                    result += this.time % 60;
                    sw.WriteLine(result);
                }
            }
        }


        private void PlayingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            menu.Enabled = true;
            menu.updateBest();
            menu.Show();
        }

        //get the image pictureBox using layout pictureBox's Tag
        private PictureBox getRelativeLayout(string tag)
        {
            string[] tagSep = tag.Split('/');
            foreach (var control in this.Controls)
            {
                if (control is PictureBox)
                {
                    if (((PictureBox)control).Tag.ToString().Contains($"{tagSep[1]}/{tagSep[2]}") && ((PictureBox)control).Visible == false)
                        return (PictureBox)control;
                }
            }
            return null;
        }

        //delay before hiding 2 unmatched images
        private void unmatchTimer_Tick(object sender, EventArgs e)
        {
            unmatchTimer.Stop();

            firstClicked.Visible = false;
            secondClicked.Visible = false;

            firstClicked = null;
            firstClickedLayout = null;
            secondClicked = null;
            secondClickedLayout = null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            time++;
            string result = "Time:";
            if (this.time / 60 < 10)
                result += 0;
            result += $"{this.time / 60}:";
            if (this.time % 60 < 10)
                result += 0;
            result += this.time % 60;
            labelTime.Text = result;
        }
    }
}