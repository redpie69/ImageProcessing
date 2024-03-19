using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IPLibrary;

namespace IPForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[,] matrix = new int[4, 4] { { 1,2,1,0 }, { -1,-2,3,-4 }, { 0,0,5,6 },{ 1,1,6,7} };
            double result=IP.Determinant(matrix);
            MessageBox.Show(result.ToString());
        }
    }
}
