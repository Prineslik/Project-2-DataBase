using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Baltika_4.Forms.AddForms
{
    public partial class AddForm : Form
    {
        public static MainForm mainForm;

        public AddForm(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private bool DateCompare(DateTime now, DateTime date)
        {
            DateTime month = date.AddMonths(1);//new DateTime(now.Year, 1 + now.Month, now.Day);

            if (date <= month)
            {
                return true;
            }
            return false;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            DateTime dateNow = DateTime.Now;
            
            DateTime dateOut = new DateTime(Convert.ToInt32(maskedTextBox1.Text.Substring(6,4)), Convert.ToInt32(maskedTextBox1.Text.Substring(3, 2)), Convert.ToInt32(maskedTextBox1.Text.Substring(0, 2)));
            //MessageBox.Show("God - "+dateOut.Year +" monyh " +dateOut.Month+" day "+dateOut.Day);
            DateTime dateIn = new DateTime(Convert.ToInt32(maskedTextBox2.Text.Substring(6,4)), Convert.ToInt32(maskedTextBox2.Text.Substring(3, 2)), Convert.ToInt32(maskedTextBox2.Text.Substring(0, 2)));
            //MessageBox.Show("God - "+ dateIn.Year +" monyh " + dateIn.Month+" day "+ dateIn.Day);

            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == ""
                || maskedTextBox1.Text == "" || maskedTextBox2.Text == "")
            {
                MessageBox.Show("Текстовые поля должны быть заполнены");
                return;
            }
            if (!DateCompare(dateNow, dateOut) || !DateCompare(dateNow, dateOut))
            {
                MessageBox.Show("Вводимая дата должна быть не больше чем на месяц от текущей");
                return;
            }
            if (dateIn<dateOut)
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
                        CommandText = "insert into Batches (CustomValue, DepartureDate, ArriveDate, OriginID, DestinationID, CustomClearance) " +
                        "values (@customValue, @departureDate, @arriveDate, @originID, @destinationID, @customClearance)"
                    };
                    command.Parameters.AddWithValue("@customValue", textBox2.Text);
                    command.Parameters.AddWithValue("@departureDate", Convert.ToDateTime(maskedTextBox1.Text));
                    command.Parameters.AddWithValue("@arriveDate", Convert.ToDateTime(maskedTextBox2.Text));
                    command.Parameters.AddWithValue("@originID", textBox3.Text);
                    command.Parameters.AddWithValue("@destinationID", textBox4.Text);
                    command.Parameters.AddWithValue("@customClearance", checkBox1.Checked);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    maskedTextBox1.Text = "";
                    maskedTextBox2.Text = "";
                    return;
                }
            }
            Sql.ClearTable(mainForm.dataGridView1);
            Sql.ConnectToAccess(Sql.dataSets[0], Sql.adapters[0], mainForm.dataGridView1, Sql.selectQueries[0], Sql.tables[0]);
            foreach (TextBox textBox in Controls.OfType<TextBox>())
            {
                textBox.Text = "";
            }
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            checkBox1.Checked = false;
            MessageBox.Show("Запись добавлена");
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
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

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
