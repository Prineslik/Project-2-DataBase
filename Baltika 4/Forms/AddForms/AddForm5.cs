using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Baltika_4.Forms.AddForms
{
    public partial class AddForm5 : Form
    {
        public static MainForm mainForm;

        public AddForm5(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox3.Text == ""
                || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" 
                || maskedTextBox1.Text == "" || textBox7.Text == "")
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
                        CommandText = "insert into Ships ([RegNumber], [Name], [TypeShipID], [Skipper], [Capacity], [Year], [DockyerdID]) " +
                        "values (@regNumber, @name, @typeShipID, @skipper, @capacity, @year, @dockyerdID)"
                    };
                    command.Parameters.AddWithValue("@regNumber", textBox2.Text);
                    command.Parameters.AddWithValue("@name", textBox3.Text);
                    command.Parameters.AddWithValue("@typeShipID", textBox4.Text);
                    command.Parameters.AddWithValue("@skipper", textBox5.Text);
                    command.Parameters.AddWithValue("@capacity", textBox6.Text);
                    command.Parameters.AddWithValue("@year", Convert.ToDateTime(maskedTextBox1.Text));
                    command.Parameters.AddWithValue("@dockyerdID", textBox7.Text);
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
            Sql.ConnectToAccess(Sql.dataSets[5], Sql.adapters[5], mainForm.dataGridView1, Sql.selectQueries[5], Sql.tables[5]);
            foreach (TextBox textBox in Controls.OfType<TextBox>())
            {
                textBox.Text = "";
            }
            maskedTextBox1.Text = "";
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

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
