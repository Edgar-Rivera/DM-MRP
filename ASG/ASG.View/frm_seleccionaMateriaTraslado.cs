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
    public partial class frm_seleccionaMateriaTraslado : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        List<string> listaProductos = new List<string>();
        public frm_seleccionaMateriaTraslado(string sucursalActual, string rol, bool temp)
        {
            InitializeComponent();
            cargaSucursales();
            comboBox2.Text = sucursalActual;
            cargaMercaderia();
            if (rol != "ADMINISTRADOR")
            {
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[0].Width = 200;
                dataGridView1.Columns[1].Width = 300;
                dataGridView1.Columns[4].Width = 200;
                this.dataGridView1.Size = new Size(660, 331);
                this.dataGridView1.Location = new Point(45, 135);
            }
            comboBox2.Enabled = temp;
        }
        private void cargaSucursales()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT NOMBRE_SUCURSAL FROM SUCURSAL WHERE  ESTADO_TUPLA = TRUE;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
        }
        private void cargaMercaderia()
        {
            try
            {
                dataGridView1.Rows.Clear();
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA WHERE SUCURSAL = '{0}';", comboBox2.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                        //styleDV(this.dataGridView1);
                    }
                    funcionSleeccionados();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void frm_seleccionaMateriaTraslado_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[6].ReadOnly = false;
        }

        private void frm_seleccionaMateriaTraslado_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_seleccionaMateriaTraslado_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_seleccionaMateriaTraslado_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
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
            else if (e.KeyData == Keys.Enter)
            {
                if (dataGridView1.RowCount > 0)
                {
                    e.SuppressKeyPress = true;
                    if (dataGridView1.CurrentRow.Cells[6].Value != null)
                    {
                        dataGridView1.CurrentRow.Cells[6].Value = null;
                        listaProductos.Remove(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                        //MessageBox.Show("esta seleccionado y cambio a no seleccionarse");
                    }
                    else
                    {
                        //MessageBox.Show("se seleeccino el producto");
                        dataGridView1.CurrentRow.Cells[6].Value = true;
                        listaProductos.Add(dataGridView1.CurrentRow.Cells[0].Value.ToString());

                    }
                }
            }
        }
        private void funcionSleeccionados()
        {
            if (listaProductos != null)
            {
                if (dataGridView1.RowCount > 0)
                {
                    foreach (var codigo in listaProductos)
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value.ToString() == codigo)
                            {
                                dataGridView1.Rows[i].Cells[6].Value = true;
                            }
                        }
                    }
                }
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            cargaMercaderia();
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Red;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.BackColor = Color.Transparent;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                button4.PerformClick();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[6].Value = true;
                    listaProductos.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }
            }
            else
            {
                button4.PerformClick();
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[6].Value = null;
                    listaProductos.Remove(dataGridView1.Rows[i].Cells[0].Value.ToString());
                }
            }
        }

        private void frm_seleccionaMateriaTraslado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if (listaProductos == null)
                {
                    this.Close();
                }
                else
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR?", "COMBO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.S))
            {
                button3.PerformClick();
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                if (e.ColumnIndex == 6)
                {
                    SendKeys.Send("{ENTER}");
                }
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dataGridView1.RowCount > 0)
                {
                    e.SuppressKeyPress = true;
                    if (dataGridView1.CurrentRow.Cells[6].Value != null)
                    {
                        dataGridView1.CurrentRow.Cells[6].Value = null;
                        listaProductos.Remove(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        textBox1.Focus();
                        //MessageBox.Show("esta seleccionado y cambio a no seleccionarse");
                    }
                    else
                    {
                        //MessageBox.Show("se seleeccino el producto");
                        dataGridView1.CurrentRow.Cells[6].Value = true;
                        listaProductos.Add(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        textBox1.Focus();
                    }
                }
            }
        }
        private bool productoSeleccionado()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[6].Value != null)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[6].Value.ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // validacion de que haya seleccionado algun producto
            if (productoSeleccionado())
            {
                /* for (int i = 0; i < dataGridView1.RowCount; i++)
                 {
                     if (dataGridView1.Rows[i].Cells[5].Value != null)
                     {
                         if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[5].Value.ToString()))
                         {
                             listaProductos.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                         }

                     }
                 }*/
                if (listaProductos != null)
                {
                    DialogResult = DialogResult.OK;
                }

            }
            else
            {
                MessageBox.Show("NO HA SELECCIONADO NINGUN PRODUCTO A TRASLADAR!", "TRASLADOS MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        internal frm_TrasladoLotesMateria.CargaProductos currenProductos
        {
            get
            {
                return new frm_TrasladoLotesMateria.CargaProductos()
                {
                    listaCodigo = listaProductos
                };
            }
            set
            {
                currenProductos.listaCodigo = listaProductos;
            }
        }
        internal class CargaProductos
        {
            public List<string> listaCodigo { get; set; }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                try
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA WHERE SUCURSAL = '{0}' AND ID_MATERIA_PRIMA LIKE '%{1}%';", comboBox2.Text, textBox1.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                            //styleDV(this.dataGridView1);
                        }
                        funcionSleeccionados();
                    }
                    else
                    {
                        sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA WHERE SUCURSAL = '{0}' AND DESCRIPCION LIKE '%{1}%';", comboBox2.Text, textBox1.Text);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Clear();
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                                //styleDV(this.dataGridView1);
                            }
                            funcionSleeccionados();
                        }
                        else
                        {
                            sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA WHERE SUCURSAL = '{0}' AND PROVEEDOR LIKE '%{1}%';", comboBox2.Text, textBox1.Text);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                                    //styleDV(this.dataGridView1);
                                }
                                funcionSleeccionados();
                            }
                            else
                            {
                                sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA WHERE SUCURSAL = '{0}' AND PRECIO LIKE '%{1}%';", comboBox2.Text, textBox1.Text);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                                        //styleDV(this.dataGridView1);
                                    }
                                    funcionSleeccionados();
                                }
                                else
                                {
                                    sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA WHERE SUCURSAL = '{0}' AND TOTAL_UNIDADES LIKE '%{1}%';", comboBox2.Text, textBox1.Text);
                                    cmd = new OdbcCommand(sql, conexion);
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        dataGridView1.Rows.Clear();
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                                        while (reader.Read())
                                        {
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                                            //styleDV(this.dataGridView1);
                                        }
                                        funcionSleeccionados();
                                    }
                                    else
                                    {
                                        sql = string.Format("SELECT * FROM VISTA_MATERIA_PRIMA WHERE SUCURSAL = '{0}' AND MEDIDA LIKE '%{1}%';", comboBox2.Text, textBox1.Text);
                                        cmd = new OdbcCommand(sql, conexion);
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            dataGridView1.Rows.Clear();
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                                            while (reader.Read())
                                            {
                                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(3)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(4)), reader.GetString(5));
                                                //styleDV(this.dataGridView1);
                                            }
                                            funcionSleeccionados();
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
                cargaMercaderia();
            }
        }
    }
}
