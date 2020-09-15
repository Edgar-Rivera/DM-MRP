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
    public partial class frm_buscaGasto : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string codigo;
        string nombreSucursal;
        public frm_buscaGasto(string sucursal)
        {
            
            InitializeComponent();
            nombreSucursal = sucursal;
            cargaGastos(nombreSucursal);
            cargaSucursales();
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
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add(reader.GetString(0));
                    //comboBox4.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));
                       
                    }
                    comboBox2.Text = nombreSucursal;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            conexion.Close();
        }
        private void cargaGastos(string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView1.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_GASTOS_BUSCAR WHERE NOMBRE_SUCURSAL = '{0}' ORDER BY FECHA_GASTO DESC LIMIT 80;", sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            conexion.Close();
        }
        private void frm_buscaGasto_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frm_buscaGasto_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;

        }

        private void frm_buscaGasto_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_buscaGasto_MouseUp(object sender, MouseEventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            cargaGastos(nombreSucursal);
        }
        internal frm_gastos.codigoGasto currentGasto
        {
            get
            {
                return new frm_gastos.codigoGasto()
                {
                    getCode = codigo
                };
            }
            set
            {
                currentGasto.getCode = codigo;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.Text != nombreSucursal)
            {
                MessageBox.Show("ACCEDERA A LOS GASTOS QUE SE ENCUENTRAN EN OTRA SUCURSAL", "BUSCAR GASTO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cargaGastos(comboBox2.Text);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {
                    dataGridView1.Rows.Clear();
                    string sql = string.Format("SELECT * FROM VISTA_GASTOS_BUSCAR WHERE NOMBRE_SUCURSAL LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                        }
                    }
                    else
                    {
                        dataGridView1.Rows.Clear();
                        sql = string.Format("SELECT * FROM VISTA_GASTOS_BUSCAR WHERE IDGASTO LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                            }
                        }
                        else
                        {
                            dataGridView1.Rows.Clear();
                            sql = string.Format("SELECT * FROM VISTA_GASTOS_BUSCAR WHERE DESCRIPCION LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                                }
                            }
                            else
                            {
                                dataGridView1.Rows.Clear();
                                sql = string.Format("SELECT * FROM VISTA_GASTOS_BUSCAR WHERE NOMBRE_USUARIO LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                                    }
                                }
                                else
                                {
                                    dataGridView1.Rows.Clear();
                                    sql = string.Format("SELECT * FROM VISTA_GASTOS_BUSCAR WHERE FECHA_GASTO LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                                    cmd = new OdbcCommand(sql, conexion);
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                                        while (reader.Read())
                                        {
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                                        }
                                    }
                                    else
                                    {
                                        dataGridView1.Rows.Clear();
                                        sql = string.Format("SELECT * FROM VISTA_GASTOS_BUSCAR WHERE MONTO LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                                        cmd = new OdbcCommand(sql, conexion);
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                                            while (reader.Read())
                                            {
                                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                                            }
                                        }
                                        else
                                        {
                                            dataGridView1.Rows.Clear();
                                            sql = string.Format("SELECT * FROM VISTA_GASTOS_BUSCAR WHERE OBSERVACIONES LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                                            cmd = new OdbcCommand(sql, conexion);
                                            reader = cmd.ExecuteReader();
                                            if (reader.Read())
                                            {
                                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
                                                while (reader.Read())
                                                {
                                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)));
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
                cargaGastos(nombreSucursal);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (dataGridView1.RowCount > 0)
                {
                    dataGridView1.Focus();
                    if (dataGridView1.RowCount > 1)
                        this.dataGridView1.CurrentCell = this.dataGridView1[1, dataGridView1.CurrentRow.Index + 1];
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if(dataGridView1.RowCount > 0)
                {
                    codigo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dataGridView1.RowCount > 0)
                {
                    codigo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    DialogResult = DialogResult.OK;
                }

            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                codigo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                DialogResult = DialogResult.OK;
            }
        }

        private void frm_buscaGasto_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;
        }
    }
}
