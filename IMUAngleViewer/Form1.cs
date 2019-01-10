using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMUAngleViewer
{
    public partial class Form1 : Form
    {
        IsometricDraw Idraw;
        public Form1()
        {
            InitializeComponent();
            Idraw = new IsometricDraw(pbCanvas.Width, pbCanvas.Height, 0.2f);
            Bitmap canvas = new Bitmap(pbCanvas.Width, pbCanvas.Height);
            Idraw.DrawOriginalAxis(ref canvas);
            pbCanvas.Image = canvas;
        }

        private void btConnectCom_Click(object sender, EventArgs e)
        {

        }
    }
}
