using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASG.ASG.View
{
    public partial class frm_vistaReceta : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        public frm_vistaReceta()
        {
            InitializeComponent();
        }

        private void btnDetalleReceta_Click(object sender, EventArgs e)
        {
            frm_vistaDetalleReceta frmDReceta = new frm_vistaDetalleReceta();
            frmDReceta.setIdReceta(Convert.ToInt32(lblCodReceta.Text));
            frmDReceta.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_vistaReceta_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frm_vistaReceta_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_vistaReceta_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_vistaReceta_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }
    }
}
