using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Baltika_4.Forms.AddForms
{
    public partial class AddForm6 : Form
    {
        public static MainForm mainForm;

        public AddForm6(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Текстовые поля должны быть заполнены");
                return;
            }
            using (OleDbConnection connection = new OleDbConnection(Properties.Settings.Default.connectionString))
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand
                    {
                        Connection = connection,
                        CommandText = "insert into Ships_Batches (ShipID, BatchID) " +
                        "values (@shipID, @batchID)"
                    };
                    command.Parameters.AddWithValue("@shipID", textBox2.Text);
                    command.Parameters.AddWithValue("@batchID", textBox3.Text);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            Sql.ClearTable(mainForm.dataGridView1);
            Sql.ConnectToAccess(Sql.dataSets[6], Sql.adapters[6], mainForm.dataGridView1, Sql.selectQueries[6], Sql.tables[6]);
            foreach (TextBox textBox in Controls.OfType<TextBox>())
            {
                textBox.Text = "";
            }
            MessageBox.Show("Запись добавлена");
        }
    }
}
