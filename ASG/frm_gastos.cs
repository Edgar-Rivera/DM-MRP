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
    public partial class frm_gastos : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string nombreUsuario;
        string rolUsuario;
        string usuario;
        string nombreSucursal;
        bool flag = false;
        ContextMenuStrip mymenu = new ContextMenuStrip();
        public frm_gastos(string nameUser, string rolUser, string user, string sucursal)
        {          
            InitializeComponent();
            nombreUsuario = nameUser;
            rolUsuario = rolUser;
            usuario = user;
            nombreSucursal = sucursal;
            label19.Text = label19.Text + "" + nameUser;
            cargaSucursales();
            cargaGastos();
            stripMenu();
        }
        private void stripMenu()
        {
            mymenu.Items.Add("Ocultar Fila");
            mymenu.Items[0].Name = "ColHidden";
            mymenu.Items.Add("Editar Gasto");
            mymenu.Items[1].Name = "ColEdit";
            mymenu.Items.Add("Eliminar Gasto");
            mymenu.Items[2].Name = "ColDelete";
            mymenu.Items.Add("Ver Detalle del Gasto");
            mymenu.Items[3].Name = "ColView";
        }
        internal class codigoGasto
        {
            public string getCode { get; set; }
        }
       
        private void cargaGastos()
        {
            OdbcConnection conexion = ASG_DB.connectionResult(); 
            try
            {
                dataGridView1.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_GASTOS ORDER BY FECHA_GASTO DESC LIMIT 180;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                    }                  
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            conexion.Close();
        }
        private void cargaGastosSucursal(string nombre)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView1.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_GASTOS WHERE NOMBRE_SUCURSAL = '{0}' ORDER BY FECHA_GASTO DESC LIMIT 150;", nombre);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
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
                    comboBox2.Items.Clear();
                    comboBox1.Items.Clear();
                    comboBox5.Items.Clear();
                    comboBox2.Items.Add(reader.GetString(0));
                    comboBox1.Items.Add(reader.GetString(0));
                    comboBox5.Items.Add(reader.GetString(0));
                    //comboBox4.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));
                        comboBox1.Items.Add(reader.GetString(0));
                        comboBox5.Items.Add(reader.GetString(0));
                    }
                    comboBox5.Items.Add("TODAS LAS SUCURSALES");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            conexion.Close();
        }
        private void frm_gastos_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                if (textBox8.Text != "" | textBox6.Text != "" | textBox8.Text != "" | textBox7.Text != "" | textBox2.Text != "" | textBox16.Text != "")
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR SIN GUARDAR LA OPERACION?", "REGISTRO DE GASTOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.S))
            {
                // GUARDAR DATOS EN EDICCION E INGRESO
                if (tabControl1.SelectedTab == tabPage2)
                {
                    button2.PerformClick();
                } else if (tabControl1.SelectedTab == tabPage3)
                {
                    button7.PerformClick();
                }

            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.B))
            {
                // BUSCAR GASTO
                if (tabControl1.SelectedTab == tabPage3)
                {
                    button8.PerformClick();
                }

            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(textBox8.Text != "" | textBox6.Text != "" | textBox8.Text != "" | textBox7.Text != "" | textBox2.Text != "" | textBox16.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR SIN GUARDAR LA OPERACION?", "REGISTRO DE GASTOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    this.Close();
                }
            } else
            {
                this.Close();
            }
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void frm_gastos_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;
        }

        private void frm_gastos_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_gastos_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_gastos_MouseUp(object sender, MouseEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox16.Text == "")
            {
                textBox16.Text = "   ";
            }
            if (textBox2.Text != ""  & textBox4.Text != "" & comboBox1.Text != "")
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {
                    string sql = string.Format("INSERT INTO GASTOS VALUES (null,(SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{0}' AND ESTADO_TUPLA = 1 LIMIT 1),'{1}','{2}','{3}','{4}',{5}, TRUE);", comboBox1.Text, usuario,textBox2.Text, dateTimePicker1.Value.ToString("yyyy-MM-dd"), textBox16.Text,textBox4.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        var forma = new frm_done();
                        forma.ShowDialog();
                        textBox2.Text = "";
                        textBox16.Text = "";
                        textBox4.Text = "";
                        dateTimePicker1.Value = DateTime.Now;
                        comboBox1.SelectedIndex = -1;
                        cargaGastos();
                       
                    }
                } catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            } else
            {
                MessageBox.Show("DEBE DE LLENAR TODOS LOS CAMPOS", "REGISTRO DE GASTOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" | textBox16.Text != "" | textBox4.Text != "" | comboBox1.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "REGISTRO DE GASTOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    textBox2.Text = "";
                    textBox4.Text = "";
                    textBox16.Text = "";
                    comboBox1.SelectedIndex = -1;
                    dateTimePicker1.Value = DateTime.Now;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "" | textBox7.Text != "" | textBox5.Text != "" | comboBox2.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "REGISTRO DE GASTOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    textBox8.Text = "";
                    textBox7.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox14.Text = "";
                    comboBox2.SelectedIndex = -1;
                    dateTimePicker2.Value = DateTime.Now;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox8.Text != "" & textBox7.Text != "" & textBox5.Text != "" & comboBox2.Text != "")
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {
                    string sql = string.Format("CALL ACTUALIZA_GASTO ({0},'{1}','{2}','{3}','{4}',{5});", textBox6.Text.Trim(), comboBox2.Text, textBox8.Text.Trim(), dateTimePicker2.Value.ToString("yyyy-MM-dd"), textBox7.Text, textBox5.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        var forma = new frm_done();
                        forma.ShowDialog();
                        textBox8.Text = "";
                        textBox7.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        textBox14.Text = "";
                        comboBox2.SelectedIndex = -1;
                        dateTimePicker2.Value = DateTime.Now;
                        cargaGastos();
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
            } else
            {
                MessageBox.Show("DEBE DE LLENAR TODOS LOS CAMPOS", "REGISTRO DE GASTOS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cargaGastos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR EL GASTO?", "REGISTRO DE GASTOS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    try
                    {
                        string sql = string.Format("UPDATE GASTOS SET ESTADO_TUPLA = 0 WHERE IDGASTO = {0};",dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        OdbcCommand cmd = new OdbcCommand(sql, conexion);
                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            var forma = new frm_done();
                            forma.ShowDialog();
                            button4.PerformClick();
                        }
                        else
                        {
                            MessageBox.Show("IMPOSIBLE ELIMINAR EL GASTO!", "REGISTRO DE GASTOS", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
        private void getData(string codigo)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView1.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_GASTOS WHERE IDGASTO = {0};", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox6.Text = reader.GetString(0);
                    comboBox2.Text = reader.GetString(1);
                    dateTimePicker2.Value = reader.GetDate(3);
                    textBox8.Text = reader.GetString(4);
                    textBox7.Text = reader.GetString(5);
                    textBox5.Text = reader.GetString(6);
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            var forma = new frm_buscaGasto(nombreSucursal);
             if(forma.ShowDialog() == DialogResult.OK)
             {
                getData(forma.currentGasto.getCode);
                pictureBox2.Visible = true;
             }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {
                    dataGridView1.Rows.Clear();
                    string sql = string.Format("SELECT * FROM VISTA_GASTOS WHERE IDGASTO LIKE '%{0}%'  ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                        }
                    } else
                    {
                        dataGridView1.Rows.Clear();
                        sql = string.Format("SELECT * FROM VISTA_GASTOS WHERE NOMBRE_SUCURSAL LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                            }
                        } else
                        {
                            dataGridView1.Rows.Clear();
                            sql = string.Format("SELECT * FROM VISTA_GASTOS WHERE NOMBRE_USUARIO LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                                }
                            } else
                            {
                                dataGridView1.Rows.Clear();
                                sql = string.Format("SELECT * FROM VISTA_GASTOS WHERE FECHA_GASTO LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                                    }
                                } else
                                {
                                    dataGridView1.Rows.Clear();
                                    sql = string.Format("SELECT * FROM VISTA_GASTOS WHERE DESCRIPCION LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                                    cmd = new OdbcCommand(sql, conexion);
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                                        while (reader.Read())
                                        {
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                                        }
                                    } else
                                    {
                                        dataGridView1.Rows.Clear();
                                        sql = string.Format("SELECT * FROM VISTA_GASTOS WHERE OBSERVACIONES LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                                        cmd = new OdbcCommand(sql, conexion);
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                                            while (reader.Read())
                                            {
                                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                                            }
                                        } else
                                        {
                                            dataGridView1.Rows.Clear();
                                            sql = string.Format("SELECT * FROM VISTA_GASTOS WHERE MONTO LIKE '%{0}%' ORDER BY FECHA_GASTO DESC;", textBox1.Text);
                                            cmd = new OdbcCommand(sql, conexion);
                                            reader = cmd.ExecuteReader();
                                            if (reader.Read())
                                            {
                                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
                                                while (reader.Read())
                                                {
                                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(6)));
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
                cargaGastos();
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox5.Text == "TODAS LAS SUCURSALES")
            {
                cargaGastos();
            } else
            {
                cargaGastosSucursal(comboBox5.Text);
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                var fomra = new frm_vistaGasto(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), dataGridView1.CurrentRow.Cells[3].Value.ToString(), dataGridView1.CurrentRow.Cells[4].Value.ToString(), dataGridView1.CurrentRow.Cells[5].Value.ToString(), dataGridView1.CurrentRow.Cells[6].Value.ToString());
                fomra.Show();
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
                    tabControl1.SelectedTab = tabPage3;
                    textBox14.Text = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    getData(textBox14.Text.Trim());
                    mymenu.Visible = false;
                    flag = true;

                }
                else if (e.ClickedItem.Name == "ColDelete")
                {
                    mymenu.Visible = false;
                    mymenu.Enabled = false;
                    flag = true;
                    button3.PerformClick();
                }
                else if (e.ClickedItem.Name == "ColView")
                {
                    mymenu.Visible = false;
                    mymenu.Enabled = false;
                    SendKeys.Send("{ENTER}");
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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dataGridView1.RowCount > 0)
                {
                    e.SuppressKeyPress = true;
                    var fomra = new frm_vistaGasto(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(), dataGridView1.CurrentRow.Cells[3].Value.ToString(), dataGridView1.CurrentRow.Cells[4].Value.ToString(), dataGridView1.CurrentRow.Cells[5].Value.ToString(), dataGridView1.CurrentRow.Cells[6].Value.ToString());
                    fomra.Show();
                }
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if(textBox7.Text == "   ")
            {
                textBox7.Text = "";
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if(textBox7.Text == "")
            {
                textBox7.Text = "   ";
            }
        }
    }
}
