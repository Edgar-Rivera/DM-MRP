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
    public partial class frm_detallesCuenta : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string codigoCliente;
        string CodigoCuenta;
        string codigoFactura;
        public frm_detallesCuenta(string codigo,string nombre, string cuenta, string factura)
        {
            InitializeComponent();
            codigoCliente = codigo;
            CodigoCuenta = cuenta;
            label2.Text = codigo;
            label6.Text = nombre;
            label13.Text = cuenta;
            codigoFactura = factura;
            if (estadoCuenta())
            {
                label5.BackColor = Color.Turquoise;
                label5.Text = "ACTIVA";
            } else
            {
                label5.BackColor = Color.Red;
                label5.Text = "CANCELADA";
            }
            cargaDatos();
            cargaPagado();
            getAbonos();
        }
        private void getAbonos()
        {
            double total = 0;
            if(dataGridView1.RowCount > 0)
            {
                for (int i = 0; i<dataGridView1.RowCount; i++)
                {
                    total = total + Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
                }
                label17.Text = string.Format("{0:###,###,###,##0.00##}", total);
                label19.Text = "" + (Convert.ToDouble(label16.Text) - Convert.ToDouble(label21.Text));
            }
        }
        private bool estadoCuenta()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT ESTADO_TUPLA FROM CUENTA_POR_COBRAR_CLIENTE WHERE ID_CUENTA_POR_COBRAR = {0} AND ESTADO_TUPLA = TRUE;", CodigoCuenta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
            return false;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frm_detallesCuenta_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;
        }
        private void cargaDatos()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_ABONOS WHERE ID_CUENTA_POR_COBRAR = {0};", CodigoCuenta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void cargaPagado()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT TOTAL_PAGADO, (select total_factura from factura where id_factura =  (select id_factura from cuenta_por_cobrar_cliente where id_cuenta_por_cobrar = '{0}')) FROM CUENTA_POR_COBRAR_CLIENTE WHERE ID_CUENTA_POR_COBRAR = {0};", CodigoCuenta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   label21.Text = string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(0));
                   label16.Text = string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(1));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void frm_detallesCuenta_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_detallesCuenta_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_detallesCuenta_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void frm_detallesCuenta_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.D))
            {
                button2.PerformClick();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount > 0)
            {
                var forma = new frm_detalleCodigo(codigoFactura);
                forma.Show();
            }
        }
    }
}
