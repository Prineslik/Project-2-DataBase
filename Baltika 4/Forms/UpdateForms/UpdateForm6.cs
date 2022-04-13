using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Baltika_4.Forms.UpdateForms
{
    public partial class UpdateForm6 : Form
    {
        private readonly MainForm mainForm;

        public UpdateForm6(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private void UpdateForm6_Load(object sender, EventArgs e)
        {
            (Controls["textBox2"] as TextBox).Text = mainForm.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            (Controls["textBox3"] as TextBox).Text = mainForm.dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
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
                            CommandText = "update [Ships_Batches] set [ShipID] = @shipID, [BatchID] = @batchID where [ID] = " + mainForm.dataGridView1.CurrentRow.Cells[0].Value
                        };
                        command.Parameters.AddWithValue("@shipID", textBox2.Text);
                        command.Parameters.AddWithValue("@batchID", textBox3.Text);

                        Sql.adapters[6].UpdateCommand = command;
                        Sql.adapters[6].UpdateCommand.ExecuteNonQuery();
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
                MessageBox.Show("Запись изменена");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
