using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Drawing.Printing;
using CrystalDecisions.CrystalReports.Engine;
/// <summary>
/// Programador: Edgar Rivera
/// Qwerty Labs 2019
/// Este formulario muestra las estadisticas o reportes segun las requieran de las sucursale y fechas a ser efectuadas
/// </summary>

namespace ASG
{
    public partial class frm_estadisticas : Form
    {
        string nameUs;
        string rolUs;
        string usuario;
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string currentSucursal;
        public frm_estadisticas(string nameUser, string rolUser, string user, string sucursal)
        {
            InitializeComponent();
            usuario = user;
            nameUs = nameUser;
            rolUs = rolUser;
            currentSucursal = sucursal;
            label19.Text = label19.Text + "" + nameUs;
            cargaSucursales();
            CargaMasVendidos();
            // DASHBORAD DIARIO
            totalFacturas();
            totalEfectivo();
            totalCredito();
            ganancias();
            countCreditos();
            countEfectivo();
            cargaClientes();
            cargaProveedores();
            cargaMercaderia();
            cargaMercaderiaInactiva();
            // TERMINA DASHBOARD DIARIO
            // GRAFICAS DE LOS PRODUCTOS
            graficasGanancia();
            graficasGananciaPeriodos();
            // SE OBTIENEN LOS PRIVILEGISO DE VER REPORTES
            getPrivilegios(user, rolUs);
            
        }
        // LA FUNCION TOMA LOS PRIVILEGISO DESDE LAS VARIALBES DE REPORTES COMO TIPO BOLEANO Y ES ASIGNADO A EL ESSTADO DE UN BOTON  -- SE REDUJO EL CIDGO SIGNIFICATIVAMENTE 
        private void getPrivilegios(string id_usuario, string id_rol)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM PRIVILEGIOS_USUARIO WHERE USUARIO_ID_USUARIO = '{0}' AND ROL_ID_ROL = '{1}';", id_usuario, id_rol);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {                 
                    /// Se cargan lso privilegiso de reperotes en esta seccion
                    /// 
                    button5.Enabled = reader.GetBoolean(20);
                    button19.Enabled = reader.GetBoolean(20);
                    button7.Enabled = reader.GetBoolean(21);
                    button18.Enabled = reader.GetBoolean(21);
                    button8.Enabled = reader.GetBoolean(22);
                    button6.Enabled = reader.GetBoolean(23);
                    button4.Enabled = reader.GetBoolean(24);
                    button17.Enabled = reader.GetBoolean(25);
                    button1.Enabled = reader.GetBoolean(26);
                    button2.Enabled = reader.GetBoolean(27);
                    button13.Enabled = reader.GetBoolean(28);
                    button16.Enabled = reader.GetBoolean(29);
                    button14.Enabled  = reader.GetBoolean(30);
                    button15.Enabled = reader.GetBoolean(31);
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION USUARIOS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void cargaSucursales()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ESTADO_TUPLA = TRUE;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox2.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));
                    }
                    comboBox2.Items.Add("TODAS LAS SUCURSALES");
                    comboBox2.Text = "TODAS LAS SUCURSALES";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
      /*  private void cargaSucursalesPorNombre()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_MAS_VENDIDOS WHERE NOMBRE_SUCURSAL = '{0}';",comboBox2.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Mercaderia.Add(reader.GetString(0) + " " + reader.GetString(1));
                    Total.Add(reader.GetString(3));
                    while (reader.Read())
                    {
                        Mercaderia.Add(reader.GetString(0) + " " + reader.GetString(1));
                        Total.Add(reader.GetString(3));
                    }
                    chart1.Series[0].Points.DataBindXY(Mercaderia, Total);
                }
                else
                {
                    MessageBox.Show("NO EXISTEN DATOS PARA MOSTRAR GRAFICAS!", "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }*/
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frm_estadisticas_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_estadisticas_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_estadisticas_MouseUp(object sender, MouseEventArgs e)
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

        private void frm_estadisticas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            } else if (e.KeyData == Keys.F5)
            {
                if(label8.Visible == false)
                {
                    label8.Visible = true;
                    label9.Visible = true;
                } else
                {
                    label8.Visible = false;
                    label9.Visible = false;
                }
            }
        }
        ArrayList Total = new ArrayList();
        ArrayList Mercaderia = new ArrayList();

        private void CargaMasVendidos()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_MAS_VENDIDOS;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Mercaderia.Add(reader.GetString(1));
                    Total.Add(reader.GetString(2));
                    while (reader.Read())
                    {
                        Mercaderia.Add(reader.GetString(1));
                        Total.Add(reader.GetString(2));
                    }
                    chart1.Series[0].Points.DataBindXY(Mercaderia, Total);
                }
                else
                {
                   // MessageBox.Show("NO EXISTEN DATOS PARA MOSTRAR GRAFICAS!", "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void frm_estadisticas_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if(comboBox2.Text == "TODAS")
            {
                Total.Clear();
                Mercaderia.Clear();
                CargaMasVendidos();
            } else
            {
                Total.Clear();
                Mercaderia.Clear();
                //cargaSucursalesPorNombre();
            }*/
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_MAS_VENDIDOS_MES WHERE MONTH(Name_exp_4) = '{0}' AND YEAR(Name_exp_4) = YEAR(NOW());", comboBox3.SelectedIndex + 1);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Total.Clear();
                    Mercaderia.Clear();
                    Mercaderia.Add(reader.GetString(1));
                    Total.Add(reader.GetString(2));
                    while (reader.Read())
                    {
                        Mercaderia.Add(reader.GetString(1));
                        Total.Add(reader.GetString(2));
                    }

                    chart1.Series[0].Points.DataBindXY(Mercaderia, Total);
                }
                else
                {
                   // MessageBox.Show("NO EXISTEN DATOS PARA MOSTRAR GRAFICAS PARA ESE MES!", "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void totalFacturas()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT COUNT(ID_FACTURA) FROM FACTURA WHERE ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND FECHA_EMISION_FACTURA >= curdate();");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label2.Text = reader.GetString(0);
                }
                else
                {
                    label2.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        double efectivo = 0;
        private void totalEfectivo()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(TOTAL_FACTURA) FROM FACTURA WHERE ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'EFECTIVO' AND FECHA_EMISION_FACTURA >= curdate();");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label3.Text = "0";
                    }
                    else
                    {
                        efectivo = reader.GetDouble(0);
                        label3.Text = string.Format("Q.{0:###,###,###,##0.00##}", efectivo);
                    }
                }
                else
                {
                    label3.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        double credito = 0;
        private void totalCredito()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(TOTAL_FACTURA) FROM FACTURA WHERE ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'CREDITO' AND FECHA_EMISION_FACTURA >= curdate();");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label6.Text = "0";
                    }
                    else
                    {
                        credito = reader.GetDouble(0);
                        label6.Text = string.Format("Q.{0:###,###,###,##0.00##}", credito);
                    }
                }
                else
                {
                    label6.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }

        private void ganancias()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(COMPRA - VENTA) FROM GANANCIAS;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label8.Text = "0";
                    }
                    else
                    {
                        double totalParcial = reader.GetDouble(0);
                        label8.Text = string.Format("Q.{0:###,###,###,##0.00##}", Math.Round(totalParcial, 2));
                    }
                }
                else
                {
                    label8.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
        private void countCreditos()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT COUNT(ID_FACTURA) FROM FACTURA WHERE ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND FECHA_EMISION_FACTURA >= CURDATE() AND TIPO_FACTURA = 'EFECTIVO';");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label12.Text = reader.GetString(0);
                }
                else
                {
                    label12.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void countEfectivo()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT COUNT(ID_FACTURA) FROM FACTURA WHERE ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND FECHA_EMISION_FACTURA >= CURDATE() AND TIPO_FACTURA = 'CREDITO';");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   

                        label10.Text = reader.GetString(0);

                }
                else
                {
                    label10.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void cargaClientes()
        {

            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format(" SELECT COUNT(ID_CLIENTE) FROM CLIENTE WHERE FECHA_INGRESO >= CURDATE() AND ESTADO_TUPLA = TRUE;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    label14.Text = reader.GetString(0);

                }
                else
                {
                    label14.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void cargaProveedores()
        {

            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format(" SELECT COUNT(ID_PROVEEDOR) FROM PROVEEDOR WHERE FECHA_INGRESO >= CURDATE() AND ESTADO_TUPLA = TRUE;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    label17.Text = reader.GetString(0);

                }
                else
                {
                    label17.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void cargaMercaderia()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format(" SELECT COUNT( distinct ID_MERCADERIA) FROM MERCADERIA WHERE ESTADO_TUPLA = TRUE AND ACTIVO = TRUE;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label22.Text = reader.GetString(0);
                }
                else
                {
                    label22.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void cargaMercaderiaInactiva()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format(" SELECT COUNT( distinct ID_MERCADERIA) FROM MERCADERIA WHERE ESTADO_TUPLA = TRUE AND ACTIVO = FALSE;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    label20.Text = reader.GetString(0);

                }
                else
                {
                    label20.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        // GRAFICAS PARA GANANCIAS
        ArrayList Ganancia = new ArrayList();
        ArrayList Mes = new ArrayList();
        private void graficasGanancia()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT MONTHNAME(F.FECHA_EMISION_FACTURA) AS MES, TRUNCATE(SUM(F.TOTAL_FACTURA),2) AS TOTAL FROM FACTURA F WHERE(YEAR(F.FECHA_EMISION_FACTURA) = YEAR(NOW()) AND F.ESTADO_TUPLA = TRUE AND F.ESTADO_FACTURA = 1) GROUP BY MES DESC; ");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {            
                    Mes.Add(reader.GetString(0));
                    Ganancia.Add(reader.GetDouble(1));
                    while (reader.Read())
                    {
                        Mes.Add(reader.GetString(0));
                        Ganancia.Add(reader.GetDouble(1));
                    }
                    chart4.Series[0].Points.DataBindXY(Mes, Ganancia);
                }
                else
                {
                   // MessageBox.Show("NO EXISTEN DATOS PARA MOSTRAR GRAFICAS!", "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        ArrayList Ganancias = new ArrayList();
        ArrayList Meses = new ArrayList();
        private void graficasGananciaPeriodos()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                double total = 0;
                string sql = string.Format("SELECT * FROM VISTA_GANANCIA_PERIODO; ");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Meses.Add(reader.GetString(0));
                    total = total + (reader.GetDouble(2) - reader.GetDouble(1));
                    Ganancias.Add(reader.GetDouble(2) - reader.GetDouble(1));
                    while (reader.Read())
                    {
                        Meses.Add(reader.GetString(0));
                        total = total + (reader.GetDouble(2) - reader.GetDouble(1));
                        Ganancias.Add(reader.GetDouble(2) - reader.GetDouble(1));
                    }
                    //label26.Text = string.Format("Q.{0:###,###,###,##0.00##}", total);
                    chart2.Series[0].Points.DataBindXY(Meses, Ganancias);
                }
                else
                {
                   // MessageBox.Show("NO EXISTEN DATOS PARA MOSTRAR GRAFICAS!", "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        ArrayList fechas = new ArrayList();
        ArrayList gananciafecha = new ArrayList();
        private void gananciasPeriodos()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_GANANCIA_DIAS WHERE MES BETWEEN '{0}' AND '{1}';", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    double total = 0;
                    fechas.Add(reader.GetString(0));
                    total = total + (reader.GetDouble(2) - reader.GetDouble(1));
                    gananciafecha.Add(reader.GetDouble(2) - reader.GetDouble(1));
                   
                    while (reader.Read())
                    {
                        fechas.Add(reader.GetString(0));
                        total = total + (reader.GetDouble(2) - reader.GetDouble(1));
                        gananciafecha.Add(reader.GetDouble(2) - reader.GetDouble(1));
                    }
                    //label26.Text = string.Format("Q.{0:###,###,###,##0.00##}", total);
                    chart2.Series[0].Points.DataBindXY(fechas, gananciafecha);
                }
                else
                {
                    //MessageBox.Show("NO EXISTEN DATOS PARA MOSTRAR GRAFICAS!", "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.ToString() != dateTimePicker2.Value.ToString())
            {
                gananciasPeriodos();
            }
            else
            {
                MessageBox.Show("LOS DATOS DEBEN GENERARSE A MAS DE UN MES DE PLAZO!", "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        void PrintChart(object sender, PrintPageEventArgs ev)
        {
            using (var f = new System.Drawing.Font("Arial", 10))
            {
                var size = ev.Graphics.MeasureString(Text, f);
               
            }

            //Note, the chart printing code wants to print in pixels.
            Rectangle marginBounds = ev.MarginBounds;
            if (ev.Graphics.PageUnit != GraphicsUnit.Pixel)
            {
                ev.Graphics.PageUnit = GraphicsUnit.Pixel;
                marginBounds.X = (int)(marginBounds.X * (ev.Graphics.DpiX / 100f));
                marginBounds.Y = (int)(marginBounds.Y * (ev.Graphics.DpiY / 100f));
                marginBounds.Width = (int)(marginBounds.Width * (ev.Graphics.DpiX / 100f));
                marginBounds.Height = (int)(marginBounds.Height * (ev.Graphics.DpiY / 100f));
            }

            chart1.Printing.PrintPaint(ev.Graphics, marginBounds);
        }
        void PrintChartG(object sender, PrintPageEventArgs ev)
        {
            using (var f = new System.Drawing.Font("Arial", 10))
            {
                var size = ev.Graphics.MeasureString(Text, f);

            }

            //Note, the chart printing code wants to print in pixels.
            Rectangle marginBounds = ev.MarginBounds;
            if (ev.Graphics.PageUnit != GraphicsUnit.Pixel)
            {
                ev.Graphics.PageUnit = GraphicsUnit.Pixel;
                marginBounds.X = (int)(marginBounds.X * (ev.Graphics.DpiX / 100f));
                marginBounds.Y = (int)(marginBounds.Y * (ev.Graphics.DpiY / 100f));
                marginBounds.Width = (int)(marginBounds.Width * (ev.Graphics.DpiX / 100f));
                marginBounds.Height = (int)(marginBounds.Height * (ev.Graphics.DpiY / 100f));
            }

            chart4.Printing.PrintPaint(ev.Graphics, marginBounds);
        }
        void PrintChartP(object sender, PrintPageEventArgs ev)
        {
            using (var f = new System.Drawing.Font("Arial", 10))
            {
                var size = ev.Graphics.MeasureString(Text, f);

            }

            //Note, the chart printing code wants to print in pixels.
            Rectangle marginBounds = ev.MarginBounds;
            if (ev.Graphics.PageUnit != GraphicsUnit.Pixel)
            {
                ev.Graphics.PageUnit = GraphicsUnit.Pixel;
                marginBounds.X = (int)(marginBounds.X * (ev.Graphics.DpiX / 100f));
                marginBounds.Y = (int)(marginBounds.Y * (ev.Graphics.DpiY / 100f));
                marginBounds.Width = (int)(marginBounds.Width * (ev.Graphics.DpiX / 100f));
                marginBounds.Height = (int)(marginBounds.Height * (ev.Graphics.DpiY / 100f));
            }

            chart2.Printing.PrintPaint(ev.Graphics, marginBounds);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            var pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintChart);

            PrintPreviewDialog pdi = new PrintPreviewDialog();
            pdi.Document = pd;
            pdi.Document.DefaultPageSettings.Landscape= true;
            if (pdi.ShowDialog() == DialogResult.OK)
                pdi.Document.Print();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintChartG);

            PrintPreviewDialog pdi = new PrintPreviewDialog();
            pdi.Document = pd;
            pdi.Document.DefaultPageSettings.Landscape = true;
            if (pdi.ShowDialog() == DialogResult.OK)
                pdi.Document.Print();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var pd = new System.Drawing.Printing.PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(PrintChartP);

            PrintPreviewDialog pdi = new PrintPreviewDialog();
            pdi.Document = pd;
            pdi.Document.DefaultPageSettings.Landscape = true;
            if (pdi.ShowDialog() == DialogResult.OK)
                pdi.Document.Print();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm_reporteVenta frm = new frm_reporteVenta();
            CrystalReport11 cr = new CrystalReport11();
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.RefreshReport();
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var forma = new frm_SeleccionaSucursal(0);
            forma.ShowDialog();        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_reporteVenta frm = new frm_reporteVenta();
            CrystalReport14 cr = new CrystalReport14();
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.RefreshReport();
            frm.ShowDialog();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            frm_reporteVenta frm = new frm_reporteVenta();
            CrystalReport15 cr = new CrystalReport15();
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.RefreshReport();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frm_reporteVenta frm = new frm_reporteVenta();
            CrystalReport16 cr = new CrystalReport16();
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.RefreshReport();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frm_reporteVenta frm = new frm_reporteVenta();
            CrystalReport15 cr = new CrystalReport15();
           // cr.ExportToHttpResponse()
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.RefreshReport();
            frm.ShowDialog();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var forma = new frm_SeleccionaSucursal(1);
            forma.ShowDialog();
        }

        private void button4_Click_2(object sender, EventArgs e)
        {
            var forma = new frm_SeleccionaSucursal(2);
            forma.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            frm_reporteVenta frm = new frm_reporteVenta();
            CrystalReport27 cr = new CrystalReport27();
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.RefreshReport();
            frm.ShowDialog();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            frm_reporteVenta frm = new frm_reporteVenta();
            CrystalReport29 cr = new CrystalReport29();
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.RefreshReport();
            frm.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var fomra = new frm_fechasReportes(nameUs, 1);
            fomra.ShowDialog();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //indexer 2 para ventas
            var fomra = new frm_fechasReportes(nameUs,4);
            fomra.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        { 
            // indexer dos para proeedores
            var fomra = new frm_fechasReportes(nameUs, 0);
            fomra.ShowDialog();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var forma = new frm_SeleccionaSucursal(0);
            forma.ShowDialog();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            frm_reporteVenta frm = new frm_reporteVenta();
            CrystalReport26 cr = new CrystalReport26();
            TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["fecha"];
            text.Text = comboBox2.Text;
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.RefreshReport();
            frm.ShowDialog();
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            if(comboBox2.Text == "TODAS LAS SUCURSALES")
            {
                totalEfectivo();
                totalCredito();
                ganancias();
                countCreditos();
                countEfectivo();
                cargaClientes();
                cargaProveedores();
                cargaMercaderia();
                cargaMercaderiaInactiva();
            } else
            {
                credito = 0;
                efectivo = 0;
                totalFacturasSucursal(comboBox2.Text);
                totalEfectivoSucursal(comboBox2.Text);
                totalCreditoSucursal(comboBox2.Text);
                countCreditosSucursal(comboBox2.Text);
                countEfectivoSucursal(comboBox2.Text);
                gananciasSucursal(comboBox2.Text);
                cargaMercaderiaSucursal(comboBox2.Text);
                cargaMercaderiaInactivaSucursal(comboBox2.Text);
            }
        }
        //  funciones que reciben la sucursal para filtrar por ese dato
        private void totalEfectivoSucursal(string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(TOTAL_FACTURA) FROM FACTURA WHERE ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'EFECTIVO' AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL ='{0}') AND FECHA_EMISION_FACTURA >= curdate();", sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label3.Text = "0";
                    }
                    else
                    {
                        efectivo = reader.GetDouble(0);
                        label3.Text = string.Format("Q.{0:###,###,###,##0.00##}", efectivo);
                    }
                }
                else
                {
                    label3.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // creditos suma con sucural
        private void totalCreditoSucursal(string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(TOTAL_FACTURA) FROM FACTURA WHERE ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'CREDITO' AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL ='{0}') AND FECHA_EMISION_FACTURA >= curdate();" , sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label6.Text = "0";
                    }
                    else
                    {
                        credito = reader.GetDouble(0);
                        label6.Text = string.Format("Q.{0:###,###,###,##0.00##}", credito);
                    }
                }
                else
                {
                    label6.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // cuenta facturas al credito de detemrinada sucursal
        private void countCreditosSucursal(string nombre)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT COUNT(ID_FACTURA) FROM FACTURA WHERE ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND FECHA_EMISION_FACTURA >= CURDATE() AND TIPO_FACTURA = 'EFECTIVO' AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{0}');", nombre);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label12.Text = reader.GetString(0);
                }
                else
                {
                    label12.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // cuenta facturas al efectivo de deterinada sucursal
        private void countEfectivoSucursal(string nombre)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT COUNT(ID_FACTURA) FROM FACTURA WHERE ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND FECHA_EMISION_FACTURA >= CURDATE() AND TIPO_FACTURA = 'CREDITO' AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{0}');", nombre);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    label10.Text = reader.GetString(0);

                }
                else
                {
                    label10.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void gananciasSucursal(string nombre)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(VENTA - COMPRA) FROM GANANCIAS_DIARIO WHERE NOMBRE_SUCURSAL = '{0}';",nombre);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label8.Text = "0";
                    }
                    else
                    {
                        double totalParcial = reader.GetDouble(0);
                        label8.Text = string.Format("Q.{0:###,###,###,##0.00##}", Math.Round(totalParcial, 2));
                    }
                }
                else
                {
                    label8.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // total de facturas en sucurales
        private void totalFacturasSucursal(string nombre)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT COUNT(ID_FACTURA) FROM FACTURA WHERE ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND FECHA_EMISION_FACTURA >= curdate() AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{0}');", nombre);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label2.Text = reader.GetString(0);
                }
                else
                {
                    label2.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // mercaderia en la sucursal
        private void cargaMercaderiaSucursal(string nombre)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format(" SELECT COUNT( distinct ID_MERCADERIA) FROM MERCADERIA WHERE ESTADO_TUPLA = TRUE AND ACTIVO = TRUE AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{0}');", nombre);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    label22.Text = reader.GetString(0);

                }
                else
                {
                    label22.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // cargamercaderia inactiva de sucural
        private void cargaMercaderiaInactivaSucursal(string nombre)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format(" SELECT COUNT( distinct ID_MERCADERIA) FROM MERCADERIA WHERE ESTADO_TUPLA = TRUE AND ACTIVO = FALSE AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{0}');", nombre);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    label20.Text = reader.GetString(0);

                }
                else
                {
                    label20.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            frm_reporteVenta frm = new frm_reporteVenta();
            CrystalReport26 cr = new CrystalReport26();
            TextObject text = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["sucursal"];
            text.Text = comboBox2.Text;
            // fecha de los datos
            TextObject fecha = (TextObject)cr.ReportDefinition.Sections["Section1"].ReportObjects["fecha"];
            if (dateTimePicker4.Value.ToString() == dateTimePicker1.Value.ToString())
            {
                fecha.Text = dateTimePicker4.Value.ToString("dd/MM/yyyy");
            } else
            {
                fecha.Text = "Del: "+ dateTimePicker4.Value.ToString("dd/MM/yyyy") + " al: " + dateTimePicker4.Value.ToString("dd/MM/yyyy");
            }
            // usuario que imprime el reportes
            TextObject usu = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["usuario"];
            usu.Text = nameUs;
            // sucursal de lso registros
            TextObject sucursasles = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["registros"];
            sucursasles.Text = comboBox2.Text;
            // fecha de impreion 
            TextObject fechas = (TextObject)cr.ReportDefinition.Sections["Section2"].ReportObjects["fdatos"];
            if (dateTimePicker4.Value.ToString() == dateTimePicker1.Value.ToString())
            {
                fechas.Text = dateTimePicker4.Value.Date.ToString("dd/MM/yyyy");
            }
            else
            {
                fechas.Text = "Reporte Generado del: " + dateTimePicker4.Value.Date.ToString("dd/MM/yyyy") + " al: " + dateTimePicker4.Value.Date.ToString("dd/MM/yyyy");
            }
            // SECCION 3 DEL INFORME NO TOCAR NADA NO SE DESPLEGARA EN VARIAS HOJAS POR AL NUEVA RESTRICCION

            // TOTAL DE LAS FACTURAS EN CREDITO
            TextObject credito = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["c_credito"];
            credito.Text = label10.Text;
            // TOTAL DE LAS FACTURASN EN EFECTIVO
            TextObject efectivo = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["c_efectivo"];
            efectivo.Text = label12.Text;
            // MONTO DE LA FACTUAS EN CREDITO
            TextObject t_credito = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["t_credito"];
            t_credito.Text = label3.Text;
            // MONTO DE LAS FACTURAS EN EFECTIVO
            TextObject t_efectivo = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["t_efectivo"];
            t_efectivo.Text = label6.Text;
            // TOTAL DEL CALCULO DE GANANCIAS
            TextObject ganancia = (TextObject)cr.ReportDefinition.Sections["Section3"].ReportObjects["ganancia"];
            ganancia.Text = label8.Text;

            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.RefreshReport();
            frm.Show();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            if(dateTimePicker3.Value.Date < dateTimePicker4.Value.Date)
            {
                dateTimePicker3.Value = DateTime.Now;
                MessageBox.Show("LA FECHA NO PUEDE SER MENOR A LA FECHA DE PARTIDA", "REPORTE DE VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker4.Value.Date > dateTimePicker3.Value.Date)
            {
                dateTimePicker4.Value = DateTime.Now;
                MessageBox.Show("LA FECHA NO PUEDE SER MAYOR A LA FECHA DE DESTINO", "REPORTE DE VENTAS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("¿ESTA SEGURO QUE DESEA RESTABLECER LOS DATOS A LA FECHA ACTUAL?", "TRASLADO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                comboBox2.Text = "TODAS LAS SUCURSALES";
                dateTimePicker4.Value = DateTime.Now;
                dateTimePicker3.Value = DateTime.Now;
            }
        }

        private void button20_Click_1(object sender, EventArgs e)
        {
            if (comboBox2.Text == "TODAS LAS SUCURSALES")
            {
                if (dateTimePicker4.Value.Date == dateTimePicker3.Value.Date)
                {
                    totalEfectivo();
                    totalCredito();
                    ganancias();
                    countCreditos();
                    countEfectivo();
                    cargaClientes();
                    cargaProveedores();
                    cargaMercaderia();
                    cargaMercaderiaInactiva();
                }
                else
                {
                    countCreditosFechas(dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                    countEfectivoFechas(dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                    totalEfectivoFechas(dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                    totalCreditoFechas(dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                    gananciasFechas(dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                }
            } else
            {
                //HAY SLEECIIONADA UNA SUCURSAL EN ESPECIFICO
                if (dateTimePicker4.Value.Date == dateTimePicker3.Value.Date)
                {
                    credito = 0;
                    efectivo = 0;
                    totalFacturasSucursal(comboBox2.Text);
                    totalEfectivoSucursal(comboBox2.Text);
                    totalCreditoSucursal(comboBox2.Text);
                    countCreditosSucursal(comboBox2.Text);
                    countEfectivoSucursal(comboBox2.Text);
                    gananciasSucursal(comboBox2.Text);
                    cargaMercaderiaSucursal(comboBox2.Text);
                    cargaMercaderiaInactivaSucursal(comboBox2.Text);
                } else
                {
                    countCreditosFechasSucursal(dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                    countEfectivoFechasSucursal(dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                    totalEfectivoFechasSucursal(dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                    totalCreditoFechasSucursal(dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                    gananciasFechasSucursal(dateTimePicker4.Value.ToString("yyyy-MM-dd"), dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                }
            }
            
        }
        // ************************FUNCIONES QUE BUSCARAN LOS DATSO DEPNDEINDO DE LAS FECHAS Y SUCURSALES *********************



        // CONTADOR DE FACTURAS  AL EFECTIVO EN ESE PERIODOD DE TIEMPO OJO RECIBE PARAMETROS DE FECHAS EN TIPO STRING Y SUCURSAL SELECCIONADA
        private void countCreditosFechasSucursal(string desde, string hasta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT COUNT(F.ID_FACTURA) FROM FACTURA F WHERE F.FECHA_EMISION_FACTURA BETWEEN '{0}' AND '{1}' AND F.ESTADO_TUPLA = TRUE AND F.ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'CREDITO' AND F.ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{2}');", desde, hasta, comboBox2.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label10.Text = reader.GetString(0);
                }
                else
                {
                    label10.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // CONTADOR DE FACTURAS EN EFECTIVO PARA MOSTRAR EL TOTAL EN LAS FECHAS RECIBE DOS PARAMETROS DEL BOOTN DE GENRAR
        private void countEfectivoFechasSucursal(string desde, string hasta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT COUNT(F.ID_FACTURA) FROM FACTURA F WHERE F.FECHA_EMISION_FACTURA BETWEEN '{0}' AND '{1}' AND F.ESTADO_TUPLA = TRUE AND F.ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'EFECTIVO' AND F.ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{2}');", desde, hasta, comboBox2.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    label12.Text = reader.GetString(0);

                }
                else
                {
                    label12.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // FUNCION QUE PERMITE OBTNEER EL TOTAL DE LAS FACTURAS EN EFECTIVO DE LAS FECHAS
        private void totalEfectivoFechasSucursal(string desde, string hasta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(TOTAL_FACTURA) FROM FACTURA WHERE FECHA_EMISION_FACTURA BETWEEN '{0}' AND '{1}' AND ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'EFECTIVO' AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{2}');", desde, hasta, comboBox2.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label3.Text = "0";
                    }
                    else
                    {
                        efectivo = reader.GetDouble(0);
                        label3.Text = string.Format("Q.{0:###,###,###,##0.00##}", efectivo);
                    }
                }
                else
                {
                    label3.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // FUNCION QUE PERMITE OBTENER EL TOTAL DE LAS FACTURAS EN CREDITOS
        private void totalCreditoFechasSucursal(string desde, string hasta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(TOTAL_FACTURA) FROM FACTURA WHERE FECHA_EMISION_FACTURA BETWEEN '{0}' AND '{1}' AND ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'CREDITO' AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{2}');", desde, hasta,comboBox2.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label6.Text = "0";
                    }
                    else
                    {
                        credito = reader.GetDouble(0);
                        label6.Text = string.Format("Q.{0:###,###,###,##0.00##}", credito);
                    }
                }
                else
                {
                    label6.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // FUNCION QUE OBTIENE EL TOTAL DE GANANCIAS DE EL NEGOCIO E BASE A LAS FECHAS KSJDKLFJSKLJDF JAJAJAJA! CON SUCURAL AHORA
        private void gananciasFechasSucursal(string desde, string hasta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(VENTA - COMPRA) FROM GANANCIAS_FECHAS_SUCURSAL WHERE FECHA_EMISION_FACTURA BETWEEN '{0}' AND '{1}' AND NOMBRE_SUCURSAL = '{2}';", desde, hasta, comboBox2.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label8.Text = "0";
                    }
                    else
                    {
                        double totalParcial = reader.GetDouble(0);

                        label8.Text = string.Format("Q.{0:###,###,###,##0.00##}", Math.Round(totalParcial, 2));
                    }
                }
                else
                {
                    label8.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }

        //  ************************FUNCIONES QUE BUSCARAN LOS DATOS DEPNDIENDO DE LAS FECHAS********************************

        // CONTADOR DE FACTURAS  AL EFECTIVO EN ESE PERIODOD DE TIEMPO OJO RECIBE PARAMETROS DE FECHAS EN TIPO STRING
        private void countCreditosFechas(string desde, string hasta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT COUNT(F.ID_FACTURA) FROM FACTURA F WHERE F.FECHA_EMISION_FACTURA BETWEEN '{0}' AND '{1}' AND F.ESTADO_TUPLA = TRUE AND F.ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'CREDITO';", desde, hasta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label10.Text = reader.GetString(0);
                }
                else
                {
                    label10.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // CONTADOR DE FACTURAS EN EFECTIVO PARA MOSTRAR EL TOTAL EN LAS FECHAS RECIBE DOS PARAMETROS DEL BOOTN DE GENRAR
        private void countEfectivoFechas(string desde, string hasta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT COUNT(F.ID_FACTURA) FROM FACTURA F WHERE F.FECHA_EMISION_FACTURA BETWEEN '{0}' AND '{1}' AND F.ESTADO_TUPLA = TRUE AND F.ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'EFECTIVO';", desde, hasta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    label12.Text = reader.GetString(0);

                }
                else
                {
                    label12.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // FUNCION QUE PERMITE OBTNEER EL TOTAL DE LAS FACTURAS EN EFECTIVO DE LAS FECHAS
        private void totalEfectivoFechas(string desde, string hasta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(TOTAL_FACTURA) FROM FACTURA WHERE FECHA_EMISION_FACTURA BETWEEN '{0}' AND '{1}' AND ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'EFECTIVO';", desde, hasta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label3.Text = "0";
                    }
                    else
                    {
                        efectivo = reader.GetDouble(0);
                        label3.Text = string.Format("Q.{0:###,###,###,##0.00##}", efectivo);
                    }
                }
                else
                {
                    label3.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // FUNCION QUE PERMITE OBTENER EL TOTAL DE LAS FACTURAS EN CREDITOS
        private void totalCreditoFechas(string desde, string hasta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(TOTAL_FACTURA) FROM FACTURA WHERE FECHA_EMISION_FACTURA BETWEEN '{0}' AND '{1}' AND ESTADO_TUPLA = TRUE AND ESTADO_FACTURA = 1 AND TIPO_FACTURA = 'CREDITO';", desde, hasta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label6.Text = "0";
                    }
                    else
                    {
                        credito = reader.GetDouble(0);
                        label6.Text = string.Format("Q.{0:###,###,###,##0.00##}", credito);
                    }
                }
                else
                {
                    label6.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        // FUNCION QUE OBTIENE EL TOTAL DE GANANCIAS DE EL NEGOCIO E BASE A LAS FECHAS KSJDKLFJSKLJDF JAJAJAJA!
        private void gananciasFechas(string desde , string hasta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT SUM(COMPRA - VENTA) FROM GANANCIAS_FECHAS WHERE FECHA_EMISION_FACTURA BETWEEN '{0}' AND '{1}';", desde, hasta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        label8.Text = "0";
                    }
                    else
                    {
                        double totalParcial = reader.GetDouble(0);

                        label8.Text = string.Format("Q.{0:###,###,###,##0.00##}", Math.Round(totalParcial,2));
                    }
                }
                else
                {
                    label8.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            var fomra = new frm_fechasReportes(nameUs, 2);
            fomra.ShowDialog();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //indexer 3 para COBOROS
            var fomra = new frm_fechasReportes(nameUs, 3);
            fomra.ShowDialog();
        }
    }
}
 