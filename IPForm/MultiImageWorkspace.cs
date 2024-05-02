using IPLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace IPForm
{
    public partial class MultiImageWorkspace : UserControl
    {
        public Image ImageA { get; set; }
        public Image ImageB { get; set; }
        public Image ImageDisplay { get; set; }

        public MultiImageWorkspace()
        {
            InitializeComponent();
            _ = chartPic.Series.Add("Gray");
            chartPic.Series["Gray"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chartPic.ChartAreas[0].AxisY.Maximum = 1000;
            chartPic.ChartAreas[0].AxisX.Minimum = 0;
            SetControlsEnabledTo(false);
        }

        public void SetControlsEnabledTo(bool val)
        {
            groupBoxOps.Enabled = val;
            buttonPicSave.Enabled = val;
        }

        private bool ImagesValid()
        {
            return (ImageA != null && ImageB != null);
        }

        private void SetImage(Image image)
        {
            ImageDisplay = image;
            pictureBox.Image = ImageDisplay;
            RefreshWorkspace();
        }

        public void TransferImages(Image imageA, Image imageB)
        {
            if (imageA == null || imageB == null)
            {
                return;
            }

            SetControlsEnabledTo(false);

            ImageA = (Image)imageA.Clone();
            ImageB = (Image)imageB.Clone();

            if (!ImagesValid())
                return;

            SetControlsEnabledTo(true);
        }

        public void RefreshWorkspace()
        {
            drawGrayHistogram();
            pictureBox.Refresh();
        }

        private void buttonAdd_Click(object sender, System.EventArgs e)
        {
            Operation(ArithmeticOperations.Add);
        }

        private void Operation(ArithmeticOperations operation)
        {
            if (!ImagesValid())
            {
                return;
            }
            try
            {
                Bitmap result = IP.ArithmeticOperation((Bitmap)ImageA, (Bitmap)ImageB, operation);
                SetImage(result);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
        }

        private void drawGrayHistogram()
        {
            // get the count of each gray level
            int[] grayCount = new int[256];
            for (int i = 0; i < ImageDisplay.Width; i++)
            {
                for (int j = 0; j < ImageDisplay.Height; j++)
                {
                    Color color = ((Bitmap)ImageDisplay).GetPixel(i, j);
                    int gray = (color.R + color.G + color.B) / 3;
                    grayCount[gray]++;
                }
            }
            // draw the histogram data to chartPic
            chartPic.Series["Gray"].Points.Clear();
            for (int i = 0; i < 256; i++)
            {
                _ = chartPic.Series["Gray"].Points.AddXY(i, grayCount[i]);
            }
        }

        private void buttonSub_Click(object sender, System.EventArgs e)
        {
            Operation(ArithmeticOperations.Subtract);
        }

        private void buttonMult_Click(object sender, System.EventArgs e)
        {
            Operation(ArithmeticOperations.Multiply);
        }

        private void buttonDiv_Click(object sender, System.EventArgs e)
        {
            Operation(ArithmeticOperations.Divide);
        }

        // opens the save dialog for Image
        private void buttonPicSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "JPEG Image|*.jpg|Bitmap Image|*.bmp|PNG Image|*.png"
                };
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = sfd.FileName;
                    ImageDisplay.Save(fileName);
                }

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
        }

    }
}
