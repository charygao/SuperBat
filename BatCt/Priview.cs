using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt
{
    public partial class Priview : Form
    {
        public Priview()
        {
            InitializeComponent();
        }

        private void Priview_Load(object sender, EventArgs e)
        {

        }

        public void SetText(string stringValue)
        {
            richTextBox1.Text = stringValue;
        }

        public string GetText()
        {
            return richTextBox1.Text;
        }
    }
}
