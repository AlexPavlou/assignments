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
        private List<ComicItem> comicElements = new List<ComicItem>();
        private ComicItem selectedElement;
        private Point offset;
        private int page = 1;
        private string comicsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comics");
        private string assetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");
        private bool isResizing = false;

        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.pnlCanvasArea.Resize += (s, e) => CenterCanvas();
            this.Load += (s, e) => CenterCanvas();
            this.pnlCanvas.DragEnter += (s, e) => e.Effect = DragDropEffects.Copy; // copies image on to the canvas

            pnlCanvas.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(pnlCanvas, true);

            if (!Directory.Exists(comicsDir)) Directory.CreateDirectory(comicsDir);
            if (!Directory.Exists(assetDir)) Directory.CreateDirectory(assetDir);

            this.KeyPreview = true;
            LoadAssets();
        }

        private void CenterCanvas()
        {
            if (pnlCanvas != null && pnlCanvasArea != null)
            {
                pnlCanvas.Left = (pnlCanvasArea.Width - pnlCanvas.Width) / 2;
                pnlCanvas.Top = (pnlCanvasArea.Height - pnlCanvas.Height) / 2;
            }
        }

        // Load image into memory so it can be deleted or overwritten by the user
        // while the app is running
        private Image LoadInMem(string path)
        {
            using (var ms = new MemoryStream(File.ReadAllBytes(path)))
                return Image.FromStream(ms);
        }

        private void LoadAssets()
        {
            toolboxAssets.Controls.Clear();
            foreach (string f in Directory.GetFiles(assetDir))
            {
                // create a PictureBox element for each file in our assets folder
                var pb = new PictureBox { Image = LoadInMem(f), Size = new Size(80, 80), SizeMode = PictureBoxSizeMode.Zoom, Margin = new Padding(5), Tag = f };
                // initiate drag-and-drop using the file path stored in the Tag property
                pb.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) pb.DoDragDrop(pb.Tag, DragDropEffects.Copy); };
                toolboxAssets.Controls.Add(pb);
            }
        }

        private void pnlCanvas_DragAndDrop(object sender, DragEventArgs e)
        {
            string f = (string)e.Data.GetData(DataFormats.StringFormat);
            var img = LoadInMem(f);
            int w = Math.Min(img.Width, 200);
            int h = (int)(w / ((float)img.Width / img.Height));
            comicElements.Add(new ComicItem
            {
                Img = img,
                Rect = new Rectangle(pnlCanvas.PointToClient(new Point(e.X, e.Y)), new Size(w, h)),
                Ratio = (float)img.Width / img.Height
            });
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            // if not Mouse_1 then exit
            if (e.Button != MouseButtons.Left) return;

            for (int i = comicElements.Count - 1; i >= 0; i--)
            {
                // create trigger for resizing
                Rectangle resizeTrigger = new Rectangle(comicElements[i].Rect.Right - 15, comicElements[i].Rect.Bottom - 15, 15, 15);
                if (resizeTrigger.Contains(e.Location))
                {
                    selectedElement = comicElements[i];
                    isResizing = true;
                    pnlCanvas.Invalidate();
                    return;
                }
            }

            selectedElement = null;
            // Iterate backwards (z-order, top-to-bottom)
            for (int i = comicElements.Count - 1; i >= 0; i--)
            {
                if (comicElements[i].Rect.Contains(e.Location))
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
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_MouseUp(object sender, MouseEventArgs e) { isResizing = false; pnlCanvas.Cursor = Cursors.Default; }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T)
            {
                btnAddText_Click(null, null);
                return;
            }

            // if no item is selected then exit
            if (selectedElement == null) return;

            if (e.KeyCode == Keys.Delete)
            {
                comicElements.Remove(selectedElement);
                selectedElement = null;
            }
            else if (e.KeyCode == Keys.V && !selectedElement.IsText)
            {
                FlipVertical();
            }
            else if (e.KeyCode == Keys.H && !selectedElement.IsText)
            {
                FlipHorizontal();
            }

            pnlCanvas.Invalidate(); // redraw
        }

        private void FlipHorizontal() => selectedElement.Img.RotateFlip(RotateFlipType.RotateNoneFlipX);
        private void FlipVertical() => selectedElement.Img.RotateFlip(RotateFlipType.RotateNoneFlipY);

        private void btnFlipH_Click(object sender, EventArgs e) { if (selectedElement != null && !selectedElement.IsText) { FlipHorizontal(); pnlCanvas.Invalidate(); } }
        private void btnFlipV_Click(object sender, EventArgs e) { if (selectedElement != null && !selectedElement.IsText) { FlipVertical(); pnlCanvas.Invalidate(); } }

        private void pnlCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            bool overHandle = false;
            foreach (var i in comicElements)
                if (new Rectangle(i.Rect.Right - 15, i.Rect.Bottom - 15, 15, 15).Contains(e.Location)) overHandle = true;

            if (selectedElement == null) { pnlCanvas.Cursor = overHandle ? Cursors.SizeNWSE : Cursors.Default; return; }

            if (isResizing)
            {
                int newW = Math.Max(20, e.X - selectedElement.Rect.X);
                selectedElement.Rect.Width = newW;
                selectedElement.Rect.Height = (int)(newW / selectedElement.Ratio);
            }
            else if (e.Button == MouseButtons.Left)
                selectedElement.Rect.Location = new Point(e.X - offset.X, e.Y - offset.Y);

            pnlCanvas.Invalidate(); //redraw
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = string.IsNullOrWhiteSpace(txtComicName.Text) ? "comic" : txtComicName.Text;
            string path = Path.Combine(comicsDir, $"{name}_{page}.jpg");

            using (Bitmap bmp = new Bitmap(pnlCanvas.Width, pnlCanvas.Height))
            {
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    gfx.Clear(Color.White);

                    foreach (ComicItem item in comicElements)
                    {
                        if (item.IsText)
                            gfx.DrawString(item.Content, new Font("Microsoft Sans Serif", 14), Brushes.Black, item.Rect);
                        else
                            gfx.DrawImage(item.Img, item.Rect);
                    }
                }

                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L); // set quality to 100%
                bmp.Save(path, GetEncoder(ImageFormat.Jpeg), encoderParams);
            }
        }


        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
                if (codec.FormatID == format.Guid) return codec;
            return null;
        }

        private void btnAddText_Click(object sender, EventArgs e)
        {
            comicElements.Add(new ComicItem { IsText = true, Content = "Text", Rect = new Rectangle(50, 50, 100, 40), Ratio = 2.5f });
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (var i in comicElements)
                if (i.IsText) e.Graphics.DrawString(i.Content, new Font("Microsoft Sans Serif", 14), Brushes.Black, i.Rect);
                else e.Graphics.DrawImage(i.Img, i.Rect);
            if (selectedElement != null) ControlPaint.DrawFocusRectangle(e.Graphics, selectedElement.Rect);
        }

        private void newComicMenuItem_Click(object sender, EventArgs e)
        {
            comicElements.Clear();
            page = 1;
            txtComicName.Text = "";
            lblPageNum.Text = "Page: 1";
            pnlCanvas.Invalidate();
        }

        private void openComicMenuItem_Click(object sender, EventArgs e)
        {
            using (var od = new OpenFileDialog { Filter = "Comic Pages|*.jpg", InitialDirectory = comicsDir })
                if (od.ShowDialog() == DialogResult.OK)
                {
                    // Get the filename, without the folder or the extension
                    string fn = Path.GetFileNameWithoutExtension(od.FileName);

                    // split the name, add the page number after the last '_' (adds it if it doesn't exist already)
                    string name = fn.Contains("_") ? fn.Substring(0, fn.LastIndexOf('_')) : fn;
                    int.TryParse(fn.Substring(fn.LastIndexOf('_') + 1), out page);

                    txtComicName.Text = name;
                    lblPageNum.Text = "Page: " + page;
                    LoadImageAsItem(name, page);
                }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            page++;
            lblPageNum.Text = "Page: " + page;
            LoadImageAsItem(txtComicName.Text, page);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (page > 1) { page--; lblPageNum.Text = "Page: " + page; LoadImageAsItem(txtComicName.Text, page); }
        }

        private void btnAddAssets_Click(object sender, EventArgs e)
        {
            using (var od = new OpenFileDialog()) if (od.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(od.FileName, Path.Combine(assetDir, Path.GetFileName(od.FileName)), true);
                    LoadAssets();
                }
        }

        // load an existing page as an image
        private void LoadImageAsItem(string name, int pNum)
        {
            string path = Path.Combine(comicsDir, $"{name}_{pNum}.jpg");
            comicElements.Clear();
            if (File.Exists(path))
            {
                var img = LoadInMem(path);
                comicElements.Add(new ComicItem { Img = img, Rect = new Rectangle(0, 0, img.Width, img.Height), Ratio = (float)img.Width / img.Height });
            }
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (selectedElement != null && selectedElement.IsText)
            {
                using (Form f = new Form { Width = 300, Height = 120, Text = "Edit Text", StartPosition = FormStartPosition.CenterParent })
                {
                    TextBox txt = new TextBox { Left = 10, Top = 10, Width = 260, Text = selectedElement.Content };
                    Button btn = new Button { Text = "OK", Left = 190, Width = 80, Top = 40, DialogResult = DialogResult.OK };
                    f.Controls.Add(txt); f.Controls.Add(btn); f.AcceptButton = btn;
                    if (f.ShowDialog() == DialogResult.OK) { selectedElement.Content = txt.Text; pnlCanvas.Invalidate(); }
                }
            }
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
    }
}