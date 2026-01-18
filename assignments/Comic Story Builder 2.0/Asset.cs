using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Comic_Story_Builder_2._0
{
    internal class Asset
    {
        public PictureBox picturebox { get; set; }
        public Point firstloc { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Control firstparent { get; set; }

        public Asset(PictureBox pb, MouseEventHandler mouseup, MouseEventHandler mousedown, EventHandler mouseenter, EventHandler mouseleave, MouseEventHandler mousemove)
        {
            picturebox = pb;
            firstloc = pb.Location;
            firstparent = pb.Parent;
            width = pb.Width;
            height = pb.Height;
            pb.MouseUp += mouseup;
            pb.MouseDown += mousedown;
            pb.MouseEnter += mouseenter;
            pb.MouseLeave += mouseleave;
            pb.MouseMove += mousemove;
        }

        public bool Reset_Asset(PictureBox pb)
        {
            bool found = false;
            if (picturebox == pb)
            {
                picturebox.Location = firstloc;
                picturebox.Width = width;
                picturebox.Height = height;
                picturebox.Parent = firstparent;
                found = true;
            }
            return found;
        }
    }
}
