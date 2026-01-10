using _1h_telikh;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace _1h_telikh
{
    public partial class Form1 : Form
    {
        private int page = 1;
        private string comicsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comics");
        private string assetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");

        public Form1()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            if (!Directory.Exists(comicsDir)) Directory.CreateDirectory(comicsDir);
            if (!Directory.Exists(assetDir)) Directory.CreateDirectory(assetDir);

            this.KeyPreview = true;
            LoadAssets();
        }

        private void CenterCanvas(object sender, EventArgs e)
        {
            if (pnlCanvas != null && pnlCanvasArea != null)
            {
                pnlCanvas.Left = (pnlCanvasArea.Width - pnlCanvas.Width) / 2;
                pnlCanvas.Top = (pnlCanvasArea.Height - pnlCanvas.Height) / 2;
            }
        }

        // loads image into memory
        private Image LoadImage(string path)
        {
            return Image.FromStream(new MemoryStream(File.ReadAllBytes(path)));
        }

        private void LoadAssets()
        {
            toolboxAssets.Controls.Clear();
            foreach (string f in Directory.GetFiles(assetDir))
            {
                // create a PictureBox element for each file in our assets folder
                var pb = new PictureBox { Image = LoadImage(f), Size = new Size(80, 80), SizeMode = PictureBoxSizeMode.Zoom, Margin = new Padding(5), Tag = f };
                // attach to it the following listener
                // this uses the 'DoDragDrop' method to copy and paste the image through its file path (contained in the Tag property)
                pb.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) pb.DoDragDrop(pb.Tag, DragDropEffects.Copy); };
                toolboxAssets.Controls.Add(pb);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T)
            {
                btnAddText_Click(null, null); // add a text element
                return;
            }

            if (pnlCanvas.selectedElement == null) return;

            if (e.KeyCode == Keys.Delete)
            {
                pnlCanvas.comicElements.Remove(pnlCanvas.selectedElement);
                pnlCanvas.selectedElement = null;
            }
            else if (e.KeyCode == Keys.V && !pnlCanvas.selectedElement.IsText)
            {
                pnlCanvas.FlipVertical();
            }
            else if (e.KeyCode == Keys.H && !pnlCanvas.selectedElement.IsText)
            {
                pnlCanvas.FlipHorizontal();
            }

            pnlCanvas.Invalidate(); // redraw
        }

        private void btnFlipH_Click(object sender, EventArgs e) { if (pnlCanvas.selectedElement != null && !pnlCanvas.selectedElement.IsText) { pnlCanvas.FlipHorizontal(); pnlCanvas.Invalidate(); } }
        private void btnFlipV_Click(object sender, EventArgs e) { if (pnlCanvas.selectedElement != null && !pnlCanvas.selectedElement.IsText) { pnlCanvas.FlipVertical(); pnlCanvas.Invalidate(); } }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = string.IsNullOrWhiteSpace(txtComicName.Text) ? "comic" : txtComicName.Text;
            string path = Path.Combine(comicsDir, $"{name}_{page}.png"); // comic_1.png, comic_2.png etc

            pnlCanvas.SaveToImage(path);
        }

        private void btnAddText_Click(object sender, EventArgs e)
        {
            pnlCanvas.comicElements.Add(new ComicItem { IsText = true, Content = "Text", Rect = new Rectangle(50, 50, 100, 40), Ratio = 2.5f });
            pnlCanvas.Invalidate();
        }

        private void newComicMenuItem_Click(object sender, EventArgs e)
        {
            pnlCanvas.comicElements.Clear();
            pnlCanvas.selectedElement = null;
            page = 1;
            txtComicName.Text = "";
            lblPageNum.Text = "Page: 1";
            pnlCanvas.Invalidate();
        }

        private void openComicMenuItem_Click(object sender, EventArgs e)
        {
            pnlCanvas.selectedElement = null;
            using (var od = new OpenFileDialog { Filter = "Comic Pages|*.png", InitialDirectory = comicsDir })
                if (od.ShowDialog() == DialogResult.OK)
                {
                    // Get the filename, without the folder or the extension
                    string fn = Path.GetFileNameWithoutExtension(od.FileName);

                    string name = fn.Contains("_") ? fn.Substring(0, fn.LastIndexOf('_')) : fn;
                    int.TryParse(fn.Substring(fn.LastIndexOf('_') + 1), out page);

                    txtComicName.Text = name;
                    lblPageNum.Text = "Page: " + page;
                    LoadPageAsImage(name, page);
                }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            page++;
            lblPageNum.Text = "Page: " + page;
            LoadPageAsImage(txtComicName.Text, page);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (page > 1) { page--; lblPageNum.Text = "Page: " + page; LoadPageAsImage(txtComicName.Text, page); }
        }

        private void btnAddAssets_Click(object sender, EventArgs e)
        {
            using (var od = new OpenFileDialog()) if (od.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(od.FileName, Path.Combine(assetDir, Path.GetFileName(od.FileName)), true);
                    LoadAssets();
                }
        }

        private void LoadPageAsImage(string name, int pNum)
        {
            string path = Path.Combine(comicsDir, $"{name}_{pNum}.png");
            pnlCanvas.comicElements.Clear();
            if (File.Exists(path))
            {
                var img = LoadImage(path);
                pnlCanvas.comicElements.Add(new ComicItem { Img = img, Rect = new Rectangle(0, 0, img.Width, img.Height), Ratio = (float)img.Width / img.Height });
            }
            pnlCanvas.Invalidate();
        }

        private void btnView_Click(object sender, EventArgs e) => new ViewerForm(txtComicName.Text).Show();
    }

    public class ComicItem
    {
        public Image Img;
        public Rectangle Rect;
        public bool IsText;
        public string Content;
        public float Ratio;


        public bool ContainsPoint(Point p)
        {
            return Rect.Contains(p);
        }
    }

    public class canvasArea : Panel
    {
        public List<ComicItem> comicElements = new List<ComicItem>();
        public ComicItem selectedElement;
        public Point offset; // offset between a comicItem and the cursor
        public bool isResizing = false;

        public canvasArea()
        {
            this.DoubleBuffered = true;
            this.AllowDrop = true;
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        protected override void OnDragDrop(DragEventArgs e)
        {
            // Retrieve the file path string that was passed from the DragDrop event
            string f = (string)e.Data.GetData(DataFormats.StringFormat);
            var img = Image.FromStream(new MemoryStream(File.ReadAllBytes(f)));
            int w = Math.Min(img.Width, 200);
            // keep the aspect ratio locked
            int h = (int)(w / ((float)img.Width / img.Height));
            comicElements.Add(new ComicItem
            {
                Img = img,
                Rect = new Rectangle(this.PointToClient(new Point(e.X, e.Y)), new Size(w, h)),
                Ratio = (float)img.Width / img.Height
            });

            // redraw canvas
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            // if the button pressed wasn't Mouse_1 then exit
            if (e.Button != MouseButtons.Left) return;

            for (int i = comicElements.Count - 1; i >= 0; i--)
            {
                // create trigger rectangle for resizing, (15px*15px)
                Rectangle resizeTrigger = new Rectangle(comicElements[i].Rect.Right - 15, comicElements[i].Rect.Bottom - 15, 15, 15);
                if (resizeTrigger.Contains(e.Location))
                {
                    selectedElement = comicElements[i];
                    isResizing = true;
                    this.Invalidate();
                    return;
                }
            }

            selectedElement = null;
            // Iterate backwards (z-order, top-to-bottom)
            for (int i = comicElements.Count - 1; i >= 0; i--)
            {
                if (comicElements[i].ContainsPoint(e.Location))
                {
                    selectedElement = comicElements[i];
                    offset = new Point(e.X - selectedElement.Rect.X, e.Y - selectedElement.Rect.Y);

                    // re-insert element at the top of the list so that it has the highest
                    // z coordinate
                    comicElements.RemoveAt(i);
                    comicElements.Add(selectedElement);
                    break;
                }
            }
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool isCursorOverElem = false;
            foreach (var i in comicElements)
                // check if the cursor is on top of a comic element
                if (new Rectangle(i.Rect.Right - 15, i.Rect.Bottom - 15, 15, 15).Contains(e.Location)) isCursorOverElem = true;

            // adjust cursor icon if  cursor
            this.Cursor = isCursorOverElem ? Cursors.SizeNWSE : Cursors.Default;

            if (selectedElement == null) { return; }

            if (isResizing)
            {
                // update element width while mantaining aspect ratio
                int newWidth = Math.Max(20, e.X - selectedElement.Rect.X);
                selectedElement.Rect.Width = newWidth;
                selectedElement.Rect.Height = (int)(newWidth / selectedElement.Ratio);
            }
            else if (e.Button == MouseButtons.Left)
                selectedElement.Rect.Location = new Point(e.X - offset.X, e.Y - offset.Y);

            this.Invalidate(); //redraw
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isResizing = false;
            this.Cursor = Cursors.Default;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // draw all comic elements and text
            foreach (var i in comicElements)
                if (i.IsText) e.Graphics.DrawString(i.Content, new Font("Microsoft Sans Serif", 14), Brushes.Black, i.Rect);
                else e.Graphics.DrawImage(i.Img, i.Rect);
            if (selectedElement != null) ControlPaint.DrawFocusRectangle(e.Graphics, selectedElement.Rect); // draw "selected" rectangle indicator
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (selectedElement != null && selectedElement.IsText)
            {
                using (Form f = new Form { Width = 300, Height = 120, Text = "Edit Text", StartPosition = FormStartPosition.CenterParent })
                {
                    TextBox txt = new TextBox { Left = 10, Top = 10, Width = 260, Text = selectedElement.Content };
                    Button btn = new Button { Text = "OK", Left = 190, Width = 80, Top = 40, DialogResult = DialogResult.OK };
                    f.Controls.Add(txt); f.Controls.Add(btn); f.AcceptButton = btn;
                    // update text value and redraw
                    if (f.ShowDialog() == DialogResult.OK) { selectedElement.Content = txt.Text; this.Invalidate(); }
                }
            }
        }

        public void FlipHorizontal() => selectedElement.Img.RotateFlip(RotateFlipType.RotateNoneFlipX);
        public void FlipVertical() => selectedElement.Img.RotateFlip(RotateFlipType.RotateNoneFlipY);

        public void SaveToImage(string path)
        {
            using (Bitmap bmp = new Bitmap(this.Width, this.Height))
            {
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    // quality settings
                    gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                    gfx.Clear(Color.White);

                    foreach (ComicItem item in comicElements)
                    {
                        if (item.IsText)
                        {
                            using (Font font = new Font("Microsoft Sans Serif", 14))
                                gfx.DrawString(item.Content, font, Brushes.Black, item.Rect);
                        }
                        else
                        {
                            gfx.DrawImage(item.Img, item.Rect);
                        }
                    }
                }

                bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png); // save page
            }
        }
    }
}