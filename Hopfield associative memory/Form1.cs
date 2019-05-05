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
        
        static int i = 0;
        List<List<int>> vec;
        List<int> temp;
        int [,]weight;

        public Form1()
        {
            vec = new List<List<int>>();
            temp = new List<int>();
            
            InitializeComponent();
            
            
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
            vec.Add(temp);
            ++i;
            
            textBox1.Clear();
            //temp.Clear();
            temp = new List<int>(); 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(" 1 ");
            temp.Add(1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(" -1 ");
            temp.Add(-1);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(" 0 ");
            temp.Add(-1);
        }
        public void hopfield() {
            weight = new int[vec[0].Count,vec[0].Count];
            for (int x = 0; x < vec.Count; x++)
            {
                for (int y = 0; y < weight.GetLength(0); y++)
                {
                    for (int z=0 ; z < weight.GetLength(1); z++) {

                        if (y == z) { weight[y, z] = 0; }
                        else { weight[y, z] += (vec[x][y] * vec[x][z]); }
                    }
                }
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //string separator = " , ";
            this.hopfield();
            StringBuilder sb = new StringBuilder("Weight: ");
            for (int y = 0; y < weight.GetLength(0); y++)
            {
                for (int z = 0; z < weight.GetLength(1); z++)
                {

                    sb.Append(weight[y,z].ToString()+" ");
                }
                sb.Append("\n");
            }
            textBox2.Text = sb.ToString();
        }
    }
}
