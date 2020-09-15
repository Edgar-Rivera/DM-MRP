using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASG
{
    public partial class frm_formaDeVenta : Form
    {
        public frm_formaDeVenta(string tipoVenta, string descripcion)
        {
            InitializeComponent();
            label1.Text = tipoVenta;
            label3.Text = descripcion;
            timer1.Interval = 1000;
            timer1.Enabled = true;
        }

        private void frm_formaDeVenta_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
