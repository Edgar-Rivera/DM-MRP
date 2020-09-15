using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASG
{
    public partial class frm_vistaSucursal : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        public frm_vistaSucursal(string idSucursal)
        {
            InitializeComponent();
            cargaDatos(idSucursal);
            cargaMercaderia(idSucursal);
        }
        private void cargaMercaderia(string codigo)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format(" SELECT COUNT( distinct ID_MERCADERIA) FROM MERCADERIA WHERE ESTADO_TUPLA = TRUE AND ACTIVO = TRUE AND ID_SUCURSAL = {0};", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label16.Text = reader.GetString(0);
                }
                else
                {
                    label16.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void cargaDatos(string codigo)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM  VISTA_SUCURSALES WHERE ID_SUCURSAL = {0};", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label12.Text = reader.GetString(0);
                    label13.Text = reader.GetString(1);
                    label14.Text = reader.GetString(2);
                    label15.Text = reader.GetString(3);
                    label9.Text = reader.GetString(4);
                }
                else
                {
                    MessageBox.Show("NO EXISTE la SUCURSAL!", "SUCURSALES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "SUCURSALES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void frm_vistaSucursal_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frm_vistaSucursal_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_vistaSucursal_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_vistaSucursal_MouseUp(object sender, MouseEventArgs e)
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

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void frm_vistaSucursal_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
