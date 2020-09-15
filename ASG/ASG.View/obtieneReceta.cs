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
    public partial class obtieneReceta : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string codigoRecetaObtenido;
        string mercaderiaS;
        public obtieneReceta(string mercaderia)
        {
            InitializeComponent();
            mercaderiaS = mercaderia;
            cargaDatos(mercaderia);
        }
        private void cargaDatos(string mercaderia)
        {

            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_RECETAS WHERE ID_MERCADERIA = '{0}';", mercaderia);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "ACTIVA");
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "ACTIVA");
                        //styleDV(this.dataGridView1);
                    }
                }
                else
                {
                   // MessageBox.Show("NO EXISTEN CLIENTES!", "GESTION CLIENTES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Text = "";
                    textBox1.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION RECETAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void obtieneReceta_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void obtieneReceta_MouseDown(object sender, MouseEventArgs e)
        {

            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void obtieneReceta_MouseMove(object sender, MouseEventArgs e)
        {

            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void obtieneReceta_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }
        internal ordenProduccion.Receta CurrentReceta
        {
            get
            {
                return new ordenProduccion.Receta()
                {
                    codigoReceta = codigoRecetaObtenido
                };
            }
            set
            {
                CurrentReceta.codigoReceta = codigoRecetaObtenido;
            }
        }
        private void obtieneReceta_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = false;
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
                if (dataGridView1.RowCount > 0)
                {
                    codigoRecetaObtenido = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.RowCount > 0)
            {
                codigoRecetaObtenido = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                DialogResult = DialogResult.OK;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (dataGridView1.RowCount > 0)
                {
                    e.SuppressKeyPress = true;
                    codigoRecetaObtenido = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           if(textBox1.Text != null)
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {
                    string sql = string.Format("SELECT * FROM VISTA_RECETAS WHERE ID_MERCADERIA = '{0}' AND ID_RECETA LIKE '%{1}%';", mercaderiaS, textBox1.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "ACTIVA");
                        while (reader.Read())
                        {
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "ACTIVA");
                            //styleDV(this.dataGridView1);
                        }
                    }
                    else
                    {
                        sql = string.Format("SELECT * FROM VISTA_RECETAS WHERE ID_MERCADERIA = '{0}' AND ID_MERCADERIA LIKE '%{1}%';", mercaderiaS, textBox1.Text);
                        cmd = new OdbcCommand(sql, conexion);
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            dataGridView1.Rows.Clear();
                            dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "ACTIVA");
                            while (reader.Read())
                            {
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "ACTIVA");
                                //styleDV(this.dataGridView1);
                            }
                        }
                        else
                        {
                            sql = string.Format("SELECT * FROM VISTA_RECETAS WHERE ID_MERCADERIA = '{0}' AND DESCRIPCION LIKE '%{1}%';", mercaderiaS, textBox1.Text);
                            cmd = new OdbcCommand(sql, conexion);
                            reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "ACTIVA");
                                while (reader.Read())
                                {
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "ACTIVA");
                                    //styleDV(this.dataGridView1);
                                }
                            }
                            else
                            {
                                sql = string.Format("SELECT * FROM VISTA_RECETAS WHERE ID_MERCADERIA = '{0}' AND FECHA_CREACION LIKE '%{1}%';", mercaderiaS, textBox1.Text);
                                cmd = new OdbcCommand(sql, conexion);
                                reader = cmd.ExecuteReader();
                                if (reader.Read())
                                {
                                    dataGridView1.Rows.Clear();
                                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "ACTIVA");
                                    while (reader.Read())
                                    {
                                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), "ACTIVA");
                                        //styleDV(this.dataGridView1);
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
            } else
            {
                cargaDatos(mercaderiaS);
            }
        }
    }
}
