using Baltika_4.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Baltika_4.Forms
{
    public partial class DeleteForm : Form
    {
        public static MainForm mainForm;

        public DeleteForm(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => Close();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            if (!char.IsDigit(number) && number != 8)
            {
                e.Handled = true;
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Поле не может быть пустым");
                    return;
                }
                int rowIndex = int.Parse(textBox1.Text);
                int rowToDelete = -1;
                foreach (DataGridViewRow row in mainForm.dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString() == rowIndex.ToString())
                    {
                        rowToDelete = row.Index;
                        break;
                    }
                }
                if (rowToDelete == -1)
                {
                    MessageBox.Show("Такой записи не существует");
                    return;
                }
                SQLDelete.DeleteRecord(Sql.deleteQueries[MainForm.indexer], rowIndex, Sql.adapters[MainForm.indexer], Sql.dataSets[MainForm.indexer]);
                mainForm.dataGridView1.Rows.RemoveAt(rowToDelete);
                textBox1.Text = "";
                MessageBox.Show("Запись удалена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
