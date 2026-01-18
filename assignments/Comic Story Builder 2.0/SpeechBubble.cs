using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comic_Story_Builder_2._0
{
    internal class SpeechBubble : Asset
    {
        public RichTextBox rtb;
        public SpeechBubble(PictureBox pb, EventHandler doubleclick, MouseEventHandler mouseup, MouseEventHandler mousedown, EventHandler mouseenter, EventHandler mouseleave, MouseEventHandler mousemove) : base(pb, mouseup, mousedown, mouseenter, mouseleave, mousemove)
        {
            picturebox.DoubleClick += doubleclick;
        }

        public void CreateRTB()
        {
            rtb = new RichTextBox();
            rtb.Location = new System.Drawing.Point(picturebox.Width/5, picturebox.Height/4);
            rtb.Width = 3 * picturebox.Width/5;
            rtb.Height = 2 * picturebox.Height/5;
            rtb.BackColor = Color.White;
            rtb.BorderStyle = BorderStyle.None;
            rtb.SelectionAlignment = HorizontalAlignment.Center;
            picturebox.Controls.Add(rtb);
        }

        public bool Reset_Bubble(PictureBox pb)
        {
            bool found = false;
            if (picturebox == pb)
            {
                picturebox.Location = firstloc;
                picturebox.Width = width;
                picturebox.Height = height;
                picturebox.Parent = firstparent;
                picturebox.Controls.Remove(rtb);
                found = true;
            }
            return found;
        }
    }
}
