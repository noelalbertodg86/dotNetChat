using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "index.html");
            webBrowser1.Navigate(path);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void avanzar_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void refrescar_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void homo_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        private void ir_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(toolStrip1.Text);
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName, "index.html");
            webBrowser1.Navigate(path);
        }
    }
}
