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
    public partial class frm_vistaCotizaciones : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;

        public frm_vistaCotizaciones(string nombreSucursal)
        {
            InitializeComponent();
            cargaSucursales();
            comboBox9.Text = nombreSucursal;
            cargaCotizaciones(nombreSucursal);
            this.dataGridView7.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUserDetails_RowPostPaint);
        }
        private void dgvUserDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView7.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
        private void Frm_vistaCotizaciones_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }
        private void cargaSucursales()
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ESTADO_TUPLA = TRUE;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox9.Items.Clear();
                    comboBox9.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox9.Items.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void Frm_vistaCotizaciones_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void Frm_vistaCotizaciones_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void Frm_vistaCotizaciones_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void PictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void PictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void cargaCotizaciones(string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView7.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_COTIZACION WHERE NOMBRE_SUCURSAL  = '{0}' AND ESTADO_TUPLA = TRUE ORDER BY FECHA_EMISION_FACTURA DESC;", sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView7.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2) + " " + reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(7)), reader.GetString(8), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(10)), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(11)), reader.GetString(13), reader.GetString(14));
                    while (reader.Read())
                    {
                        dataGridView7.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2) + " " + reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(7)), reader.GetString(8), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(10)), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(11)), reader.GetString(13), reader.GetString(14));
                        //styleDV(this.dataGridView1);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION CLIENTES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }

        private void ComboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaCotizaciones(comboBox9.Text);
        }

        private void DataGridView7_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView7.RowCount > 0)
            {
                var forma = new frm_detalleFactura(dataGridView7.CurrentRow.Cells[0].Value.ToString(), dataGridView7.CurrentRow.Cells[8].Value.ToString(), dataGridView7.CurrentRow.Cells[9].Value.ToString(), dataGridView7.CurrentRow.Cells[6].Value.ToString(), dataGridView7.CurrentRow.Cells[2].Value.ToString(), dataGridView7.CurrentRow.Cells[1].Value.ToString(), true, dataGridView7.CurrentRow.Cells[7].Value.ToString(), dataGridView7.CurrentRow.Cells[3].Value.ToString(), dataGridView7.CurrentRow.Cells[11].Value.ToString(), dataGridView7.CurrentRow.Cells[10].Value.ToString(), 1, true);
                forma.Show();
            }
        }

        private void frm_vistaCotizaciones_Load(object sender, EventArgs e)
        {
            dataGridView7.AllowUserToDeleteRows = false;
        }
    }
}
