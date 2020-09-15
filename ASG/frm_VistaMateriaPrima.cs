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
    public partial class frm_VistaMateriaPrima : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string rolUsuario;
        public frm_VistaMateriaPrima(string codigo, string desc, string proveedor, string unidad, string total, string sucursal, string precio, string rol)
        {
            InitializeComponent();
            label12.Text = codigo;
            label7.Text = desc;
            rolUsuario = rol;
            label13.Text = proveedor;
            label15.Text = unidad;
            label10.Text = total;
            label14.Text = sucursal;
            label16.Text = precio;
        }

        private void frm_VistaMateriaPrima_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.H))
            {
               
                    button25.PerformClick();
                
            }
        }

        private void frm_VistaMateriaPrima_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_VistaMateriaPrima_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_VistaMateriaPrima_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void frm_VistaMateriaPrima_Load(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (label12.Text != "")
            {
                var forma = new frm_HistorialPrecios(label12.Text, label14.Text);
                forma.Show();
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun producto!", "Historial de Precios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var done = new frm_VistaGeneralMateria(label12.Text, label7.Text, 1,rolUsuario);
            done.Show();
        }
    }
}
