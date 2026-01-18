using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Comic_Story_Builder_2._0
{
    public partial class Form1 : Form
    {
        private bool moving = false;
        private bool resize = false;
        private Point loc = new Point();
        private List<Asset> assetsList = new List<Asset>();
        private List<SpeechBubble> bubbleList = new List<SpeechBubble>();
        private Rectangle rect;
        private bool saved = false;
        private String connectionString = "Data Source = \"C:\\Users\\grigo\\OneDrive\\Υπολογιστής\\ComicStory.db\"; version=3";
        private SQLiteConnection connection;

        public Form1()
        {
            InitializeComponent();
            newAsset(pictureBox1);
            newAsset(pictureBox2);
            newAsset(pictureBox3);
            newBubble(pictureBox4);
            newBubble(pictureBox5);
            newBubble(pictureBox6);
            rect = new Rectangle(30, 20, panel1.Width - 60, panel1.Height - 40);
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection(connectionString);
        }

        private void pb_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void pb_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void pb_MouseDown(object sender, MouseEventArgs e)
        {
            ((PictureBox)sender).BringToFront();
            moving = true;
            loc = e.Location;
            if (((PictureBox)sender).Cursor == Cursors.SizeNWSE && ((PictureBox)sender).Parent == panel1)
            {
                resize = true;
                moving = false;
            }
        }

        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            resize = false;
            if (((PictureBox)sender).Parent == this && ((PictureBox)sender).Bounds.IntersectsWith(panel1.Bounds))
            {
                ((PictureBox)sender).Parent = panel1;
                ((PictureBox)sender).Location = new Point(((PictureBox)sender).Location.X - panel1.Location.X,
                    ((PictureBox)sender).Location.Y - panel1.Location.Y);
            }
            else if (((PictureBox)sender).Parent == panel1 && !((PictureBox)sender).Bounds.IntersectsWith(rect) || ((PictureBox)sender).Parent == this && !((PictureBox)sender).Bounds.IntersectsWith(panel1.Bounds))
            {
                if (((PictureBox)sender).Tag is SpeechBubble)
                {
                    for (int i = 0; i < bubbleList.Count; i++)
                    {
                        if (bubbleList[i].Reset_Bubble((PictureBox)sender)) break;
                    }
                }
                else if (((PictureBox)sender).Tag is Asset)
                {
                    for (int i = 0; i < assetsList.Count; i++)
                    {
                        if (assetsList[i].Reset_Asset((PictureBox)sender)) break;
                    }
                }

            }
        }

        private void pb_MouseMove(object sender, MouseEventArgs e)
        {
            if (moving)
            {
                ((PictureBox)sender).Location = new Point(
                    ((PictureBox)sender).Location.X + e.X - loc.X,
                    ((PictureBox)sender).Location.Y + e.Y - loc.Y
                );
            }
            else if (((PictureBox)sender).Parent == panel1 && e.Location.X > ((PictureBox)sender).Width - 10
                && e.Location.Y > ((PictureBox)sender).Height - 10)
            {
                ((PictureBox)sender).Cursor = Cursors.SizeNWSE;
                if (resize)
                {
                    ((PictureBox)sender).Size = new System.Drawing.Size(e.X, e.Y);
                }
            }
            else
            {
                ((PictureBox)sender).Cursor = Cursors.Hand;
            }
        }

        private void pb_DoubleClick(object sender, EventArgs e)
        {
            if (((PictureBox)sender).Tag is SpeechBubble)
            {
                ((SpeechBubble)((PictureBox)sender).Tag).CreateRTB();
            }
        }

        private void newAsset(PictureBox pb)
        {
            Asset asset = new Asset(pb, pb_MouseUp, pb_MouseDown, pb_MouseEnter, pb_MouseLeave, pb_MouseMove);
            assetsList.Add(asset);
            pb.Tag = asset;
        }

        private void newBubble(PictureBox pb)
        {
            SpeechBubble speechbubble = new SpeechBubble(pb, pb_DoubleClick, pb_MouseUp, pb_MouseDown, pb_MouseEnter, pb_MouseLeave, pb_MouseMove);
            bubbleList.Add(speechbubble);
            pb.Tag = speechbubble;
        }

        private void resetPanel()
        {
            List<PictureBox> resetList = panel1.Controls.OfType<PictureBox>().ToList();
            foreach (PictureBox pb in resetList)
            {
                if (pb.Tag is SpeechBubble)
                {
                    ((SpeechBubble)pb.Tag).Reset_Bubble(pb);
                }
                else if (pb.Tag is Asset)
                {
                    ((Asset)pb.Tag).Reset_Asset(pb);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!saved)
            {
                MessageBox.Show("Save current page first!");
            }
            else
            {
                resetPanel();
                saved = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                savePage(panel1);
                saved = true;
            }
            else
            {
                //"enter a name"
            }
        }

        private void savePage(Panel panel)
        {
            Bitmap bitmap = new Bitmap(panel.Width, panel.Height);
            panel.DrawToBitmap(bitmap, new Rectangle(0, 0, panel.Width, panel.Height));

            byte[] imageBytes;
            using (var ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageBytes = ms.ToArray();
            }

            String connectionString = "Data Source = \"C:\\Users\\grigo\\OneDrive\\Υπολογιστής\\ComicStory.db\"; version=3";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Pages (Image) VALUES (@Image)";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Image", imageBytes);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        private void loadStory(PictureBox pb)
        {

        }
    }
}
