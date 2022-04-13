using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baltika_4.Classes;

namespace Baltika_4.Forms
{
    public partial class Report : Form
    {
        private readonly MainForm mainForm;
        private Label parentL;

        public Report(MainForm owner)
        {
            mainForm = owner;
            InitializeComponent();

            BackBtn.Click += (s, e) => { Close(); };
            BackBtn.Location = new Point(this.Size.Width-55,-1);
        }

        private void Report_Load(object sender, EventArgs e)
        {
            try
            {
                DataGridView grid = mainForm.dataGridView1;

                int indexT = MainForm.indexer;
                int indexS = Convert.ToInt32(grid.CurrentRow.Index);//индекс судна в таблице
                int idS = Convert.ToInt32(grid.CurrentRow.Cells[0].Value);//ИД судна в БД

                DataSet dataSet = new DataSet();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
                Sql.ClearTable(dataGridView1);
                Sql.ConnectToAccess(dataSet, dataAdapter, dataGridView1, Sql.selectQueries[5], Sql.tables[5]);

                string report = "";

                label2.Text += " " + dataGridView1.Rows[indexS].Cells[1].Value.ToString();
                label3.Text += " " + dataGridView1.Rows[indexS].Cells[2].Value.ToString();
                label4.Text += " " + FindId(7, dataGridView1.Rows[indexS].Cells[3].Value.ToString());
                label5.Text += " " + dataGridView1.Rows[indexS].Cells[4].Value.ToString();
                label6.Text += " " + dataGridView1.Rows[indexS].Cells[5].Value.ToString();
                label7.Text += " " + dataGridView1.Rows[indexS].Cells[6].Value.ToString().Substring(0, 10);
                label8.Text += " " + FindId(4, dataGridView1.Rows[indexS].Cells[7].Value.ToString());

                dataSet = new DataSet();
                dataAdapter = new OleDbDataAdapter();
                Sql.ClearTable(dataGridView1);
                Sql.ConnectToAccess(dataSet, dataAdapter, dataGridView1, Sql.selectQueries[6], Sql.tables[6]);

                string relat = "";
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == idS)
                    {
                        relat += dataGridView1.Rows[i].Cells[2].Value.ToString() + "/";
                    }
                }
                string[] idBatches = relat.Substring(0, relat.Length - 1).Split('/');
                lab8.Text = idBatches.Length.ToString();//кол-во партий

                dataSet = new DataSet();
                dataAdapter = new OleDbDataAdapter();
                Sql.ClearTable(dataGridView1);
                Sql.ConnectToAccess(dataSet, dataAdapter, dataGridView1, Sql.selectQueries[0], Sql.tables[0]);

                parentL = label9;
                for (int i = 0; i < idBatches.Length; i++)
                {
                    AddBatch(idBatches[i]);
                }

                dataSet = new DataSet();
                dataAdapter = new OleDbDataAdapter();
                Sql.ClearTable(dataGridView1);
                Sql.ConnectToAccess(dataSet, dataAdapter, dataGridView1, Sql.selectQueries[2], Sql.tables[2]);

                relat = "";
                for (int j = 0; j < idBatches.Length; j++)
                {
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        if (dataGridView1.Rows[i].Cells[2].Value.ToString() == idBatches[j])
                        {
                            relat += dataGridView1.Rows[i].Cells[1].Value.ToString() + "/";
                        }
                    }
                }
                string[] idCargoes = relat.Substring(0, relat.Length - 1).Split('/');
                Label labelCarg = new Label() { Text = "Кол-во грузов = " + idCargoes.Length, Location = new Point(parentL.Location.X, parentL.Location.Y + 28), Font = parentL.Font, AutoSize = true };
                Controls.Add(labelCarg);

                dataSet = new DataSet();
                dataAdapter = new OleDbDataAdapter();
                Sql.ClearTable(dataGridView1);
                Sql.ConnectToAccess(dataSet, dataAdapter, dataGridView1, Sql.selectQueries[1], Sql.tables[1]);

                for (int i = 0; i < idCargoes.Length; i++)
                {
                    AddCargo(idCargoes[i]);
                }

                dataGridView1.Hide();
                dataGridView2.Hide();
            }
            catch (Exception)
            {
                MessageBox.Show("У данного судна отсутсвуют партии либо груз");
                dataGridView1.Hide();
                dataGridView2.Hide();
            }

            
        }

        private string FindId( int idTable, string findObj)
        {
            DataSet dataSet = new DataSet();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            
            Sql.ClearTable(dataGridView2);
            Sql.ConnectToAccess(dataSet, dataAdapter, dataGridView2, Sql.selectQueries[idTable], Sql.tables[idTable]);

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[0].Value.ToString() == findObj)
                {
                    return dataGridView2.Rows[i].Cells[1].Value.ToString();
                }
            }

            dataSet.Dispose();
            dataAdapter.Dispose();
            return "<Ошибка: поле не найдено>";
        }

        private void AddBatch(string idB)
        {
            Label customV = new Label() { Text = "Таможенный номер: ", Location = new Point(parentL.Location.X,parentL.Location.Y + 50), Font = parentL.Font, AutoSize = true};
            Label DepartD = new Label() { Text = "Дата отправки: ", Location = new Point(parentL.Location.X, customV.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label ArriveD = new Label() { Text = "Дата прибытия: ", Location = new Point(parentL.Location.X, DepartD.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label Origin = new Label() { Text = "Пункт отправления: ", Location = new Point(parentL.Location.X, ArriveD.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label Destin = new Label() { Text = "Пункт назначения: ", Location = new Point(parentL.Location.X, Origin.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label CustomC = new Label() { Text = "Таможенная декларация: ", Location = new Point(parentL.Location.X, Destin.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == idB)
                {
                    customV.Text += dataGridView1.Rows[i].Cells[1].Value.ToString();
                    DepartD.Text += dataGridView1.Rows[i].Cells[2].Value.ToString().Substring(0,10);
                    ArriveD.Text += dataGridView1.Rows[i].Cells[3].Value.ToString().Substring(0, 10);
                    Origin.Text += dataGridView1.Rows[i].Cells[4].Value.ToString();
                    Destin.Text += dataGridView1.Rows[i].Cells[5].Value.ToString();
                    CustomC.Text += dataGridView1.Rows[i].Cells[6].Value.ToString();
                }
            }
            parentL = CustomC;
            Controls.Add(customV);
            Controls.Add(DepartD);
            Controls.Add(ArriveD);
            Controls.Add(Origin);
            Controls.Add(Destin);
            Controls.Add(CustomC);
            //this.Size = new Size(this.Size.Width, parentL.Location.Y + 100);
        }

        private void AddCargo(string idC)
        {
            Label sender = new Label() { Text = "Отправитель: ", Location = new Point(parentL.Location.X, parentL.Location.Y + 70), Font = parentL.Font, AutoSize = true };
            Label shipment = new Label() { Text = "Название груза: ", Location = new Point(parentL.Location.X, sender.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label decValue = new Label() { Text = "Заявленная величина груза: ", Location = new Point(parentL.Location.X, shipment.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label unit = new Label() { Text = "Единица измерения: ", Location = new Point(parentL.Location.X, decValue.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label insValue = new Label() { Text = "Застрахованная величина груза: ", Location = new Point(parentL.Location.X, unit.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label innS = new Label() { Text = "ИНН отправителя: ", Location = new Point(parentL.Location.X, insValue.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label bankS = new Label() { Text = "Банк отправителя: ", Location = new Point(parentL.Location.X, innS.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label adrS = new Label() { Text = "Адрес отправителя: ", Location = new Point(parentL.Location.X, bankS.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label cons = new Label() { Text = "Получатель: ", Location = new Point(parentL.Location.X, adrS.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label innC = new Label() { Text = "ИНН получателя: ", Location = new Point(parentL.Location.X, cons.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label bankC = new Label() { Text = "Банк получателя: ", Location = new Point(parentL.Location.X, innC.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label adrC = new Label() { Text = "Адрес получателя: ", Location = new Point(parentL.Location.X, bankC.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label comm = new Label() { Text = "Примечание: ", Location = new Point(parentL.Location.X, adrC.Location.Y + 28), Font = parentL.Font, AutoSize = true };
            Label num = new Label() { Text = "Номер в партии: ", Location = new Point(parentL.Location.X, comm.Location.Y + 28), Font = parentL.Font, AutoSize = true };

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() == idC)
                {
                    sender.Text += FindId(3,dataGridView1.Rows[i].Cells[1].Value.ToString());
                    shipment.Text += dataGridView1.Rows[i].Cells[2].Value.ToString();
                    decValue.Text += dataGridView1.Rows[i].Cells[3].Value.ToString();
                    unit.Text += dataGridView1.Rows[i].Cells[4].Value.ToString();
                    insValue.Text += dataGridView1.Rows[i].Cells[5].Value.ToString();
                    innS.Text += dataGridView1.Rows[i].Cells[6].Value.ToString();
                    bankS.Text += dataGridView1.Rows[i].Cells[7].Value.ToString();
                    adrS.Text += dataGridView1.Rows[i].Cells[8].Value.ToString();
                    cons.Text += FindId(3,dataGridView1.Rows[i].Cells[9].Value.ToString());
                    innC.Text += dataGridView1.Rows[i].Cells[10].Value.ToString();
                    bankC.Text += dataGridView1.Rows[i].Cells[11].Value.ToString();
                    adrC.Text += dataGridView1.Rows[i].Cells[12].Value.ToString();
                    comm.Text += dataGridView1.Rows[i].Cells[13].Value.ToString();
                    num.Text += dataGridView1.Rows[i].Cells[14].Value.ToString();
                }
            }

            parentL = num;
            Controls.Add(sender);
            Controls.Add(shipment);
            Controls.Add(decValue);
            Controls.Add(unit);
            Controls.Add(insValue);
            Controls.Add(innS);
            Controls.Add(bankS);
            Controls.Add(adrS);
            Controls.Add(cons);
            Controls.Add(innC);
            Controls.Add(bankC);
            Controls.Add(adrC);
            Controls.Add(comm);
            Controls.Add(num);
        }
    }
}
