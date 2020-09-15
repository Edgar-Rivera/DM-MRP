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
    public partial class frm_vistaGasto : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        public frm_vistaGasto(string codigo, string sucursal, string usuario, string fecha, string descripcion, string observaciones, string monto)
        {
            InitializeComponent();
            label12.Text = codigo;
            label14.Text = sucursal;
            label13.Text = usuario;
            label15.Text = fecha;
            label7.Text = descripcion;
            if (observaciones != "   ")
            {
                label10.Text = observaciones;
            }
            label16.Text = monto;
        }

        private void frm_vistaGasto_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frm_vistaGasto_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_vistaGasto_MouseLeave(object sender, EventArgs e)
        {

        }

        private void frm_vistaGasto_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }

        }

        private void frm_vistaGasto_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_vistaGasto_Load(object sender, EventArgs e)
        {

        }
    }
}
