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
using CrystalDecisions.CrystalReports.Engine;

namespace ASG
{
    public partial class frm_detalleCodigo : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string codigo;
        string codigoPersonalCliente;
        public frm_detalleCodigo(string code)
        {
            InitializeComponent();
            codigo = code;
            label9.Text = code;
            cargaEncabezado();
            cargaDatosDB();
            cargaDetalle();
            this.dataGridView1.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dgvUserDetails_RowPostPaint);
        }
        private void cargaDatosDB()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT  CODIGOCLIENTE FROM CLIENTE WHERE ID_CLIENTE = '{0}' ;", label20.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {       
                    codigoPersonalCliente = reader.GetString(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void cargaDetalle()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT D.ID_MERCADERIA, D.DESCRIPCION_DETALLE, D.CANTIDAD_DET_FACTURA, D.SUBTOTAL_DETALLE,D.DESCUENTO_DET_FACTURA,D.TOTAL_DET_FACTURA FROM DET_FACTURA D WHERE ID_FACTURA = '{0}' AND D.ESTADO_TUPLA = TRUE;", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)));
                        //styleDV(this.dataGridView1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void dgvUserDetails_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dataGridView1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }
        private void cargaEncabezado()
        {
            OdbcConnection connection = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT ID_CLIENTE, TIPO_FACTURA, NOMBRE, FECHA_EMISION_FACTURA, DIRECCION_CLIENTE, NOMBRE_USUARIO, SUBTOTAL_FACTURA, DESCUENTO_FACTURA, TOTAL_FACTURA  FROM VISTA_FACTURAS  WHERE  ID_FACTURA = '{0}';", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, connection);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label20.Text = reader.GetString(0);
                    label22.Text = reader.GetString(1);
                    label8.Text = reader.GetString(2);
                    label15.Text = reader.GetString(3);
                    label16.Text = reader.GetString(4);
                    label18.Text = reader.GetString(5);
                    label5.Text = string.Format("Q {0:###,###,###,##0.00##}", reader.GetDouble(6));
                    label1.Text = string.Format("Q {0:###,###,###,##0.00##}", reader.GetDouble(7));
                    label7.Text = string.Format("Q {0:###,###,###,##0.00##}", reader.GetDouble(8));
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            connection.Close();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_detalleCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.P))
            {
                button3.PerformClick();
            }
        }

        private void frm_detalleCodigo_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_detalleCodigo_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_detalleCodigo_MouseUp(object sender, MouseEventArgs e)
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

        private void frm_detalleCodigo_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataSetVenta ds = new DataSetVenta();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                ds.Tables[0].Rows.Add
                    (new object[]
                    {
                                    dataGridView1[2,i].Value.ToString(),
                                    dataGridView1[1,i].Value.ToString(),
                                    dataGridView1[3,i].Value.ToString(),
                                    dataGridView1[5,i].Value.ToString()
                    });
            }
            frm_reporteVenta frm = new frm_reporteVenta();
            CrystalReport4 cr = new CrystalReport4();
            cr.SetDataSource(ds);
            TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["numero"];
            text.Text = label9.Text;
            TextObject textn = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["nombre"];
            textn.Text = label8.Text;
            TextObject textd = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["direccion"];
            textd.Text = label16.Text;
            TextObject textc = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["codigo"];
            textc.Text = codigoPersonalCliente;
            TextObject textv = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["vendedor"];
            textv.Text = label18.Text;
            TextObject textt = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["terminos"];
            textt.Text = label22.Text;
            TextObject texttotal = (TextObject)cr.ReportDefinition.Sections["Section4"].ReportObjects["total"];
            texttotal.Text = label7.Text;
            TextObject texttotaletra = (TextObject)cr.ReportDefinition.Sections["Section5"].ReportObjects["letters"];
            numberToString nt = new numberToString();
            string temp = label7.Text;
            temp = temp.Replace("Q", "");
            texttotaletra.Text = nt.enletras(temp) + " Q.";
            frm.crystalReportViewer1.ReportSource = cr;
            frm.ShowDialog();
        }
    }
}
