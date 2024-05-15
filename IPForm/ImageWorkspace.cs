using IPLibrary;
using System;
using System.Drawing;
using System.Windows.Forms;
namespace IPForm
{
    public partial class ImageWorkspace : UserControl
    {
        public string ImageName { get; set; }
        private Image ResetImage { get; set; }
        private bool isReset { get; set; }
        public Image Image { get; set; }
        public ImageWorkspace()
        {
            InitializeComponent();
            _ = chartPic.Series.Add("Gray");
            chartPic.Series["Gray"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chartPic.ChartAreas[0].AxisY.Maximum = 1000;
            chartPic.ChartAreas[0].AxisX.Minimum = 0;
            SetControlsEnabledTo(false);
            buttonPicLoad.Enabled = true;
            comboBoxStructureElements.DataSource = Enum.GetValues(typeof(MorphologicalStructuringElements));
        }

        public void SetImage(Image image)
        {
            Image = image;
            SetCropRange();

            pictureBox.Image = Image;

            RefreshWorkspace();
        }

        public void SetCropRange()
        {
            numUDCropLeft.Minimum = 1;
            numUDCropRight.Minimum = 1;
            numUDCropTop.Minimum = 1;
            numUDCropBottom.Minimum = 1;

            numUDCropLeft.Maximum = Image.Width - 1;
            numUDCropRight.Maximum = Image.Width - 1;
            numUDCropRight.Value = Image.Width - 1;

            numUDCropTop.Maximum = Image.Height - 1;
            numUDCropTop.Value = Image.Height - 1;
            numUDCropBottom.Maximum = Image.Height - 1;
        }
        public void RefreshWorkspace()
        {
            drawGrayHistogram();
            pictureBox.Refresh();
        }

        private void buttonPicLoad_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Image Files(*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ImageName = ofd.FileName;
                Image = Image.FromFile(ImageName);
                ResetImage = (Image)Image.Clone();

                SetImage(Image);
                SetControlsEnabledTo(true);
            }


        }
        private void SetControlsEnabledTo(bool val)
        {
            buttonPicLoad.Enabled = val;
            buttonPicSave.Enabled = val;
            buttonPicReset.Enabled = val;
            groupBoxColorSpaceOps.Enabled = val;
            groupBoxContOps.Enabled = val;
            groupBoxConvOps.Enabled = val;
            groupBoxGeoOps.Enabled = val;
            groupBoxEdgeDetectOps.Enabled = val;
            groupBoxFilterOps.Enabled = val;
            groupBoxHistOps.Enabled = val;
            groupBoxTreshOps.Enabled = val;
            groupBoxMorfOps.Enabled = val;
            groupBoxNoiseOps.Enabled = val;
        }
        private void changeReset()
        {
            isReset = !isReset;
            buttonPicReset.Enabled = isReset;
        }
        private void buttonPicReset_Click(object sender, System.EventArgs e)
        {
            Image = (Image)ResetImage.Clone();
            changeReset();
            SetImage(Image);
        }

        private void drawGrayHistogram()
        {
            // get the count of each gray level
            int[] grayCount = new int[256];
            for (int i = 0; i < Image.Width; i++)
            {
                for (int j = 0; j < Image.Height; j++)
                {
                    Color color = ((Bitmap)Image).GetPixel(i, j);
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

        private void buttonRotateRight_Click(object sender, System.EventArgs e)
        {
            SetControlsEnabledTo(false);
            decimal angle = 180 / numUDRotate.Value;
            IP.Rotate((Bitmap)Image, Math.PI / (double)angle, System.Drawing.Drawing2D.InterpolationMode.Bilinear);
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonRotateLeft_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            decimal angle = 180 / numUDRotate.Value;
            IP.Rotate((Bitmap)Image, -Math.PI / (double)angle, System.Drawing.Drawing2D.InterpolationMode.Bilinear);
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonCrop_Click(object sender, EventArgs e)
        {
            // create a new Point x is numUDCropLeft.Value y is numUDCropTop.Value
            Point leftTop = new Point((int)numUDCropLeft.Value, (int)numUDCropTop.Value);
            Point rightBottom = new Point((int)numUDCropRight.Value, (int)numUDCropBottom.Value);
            // validate if rightBottom.X is smaller than leftTop.X or if rightBottom.Y is bigger smaller than leftTop.Y
            if (leftTop.X > rightBottom.X || leftTop.Y < rightBottom.Y)
            {
                _ = MessageBox.Show("Unvalid crop values");
                return;
            }

            SetControlsEnabledTo(false);
            try
            {
                Bitmap cropped = IP.Crop((Bitmap)Image, leftTop, rightBottom);
                SetImage(cropped);

            }
            catch (Exception ex)
            {
                // return the error message
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonZoom_Click(object sender, EventArgs e)
        {
            decimal value = numUDZoom.Value / 100;
            int width = (int)(value * Image.Width);
            int height = (int)(value * Image.Height);
            SetControlsEnabledTo(false);
            try
            {
                _ = MessageBox.Show($"{value}");
                Bitmap zoomedImage = IP.Scaling((Bitmap)Image, System.Drawing.Drawing2D.InterpolationMode.Bilinear, width, height);
                SetImage(zoomedImage);
            }
            catch (Exception ex)
            {
                // return the error message
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonHistStretch_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                IP.HistogramGerme((Bitmap)Image, (int)numUDHistMax.Value, (int)numUDHistMin.Value);
            }
            catch (Exception ex)
            {
                // return the error message
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonHistExtend_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                IP.HistogramGenisletme((Bitmap)Image);
            }
            catch (Exception ex)
            {
                // return the error message
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonContrast_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            double val = 100 / (double)numUDContrast.Value;
            try
            {
                IP.Constrast((Bitmap)Image, val);
            }
            catch (Exception ex)
            {
                // return the error message
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonBrightnessIncrease_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                IP.Brightness((Bitmap)Image, (int)numUDBrightness.Value);
            }
            catch (Exception ex)
            {
                // return the error message
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonBrightnessDecrease_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                IP.Brightness((Bitmap)Image, -(int)numUDBrightness.Value);
            }
            catch (Exception ex)
            {
                // return the error message
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
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
                    Image.Save(fileName);
                }

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
        }

        private void buttonColorSpaceRGB2Gray_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                IP.RGB2GrayScale((Bitmap)Image);
            }
            catch (Exception ex)
            {
                // return the error message
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonFilterMedian_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                IP.MedianFilter((Bitmap)Image, (int)numUDFilterMatrixSize.Value);
            }
            catch (Exception ex)
            {
                // return the error message
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonSaltPepper_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                IP.AddSaltAndPepper((Bitmap)Image, (double)numUDSaltChance.Value, (double)numUDPepperChance.Value);
            }
            catch (Exception ex)
            {

                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonThreshholding_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                IP.CiftEsikleme((Bitmap)Image, (int)numUDThreshMin.Value, (int)numUDThreshMax.Value);
            }
            catch (Exception ex)
            {

                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonConvolution_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            // create a 3x3 kernel of double[,] to use in convolution
            try
            {
                double[,] kernel = matrixGrid1.GetMatrix();
                IP.FilterWith((Bitmap)Image, kernel);
            }
            catch (Exception ex)
            {

                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void ButtonEdgeDetectCanny_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                IP.CannyEdgeDetection((Bitmap)Image, 1);
            }
            catch (Exception ex)
            {

                _ = MessageBox.Show(ex.Message);
                //throw ex;
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void buttonFilterMean_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                IP.MeanFilter((Bitmap)Image, (int)numUDFilterMatrixSize.Value);
            }
            catch (Exception ex)
            {
                // return the error message
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void ButtonMorphDilation_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                bool[,] structure = IP.GenerateStructuringElement((MorphologicalStructuringElements)comboBoxStructureElements.SelectedIndex, (int)numUDMorphSize.Value);
                Bitmap result = IP.ApplyMorphologicalOperation((Bitmap)Image, structure, MorphologicalOps.Dilation);
                SetImage(result);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void ButtonMorphClosing_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                bool[,] structure = IP.GenerateStructuringElement((MorphologicalStructuringElements)comboBoxStructureElements.SelectedIndex, (int)numUDMorphSize.Value);
                Bitmap result = IP.ApplyMorphologicalOperation((Bitmap)Image, structure, MorphologicalOps.Closing);
                SetImage(result);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void ButtonMorphOpening_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                bool[,] structure = IP.GenerateStructuringElement((MorphologicalStructuringElements)comboBoxStructureElements.SelectedIndex, (int)numUDMorphSize.Value);
                Bitmap result = IP.ApplyMorphologicalOperation((Bitmap)Image, structure, MorphologicalOps.Opening);
                SetImage(result);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void ButtonMorphErosion_Click(object sender, EventArgs e)
        {
            SetControlsEnabledTo(false);
            try
            {
                bool[,] structure = IP.GenerateStructuringElement((MorphologicalStructuringElements)comboBoxStructureElements.SelectedIndex, (int)numUDMorphSize.Value);
                Bitmap result = IP.ApplyMorphologicalOperation((Bitmap)Image, structure, MorphologicalOps.Erosion);
                SetImage(result);
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message);
            }
            RefreshWorkspace();
            SetControlsEnabledTo(true);
        }

        private void ButtonConvNewMatrix_Click(object sender, EventArgs e)
        {
            matrixGrid1.CreateMatrix((int)NumUDConvMatrixSize.Value);
        }

        private void ButtonFilterGauss_Click(object sender, EventArgs e)
        {

        }
    }
}
