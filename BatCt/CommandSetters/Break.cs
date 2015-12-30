using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatCt.CommandSetters
{
    public partial class Break : BatCt.CommandSetter
    {
        public Break(BatCommand command)
            : base(command)
        {
            InitializeComponent();
        }
       
    }
}
