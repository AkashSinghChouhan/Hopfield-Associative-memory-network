using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Hopfield_associative_memory
{
    
    
    public partial class Form1 : Form
    {
        
        
        List<List<int>> vec;
        List<int> temp;
        List<int> testVec;
        int [,]weight;
        static int itr = 0;
        int[] temp_arr;

        public Form1()
        {
            
            vec = new List<List<int>>();
            temp = new List<int>();
            testVec = new List<int>();
            InitializeComponent();

            textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size * 1.2f);
            textBox2.Font = new Font(textBox2.Font.FontFamily, textBox2.Font.Size * 1.2f);
            textBox3.Font = new Font(textBox3.Font.FontFamily, textBox3.Font.Size * 1.2f);
            textBox1.Text = "Enter the Vectors here..";
            textBox1.AppendText(Environment.NewLine);
            textBox3.Text = "Enter the Test vector here: ";
            textBox3.AppendText(Environment.NewLine);
            

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }


        private void button3_Click(object sender, EventArgs e)
        {


            StringBuilder sb = new StringBuilder("");
            string data;
            
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {

                StreamReader sr = new StreamReader(openFileDialog1.FileName);

                sb.Append(sr.ReadToEnd());
                
                sr.Close();
            }
            data = sb.ToString();

            vec = new List<List<int>>();
            List<int> temp1 = new List<int>();
            for (int i = 0; i <data.Length; i++)
            {
                if (data.ElementAt(i) == '1') { temp1.Add(1); }

                else if (data.ElementAt(i) == '-') { temp1.Add(-1);++i; }

                else if (data.ElementAt(i) == '0') { temp1.Add(-1); }

                else if (data.ElementAt(i) == ']') { vec.Add(temp1); temp1 = new List<int>(); }

                

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            vec.Add(temp);
            if (vec[0].Count != temp.Count) {

                textBox1.AppendText(Environment.NewLine+" Unmatched Vector length");
            }
            else {
                

                textBox1.AppendText(Environment.NewLine);

                temp = new List<int>();
            }
            
            
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
            temp_arr = new int[vec[0].Count];
            //string separator = " , ";
            this.hopfield();
            StringBuilder sb = new StringBuilder("Weight matrix: "+Environment.NewLine);
            for (int y = 0; y < weight.GetLength(0); y++)
            {
                for (int z = 0; z < weight.GetLength(1); z++)
                {

                    sb.Append(weight[y,z].ToString()+" ");
                }
                sb.Append(Environment.NewLine);
            }
            textBox2.Text = sb.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox3.AppendText(" -1 ");
            testVec.Add(-1);

        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox3.AppendText(" 0 ");
            testVec.Add(0);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox3.AppendText(" 1 ");
            testVec.Add(1);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            List<int> a;
            int[] b = new int[testVec.Count];
            int[] arr_ls = new int[testVec.Count];
            testVec.CopyTo(arr_ls);

            for (int j = 0; j < testVec.Count; j++)
            {

                a = new List<int>();

                for (int k = 0; k < weight.GetLength(1); k++)
                {

                    if (j == k) { a.Add(0); }
                    else
                    {
                        a.Add(testVec.ElementAt(k) * weight[j, k]);

                    }
                    b.SetValue(a.Sum(), j);
                }

            }

            sign(ref b);

            if (matchVec(ref b, ref arr_ls)) {
                textBox3.AppendText(Environment.NewLine + "Vector converges to itself."+Environment.NewLine);
            }

            else {
                
                StringBuilder sb = new StringBuilder(Environment.NewLine+"Converging Pattern is :"+Environment.NewLine);
                for (int z = 0; z < weight.GetLength(1); z++)
                {
                    sb.Append(b[z].ToString() + " ");
                }
                textBox3.AppendText(sb.ToString());
            }
            testVec = new List<int>();
            textBox3.AppendText(Environment.NewLine);
        }

        public void sign(ref int[] a) {

            for (int i=0;i<a.Length;i++) {

                if (a.ElementAt(i)<0) {
                    a[i] = -1;
                }
                else {
                    a[i] = 1;
                }
            }
           
        }

        public bool matchVec(ref int[] b,ref int[] ls_temp ) {
            for (int x = 0; x < b.Length; x++) {

                if (b[x]!=ls_temp[x]) { return false; }
            }
            return true;
            
        }

        private void StabilityTest_Click(object sender, EventArgs e)
        {
            int c = 1;
            textBox2.AppendText(Environment.NewLine+"Testing Statbility of all input vectors..."+Environment.NewLine);
            
            int[] arr_ls;

            foreach (var ls in vec) {

                
                arr_ls = new int[ls.Count];
                ls.CopyTo(arr_ls);

                iterate(arr_ls, c);
               
                c++;
            }
        }

        public void iterate(int[] arr_ls, int c) {
            
            List<int> a;
            int[] b = new int[arr_ls.Length];
           

            for (int j = 0; j < arr_ls.Length; j++)
            {

                a = new List<int>();

                for (int k = 0; k < weight.GetLength(1); k++)
                {

                    if (j == k) { a.Add(0); }
                    else
                    {
                        a.Add(arr_ls.ElementAt(k) * weight[j, k]);

                    }
                    b.SetValue(a.Sum(), j);
                }

            }

            sign(ref b);
            if (matchVec(ref b, ref arr_ls)) { textBox2.AppendText(Environment.NewLine + "Iteration "+itr+" : Vector " + c.ToString() + " is stable.");
                itr = 0;
                Array.Clear(temp_arr,0,b.Length);
            }

            else
            {
                if (itr > 2 && matchVec(ref temp_arr, ref b)) {
                    textBox2.AppendText(Environment.NewLine+"Infinite iterative loop detected for Vector "+ c +" . Ending iteration!");
                    itr = 0;
                }

                else {
                    //temp_arr =new int[b.Length];
                    b.CopyTo(temp_arr,0);
                    textBox2.AppendText(Environment.NewLine + "Iteration " + itr + " : Vector " + c.ToString() + " is Unstable.");
                    ++itr;

                    StringBuilder sb = new StringBuilder(Environment.NewLine + "v"+c+" is :");
                    for (int z = 0; z < weight.GetLength(1); z++)
                    {
                        sb.Append(b[z].ToString() + " ");
                    }
                    textBox2.AppendText(sb.ToString()+Environment.NewLine);
                    iterate(b, c);

                }
                



            }


        }
    }
}
