namespace _1h_telikh
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem newComicMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openComicMenuItem;
        private System.Windows.Forms.Panel pnlAssets;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Panel pnlCanvasArea;
        private canvasArea pnlCanvas; // Fixed: Correct Type
        private System.Windows.Forms.FlowLayoutPanel toolboxAssets;
        private System.Windows.Forms.Label lblAssetsTitle;
        private System.Windows.Forms.Label lblControlTitle;
        private System.Windows.Forms.Label lblPageNum;
        private System.Windows.Forms.TextBox txtComicName;
        private System.Windows.Forms.Label lblProjName;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnAddAssets;
        private System.Windows.Forms.Button btnAddText;
        private System.Windows.Forms.Button btnFlipH;
        private System.Windows.Forms.Button btnFlipV;
        private System.Windows.Forms.Label lblShortcuts;
        private System.Windows.Forms.TabControl tabAssets;
        private System.Windows.Forms.TabPage tabImages;
        private System.Windows.Forms.TabPage tabTools;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newComicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openComicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlAssets = new System.Windows.Forms.Panel();
            this.tabAssets = new System.Windows.Forms.TabControl();
            this.tabImages = new System.Windows.Forms.TabPage();
            this.toolboxAssets = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddAssets = new System.Windows.Forms.Button();
            this.tabTools = new System.Windows.Forms.TabPage();
            this.btnAddText = new System.Windows.Forms.Button();
            this.btnFlipH = new System.Windows.Forms.Button();
            this.btnFlipV = new System.Windows.Forms.Button();
            this.lblAssetsTitle = new System.Windows.Forms.Label();
            this.lblShortcuts = new System.Windows.Forms.Label();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.lblControlTitle = new System.Windows.Forms.Label();
            this.txtComicName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPageNum = new System.Windows.Forms.Label();
            this.lblProjName = new System.Windows.Forms.Label();
            this.pnlCanvasArea = new System.Windows.Forms.Panel();
            this.pnlCanvas = new _1h_telikh.canvasArea();
            this.menuStrip1.SuspendLayout();
            this.pnlAssets.SuspendLayout();
            this.tabAssets.SuspendLayout();
            this.tabImages.SuspendLayout();
            this.tabTools.SuspendLayout();
            this.pnlControls.SuspendLayout();
            this.pnlCanvasArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1100, 28);
            this.menuStrip1.TabIndex = 3;
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newComicMenuItem,
            this.openComicMenuItem});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(46, 24);
            this.fileMenu.Text = "File";
            // 
            // newComicMenuItem
            // 
            this.newComicMenuItem.Name = "newComicMenuItem";
            this.newComicMenuItem.Size = new System.Drawing.Size(174, 26);
            this.newComicMenuItem.Text = "New Comic";
            this.newComicMenuItem.Click += new System.EventHandler(this.newComicMenuItem_Click);
            // 
            // openComicMenuItem
            // 
            this.openComicMenuItem.Name = "openComicMenuItem";
            this.openComicMenuItem.Size = new System.Drawing.Size(174, 26);
            this.openComicMenuItem.Text = "Open Comic";
            this.openComicMenuItem.Click += new System.EventHandler(this.openComicMenuItem_Click);
            // 
            // pnlAssets
            // 
            this.pnlAssets.Controls.Add(this.tabAssets);
            this.pnlAssets.Controls.Add(this.lblAssetsTitle);
            this.pnlAssets.Controls.Add(this.lblShortcuts);
            this.pnlAssets.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAssets.Location = new System.Drawing.Point(0, 28);
            this.pnlAssets.Name = "pnlAssets";
            this.pnlAssets.Size = new System.Drawing.Size(200, 772);
            this.pnlAssets.TabIndex = 1;
            // 
            // tabAssets
            // 
            this.tabAssets.Controls.Add(this.tabImages);
            this.tabAssets.Controls.Add(this.tabTools);
            this.tabAssets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabAssets.Location = new System.Drawing.Point(0, 30);
            this.tabAssets.Name = "tabAssets";
            this.tabAssets.SelectedIndex = 0;
            this.tabAssets.Size = new System.Drawing.Size(200, 658);
            this.tabAssets.TabIndex = 0;
            // 
            // tabImages
            // 
            this.tabImages.Controls.Add(this.toolboxAssets);
            this.tabImages.Controls.Add(this.btnAddAssets);
            this.tabImages.Location = new System.Drawing.Point(4, 25);
            this.tabImages.Name = "tabImages";
            this.tabImages.Size = new System.Drawing.Size(192, 629);
            this.tabImages.TabIndex = 0;
            this.tabImages.Text = "Assets";
            // 
            // toolboxAssets
            // 
            this.toolboxAssets.AutoScroll = true;
            this.toolboxAssets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolboxAssets.Location = new System.Drawing.Point(0, 23);
            this.toolboxAssets.Name = "toolboxAssets";
            this.toolboxAssets.Size = new System.Drawing.Size(192, 606);
            this.toolboxAssets.TabIndex = 0;
            // 
            // btnAddAssets
            // 
            this.btnAddAssets.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddAssets.Location = new System.Drawing.Point(0, 0);
            this.btnAddAssets.Name = "btnAddAssets";
            this.btnAddAssets.Size = new System.Drawing.Size(192, 23);
            this.btnAddAssets.TabIndex = 1;
            this.btnAddAssets.Text = "Add assets (+)";
            this.btnAddAssets.Click += new System.EventHandler(this.btnAddAssets_Click);
            // 
            // tabTools
            // 
            this.tabTools.Controls.Add(this.btnAddText);
            this.tabTools.Controls.Add(this.btnFlipH);
            this.tabTools.Controls.Add(this.btnFlipV);
            this.tabTools.Location = new System.Drawing.Point(4, 25);
            this.tabTools.Name = "tabTools";
            this.tabTools.Size = new System.Drawing.Size(192, 629);
            this.tabTools.TabIndex = 1;
            this.tabTools.Text = "Tools";
            // 
            // btnAddText
            // 
            this.btnAddText.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddText.Location = new System.Drawing.Point(10, 11);
            this.btnAddText.Name = "btnAddText";
            this.btnAddText.Size = new System.Drawing.Size(45, 45);
            this.btnAddText.TabIndex = 0;
            this.btnAddText.Text = "𝓣";
            this.btnAddText.UseVisualStyleBackColor = true;
            this.btnAddText.Click += new System.EventHandler(this.btnAddText_Click);
            // 
            // btnFlipH
            // 
            this.btnFlipH.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFlipH.Location = new System.Drawing.Point(61, 11);
            this.btnFlipH.Name = "btnFlipH";
            this.btnFlipH.Size = new System.Drawing.Size(45, 45);
            this.btnFlipH.TabIndex = 1;
            this.btnFlipH.Text = "↔";
            this.btnFlipH.UseVisualStyleBackColor = true;
            this.btnFlipH.Click += new System.EventHandler(this.btnFlipH_Click);
            // 
            // btnFlipV
            // 
            this.btnFlipV.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFlipV.Location = new System.Drawing.Point(112, 11);
            this.btnFlipV.Name = "btnFlipV";
            this.btnFlipV.Size = new System.Drawing.Size(45, 45);
            this.btnFlipV.TabIndex = 2;
            this.btnFlipV.Text = "↕";
            this.btnFlipV.UseVisualStyleBackColor = true;
            this.btnFlipV.Click += new System.EventHandler(this.btnFlipV_Click);
            // 
            // lblAssetsTitle
            // 
            this.lblAssetsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAssetsTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblAssetsTitle.Location = new System.Drawing.Point(0, 0);
            this.lblAssetsTitle.Name = "lblAssetsTitle";
            this.lblAssetsTitle.Size = new System.Drawing.Size(200, 30);
            this.lblAssetsTitle.TabIndex = 1;
            this.lblAssetsTitle.Text = "Assets Toolbox";
            this.lblAssetsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShortcuts
            // 
            this.lblShortcuts.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblShortcuts.Font = new System.Drawing.Font("Arial", 7F);
            this.lblShortcuts.Location = new System.Drawing.Point(0, 688);
            this.lblShortcuts.Name = "lblShortcuts";
            this.lblShortcuts.Size = new System.Drawing.Size(200, 84);
            this.lblShortcuts.TabIndex = 2;
            this.lblShortcuts.Text = "V: Vertically flip\r\nH: Horizontally flip\r\nDel: Delete item\r\nT: Add text\r\n";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.lblControlTitle);
            this.pnlControls.Controls.Add(this.txtComicName);
            this.pnlControls.Controls.Add(this.btnSave);
            this.pnlControls.Controls.Add(this.btnView);
            this.pnlControls.Controls.Add(this.btnPrev);
            this.pnlControls.Controls.Add(this.btnNext);
            this.pnlControls.Controls.Add(this.lblPageNum);
            this.pnlControls.Controls.Add(this.lblProjName);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControls.Location = new System.Drawing.Point(920, 28);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(180, 772);
            this.pnlControls.TabIndex = 2;
            // 
            // lblControlTitle
            // 
            this.lblControlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblControlTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold);
            this.lblControlTitle.Location = new System.Drawing.Point(0, 0);
            this.lblControlTitle.Name = "lblControlTitle";
            this.lblControlTitle.Size = new System.Drawing.Size(180, 30);
            this.lblControlTitle.TabIndex = 0;
            this.lblControlTitle.Text = "Control Center";
            this.lblControlTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtComicName
            // 
            this.txtComicName.Location = new System.Drawing.Point(10, 60);
            this.txtComicName.Name = "txtComicName";
            this.txtComicName.Size = new System.Drawing.Size(150, 22);
            this.txtComicName.TabIndex = 1;
            this.txtComicName.Text = "comic";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(10, 100);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Page";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(10, 150);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(150, 40);
            this.btnView.TabIndex = 3;
            this.btnView.Text = "View Comic";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(10, 200);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(70, 30);
            this.btnPrev.TabIndex = 4;
            this.btnPrev.Text = "←";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(90, 200);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(70, 30);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "→";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblPageNum
            // 
            this.lblPageNum.Location = new System.Drawing.Point(10, 240);
            this.lblPageNum.Name = "lblPageNum";
            this.lblPageNum.Size = new System.Drawing.Size(100, 23);
            this.lblPageNum.TabIndex = 6;
            this.lblPageNum.Text = "Page: 1";
            // 
            // lblProjName
            // 
            this.lblProjName.AutoSize = true;
            this.lblProjName.Location = new System.Drawing.Point(10, 40);
            this.lblProjName.Name = "lblProjName";
            this.lblProjName.Size = new System.Drawing.Size(88, 16);
            this.lblProjName.TabIndex = 7;
            this.lblProjName.Text = "Comic Name:";
            // 
            // pnlCanvasArea
            // 
            this.pnlCanvasArea.BackColor = System.Drawing.Color.DimGray;
            this.pnlCanvasArea.Controls.Add(this.pnlCanvas);
            this.pnlCanvasArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCanvasArea.Location = new System.Drawing.Point(200, 28);
            this.pnlCanvasArea.Name = "pnlCanvasArea";
            this.pnlCanvasArea.Size = new System.Drawing.Size(720, 772);
            this.pnlCanvasArea.TabIndex = 0;
            // 
            // pnlCanvas
            // 
            this.pnlCanvas.AllowDrop = true;
            this.pnlCanvas.BackColor = System.Drawing.Color.White;
            this.pnlCanvas.Location = new System.Drawing.Point(0, 0);
            this.pnlCanvas.Name = "pnlCanvas";
            this.pnlCanvas.Size = new System.Drawing.Size(500, 700);
            this.pnlCanvas.TabIndex = 0;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1100, 800);
            this.Controls.Add(this.pnlCanvasArea);
            this.Controls.Add(this.pnlAssets);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Comics Editor";
            this.Load += new System.EventHandler(this.CenterCanvas);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.CenterCanvas);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnlAssets.ResumeLayout(false);
            this.tabAssets.ResumeLayout(false);
            this.tabImages.ResumeLayout(false);
            this.tabTools.ResumeLayout(false);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.pnlCanvasArea.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}