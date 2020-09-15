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
    public partial class frm_VistaProductosAgotados : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;

        public frm_VistaProductosAgotados(string sucursalActual, string rolUs)
        {
            InitializeComponent();
            cargaSucursales();
            comboBox2.Text = sucursalActual;
            cargaMercaderiaTodo(sucursalActual);
            if (rolUs != "ADMINISTRADOR" && rolUs != "ROOT" && rolUs != "DATA BASE ADMIN")
            {
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[5].Visible = false;             
            }
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUserDetails_RowPostPaint);
        }
        private void dgvUserDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
        private void setDescripcion(int celda)
        {
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[celda].Value.ToString() == "0")
                {
                    dataGridView1.Rows[i].Cells[celda].Value = "INACTIVO";
                    dataGridView1.Rows[i].Cells[celda].Style.ForeColor = Color.White;
                    dataGridView1.Rows[i].Cells[celda].Style.BackColor = Color.OrangeRed;
                }
                else
                {
                    dataGridView1.Rows[i].Cells[celda].Value = "ACTIVO";
                }
            }
        }
        private void setDisponible(int celda)
        {
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if ((dataGridView1.Rows[i].Cells[celda].Value.ToString() == "") | ((dataGridView1.Rows[i].Cells[celda].Value.ToString() == "0")))
                {
                    dataGridView1.Rows[i].Cells[celda].Value = "0";
                    dataGridView1.Rows[i].Cells[celda].Style.ForeColor = Color.White;
                    dataGridView1.Rows[i].Cells[celda].Style.BackColor = Color.FromArgb(207, 136, 65);
                }
                else if ((Convert.ToDouble(dataGridView1.Rows[i].Cells[celda].Value.ToString()) < 0))
                {

                    dataGridView1.Rows[i].Cells[celda].Style.ForeColor = Color.White;
                    dataGridView1.Rows[i].Cells[celda].Style.BackColor = Color.FromArgb(38, 39, 48);
                }
            }
        }
        private void cargaMercaderiaTodo(string sucursal)
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MERCADERIA_C  WHERE NOMBRE_SUCURSAL = '{0}' AND TOTAL_UNIDADES <= 0 AND ACTIVO ORDER BY TOTAL_UNIDADES ASC;", sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(4), string.Format("Q{0:###,###,###,##0.00##}", reader.GetDouble(5)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(6)), reader.GetString(2), reader.GetString(3), reader.GetString(1), reader.GetString(7), reader.GetString(8));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(4), string.Format("Q{0:###,###,###,##0.00##}", reader.GetDouble(5)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(6)), reader.GetString(2), reader.GetString(3), reader.GetString(1), reader.GetString(7), reader.GetString(8));
                        //styleDV(this.dataGridView1);
                    }
                    setDescripcion(7);
                    setDisponible(3);
                    //label18.Text = string.Format("{0:#,###,###,###,###}", dataGridView1.RowCount);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void PictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void PictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void Frm_VistaProductosAgotados_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            cargaMercaderiaTodo(comboBox2.Text);
        }

        private void Frm_VistaProductosAgotados_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void Frm_VistaProductosAgotados_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void Frm_VistaProductosAgotados_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void Frm_VistaProductosAgotados_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }
    }
}
