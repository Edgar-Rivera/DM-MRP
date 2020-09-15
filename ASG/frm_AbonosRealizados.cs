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
    public partial class frm_AbonosRealizados : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        bool flag = false;
        string nombreSucursal;
        ContextMenuStrip mymenu = new ContextMenuStrip();
        public frm_AbonosRealizados(string sucursal)
        {
            InitializeComponent();           
            nombreSucursal = sucursal;
            cargaSucursales();
            cargaAbonos(comboBox5.Text);
            stripMenu();
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
                    // ver cuanta del cliente
                    mymenu.Visible = false;
                    if (dataGridView1.RowCount > 0)
                    {
                        var forma = new frm_detalleCodigo(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                        forma.Show();
                    }
                    flag = true;

                }
                else if (e.ClickedItem.Name == "ColDelete")
                {
                    // ver detalle de factura
                    mymenu.Visible = false;
                    if (dataGridView1.RowCount > 0)
                    {
                        var forma = new frm_detallesCuenta(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[4].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
                        forma.Show();
                    }
                    mymenu.Enabled = false;
                    flag = true;
                   
                }

            }
        }
        private void stripMenu()
        {
            mymenu.Items.Add("Ocultar Fila");
            mymenu.Items[0].Name = "ColHidden";
            mymenu.Items.Add("Ver Detalle de Factura");
            mymenu.Items[1].Name = "ColEdit";
            mymenu.Items.Add("Ver Cuenta del Cliente");
            mymenu.Items[2].Name = "ColDelete";
        }
        private void frm_AbonosRealizados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.D))
            {
                button2.PerformClick();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.S))
            {
                button3.PerformClick();
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cargaAbonos(nombreSucursal);
        }
        private void cargaAbonos(string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView1.Rows.Clear();
                string sql = string.Format("SELECT * FROM RECORD_ABONOS WHERE SUCURSAL = '{0}' ORDER BY FECHA_PAGO DESC limit 125;", sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {                  
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4),  string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2),reader.GetString(3), reader.GetString(4),  string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void frm_AbonosRealizados_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;
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
                    comboBox5.Items.Clear();
                    comboBox5.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox5.Items.Add(reader.GetString(0));
                    }
                    comboBox5.Text = nombreSucursal;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void frm_AbonosRealizados_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_AbonosRealizados_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_AbonosRealizados_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView1.Rows.Clear();
                string sql = string.Format("SELECT * FROM RECORD_ABONOS where  FECHA_PAGO LIKE '%{0}%' and sucursal = '{1}';", dateTimePicker1.Value.ToString("yyyy-MM-dd"), comboBox5.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {
                   
                    string sql = string.Format("SELECT * FROM RECORD_ABONOS where id_cliente like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                        }
                    } else
                    {
                        conexion.Close();
                        conexion = ASG_DB.connectionResult();
                        sql = string.Format("SELECT * FROM RECORD_ABONOS where nombre like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Clear();
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                            }
                        } else
                        {
                            conexion.Close();
                            conexion = ASG_DB.connectionResult();
                            sql = string.Format("SELECT * FROM RECORD_ABONOS where id_factura like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                }
                            } else
                            {
                                conexion.Close();
                                conexion = ASG_DB.connectionResult();
                                sql = string.Format("SELECT * FROM RECORD_ABONOS where id_pago like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                    }
                                }
                                else {

                                    conexion.Close();
                                    conexion = ASG_DB.connectionResult();
                                    sql = string.Format("SELECT * FROM RECORD_ABONOS where id_cuenta_por_cobrar like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                                    cmd = new OdbcCommand(sql, conexion);
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        dataGridView1.Rows.Clear();
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                        while (reader.Read())
                                        {
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                        }
                                    } else
                                    {
                                        conexion.Close();
                                        conexion = ASG_DB.connectionResult();
                                        sql = string.Format("SELECT * FROM RECORD_ABONOS where total like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                                        cmd = new OdbcCommand(sql, conexion);
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            dataGridView1.Rows.Clear();
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                            while (reader.Read())
                                            {
                                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                            }
                                        } else
                                        {
                                            conexion.Close();
                                            conexion = ASG_DB.connectionResult();
                                            sql = string.Format("SELECT * FROM RECORD_ABONOS where fecha_pago like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                                            cmd = new OdbcCommand(sql, conexion);
                                            reader = cmd.ExecuteReader();
                                            if (reader.Read())
                                            {
                                                dataGridView1.Rows.Clear();
                                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                                while (reader.Read())
                                                {
                                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
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
                cargaAbonos(comboBox5.Text);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount > 0)
            {
                var forma = new frm_detalleCodigo(dataGridView1.CurrentRow.Cells[2].Value.ToString());
                forma.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                var forma = new frm_detallesCuenta(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[4].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
                forma.Show();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaAbonos(comboBox5.Text);
        }
    }
}
