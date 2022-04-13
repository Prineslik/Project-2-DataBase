using Baltika_4.Classes;
using Baltika_4.Forms;
using Baltika_4.Forms.AddForms;
using Baltika_4.Forms.UpdateForms;
using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Baltika_4
{
    public partial class MainForm : Form
    {
        public static int indexer = 0;
        public static DataSet ds = new DataSet();
        public static OleDbDataAdapter adapter = new OleDbDataAdapter();

        public MainForm()
        {
            InitializeComponent();

            ExitBtn.Click += (s, e) => Close();

            DeleteBtn.Click += (s, e) =>
            {
                DeleteForm deleteForm = new DeleteForm(this);
                deleteForm.ShowDialog();
            };

            SearchBtn.Click += (s, e) =>
            {
                SearchForm searchForm = new SearchForm(this);
                searchForm.ShowDialog();
            };
            ReportBtn.Click += (s, e) =>
            {
                Report report = new Report(this);
                report.ShowDialog();
            };
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Header_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x112, 0xf012, 0);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            switch (indexer)
            {
                case 0:
                    new AddForm(this).ShowDialog();
                    break;
                case 1:
                    new AddForm1(this).ShowDialog();
                    break;
                case 2:
                    new AddForm2(this).ShowDialog();
                    break;
                case 3:
                    new AddForm3(this).ShowDialog();
                    break;
                case 4:
                    new AddForm4(this).ShowDialog();
                    break;
                case 5:
                    new AddForm5(this).ShowDialog();
                    break;
                case 6:
                    new AddForm6(this).ShowDialog();
                    break;
                case 7:
                    new AddForm7(this).ShowDialog();
                    break;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            indexer = 0;
            Sql.ConnectToAccess(ds, adapter, dataGridView1, Sql.selectQueries[indexer], "[Person]");
            char[] s = new char[] { '[', ']' };
            for (int i = 0; i < Sql.tables.Count; i++)
            {
                comboBox1.Items.Add(Sql.tables[i].Trim(s));
            }
            comboBox1.SelectedIndex = 0;
        }

        private void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index < dataGridView1.Rows.Count)
            {
                switch (MainForm.indexer)
                {
                    case 0:
                        new UpdateForm(this).ShowDialog();
                        break;
                    case 1:
                        new UpdateForm1(this).ShowDialog();
                        break;
                    case 2:
                        new UpdateForm2(this).ShowDialog();
                        break;
                    case 3:
                        new UpdateForm3(this).ShowDialog();
                        break;
                    case 4:
                        new UpdateForm4(this).ShowDialog();
                        break;
                    case 5:
                        new UpdateForm5(this).ShowDialog();
                        break;
                    case 6:
                        new UpdateForm6(this).ShowDialog();
                        break;
                    case 7:
                        new UpdateForm7(this).ShowDialog();
                        break;
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            indexer = comboBox1.SelectedIndex;
            switch (indexer)
            {
                case 0:
                    indexer = 0;
                    Sql.ClearTable(dataGridView1);
                    Sql.ConnectToAccess(Sql.dataSets[0], Sql.adapters[0], dataGridView1, Sql.selectQueries[0], "[Batches]");
                    break;
                case 1:
                    indexer = 1;
                    Sql.ClearTable(dataGridView1);
                    Sql.ConnectToAccess(Sql.dataSets[1], Sql.adapters[1], dataGridView1, Sql.selectQueries[1], "[Cargoes]");
                    break;
                case 2:
                    indexer = 2;
                    Sql.ClearTable(dataGridView1);
                    Sql.ConnectToAccess(Sql.dataSets[2], Sql.adapters[2], dataGridView1, Sql.selectQueries[2], "[Cargoes_Batches]");
                    break;
                case 3:
                    indexer = 3;
                    Sql.ClearTable(dataGridView1);
                    Sql.ConnectToAccess(Sql.dataSets[3], Sql.adapters[3], dataGridView1, Sql.selectQueries[3], "[ConsSender]");
                    break;
                case 4:
                    indexer = 4;
                    Sql.ClearTable(dataGridView1);
                    Sql.ConnectToAccess(Sql.dataSets[4], Sql.adapters[4], dataGridView1, Sql.selectQueries[4], "[Ports]");
                    break;
                case 5:
                    indexer = 5;
                    Sql.ClearTable(dataGridView1);
                    Sql.ConnectToAccess(Sql.dataSets[5], Sql.adapters[5], dataGridView1, Sql.selectQueries[5], "[Ships]");
                    break;
                case 6:
                    indexer = 6;
                    Sql.ClearTable(dataGridView1);
                    Sql.ConnectToAccess(Sql.dataSets[6], Sql.adapters[6], dataGridView1, Sql.selectQueries[6], "[Ships_Batches]");
                    break;
                case 7:
                    indexer = 7;
                    Sql.ClearTable(dataGridView1);
                    Sql.ConnectToAccess(Sql.dataSets[7], Sql.adapters[7], dataGridView1, Sql.selectQueries[7], "[ShipsType]");
                    break;
            }
            if (indexer == 5)
            {
                ReportBtn.Show();
            }
            else
            {
                ReportBtn.Hide();
            }
        }
    }
}
