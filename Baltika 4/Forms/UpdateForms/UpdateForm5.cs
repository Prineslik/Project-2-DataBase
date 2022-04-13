using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Baltika_4.Forms.UpdateForms
{
    public partial class UpdateForm5 : Form
    {
        private readonly MainForm mainForm;

        public UpdateForm5(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private void UpdateForm5_Load(object sender, EventArgs e)
        {
            for (int i = 2; i < 9; i++)
            {
                if (i == 7) { continue; }
                (Controls["textBox" + i.ToString()] as TextBox).Text = mainForm.dataGridView1.CurrentRow.Cells[i-1].Value.ToString();
            }
            maskedTextBox1.Text = mainForm.dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "" || maskedTextBox1.Text == "" || textBox3.Text == ""
                    || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox8.Text == "")
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
                            CommandText = "update [Ships] set [RegNumber] = @regNumber, [Name] = @name, [TypeShipID] = @typeShipID, [Skipper] = @skipper, [Capacity] = @capacity, [Year] = @year, [DockyerdID] = @dockyerdID where [ShipID] = " + mainForm.dataGridView1.CurrentRow.Cells[0].Value
                        };
                        command.Parameters.AddWithValue("@regNumber", textBox2.Text);
                        command.Parameters.AddWithValue("@name", textBox3.Text);
                        command.Parameters.AddWithValue("@typeShipID", textBox4.Text);
                        command.Parameters.AddWithValue("@skipper", textBox5.Text);
                        command.Parameters.AddWithValue("@capacity", textBox6.Text);
                        command.Parameters.AddWithValue("@year", Convert.ToDateTime(maskedTextBox1.Text));
                        command.Parameters.AddWithValue("@dockyerdID", textBox8.Text);

                        Sql.adapters[5].UpdateCommand = command;
                        Sql.adapters[5].UpdateCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        //Notify.ShowNotify("Неверный формат даты", Properties.Resources.Info);

                        maskedTextBox1.Text = "";
                        return;
                    }
                }
                Sql.ClearTable(mainForm.dataGridView1);
                Sql.ConnectToAccess(Sql.dataSets[5], Sql.adapters[5], mainForm.dataGridView1, Sql.selectQueries[5], Sql.tables[5]);
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
