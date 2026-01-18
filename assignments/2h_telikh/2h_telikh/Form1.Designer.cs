namespace _2h_telikh
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.νέοςΔιάλογοςToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.εισαγωγήΔιαλόγουToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.εξαγωγήΔιαλόγουToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.έξοδοςToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.βΔToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.αποθλToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.προβολήΙστοToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.εξαγωγηΔιαλογουToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.έξοδοςToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.PictureBox();
            this.promptBox = new _2h_telikh.Form1.promptPanel();
            this.discussionBox = new _2h_telikh.Form1.discussionPanel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenu,
            this.βΔToolStripMenuItem,
            this.aIToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 4;
            // 
            // fileMenu
            // 
            this.fileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.νέοςΔιάλογοςToolStripMenuItem,
            this.εισαγωγήΔιαλόγουToolStripMenuItem,
            this.εξαγωγήΔιαλόγουToolStripMenuItem,
            this.έξοδοςToolStripMenuItem1});
            this.fileMenu.Name = "fileMenu";
            this.fileMenu.Size = new System.Drawing.Size(70, 24);
            this.fileMenu.Text = "Αρχείο";
            // 
            // νέοςΔιάλογοςToolStripMenuItem
            // 
            this.νέοςΔιάλογοςToolStripMenuItem.Name = "νέοςΔιάλογοςToolStripMenuItem";
            this.νέοςΔιάλογοςToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.νέοςΔιάλογοςToolStripMenuItem.Text = "Νέος Διάλογος";
            this.νέοςΔιάλογοςToolStripMenuItem.Click += new System.EventHandler(this.νέοςΔιάλογοςToolStripMenuItem_Click);
            // 
            // εισαγωγήΔιαλόγουToolStripMenuItem
            // 
            this.εισαγωγήΔιαλόγουToolStripMenuItem.Name = "εισαγωγήΔιαλόγουToolStripMenuItem";
            this.εισαγωγήΔιαλόγουToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.εισαγωγήΔιαλόγουToolStripMenuItem.Text = "Εισαγωγή Ερώτησης";
            this.εισαγωγήΔιαλόγουToolStripMenuItem.Click += new System.EventHandler(this.εισαγωγήΔιαλόγουToolStripMenuItem_Click);
            // 
            // εξαγωγήΔιαλόγουToolStripMenuItem
            // 
            this.εξαγωγήΔιαλόγουToolStripMenuItem.Name = "εξαγωγήΔιαλόγουToolStripMenuItem";
            this.εξαγωγήΔιαλόγουToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.εξαγωγήΔιαλόγουToolStripMenuItem.Text = "Εξαγωγή Διαλόγου";
            this.εξαγωγήΔιαλόγουToolStripMenuItem.Click += new System.EventHandler(this.εξαγωγήΔιαλόγουToolStripMenuItem_Click);
            // 
            // έξοδοςToolStripMenuItem1
            // 
            this.έξοδοςToolStripMenuItem1.Name = "έξοδοςToolStripMenuItem1";
            this.έξοδοςToolStripMenuItem1.Size = new System.Drawing.Size(233, 26);
            this.έξοδοςToolStripMenuItem1.Text = "Έξοδος";
            this.έξοδοςToolStripMenuItem1.Click += new System.EventHandler(this.έξοδοςToolStripMenuItem1_Click);
            // 
            // βΔToolStripMenuItem
            // 
            this.βΔToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.αποθλToolStripMenuItem,
            this.προβολήΙστοToolStripMenuItem});
            this.βΔToolStripMenuItem.Name = "βΔToolStripMenuItem";
            this.βΔToolStripMenuItem.Size = new System.Drawing.Size(42, 24);
            this.βΔToolStripMenuItem.Text = "ΒΔ";
            // 
            // αποθλToolStripMenuItem
            // 
            this.αποθλToolStripMenuItem.Name = "αποθλToolStripMenuItem";
            this.αποθλToolStripMenuItem.Size = new System.Drawing.Size(266, 26);
            this.αποθλToolStripMenuItem.Text = "Αποθήκευση τρεχ. απαντ.";
            this.αποθλToolStripMenuItem.Click += new System.EventHandler(this.αποθλToolStripMenuItem_Click);
            // 
            // προβολήΙστοToolStripMenuItem
            // 
            this.προβολήΙστοToolStripMenuItem.Name = "προβολήΙστοToolStripMenuItem";
            this.προβολήΙστοToolStripMenuItem.Size = new System.Drawing.Size(266, 26);
            this.προβολήΙστοToolStripMenuItem.Text = "Προβολή Ιστορικού";
            this.προβολήΙστοToolStripMenuItem.Click += new System.EventHandler(this.προβολήΙστοToolStripMenuItem_Click);
            // 
            // aIToolStripMenuItem
            // 
            this.aIToolStripMenuItem.Name = "aIToolStripMenuItem";
            this.aIToolStripMenuItem.Size = new System.Drawing.Size(37, 24);
            this.aIToolStripMenuItem.Text = "AI";
            // 
            // εξαγωγηΔιαλογουToolStripMenuItem
            // 
            this.εξαγωγηΔιαλογουToolStripMenuItem.Name = "εξαγωγηΔιαλογουToolStripMenuItem";
            this.εξαγωγηΔιαλογουToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.εξαγωγηΔιαλογουToolStripMenuItem.Text = "Εξαγωγή Διαλόγου";
            // 
            // έξοδοςToolStripMenuItem
            // 
            this.έξοδοςToolStripMenuItem.Name = "έξοδοςToolStripMenuItem";
            this.έξοδοςToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.έξοδοςToolStripMenuItem.Text = "Έξοδος";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(180, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Input question:";
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Image = ((System.Drawing.Image)(resources.GetObject("btnSend.Image")));
            this.btnSend.Location = new System.Drawing.Point(544, 373);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(30, 30);
            this.btnSend.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnSend.TabIndex = 8;
            this.btnSend.TabStop = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // promptBox
            // 
            this.promptBox.AcceptsTab = true;
            this.promptBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.promptBox.BackColor = System.Drawing.SystemColors.Control;
            this.promptBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.promptBox.Location = new System.Drawing.Point(184, 331);
            this.promptBox.Name = "promptBox";
            this.promptBox.Size = new System.Drawing.Size(400, 82);
            this.promptBox.TabIndex = 6;
            this.promptBox.Text = "";
            // 
            // discussionBox
            // 
            this.discussionBox.AcceptsTab = true;
            this.discussionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.discussionBox.BackColor = System.Drawing.Color.White;
            this.discussionBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.discussionBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.discussionBox.Location = new System.Drawing.Point(184, 33);
            this.discussionBox.Name = "discussionBox";
            this.discussionBox.ReadOnly = true;
            this.discussionBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.discussionBox.Size = new System.Drawing.Size(400, 272);
            this.discussionBox.TabIndex = 5;
            this.discussionBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.promptBox);
            this.Controls.Add(this.discussionBox);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Microslop Wrapper (v1.0)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSend)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenu;
        private System.Windows.Forms.ToolStripMenuItem εξαγωγηΔιαλογουToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem έξοδοςToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem βΔToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem αποθλToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem προβολήΙστοToolStripMenuItem;
        private discussionPanel discussionBox;
        private promptPanel promptBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnSend;
        private System.Windows.Forms.ToolStripMenuItem νέοςΔιάλογοςToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem εισαγωγήΔιαλόγουToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem εξαγωγήΔιαλόγουToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem έξοδοςToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aIToolStripMenuItem;
    }
}