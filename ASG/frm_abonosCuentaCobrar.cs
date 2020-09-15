﻿using System;
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
    public partial class frm_abonosCuentaCobrar : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string numeroCuenta;
        double abonado;
        double totalFactura;
        string rolUsuario;
        bool flag = false;
        string codigoCliente;
        string codigoSucursal;
  
        bool estadoC;
        string codigoCaja;
        string nombreUsuarioReal;
        bool[] privilegios;
        string nombreUsuario;
        ContextMenuStrip mymenu = new ContextMenuStrip();
        public frm_abonosCuentaCobrar(string codigo, string nombre, string numero, string emision, string vencimiento, string cuenta, bool  estado, string total, string rol, string sucursal, string usuario, string caja, string nombreUsuarios, bool[] privilegio)
        {
            InitializeComponent();
            label2.Text = codigo;
            label6.Text = nombre;
            label11.Text = numero;
            label8.Text = emision;
            this.privilegios = privilegio;
            label10.Text = vencimiento;
            nombreUsuarioReal = nombreUsuarios;
            nombreUsuario = usuario;
            numeroCuenta = cuenta;
            codigoCliente = codigo;
            rolUsuario = rol;
            codigoSucursal = sucursal;
            nombreUsuario = usuario;
            stripMenu();
            numeroCuenta = cuenta;
            totalFactura = Convert.ToDouble(total);
            label15.Text = total;
            estadoC = estado;
            cargaDatos();
            getAbonos();
            codigoCaja = caja;
            setterForm();
            if (rolUsuario != "ADMINISTRADOR")
            {
                button1.Enabled = false;
                mymenu.Items[1].Enabled = false;
            }
            if (estado == true)
            {
                label19.Text = "NO CANCELADO";
                label19.BackColor = Color.OrangeRed;
            }
            else
            {
                label19.Text = "CANCELADO";
                label19.BackColor = Color.Blue;
                //button8.Enabled = false;
            }
            dataGridView1.Focus();
        }
        private void stripMenu()
        {
            mymenu.Items.Add("Ocultar Fila");
            mymenu.Items[0].Name = "ColHidden";
            mymenu.Items.Add("Eliminar Abono Cliente");
            mymenu.Items[1].Name = "ColEdit";
           
        }

        private void cargaDatos()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_ABONOS WHERE ID_CUENTA_POR_COBRAR = {0};", numeroCuenta);
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
        private void setterForm()
        {
            double balance = totalFactura - abonado;
            label23.Text = string.Format("{0:###,###,###,##0.00##}", balance);
        }
        private void obtienResCaja(string numero, string codigo)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                double temp = Convert.ToDouble(numero);
                string sql = string.Format("UPDATE CAJA SET TOTAL_FACTURADO_CAJA = TOTAL_FACTURADO_CAJA - {0} WHERE ID_CAJA = {1};", temp, codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {

                }
                else
                {
                    MessageBox.Show("IIMPOSIBLE ACTUALIZAR CAJA!", "GESTION CAJA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();

        }
        private void getAbonos()
        {
            if(dataGridView1.RowCount > 0)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    abonado = abonado + Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
                }
                label17.Text = string.Format("{0:###,###,###,##0.00##}", abonado);
            } else
            {
                label17.Text = "0";
            }
            
        }
        private void frm_abonosCuentaCobrar_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void frm_abonosCuentaCobrar_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void frm_abonosCuentaCobrar_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_abonosCuentaCobrar_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_abonosCuentaCobrar_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }
        private void my_menu_Itemclicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (flag == false)
            {
                if (e.ClickedItem.Name == "ColHidden")
                {
                    dataGridView1.Rows[this.dataGridView1.CurrentRow.Index].Visible = false;
                    flag = true;
                }
                else if (e.ClickedItem.Name == "ColEdit")
                {
                   
                    mymenu.Visible = false;
                    button1.PerformClick();
                    flag = true;

                }
                
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    mymenu.Show(dataGridView1, new Point(e.X, e.Y));
                    mymenu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_Itemclicked);
                    mymenu.Enabled = true;
                    flag = false;
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                double cast = Convert.ToDouble(label23.Text);
                //MessageBox.Show("" + cast);
                var forma = new frm_abono(dataGridView1.CurrentRow.Cells[1].Value.ToString(), cast, codigoCliente, true,codigoSucursal,nombreUsuario, codigoCaja);
                if (forma.ShowDialog() == DialogResult.OK)
                {
                    cargaDatos();
                    abonado = 0;
                    getAbonos();
                    setterForm();
                    
                }
            }
        }
        private void updateDisponible(double abono)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE CLIENTE SET CREDITO_DISPONIBLE = CREDITO_DISPONIBLE - {0} WHERE ID_CLIENTE = '{1}';", abono, codigoCliente);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    if (estadoC)
                    {
                        var forma = new frm_creditoActualizado();
                        forma.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("IMPOSILE ACTUALIZAR EL CREDITO DEL CLIENTE!", "CUENTAS POR COBRAR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void updateAbono(double abono)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                
                string sql = string.Format("UPDATE CUENTA_POR_COBRAR_CLIENTE SET TOTAL_PAGADO = TOTAL_PAGADO - {0} WHERE ID_CUENTA_POR_COBRAR = {1};", abono, dataGridView1.CurrentRow.Cells[1].Value.ToString());
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    updateDisponible(abono);
                }
                else
                {
                    MessageBox.Show("IMPOSILE ACTUALIZAR LOS ABONOS DEL CLIENTE!", "CUENTA POR COBRAR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void eliminaPago(string codigoPago)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE PAGO_CLIENTE SET ESTADO_TUPLA = FALSE WHERE ID_PAGO = {0};", codigoPago);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    updateAbono(Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value.ToString()));
                }
                else
                {
                    MessageBox.Show("IMPOSIBLE ELIMINAR EL ABONO!", "CUENTAS POR PAGAR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }

        private string obtieneCajaFactura(string cuenta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT ID_CAJA FROM DET_CAJA WHERE ID_FACTURA = (SELECT ID_FACTURA FROM CUENTA_POR_COBRAR_CLIENTE WHERE ID_CUENTA_POR_COBRAR = {0});", cuenta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
                else
                {
                    MessageBox.Show("IMPOSIBLE OBTENER CODIGO DE CAJA!", "CUENTAS POR COBRAR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
            return null;
        }
        private void eliminaFactura(string codigo)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE DET_CAJA SET ESTADO_TUPLA = FALSE WHERE ID_CAJA = {0};", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                   
                }
                else
                {
                    MessageBox.Show("IMPOSIBLE ELIMINAR LA FACTURA DE LA CAJA!", "CUENTAS POR COBRAR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private bool revisaCaja()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT ID_CAJA FROM CAJA WHERE ID_USUARIO = '{0}' AND ID_SUCURSAL = {1} AND ESTADO_TUPLA = TRUE;", nombreUsuario, codigoSucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    conexion.Close();
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
      
        private void obtieneCaja()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT ID_CAJA FROM CAJA WHERE ID_SUCURSAL = {0} AND ID_USUARIO = '{1}' AND ESTADO_TUPLA = TRUE AND ESTADO_CAJA = TRUE;", codigoSucursal, nombreUsuario);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    codigoCaja = reader.GetString(0);
                }
                else
                {
                    aperturaCaja();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void aperturaCaja()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("INSERT INTO CAJA VALUES (NULL,{0},'{1}',NOW(),{2},NOW(),{2},'APERTURADA',TRUE,TRUE);", codigoSucursal, nombreUsuario, 0);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    obtieneCaja();
                }
                else
                {
                    MessageBox.Show("NO SE PUDO GENERAR NUEVA ORDEN DE COMPRA!", "CAJA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
       
        private bool obtieneAjustes()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT CAJA FROM CONFIGURACION_SISTEMA WHERE IDCONFIGURACION = 111 AND CAJA = TRUE;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    conexion.Close();
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
       
       
        private double totalfactura(string cuenta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT TOTAL_FACTURA FORM FACTURA WHERE ID_FACTURA = (SELECT ID_FACTURA FROM CUENTA_POR_COBRAR_CLIENTE WHERE ID_CUENTA_POR_COBRAR = {0});", cuenta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    conexion.Close();
                    return reader.GetDouble(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
            return 0;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                if (estadoC)
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR EL ABONO DEL CLIENTE?", "CUENTAS POR COBRAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        eliminaPago(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        obtienResCaja(dataGridView1.CurrentRow.Cells[5].Value.ToString(), codigoCaja);
                        adressUser.ingresaBitacora(codigoSucursal, nombreUsuario, "ELIMINA PAGO PARCIAL", codigoCliente);
                        string codigoC = obtieneCajaFactura(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        //MessageBox.Show(codigoCaja + " " + "codigo actual");
                        //MessageBox.Show(codigoC + " " + "codigo anterior");
                        if (codigoC != codigoCaja)
                        {
                            MessageBox.Show("LA FACTURA PERTENECE A OTRO CORTE DE CAJA, LA FACTURA SERA ELIMINADA Y EL MONTO SERA RESTADO DEL TOTAL FACTURADO DE LA CAJA ACTUAL!", "CUENTAS POR COBRAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            obtienResCaja(dataGridView1.CurrentRow.Cells[5].Value.ToString(), codigoC);

                        }
                        cargaDatos();
                        abonado = 0;
                        getAbonos();
                        setterForm();
                    }
                }
                else
                {
                    DialogResult result;
                    result = MessageBox.Show("EL TOTAL DE LA FACTURA HA SIDO PAGADO! , SI ELIMINA EL ABONO DEL CLIENTE LA CUENTA SE ACTIVARA Y TENDRA UN SALDO PENDIENTE, ¿ESTA SEGURO QUE DESEA ELIMINAR EL ABONO DEL CLIENTE?", "CUENTAS POR COBRAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        eliminaPago(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        obtienResCaja(dataGridView1.CurrentRow.Cells[5].Value.ToString(), codigoCaja);
                        adressUser.ingresaBitacora(codigoSucursal, nombreUsuario, "ELIMINA PAGO PARCIAL", codigoCliente);
                        string codigoC = obtieneCajaFactura(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        //MessageBox.Show(codigoCaja + " " + "codigo actual");
                        //MessageBox.Show(codigoC + " " + "codigo anterior");
                        if (codigoC != codigoCaja)
                        {
                            MessageBox.Show("LA FACTURA PERTENECE A OTRO CORTE DE CAJA, LA FACTURA SERA ELIMINADA Y EL MONTO SERA RESTADO DEL TOTAL FACTURADO DE LA CAJA ACTUAL!", "CUENTAS POR COBRAR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            obtienResCaja(dataGridView1.CurrentRow.Cells[5].Value.ToString(), codigoC);
                        }
                       
                        activaCuenta();
                        cargaDatos();
                        abonado = 0;
                        getAbonos();
                        setterForm();
                    }
                }
            }
            else
            {
                MessageBox.Show("NO EXISTEN ABONOS A LAS CUALES ELIMINAR UN SALDO!", "CUENTAS POR COBRAR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void activaCuenta()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE CUENTA_POR_COBRAR_CLIENTE SET ESTADO_TUPLA = TRUE WHERE ID_CUENTA_POR_COBRAR = {0};", numeroCuenta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    var forma = new frm_creditoActualizado();
                    forma.ShowDialog();
                }
                else
                {
                    MessageBox.Show("IMPOSIBLE ACTIVAR DE NUEVO LA CUENTA", "CUENTAS POR COBRAR", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Delete)
            {
                button1.PerformClick();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_abonosCuentaCobrar_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }
    }
}
