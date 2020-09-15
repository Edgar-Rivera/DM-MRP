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
    public partial class frm_PrecioMateriaPrima : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string codigoMercaderia;
        public frm_PrecioMateriaPrima(string mercaderia, string sucursal)
        {
            InitializeComponent();
            codigoMercaderia = mercaderia;
            cargaSucursales(mercaderia);
            cargaMateriaPrima(mercaderia, sucursal);
        }
        private void cargaMateriaPrima(string codigo, string sucursal)
        {
            try
            {
                dataGridView1.Rows.Clear();
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE ID_MERCADERIA = '{0}' AND SUCURSAL = '{1}';",codigo, sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                        //styleDV(this.dataGridView1);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "PRECIOS MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void frm_PrecioMateriaPrima_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_PrecioMateriaPrima_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }
        private void cargaSucursales(string mercaderia)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {

                string sql = string.Format("SELECT * FROM VISTA_SUCURSALES_PRECIOS_MARCADERIA WHERE ID_MERCADERIA = '{0}';", mercaderia);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(1));
                    while (reader.Read())
                    {
                        dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(1));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void frm_PrecioMateriaPrima_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void frm_PrecioMateriaPrima_Load(object sender, EventArgs e)
        {
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[1].ReadOnly = true;
            dataGridView2.Columns[2].ReadOnly = false;
        }

        private void frm_PrecioMateriaPrima_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "PRECIOS MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.S))
            {
                 button2.PerformClick();              
            }
        }
        private bool sucursalSeleccionada()
        {
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (dataGridView2.Rows[i].Cells[2].Value != null)
                {
                    if (Convert.ToBoolean(dataGridView2.Rows[i].Cells[2].Value.ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "TRASLADO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }
        private bool ActualizaPrecios(string precio, string codigo, string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE SUCURSAL_HAS_MERCADERIA SET " +
                    "PRECIO = {0} WHERE " +
                    " ID_MERCADERIA = '{1}' AND ID_SUCURSAL = {2};",
                     precio, codigo, sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if ((cmd.ExecuteNonQuery() == 1))
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
            return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (sucursalSeleccionada() & dataGridView1.RowCount > 0)
            {
                for(int i = 0; i<dataGridView2.RowCount; i++)
                {
                    ActualizaPrecios(dataGridView1.Rows[0].Cells[4].Value.ToString(),
                        dataGridView1.Rows[0].Cells[0].Value.ToString(),
                        dataGridView2.Rows[i].Cells[0].Value.ToString());
                }
                var done = new frm_done();
                done.Show();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("NO HA SELECCIONADO NINGUNA SUCURSAL!", "PRECIOS MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
