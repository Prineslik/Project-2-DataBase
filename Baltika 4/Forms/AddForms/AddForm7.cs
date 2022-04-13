using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Baltika_4.Forms.AddForms
{
    public partial class AddForm7 : Form
    {
        public static MainForm mainForm;

        public AddForm7(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
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
                        CommandText = "insert into ShipsType (TypeShip) " +
                        "values (@typeShip)"
                    };
                    command.Parameters.AddWithValue("@typeShip", textBox2.Text);
                    command.ExecuteNonQuery();
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
            Sql.ConnectToAccess(Sql.dataSets[7], Sql.adapters[7], mainForm.dataGridView1, Sql.selectQueries[7], Sql.tables[7]);
            foreach (TextBox textBox in Controls.OfType<TextBox>())
            {
                textBox.Text = "";
            }
            MessageBox.Show("Запись добавлена");
            //Notify.ShowNotify("Запись добавлена", Properties.Resources.Info);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}
