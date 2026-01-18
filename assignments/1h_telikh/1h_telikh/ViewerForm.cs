using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace _1h_telikh
{
    public partial class ViewerForm : Form
    {
        private int cur_page = 1;
        private string comicName;
        private string comicsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comics");

        public ViewerForm(string p)
        {
            // use "comic" as default comic name
            comicName = string.IsNullOrWhiteSpace(p) ? "comic" : p;
            InitializeComponent();
            ShowPage(0);
        }

        private bool ShowPage(int step)
        {
            int nextPage = Math.Max(1, cur_page + step);
            string path = Path.Combine(comicsDir, $"{comicName}_{nextPage}.png"); // find the next page's path

            if (File.Exists(path))
            {
                cur_page = nextPage;

                if (pb.Image != null) pb.Image.Dispose();

                using (var ms = new MemoryStream(File.ReadAllBytes(path)))
                    pb.Image = Image.FromStream(ms); // load next page

                return true;
            }
            return false;
        }

        private void btnP_Click(object sender, EventArgs e) => ShowPage(-1);
        private void btnN_Click(object sender, EventArgs e) => ShowPage(1);
        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            else
            {
                if (numSpeed.Value <= 0)
                {
                    return;
                }

                timer1.Interval = (int)numSpeed.Value * 1000;
                timer1.Start();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!ShowPage(1)) timer1.Stop();
        }
    }
}