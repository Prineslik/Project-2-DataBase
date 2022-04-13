using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Baltika_4.Forms.UpdateForms
{
    public partial class UpdateForm : Form
    {
        private readonly MainForm mainForm;

        public UpdateForm(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private bool DateCompare(DateTime now, DateTime date)
        {
            DateTime month = date.AddMonths(1);

            if (date <= month)
            {
                return true;
            }
            return false;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {

            try
            {
                DateTime dateNow = DateTime.Now;

                DateTime dateOut = new DateTime(Convert.ToInt32(maskedTextBox1.Text.Substring(6, 4)), Convert.ToInt32(maskedTextBox1.Text.Substring(3, 2)), Convert.ToInt32(maskedTextBox1.Text.Substring(0, 2)));
                //MessageBox.Show("God - " + dateOut.Year + " monyh " + dateOut.Month + " day " + dateOut.Day);
                DateTime dateIn = new DateTime(Convert.ToInt32(maskedTextBox2.Text.Substring(6, 4)), Convert.ToInt32(maskedTextBox2.Text.Substring(3, 2)), Convert.ToInt32(maskedTextBox2.Text.Substring(0, 2)));
                //MessageBox.Show("God - " + dateIn.Year + " monyh " + dateIn.Month + " day " + dateIn.Day);


                if (textBox1.Text == "" || maskedTextBox1.Text == "" || textBox4.Text == ""
                    || textBox5.Text == "" || maskedTextBox2.Text == "")
                {
                    MessageBox.Show("Текстовые поля должны быть заполнены");
                    //Notify.ShowNotify("Текстовые поля должны быть заполнены", Properties.Resources.Info);
                    
                    return;
                }
                if (!DateCompare(dateNow, Convert.ToDateTime(maskedTextBox1.Text)) || !DateCompare(dateNow, Convert.ToDateTime(maskedTextBox2.Text)))
                {
                    MessageBox.Show("Вводимая дата должна быть не больше чем на месяц от текущей");
                    return;
                }
                if (dateIn < dateOut)
                {
                    MessageBox.Show("Дата отправки не должна быть меньше даты отправки");
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
                            CommandText = "update [Batches] set [CustomValue] = @customValue, [DepartureDate] = @departureDate, [ArriveDate] = @arriveDate, [OriginID] = @originID, [DestinationID] = @destinationID, " + "[CustomClearance] = @customClearance where [BatchID] = " + mainForm.dataGridView1.CurrentRow.Cells[0].Value
                        };
                        command.Parameters.AddWithValue("@customValue", textBox1.Text);
                        command.Parameters.AddWithValue("@departureDate", Convert.ToDateTime(maskedTextBox1.Text));
                        command.Parameters.AddWithValue("@arriveDate", Convert.ToDateTime(maskedTextBox2.Text));
                        command.Parameters.AddWithValue("@originID", textBox4.Text);
                        command.Parameters.AddWithValue("@destinationID", textBox5.Text);
                        command.Parameters.AddWithValue("@customClearance", checkBox1.Checked);
                        Sql.adapters[0].UpdateCommand = command;
                        Sql.adapters[0].UpdateCommand.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Неверный формат даты");
                        //Notify.ShowNotify("Неверный формат даты", Properties.Resources.Info);

                        maskedTextBox1.Text = "";
                        return;
                    }
                }
                Sql.ClearTable(mainForm.dataGridView1);
                Sql.ConnectToAccess(Sql.dataSets[0], Sql.adapters[0], mainForm.dataGridView1, Sql.selectQueries[0], Sql.tables[0]);
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

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = Convert.ToBoolean(mainForm.dataGridView1.CurrentRow.Cells[6].Value);
            for (int i = 1; i < 6; i++)
            {
                if (i == 2 || i == 3) { continue; }
                (Controls["textBox" + i.ToString()] as TextBox).Text = mainForm.dataGridView1.CurrentRow.Cells[i].Value.ToString();
            }
            maskedTextBox1.Text = mainForm.dataGridView1.CurrentRow.Cells[2].Value.ToString();
            maskedTextBox2.Text = mainForm.dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
