using com.calitha.goldparser;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiler
{
    public partial class Form1 : Form
    {
        MyParser sm;
        public Form1()
        {
            InitializeComponent();
            sm = new MyParser("project.cgt", listBox1);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            sm.Parse(textBox1.Text);
        }
    }
}
