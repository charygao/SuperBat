using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Bootcfg : BatCt.CommandSetter
    {
        public Bootcfg(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }

        private void Bootcfg_Load(object sender, EventArgs e)
        {
            Comment = Command.CommandDiscription;
        }
    }
}
