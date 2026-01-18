using System;
using System.Drawing;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Globalization;

namespace WikiFormsApp
{
    public partial class Form1 : Form
    {
        private readonly WikiServices _wikiServices = new WikiServices();
        private readonly DatabaseManager _dbManager = new DatabaseManager();
        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();
        private WikiArticle _currentArticle;

        public Form1()
        {
            InitializeComponent();
            LoadFavorites();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text)) return;

            // δειχνουμε το εικονιδιο αναμονης για να ξερει ο χρηστης οτι ψαχνουμε
            this.Cursor = Cursors.WaitCursor;

            try
            {
                _currentArticle = await _wikiServices.GetSummaryAsync(txtSearch.Text);

                if (_currentArticle != null)
                {
                    lblTitle.Text = _currentArticle.Title;
                    txtExtract.Text = _currentArticle.Extract;

                    // αν υπαρχει εικονα, την κατεβαζουμε και τη δειχνουμε
                    if (!string.IsNullOrEmpty(_currentArticle.ThumbnailUrl))
                    {
                        var stream = await _wikiServices.GetImageStreamAsync(_currentArticle.ThumbnailUrl);
                        pictureBox1.Image = Image.FromStream(stream);
                    }
                }
                else { MessageBox.Show("δεν το βρηκα!"); }
            }
            finally { this.Cursor = Cursors.Default; }
        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            // εδω βαζουμε το προγραμμα να διαβασει το κειμενο στα ελληνικα
            _synth.SpeakAsyncCancelAll();
            try
            {
                _synth.SelectVoiceByHints(VoiceGender.Neutral, VoiceAge.NotSet, 0, new CultureInfo("el-GR"));
                _synth.SpeakAsync(txtExtract.Text);
            }
            catch { MessageBox.Show("δεν εχεις ελληνικη φωνη στα windows"); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // αποθηκευση στη βαση δεδομενων
            if (_currentArticle != null)
            {
                _dbManager.AddFavorite(_currentArticle.Title, _currentArticle.PageUrl);
                LoadFavorites();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // σβησιμο απο τα αγαπημενα
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);
                _dbManager.DeleteFavorite(id);
                LoadFavorites();
            }
        }

        private void LoadFavorites()
        {
            // ανανεωση της λιστας που βλεπει ο χρηστης
            dataGridView1.DataSource = _dbManager.GetFavorites();
            if (dataGridView1.Columns["Id"] != null) dataGridView1.Columns["Id"].Visible = false;
        }
    }
}