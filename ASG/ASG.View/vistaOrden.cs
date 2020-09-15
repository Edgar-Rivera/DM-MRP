using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;

namespace ASG
{
    public partial class vistaOrden : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string recetaP;
        public vistaOrden(string codigo, string user, string codm, string estado, string init, string fin, string desc, string cantidad, string receta)
        {
            InitializeComponent();
            label12.Text = codigo;
            label13.Text = user;
            label14.Text = codm;
            label11.Text = estado;
            label7.Text = init;
            label9.Text = fin;
            label5.Text = desc;
            label18.Text = cantidad;
            recetaP = receta;
            cargaDetalleReceta(receta);
        }
        private void cargaTotales()
        {
            if (dataGridView2.RowCount > 0)
            {
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    dataGridView2.Rows[i].Cells[4].Value = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value.ToString()) * Convert.ToDouble(label18.Text);
                }
            }
        }
        private void cargaDetalleReceta(string receta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_DETALLE_RECETA WHERE ID_RECETA = {0};", receta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView2.Rows.Clear();
                    dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), ' ', reader.GetString(4));
                    while (reader.Read())
                    {
                        dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), ' ', reader.GetString(4));
                        //styleDV(this.dataGridView1);
                    }
                    cargaTotales();
                }
                else
                {
                    // MessageBox.Show("NO EXISTEN CLIENTES!", "GESTION CLIENTES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION RECETAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void vistaOrden_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void vistaOrden_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void vistaOrden_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void vistaOrden_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void vistaOrden_Load(object sender, EventArgs e)
        {

        }
    }
}
