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
    public partial class frm_AbonosProveedoresRealizados : Form
    {
        ContextMenuStrip mymenu = new ContextMenuStrip();
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        bool flag = false;
        public frm_AbonosProveedoresRealizados()
        {
            InitializeComponent();
            stripMenu();
            cargaAbonos();
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
                        var fomra = new frm_detalleCompra(dataGridView1.CurrentRow.Cells[2].Value.ToString(), null, null, null, false);
                        fomra.Show();
                    }
                    flag = true;

                }
                else if (e.ClickedItem.Name == "ColDelete")
                {
                    // ver detalle de factura
                    mymenu.Visible = false;
                    if (dataGridView1.RowCount > 0)
                    {
                        var forma = new frm_detalleCuentaPagar(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[4].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
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
            mymenu.Items.Add("Ver Detalle de Compra");
            mymenu.Items[1].Name = "ColEdit";
            mymenu.Items.Add("Ver Cuenta del Proveedor");
            mymenu.Items[2].Name = "ColDelete";
        }
        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void frm_AbonosProveedoresRealizados_KeyDown(object sender, KeyEventArgs e)
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void cargaAbonos()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView1.Rows.Clear();
                string sql = string.Format("SELECT * FROM RECORD_ABONOS_PROVEEDOR ORDER BY FECHA_PAGO DESC limit 125;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            cargaAbonos();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {

                    string sql = string.Format("SELECT * FROM RECORD_ABONOS_PROVEEDOR where id_proveedor like '%{0}' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                        }
                    }
                    else
                    {
                        conexion.Close();
                        conexion = ASG_DB.connectionResult();
                        sql = string.Format("SELECT * FROM RECORD_ABONOS_PROVEEDOR where nombre_proveedor like '%{0}' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Clear();
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                            }
                        }
                        else
                        {
                            conexion.Close();
                            conexion = ASG_DB.connectionResult();
                            sql = string.Format("SELECT * FROM RECORD_ABONOS_PROVEEDOR where id_COMPRA like '%{0}' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                                }
                            }
                            else
                            {
                                conexion.Close();
                                conexion = ASG_DB.connectionResult();
                                sql = string.Format("SELECT * FROM RECORD_ABONOS_PROVEEDOR where id_pago like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                                    }
                                }
                                else
                                {

                                    conexion.Close();
                                    conexion = ASG_DB.connectionResult();
                                    sql = string.Format("SELECT * FROM RECORD_ABONOS_PROVEEDOR where id_cuenta_por_pagar like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                                    cmd = new OdbcCommand(sql, conexion);
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        dataGridView1.Rows.Clear();
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                                        while (reader.Read())
                                        {
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                                        }
                                    }
                                    else
                                    {
                                        conexion.Close();
                                        conexion = ASG_DB.connectionResult();
                                        sql = string.Format("SELECT * FROM RECORD_ABONOS where total_pago like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                                        cmd = new OdbcCommand(sql, conexion);
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            dataGridView1.Rows.Clear();
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                                            while (reader.Read())
                                            {
                                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                                            }
                                        }
                                        else
                                        {
                                            conexion.Close();
                                            conexion = ASG_DB.connectionResult();
                                            sql = string.Format("SELECT * FROM RECORD_ABONOS where fecha_pago like '%{0}%' ORDER BY FECHA_PAGO DESC limit 125;", textBox1.Text);
                                            cmd = new OdbcCommand(sql, conexion);
                                            reader = cmd.ExecuteReader();
                                            if (reader.Read())
                                            {
                                                dataGridView1.Rows.Clear();
                                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                                                while (reader.Read())
                                                {
                                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
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
            }
            else
            {
                cargaAbonos();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView1.Rows.Clear();
                string sql = string.Format("SELECT * FROM RECORD_ABONOS_PROVEEDOR where FECHA_PAGO LIKE '%{0}%';", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), string.Format("{0:###,###,###,##0.00##}", reader.GetDouble(5)), reader.GetString(6));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }

        private void frm_AbonosProveedoresRealizados_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
           
        }

        private void frm_AbonosProveedoresRealizados_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_AbonosProveedoresRealizados_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_AbonosProveedoresRealizados_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount > 0)
            {
                var fomra = new frm_detalleCompra(dataGridView1.CurrentRow.Cells[2].Value.ToString(), null,null,null, false);
                fomra.Show();
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

        private void button3_Click(object sender, EventArgs e)
        {
            if(dataGridView1.RowCount > 0)
            {
                var forma = new frm_detalleCuentaPagar(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[4].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString());
                forma.Show();
            }
        }
    }
}
