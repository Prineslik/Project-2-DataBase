using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Baltika_4.Forms.UpdateForms
{
    public partial class UpdateForm2 : Form
    {
        private readonly MainForm mainForm;

        public UpdateForm2(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    if (textBox2.Text == "" || textBox3.Text == "")
                    {
                        MessageBox.Show("Текстовые поля должны быть заполнены");
                        //Notify.ShowNotify("Текстовые поля должны быть заполнены", Properties.Resources.Info);

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
                                CommandText = "update [Cargoes_Batches] set [IDCargo] = @idCargo, [IDBatch] = @idBatch where [ID_CB] = " + mainForm.dataGridView1.CurrentRow.Cells[0].Value
                            };
                            command.Parameters.AddWithValue("@idCargo", textBox2.Text);
                            command.Parameters.AddWithValue("@idBatch", textBox3.Text);

                            Sql.adapters[2].UpdateCommand = command;
                            Sql.adapters[2].UpdateCommand.ExecuteNonQuery();
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            //Notify.ShowNotify("Неверный формат даты", Properties.Resources.Info);
                            return;
                        }
                    }
                    Sql.ClearTable(mainForm.dataGridView1);
                    Sql.ConnectToAccess(Sql.dataSets[2], Sql.adapters[2], mainForm.dataGridView1, Sql.selectQueries[2], Sql.tables[2]);
                    MessageBox.Show("Запись изменена");

                    //Notify.ShowNotify("Запись изменена", Properties.Resources.Info);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    //Notify.ShowNotify(ex.Message, Properties.Resources.Info);
                }
            }
        }

        private void UpdateForma2_Load(object sender, EventArgs e)
        {
            (Controls["textBox2"] as TextBox).Text = mainForm.dataGridView1.CurrentRow.Cells[1].Value.ToString();
            (Controls["textBox3"] as TextBox).Text = mainForm.dataGridView1.CurrentRow.Cells[2].Value.ToString();
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
