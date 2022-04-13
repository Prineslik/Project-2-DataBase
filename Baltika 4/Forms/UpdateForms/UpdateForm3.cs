using Baltika_4.Classes;
using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;

namespace Baltika_4.Forms.UpdateForms
{
    public partial class UpdateForm3 : Form
    {
        private readonly MainForm mainForm;

        public UpdateForm3(MainForm owner)
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
                    if (textBox2.Text == "")
                    {
                        MessageBox.Show("Текстовое поле должно быть заполненым");
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
                                CommandText = "update [ConsSender] set [NameCS] = @nameCS where [ID] = " + mainForm.dataGridView1.CurrentRow.Cells[0].Value
                            };
                            command.Parameters.AddWithValue("@nameCS", textBox2.Text);
                            Sql.adapters[3].UpdateCommand = command;
                            Sql.adapters[3].UpdateCommand.ExecuteNonQuery();
                            connection.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            //Notify.ShowNotify(ex.Message, Properties.Resources.Info);

                            return;
                        }
                    }
                    Sql.ClearTable(mainForm.dataGridView1);
                    Sql.ConnectToAccess(Sql.dataSets[3], Sql.adapters[3], mainForm.dataGridView1, Sql.selectQueries[3], Sql.tables[3]);
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

        private void UpdateForm3_Load(object sender, EventArgs e)
        {
            (Controls["textBox2"] as TextBox).Text = mainForm.dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
    }
}
