
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
using BarcodeLib;
using System.Drawing.Imaging;
using System.Security.Cryptography;



namespace ASG
{
    public partial class frm_compras : Form
    {
        string nameUs;
        string rolUs;
        string usuario;

        Point DragCursor;
        Point DragForm;
        bool Dragging;
        bool flag = false;
        bool flags = false;


 
        string idSucursal;

        string precio_ponderado;
        string ponderado_especias;
        ContextMenuStrip mymenu = new ContextMenuStrip();
        ContextMenuStrip mymenuS = new ContextMenuStrip();
        bool[] privilegios;

        public frm_compras(string nameUser, string rolUser, string user, string sucursal, bool[] privilegio)
        {
            InitializeComponent();
            usuario = user;
            nameUs = nameUser;
            rolUs = rolUser;
            idSucursal = sucursal;
            this.privilegios = privilegio;
            label19.Text = label19.Text + "" + nameUs;
            cargaSucursales();
            stripMenu();
            stripMenuS();
           
           
        }
       
        private void stripMenuS()
        {
            mymenuS.Items.Add("Ocultar Fila");
            mymenuS.Items[0].Name = "ColHidden";
            mymenuS.Items.Add("Eliminar Compra");
            mymenuS.Items[1].Name = "ColEdit";
            mymenuS.Items.Add("Ver Detalle de Compra");
            mymenuS.Items[2].Name = "ColDel";

        }
       
        private void deleteDatagridE()
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                conexion.Open();
                string sql = sql = string.Format("SELECT ID_STOCK FROM MERCADERIA_PRESENTACIONES WHERE ID_STOCK = '{0}';", dataGridView4.CurrentRow.Cells[0].Value.ToString());
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   // MessageBox.Show("se encontro en stock");
                    conexion.Close();
                    conexion = ASG_DB.connectionResult();
                    sql = sql = string.Format("UPDATE MERCADERIA_PRESENTACIONES SET ESTADO_TUPLA = FALSE, ID_STOCK = '{0}' WHERE ID_STOCK = '{1}';",getCode(), dataGridView4.CurrentRow.Cells[0].Value.ToString());
                    cmd = new OdbcCommand(sql, conexion);
                    if ((cmd.ExecuteNonQuery() == 1) | (cmd.ExecuteNonQuery() > 1) | ((cmd.ExecuteNonQuery() == 0)))
                    {
                        dataGridView4.Rows.RemoveAt(dataGridView4.CurrentRow.Index);
                    }
                    dataGridView4.Rows.RemoveAt(dataGridView4.CurrentRow.Index);
                }
                else
                {
                    dataGridView4.Rows.RemoveAt(dataGridView4.CurrentRow.Index);
                }
            }
            catch
          (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void deleteDatagrid()
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = sql = string.Format("SELECT ID_STOCK FROM MERCADERIA_PRESENTACIONES WHERE ID_STOCK = '{0}';", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   // MessageBox.Show("se encontro en stock");
                    conexion.Close();
                    conexion = ASG_DB.connectionResult();
                    sql = sql = string.Format("UPDATE MERCADERIA_PRESENTACIONES SET ESTADO_TUPLA = FALSE, ID_STOCK = '{0}' WHERE ID_STOCK = '{1}';", getCode(), dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    cmd = new OdbcCommand(sql, conexion);
                    if ((cmd.ExecuteNonQuery() == 1 )| (cmd.ExecuteNonQuery() > 1) | (cmd.ExecuteNonQuery() > 0))
                    {
                        //dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                    }
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                }
                else
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                }
            }
            catch
          (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void my_menu_ItemclickedE(object sender, ToolStripItemClickedEventArgs e)
        {
            if (flags == false)
            {
                if (e.ClickedItem.Name == "ColHidden")
                {
                    dataGridView4.Rows[this.dataGridView4.CurrentRow.Index].Visible = false;
                    flags = true;
                }
                else if (e.ClickedItem.Name == "ColEdit")
                {
                    if (dataGridView4.RowCount > 0)
                    {
                        DialogResult result;
                        result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR LA PRESENTACION DEL PRODUCTO?", "INGRESO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            deleteDatagridE();
                        }
                    }
                    flags = true;
                }
            }
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
                    if (dataGridView1.RowCount > 0)
                    {
                        DialogResult result;
                        result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR LA PRESENTACION DEL PRODUCTO?", "INGRESO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            deleteDatagrid();
                        }
                    }
                    flag = true;
                }
            }
        }
        public string createCode(int longitud)
        {
            string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }
        private void stripMenu()
        {
            mymenu.Items.Add("Ocultar Fila");
            mymenu.Items[0].Name = "ColHidden";
            mymenu.Items.Add("Eliminar Tipo de Venta");
            mymenu.Items[1].Name = "ColEdit";
            
        }
        private void generaBarcode(string barString)
        {
            /*BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
            Codigo.IncludeLabel = true;
            pictureBox1.Image = Codigo.Encode(BarcodeLib.TYPE.CODE128, barString);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;*/
        }
        private void cargaCompras()
        {
           
        }
        private void cargaProveedor()
        {
           
        }
        private void limpiaCompra()
        {
          
           
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "" | textBox8.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "INGRESO MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            } else
            {
                this.Close();
            }
        }
        private void frm_compras_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void frm_compras_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }
        private void frm_compras_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }
        private void getCalculosTotalesAbarrotes()
        {
            if (Convert.ToDouble(textBox4.Text) > 0)
            {
                // la cantidad del prudcto es maor a 0
                if (dataGridView1.RowCount > 0)
                {
                    double temp = Convert.ToDouble(textBox4.Text);
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        // CALCULO SE ESPERA LA VARIABLE DE TIPO DE VARIABLE PARA DIVIIR
                        
                        
                            double tempX = Math.Truncate(temp / Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                            if (tempX > 0)
                            {
                                dataGridView1.Rows[i].Cells[3].Value = string.Format("{0:###,###,###,##}", tempX);
                            }
                            else
                            {
                                dataGridView1.Rows[i].Cells[3].Value = "0";
                            }
                        
                    }
                }

            }
            else
            {
                // la cantidad disponible es mayor a 0
                if (dataGridView1.RowCount > 0)
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        dataGridView1.Rows[i].Cells[3].Value = "0";
                    }
                }
            }
        }
        private void getCalculosTotalesEspecias()
        {
            if (Convert.ToDouble(textBox11.Text) > 0)
            {
                // la cantidad del prudcto es maor a 0
                if (dataGridView4.RowCount > 0)
                {
                    double temp = Convert.ToDouble(textBox11.Text);
                    for (int i = 0; i < dataGridView4.RowCount; i++)
                    {
                        // CALCULO SE ESPERA LA VARIABLE DE TIPO DE VARIABLE PARA DIVIIR


                        double tempX = Math.Truncate(temp / Convert.ToDouble(dataGridView4.Rows[i].Cells[2].Value.ToString()));
                        if (tempX > 0)
                        {
                            dataGridView4.Rows[i].Cells[3].Value = string.Format("{0:###,###,###,##}", tempX);
                        }
                        else
                        {
                            dataGridView4.Rows[i].Cells[3].Value = "0";
                        }

                    }
                }

            }
            else
            {
                // la cantidad disponible es mayor a 0
                if (dataGridView4.RowCount > 0)
                {
                    for (int i = 0; i < dataGridView4.RowCount; i++)
                    {
                        dataGridView4.Rows[i].Cells[3].Value = "0";
                    }
                }
            }
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void preciosUtilidadesEspecias()
        {
            if (dataGridView4.RowCount > 0)
            {
                for (int i = 0; i < dataGridView4.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView4.ColumnCount; j++)
                    {
                        if ((j == 6) | (j == 7) | (j == 8) | (j == 9) | (j == 10))
                        {
                            if (dataGridView4.Rows[i].Cells[j].Value.ToString() != "0")
                            {
                                if (Convert.ToDouble(dataGridView4.Rows[i].Cells[j].Value.ToString()) < Convert.ToDouble(dataGridView4.Rows[i].Cells[5].Value.ToString()))
                                {
                                    // MessageBox.Show("EL PRECIO DE VENTA APLICADO ES MENOR AL PRECIO COSTO!", "INGRESO DE MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    dataGridView4.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(207, 136, 65);
                                    dataGridView4.Rows[i].Cells[j].Style.ForeColor = Color.White;
                                }
                                else
                                {
                                    dataGridView4.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                    dataGridView4.Rows[i].Cells[j].Style.BackColor = dataGridView4.Rows[i].DefaultCellStyle.BackColor;
                                }
                                double diferencial = Convert.ToDouble(dataGridView4.Rows[i].Cells[j].Value.ToString()) - Convert.ToDouble(dataGridView4.Rows[i].Cells[5].Value.ToString());
                                double porcentaje = Math.Round(diferencial / Convert.ToDouble(dataGridView4.Rows[i].Cells[5].Value.ToString()), 4) * 100;
                                if (j == 6)
                                {
                                    dataGridView4.Rows[i].Cells[11].Value = porcentaje;
                                }
                                else if (j == 7)
                                {
                                    dataGridView4.Rows[i].Cells[12].Value = porcentaje;
                                }
                                else if (j == 8)
                                {
                                    dataGridView4.Rows[i].Cells[13].Value = porcentaje;
                                }
                                else if (j == 9)
                                {
                                    dataGridView4.Rows[i].Cells[14].Value = porcentaje;
                                }
                                else if (j == 10)
                                {
                                    dataGridView4.Rows[i].Cells[15].Value = porcentaje;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void preciosUtilidadesAbarrotes()
        {
            if (dataGridView1.RowCount > 0)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        if ((j == 6) | (j == 7) | (j == 8) | (j == 9) | (j == 10))
                        {
                            if (dataGridView1.Rows[i].Cells[j].Value.ToString() != "0")
                            {
                                if (Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value.ToString()) < Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString()))
                                {
                                    // MessageBox.Show("EL PRECIO DE VENTA APLICADO ES MENOR AL PRECIO COSTO!", "INGRESO DE MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(207, 136, 65);
                                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.White;
                                }
                                else
                                {
                                    dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.Black;
                                    dataGridView1.Rows[i].Cells[j].Style.BackColor = dataGridView1.Rows[i].DefaultCellStyle.BackColor;
                                }
                                double diferencial = Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString());
                                double porcentaje = Math.Round(diferencial / Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString()), 4) * 100;
                                if (j == 6)
                                {
                                    dataGridView1.Rows[i].Cells[11].Value = porcentaje;
                                }
                                else if (j == 7)
                                {
                                    dataGridView1.Rows[i].Cells[12].Value = porcentaje;
                                }
                                else if (j == 8)
                                {
                                    dataGridView1.Rows[i].Cells[13].Value = porcentaje;
                                }
                                else if (j == 9)
                                {
                                    dataGridView1.Rows[i].Cells[14].Value = porcentaje;
                                }
                                else if (j == 10)
                                {
                                    dataGridView1.Rows[i].Cells[15].Value = porcentaje;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void frm_compras_Load(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            dataGridView1.Columns[0].ReadOnly = false;
            dataGridView1.Columns[1].ReadOnly = false;
            dataGridView1.Columns[2].ReadOnly = false;
            dataGridView1.Columns[3].ReadOnly = false;
            dataGridView1.Columns[4].ReadOnly = false;
            dataGridView1.Columns[5].ReadOnly = false;
            dataGridView1.Columns[6].ReadOnly = false;
            dataGridView1.Columns[7].ReadOnly = false;
            // DATAGRID VIEW ESPECIAS //
            dataGridView4.Columns[0].ReadOnly = false;
            dataGridView4.Columns[1].ReadOnly = false;
            dataGridView4.Columns[2].ReadOnly = false;
            dataGridView4.Columns[3].ReadOnly = false;
            dataGridView4.Columns[4].ReadOnly = false;
            dataGridView4.Columns[5].ReadOnly = false;
            dataGridView4.Columns[6].ReadOnly = false;
            dataGridView4.Columns[7].ReadOnly = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView4.AllowUserToDeleteRows = false;
        }
        private void getEspecias(int numColum)
        {
            if (textBox13.Text != "")
            {
                if (textBox11.Text != "")
                {
                    if (numColum == 2)
                    {
                        if (dataGridView4.CurrentRow.Cells[numColum].Value.ToString() != "0")
                        {
                            double cantidad = Convert.ToDouble(textBox13.Text) + Convert.ToDouble(textBox11.Text);
                            dataGridView4.CurrentRow.Cells[3].Value = Math.Truncate(cantidad / Convert.ToDouble(dataGridView4.CurrentRow.Cells[numColum].Value.ToString()));
                            dataGridView4.CurrentRow.Cells[5].Value = Convert.ToDouble(dataGridView4.CurrentRow.Cells[numColum].Value.ToString()) * Convert.ToDouble(textBox12.Text);
                        }
                    }
                }
                else
                {
                    if (numColum == 2)
                    {
                        if (dataGridView4.CurrentRow.Cells[numColum].Value.ToString() != "0")
                        {
                            dataGridView4.CurrentRow.Cells[3].Value = Math.Truncate(Convert.ToDouble(textBox13.Text) / Convert.ToDouble(dataGridView4.CurrentRow.Cells[numColum].Value.ToString()));
                            dataGridView4.CurrentRow.Cells[5].Value = Convert.ToDouble(dataGridView4.CurrentRow.Cells[numColum].Value.ToString()) * Convert.ToDouble(textBox12.Text);
                        }
                    }
                }
            }
        }
        private void getCantidades(int numColum)
        {
            if (textBox3.Text != "")
            {
                if (textBox4.Text != "")
                {
                    if (numColum == 2)
                    {
                        if (dataGridView1.CurrentRow.Cells[numColum].Value.ToString() != "0")
                        {
                            double cantidad = Convert.ToDouble(textBox4.Text) + Convert.ToDouble(textBox3.Text);
                            dataGridView1.CurrentRow.Cells[3].Value = Math.Truncate(cantidad / Convert.ToDouble(dataGridView1.CurrentRow.Cells[numColum].Value.ToString()));
                            dataGridView1.CurrentRow.Cells[5].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[numColum].Value.ToString()) * Convert.ToDouble(textBox7.Text);
                        }
                    }
                }
                else
                { 
                    if (numColum == 2)
                    {
                        if (dataGridView1.CurrentRow.Cells[numColum].Value.ToString() != "0")
                        {
                            dataGridView1.CurrentRow.Cells[3].Value = Math.Truncate(Convert.ToDouble(textBox3.Text) / Convert.ToDouble(dataGridView1.CurrentRow.Cells[numColum].Value.ToString()));
                            dataGridView1.CurrentRow.Cells[5].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[numColum].Value.ToString()) * Convert.ToDouble(textBox7.Text);
                        }
                    }
                }
            }
        }
         // PERMITE REVISAR SI SUBCODIGO DEL MATRIZ YA EXISTE
        private bool revisaStock(string codigoStock)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT ID_STOCK FROM MERCADERIA_PRESENTACIONES WHERE ID_STOCK = '{0}';", codigoStock);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {                  
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
            return true;
        }
        private void calculaPorcentajes()
        {

        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value != null)
            {
                if ((e.ColumnIndex == 6) | (e.ColumnIndex == 7) | (e.ColumnIndex == 8) | (e.ColumnIndex == 9) | (e.ColumnIndex == 10))
                {
                    if (dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString() != "0")
                    {
                        if (Convert.ToDouble(dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) <  Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value.ToString()))
                        {
                            MessageBox.Show("EL PRECIO DE VENTA APLICADO ES MENOR AL PRECIO COSTO!", "INGRESO DE MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dataGridView1.CurrentRow.Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(207, 136, 65);
                            dataGridView1.CurrentRow.Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                        } else
                        {
                            dataGridView1.CurrentRow.Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
                            dataGridView1.CurrentRow.Cells[e.ColumnIndex].Style.BackColor = dataGridView1.CurrentRow.DefaultCellStyle.BackColor;
                        }
                        double diferencial = Convert.ToDouble(dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) - Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value.ToString());
                        double porcentaje = Math.Round(diferencial / Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value.ToString()), 4) * 100;
                        if (e.ColumnIndex == 6)
                        {
                            dataGridView1.CurrentRow.Cells[11].Value = porcentaje;
                        }
                        else if (e.ColumnIndex == 7)
                        {
                            dataGridView1.CurrentRow.Cells[12].Value = porcentaje;
                        }
                        else if (e.ColumnIndex == 8)
                        {
                            dataGridView1.CurrentRow.Cells[13].Value = porcentaje;
                        }
                        else if (e.ColumnIndex == 9)
                        {
                            dataGridView1.CurrentRow.Cells[14].Value = porcentaje;
                        }
                        else if (e.ColumnIndex == 10)
                        {
                            dataGridView1.CurrentRow.Cells[15].Value = porcentaje;
                        }
                    }
                } else if (e.ColumnIndex == 0)
                {
                    if (!revisaStock(dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString()))
                    {
                        dataGridView1.CurrentRow.Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                        dataGridView1.CurrentRow.Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(207, 136, 65);
                        MessageBox.Show("EL CODIGO INGRESADO YA EXISTE ACTUALMENTE", "COMPRA PRODUCTOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                } else if((e.ColumnIndex == 11) | (e.ColumnIndex == 12) | (e.ColumnIndex == 13) | (e.ColumnIndex == 14) | (e.ColumnIndex == 15))
                {
                    if( (Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value.ToString()) > 0))
                    {
                        double precio = Convert.ToDouble(dataGridView1.CurrentRow.Cells[5].Value.ToString());
                        precio = precio + (precio *( Convert.ToDouble(dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString())/100));
                       // precio = Math.Round(precio, 4);
                        if (e.ColumnIndex == 11)
                        {
                            dataGridView1.CurrentRow.Cells[6].Value = precio;
                        }
                        else if (e.ColumnIndex == 12)
                        {
                            dataGridView1.CurrentRow.Cells[7].Value = precio;
                        }
                        else if (e.ColumnIndex == 13)
                        {
                            dataGridView1.CurrentRow.Cells[8].Value = precio;
                        }
                        else if (e.ColumnIndex == 14)
                        {
                            dataGridView1.CurrentRow.Cells[9].Value = precio;
                        }
                        else if (e.ColumnIndex == 15)
                        {
                            dataGridView1.CurrentRow.Cells[10].Value = precio;
                        }
                    }
                }
                if (textBox3.Text != "")
                {
                    if (textBox4.Text != "")
                    {
                        if (e.ColumnIndex == 2)
                        {
                            if (dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString() != "0")
                            {
                                double cantidad = Convert.ToDouble(textBox4.Text) + Convert.ToDouble(textBox3.Text);
                                dataGridView1.CurrentRow.Cells[3].Value = Math.Truncate(cantidad / Convert.ToDouble(dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString()));
                                dataGridView1.CurrentRow.Cells[5].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) * Convert.ToDouble(textBox7.Text);
                            }
                        }
                    }
                    else
                    {
                        if (e.ColumnIndex == 2)
                        {
                            if (dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString() != "0")
                            {
                                dataGridView1.CurrentRow.Cells[3].Value = Math.Truncate(Convert.ToDouble(textBox3.Text) / Convert.ToDouble(dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString()));
                                dataGridView1.CurrentRow.Cells[5].Value = Convert.ToDouble(dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) * Convert.ToDouble(textBox7.Text);
                            }
                        }
                    }
                }
            } else
            {
                if (e.ColumnIndex > 1)
                    dataGridView1.CurrentCell.Value = "0";
                else dataGridView1.CurrentCell.Value = "INGRESE UNA PRESENTACION!";
            }
        }
       
        private void timerCompra()
        {
            frm_alert frm = new frm_alert();
            frm.ShowDialog();
            timer1.Interval = 1500;
            timer1.Enabled = true;
        }
        private void timerActions()
        {
            frm_done frm = new frm_done();
            frm.ShowDialog();
            timer1.Interval = 1500;
            timer1.Enabled = true;
        }
        private int existeSucursales(string codigo)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = sql = string.Format("SELECT COUNT(ID_SUCURSAL) FROM MERCADERIA WHERE ID_MERCADERIA = '{0}';", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return 0;
        }
        private void presentacionesIngreso(string mercaderia, string stock, string descipcion, string cantidadP, string total, string cantM, string p1, string p2, string p3, string p4, string p5, string p6, string u1, string u2, string u3, string u4, string u5)
        {
           
        }
        private void sucursalesDisponibles(string codigo, string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                dataGridView9.Rows.Clear();
                string sql = sql = string.Format("SELECT ID_SUCURSAL, TOTAL_UNIDADES FROM MERCADERIA WHERE ID_MERCADERIA = '{0}' AND ESTADO_TUPLA = TRUE AND ID_SUCURSAL <> (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{1}');", codigo, sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView9.Rows.Add(reader.GetString(0), reader.GetString(1));
                    while (reader.Read())
                    {
                        dataGridView9.Rows.Add(reader.GetString(0), reader.GetString(1));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void updateEspecias()
        {
          
        }
        private void updateMercaderia()
        {
           
        }
        private void getTotal()
        {
        }
        private void inventarioEscx()
        {
           
        }
        private void inventarioDex()
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
        internal class Presentaciones
        {
            public string getPresentacion { get; set; }
            public string getCantidad { get; set; }
        }
        internal class Proveedor
        {
            public string getProveedor { get; set; }
            
        }
        internal class precioPonderado
        {
            public string getPrecio { get; set; }
            public string getGasto { get; set; }
            public string getSubtotal { get; set; }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
          
        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
        }
        private bool codigoRepetido(string code)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT ID_MERCADERIA FROM MERCADERIA WHERE ID_MERCADERIA =  '{0}' AND ESTADO_TUPLA = TRUE;", code);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //MessageBox.Show("el codigo ya existe");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
            return true;
        }
        private bool codigostock(string code)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT ID_STOCK FROM MERCADERIA_PRESENTACIONES WHERE ID_STOCK =  '{0}' AND ESTADO_TUPLA = TRUE;", code);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   // MessageBox.Show("el codigo ya existe");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
            return true;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (button8.Text == "") {
                bool codex = true;
                while (codex)
                {
                    string temp = createCode(13);
                    if (codigoRepetido(temp)) {
                        textBox2.Text = temp;
                        codex = false;
                        textBox1.Focus();
                    }

                }
            }
            else
            {
                var forma = new frm_alterCodes(textBox2.Text.Trim(), textBox1.Text,1);
                forma.ShowDialog();
            }
        }
        private string getCode()
        {
            bool codex = true;
            while (codex)
            {
                string temp = createCode(10);
                if (codigostock(temp))
                {
                    codex = false;
                    return temp;
                }
                
            }
            return null;
        }
        void text_SKeyUp(object sender, KeyEventArgs e)
        {
            /*int rowIndex = ((System.Windows.Forms.DataGridViewTextBoxEditingControl)(sender)).EditingControlRowIndex;

            if (e.KeyCode == Keys.Enter)
            {
/*
                int columna = dataGridView1.CurrentCell.ColumnIndex;
                int fila = dataGridView1.CurrentCell.RowIndex;

                
                    e.Handled = true;
                    //obtienePresentacion();
                    if (dataGridView1.RowCount == 1)
                    {
                    this.dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
                } else
                    {
                    this.dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
                }
                    
                
            }*/
        }
      
        private void button2_Click_1(object sender, EventArgs e)
        {
            if ((textBox2.Text != "") && (textBox1.Text != ""))
            {
                dataGridView1.Rows.Add(getCode(), "", 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0,0);
                //styleDV(this.dataGridView1);
                if (dataGridView1.RowCount == 1)
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1[1, dataGridView1.CurrentRow.Index];
                    
                } else
                {
                    this.dataGridView1.CurrentCell = this.dataGridView1[1, dataGridView1.RowCount - 1];
                }
                dataGridView1.Focus();
               
            } else
            {
                MessageBox.Show("NO SE HA CREADO AUN EL PRODUCTO!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            /*if (e.ColumnIndex > 1)
            {
                //double newInteger;
                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    dataGridView1.CurrentRow.Cells[e.ColumnIndex].Value = "0";
                }
            } */
        }
        private void cleanForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox7.Text = "";
            textBox7.BackColor = Color.White;
            textBox3.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox2.Focus();
            button8.Text = "";
            //button8.Image = ASG.Properties.Resources.barcode;
            
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
                       
                cleanForm();
        }
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if((e.KeyChar == 's') || (e.KeyChar == 'S') )
            {
                if(textBox5.Text != "")
                {
                    textBox7.Text = textBox5.Text;
                    textBox3.Focus();
                }
            }

            /* if ((e.KeyChar >= '0' & e.KeyChar <= '9') || (e.KeyChar == 08) || (e.KeyChar == 46))

             {
                 e.Handled = false;
             }
             else
             {
                 e.Handled = true;
             }*/
            e.Handled = solonumeros(Convert.ToInt32(e.KeyChar)); //llamada a la funcion que evalua que tecla es aceptada

        }
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if ((e.KeyChar >= '0' & e.KeyChar <= '9') || (e.KeyChar == 08) || (e.KeyChar == 46))

            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }*/
             e.Handled = solonumerosCantidad(Convert.ToInt32(e.KeyChar)); //llamada a la funcion que evalua que tecla es aceptada
        }
        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text != "")
            {
                if ((textBox4.Text != ""))
                {
                    if ((Convert.ToDouble(textBox3.Text) > 0) && (textBox3.Text != "") && (dataGridView1.RowCount > 0))
                    {
                        double existente = Convert.ToDouble(textBox4.Text) + Convert.ToDouble(textBox3.Text);
                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {
                                dataGridView1.Rows[i].Cells[3].Value = Math.Truncate( existente / Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value));
                            }
                    }
                } else
                {
                    if ((Convert.ToDouble(textBox3.Text) > 0) && (textBox3.Text != "") && (dataGridView1.RowCount > 0))
                    {
                       
                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {
                                dataGridView1.Rows[i].Cells[3].Value = Math.Truncate(Convert.ToDouble(textBox3.Text) / Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value));
                            }
                        
                    }
                }
            }
        }
        private void frm_compras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if(textBox2.Text != "" | textBox8.Text != "")
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "COMPRAS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Close();
                    }
                } else
                this.Close();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.N))
            {
                if (tabControl1.SelectedTab == tabPage1)
                {
                    textBox2.Focus();
                } else if (tabControl1.SelectedTab == tabPage2)
                {
                  
                    textBox8.Focus();
                } 
            }
            
           
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.B))
            {

                if (tabControl1.SelectedTab == tabPage1)
                {
                    button22.PerformClick();
                }
                else if (tabControl1.SelectedTab == tabPage2)
                {
                    button23.PerformClick();
                }
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.T))
            {
                
                    if (tabControl1.SelectedTab == tabPage1)
                    {
                        if (textBox2.Text != "")
                        {
                            button2.PerformClick();
                        } else
                        {
                            MessageBox.Show("NO HA INGRESADO UN PRODUCTO", "PRESENTACIONES DEL PRODUCTO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (tabControl1.SelectedTab == tabPage2)
                    {
                        if (textBox8.Text != "")
                        {
                            button12.PerformClick();
                        } else
                        {
                            MessageBox.Show("NO HA INGRESADO UN PRODUCTO", "PRESENTACIONES DEL PRODUCTO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.E))
            {
                tabControl1.SelectedTab = tabPage2;
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.A))
            {
                tabControl1.SelectedTab = tabPage1;
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.S))
            {
                
                    if (tabControl1.SelectedTab == tabPage1)
                    {
                        button5.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage2)
                    {
                        button13.PerformClick();
                    }
                
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.P))
            {
                
                    if (tabControl1.SelectedTab == tabPage1)
                    {
                        button16.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage2)
                    {
                        button17.PerformClick();
                    }
                
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.O))
            {
                
                    if (tabControl1.SelectedTab == tabPage1)
                    {
                        button33.PerformClick();
                    }
                    else if (tabControl1.SelectedTab == tabPage2)
                    {
                        button24.PerformClick();
                    }
                
            }
           
            

        }
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (textBox2.Text == "") {
                    var forma = new frm_busca(1,comboBox1.Text, rolUs);
                    if (forma.ShowDialog() == DialogResult.OK)
                    {
                        textBox2.Text = forma.CurrentMercaderia.getMercaderia;
                        if (textBox2.Text != "")
                        {
                            textBox7.Focus();
                        }
                    }
                } else
                {
                    textBox1.Focus();
                }
            }  else if (e.KeyChar == 32)
            {
                e.Handled = true;
                button8.PerformClick();
                textBox1.Focus();
            }
        }
        internal class Mercaderia
        {
            public string getMercaderia { get; set; }
        }
        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
           
        }

       
        private bool codigoCompra(string code)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT ID_COMPRA FROM COMPRA WHERE ID_COMPRA =  '{0}' AND ESTADO_TUPLA = TRUE;", code);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //MessageBox.Show("EL CODIGO YA EXITSTE");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
            return true;
        }
        private string getCompra()
        {
            bool codex = true;
            while (codex)
            {
                string temp = createCodecompra(7);
                if (codigoCompra(temp))
                {
                    codex = false;
                    return temp;
                }

            }
            return null;
        }
        public string createCodecompra(int longitud)
        {
            string caracteres = "1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }
      /*  private void nuevaCompra(string code)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("CALL NUEVA_COMPRA ({0},'{1}');",code,usuario);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    timerCompra();
                }
                else
                {
                    MessageBox.Show("NO SE PUDO GENERAR NUEVA ORDEN DE COMPRA!", "GESTION COMPRAS", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }*/
        internal class nuevaCompra
        {
            public string getCompra { get; set; }
            public string getSucursal { get; set; }
            public string getProveedor { get; set; }
        }
        private bool existeComra(string comopra)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT ID_COMPRA FROM CONTROL_COMPRA WHERE ID_COMPRA = '{0}';", comopra);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                } else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
           
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space) 
            {
                e.Handled = false;
                button8.PerformClick();
                textBox1.Focus();
            }
        }
        private void ingresaDetalle()
        {
           
        }
        private void ingresaDetalleEsp()
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
           
        }
        private void getCalculosEspecias(string existencia)
        {
            if (Convert.ToDouble(existencia) > 0)
            {
                // la cantidad del prudcto es maor a 0
                if (dataGridView4.RowCount > 0)
                {
                    double temp = Convert.ToDouble(existencia);
                    for (int i = 0; i < dataGridView4.RowCount; i++)
                    {
                        // CALCULO SE ESPERA LA VARIABLE DE TIPO DE VARIABLE PARA DIVIIR
                       
                       
                            dataGridView4.Rows[i].Cells[3].Value = string.Format("{0:###,###,###,##0.00##}", Math.Round(temp / Convert.ToDouble(dataGridView4.Rows[i].Cells[2].Value.ToString()), 2));
                        
                    }
                }

            }
            else
            {
                // la cantidad disponible es mayor a 0
                if (dataGridView4.RowCount > 0)
                {
                    for (int i = 0; i < dataGridView4.RowCount; i++)
                    {
                        dataGridView4.Rows[i].Cells[3].Value = "0";
                    }
                }
            }
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (textBox8.Text == "")
                {
                    var forma = new frm_busca(2,comboBox2.Text, rolUs);
                    if (forma.ShowDialog() == DialogResult.OK)
                    {
                        textBox8.Text = forma.CurrentMercaderia.getMercaderia;
                        if (textBox8.Text != "")
                        {
                            textBox12.Focus();
                        }
                    }
                }
                else
                {
                    textBox9.Focus();
                }
            } else if (e.KeyChar == 32)
            {
                e.Handled = true;
                button9.PerformClick();
                textBox9.Focus();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (button9.Text == "") {
                bool codex = true;
                while (codex)
                {
                    string temp = createCode(10);
                    if (codigoRepetido(temp))
                    {
                        textBox8.Text = "";
                        textBox8.Text = temp;
                        codex = false;
                        //generaBarcode(temp);
                        textBox9.Focus();
                    }

                }
            } else
            {
                var forma = new frm_alterCodes(textBox8.Text.Trim(), textBox9.Text,1);
                forma.ShowDialog();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if ((textBox8.Text != "") && (textBox9.Text != ""))
            {
                dataGridView4.Rows.Add(getCode(), "", 0, 0, 0, 0, 0, 0, 0, 0, 0,0,0,0,0,0);
                //styleDV(this.dataGridView4);
                if (dataGridView4.RowCount == 1)
                {
                    this.dataGridView4.CurrentCell = this.dataGridView4[1, dataGridView4.CurrentRow.Index];

                }
                else
                {
                    this.dataGridView4.CurrentCell = this.dataGridView4[1, dataGridView4.RowCount - 1];
                }
                dataGridView4.Focus();
            }
            else
            {
                MessageBox.Show("NO SE HA CREADO AUN EL PRODUCTO!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void generalizaAbarrotes()
        {
            
        }
        private void generalizaEspecias()
        {
           
        }
        private void button16_Click(object sender, EventArgs e)
        {
            if((textBox7.Text != "") && (textBox3.Text != ""))
            {     
                
                    if ((textBox4.Text != "")&&(textBox5.Text != "")) {
                   
                    if (Convert.ToDouble(textBox4.Text) > 0)
                    {
                        var forma = new frm_precioPonderado(textBox5.Text, textBox7.Text, textBox4.Text, textBox3.Text);
                        forma.ShowDialog();
                        if (forma.DialogResult == DialogResult.OK)
                        {
                            precio_ponderado = forma.CurrentPrecio.getPrecio;
                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {
                                dataGridView1.Rows[i].Cells[5].Value = (Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(precio_ponderado));
                                preciosUtilidadesAbarrotes();
                            }
                        }
                        else if (forma.DialogResult == DialogResult.Yes)
                        {
                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {
                                dataGridView1.Rows[i].Cells[5].Value = (Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(textBox7.Text));
                                preciosUtilidadesAbarrotes();
                            }
                        }

                    }
                    else
                    {
                        DialogResult result;
                        result = MessageBox.Show("LA EXISTENCIA DEL PRODUCTO DEBE DE SER MAYOR A 0!, ¿DESEA APLICAR EL PRECIO DE COMPRA A EL PRODUCTO?", "TRASLADO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            for (int i = 0; i < dataGridView1.RowCount; i++)
                            {
                                dataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(textBox7.Text);
                            }
                        }
                    }
                    } else
                    {
                        MessageBox.Show("PARA APLICAR EL PRECIO COSTO NUEVO DEBE DE EXISTIR UN PRECIO Y EXISTENCIA ANTEIORES!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }         
            } else
            {
                MessageBox.Show("PARA APLICAR EL PRECIO COSTO NUEVO DEBE DE EXISTIR UN PRECIO Y EXISTENCIA ANTEIORES!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
              
        }
        private void button12_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
        private void dataGridView4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dataGridView4.RowCount > 0)
                {
                    int numColumn = dataGridView4.CurrentCell.ColumnIndex;
                    int numRow = dataGridView4.CurrentCell.RowIndex;
                    var forma = new frm_especias();
                    if (forma.ShowDialog() == DialogResult.OK)
                    {
                        dataGridView4.CurrentRow.Cells[numColumn].Value = forma.CurrentPresentacion.getPresentacion;
                        dataGridView4.CurrentRow.Cells[2].Value = forma.CurrentCantidad.getCantidad;
                        this.dataGridView4.CurrentCell = this.dataGridView4[2, dataGridView4.CurrentRow.Index];
                        dataGridView4.Focus();
                        getEspecias(2);
                    }
                }
            }
            else if (e.KeyData == Keys.Delete)
            {
                if (dataGridView4.RowCount > 0)
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA PRESENTACION DEL PRODUCTO?", "INGRESO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        deleteDatagridE();
                    }
                }
            }
        }
        
        private void button17_Click(object sender, EventArgs e)
        {
            if ((textBox12.Text != "") && (textBox13.Text != ""))
            {
                if ((textBox10.Text != "") && (textBox11.Text != ""))
                {
                    if (Convert.ToDouble(textBox11.Text) > 0)
                    {
                        var forma = new frm_precioPonderado(textBox10.Text, textBox12.Text, textBox11.Text, textBox13.Text);
                        forma.ShowDialog();
                        if (forma.DialogResult == DialogResult.OK)
                        {
                            ponderado_especias = forma.CurrentPrecio.getPrecio;
                            for (int i = 0; i < dataGridView4.RowCount; i++)
                            {
                                dataGridView4.Rows[i].Cells[5].Value = (Convert.ToDouble(dataGridView4.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(ponderado_especias));
                                preciosUtilidadesEspecias();
                            }
                        }
                        else if (forma.DialogResult == DialogResult.Yes)
                        {
                            for (int i = 0; i < dataGridView4.RowCount; i++)
                            {
                                dataGridView4.Rows[i].Cells[5].Value = (Convert.ToDouble(dataGridView4.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(textBox12.Text));
                                preciosUtilidadesEspecias();
                            }
                        }
                    }
                    else
                    {
                       // MessageBox.Show("", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        DialogResult result;
                        result = MessageBox.Show("LA EXISTENCIA DEL PRODUCTO DEBE DE SER MAYOR A 0!, ¿DESEA APLICAR EL PRECIO DE COMPRA A EL PRODUCTO?", "TRASLADO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == System.Windows.Forms.DialogResult.Yes)
                        {
                            for (int i = 0; i < dataGridView4.RowCount; i++)
                            {
                                dataGridView4.Rows[i].Cells[5].Value = Math.Round(Convert.ToDouble(dataGridView4.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(textBox12.Text), 2);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("PARA APLICAR EL PRECIO COSTO NUEVO DEBE DE EXISTIR UN PRECIO Y EXISTENCIA ANTEIORES!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("PARA APLICAR EL PRECIO COSTO NUEVO DEBE DE EXISTIR UN PRECIO Y EXISTENCIA ANTEIORES!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
           
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{tab}");
                e.Handled = true;
            }
        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value != null)
            {
                if ((e.ColumnIndex == 6) | (e.ColumnIndex == 7) | (e.ColumnIndex == 8) | (e.ColumnIndex == 9) | (e.ColumnIndex == 10))
                {
                    if (dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString() != "0")
                    {
                        if (Convert.ToDouble(dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) < Convert.ToDouble(dataGridView4.CurrentRow.Cells[5].Value.ToString()))
                        {
                            MessageBox.Show("EL PRECIO DE VENTA APLICADO ES MENOR AL PRECIO COSTO!", "INGRESO DE MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dataGridView4.CurrentRow.Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(207, 136, 65);
                            dataGridView4.CurrentRow.Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                        } else
                        {
                            dataGridView4.CurrentRow.Cells[e.ColumnIndex].Style.ForeColor = Color.Black;
                            dataGridView4.CurrentRow.Cells[e.ColumnIndex].Style.BackColor = dataGridView4.CurrentRow.DefaultCellStyle.BackColor;
                        }
                        double diferencial = Convert.ToDouble(dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) - Convert.ToDouble(dataGridView4.CurrentRow.Cells[5].Value.ToString());
                        double porcentaje = Math.Round(diferencial / Convert.ToDouble(dataGridView4.CurrentRow.Cells[5].Value.ToString()), 4) * 100;
                        if (e.ColumnIndex == 6)
                        {
                            dataGridView4.CurrentRow.Cells[11].Value = porcentaje;
                        }
                        else if (e.ColumnIndex == 7)
                        {
                            dataGridView4.CurrentRow.Cells[12].Value = porcentaje;
                        }
                        else if (e.ColumnIndex == 8)
                        {
                            dataGridView4.CurrentRow.Cells[13].Value = porcentaje;
                        }
                        else if (e.ColumnIndex == 9)
                        {
                            dataGridView4.CurrentRow.Cells[14].Value = porcentaje;
                        }
                        else if (e.ColumnIndex == 10)
                        {
                            dataGridView4.CurrentRow.Cells[15].Value = porcentaje;
                        }
                    }
                }
                else if (e.ColumnIndex == 0)
                {
                    if (!revisaStock(dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString()))
                    {
                        dataGridView4.CurrentRow.Cells[e.ColumnIndex].Style.ForeColor = Color.White;
                        dataGridView4.CurrentRow.Cells[e.ColumnIndex].Style.BackColor = Color.FromArgb(207, 136, 65);
                        MessageBox.Show("EL CODIGO INGRESADO YA EXISTE ACTUALMENTE", "COMPRA PRODUCTOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if ((e.ColumnIndex == 11) | (e.ColumnIndex == 12) | (e.ColumnIndex == 13) | (e.ColumnIndex == 14) | (e.ColumnIndex == 15))
                {
                    if ((Convert.ToDouble(dataGridView4.CurrentRow.Cells[5].Value.ToString()) > 0))
                    {
                        double precio = Convert.ToDouble(dataGridView4.CurrentRow.Cells[5].Value.ToString());
                        precio = precio + (precio * (Convert.ToDouble(dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) / 100));
                        precio = Math.Round(precio, 4);
                        if (e.ColumnIndex == 11)
                        {
                            dataGridView4.CurrentRow.Cells[6].Value = precio;
                        }
                        else if (e.ColumnIndex == 12)
                        {
                            dataGridView4.CurrentRow.Cells[7].Value = precio;
                        }
                        else if (e.ColumnIndex == 13)
                        {
                            dataGridView4.CurrentRow.Cells[8].Value = precio;
                        }
                        else if (e.ColumnIndex == 14)
                        {
                            dataGridView4.CurrentRow.Cells[9].Value = precio;
                        }
                        else if (e.ColumnIndex == 15)
                        {
                            dataGridView4.CurrentRow.Cells[10].Value = precio;
                        }
                    }
                }
                if (textBox13.Text != "")
                {
                    if (textBox11.Text != "")
                    {
                        if (e.ColumnIndex == 2)
                        {
                            if (dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString() != "0")
                            {
                                double cantidad = Convert.ToDouble(textBox11.Text) + Convert.ToDouble(textBox13.Text);
                                dataGridView4.CurrentRow.Cells[3].Value = Math.Round(cantidad / Convert.ToDouble(dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString()), 2);
                                dataGridView4.CurrentRow.Cells[5].Value = Convert.ToDouble(dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) * Convert.ToDouble(textBox12.Text);
                            }
                        }
                    }
                    else
                    {
                        if (e.ColumnIndex == 2)
                        {
                            if (dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString() != "0")
                            {
                                dataGridView4.CurrentRow.Cells[3].Value = Math.Round(Convert.ToDouble(textBox13.Text) / Convert.ToDouble(dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString()), 2);
                                dataGridView4.CurrentRow.Cells[5].Value = Convert.ToDouble(dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value.ToString()) * Convert.ToDouble(textBox12.Text);
                            }
                        }
                    }
                }
            } else
            {
                if (e.ColumnIndex > 1)
                    dataGridView4.CurrentCell.Value = "0";
                else dataGridView4.CurrentCell.Value = "INGRESE UNA PRESENTACION!";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (Revisa_preciosEspecias())
            {
                if ((dataGridView4.Rows.Count > 0) && (textBox8.Text != "") && (textBox9.Text != "") && (textBox12.Text != "") && (textBox13.Text != "") && (comboBox2.Text != ""))
                {
                    DialogResult result;
                    result = MessageBox.Show("¿DESEA GUARDAR EL PRODUCTO?" + "\n", "GESTION MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        
                        if (!existeMercaderia(textBox8.Text, comboBox2.Text))
                        {
                            // NO existe la mercaderia
                            OdbcConnection conexion = ASG_DB.connectionResult();
                            try
                            {
                                conexion.Open();
                                string sql = string.Format("INSERT INTO MERCADERIA VALUES ('{0}', 2, '{1}',{2},{3},TRUE);",
                                textBox8.Text, textBox9.Text, textBox12.Text, Convert.ToBoolean(checkBox3.Checked));
                                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                                if (cmd.ExecuteNonQuery() == 1)
                                {
                                    // se ingreso correctamente la mercaderia
                                    if (agregaCantidadEnSucursal(textBox8.Text, comboBox2.Text, textBox13.Text, textBox12.Text))
                                    {
                                        ingresaPresentacionesML();
                                        var done = new frm_done();
                                        done.Show();
                                        limpiaFormularioML();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            conexion.Close();
                        }
                        else
                        {
                            // SI EXISTE ENTONCES ACTUALIZA LOS DISPONIBLES
                            OdbcConnection conexion = ASG_DB.connectionResult();
                            try
                            {
                                conexion.Open();
                                string sql = string.Format("UPDATE  MERCADERIA SET DESCRIPCION = '{0}', " +
                                    "PRECIO_COMPRA = {1} WHERE ID_MERCADERIA = '{2}';",
                                    textBox9.Text, textBox12.Text, textBox8.Text);
                                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                                if ((cmd.ExecuteNonQuery() == 1) | (cmd.ExecuteNonQuery() == 0))
                                {
                                    if (ActualizaCantidadEnSucursal(textBox8.Text, comboBox2.Text, textBox12.Text, textBox13.Text))
                                    {
                                        ingresaPresentacionesML();
                                        var done = new frm_done();
                                        done.Show();
                                        limpiaFormularioML();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            conexion.Close();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("DEBE DE LLENAR TODOS LOS DATOS!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void guardaEspeciasPrecios()
        {
            if (Revisa_preciosEspecias())
            {
                if ((dataGridView4.Rows.Count > 0) && (textBox8.Text != "") && (textBox9.Text != "") && (textBox12.Text != "") && (textBox13.Text != ""))
                {
                    inventarioEscx();
                    ponderado_especias = "";
                    CleanFormE();
                    textBox8.Focus();
                }
                else
                {
                    MessageBox.Show("DEBE DE LLENAR TODOS LOS DATOS!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void dataGridView4_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
             /*   double newInteger;
                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value = "0";
                }
                if (!double.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0)
                {
                    dataGridView4.CurrentRow.Cells[e.ColumnIndex].Value = "0";
                }
            */
        }

        private void dataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView4_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView4.RowCount > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    mymenu.Show(dataGridView4, new Point(e.X, e.Y));
                    mymenu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemclickedE);
                    mymenu.Enabled = true;
                    flags = false;
                }
            }
        }
        private void CleanFormE()
        {
            dataGridView4.Rows.Clear();
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox12.BackColor = Color.White;    
            //cargaProveedor();
            button9.Text = "";
            //button9.Image = ASG.Properties.Resources.barcode;
            
        }
        private void button14_Click(object sender, EventArgs e)
        {
            CleanFormE();
            /* if ((dataGridView4.Rows.Count > 0) && (textBox9.Text != "") && (textBox8.Text != "") &&(textBox8.BackColor != Color.LightGreen))

             {
                 DialogResult result;
                 result = MessageBox.Show("¿DESEA GUARDAR EL PRODUCTO EN LA COMPRA?" + "\n" + "  EL PRODUCTO SE AGREGARA A LA COMPRA", "GESTION MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (result == System.Windows.Forms.DialogResult.Yes)
                 {
                     inventarioEscx();
                     CleanFormE();
                 }
             }
             else
             {
                 CleanFormE();
             }*/
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                int numColumn = dataGridView4.CurrentCell.ColumnIndex;
                int numRow = dataGridView4.CurrentCell.RowIndex;
                var forma = new frm_especias();
                if (forma.ShowDialog() == DialogResult.OK)
                {
                    dataGridView4.CurrentRow.Cells[numColumn].Value = forma.CurrentPresentacion.getPresentacion;
                    dataGridView4.CurrentRow.Cells[2].Value = forma.CurrentCantidad.getCantidad;
                    this.dataGridView4.CurrentCell = this.dataGridView4[2, dataGridView4.CurrentRow.Index];
                    dataGridView1.Focus();
                    getEspecias(2);

                }
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 's') || (e.KeyChar == 'S'))
            {
                if (textBox10.Text != "")
                {
                    textBox12.Text = textBox10.Text;
                    textBox13.Focus();
                }
            }
            /*if ((e.KeyChar >= '0' & e.KeyChar <= '9') || (e.KeyChar == 08) || (e.KeyChar == 46))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }*/
            e.Handled = solonumerosEspecias(Convert.ToInt32(e.KeyChar)); //llamada a la funcion que evalua que tecla es aceptada
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if ((e.KeyChar >= '0' & e.KeyChar <= '9') || (e.KeyChar == 08) || (e.KeyChar == 46))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }*/
            e.Handled = solonumerosEspeciasCantidad(Convert.ToInt32(e.KeyChar)); //llamada a la funcion que evalua que tecla es aceptada
        }
        private void obtienePresentacion()
        {
          
                int numColumn = dataGridView1.CurrentCell.ColumnIndex;
                int numRow = dataGridView1.CurrentCell.RowIndex;
                var forma = new frm_getPresentacion();

                if (forma.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.Rows[numRow-1].Cells[numColumn].Value = forma.CurrentPresentacion.getPresentacion;
                    dataGridView1.Rows[numRow- 1].Cells[2].Value = forma.CurrentCantidad.getCantidad;
                    this.dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
                    getCantidades(2);
                }
            
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                int numColumn = dataGridView1.CurrentCell.ColumnIndex;
                int numRow = dataGridView1.CurrentCell.RowIndex;           
                var forma = new frm_getPresentacion();
            
                if (forma.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.CurrentRow.Cells[numColumn].Value = forma.CurrentPresentacion.getPresentacion;
                    dataGridView1.CurrentRow.Cells[2].Value = forma.CurrentCantidad.getCantidad;
                    this.dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
                    getCantidades(2);
                }
            }
        }

        private void textBox13_Leave(object sender, EventArgs e)
        {
            if (textBox13.Text != "")
            {
                if ((textBox11.Text != ""))
                {
                    if ((Convert.ToDouble(textBox13.Text) > 0) && (textBox13.Text != "") && (dataGridView4.RowCount > 0))
                    {
                        double existente = Convert.ToDouble(textBox11.Text) + Convert.ToDouble(textBox13.Text);
                        for (int i = 0; i < dataGridView4.RowCount; i++)
                        {
                            dataGridView4.Rows[i].Cells[3].Value = Math.Round(existente / Convert.ToDouble(dataGridView4.Rows[i].Cells[2].Value),2);
                        }
                    }
                }
                else
                {
                    if ((Convert.ToDouble(textBox13.Text) > 0) && (textBox13.Text != "") && (dataGridView4.RowCount > 0))
                    {
                       
                            for (int i = 0; i < dataGridView4.RowCount; i++)
                            {
                                dataGridView4.Rows[i].Cells[3].Value = Math.Round(Convert.ToDouble(textBox13.Text) / Convert.ToDouble(dataGridView4.Rows[i].Cells[2].Value),2);
                            }
                       
                    }
                }
            }
        }
        private bool existeDeuda(string codigo)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT ID_COMPRA FROM CUENTA_POR_PAGAR WHERE ID_COMPRA = {0} AND ESTADO_TUPLA = TRUE AND ESTADO_CUENTA = TRUE;", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   // MessageBox.Show("FACTURA GURDADA");
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
        private void button15_Click(object sender, EventArgs e)
        {
           
        }
        private void buscaCompras()
        {
           
        }
        private void buscabyName()
        {
           
        }
        private void CargaPorNombre()
        {
           
        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {
          
        }
        private void setDescripcion(int celda)
        {
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            /*if (dataGridView2.RowCount > 0)
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_CONTROL_COMPRA WHERE ID_COMPRA = {0};", dataGridView2.CurrentRow.Cells[0].Value.ToString());
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView3.Rows.Clear();
                    dataGridView3.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("Q{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("Q{0:###,###,###,##0.00##}", reader.GetDouble(5)));
                    while (reader.Read())
                    {
                        dataGridView3.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("Q{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("Q{0:###,###,###,##0.00##}", reader.GetDouble(5)));
                        styleDV(this.dataGridView3);
                    }
                } else
                {
                    dataGridView3.Rows.Clear();
                }

            }*/
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           /* if (pictureBox1.Image != null)
            {
                Image imgFinal = (Image)pictureBox1.Image.Clone();
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Guardar codigo de Barra";
                saveFileDialog1.AddExtension = true;
                saveFileDialog1.Filter = "Image PNG (*.png)|*.png";
                saveFileDialog1.ShowDialog();
                if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                {
                    imgFinal.Save(saveFileDialog1.FileName, ImageFormat.Png);
                }
                imgFinal.Dispose();
            }
            else
            {
                MessageBox.Show("NO SE HA GENERADO CODIGO DE BARRA!", "GESTION COMPRAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }*/
        }

        private void textBox2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var forma = new frm_busca(1,comboBox1.Text,rolUs);
            if (forma.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = forma.CurrentMercaderia.getMercaderia;
                if (textBox2.Text != "")
                {
                    textBox7.Focus();
                }
            }
        }

       

        private void dataGridView4_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (dataGridView4.CurrentCell.ColumnIndex > 1)
            {
                e.Control.KeyPress += new KeyPressEventHandler(dgr_KeyPress_NumericTesterdgv4);
            }

        }

        private void dataGridView4_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
       
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex <= 1)
            {
                dataGridView1.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }
            else
            {
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            }

        }
        
        

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex >1) {
                e.Control.KeyPress += new KeyPressEventHandler(dgr_KeyPress_NumericTester);
            } 
            /*
            DataGridViewTextBoxEditingControl dText = (DataGridViewTextBoxEditingControl)e.Control;
            dText.KeyUp -= new KeyEventHandler(text_SKeyUp);
            dText.KeyUp += new KeyEventHandler(text_SKeyUp);*/
        }
        private void limpiaFormulario()
        {
            textBox2.ReadOnly = false;
            textBox2.Text = "";
            textBox1.Text = "";
            textBox7.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            dataGridView1.Rows.Clear();
            comboBox1.SelectedIndex = -1;
            checkBox2.Checked = false;
        }
        private void limpiaFormularioML()
        {
            textBox8.ReadOnly = false;
            textBox8.Text = "";
            textBox9.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            dataGridView4.Rows.Clear();
            comboBox2.SelectedIndex = -1;
            checkBox3.Checked = false;
        }
        private void button18_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "" | dataGridView1.RowCount > 0)
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "INGRESO MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    limpiaFormulario();
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if ((dataGridView4.Rows.Count > 0) | (textBox8.Text != "") | (textBox9.Text != "") |
                (textBox12.Text != "") | (textBox13.Text != "") | (comboBox2.Text != ""))
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "INGRESO MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    limpiaFormularioML();
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox7.Focus();
            }
        }
        Boolean permitir = true;
        public bool solonumeros(int code)
        {
            bool resultado;

            if (code == 46 && textBox7.Text.Contains("."))//se evalua si es punto y si es punto se rebiza si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;
        }
        public bool solonumerosEspecias(int code)
        {
            bool resultado;

            if (code == 46 && textBox12.Text.Contains("."))//se evalua si es punto y si es punto se rebiza si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }
            return resultado;
        }
        public bool solonumerosEspeciasCantidad(int code)
        {
            bool resultado;

            if (code == 46 && textBox13.Text.Contains("."))//se evalua si es punto y si es punto se rebiza si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }
            return resultado;
        }
        public bool solonumerosCantidad(int code)
        {
            bool resultado;

            if (code == 46 && textBox3.Text.Contains("."))//se evalua si es punto y si es punto se rebiza si ya existe en el textbox
            {
                resultado = true;
            }
            else if ((((code >= 48) && (code <= 57)) || (code == 8) || code == 46)) //se evaluan las teclas validas
            {
                resultado = false;
            }
            else if (!permitir)
            {
                resultado = permitir;
            }
            else
            {
                resultado = true;
            }

            return resultado;

        }
        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox3.Focus();
            }
            bool paste = (Convert.ToInt32(e.KeyData) == (Convert.ToInt32(Keys.Control) | Convert.ToInt32(Keys.V)));
            bool copy = (Convert.ToInt32(e.KeyData) == (Convert.ToInt32(Keys.Control) | Convert.ToInt32(Keys.C)));
            if (paste || copy)
            {
                permitir = false;
            }
            else
            {
                permitir = true;
            }
        }
      

       
        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyData == Keys.Enter)
            {
                button2.Focus();
            }
            bool paste = (Convert.ToInt32(e.KeyData) == (Convert.ToInt32(Keys.Control) | Convert.ToInt32(Keys.V)));
            bool copy = (Convert.ToInt32(e.KeyData) == (Convert.ToInt32(Keys.Control) | Convert.ToInt32(Keys.C)));
            if (paste || copy)
            {
                permitir = false;
            }
            else
            {
                permitir = true;
            }
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox12.Focus();
            }
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                textBox13.Focus();
            }
            bool paste = (Convert.ToInt32(e.KeyData) == (Convert.ToInt32(Keys.Control) | Convert.ToInt32(Keys.V)));
            bool copy = (Convert.ToInt32(e.KeyData) == (Convert.ToInt32(Keys.Control) | Convert.ToInt32(Keys.C)));
            if (paste || copy)
            {
                permitir = false;
            }
            else
            {
                permitir = true;
            }
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button12.Focus();
            }
            bool paste = (Convert.ToInt32(e.KeyData) == (Convert.ToInt32(Keys.Control) | Convert.ToInt32(Keys.V)));
            bool copy = (Convert.ToInt32(e.KeyData) == (Convert.ToInt32(Keys.Control) | Convert.ToInt32(Keys.C)));
            if (paste || copy)
            {
                permitir = false;
            }
            else
            {
                permitir = true;
            }
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void textBox8_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var forma = new frm_busca(2,comboBox2.Text, rolUs);
            if (forma.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = forma.CurrentMercaderia.getMercaderia;
                if (textBox2.Text != "")
                {
                    textBox12.Focus();
                }
            }
        }

        private void dataGridView4_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.CurrentCell.ColumnIndex <=1)
            {
                dataGridView4.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }
            else
            {
                dataGridView4.EditMode = DataGridViewEditMode.EditOnEnter;
            }
        }

        private void dataGridView4_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
          

        }

        private void dataGridView1_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dataGridView1.RowCount > 0)
                {
                    int numColumn = dataGridView1.CurrentCell.ColumnIndex;
                    int numRow = dataGridView1.CurrentCell.RowIndex;
                    if (numColumn == 1)
                    {
                        var forma = new frm_getPresentacion();

                        if (forma.ShowDialog() == DialogResult.OK)
                        {
                            dataGridView1.CurrentRow.Cells[numColumn].Value = forma.CurrentPresentacion.getPresentacion;
                            dataGridView1.CurrentRow.Cells[2].Value = forma.CurrentCantidad.getCantidad;
                            this.dataGridView1.CurrentCell = this.dataGridView1[2, dataGridView1.CurrentRow.Index];
                            getCantidades(2);
                        }
                    }
                }
            } else if (e.KeyData == Keys.Delete)
            {
                if(dataGridView1.RowCount > 0)
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR LA PRESENTACION DEL PRODUCTO?", "INGRESO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        deleteDatagrid();
                    }
                   
                }
            }
        }
        private void dgr_KeyPress_NumericTesterdgv4(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0' & e.KeyChar <= '9') || (e.KeyChar == 08) || (e.KeyChar == 46))
            {
                if (dataGridView4.CurrentCell.ColumnIndex > 1)
                {
                    e.Handled = false;
                }

            }
            else
            {
                if (dataGridView4.CurrentCell.ColumnIndex > 1)
                {
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            }

        }
        private void dgr_KeyPress_NumericTester(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar >= '0' & e.KeyChar <= '9') || (e.KeyChar == 08) || (e.KeyChar == 46))
            {
                if (dataGridView1.CurrentCell.ColumnIndex > 1)
                {
                    e.Handled = false;
                } 
                
            }
            else
            {
               if(dataGridView1.CurrentCell.ColumnIndex  > 1)
                {
                    e.Handled = true;
                } else
                {
                    e.Handled = false;
                }
            }

        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cleanForm();
        }
        private bool Revisa_preciosEspecias()
        {
            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                for (int j = 6; j < 11; j++)
                {
                    if (dataGridView4.Rows[i].Cells[j].Value.ToString() != "0")
                    {
                        if (Convert.ToDouble(dataGridView4.Rows[i].Cells[j].Value.ToString()) < Convert.ToDouble(dataGridView4.Rows[i].Cells[5].Value.ToString()))
                        {
                            dataGridView4.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(207, 136, 65);
                            dataGridView4.Rows[i].Cells[j].Style.ForeColor = Color.White;
                            MessageBox.Show("EL PRECIO APLICADO EN UNA PRESENTACION DE ESPECIAS ES MENOR AL PRECIO COSTO !", "INGRESO MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        private bool Revisa_precios()
        {
            for(int i = 0; i< dataGridView1.RowCount; i++)
            {
               for (int j = 6; j<11; j++)
               {
                    if(dataGridView1.Rows[i].Cells[j].Value.ToString() != "0")
                    {
                        if (Convert.ToDouble(dataGridView1.Rows[i].Cells[j].Value.ToString()) < Convert.ToDouble(dataGridView1.Rows[i].Cells[5].Value.ToString()))
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(207, 136, 65);
                            dataGridView1.Rows[i].Cells[j].Style.ForeColor = Color.White;
                            MessageBox.Show("EL PRECIO APLICADO EN UNA PRESENTACION DE ABARROTES ES MENOR AL PRECIO COSTO !", "INGRESO MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
               }
            }
            return true;
        }
        private void cargaSucursales()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE ESTADO_TUPLA = TRUE;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox2.Items.Clear();
                    comboBox1.Items.Clear();
                    comboBox2.Items.Add(reader.GetString(0));
                    comboBox1.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));
                        comboBox1.Items.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        // DESDE ESTE SEGMENTO EMPEZARA TODA LA VAINA CON LO DE EL INGRESO NUEVO DE LA MERCADERIA ESPERO TEMRINARN HOY ! HOY !
        private bool existeMercaderia(string code, string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT ID_MERCADERIA FROM SUCURSAL_HAS_MERCADERIA WHERE ID_MERCADERIA = '{0}'" +
                    " AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{1}');", code, sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //MessageBox.Show("el codigo ya existe");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
            return false;
        }
        private bool agregaCantidadEnSucursal(string codigo, string sucursal, string unidades, string precio)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("INSERT INTO SUCURSAL_HAS_MERCADERIA VALUES " +
                    "('{0}',(SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{1}'),{2},{3}, TRUE);",
                    codigo, sucursal, unidades, precio);
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
            conexion.Close();
            return false;
        }
        private bool existePresentacion(string code, string stock)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT ID_STOCK FROM MERCADERIA_PRESENTACIONES WHERE ID_MERCADERIA = '{0}'" +
                    " AND ID_STOCK = '{1}';", code, stock);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //MessageBox.Show("el codigo ya existe");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
            return false;
        }
        private void ingresaPresentaciones()
        {
            if(dataGridView1.RowCount > 0)
            {
                for(int i = 0; i<dataGridView1.RowCount; i++)
                {

                    if (!existePresentacion(textBox2.Text, dataGridView1.Rows[i].Cells[0].Value.ToString()))
                    {
                        OdbcConnection conexion = ASG_DB.connectionResult();
                        try
                        {
                            conexion.Open();
                            string sql = string.Format("INSERT INTO MERCADERIA_PRESENTACIONES VALUES ('{0}','{1}','{2}'" +
                                ",{3},{4},{5},{6},{7},{8},{9},{10},TRUE);",
                                 dataGridView1.Rows[i].Cells[0].Value.ToString(),
                                 textBox2.Text,
                                 dataGridView1.Rows[i].Cells[1].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[2].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[4].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[5].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[6].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[7].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[8].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[9].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[10].Value.ToString());
                            OdbcCommand cmd = new OdbcCommand(sql, conexion);
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                // se ingreso la presentaciones
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        conexion.Close();
                    } else
                    {
                        // la presetnacion aya exiiste entonces se actualiza
                        OdbcConnection conexion = ASG_DB.connectionResult();
                        try
                        {
                            conexion.Open();
                            string sql = string.Format("CALL ACTUALIZA_PRESENTACION ('{0}','{1}','{2}'" +
                                ",{3},{4},{5},{6},{7},{8},{9},{10});",
                                 dataGridView1.Rows[i].Cells[0].Value.ToString(),
                                 textBox2.Text,
                                 dataGridView1.Rows[i].Cells[1].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[2].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[4].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[5].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[6].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[7].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[8].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[9].Value.ToString(),
                                 dataGridView1.Rows[i].Cells[10].Value.ToString());
                            OdbcCommand cmd = new OdbcCommand(sql, conexion);
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                // se ingreso la presentaciones
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        conexion.Close();
                    }
                }
            }
        }
        private void ingresaPresentacionesML()
        {
            if (dataGridView4.RowCount > 0)
            {
                for (int i = 0; i < dataGridView4.RowCount; i++)
                {

                    if (!existePresentacion(textBox8.Text, dataGridView4.Rows[i].Cells[0].Value.ToString()))
                    {
                        OdbcConnection conexion = ASG_DB.connectionResult();
                        try
                        {
                            conexion.Open();
                            string sql = string.Format("INSERT INTO MERCADERIA_PRESENTACIONES VALUES ('{0}','{1}','{2}'" +
                                ",{3},{4},{5},{6},{7},{8},{9},{10},TRUE);",
                                 dataGridView4.Rows[i].Cells[0].Value.ToString(),
                                 textBox8.Text,
                                 dataGridView4.Rows[i].Cells[1].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[2].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[4].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[5].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[6].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[7].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[8].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[9].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[10].Value.ToString());
                            OdbcCommand cmd = new OdbcCommand(sql, conexion);
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                // se ingreso la presentaciones
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        conexion.Close();
                    }
                    else
                    {
                        // la presetnacion aya exiiste entonces se actualiza
                        OdbcConnection conexion = ASG_DB.connectionResult();
                        try
                        {
                            conexion.Open();
                            string sql = string.Format("CALL ACTUALIZA_PRESENTACION ('{0}','{1}','{2}'" +
                                ",{3},{4},{5},{6},{7},{8},{9},{10});",
                                 dataGridView4.Rows[i].Cells[0].Value.ToString(),
                                 textBox8.Text,
                                 dataGridView4.Rows[i].Cells[1].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[2].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[4].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[5].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[6].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[7].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[8].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[9].Value.ToString(),
                                 dataGridView4.Rows[i].Cells[10].Value.ToString());
                            OdbcCommand cmd = new OdbcCommand(sql, conexion);
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                // se ingreso la presentaciones
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                        conexion.Close();
                    }
                }
            }
        }
        private bool ActualizaCantidadEnSucursal(string codigo,string sucursal, string precio, string unidades)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("UPDATE SUCURSAL_HAS_MERCADERIA " +
                    "SET PRECIO = {0}, TOTAL_UNIDADES = TOTAL_UNIDADES + {1} WHERE " +
                    " ID_MERCADERIA = '{2}' AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{3}');",
                    precio, unidades, codigo, sucursal);
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
            conexion.Close();
            return false;
        }
       
        private void button5_Click(object sender, EventArgs e)
        {
            if (Revisa_precios())
            {
                if ((dataGridView1.Rows.Count > 0) && (textBox1.Text != "") && (textBox2.Text != "") && (textBox7.Text != "") && (textBox3.Text != "") &&( comboBox1.Text != ""))
                {
                    DialogResult result;
                    result = MessageBox.Show("¿DESEA GUARDAR EL PRODUCTO?" + "\n", "GESTION MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        Revisa_precios();
                        if (!existeMercaderia(textBox2.Text, comboBox1.Text))
                        {
                            // NO existe la mercaderia
                            OdbcConnection conexion = ASG_DB.connectionResult();
                            try
                            {
                                conexion.Open();
                                string sql = string.Format("INSERT INTO MERCADERIA VALUES ('{0}', 1, '{1}',{2},{3},TRUE);",
                                    textBox2.Text, textBox1.Text, textBox7.Text, Convert.ToBoolean(checkBox2.Checked));
                                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                                if (cmd.ExecuteNonQuery() == 1)
                                {                                  
                                    // se ingreso correctamente la mercaderia
                                    if (agregaCantidadEnSucursal(textBox2.Text, comboBox1.Text, textBox3.Text, textBox7.Text))
                                    {
                                        ingresaPresentaciones();
                                        var done = new frm_done();
                                        done.Show();
                                        limpiaFormulario();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            conexion.Close();
                        } else
                        {
                            // SI EXISTE ENTONCES ACTUALIZA LOS DISPONIBLES
                            OdbcConnection conexion = ASG_DB.connectionResult();
                            try
                            {
                                conexion.Open();
                                string sql = string.Format("UPDATE  MERCADERIA SET DESCRIPCION = '{0}', " +
                                    "PRECIO_COMPRA = {1} WHERE ID_MERCADERIA = '{2}';",
                                    textBox1.Text, textBox7.Text, textBox2.Text);
                                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                                if ((cmd.ExecuteNonQuery() == 1) | (cmd.ExecuteNonQuery() == 0))
                                {
                                    if (ActualizaCantidadEnSucursal(textBox2.Text, comboBox1.Text, textBox7.Text, textBox3.Text))
                                    {
                                        ingresaPresentaciones();
                                        var done = new frm_done();
                                        done.Show();
                                        limpiaFormulario();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                            conexion.Close();
                        }
                           
                    }
                }
                else
                {
                    MessageBox.Show("DEBE DE LLENAR TODOS LOS DATOS!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }  
        }
        private void guardaPrecios()
        {
            if (Revisa_precios())
            {
                if ((dataGridView1.Rows.Count > 0) && (textBox1.Text != "") && (textBox2.Text != "") && (textBox7.Text != "") && (textBox3.Text != ""))
                {
                    Revisa_precios();
                    inventarioDex();
                    precio_ponderado = "";
                    cleanForm();
                    textBox2.Focus();
                }
                else
                {
                    MessageBox.Show("DEBE DE LLENAR TODOS LOS DATOS!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
           
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaPorNombre();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var forma = new frm_buscaMateriaPrimaS(comboBox1.Text, rolUs, true,1);
            if (forma.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = forma.currentMateria.getCodigo;
                comboBox1.Text = forma.currentMateria.getSucursal;
                if (textBox2.Text != "")
                {
                    getDatosPrincipal(forma.currentMateria.getCodigo, forma.currentMateria.getSucursal);
                    textBox2.ReadOnly = true;
                    textBox7.Text = "";
                    textBox3.Text = "";
                    textBox7.Focus();
                    textBox3.Focus();
                    textBox7.Focus();
                }
            }
        }
        private void getDatosPresentaciones(string codigo)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT * FROM MERCADERIA_PRESENTACIONES WHERE ID_MERCADERIA = '{0}';", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2),
                        reader.GetString(3),0, reader.GetString(4), reader.GetString(5), reader.GetString(6),
                        reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2),
                        reader.GetString(3),0, reader.GetString(4), reader.GetString(5), reader.GetString(6),
                        reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
                    }
                    getCalculosTotalesAbarrotes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void getDatosPresentacionesML(string codigo)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT * FROM MERCADERIA_PRESENTACIONES WHERE ID_MERCADERIA = '{0}';", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView4.Rows.Clear();
                    dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(2),
                        reader.GetString(3), 0, reader.GetString(4), reader.GetString(5), reader.GetString(6),
                        reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
                    while (reader.Read())
                    {
                        dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(2),
                        reader.GetString(3), 0, reader.GetString(4), reader.GetString(5), reader.GetString(6),
                        reader.GetString(7), reader.GetString(8), reader.GetString(9), reader.GetString(10));
                    }
                    getCalculosTotalesEspecias();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void getDatosPrincipal(string codigo, string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT PRECIO, TOTAL_UNIDADES, DESCRIPCION FROM VISTA_MERCADERIA WHERE ID_MERCADERIA = '{0}'" +
                    " AND SUCURSAL = '{1}';", codigo, sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //MessageBox.Show("el codigo ya existe");     
                    textBox5.Text = reader.GetString(0);
                    textBox4.Text = reader.GetString(1);
                    textBox1.Text = reader.GetString(2);
                    getDatosPresentaciones(codigo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void getDatosPrincipalML(string codigo, string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT PRECIO, TOTAL_UNIDADES, DESCRIPCION FROM VISTA_MERCADERIA WHERE ID_MERCADERIA = '{0}'" +
                    " AND SUCURSAL = '{1}';", codigo, sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    //MessageBox.Show("el codigo ya existe");     
                    textBox10.Text = reader.GetString(0);
                    textBox11.Text = reader.GetString(1);
                    textBox9.Text = reader.GetString(2);
                    getDatosPresentacionesML(codigo);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void button23_Click(object sender, EventArgs e)
        {
            var forma = new frm_buscaMateriaPrimaS(comboBox2.Text, rolUs, true, 2);
            if (forma.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = forma.currentMateria.getCodigo;
                comboBox2.Text = forma.currentMateria.getSucursal;
                if (textBox8.Text != "")
                {
                    getDatosPrincipalML(forma.currentMateria.getCodigo, forma.currentMateria.getSucursal);
                    textBox8.ReadOnly = true;
                    textBox12.Text = "";
                    textBox13.Text = "";
                    textBox12.Focus();
                    textBox13.Focus();
                    textBox12.Focus();
                }
            }
        }
        private int getSucursales(string mercaderiaC)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            int temp = 0;
            try
            {
                conexion.Open();
                string sql = string.Format("SELECT COUNT(NOMBRE_SUCURSAL) FROM VISTA_SUCURSALES_PRECIOS WHERE ID_MERCADERIA = '{0}';", mercaderiaC);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    temp = reader.GetInt32(0);
                    return temp;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
            conexion.Close();
            return temp;
        }
        private void button33_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("PARA APLICAR LOS PRECIOS EN LAS SUCURSALES / BODEGAS DEBE GUARDAR LOS DATOS ¿DESEA GUARDAR LOS DATOS?", "TRASLADO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                if (Revisa_precios())
                {
                    if ((dataGridView1.Rows.Count > 0) && (textBox1.Text != "") && (textBox2.Text != "") && (textBox7.Text != "") && (textBox3.Text != "") && (comboBox1.Text != ""))
                    {
                        DialogResult results;
                        results = MessageBox.Show("¿DESEA GUARDAR EL PRODUCTO?" + "\n", "GESTION MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (results == System.Windows.Forms.DialogResult.Yes)
                        {
                            Revisa_precios();
                            if (!existeMercaderia(textBox2.Text, comboBox1.Text))
                            {
                                // NO existe la mercaderia
                                OdbcConnection conexion = ASG_DB.connectionResult();
                                try
                                {
                                    conexion.Open();
                                    string sql = string.Format("INSERT INTO MERCADERIA VALUES ('{0}', 1, '{1}',{2},{3},TRUE);",
                                        textBox2.Text, textBox1.Text, textBox7.Text, Convert.ToBoolean(checkBox2.Checked));
                                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                                    if (cmd.ExecuteNonQuery() == 1)
                                    {
                                        // se ingreso correctamente la mercaderia
                                        if (agregaCantidadEnSucursal(textBox2.Text, comboBox1.Text, textBox3.Text, textBox7.Text))
                                        {
                                            ingresaPresentaciones();
                                            var done = new frm_done();
                                            done.Show();
                                            var sand = new frm_PrecioMateriaPrima(textBox2.Text, comboBox1.Text);
                                            sand.ShowDialog();
                                            limpiaFormulario();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                                conexion.Close();
                            }
                            else
                            {
                                // SI EXISTE ENTONCES ACTUALIZA LOS DISPONIBLES
                                OdbcConnection conexion = ASG_DB.connectionResult();
                                try
                                {
                                    conexion.Open();
                                    string sql = string.Format("UPDATE  MERCADERIA SET DESCRIPCION = '{0}', " +
                                        "PRECIO_COMPRA = {1} WHERE ID_MERCADERIA = '{2}';",
                                        textBox1.Text, textBox7.Text, textBox2.Text);
                                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                                    if ((cmd.ExecuteNonQuery() == 1) | (cmd.ExecuteNonQuery() == 0))
                                    {
                                        if (ActualizaCantidadEnSucursal(textBox2.Text, comboBox1.Text, textBox7.Text, textBox3.Text))
                                        {
                                            ingresaPresentaciones();
                                            var done = new frm_done();
                                            done.Show();
                                            var sand = new frm_PrecioMateriaPrima(textBox2.Text, comboBox1.Text);
                                            sand.ShowDialog();
                                            limpiaFormulario();
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                                conexion.Close();
                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("DEBE DE LLENAR TODOS LOS DATOS!", "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            
        }

        private void button26_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if(textBox7.Text != "")
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(textBox7.Text);
                    preciosUtilidadesAbarrotes();
                }
            } else
            {
                if (textBox5.Text != "")
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        dataGridView1.Rows[i].Cells[5].Value = Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(textBox5.Text);
                        preciosUtilidadesAbarrotes();
                    }
                } 
            }
        }

        private void textBox12_Leave(object sender, EventArgs e)
        {
            if (textBox12.Text != "")
            {
                for (int i = 0; i < dataGridView4.RowCount; i++)
                {
                    dataGridView4.Rows[i].Cells[5].Value = Convert.ToDouble(dataGridView4.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(textBox12.Text);
                    preciosUtilidadesEspecias();
                }
            }
            else
            {
                if (textBox10.Text != "")
                {
                    for (int i = 0; i < dataGridView4.RowCount; i++)
                    {
                        dataGridView4.Rows[i].Cells[5].Value = Convert.ToDouble(dataGridView4.Rows[i].Cells[2].Value.ToString()) * Convert.ToDouble(textBox10.Text);
                        preciosUtilidadesEspecias();
                    }
                }
            }
        }
    }
}
