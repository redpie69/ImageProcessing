using System.Windows.Forms;

namespace IPForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonTransfer_Click(object sender, System.EventArgs e)
        {
            multiImageWorkspace1.TransferImages(imageWorkspace1.Image, imageWorkspace2.Image);
        }
    }
}
