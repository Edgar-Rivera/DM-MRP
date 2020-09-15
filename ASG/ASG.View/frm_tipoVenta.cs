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
    public partial class frm_tipoVenta : Form
    {
        string codigoProducto;
        string descripcionMercaderia;
        string fechaIngreso;
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string nombreSucursal;
        string usuarioRol;
        string clasificacionMercaderia;
        bool flag = false;
        ContextMenuStrip mymenu = new ContextMenuStrip();
        public frm_tipoVenta(string codigo,string descripcion, string disponible, string fecha, string sucursal, string rol, string clasificacion)
        {
            InitializeComponent();
            codigoProducto = codigo;
            descripcionMercaderia = descripcion;
            clasificacionMercaderia = clasificacion;
            fechaIngreso = fecha;
            if(clasificacion == "Liquido / Masa")
            {
                label13.Text = label13.Text + "Liquido / Masa";
               pictureBox1.Image = Properties.Resources.liter__1_;
            } else
            {
                label13.Text = label13.Text + "Basico";
               pictureBox1.Image = Properties.Resources.shelf__2_;
            }
            stripMenu();        
            label1.Text = label1.Text + " " + codigoProducto;
            label2.Text = label2.Text + " " + descripcionMercaderia;
            label5.Text = label5.Text + " " + disponible;
            label6.Text = label6.Text + " " + fecha;
            label11.Text = label11.Text + " " + sucursal;
            nombreSucursal = sucursal;
            
            initFormualrio();
                
            usuarioRol = rol;
            if(rol != "ADMINISTRADOR" && rol != "ROOT" && rol != "DATA BASE ADMIN")
            {
                dataGridView1.Columns[5].Visible = false;
            }
            getCalculosTotales();
            preciosUtilidadesAbarrotes();
        }
        private void getCalculosTotales()
        {
            if (Convert.ToDouble(label5.Text) > 0)
            {
                // la cantidad del prudcto es maor a 0
                if (dataGridView1.RowCount > 0)
                {
                    double temp = Convert.ToDouble(label5.Text);
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        // CALCULO SE ESPERA LA VARIABLE DE TIPO DE VARIABLE PARA DIVIIR
                        if (clasificacionMercaderia == "Basico")
                        {
                            double tempX = Math.Truncate(temp / Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()));
                            if (tempX > 0) {
                                dataGridView1.Rows[i].Cells[3].Value = string.Format("{0:###,###,###,##}", tempX);
                            } else
                            {
                                dataGridView1.Rows[i].Cells[3].Value = "SIN EXISTENCIAS";
                            }
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[3].Value = string.Format("{0:###,###,###,##0.00##}", Math.Round(temp / Convert.ToDouble(dataGridView1.Rows[i].Cells[2].Value.ToString()), 2));
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
                        dataGridView1.Rows[i].Cells[3].Value = "SIN EXISTENCIAS";
                    }
                }
            }
        }
        private void stripMenu()
        {
            mymenu.Items.Add("Ocultar Fila");
            mymenu.Items[0].Name = "ColHidden";
            mymenu.Items.Add("Ver Sub-Codigos");
            mymenu.Items[1].Name = "ColEdit";
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
                        var forma = new frm_alterCodes(dataGridView1.CurrentRow.Cells[0].Value.ToString(), label2.Text + " - " + dataGridView1.CurrentRow.Cells[1].Value.ToString(), 0);
                        forma.ShowDialog();
                    }
                    mymenu.Visible = false;
                    flag = true;

                }
               
            }
        }
        private void initFormualrio()
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_STOCK WHERE ID_MERCADERIA = '{0}';", codigoProducto);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0),reader.GetString(1), reader.GetString(2), 0,  reader.GetDouble(3), reader.GetString(4),reader.GetDouble(5), reader.GetDouble(6),  reader.GetDouble(7),reader.GetDouble(8),  reader.GetDouble(9), 0,0,0,0,0);
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), 0, reader.GetDouble(3), reader.GetString(4), reader.GetDouble(5), reader.GetDouble(6), reader.GetDouble(7), reader.GetDouble(8), reader.GetDouble(9), 0, 0, 0, 0, 0);
                        //styleDV(this.dataGridView1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
     private void frm_tipoVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.D))
            {
                button2.PerformClick();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.H))
            {
                //button1.PerformClick();
            }
        }

        private void frm_tipoVenta_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_tipoVenta_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }
        private void frm_tipoVenta_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var forma = new frm_vistaGeneral(codigoProducto, descripcionMercaderia, fechaIngreso,0, usuarioRol);
            forma.ShowDialog();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dataGridView1.RowCount > 0)
            {
                var forma = new frm_alterCodes(dataGridView1.CurrentRow.Cells[0].Value.ToString(), label2.Text + " - " + dataGridView1.CurrentRow.Cells[1].Value.ToString(), 0);
                forma.ShowDialog();
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

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
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
        private void frm_tipoVenta_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (codigoProducto!= "")
            {
                var forma = new frm_HistorialPrecios(codigoProducto, "");
                forma.Show();
            }
            else
            {
                MessageBox.Show("El codigo del producto no existe!", "Historial de Precios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
