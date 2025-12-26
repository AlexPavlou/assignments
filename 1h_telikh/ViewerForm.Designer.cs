namespace _1h_telikh
{
    partial class ViewerForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pb;
        private System.Windows.Forms.Panel pnlView;
        private System.Windows.Forms.Panel pnlSide;
        private System.Windows.Forms.Button btnN;
        private System.Windows.Forms.Button btnP;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.NumericUpDown numSpeed;
        private System.Windows.Forms.Timer timer1;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pb = new System.Windows.Forms.PictureBox();
            this.pnlView = new System.Windows.Forms.Panel();
            this.pnlSide = new System.Windows.Forms.Panel();
            this.btnP = new System.Windows.Forms.Button();
            this.btnN = new System.Windows.Forms.Button();
            this.btnAuto = new System.Windows.Forms.Button();
            this.numSpeed = new System.Windows.Forms.NumericUpDown();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
            this.pnlView.SuspendLayout();
            this.pnlSide.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.BackColor = System.Drawing.Color.DimGray;
            this.pb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb.Location = new System.Drawing.Point(0, 0);
            this.pb.Name = "pb";
            this.pb.Padding = new System.Windows.Forms.Padding(2);
            this.pb.Size = new System.Drawing.Size(750, 600);
            this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb.TabIndex = 0;
            this.pb.TabStop = false;
            // 
            // pnlView
            // 
            this.pnlView.Controls.Add(this.pb);
            this.pnlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlView.Location = new System.Drawing.Point(0, 0);
            this.pnlView.Name = "pnlView";
            this.pnlView.Size = new System.Drawing.Size(750, 600);
            this.pnlView.TabIndex = 0;
            // 
            // pnlSide
            // 
            this.pnlSide.BackColor = System.Drawing.SystemColors.Control;
            this.pnlSide.Controls.Add(this.btnP);
            this.pnlSide.Controls.Add(this.btnN);
            this.pnlSide.Controls.Add(this.btnAuto);
            this.pnlSide.Controls.Add(this.numSpeed);
            this.pnlSide.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSide.Location = new System.Drawing.Point(750, 0);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(150, 600);
            this.pnlSide.TabIndex = 1;
            // 
            // btnP
            // 
            this.btnP.Location = new System.Drawing.Point(10, 20);
            this.btnP.Name = "btnP";
            this.btnP.Size = new System.Drawing.Size(60, 40);
            this.btnP.TabIndex = 0;
            this.btnP.Text = "Back";
            this.btnP.Click += new System.EventHandler(this.btnP_Click);
            // 
            // btnN
            // 
            this.btnN.Location = new System.Drawing.Point(80, 20);
            this.btnN.Name = "btnN";
            this.btnN.Size = new System.Drawing.Size(60, 40);
            this.btnN.TabIndex = 1;
            this.btnN.Text = "Next";
            this.btnN.Click += new System.EventHandler(this.btnN_Click);
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(10, 80);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(130, 30);
            this.btnAuto.TabIndex = 2;
            this.btnAuto.Text = "Auto-turn";
            this.btnAuto.Click += new System.EventHandler(this.btnAuto_Click);
            // 
            // numSpeed
            // 
            this.numSpeed.Location = new System.Drawing.Point(10, 120);
            this.numSpeed.Name = "numSpeed";
            this.numSpeed.Size = new System.Drawing.Size(120, 22);
            this.numSpeed.TabIndex = 3;
            this.numSpeed.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ViewerForm
            // 
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.pnlView);
            this.Controls.Add(this.pnlSide);
            this.Name = "ViewerForm";
            this.Text = "Comic Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
            this.pnlView.ResumeLayout(false);
            this.pnlSide.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed)).EndInit();
            this.ResumeLayout(false);

        }
    }
}