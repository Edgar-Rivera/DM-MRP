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

namespace ASG
{
    public partial class frm_materiaPrima : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string nameUs;
        string rolUs;
        string usuario;
        string idSucursal;
        string nombreSucursal;
        bool flag = false;
        string Traslado;
        ContextMenuStrip mymenu = new ContextMenuStrip();
        public frm_materiaPrima(string nameUser, string rolUser, string user, string sucursal, string nombre)
        {
            InitializeComponent();
            usuario = user;
            label19.Text = label19.Text + nameUser;
            nameUs = nameUser;
            rolUs = rolUser;
            nombreSucursal = nombre;
            idSucursal = sucursal;
            cargaSucursales();
            comboBox2.Text = nombreSucursal;
            cargaMateriaPrima(comboBox2.Text);
            stripMenu();
            cargaUnidadesMedida();
            cargaProveedores();
            cargaTraslados();
        }
        private void stripMenu()
        {
            mymenu.Items.Add("Ocultar Fila");
            mymenu.Items[0].Name = "ColHidden";
            mymenu.Items.Add("Editar Materia Prima");
            mymenu.Items[1].Name = "ColEdit";
            mymenu.Items.Add("Eliminar Materia Prima");
            mymenu.Items[2].Name = "ColDelete";
            mymenu.Items[2].Enabled = false;
            mymenu.Items.Add("Activar / Desactivar Materia Prima");
            mymenu.Items[3].Name = "ColDesac";
            mymenu.Items.Add("Ver Datos de Materia Prima");
            mymenu.Items[4].Name = "ColGeneral";
            mymenu.Items.Add("Ver Diponibilidad de Materia Prima");
            mymenu.Items[5].Name = "ColDisp";
        }
        private void cargaMateriaPrima(string sucursal)
        {
            try
            {
                dataGridView1.Rows.Clear();
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA_FORM WHERE SUCURSAL = '{0}';", sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                        //styleDV(this.dataGridView1);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
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
                    comboBox4.Items.Clear();
                    comboBox4.Items.Add(reader.GetString(0));
                    comboBox2.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox4.Items.Add(reader.GetString(0));
                        comboBox2.Items.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void frm_materiaPrima_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if (textBox9.Text != "" | textBox14.Text != "")
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "GESTION MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.B))
            {
                if (tabControl1.SelectedTab == tabPage2)
                {
                    button22.PerformClick();
                } else if (tabControl1.SelectedTab == tabPage4)
                {
                    button10.PerformClick();
                }
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.S))
            {
                if (tabControl1.SelectedTab == tabPage2)
                {
                    button13.PerformClick();
                } else if (tabControl1.SelectedTab == tabPage4)
                {
                    button14.PerformClick();
                }
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.P))
            {
                if (tabControl1.SelectedTab == tabPage2)
                {
                   
                }
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.H))
            {
                if (tabControl1.SelectedTab == tabPage2)
                {
                    button25.PerformClick();
                }
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.L))
            {
                if (tabControl1.SelectedTab == tabPage4)
                {
                    button36.PerformClick();
                }
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.O))
            {
                if (tabControl1.SelectedTab == tabPage2)
                {
                    button33.PerformClick();
                }
            }
        }

        private void frm_materiaPrima_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_materiaPrima_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_materiaPrima_MouseUp(object sender, MouseEventArgs e)
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

        private void pictureBox3_Click(object sender, EventArgs e)
        {
           if(textBox9.Text != "" | textBox14.Text != "")
           {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "GESTION MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
        private void cargaProveedores()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT NOMBRE_PROVEEDOR FROM PROVEEDOR WHERE ESTADO_TUPLA = TRUE;");
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void cargaUnidadesMedida()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {

                string sql = string.Format("SELECT NOMBRE FROM UNIDAD_MEDIDA;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox3.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader.GetString(0));
                    }
                }
                else
                {
                    // MOSTARA EN LABEL NO EXISTEN PRODUCTOS
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void frm_materiaPrima_Load(object sender, EventArgs e)
        {

        }
        private void cargaTotal(string codigo)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format(" SELECT COUNT(ID_MATERIA_PRIMA) FROM SUCURSAL_HAS_MATERIA_PRIMA" +
                    " WHERE ESTADO_TUPLA = TRUE AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{0}' AND ESTADO_TUPLA = TRUE LIMIT 1);", codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {


                    label59.Text = reader.GetString(0);

                }
                else
                {
                    label59.Text = "0";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "ESTADISTICAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void button17_Click(object sender, EventArgs e)
        {
            cargaMateriaPrima(nombreSucursal);
            comboBox2.Text = nombreSucursal;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text != "")
            {
                try
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA_FORM WHERE SUCURSAL = '{0}' AND ID_MATERIA_PRIMA LIKE '%{1}%';", comboBox2.Text, textBox8.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                            //styleDV(this.dataGridView1);
                        }
                    }
                    else
                    {
                        sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA_FORM WHERE SUCURSAL = '{0}' AND DESCRIPCION LIKE '%{1}%';", comboBox2.Text, textBox8.Text);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Clear();
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                //styleDV(this.dataGridView1);
                            }
                        }
                        else
                        {
                            sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA_FORM WHERE SUCURSAL = '{0}' AND PROVEEDOR LIKE '%{1}%';", comboBox2.Text, textBox8.Text);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                    //styleDV(this.dataGridView1);
                                }
                            }
                            else
                            {
                                sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA_FORM WHERE SUCURSAL = '{0}' AND PRECIO LIKE '%{1}%';", comboBox2.Text, textBox8.Text);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                        //styleDV(this.dataGridView1);
                                    }
                                }
                                else
                                {
                                    sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA_FORM WHERE SUCURSAL = '{0}' AND TOTAL_UNIDADES LIKE '%{1}%';", comboBox2.Text, textBox8.Text);
                                    cmd = new OdbcCommand(sql, conexion);
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        dataGridView1.Rows.Clear();
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                        while (reader.Read())
                                        {
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                            //styleDV(this.dataGridView1);
                                        }
                                    } else
                                    {
                                        sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA_FORM WHERE SUCURSAL = '{0}' AND MEDIDA LIKE '%{1}%';", comboBox2.Text, textBox8.Text);
                                        cmd = new OdbcCommand(sql, conexion);
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            dataGridView1.Rows.Clear();
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                            while (reader.Read())
                                            {
                                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6), reader.GetString(7));
                                                //styleDV(this.dataGridView1);
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
            }
            else
            {
                cargaMateriaPrima(comboBox2.Text);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaTotal(comboBox2.Text);
            cargaMateriaPrima(comboBox2.Text);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "TODOS")
            {
                button17.PerformClick();
            } else if (comboBox1.Text == "PRODUCTOS AGOTADOS")
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA_FORM WHERE SUCURSAL = '{0}' AND TOTAL_UNIDADES <= 0;", comboBox2.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6));
                            //styleDV(this.dataGridView1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            } else if (comboBox1.Text == "PRODUCTOS INACTIVOS")
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA_FORM WHERE SUCURSAL = '{0}' AND ESTADO = 'INACTIVO';", comboBox2.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {

                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)), reader.GetString(6));
                            //styleDV(this.dataGridView1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                var xcode = new frm_VistaMateriaPrima(dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                   dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                   dataGridView1.CurrentRow.Cells[3].Value.ToString(),
                   dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                   dataGridView1.CurrentRow.Cells[5].Value.ToString(),
                   dataGridView1.CurrentRow.Cells[6].Value.ToString(),
                   dataGridView1.CurrentRow.Cells[4].Value.ToString(), rolUs);
                xcode.Show();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dataGridView1.RowCount > 0)
                {
                    var xcode = new frm_VistaMateriaPrima(dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[3].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[5].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[6].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[4].Value.ToString(), rolUs);
                    xcode.Show();
                }
            }
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dataGridView1.RowCount > 0)
                {
                    var xcode = new frm_VistaMateriaPrima(dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[3].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[5].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[6].Value.ToString(),
                       dataGridView1.CurrentRow.Cells[4].Value.ToString(), rolUs);
                    xcode.Show();
                }
            }
            else if (e.KeyData == Keys.Down)
            {
                if (dataGridView1.RowCount > 0)
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
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "ACTIVO")
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA DESACTIVAR LA MATERIA PRIMA?" + "\n" + "  " + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + " - " + this.dataGridView1.CurrentRow.Cells[1].Value.ToString(), "GESTION MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        OdbcConnection conexion = ASG_DB.connectionResult();
                        try
                        {
                            string sql = string.Format("UPDATE SUCURSAL_HAS_MATERIA_PRIMA SET ESTADO_TUPLA = FALSE WHERE ID_MATERIA_PRIMA = '{0}' AND " +
                                "ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{1}');",
                                this.dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                                this.dataGridView1.CurrentRow.Cells[6].Value.ToString());
                            OdbcCommand cmd = new OdbcCommand(sql, conexion);
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                adressUser.ingresaBitacora(idSucursal, usuario, "MATERIA PRIMA DESACTIVADA", this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                                var done = new frm_done();
                                done.Show();
                            }
                            else
                            {
                                MessageBox.Show("NO SE ENCONTRO LA MATERIA PRIMA A DESACTIVAR!", "GESTION MATERIA PRIMA", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                        button17.PerformClick();
                    }
                }
                else
                {
                    MessageBox.Show("LA MATERIA PRIMA SE ENCUENTRA INACTIVA!", "GESTION MATERIA PRIMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var forma = new frm_buscaProveedor(1, nameUs, rolUs, usuario, idSucursal);

            if (forma.ShowDialog() == DialogResult.OK)
            {
                comboBox5.Items.Clear();
                cargaProveedores();
                comboBox5.Text = forma.currentProveedor.getProveedor;

            }
        }
        private void getDataMateria(string codigo, string sucursal)
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA WHERE SUCURSAL = '{0}' " +
                    "AND ID_MATERIA_PRIMA = '{1}';", sucursal, codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox9.Text = reader.GetString(0);
                    textBox6.Text = reader.GetString(1);
                    comboBox5.Text = reader.GetString(2);
                    textBox5.Text = reader.GetString(3);
                    textBox4.Text = reader.GetString(4);
                    textBox2.Text = reader.GetString(5);
                    comboBox3.Text = reader.GetString(6);
                    textBox7.Focus();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void generaBarcode(string barString)
        {
            BarcodeLib.Barcode Codigo = new BarcodeLib.Barcode();
            Codigo.IncludeLabel = true;
            pictureBox1.Image = Codigo.Encode(BarcodeLib.TYPE.CODE128, barString);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        }
        
        private void button22_Click(object sender, EventArgs e)
        {
            var forma = new frm_buscaMateriaPrima(nombreSucursal, rolUs, true);
            if (forma.ShowDialog() == DialogResult.OK)
            {
                getDataMateria(forma.currentMateria.getCodigo, forma.currentMateria.getSucursal);
                generaBarcode(forma.currentMateria.getCodigo);
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (textBox9.Text != "" | textBox6.Text != "" | textBox5.Text != "" | textBox4.Text != "" | textBox2.Text != "" |
                comboBox5.Text != "" | comboBox3.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "GESTION MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    textBox9.Text = "";
                    textBox6.Text = "";
                    textBox5.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                    textBox3.Text = "";                   
                    textBox2.Text = "";
                    comboBox3.SelectedIndex = -1;
                    comboBox5.SelectedIndex = -1;
                    pictureBox1.Image = null;
                }
            }
        }
        private bool ActualizaCantidadEnSucursal(string precio, string cantidad)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE SUCURSAL_HAS_MATERIA_PRIMA " +
                    "SET PRECIO = {0}, TOTAL_UNIDADES = {1} WHERE " +
                    " ID_MATERIA_PRIMA = '{2}' AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{3}');",
                    precio, cantidad, textBox9.Text, textBox2.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if ((cmd.ExecuteNonQuery() == 1) |(cmd.ExecuteNonQuery() == 0))
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
        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox9.Text != "" & textBox6.Text != "" & textBox5.Text != "" & textBox4.Text != "" & textBox2.Text != "" &
                comboBox5.Text != "" & comboBox3.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA GUARDAR LOS CAMBIOS?", "GESTION MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    try
                    {
                        string sql = string.Format("UPDATE  MATERIA_PRIMA SET DESCRIPCION = '{0}', " +
                            "ID_PROVEEDOR = (SELECT ID_PROVEEDOR FROM PROVEEDOR WHERE NOMBRE_PROVEEDOR = '{1}'), " +
                            "ID_UNIDAD_MEDIDA = (SELECT ID_UNIDAD_MEDIDA FROM UNIDAD_MEDIDA WHERE NOMBRE = '{2}')" +
                            " WHERE ID_MATERIA_PRIMA = '{3}';",
                            textBox6.Text, comboBox5.Text, comboBox3.Text, textBox9.Text);
                        OdbcCommand cmd = new OdbcCommand(sql, conexion);
                        if ((cmd.ExecuteNonQuery() == 1) | (cmd.ExecuteNonQuery() == 0))
                        {
                            string precio; string cantidad;
                            if(textBox7.Text != "")
                            {
                                precio = textBox7.Text;
                            } else
                            {
                                precio = textBox5.Text;
                            }
                            if (textBox3.Text != "")
                            {
                                cantidad = textBox3.Text;
                            }
                            else
                            {
                                cantidad = textBox4.Text;
                            }
                            if (ActualizaCantidadEnSucursal(precio, cantidad))
                            {
                                var done = new frm_done();
                                done.Show();
                                textBox9.Text = "";
                                textBox6.Text = "";
                                textBox5.Text = "";
                                textBox4.Text = "";
                                textBox7.Text = "";
                                textBox3.Text = "";
                                textBox2.Text = "";
                                comboBox3.SelectedIndex = -1;
                                comboBox5.SelectedIndex = -1;
                                pictureBox1.Image = null;
                                cargaMateriaPrima(nombreSucursal);
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
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
                MessageBox.Show("NO SE HA GENERADO CODIGO DE BARRA!", "GESTION MATERIA PRIMA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void my_menu_ItemclickedE(object sender, ToolStripItemClickedEventArgs e)
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
                    tabControl1.SelectedTab = tabPage2;
                    getDataMateria(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[6].Value.ToString());
                    flag = true;
                }
                else if (e.ClickedItem.Name == "ColDelete")
                {
                    flag = true;
                    mymenu.Visible = false;
                    button11.PerformClick();                
                }
                else if (e.ClickedItem.Name == "ColDesac")
                {
                    flag = true;
                    mymenu.Visible = false;
                    if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "ACTIVO")
                    {
                       
                        button11.PerformClick();
                        button17.PerformClick();
                    }
                    else
                    {
                        activaMateria();
                        button17.PerformClick();
                    }

                }
                
               
                else if (e.ClickedItem.Name == "ColGeneral")
                {
                    flag = true;
                    mymenu.Visible = false;
                    if (dataGridView1.RowCount > 0)
                    {
                        var xcode = new frm_VistaMateriaPrima(dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                           dataGridView1.CurrentRow.Cells[1].Value.ToString(),
                           dataGridView1.CurrentRow.Cells[3].Value.ToString(),
                           dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                           dataGridView1.CurrentRow.Cells[5].Value.ToString(),
                           dataGridView1.CurrentRow.Cells[6].Value.ToString(),
                           dataGridView1.CurrentRow.Cells[4].Value.ToString(), rolUs);
                        xcode.Show();
                    }
                }
                else if (e.ClickedItem.Name == "ColDisp")
                {
                    flag = true;
                    mymenu.Visible = false;
                    var forma = new frm_VistaGeneralMateria(dataGridView1.CurrentRow.Cells[0].Value.ToString(), dataGridView1.CurrentRow.Cells[1].Value.ToString(), 0, rolUs);
                    forma.ShowDialog();
                }
            }
        }
      
        private void activaMateria()
        {
            if (this.dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                if (dataGridView1.CurrentRow.Cells[7].Value.ToString() == "INACTIVO")
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ACTIVAR LA MATERIA PRIMA?" + "\n" + "  " + this.dataGridView1.CurrentRow.Cells[0].Value.ToString() + " - " + this.dataGridView1.CurrentRow.Cells[1].Value.ToString(), "GESTION MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        OdbcConnection conexion = ASG_DB.connectionResult();
                        try
                        {
                            string sql = string.Format("UPDATE SUCURSAL_HAS_MATERIA_PRIMA SET ESTADO_TUPLA = TRUE WHERE ID_MATERIA_PRIMA = '{0}' AND " +
                                "ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{1}');",
                                this.dataGridView1.CurrentRow.Cells[0].Value.ToString(),
                                this.dataGridView1.CurrentRow.Cells[6].Value.ToString());
                            OdbcCommand cmd = new OdbcCommand(sql, conexion);
                            if (cmd.ExecuteNonQuery() == 1)
                            {
                                adressUser.ingresaBitacora(idSucursal, usuario, "MATERIA PRIMA ACTIVADA", this.dataGridView1.CurrentRow.Cells[0].Value.ToString());
                                var done = new frm_done();
                                done.Show();
                            }
                            else
                            {
                                MessageBox.Show("NO SE ENCONTRO LA MATERIA PRIMA A DESACTIVAR!", "GESTION MATERIA PRIMA", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                        button17.PerformClick();
                    }
                }
                else
                {
                    MessageBox.Show("LA MATERIA PRIMA SE ENCUENTRA INACTIVA!", "GESTION MATERIA PRIMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                if ((e.Button == MouseButtons.Right))
                {
                    mymenu.Show(dataGridView1, new Point(e.X, e.Y));
                    mymenu.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemclickedE);
                    mymenu.Enabled = true;
                    flag = false;
                }
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if(textBox9.Text != "")
            {
                var forma = new frm_HistorialPrecios(textBox9.Text.Trim(), textBox2.Text);
                forma.Show();
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun producto!", "Historial de Precios", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }
        private void getDatosTraslado(string codigo, string sucursal)
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA WHERE SUCURSAL = '{0}' " +
                    "AND ID_MATERIA_PRIMA = '{1}';", sucursal, codigo);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox14.Text = reader.GetString(0);
                    textBox12.Text = reader.GetString(0);
                    textBox11.Text = reader.GetString(1);
                    textBox13.Text = reader.GetString(2);
                    textBox1.Text = reader.GetString(3);
                    textBox10.Text = reader.GetString(4);
                    textBox17.Text = reader.GetString(5);
                    textBox15.Text = reader.GetString(6);
                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            var forma = new frm_buscaMateriaPrima(nombreSucursal, rolUs, true);
            if (forma.ShowDialog() == DialogResult.OK)
            {
                getDatosTraslado(forma.currentMateria.getCodigo, forma.currentMateria.getSucursal);
            }
        }
        private bool codigoTraslado(string code)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT ID_TRASLADO FROM TRASLADOS_MATERIA_PRIMA WHERE ID_TRASLADO =  {0};", code);
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
        
        public string createCodeTraslado(int longitud)
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
        private string numeroTraslado()
        {
            bool codex = true;
            while (codex)
            {
                string temp = createCodeTraslado(7);
                if (codigoTraslado(temp))
                {
                    codex = false;
                    return temp;
                }

            }
            return null;
        }
        // crea un nuevo traslado para la mercaeria
        private bool creaNuevoTraslado()
        {
            Traslado = numeroTraslado();
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("INSERT INTO TRASLADOS_MATERIA_PRIMA VALUES ({0},'{1}', (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{2}'),(SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{3}'), NOW(), TRUE);",
                   Traslado, usuario, textBox17.Text, comboBox4.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    adressUser.ingresaBitacora(idSucursal, usuario, "TRASLADO DE MATERIA PRIMA", Traslado);
                    return true;
                }
                else
                {
                    MessageBox.Show("NO SE CREO EL NUEVO TRASLADO!", "TRASLADOS MATERIA PRIMA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
            return false;
        }
        // actualiza de donde se traslado y a donde se traslado si existe el porudcto
        private bool ActualizaCantidadEnSucursalTraslado(string cantidad, string codigo, string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE SUCURSAL_HAS_MATERIA_PRIMA " +
                    "SET TOTAL_UNIDADES = {0} WHERE " +
                    " ID_MATERIA_PRIMA = '{1}' AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{2}');",
                     cantidad, codigo, sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if ((cmd.ExecuteNonQuery() == 1))
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
        private bool existeEnSucursal(string codigo, string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT ID_MATERIA_PRIMA FROM  SUCURSAL_HAS_MATERIA_PRIMA " +
                    " WHERE " +
                    " ID_MATERIA_PRIMA = '{0}' AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{1}');",
                     codigo, sucursal);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())                  
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
        // ingresa detalle de producto de tarslaod
        private bool ingresaTraslado()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("CALL INGRESA_PRODUCTOS_TRASLADO_MATERIA ({0},'{1}',{2});", Traslado, textBox12.Text, textBox20.Text);
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
        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox14.Text != "" & textBox12.Text != "" & textBox11.Text != "" & textBox1.Text != "" & textBox13.Text != "" &
                textBox15.Text != "" & textBox17.Text != "" & textBox10.Text != "" & textBox22.Text != "" &
                textBox16.Text != "" & textBox20.Text != "" & textBox18.Text != "" & comboBox4.Text != "")
            {
                // empieza a guardar todo
                if (creaNuevoTraslado())
                {
                    if (ActualizaCantidadEnSucursalTraslado(textBox22.Text, textBox12.Text, textBox17.Text))
                    {
                        // se actualizo de donde se traslado
                        if (existeEnSucursal(textBox12.Text, comboBox4.Text))
                        {
                            // el proudcto ya existia en sucursal asi que hay que actualizar el disponibel de unidades
                            if (ActualizaCantidadEnSucursalTraslado(textBox18.Text, textBox12.Text, comboBox4.Text))
                            {
                                // se agrega este detalle al trastlado ya existente
                                if (ingresaTraslado())
                                {
                                    var done = new frm_done();
                                    done.Show();
                                    limpiaTraslado();
                                }
                            }
                        }
                        else
                        {
                            // se crea el producto en la tabla de has
                           // MessageBox.Show("el proudcto no existe");
                            OdbcConnection conexion = ASG_DB.connectionResult();
                            try
                            {
                                string sql = string.Format("INSERT INTO SUCURSAL_HAS_MATERIA_PRIMA VALUES " +
                                    "((SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{0}'),'{1}',{2},{3}, TRUE);",
                                    comboBox4.Text, textBox12.Text, textBox18.Text, textBox1.Text);
                                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                                if (cmd.ExecuteNonQuery() == 1)
                                {
                                    //MessageBox.Show("se guardo en has");
                                    if (ingresaTraslado())
                                    {
                                        var done = new frm_done();
                                        done.Show();
                                        limpiaTraslado();
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
            }    
         }
        
        private void limpiaTraslado()
        {
            textBox14.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox1.Text = "";
            textBox13.Text = "";
            textBox15.Text = "";
            textBox17.Text = "";
            textBox10.Text = "";
            textBox22.Text = "";
            textBox16.Text = "";
            textBox18.Text = "";
            textBox20.Text = "";
            comboBox4.SelectedIndex = -1;
            Traslado = "";
        }
        private void button18_Click(object sender, EventArgs e)
        {
            if(textBox14.Text != "" | textBox12.Text != "" | textBox11.Text != "" | textBox1.Text != "" | textBox13.Text != "" |
                textBox15.Text != "" | textBox17.Text != "" | textBox10.Text != "" | textBox22.Text != "" |
                textBox16.Text != "" | textBox20.Text != "" | textBox18.Text != "" | comboBox4.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "TRASLADO DE MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    limpiaTraslado();
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox4.Text != textBox17.Text)
            {
                try
                {
                    // competa los datos con lo que hay en sucursal
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT TOTAL_UNIDADES FROM VISTA_MATERIA_PRIMA WHERE SUCURSAL = '{0}' " +
                        "AND ID_MATERIA_PRIMA = '{1}';", comboBox4.Text, textBox12.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        textBox16.Text = reader.GetString(0);
                        
                    }
                    else
                    {
                        textBox16.Text = "0";
                    }
                    textBox20.Focus();
                }
                catch (Exception eX)
                {
                    MessageBox.Show(eX.ToString());
                }
            }
            else
            {
                comboBox4.SelectedIndex = -1;
            }
        }

        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            if(textBox20.Text != "")
            {
                if(textBox16.Text != "")
                {
                    textBox18.Text = Convert.ToString(Convert.ToDouble(textBox16.Text) + Convert.ToDouble(textBox20.Text));
                    textBox22.Text = Convert.ToString(Convert.ToDouble(textBox10.Text) - Convert.ToDouble(textBox20.Text));
                }
            } else
            {
                if (textBox16.Text != "")
                {
                    textBox18.Text = textBox16.Text;
                    textBox22.Text = "";
                } else
                {
                    textBox18.Text = "";
                    textBox22.Text = "";
                }
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            comboBox12.SelectedIndex = -1;
            comboBox13.SelectedIndex = -1;
            cargaTraslados();
        }
        private void cargaTraslados()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView8.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_TRASLADOS_MATERIA ORDER BY FECHA_TRASLADO DESC LIMIT 80;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    while (reader.Read())
                    {
                        dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    }                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void cargaTrasladosEstado(string estado)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView8.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_TRASLADOS_MATERIA WHERE ESTADO_TRASLADO = '{0}' ORDER BY FECHA_TRASLADO DESC LIMIT 80;", estado);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    while (reader.Read())
                    {
                        dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaTrasladosEstado(comboBox13.Text);         
        }
        private void cargaTrasladosSucursal(string sucursalTraslado)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView8.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_TRASLADOS_MATERIA WHERE TRASLADO_DE = '{0}' ORDER BY FECHA_TRASLADO DESC LIMIT 80;", sucursalTraslado);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    while (reader.Read())
                    {
                        dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox12.Text != "")
            {
                cargaTrasladosSucursal(comboBox12.Text);
            }
        }

        private void textBox35_TextChanged(object sender, EventArgs e)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            if (textBox35.Text != "")
            {
                try
                {
                    dataGridView8.Rows.Clear();
                    string sql = string.Format("SELECT * FROM VISTA_TRASLADOS_MATERIA WHERE ID_TRASLADO LIKE '%{0}%' ORDER BY FECHA_TRASLADO DESC;", textBox35.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                       
                        dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                        while (reader.Read())
                        {
                            dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                        }
                    }
                    else
                    {
                        dataGridView8.Rows.Clear();
                        sql = string.Format("SELECT * FROM VISTA_TRASLADOS_MATERIA WHERE FECHA_TRASLADO LIKE '%{0}%' ORDER BY FECHA_TRASLADO DESC;", textBox35.Text);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                            while (reader.Read())
                            {
                                dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                            }
                        }
                        else
                        {
                            dataGridView8.Rows.Clear();
                            sql = string.Format("SELECT * FROM VISTA_TRASLADOS_MATERIA WHERE NOMBRE_USUARIO LIKE '%{0}%' ORDER BY FECHA_TRASLADO DESC;", textBox35.Text);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                                while (reader.Read())
                                {
                                    dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                                }
                            }
                            else
                            {
                                dataGridView8.Rows.Clear();
                                sql = string.Format("SELECT * FROM VISTA_TRASLADOS_MATERIA WHERE TRASLADO_DE LIKE '%{0}%' ORDER BY FECHA_TRASLADO DESC;", textBox35.Text);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                                    while (reader.Read())
                                    {
                                        dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                                    }
                                }
                                else
                                {
                                    dataGridView8.Rows.Clear();
                                    sql = string.Format("SELECT * FROM VISTA_TRASLADOS_MATERIA WHERE TRASLADO_DESTINO LIKE '%{0}%' ORDER BY FECHA_TRASLADO DESC;", textBox35.Text);
                                    cmd = new OdbcCommand(sql, conexion);
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                                        while (reader.Read())
                                        {
                                            dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                                        }
                                    }
                                    else
                                    {
                                        dataGridView8.Rows.Clear();
                                        sql = string.Format("SELECT * FROM VISTA_TRASLADOS_MATERIA WHERE NUMERO_PRODUCTOS LIKE '%{0}%' ORDER BY FECHA_TRASLADO DESC;", textBox35.Text);
                                        cmd = new OdbcCommand(sql, conexion);
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                                            while (reader.Read())
                                            {
                                                dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
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
            }
            else
            {
                button41.PerformClick();
            }
            conexion.Close();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView8.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_TRASLADOS_MATERIA WHERE FECHA_TRASLADO LIKE '%{0}%' ORDER BY FECHA_TRASLADO DESC LIMIT 80;", dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    while (reader.Read())
                    {
                        dataGridView8.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }

        private void textBox35_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (dataGridView8.RowCount > 0)
                {
                    dataGridView8.Focus();
                    if (dataGridView8.RowCount > 1)
                        this.dataGridView8.CurrentCell = this.dataGridView8[1, dataGridView8.CurrentRow.Index + 1];
                }
            }
            else if (e.KeyData == Keys.Enter)
            {
                if (dataGridView8.RowCount > 0)
                {
                    var forma = new frm_detalleTrasladoMateria(dataGridView8.CurrentRow.Cells[0].Value.ToString(), dataGridView8.CurrentRow.Cells[2].Value.ToString(), dataGridView8.CurrentRow.Cells[3].Value.ToString(), dataGridView8.CurrentRow.Cells[4].Value.ToString(), dataGridView8.CurrentRow.Cells[1].Value.ToString());
                    forma.ShowDialog();
                }
            }
        }

        private void dataGridView8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                if (dataGridView8.RowCount > 0)
                {
                    button40.PerformClick();
                }
            }
            else if (e.KeyData == Keys.Enter)
            {
                if (dataGridView8.RowCount > 0)
                {
                    var forma = new frm_detalleTrasladoMateria(dataGridView8.CurrentRow.Cells[0].Value.ToString(), dataGridView8.CurrentRow.Cells[2].Value.ToString(), dataGridView8.CurrentRow.Cells[3].Value.ToString(), dataGridView8.CurrentRow.Cells[4].Value.ToString(), dataGridView8.CurrentRow.Cells[1].Value.ToString());
                    forma.ShowDialog();
                }
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR EL TRASLADO?", "TRASLADO DE MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {
                    string sql = string.Format("UPDATE TRASLADOS_MATERIA_PRIMA SET ESTADO_TUPLA = FALSE WHERE ID_TRASLADO = {0};", dataGridView8.CurrentRow.Cells[0].Value.ToString());
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        var forma = new frm_done();
                        forma.ShowDialog();
                        button41.PerformClick();
                    }
                    else
                    {
                        MessageBox.Show("NO SE PUDO ELIMINAR EL TRASLADO DE LA MERCADERIA!", "TRASLADOS MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                conexion.Close();
            }
        }

        private void dataGridView8_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView8.RowCount > 0)
            {
                var forma = new frm_detalleTrasladoMateria(dataGridView8.CurrentRow.Cells[0].Value.ToString(), dataGridView8.CurrentRow.Cells[2].Value.ToString(), dataGridView8.CurrentRow.Cells[3].Value.ToString(), dataGridView8.CurrentRow.Cells[4].Value.ToString(), dataGridView8.CurrentRow.Cells[1].Value.ToString());
                forma.ShowDialog();
            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (textBox9.Text != "" & textBox6.Text != "" & textBox5.Text != "" & textBox4.Text != "" & textBox2.Text != "" &
                comboBox5.Text != "" & comboBox3.Text != "" & textBox7.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("SE GUARDARAN LOS DATOS PARA CONTINUAR ¿ESTA SEGURO QUE DESEA GUARDAR LOS CAMBIOS?", "GESTION MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    try
                    {
                        string sql = string.Format("UPDATE  MATERIA_PRIMA SET DESCRIPCION = '{0}', " +
                            "ID_PROVEEDOR = (SELECT ID_PROVEEDOR FROM PROVEEDOR WHERE NOMBRE_PROVEEDOR = '{1}'), " +
                            "ID_UNIDAD_MEDIDA = (SELECT ID_UNIDAD_MEDIDA FROM UNIDAD_MEDIDA WHERE NOMBRE = '{2}')" +
                            " WHERE ID_MATERIA_PRIMA = '{3}';",
                            textBox6.Text, comboBox5.Text, comboBox3.Text, textBox9.Text);
                        OdbcCommand cmd = new OdbcCommand(sql, conexion);
                        if ((cmd.ExecuteNonQuery() == 1) | (cmd.ExecuteNonQuery() == 0))
                        {
                            string precio; string cantidad;
                            if (textBox7.Text != "")
                            {
                                precio = textBox7.Text;
                            }
                            else
                            {
                                precio = textBox5.Text;
                            }
                            if (textBox3.Text != "")
                            {
                                cantidad = textBox3.Text;
                            }
                            else
                            {
                                cantidad = textBox4.Text;
                            }
                            if (ActualizaCantidadEnSucursal(precio, cantidad))
                            {
                                var precioM = new frm_PrecioMateriaPrimaS(textBox9.Text, textBox2.Text);
                                if (precioM.ShowDialog() == DialogResult.OK)
                                {
                                    var done = new frm_done();
                                    done.Show();
                                    textBox9.Text = "";
                                    textBox6.Text = "";
                                    textBox5.Text = "";
                                    textBox4.Text = "";
                                    textBox7.Text = "";
                                    textBox3.Text = "";
                                    textBox2.Text = "";
                                    comboBox3.SelectedIndex = -1;
                                    comboBox5.SelectedIndex = -1;
                                    pictureBox1.Image = null;
                                    cargaMateriaPrima(nombreSucursal);
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
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            var xpose = new frm_TrasladoLotesMateria(usuario, nombreSucursal);
            xpose.Show();

        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 's') || (e.KeyChar == 'S'))
            {
                if (textBox5.Text != "")
                {
                    textBox7.Text = textBox5.Text;
                    textBox3.Focus();
                }
            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                textBox3.Focus();
            }
        }
    }
}
