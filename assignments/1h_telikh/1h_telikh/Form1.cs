using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace _1h_telikh
{
    public partial class Form1 : Form
    {
        private int curPage = 1;
        private string comicsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Comics");
        private string assetDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets");

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;

            // create the necessary directories if they don't exist
            if (!Directory.Exists(comicsDir)) Directory.CreateDirectory(comicsDir);
            if (!Directory.Exists(assetDir)) Directory.CreateDirectory(assetDir);
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

        // load all available assets into the assets toolbar
        private void LoadAssets()
        {
            toolboxAssets.Controls.Clear();
            foreach (string f in Directory.GetFiles(assetDir))
            {
                var pb = new PictureBox { Image = Image.FromFile(f), Size = new Size(80, 80), SizeMode = PictureBoxSizeMode.Zoom, Margin = new Padding(5), Tag = f };
                pb.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) pb.DoDragDrop(pb.Tag, DragDropEffects.Copy); };
                toolboxAssets.Controls.Add(pb);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.T) btnAddText_Click(null, null);
            if (pnlCanvas.selectedElement == null) return;

            if (e.KeyCode == Keys.Delete)
            {
                pnlCanvas.comicElements.Remove(pnlCanvas.selectedElement);
                pnlCanvas.selectedElement = null;
            }
            else if (e.KeyCode == Keys.V && !pnlCanvas.selectedElement.IsText) pnlCanvas.FlipVertical();
            else if (e.KeyCode == Keys.H && !pnlCanvas.selectedElement.IsText) pnlCanvas.FlipHorizontal();

            pnlCanvas.Invalidate();
        }

        private void btnFlipH_Click(object sender, EventArgs e) { if (pnlCanvas.selectedElement?.IsText == false) { pnlCanvas.FlipHorizontal(); pnlCanvas.Invalidate(); } }
        private void btnFlipV_Click(object sender, EventArgs e) { if (pnlCanvas.selectedElement?.IsText == false) { pnlCanvas.FlipVertical(); pnlCanvas.Invalidate(); } }

        private void btnSave_Click(object sender, EventArgs e) => pnlCanvas.SaveToImage(Path.Combine(comicsDir, $"{(string.IsNullOrWhiteSpace(txtComicName.Text) ? "comic" : txtComicName.Text)}_{curPage}.png"));

        private void btnAddText_Click(object sender, EventArgs e) { pnlCanvas.comicElements.Add(new comicElement { IsText = true, Content = "Text", Rect = new Rectangle(50, 50, 100, 40), Ratio = 2.5f }); pnlCanvas.Invalidate(); }

        private void newComicMenuItem_Click(object sender, EventArgs e) { foreach (var el in pnlCanvas.comicElements) el.Img?.Dispose(); pnlCanvas.comicElements.Clear(); pnlCanvas.selectedElement = null; curPage = 1; txtComicName.Text = ""; lblPageNum.Text = "Page: 1"; pnlCanvas.Invalidate(); }

        private void openComicMenuItem_Click(object sender, EventArgs e)
        {
            pnlCanvas.selectedElement = null;
            using (var od = new OpenFileDialog { Filter = "Comic Pages|*.png;*.xml", InitialDirectory = comicsDir })
                if (od.ShowDialog() == DialogResult.OK)
                {
                    string fn = Path.GetFileNameWithoutExtension(od.FileName);
                    string name = fn.Contains("_") ? fn.Substring(0, fn.LastIndexOf('_')) : fn;
                    int.TryParse(fn.Substring(fn.LastIndexOf('_') + 1), out curPage);
                    txtComicName.Text = name;
                    lblPageNum.Text = "Page: " + curPage;
                    loadPage(name, curPage);
                }
        }

        private void btnNext_Click(object sender, EventArgs e) { curPage++; lblPageNum.Text = "Page: " + curPage; loadPage(txtComicName.Text, curPage); }
        private void btnPrev_Click(object sender, EventArgs e) { if (curPage > 1) { curPage--; lblPageNum.Text = "Page: " + curPage; loadPage(txtComicName.Text, curPage); } }

        private void btnAddAssets_Click(object sender, EventArgs e)
        {
            using (var od = new OpenFileDialog()) if (od.ShowDialog() == DialogResult.OK) { File.Copy(od.FileName, Path.Combine(assetDir, Path.GetFileName(od.FileName)), true); LoadAssets(); }
        }

        private void loadPage(string name, int pageNum)
        {
            string pngPath = Path.Combine(comicsDir, $"{name}_{pageNum}.png");
            string xmlPath = Path.Combine(comicsDir, $"{name}_{pageNum}.xml");

            foreach (var el in pnlCanvas.comicElements) el.Img?.Dispose();
            pnlCanvas.comicElements.Clear();

            // look for the xml file
            if (File.Exists(xmlPath))
            {
                using (var xmlFs = File.OpenRead(xmlPath))
                {
                    var items = (List<comicElement>)new XmlSerializer(typeof(List<comicElement>)).Deserialize(xmlFs);
                    foreach (var item in items)
                    {
                        if (item.IsText)
                        {
                            pnlCanvas.comicElements.Add(item);
                        }
                        else if (File.Exists(item.filePath))
                        {
                            using (var fs = new FileStream(item.filePath, FileMode.Open, FileAccess.Read))
                            {
                                var ms = new MemoryStream();
                                fs.CopyTo(ms);
                                ms.Position = 0;
                                item.Img = new Bitmap(ms);
                            }

                            if (item.flippedHoriz) item.Img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            if (item.flippedVert) item.Img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                            pnlCanvas.comicElements.Add(item);
                        }
                    }
                }
            }
            // otherwise load the baked-in page as a single object
            else if (File.Exists(pngPath))
            {
                using (var fs = new FileStream(pngPath, FileMode.Open, FileAccess.Read))
                {
                    var img = new Bitmap(fs);
                    pnlCanvas.comicElements.Add(new comicElement { Img = img, Rect = new Rectangle(0, 0, img.Width, img.Height), Ratio = (float)img.Width / img.Height });
                }
            }
            pnlCanvas.Invalidate();
        }
        private void btnView_Click(object sender, EventArgs e) => new ViewerForm(txtComicName.Text).Show();
    }

    public class comicElement
    {
        // serialized variables
        public int X, Y, Width, Height;
        public string filePath;
        public string Content; // for text elements
        public bool IsText;
        public bool flippedVert = false;
        public bool flippedHoriz;
        public float Ratio;

        [XmlIgnore] public Image Img;
        [XmlIgnore]
        public Rectangle Rect
        {
            get => new Rectangle(X, Y, Width, Height);
            set { X = value.X; Y = value.Y; Width = value.Width; Height = value.Height; }
        }

        public bool ContainsPoint(Point p) => Rect.Contains(p);
    }

    public class canvasArea : Panel
    {
        public List<comicElement> comicElements = new List<comicElement>();
        public comicElement selectedElement;
        public Point offset;
        public bool isResizing = false;

        public canvasArea() { DoubleBuffered = true; AllowDrop = true; }

        protected override void OnDragEnter(DragEventArgs e) => e.Effect = DragDropEffects.Copy;

        protected override void OnDragDrop(DragEventArgs e)
        {
            string f = (string)e.Data.GetData(DataFormats.StringFormat);
            using (var fs = new FileStream(f, FileMode.Open, FileAccess.Read))
            {
                var img = new Bitmap(Image.FromStream(fs));
                int w = Math.Min(img.Width, 200);
                comicElements.Add(new comicElement
                {
                    Img = img,
                    filePath = f,
                    Rect = new Rectangle(PointToClient(new Point(e.X, e.Y)), new Size(w, (int)(w / ((float)img.Width / img.Height)))),
                    Ratio = (float)img.Width / img.Height
                });
            }
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            selectedElement = null;

            for (int i = comicElements.Count - 1; i >= 0; i--)
            {
                if (new Rectangle(comicElements[i].Rect.Right - 15, comicElements[i].Rect.Bottom - 15, 15, 15).Contains(e.Location))
                { selectedElement = comicElements[i]; isResizing = true; Invalidate(); return; }
            }

            for (int i = comicElements.Count - 1; i >= 0; i--)
            {
                if (comicElements[i].ContainsPoint(e.Location))
                {
                    selectedElement = comicElements[i];
                    offset = new Point(e.X - selectedElement.Rect.X, e.Y - selectedElement.Rect.Y);
                    comicElements.RemoveAt(i); comicElements.Add(selectedElement); // Bring to front
                    break;
                }
            }
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool hover = false;
            foreach (var i in comicElements) if (new Rectangle(i.Rect.Right - 15, i.Rect.Bottom - 15, 15, 15).Contains(e.Location)) hover = true;
            Cursor = hover ? Cursors.SizeNWSE : Cursors.Default;

            if (selectedElement == null) return;

            if (isResizing)
            {
                int newWidth = Math.Max(20, e.X - selectedElement.Rect.X);
                selectedElement.Rect = new Rectangle(selectedElement.X, selectedElement.Y, newWidth, (int)(newWidth / selectedElement.Ratio));
            }
            else if (e.Button == MouseButtons.Left)
            {
                selectedElement.Rect = new Rectangle(e.X - offset.X, e.Y - offset.Y, selectedElement.Width, selectedElement.Height);
            }
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e) { isResizing = false; Cursor = Cursors.Default; }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            foreach (var i in comicElements)
            {
                if (i.IsText) e.Graphics.DrawString(i.Content, new Font("Microsoft Sans Serif", 14), Brushes.Black, i.Rect);
                else if (i.Img != null) e.Graphics.DrawImage(i.Img, i.Rect);
            }
            if (selectedElement != null) ControlPaint.DrawFocusRectangle(e.Graphics, selectedElement.Rect);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (selectedElement?.IsText == true)
            {
                using (var f = new Form { Width = 300, Height = 120, Text = "Edit", StartPosition = FormStartPosition.CenterParent })
                {
                    var txt = new TextBox { Left = 10, Top = 10, Width = 260, Text = selectedElement.Content };
                    var btn = new Button { Text = "OK", Left = 190, Top = 40, DialogResult = DialogResult.OK };
                    f.Controls.AddRange(new Control[] { txt, btn }); f.AcceptButton = btn;
                    if (f.ShowDialog() == DialogResult.OK) { selectedElement.Content = txt.Text; Invalidate(); }
                }
            }
        }

        public void FlipHorizontal()
        {
            selectedElement?.Img?.RotateFlip(RotateFlipType.RotateNoneFlipX);
            try
            {
                selectedElement.flippedHoriz = !selectedElement.flippedHoriz;
            }
            catch { }
        }

        public void FlipVertical()
        {
            selectedElement?.Img?.RotateFlip(RotateFlipType.RotateNoneFlipY);
            try
            {
                selectedElement.flippedVert = !selectedElement.flippedVert;
            }
            catch { }
        }

        public void SaveToImage(string path)
        {
            using (var bmp = new Bitmap(Width, Height))
            {
                using (var gfx = Graphics.FromImage(bmp))
                {
                    gfx.Clear(Color.White);
                    gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                    foreach (var i in comicElements)
                        if (i.IsText) gfx.DrawString(i.Content, new Font("Microsoft Sans Serif", 14), Brushes.Black, i.Rect);
                        else if (i.Img != null) gfx.DrawImage(i.Img, i.Rect);
                }
                bmp.Save(path, ImageFormat.Png);
            }

            // serialize the data of allcomicElements into the xml file
            using (var fs = File.Create(Path.ChangeExtension(path, ".xml")))
                new XmlSerializer(typeof(List<comicElement>)).Serialize(fs, comicElements);
        }
    }
}