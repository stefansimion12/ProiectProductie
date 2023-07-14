using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ProiectPaw
{
    public partial class Form1 : Form
    {
        string nume, parola;
        public static bool logat = false;
        public Form1()
        {
            InitializeComponent();
        }

  
        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Produse p = new Produse();
            p.Show(); // am creat o instanta a clasei produs pe care am afisat-o
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            LoturiFabricatie lf = new LoturiFabricatie();
            lf.Show();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            FiseConsum fc = new FiseConsum();
            fc.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("XMLFile1.xml");
            XmlNodeList nod = xmlDoc.SelectNodes("/utilizatori/utilizator");
            foreach(XmlNode n in nod)
            {
                nume = n.SelectSingleNode("nume").InnerText;
                parola = n.SelectSingleNode("parola").InnerText; 
                if (textBox1.Text == nume && textBox2.Text == parola)
                {
                    logat = true;
                    MessageBox.Show("Bine ai venit, " + nume + "!");
                    break;
                }
            }
            if(logat == false)
            {
                MessageBox.Show("Nume sau parola gresita!");
            }
            

        }
    }
}
