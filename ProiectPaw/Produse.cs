using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace ProiectPaw
{
    
    public partial class Produse : Form, ICloneable, IConversieEuro
    {
        private SqlConnection c = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\simio\source\repos\ProiectPaw\ProiectPaw\Database1.mdf;Integrated Security = True");
        private string nume;
        private int cantitate;
        private float pret;
        public Produse()
        {
            InitializeComponent();
        }
        public Produse(string nume, int cantitate, float pret)
        {
            this.nume = nume;
            this.cantitate = cantitate;
            this.pret = pret;
        }

        public string Nume { get => nume; set => nume = value; }
        public int Cantitate { get => cantitate; set => cantitate = value; }
        public float Pret { get => pret; set => pret = value; }
        public static Produse operator +(Produse p, float pret)
        {
            if (p != null)
            {
                p.pret += pret;
            }
            return p;
        }

        public static bool operator >(Produse p1, Produse p2)
        {
            if (p1 != null && p2 != null)
            {
                if (p1.pret * p1.Cantitate > p2.pret * p2.Cantitate)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool operator <(Produse p1, Produse p2)
        {
            if (p1 != null && p2 != null)
            {
                if (p1.pret * p1.Cantitate < p2.pret * p2.Cantitate)
                {
                    return true;
                }
            }
            return false;
        }

        private string conversieMajuscula(string text)
        {
            return string.Concat(text[0].ToString().ToUpper(), text.Substring(1));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(Form1.logat == true)
            {
                try
                {
                    c.Open();
                    string insert = "insert into Produs(Nume, Cantitate, Pret) values (@a, @b, @c)";
                    SqlCommand cmd = new SqlCommand(insert, c);
                    cmd.Parameters.AddWithValue("a", conversieMajuscula(textBox1.Text));
                    cmd.Parameters.AddWithValue("b", Convert.ToInt32(textBox2.Text));
                    pret = float.Parse(textBox3.Text);
                    cmd.Parameters.AddWithValue("c", ConversieEuro());
                    cmd.ExecuteNonQuery();
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
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
        private void pretTotal()
        {
            try
            {
                c.Open();
                string select = "select SUM(pret) from Produs";
                SqlCommand cmd = new SqlCommand(select, c);
                SqlDataReader r = cmd.ExecuteReader();
                if(r.Read())
                {
                    label4.Text = "Pret total: " + r[0].ToString();
                }
                c.Close();
            }
            catch
            {
                c.Close();
                MessageBox.Show("A aparut o eroare!");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            chart1.Series[0].Points.Clear();
            try
            {
                c.Open();
                string select = "select * from Produs";
                SqlCommand cmd = new SqlCommand(select, c);
                SqlDataReader r = cmd.ExecuteReader();
                while(r.Read())
                {
                    chart1.Series[0].Points.AddXY(r[1].ToString(), r[3].ToString());
                    dataGridView1.Rows.Add(r[0].ToString(), 
                    r[1].ToString(), r[2].ToString(), r[3].ToString());
                }
                c.Close();
                pretTotal();
            }
            catch
            {
                c.Close();
                MessageBox.Show("A aparut o eroare!");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string cale = "";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                cale = saveFileDialog1.FileName;
            }
            try
            {
                using (StreamWriter f = new StreamWriter(cale))
                {
                    f.WriteLine("Produse:\n");
                    c.Open();
                    string select = "select * from Produs";
                    SqlCommand cmd = new SqlCommand(select, c);
                    SqlDataReader r = cmd.ExecuteReader();
                    while (r.Read())
                    {
                        f.WriteLine("ID: " + r[0].ToString() + "\nNume: " + r[1].ToString() + "\nCantitate: " + r[2].ToString() + "\nPret: " + r[3].ToString() + "\n");
                    }
                    c.Close();
                }
                MessageBox.Show("Datele au fost salvate!");
            }
            catch
            {
                c.Close();
                MessageBox.Show("A aparut o eroare!");
            }
        }
        public object Clone()
        {
            return MemberwiseClone();
        }

        public double ConversieEuro()
        {
            return pret / 4.97;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            printDocument1.Print();   
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Form1.logat == true)
            {
                try
                {
                    c.Open();
                    string delete = "delete from Produs where id = @id";
                    SqlCommand cmd = new SqlCommand(delete, c);
                    cmd.Parameters.AddWithValue("id", textBox4.Text);
                    cmd.ExecuteNonQuery();
                    c.Close();
                    button2_Click(sender, e);
                }
                catch
                {
                    c.Close();
                    MessageBox.Show("Stergerea nu a putut fi efectuata!");
                }
            }
            else
            {
                MessageBox.Show("Trebuie sa fii logat ca sa continui!");
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void Produse_Load(object sender, EventArgs e)
        {

        }


    }
}
