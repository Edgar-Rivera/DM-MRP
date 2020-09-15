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
    public partial class frm_buscaMateriaPrimaS : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string codigo;
        string sucursal;
        int clasificacion;
        public frm_buscaMateriaPrimaS(string sucursalActual, string rol, bool temp, int tipo)
        {
            InitializeComponent();
            cargaSucursales();
            if (sucursalActual != "")
            {
                comboBox2.Text = sucursalActual;
            } else
            {
                comboBox2.SelectedIndex = 0;
            }
            clasificacion = tipo;
            if (clasificacion > 2)
            {
                cargaMercaderia();
            } else
            {
                cargaMercaderiaClasificacion();
            }
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
        internal frm_comprasM.MateriaPrima currentMateria
        {
            get
            {
                return new frm_comprasM.MateriaPrima() 
                {
                    getCodigo = codigo,
                    getSucursal = sucursal
                };
            }
            set
            {
                currentMateria.getCodigo = codigo;
                currentMateria.getSucursal = sucursal;
            }
        }

        private void cargaMercaderiaClasificacion()
        {
            try
            {
                dataGridView1.Rows.Clear();
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE SUCURSAL = '{0}' AND ID_CLASIFICACION = {1};", comboBox2.Text, clasificacion);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                        //styleDV(this.dataGridView1);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void cargaMercaderia()
        {
            try
            {
                dataGridView1.Rows.Clear();
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE SUCURSAL = '{0}';", comboBox2.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
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
        private void frm_buscaMateriaPrima_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frm_buscaMateriaPrima_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_buscaMateriaPrima_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_buscaMateriaPrima_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
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
            if (clasificacion > 2)
            {
                cargaMercaderia();
            }
            else
            {
                cargaMercaderiaClasificacion();
            }
        }
        private void buscaMercaderia()
        {
            if (textBox1.Text != "")
            {
                try
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE SUCURSAL = '{0}' AND ID_MERCADERIA LIKE '%{1}%';", comboBox2.Text, textBox1.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                            //styleDV(this.dataGridView1);
                        }
                    }
                    else
                    {
                        sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE SUCURSAL = '{0}' AND DESCRIPCION LIKE '%{1}%';", comboBox2.Text, textBox1.Text);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Clear();
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                                //styleDV(this.dataGridView1);
                            }
                        }
                        else
                        {

                            sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE SUCURSAL = '{0}' AND PRECIO LIKE '%{1}%';", comboBox2.Text, textBox1.Text);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                                    //styleDV(this.dataGridView1);
                                }
                            }
                            else
                            {
                                sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE SUCURSAL = '{0}' AND TOTAL_UNIDADES LIKE '%{1}%';", comboBox2.Text, textBox1.Text);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                                        //styleDV(this.dataGridView1);
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
        private void buscaMercaderiaClasificacion()
        {
            if (textBox1.Text != "")
            {
                try
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE SUCURSAL = '{0}' AND ID_MERCADERIA LIKE '%{1}%' AND ID_CLASIFICACION = {2};", comboBox2.Text, textBox1.Text, clasificacion);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                            //styleDV(this.dataGridView1);
                        }
                    }
                    else
                    {
                        sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE SUCURSAL = '{0}' AND DESCRIPCION LIKE '%{1}%' AND ID_CLASIFICACION = {2};", comboBox2.Text, textBox1.Text, clasificacion);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Clear();
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                                //styleDV(this.dataGridView1);
                            }
                        }
                        else
                        {

                            sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE SUCURSAL = '{0}' AND PRECIO LIKE '%{1}%' AND ID_CLASIFICACION = {2};", comboBox2.Text, textBox1.Text, clasificacion);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                                    //styleDV(this.dataGridView1);
                                }
                            }
                            else
                            {
                                sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE SUCURSAL = '{0}' AND TOTAL_UNIDADES LIKE '%{1}%' AND ID_CLASIFICACION = {2};", comboBox2.Text, textBox1.Text, clasificacion);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(2)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(3)), reader.GetString(4));
                                        //styleDV(this.dataGridView1);
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
                cargaMercaderiaClasificacion();
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dataGridView1.RowCount > 0)
            {
                codigo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sucursal = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                DialogResult = DialogResult.OK;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dataGridView1.RowCount > 0)
                {
                    codigo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    sucursal = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    DialogResult = DialogResult.OK;
                }
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
            else if (e.KeyData == Keys.Enter)
            {
                if (dataGridView1.RowCount > 0)
                {
                    codigo = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    sucursal = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void frm_buscaMateriaPrima_Load(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (clasificacion > 2)
            {
                cargaMercaderia();
            } else
            {
                cargaMercaderiaClasificacion();
            }
        }
    }
}
