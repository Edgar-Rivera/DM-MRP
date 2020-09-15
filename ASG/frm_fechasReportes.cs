using CrystalDecisions.CrystalReports.Engine;
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
    public partial class frm_fechasReportes : Form
    {
        string usuario;

        Point DragCursor;
        Point DragForm;
        bool Dragging;
        int indexer;
        public frm_fechasReportes(string LocalUser, int indexerS)
        {
            InitializeComponent();
            usuario = LocalUser;
            
            indexer = indexerS;
            if (indexer == 0) cargaSucursalesCompras();
            else cargaSucursales();

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
                    // comboBox4.Items.Clear();
                    comboBox2.Items.Add(reader.GetString(0));

                    //comboBox4.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));
                        //comboBox4.Items.Add(reader.GetString(0));
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void cargaSucursalesCompras()
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ESTADO_TUPLA = TRUE AND ID_TIPO = 1;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox2.Items.Clear();
                    // comboBox4.Items.Clear();
                    comboBox2.Items.Add(reader.GetString(0));

                    //comboBox4.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));
                        //comboBox4.Items.Add(reader.GetString(0));
                    }

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_fechasReportes_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.P))
            {
                button8.PerformClick();
            }
        }

        private void frm_fechasReportes_Load(object sender, EventArgs e)
        {

        }

        private void frm_fechasReportes_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_fechasReportes_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_fechasReportes_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
            {
                dateTimePicker1.Value = DateTime.Now;
                MessageBox.Show("LA FECHA NO PUEDE SER MAYOR A LA FECHA DE DESTINO", "REPORTE DE VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
            {
                dateTimePicker2.Value = DateTime.Now;
                MessageBox.Show("LA FECHA NO PUEDE SER MENOR A LA FECHA DE PARTIDA", "REPORTE DE VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(comboBox2.Text != "")
            {
                // indexer la variable que pemrite ver que tipo de reprote se imprime
                // indessser asigan 0  al reprote de compras al proveedor
                if(indexer == 0)
                {
                    // reprote de ocmpras al proveedor los datos solo se cargn de las sucursales que administran compras
                    frm_reporteVenta frm = new frm_reporteVenta();
                    reporteCompras cr = new reporteCompras();
                    TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["sucursal"];
                    text.Text = comboBox2.Text;
                    TextObject fecha = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["fecha"];
                    if (dateTimePicker1.Value.ToString() == dateTimePicker2.Value.ToString())
                    {
                        fecha.Text = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        fecha.Text = "Del: " + dateTimePicker1.Value.Date.ToString("dd/MM/yyyy") + " al: " + dateTimePicker2.Value.Date.ToString("dd/MM/yyyy");
                    }
                    cr.SetParameterValue("desde", dateTimePicker1.Value.Date.ToString());
                    cr.SetParameterValue("hasta", dateTimePicker2.Value.Date.ToString());
                    cr.SetParameterValue("sucursal", comboBox2.Text);
                    frm.crystalReportViewer1.ReportSource = cr;
                    frm.crystalReportViewer1.RefreshReport();
                    frm.Show();

                } else if(indexer == 1)
                {
                    // imprimme los datos de los reportes de los gastos
                    // el valor de la variable  
                    frm_reporteVenta frm = new frm_reporteVenta();
                    CrystalReport25 cr = new CrystalReport25();
                    TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["sucursal"];
                    text.Text = comboBox2.Text;
                    TextObject fecha = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["periodo"];
                    if (dateTimePicker1.Value.ToString() == dateTimePicker2.Value.ToString())
                    {
                        fecha.Text = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        fecha.Text = "Del: " + dateTimePicker1.Value.Date.ToString("dd/MM/yyyy") + " al: " + dateTimePicker2.Value.Date.ToString("dd/MM/yyyy");
                    }
                    cr.SetParameterValue("sucursal", comboBox2.Text);
                    cr.SetParameterValue("desde", dateTimePicker1.Value.Date.ToString());
                    cr.SetParameterValue("hasta", dateTimePicker2.Value.Date.ToString());
                    frm.crystalReportViewer1.ReportSource = cr;
                    frm.crystalReportViewer1.RefreshReport();
                    frm.Show(); 

                } else if (indexer == 2)
                {
                    // variablel de control para los gastos
                    frm_reporteVenta frm = new frm_reporteVenta();
                    reporteGastos cr = new reporteGastos();
                    TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["sucursal"];
                    text.Text = comboBox2.Text;
                    TextObject fecha = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["fecha"];
                    if (dateTimePicker1.Value.ToString() == dateTimePicker2.Value.ToString())
                    {
                        fecha.Text = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        fecha.Text = "Del: " + dateTimePicker1.Value.Date.ToString("dd/MM/yyyy") + " al: " + dateTimePicker2.Value.Date.ToString("dd/MM/yyyy");
                    }
                    cr.SetParameterValue("sucursal", comboBox2.Text);
                    cr.SetParameterValue("desde", dateTimePicker1.Value.Date.ToString());
                    cr.SetParameterValue("hasta", dateTimePicker2.Value.Date.ToString());
                    frm.crystalReportViewer1.ReportSource = cr;
                    frm.crystalReportViewer1.RefreshReport();
                    frm.Show();
                } else if (indexer == 3)
                {
                    // variable de control es iguala 3 cuando es un reporte d elos cobros efectuados
                    frm_reporteVenta frm = new frm_reporteVenta();
                    CrystalReport30 cr = new CrystalReport30();
                    TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["sucursal"];
                    text.Text = comboBox2.Text;
                    TextObject fecha = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["fecha"];
                    if (dateTimePicker1.Value.ToString() == dateTimePicker2.Value.ToString())
                    {
                        fecha.Text = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        fecha.Text = "Del: " + dateTimePicker1.Value.Date.ToString("dd/MM/yyyy") + " al: " + dateTimePicker2.Value.Date.ToString("dd/MM/yyyy");
                    }
                    cr.SetParameterValue("sucursal", comboBox2.Text);
                    cr.SetParameterValue("desde", dateTimePicker1.Value.Date.ToString());
                    cr.SetParameterValue("hasta", dateTimePicker2.Value.Date.ToString());
                    frm.crystalReportViewer1.ReportSource = cr;
                    frm.crystalReportViewer1.RefreshReport();
                    frm.Show();
                } else if (indexer == 4)
                {
                    // variable de control es cuando es es REPORTE DE UTILIDADES  
                    frm_reporteVenta frm = new frm_reporteVenta();
                    CrystalReport31 cr = new CrystalReport31();
                    TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["sucursal"];
                    text.Text = comboBox2.Text;
                    TextObject fecha = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["fecha"];
                    if (dateTimePicker1.Value.ToString() == dateTimePicker2.Value.ToString())
                    {
                        fecha.Text = dateTimePicker1.Value.Date.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        fecha.Text = "Del: " + dateTimePicker1.Value.Date.ToString("dd/MM/yyyy") + " al: " + dateTimePicker2.Value.Date.ToString("dd/MM/yyyy");
                    }
                    cr.SetParameterValue("sucursal", comboBox2.Text);
                    cr.SetParameterValue("desde", dateTimePicker1.Value.Date.ToString());
                    cr.SetParameterValue("hasta", dateTimePicker2.Value.Date.ToString());
                    frm.crystalReportViewer1.ReportSource = cr;
                    frm.crystalReportViewer1.RefreshReport();
                    frm.Show();
                }
            } else
            {
                MessageBox.Show("NO HA SELECCIONADO UNA SUCURSAL!", "IMPRESION DE REPORTES", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
