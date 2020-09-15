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
    public partial class frm_HistorialPrecios : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        public frm_HistorialPrecios(string codigo, string sucursal)
        {
            InitializeComponent();
            cargaComprasKardex(codigo, sucursal);
            obtienPrecioPromedio();
        }

        private void frm_HistorialPrecios_Load(object sender, EventArgs e)
        {

        }
        private void obtienPrecioPromedio()
        {
            if(dataGridView2.RowCount > 0)
            {
                double precioPormedio = 0;
                for (int i = 0; i <dataGridView2.RowCount; i++)
                {
                    string dato = dataGridView2.Rows[i].Cells[6].Value.ToString();
                    dato = dato.Replace("Q", "");
                    precioPormedio = precioPormedio + Convert.ToDouble(dato);
                }
                if(precioPormedio > 0)
                {
                    double precioFinal = precioPormedio / dataGridView2.RowCount;
                    label1.Text = string.Format("Q {0:###,###,###,##0.00##}", precioFinal);
                }
            }
        }
        private void frm_HistorialPrecios_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }
        private void cargaComprasKardex(string codigo, string nombreSucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_KARDEX_COMPRA WHERE ID_MATERIA_PRIMA = '{0}'   ORDER BY FECHA_COMPRA DESC LIMIT 100;", codigo, nombreSucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(1), "COMPRA", reader.GetString(3), reader.GetString(4), string.Format("{0:#,###,###,###,##0.00##}", reader.GetDouble(5)), string.Format("Q{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                    while (reader.Read())
                    {
                        dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(1), "COMPRA", reader.GetString(3), reader.GetString(4), string.Format("{0:#,###,###,###,##0.00##}", reader.GetDouble(5)), string.Format("Q{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void frm_HistorialPrecios_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_HistorialPrecios_MouseUp(object sender, MouseEventArgs e)
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

        private void frm_HistorialPrecios_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
