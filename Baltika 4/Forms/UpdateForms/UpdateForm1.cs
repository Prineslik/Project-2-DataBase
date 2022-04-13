using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Baltika_4.Forms.UpdateForms
{
    public partial class UpdateForm1 : Form
    {
        private readonly MainForm mainForm;

        public UpdateForm1(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private void UpdateForm1_Load(object sender, EventArgs e)
        {
            for (int i = 2; i < 16; i++)
            {
                (Controls["textBox" + i.ToString()] as TextBox).Text = mainForm.dataGridView1.CurrentRow.Cells[i - 1].Value.ToString();
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == ""
                    || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "" || textBox10.Text == "" || textBox11.Text == ""
                    || textBox12.Text == "" || textBox13.Text == "" || textBox14.Text == "" || textBox15.Text == "")
                {
                    MessageBox.Show("Текстовые поля должны быть заполнены");
                    //Notify.ShowNotify("Текстовые поля должны быть заполнены", Properties.Resources.Info);

                    return;
                }
                if (textBox7.Text.Length < 10 || textBox11.Text.Length < 10)
                {
                    MessageBox.Show("ИНН должен состоять из 10 символов");

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
                            CommandText = "update [Cargoes] set [SenderID] = @senderID, [Shipment] = @shipment, [DeclareValue] = @declareValue, [Unit] = @unit, [InsureValue] = @insureValue, " +
                            "[INNSender] = @innSender, [BankSender] = @bankSender, [AddressSender] = @addressSender, [ConsigneeID] = @consigneeID, [INNConsignee] = @innConsignee, [BankConsignee] = @bankConsignee, [AddressConsignee] = @addressConsignee, [Comment] = @comment, [Number] = @number where [CargoID] = " + mainForm.dataGridView1.CurrentRow.Cells[0].Value
                        };
                        command.Parameters.AddWithValue("@senderID", Convert.ToInt32(textBox2.Text));
                        command.Parameters.AddWithValue("@shipment", textBox3.Text);
                        command.Parameters.AddWithValue("@declareValue", Convert.ToInt32(textBox4.Text));
                        command.Parameters.AddWithValue("@unit", textBox5.Text);
                        command.Parameters.AddWithValue("@insureValue", Convert.ToInt32(textBox6.Text));
                        command.Parameters.AddWithValue("@innSender", Convert.ToInt32(textBox7.Text));
                        command.Parameters.AddWithValue("@bankSender", textBox8.Text);
                        command.Parameters.AddWithValue("@addressSender", textBox9.Text);
                        command.Parameters.AddWithValue("@consigneeID", Convert.ToInt32(textBox10.Text));
                        command.Parameters.AddWithValue("@innConsignee", Convert.ToInt32(textBox11.Text));
                        command.Parameters.AddWithValue("@bankConsignee", textBox12.Text);
                        command.Parameters.AddWithValue("@addressConsignee", textBox13.Text);
                        command.Parameters.AddWithValue("@comment", textBox14.Text);
                        command.Parameters.AddWithValue("@number", textBox15.Text);

                        Sql.adapters[1].UpdateCommand = command;
                        Sql.adapters[1].UpdateCommand.ExecuteNonQuery();
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
                Sql.ConnectToAccess(Sql.dataSets[1], Sql.adapters[1], mainForm.dataGridView1, Sql.selectQueries[1], Sql.tables[1]);
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

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void textBox15_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }
    }
}