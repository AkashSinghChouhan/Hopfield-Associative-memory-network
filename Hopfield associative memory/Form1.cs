using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hopfield_associative_memory
{
    
    
    public partial class Form1 : Form
    {
        static int i = 0, j = 0;
        List<List<int>> vec;

        public Form1()
        {
            InitializeComponent();
            vec = new List<List<int>>();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename;
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                filename = openFileDialog1.FileName;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ++i;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("1 ");
            vec.ElementAt(i).Add(1);
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.AppendText("-1 ");
            vec.ElementAt(i).Add(-1);
        }
    }
}
