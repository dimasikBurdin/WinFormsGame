using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGameModel.Views
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
            var menu = new MenuControl();
            Controls.Add(menu);
        }
    }
}
