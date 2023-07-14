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
    public partial class FiseConsum : Form
    {
        BindingSource bindingSource = new BindingSource();
        private string numeProdus;
        private int cantitateConsumata;
        private float pretProdus;
        private DateTime dataEliberare;
        private SqlConnection c = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\simio\source\repos\ProiectPaw\ProiectPaw\Database1.mdf;Integrated Security = True");
        public FiseConsum()
        {
            InitializeComponent();
        }
        public FiseConsum(string numeProdus, int cantitateConsumata, DateTime dataEliberare, float pretProdus)
        {
            this.numeProdus = numeProdus;
            this.cantitateConsumata = cantitateConsumata;
            this.dataEliberare = dataEliberare;
            this.pretProdus = pretProdus;
        }
        private DateTime dataCurenta()
        {
            return DateTime.Now;
        }

        public string NumeProdus { get => numeProdus; set => numeProdus = value; }
        public int CantitateConsumata { get => cantitateConsumata; set => cantitateConsumata = value; }
        public DateTime DataEliberare { get => dataEliberare; set => dataEliberare = value; }
        public float PretProdus { get => pretProdus; set => pretProdus = value; }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Form1.logat == true)
            {
                try
                {
                    c.Open();
                    string insert = "insert into FiseConsum(NumeProdus, CantitateConsumata, PretProdus, DataEliberare) values (@a, @b, @c, @d)";
                    SqlCommand cmd = new SqlCommand(insert, c);
                    cmd.Parameters.AddWithValue("a", textBox1.Text);
                    cmd.Parameters.AddWithValue("b", Convert.ToInt32(textBox2.Text));
                    cmd.Parameters.AddWithValue("c", Convert.ToDecimal(textBox3.Text));
                    cmd.Parameters.AddWithValue("d", dataCurenta().ToShortDateString());
                    cmd.ExecuteNonQuery();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    MessageBox.Show("Produs adaugat cu succes!");
                    c.Close();
                }
                catch(Exception ex)
                {
                    c.Close();
                    MessageBox.Show("Datele introduse nu corespund!" + ex.Message);
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
                string select = "select * from FiseConsum";
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
        }
    }
    
}

