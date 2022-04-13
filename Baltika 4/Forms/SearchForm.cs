using Baltika_4.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.OleDb;
using System.Linq;

namespace Baltika_4.Forms
{
    public partial class SearchForm : Form
    {
        public static MainForm mainForm;

        public SearchForm(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();
            BackBtn.Click += (s, e) => { Close(); };
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridView grid = mainForm.dataGridView1;

            grid.ClearSelection();
            for (int i = 0; i < grid.Rows.Count - 1; i++)
            {
                for (int j = 0; j < grid.Columns.Count - 1; j++)
                {
                    if (grid.Rows[i].Cells[j].Value != null && grid.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                    {
                        grid.Rows[i].Cells[j].Selected = true;
                    }
                }
            }
        }
    }
}
