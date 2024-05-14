using System;
using System.Windows.Forms;

namespace IPForm
{
    public partial class MatrixGrid : UserControl
    {
        private double[,] Matrix { get; set; }
        public MatrixGrid()
        {
            InitializeComponent();
        }

        public void CreateMatrix(int size)
        {
            Matrix = new double[size, size];
            panel1.Controls.Clear();
            UpdateSize(size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    TextBox tb = new TextBox
                    {
                        Location = new System.Drawing.Point(j * 25, i * 20),
                        Name = "textBox" + i + j,
                        Size = new System.Drawing.Size(25, 20),
                        TabIndex = 1,
                        Text = "1"
                    };
                    panel1.Controls.Add(tb);
                }
            }
            panel1.Refresh();
        }

        public double[,] GetMatrix()
        {
            // validate the matrix values
            int x = Matrix.GetLength(0);
            int y = Matrix.GetLength(1);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (!double.TryParse(panel1.Controls["textBox" + i + j].Text, out Matrix[i, j]))
                    {
                        throw new Exception("Lütfen sayısal değerler giriniz.");
                    }
                }
            }
            return Matrix;
        }

        private void UpdateSize(int size)
        {
            // update the controls height and width
            Width = size * 25 + 10;
            Height = size * 25 + 10;
        }
    }
}
