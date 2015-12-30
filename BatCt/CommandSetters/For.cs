using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class For : BatCt.CommandSetter
    {
        public For(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }
    }
}
