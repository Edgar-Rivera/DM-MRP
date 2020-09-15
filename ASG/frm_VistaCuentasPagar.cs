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
    public partial class frm_VistaCuentasPagar : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;

        public frm_VistaCuentasPagar()
        {
            InitializeComponent();
            cargaDatos();
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUserDetails_RowPostPaint);
        }
        private void dgvUserDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
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

        private void Frm_VistaCuentasPagar_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void Frm_VistaCuentasPagar_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void Frm_VistaCuentasPagar_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Frm_VistaCuentasPagar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }
        private void setDescripcion(int celda)
        {
            for (int i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[celda].Value.ToString() == "0")
                {
                    dataGridView1.Rows[i].Cells[celda].Value = "INACTIVO";

                }
                else
                {
                    dataGridView1.Rows[i].Cells[celda].Value = "ACTIVO";
                }
            }
        }
        private void cargaDatos()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView1.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_POR_CLIENTE_COBRAR WHERE ESTADO_TUPLA = {0};", 1);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                    }
                    setDescripcion(7);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void Frm_VistaCuentasPagar_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;
        }
    }
}
