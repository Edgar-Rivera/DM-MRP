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
    public partial class frm_vistaProveedor : Form
    {
        string nombreProveedor;
        Point DragCursor;
        Point DragForm;
        bool Dragging;

        public frm_vistaProveedor(string codigo, string nombre, string direccion)
        {
            InitializeComponent();
            nombreProveedor = nombre;
            label12.Text = codigo;
            label13.Text = direccion;
            label14.Text = nombre;
            cargaDeudaProveedor();
            cargaCompras();
        }
         // FUNCION QUE PERMITE CARGAR SI HAY UNA EUDA CON EL PROVEEDOR OJO ESTO ES UNA PRUEBA 
        private void cargaDeudaProveedor()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {              
                string sql = string.Format("SELECT SUM(TOTAL_FINAL - TOTAL_PAGADO) FROM TOTAL_DEUDA WHERE PROVEEDOR = '{0}';", nombreProveedor);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label6.Text = "No se posee deuda con el proveedor";
                    }
                    else
                    {
                       
                        label6.Text = string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(0));
                    }
                }
            }
              catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void cargaCompras()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView2.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_COMPRA WHERE NOMBRE_PROVEEDOR = '{0}' ORDER BY FECHA_COMPRA DESC LIMIT 25;", nombreProveedor);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(5)), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)), reader.GetString(7));
                    while (reader.Read())
                    {
                        dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(5)), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)), reader.GetString(7));
                        //styleDV(this.dataGridView2);
                    }
                }
                else
                {
                    // MOSTARA EN LABEL NO EXISTEN PRODUCTOS
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frm_vistaProveedor_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_vistaProveedor_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_vistaProveedor_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void frm_vistaProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void frm_vistaProveedor_Load(object sender, EventArgs e)
        {
            

        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.RowCount > 0)
            {
                var forma = new frm_detalleCompra(dataGridView2.CurrentRow.Cells[0].Value.ToString(), dataGridView2.CurrentRow.Cells[3].Value.ToString(), dataGridView2.CurrentRow.Cells[4].Value.ToString(), dataGridView2.CurrentRow.Cells[5].Value.ToString(), true);
                forma.Show();
            }
        }
    }
}
