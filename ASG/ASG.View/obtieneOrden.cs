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
    public partial class obtieneOrden : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string orden;
        string mercaderia;
        string receta;
        string descripcion;
        string cantidad;
        string estado;
        public obtieneOrden(string status)
        {
            InitializeComponent();
            estado = status;
            cargaOrdenes();

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cargaOrdenes()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView4.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE ESTADO = '{0}' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", estado);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                    while (reader.Read())
                    {
                        dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            conexion.Close();
        }
        private void obtieneOrden_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void obtieneOrden_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void obtieneOrden_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void obtieneOrden_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {

                    string sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE ID_ORDEN_PRODUCCION LIKE '%{0}%' AND ESTADO = '{1}' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim(), estado);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView4.Rows.Clear();
                        dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                        while (reader.Read())
                        {
                            dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                        }
                    }
                    else
                    {
                        sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE USUARIO LIKE '%{0}%' AND ESTADO = '{1}' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim(), estado);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView4.Rows.Clear();
                            dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                            while (reader.Read())
                            {
                                dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                            }
                        }
                        else
                        {
                            sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE ID_MERCADERIA LIKE '%{0}%' AND ESTADO = '{1}' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim(), estado);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView4.Rows.Clear();
                                dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                while (reader.Read())
                                {
                                    dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                }
                            }
                            else
                            {
                                sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE MERCADERIA LIKE '%{0}%' AND ESTADO = '{1}' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim(), estado);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView4.Rows.Clear();
                                    dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                    while (reader.Read())
                                    {
                                        dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                    }
                                }
                                else
                                {
                                    sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE FECHA_INICIO LIKE '%{0}%' AND ESTADO = '{1}' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim(), estado);
                                    cmd = new OdbcCommand(sql, conexion);
                                    reader = cmd.ExecuteReader();
                                    if (reader.Read())
                                    {
                                        dataGridView4.Rows.Clear();
                                        dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                        while (reader.Read())
                                        {
                                            dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                        }
                                    }
                                    else
                                    {
                                        sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE FECHA_FIN LIKE '%{0}%' AND ESTADO = '{1}' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim(), estado);
                                        cmd = new OdbcCommand(sql, conexion);
                                        reader = cmd.ExecuteReader();
                                        if (reader.Read())
                                        {
                                            dataGridView4.Rows.Clear();
                                            dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                            while (reader.Read())
                                            {
                                                dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                            }
                                        }
                                        else
                                        {
                                            sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE CANTIDAD_IN LIKE '%{0}%' AND ESTADO = '{1}' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim(), estado);
                                            cmd = new OdbcCommand(sql, conexion);
                                            reader = cmd.ExecuteReader();
                                            if (reader.Read())
                                            {
                                                dataGridView4.Rows.Clear();
                                                dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                                while (reader.Read())
                                                {
                                                    dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                                }
                                            }
                                            else
                                            {
                                                sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE CANTIDAD_OUT LIKE '%{0}%' AND ESTADO = '{1}' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim(), estado);
                                                cmd = new OdbcCommand(sql, conexion);
                                                reader = cmd.ExecuteReader();
                                                if (reader.Read())
                                                {
                                                    dataGridView4.Rows.Clear();
                                                    dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                                    while (reader.Read())
                                                    {
                                                        dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                                    }
                                                }
                                                else
                                                {
                                                    sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE ESTADO LIKE '%{0}%' AND ESTADO = '{1}' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim(),estado);
                                                    cmd = new OdbcCommand(sql, conexion);
                                                    reader = cmd.ExecuteReader();
                                                    if (reader.Read())
                                                    {
                                                        dataGridView4.Rows.Clear();
                                                        dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                                        while (reader.Read())
                                                        {
                                                            dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception eX)
                {
                    MessageBox.Show(eX.ToString());
                }
                conexion.Close();
            }
            else
            {
                cargaOrdenes();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            cargaOrdenes();
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                if (dataGridView4.RowCount > 0)
                {
                    dataGridView4.Focus();
                    if (dataGridView4.RowCount > 1)
                        this.dataGridView4.CurrentCell = this.dataGridView4[1, dataGridView4.CurrentRow.Index + 1];
                }
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (dataGridView4.RowCount > 0)
                {
                    orden = dataGridView4.CurrentRow.Cells[0].Value.ToString();
                    mercaderia = dataGridView4.CurrentRow.Cells[2].Value.ToString();
                    receta = dataGridView4.CurrentRow.Cells[9].Value.ToString();
                    cantidad = dataGridView4.CurrentRow.Cells[6].Value.ToString();
                    descripcion = dataGridView4.CurrentRow.Cells[3].Value.ToString();
                    DialogResult = DialogResult.OK;
                }
            }
        }
        internal produccion.Orden CurrentOrden
        {
            get
            {
                return new produccion.Orden()
                {
                    getOrden = orden,
                    getMercaderia = mercaderia,
                    getReceta = receta,
                    getDescripcion = descripcion,
                    getCantidad = cantidad

                };
            }
            set
            {
                CurrentOrden.getOrden = orden;
                CurrentOrden.getMercaderia = mercaderia;
                CurrentOrden.getReceta = receta;
                CurrentOrden.getCantidad = cantidad;
                CurrentOrden.getDescripcion = descripcion;
            }
        }
        private void dataGridView4_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView4.RowCount > 0)
            {
                orden = dataGridView4.CurrentRow.Cells[0].Value.ToString();
                mercaderia = dataGridView4.CurrentRow.Cells[2].Value.ToString();
                receta = dataGridView4.CurrentRow.Cells[9].Value.ToString();
                cantidad = dataGridView4.CurrentRow.Cells[6].Value.ToString();
                descripcion = dataGridView4.CurrentRow.Cells[3].Value.ToString();
                DialogResult = DialogResult.OK;
            }
        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (dataGridView4.RowCount > 0)
                {
                    orden = dataGridView4.CurrentRow.Cells[0].Value.ToString();
                    mercaderia = dataGridView4.CurrentRow.Cells[2].Value.ToString();
                    receta = dataGridView4.CurrentRow.Cells[9].Value.ToString();
                    cantidad = dataGridView4.CurrentRow.Cells[6].Value.ToString();
                    descripcion = dataGridView4.CurrentRow.Cells[3].Value.ToString();
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
