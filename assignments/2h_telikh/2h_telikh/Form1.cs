using _2h_telikh.Properties;
using GenerativeAI;
using GenerativeAI.Clients;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace _2h_telikh
{
    public partial class Form1 : Form
    {
        bool haveMemory = false;
        private const string API_KEY = "AIzaSyBOp9XSiVaD-Y7mRp12kiya3F7ciNCsEBQ";

        private ChatSession chatSession;
        private string selectedModel = "models/gemini-2.5-flash";
        private string lastQuestion = "";
        private string lastAnswer = "";

        public Form1()
        {
            InitializeComponent();
            this.Icon = Resources.icon;
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(promptBox.Text)) return;

            if (chatSession == null)
            {
                var adapter = new GoogleAIPlatformAdapter(API_KEY);
                var model = new GenerativeModel(adapter, selectedModel);

                chatSession = model.StartChat();
                discussionBox.AppendText($"--- Νέα συνομιλία με το {selectedModel} ---\n");
            }

            btnSend.Enabled = false; // disable send button to avoid overloading the agent

            discussionBox.AppendText("\nGemini: Σκέφτομαι...\n");

            try
            {
                var client = new GenerativeModel(API_KEY, selectedModel);
                if (haveMemory)
                {
                    var response2 = await chatSession.GenerateContentAsync(promptBox.Text);
                    lastQuestion = promptBox.Text;
                    lastAnswer = response2.Text;
                    discussionBox.AppendText("\nΧρήστης: " + promptBox.Text + "\nGemini: " + response2.Text + "\n");
                }
                else
                {
                    var response = await client.GenerateContentAsync(promptBox.Text);
                    lastQuestion = promptBox.Text;
                    lastAnswer = response.Text;
                    discussionBox.AppendText("\nΧρήστης: " + promptBox.Text + "\nGemini: " + response.Text + "\n");
                }

                promptBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Σφάλμα: {ex.Message}", "Πρόβλημα API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                discussionBox.Text += "Αποτυχία λήψης απάντησης.";
            }
            finally
            {
                btnSend.Enabled = true;
                // auto-scrolls to the bottom of the discussion
                discussionBox.ScrollToCaret();
            }
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            // load AI models
            discussionBox.AppendText("Loading models...\n");
            try
            {
                var adapter = new GoogleAIPlatformAdapter(API_KEY);
                var client = new ModelClient(adapter);
                var response = await client.ListModelsAsync();

                if (aIToolStripMenuItem != null)
                {
                    // populate "AI" Tool Strip menu item with the available AI models
                    ListBox lbModels = new ListBox();
                    lbModels.Height = 400;
                    lbModels.Width = 300;
                    lbModels.BorderStyle = BorderStyle.None;

                    lbModels.Click += (s, args) =>
                    {
                        if (lbModels.SelectedItem != null)
                        {
                            selectedModel = lbModels.SelectedItem.ToString();
                            discussionBox.AppendText($"\nChanged Model to: {selectedModel}\n");
                            aIToolStripMenuItem.DropDown.Close();
                        }
                    };

                    foreach (var model in response.Models)
                    {
                        if (model.SupportedGenerationMethods.Contains("generateContent"))
                        {
                            lbModels.Items.Add(model.Name);
                        }
                    }

                    // pass the generated ListBox to the tool strip menu as a ToolStripControlHost object, so that it can be used
                    aIToolStripMenuItem.DropDownItems.Add(new ToolStripControlHost(lbModels));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Σφάλμα: {ex.Message}");
            }
        }

        public class promptPanel : RichTextBox
        {
            public promptPanel() { }

            public void CenterPanel(Control parent)
            {
                if (parent != null)
                {
                    this.Left = (parent.ClientSize.Width - this.Width) / 2;
                }
            }
        }

        public class discussionPanel : promptPanel
        {
            public discussionPanel() { }

            public new void CenterPanel(Control parent)
            {
                if (parent != null)
                {
                    this.Left = (parent.ClientSize.Width - this.Width) / 2;
                    this.Top = (int)(parent.ClientSize.Height * 0.1);
                }
            }

            // voodoo magic to remove the blinking line
            protected override void WndProc(ref Message m)
            {
                if (m.Msg == 0x0007) return;
                base.WndProc(ref m);
            }
        }

        private void εξαγωγήΔιαλόγουToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text file|*.txt", Title = "Export Dialogue" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                    System.IO.File.WriteAllText(sfd.FileName, discussionBox.Text);
            }
        }

        private void εισαγωγήΔιαλόγουToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text file|*.txt", Title = "Import Prompt" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                    promptBox.Text = System.IO.File.ReadAllText(ofd.FileName);
            }
        }

        private void νέοςΔιάλογοςToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // clear all important chat variables
            chatSession = null;
            promptBox.Text = "";
            discussionBox.Text = "";
        }

        private void έξοδοςToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void αποθλToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(lastQuestion) || string.IsNullOrEmpty(lastAnswer))
            {
                MessageBox.Show("No conversation to save yet.");
                return;
            }

            try
            {
                string dbPath = "chat_history.db";
                string connectionString = $"Data Source={dbPath};Version=3;";

                // create file if missing
                if (!System.IO.File.Exists(dbPath))
                {
                    System.Data.SQLite.SQLiteConnection.CreateFile(dbPath);
                }

                // open a connection the the DB
                using (var conn = new System.Data.SQLite.SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string createTable = @"CREATE TABLE IF NOT EXISTS History (
                                    ID INTEGER PRIMARY KEY AUTOINCREMENT, 
                                    Date TEXT, 
                                    Question TEXT, 
                                    Answer TEXT)";
                    using (var cmd = new System.Data.SQLite.SQLiteCommand(createTable, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    string insertSql = "INSERT INTO History (Date, Question, Answer) VALUES (@d, @q, @a)";
                    using (var cmd = new System.Data.SQLite.SQLiteCommand(insertSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@d", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@q", lastQuestion);
                        cmd.Parameters.AddWithValue("@a", lastAnswer);
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Saved to SQLite Database!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB Error: {ex.Message}");
            }
        }

        private void προβολήΙστοToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!System.IO.File.Exists("chat_history.db")) return;

            var dt = new System.Data.DataTable();
            using (var adapter = new System.Data.SQLite.SQLiteDataAdapter("SELECT * FROM History ORDER BY ID DESC", "Data Source=chat_history.db;Version=3;"))
            {
                try { adapter.Fill(dt); } catch { return; }
            }

            var grid = new DataGridView { Dock = DockStyle.Fill, DataSource = dt, ReadOnly = true, AllowUserToAddRows = false, RowHeadersVisible = false };

            grid.DataBindingComplete += (s, args) =>
            {
                grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                if (grid.Columns.Count >= 4)
                {
                    grid.Columns[0].Width = 40;
                    grid.Columns[1].Width = 120;
                    grid.Columns[2].Width = 200;
                    grid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            };

            new Form { Text = "Chat History", Size = new Size(900, 600), StartPosition = FormStartPosition.CenterParent, Controls = { grid } }.ShowDialog();
        }
    }
}