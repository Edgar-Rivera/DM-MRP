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
    public partial class frm_seleccionaProductosdTraslado : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string sucursalDestino;
        int clasificacion;
        List<string> listaProductos = new List<string>();
        public frm_seleccionaProductosdTraslado(int x, string sucursal, string usuarioRol)
        {
            InitializeComponent();
            textBox1.Focus();
            sucursalDestino = sucursal;
            label2.Text = label2.Text + " " + sucursalDestino;
            clasificacion = x;
            if (x == 1)
            {
                cargaMercaderia(x);
            }
            else if (x == 3)
            {
                cargaMercaderiatotal();
            }
            else if (x == 2)
            {
                cargaMercaderia(x);
            }
            if (usuarioRol != "ADMINISTRADOR")
            {
                dataGridView1.Columns[2].Visible = false;
            }
        }
        internal frm_trasladoLotes.CargaProductos currenProductos
        {
            get
            {
                return new frm_trasladoLotes.CargaProductos()
                {
                    listaCodigo = listaProductos
                };
            }
            set
            {
                currenProductos.listaCodigo = listaProductos;
            }
        }
        private void frm_seleccionaProductosdTraslado_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_seleccionaProductosdTraslado_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_seleccionaProductosdTraslado_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            if (clasificacion <= 2)
            {
                cargaMercaderia(clasificacion);
            }
            else
            {
                cargaMercaderiatotal();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (clasificacion <= 2)
            {
                try
                {
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE ID_MERCADERIA LIKE '%{0}%' AND ID_CLASIFICACION = {1} AND NOMBRE_SUCURSAL = '{2}';", textBox1.Text, clasificacion, sucursalDestino);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                            //styleDV(this.dataGridView1);
                        }
                    }
                    else
                    {
                        conexion.Close();
                        conexion = ASG_DB.connectionResult();
                        sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE DESCRIPCION LIKE '%{0}%' AND ID_CLASIFICACION = {1} AND NOMBRE_SUCURSAL = '{2}';", textBox1.Text, clasificacion, sucursalDestino);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Clear();
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                //styleDV(this.dataGridView1);
                            }
                        }
                        else
                        {
                            conexion.Close();
                            conexion = ASG_DB.connectionResult();
                            sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE NOMBRE_PROVEEDOR LIKE '%{0}%' AND ID_CLASIFICACION = {1} AND NOMBRE_SUCURSAL = '{2}';", textBox1.Text, clasificacion, sucursalDestino);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                    //styleDV(this.dataGridView1);
                                }
                            }
                            else
                            {
                                conexion.Close();
                                conexion = ASG_DB.connectionResult();
                                sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE PRECIO_COMPRA LIKE '%{0}%' AND ID_CLASIFICACION = {1} AND NOMBRE_SUCURSAL = '{2}';", textBox1.Text, clasificacion, sucursalDestino);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                        //styleDV(this.dataGridView1);
                                    }
                                }
                                else
                                {
                                    conexion.Close();
                                    conexion = ASG_DB.connectionResult();
                                    sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE TOTAL_UNIDADES LIKE '%{0}%' AND ID_CLASIFICACION = {1} AND NOMBRE_SUCURSAL = '{2}';", textBox1.Text, clasificacion, sucursalDestino);
                                    cmd = new OdbcCommand(sql, conexion);
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        dataGridView1.Rows.Clear();
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                        while (reader.Read())
                                        {
                                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                            //styleDV(this.dataGridView1);
                                        }
                                    }
                                    else
                                    {
                                        cargaMercaderia(clasificacion);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception es)
                {
                    MessageBox.Show(es.ToString());
                }
            }
            else
            {
                buscaMercaderia();
            }
        }
        private void buscaMercaderia()
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE ID_MERCADERIA LIKE '%{0}%' AND NOMBRE_SUCURSAL = '{1}';", textBox1.Text, sucursalDestino);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                        //styleDV(this.dataGridView1);
                    }
                }
                else
                {
                    conexion.Close();
                    conexion = ASG_DB.connectionResult();
                    sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE DESCRIPCION LIKE '%{0}%'  AND NOMBRE_SUCURSAL = '{1}';", textBox1.Text, sucursalDestino);
                    cmd = new OdbcCommand(sql, conexion);
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                            //styleDV(this.dataGridView1);
                        }
                    }
                    else
                    {
                        conexion.Close();
                        conexion = ASG_DB.connectionResult();
                        sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE NOMBRE_PROVEEDOR LIKE '%{0}%' AND NOMBRE_SUCURSAL = '{1}';", textBox1.Text, sucursalDestino);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Clear();
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                //styleDV(this.dataGridView1);
                            }
                        }
                        else
                        {
                            conexion.Close();
                            conexion = ASG_DB.connectionResult();
                            sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE PRECIO_COMPRA LIKE '%{0}%'  AND NOMBRE_SUCURSAL = '{1}';", textBox1.Text, sucursalDestino);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                    //styleDV(this.dataGridView1);
                                }
                            }
                            else
                            {
                                conexion.Close();
                                conexion = ASG_DB.connectionResult();
                                sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE TOTAL_UNIDADES LIKE '%{0}%' AND NOMBRE_SUCURSAL = '{1}';", textBox1.Text, sucursalDestino);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                                        ////styleDV(this.dataGridView1);
                                    }
                                }
                                else
                                {
                                    cargaMercaderiatotal();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception es)
            {
                MessageBox.Show(es.ToString());
            }
        }
        private void cargaMercaderia(int x)
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE ID_CLASIFICACION = {0} AND ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{1}' LIMIT 1);", x, sucursalDestino);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                        //styleDV(this.dataGridView1);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void cargaMercaderiatotal()
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT * FROM VISTA_MERCADERIA WHERE ID_SUCURSAL = (SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{0}' LIMIT 1);", sucursalDestino);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(2), reader.GetString(1), string.Format("Q.{0:###,###,###,##0.00##}", reader.GetDouble(4)), string.Format("{0:#,###,###,###,###}", reader.GetDouble(5)));
                        //styleDV(this.dataGridView1);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void frm_seleccionaProductosdTraslado_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[5].ReadOnly = false;
        }

        private void frm_seleccionaProductosdTraslado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private bool productoSeleccionado()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[5].Value != null)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[5].Value.ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // validacion de que haya seleccionado algun producto
            if (productoSeleccionado())
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells[5].Value != null)
                    {
                        if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[5].Value.ToString()))
                        {
                            listaProductos.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        }
                        if (listaProductos != null)
                        {
                            DialogResult = DialogResult.OK;
                        }
                    }
                }

            } else
            {
                MessageBox.Show("NO HA SELECCIONADO NINGUN PRODUCTO A TRASLADAR!", "TRASLADOS MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked == true)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[5].Value = true;
                }
            } else
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells[5].Value = null;
                }
            }
        }
    }
}
