using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Baltika_4.Forms.UpdateForms
{
    public partial class UpdateForm7 : Form
    {
        private readonly MainForm mainForm;

        public UpdateForm7(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private void UpdateForm7_Load(object sender, EventArgs e)
        {
            (Controls["textBox2"] as TextBox).Text = mainForm.dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
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
                            CommandText = "update [ShipsType] set [TypeShip] = @typeShip where [TypeShipID] = " + mainForm.dataGridView1.CurrentRow.Cells[0].Value
                        };
                        command.Parameters.AddWithValue("@typeShip", textBox2.Text);

                        Sql.adapters[7].UpdateCommand = command;
                        Sql.adapters[7].UpdateCommand.ExecuteNonQuery();
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
}
