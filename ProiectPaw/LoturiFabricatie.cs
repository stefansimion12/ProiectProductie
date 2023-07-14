using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProiectPaw
{
    public partial class LoturiFabricatie : Form, IComparable<LoturiFabricatie>
    {
        private SqlConnection c = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\simio\source\repos\ProiectPaw\ProiectPaw\Database1.mdf;Integrated Security = True");
        private DateTime dataFabricatie;
        private string numeFabricant;
        private string[] informatiiSuplimentare;
        BindingSource bindingSource = new BindingSource();

        public LoturiFabricatie(DateTime dataFabricatie, string numeFabricant, string[] informatiiSuplimentare)
        {
            this.DataFabricatie = dataFabricatie;
            this.NumeFabricant = numeFabricant;
            this.informatiiSuplimentare = informatiiSuplimentare;
        }
        public DateTime DataFabricatie { get => dataFabricatie; set => dataFabricatie = value; }
        public string NumeFabricant { get => numeFabricant; set => numeFabricant = value; }
        public string[] InformatiiSuplimentare { get => informatiiSuplimentare; set => informatiiSuplimentare = value; }

        public int CompareTo(LoturiFabricatie other)
        {
            if (this.dataFabricatie > other.dataFabricatie)
            {
                return 1;
            }
            else
                if (this.dataFabricatie < other.dataFabricatie)
            {
                return -1;
            }
            return 0;
        }
        public string this[int index]
        {
            get
            {
                return InformatiiSuplimentare[index];
            }
            set
            {
                InformatiiSuplimentare[index] = value;
            }
        }
        public LoturiFabricatie()
        {
            InitializeComponent();
        }

        private void LoturiFabricatie_Load(object sender, EventArgs e)
        {
            
        }
        private DateTime dataCurenta()
        {
            return DateTime.Now;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1.logat == true)
            {
                try
                {
                    c.Open();
                    string insert = "insert into LoturiFabricatie(DataFabricatie, NumeFabricant, InformatiiSuplimentare) values (@a, @b, @c)";
                    SqlCommand cmd = new SqlCommand(insert, c);
                    cmd.Parameters.AddWithValue("a", dataCurenta().ToShortDateString());
                    cmd.Parameters.AddWithValue("b", textBox1.Text);
                    cmd.Parameters.AddWithValue("c", textBox2.Text);
                    cmd.ExecuteNonQuery();
                    textBox1.Clear();
                    textBox2.Clear();
                    MessageBox.Show("Produs adaugat cu succes!");
                    c.Close();
                }
                catch
                {
                    c.Close();
                    MessageBox.Show("Datele introduse nu corespund!");
                }
            }
            else
            {
                MessageBox.Show("Trebuie sa fii logat ca sa continui!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
            try
            {
                c.Open();
                string select = "select * from LoturiFabricatie";
                SqlCommand cmd = new SqlCommand(select, c);
                SqlDataReader r = cmd.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(r);

                bindingSource.DataSource = dataTable;
                dataGridView1.DataSource = bindingSource;

                c.Close();
            }
            catch
            {
                c.Close();
                bindingSource.DataSource = null;
                MessageBox.Show("A aparut o eroare!");
            }
            //dataGridView1.Rows.Clear();
            //try
            //{
            //    c.Open();
            //    string select = "select * from LoturiFabricatie";
            //    SqlCommand cmd = new SqlCommand(select, c);
            //    SqlDataReader r = cmd.ExecuteReader();
            //    while (r.Read())
            //    {
            //        dataGridView1.Rows.Add(r[0].ToString(), r[1].ToString(), r[2].ToString(), r[3].ToString());
            //    }
            //    c.Close();
            //}
            //catch
            //{
            //    c.Close();
            //    MessageBox.Show("A aparut o eroare!");
            //}

        }
        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                textBox1.DoDragDrop(textBox1.Text, DragDropEffects.Copy);
            }
        }

        private void textBox2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void textBox2_DragDrop(object sender, DragEventArgs e)
        {
            string draggedText = (string)e.Data.GetData(DataFormats.Text);
            textBox2.Text = draggedText;
        }
    }
}
