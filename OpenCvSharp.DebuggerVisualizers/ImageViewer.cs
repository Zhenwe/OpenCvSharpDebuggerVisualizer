using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace OpenCvSharp.DebuggerVisualizers
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ImageViewer : Form
    {
        private readonly Bitmap bitmpa;

        /// <summary>
        /// 
        /// </summary>
        public ImageViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="proxy"></param>
        public ImageViewer(MatProxy proxy)
            : this()
        {
            bitmpa = proxy.CreateBitmap();
            propertyGrid1.SelectedObject = proxy;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            pictureBox.Image = bitmpa;
            
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = string.Empty;

        }
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {

            var point = pictureBox.PointToImage(e.Location);
            int x = point.X;
            int y = point.Y;
            string location = string.Format($"X:{x}, Y:{y}");

            string color = "";
            if (0 <= x && x < bitmpa.Width && 0 <= y && y < bitmpa.Height)
            {
                var c = bitmpa.GetPixel(x, y);
                color = $" | R:{c.R}, G:{c.G}, B:{c.B}";
            }
            
            toolStripStatusLabel.Text = location + color;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "png|*.png|bmp|*.bmp|tif|*.tif";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string fileName = sfd.FileName;
                bitmpa.Save(fileName);
            }
        }


    }
}
