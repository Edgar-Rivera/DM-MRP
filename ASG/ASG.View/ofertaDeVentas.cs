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
    public partial class ofertaDeVentas : Form
    {
        ContextMenuStrip mymenuSV = new ContextMenuStrip();
        string nameUs;
        string rolUs;
        string usuario;
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string idSucursal;
        string nombreSucursal;
        bool[] privilegios;
        bool flagsc = false;
        bool existente = false;
        double total = 0;
        double subtotal = 0;
        double descuento = 0;
        ContextMenuStrip mymenuS = new ContextMenuStrip();
        public ofertaDeVentas(string nameUser, string rolUser, string user, string sucursal, string nombre, bool[] privilegio)
        {
            InitializeComponent();
            usuario = user;
            label19.Text = label19.Text + nameUser;
            nameUs = nameUser;
            rolUs = rolUser;
            nombreSucursal = nombre;
            idSucursal = sucursal;
            this.privilegios = privilegio;
            stripMenuSC();
            stripMenuST();
        }
        private bool actualizaDetalleProveedor(string detalle)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE DETALLE_COTIZACION SET ESTADO_TUPLA = FALSE WHERE ID_DET_FACTURA = {0};", detalle);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }
        private void timerActions()
        {
            frm_done frm = new frm_done();
            frm.ShowDialog();
            timer1.Interval = 1500;
            timer1.Enabled = true;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void stripMenuSC()
        {
            mymenuS.Items.Add("Eliminar Producto de la Cotizacion");
            mymenuS.Items[0].Name = "ColHidden";
        }
        private void stripMenuST()
        {
            mymenuSV.Items.Add("Ocultar Fila");
            mymenuSV.Items[0].Name = "ColHidden";
            mymenuSV.Items.Add("Editar Cotización");
            mymenuSV.Items[1].Name = "ColEdit";
            mymenuSV.Items.Add("Eliminar Cotización");
            mymenuSV.Items[2].Name = "ColDelete";
            mymenuSV.Items.Add("Ver Detalle de Cotización");
            mymenuSV.Items[3].Name = "ColView";
            mymenuSV.Items.Add("Imprimir Cotizacion");
            mymenuSV.Items[4].Name = "ColImp";
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ofertaDeVentas_Load(object sender, EventArgs e)
        {

        }

        private void ofertaDeVentas_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void ofertaDeVentas_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void ofertaDeVentas_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void ofertaDeVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void totalFactura()
        {
            if (dataGridView6.RowCount > 0)
            {
                subtotal = 0;
                for (int i = 0; i < dataGridView6.RowCount; i++)
                {
                    if (dataGridView6.Rows[i].Cells[5].Value != null)
                    {
                        subtotal = subtotal + Convert.ToDouble(dataGridView6.Rows[i].Cells[5].Value.ToString());
                    }
                }
                subtotal = Math.Round(subtotal, 2, MidpointRounding.ToEven);
                label50.Text = string.Format("{0:###,###,###,##0.00##}", subtotal);
                if (textBox33.Text != "" && textBox33.Text != "0")
                {
                    descuento = subtotal * (Convert.ToDouble(textBox33.Text) / 100);
                    total = subtotal - descuento;
                    Math.Round(descuento, 2);
                    Math.Round(subtotal, 2);
                    Math.Round(total, 2);
                    total = Math.Round(total, 2, MidpointRounding.ToEven);
                    label51.Text = string.Format("{0:###,###,###,##0.00##}", total);
                }
                else
                {
                    total = subtotal;
                    Math.Round(subtotal, 2);
                    Math.Round(total, 2);
                    total = Math.Round(total, 2, MidpointRounding.ToEven);
                    label51.Text = string.Format("{0:###,###,###,##0.00##}", subtotal);
                }
            }
            else
            {
                total = 0;
                subtotal = 0;
                descuento = 0;
                label50.Text = "0";
                label51.Text = "0";
                textBox33.Text = "0";
            }
        }
        private bool actualizaDetalle(string detalle)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE DET_FACTURA SET ESTADO_TUPLA = FALSE WHERE ID_DET_FACTURA = {0};", detalle);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }
        private void my_menu_ItemclickedSC(object sender, ToolStripItemClickedEventArgs e)
        {
            if (flagsc == false)
            {
                if (e.ClickedItem.Name == "ColHidden")
                {
                    if (dataGridView6.RowCount > 0)
                    {
                        if (comboBox7.Text == "CLIENTES")
                        {
                            mymenuS.Visible = false;
                            DialogResult result;
                            result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR EL PRODUCTO DE LA COTIZACION?", "COTIZACIONES", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (!existente)
                                {
                                    dataGridView6.Rows.RemoveAt(dataGridView6.CurrentRow.Index);
                                    totalFactura();
                                }
                                else
                                {
                                    if (actualizaDetalle(dataGridView6.CurrentRow.Cells[9].Value.ToString()))
                                    {
                                        dataGridView6.Rows.RemoveAt(dataGridView6.CurrentRow.Index);
                                        totalFactura();
                                        timerActions();
                                    }
                                    else
                                    {
                                        MessageBox.Show("EL PRODUCTO NO PUDO SER ELIMINADO DE LA FACTURA", "COTIZACIONES EXISTENTE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }

                        }
                        else if (comboBox7.Text == "PROVEEDORES")
                        {
                            mymenuS.Visible = false;
                            DialogResult result;
                            result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR EL PRODUCTO DE LA COTIZACION?", "COTIZACIONES", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (result == System.Windows.Forms.DialogResult.Yes)
                            {
                                if (!existente)
                                {
                                    dataGridView6.Rows.RemoveAt(dataGridView6.CurrentRow.Index);
                                    totalFactura();
                                }
                                else
                                {
                                    if (actualizaDetalleProveedor(dataGridView6.CurrentRow.Cells[9].Value.ToString()))
                                    {

                                        dataGridView6.Rows.RemoveAt(dataGridView6.CurrentRow.Index);
                                        totalFactura();
                                        timerActions();
                                    }
                                    else
                                    {
                                        MessageBox.Show("EL PRODUCTO NO PUDO SER ELIMINADO DE LA COTIZACION", "COTIZACIONES EXISTENTE", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }

                        }
                        flagsc = true;
                    }
                }

            }
        }
        private void dataGridView6_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView6.RowCount > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    mymenuS.Show(dataGridView6, new Point(e.X, e.Y));
                    mymenuS.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemclickedSC);
                    mymenuS.Enabled = true;
                    flagsc = false;
                }
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (comboBox7.Text == "CLIENTES")
            {
                var forma = new frm_buscaCliente(nameUs, rolUs, usuario, idSucursal);
                forma.ShowDialog();
                if (forma.DialogResult == DialogResult.OK)
                {
                    textBox30.Text = forma.currentCliente.getCliente;
                   // datosCliente(forma.currentCliente.getCliente);
                    comboBox8.SelectedIndex = 2;
                    DateTime today = DateTime.Now;
                    textBox31.Text = today.ToString("yyyy-MM-dd");

                    textBox32.Focus();
                }
            }
            else if (comboBox7.Text == "PROVEEDORES")
            {
                var forma = new frm_buscaProveedor(0, nameUs, rolUs, usuario, idSucursal);
                forma.ShowDialog();
                if (forma.DialogResult == DialogResult.OK)
                {
                    textBox30.Text = forma.currentProveedor.getProveedor;
                   // datosProveedor(forma.currentProveedor.getProveedor);
                    comboBox8.SelectedIndex = 1;
                    DateTime today = DateTime.Now;
                    textBox31.Text = today.ToString("yyyy-MM-dd");

                    textBox32.Focus();

                }
            }
            else
            {
                MessageBox.Show("NO HA SELECCIONADO TIPO DE COTIZACION", "COTIZACIONES", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void cargaCotizaciones()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView7.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_COTIZACION WHERE NOMBRE_SUCURSAL  = '{0}' AND ESTADO_TUPLA = TRUE ORDER BY FECHA_EMISION_FACTURA DESC;", comboBox9.Text);
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
        private void button30_Click(object sender, EventArgs e)
        {

        }
    }
}
