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
    public partial class ordenProduccion : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        string nameUs;
        string rolUs;
        string usuario;
        string idSucursal;
        string nombreSucursal;
        string receta;
        public ordenProduccion(string nameUser, string rolUser, string user, string sucursal,string nombre)
        {
            InitializeComponent();
            usuario = user;
            nameUs = nameUser;
            rolUs = rolUser;
            idSucursal = sucursal;
            nombreSucursal = nombre;
            label19.Text = label19.Text + "" + nameUs;
            cargaOrdenes();
        }

        private void ordenProduccion_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void ordenProduccion_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void ordenProduccion_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void ordenProduccion_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ordenProduccion_Load(object sender, EventArgs e)
        {
            dataGridView3.Columns[0].ReadOnly = true;
            dataGridView3.Columns[1].ReadOnly = true;
            dataGridView3.Columns[2].ReadOnly = false;
        }
        private void getdataEdit(string mercaderia, string nombreSucursal)
        {
            if (textBox5.Text != "")
            {
                try
                {
                   
                    OdbcConnection conexion = ASG_DB.connectionResult();
                    string sql = string.Format("SELECT * FROM VISTA_MERCADERIA_EDICION WHERE ID_MERCADERIA = '{0}'  AND NOMBRE_SUCURSAL = '{1}';", mercaderia, nombreSucursal);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                      
                       
                        textBox16.Text = reader.GetString(1);          
                        textBox4.Text = "" + reader.GetDouble(3);

                     
                    }
                    else
                    {
                        textBox16.Text = "";
                        textBox4.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                textBox16.Text = "";
                textBox4.Text = "";
            }
        }
        internal class Receta
        {
            public string codigoReceta { get; set; }

        }
        private void button9_Click(object sender, EventArgs e)
        {
            var forma = new frm_edicionProducto(nombreSucursal, rolUs);
            if (forma.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = forma.CurrentMercaderia.getMercaderia;
                string temp = forma.CurrentSucursal.getSucursal;
                if(textBox5.Text != "")
                {
                    getdataEdit(textBox5.Text, temp);
                    cargaAreas();
                    textBox3.Text = dateTimePicker1.Value.ToShortDateString();
                    textBox6.Focus();
                }

            }
        }
        private void cargaAreas()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT id_area, nombre_area FROM area_produccion WHERE estado_tupla = true;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView3.Rows.Clear();
                    dataGridView3.Rows.Add(reader.GetString(0), reader.GetString(1), false);
                    while (reader.Read())
                    {
                        dataGridView3.Rows.Add(reader.GetString(0), reader.GetString(1), false);
                        //styleDV(this.dataGridView1);
                    }
                }
                else
                {
                    // MessageBox.Show("NO EXISTEN CLIENTES!", "GESTION CLIENTES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                  
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION RECETAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
        }
        private void cargaDetalleReceta(string receta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_DETALLE_RECETA WHERE ID_RECETA = {0};",receta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView2.Rows.Clear();
                    dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),' ',reader.GetString(4));
                    while (reader.Read())
                    {
                        dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), ' ', reader.GetString(4));
                        //styleDV(this.dataGridView1);
                    }
                }
                else
                {
                    // MessageBox.Show("NO EXISTEN CLIENTES!", "GESTION CLIENTES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION RECETAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();

        }
        private void button15_Click(object sender, EventArgs e)
        {
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            textBox3.Text = dateTimePicker1.Value.ToShortDateString();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
            if (textBox5.Text != "")
            {
                if (textBox6.Text != "")
                {
                    if (dataGridView2.RowCount > 0)
                    {
                        for (int i = 0; i < dataGridView2.RowCount; i++)
                        {
                            dataGridView2.Rows[i].Cells[4].Value = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value.ToString()) * Convert.ToDouble(textBox6.Text);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No ha seleccionado una receta valida!");
                    }
                }
                else
                {
                    MessageBox.Show("No ha ingresado la cantidad a producir");
                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun producto!");
            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {

            if (textBox5.Text != null)
            {
                var forma = new obtieneReceta(textBox5.Text);
                if (forma.ShowDialog() == DialogResult.OK)
                {
                    receta = forma.CurrentReceta.codigoReceta;
                    cargaDetalleReceta(forma.CurrentReceta.codigoReceta);
                    if (textBox6.Text != "")
                    {
                        if (dataGridView2.RowCount > 0)
                        {
                            for (int i = 0; i < dataGridView2.RowCount; i++)
                            {
                                dataGridView2.Rows[i].Cells[4].Value = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value.ToString()) * Convert.ToDouble(textBox6.Text);
                            }
                        }
                        else
                        {
                            //MessageBox.Show("No ha seleccionado una receta valida!");
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("No ha seleccionado ningun producto!");
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView3.RowCount > 0)
            {
                for(int i = 0; i < dataGridView3.RowCount; i++)
                {
                    dataGridView3.Rows[i].Cells[2].Value = true;
                }
            } else
            {
                MessageBox.Show("No existen areas de produccion, asegurese que exista al menos un area de produccion!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox5.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR, LOS DATOS DE LA ORDEN SE PERDERAN?", "ORDEN DE PRODUCCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    limpiaOrden();
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if(dataGridView2.RowCount > 0)
            {
                var forma = new disponibilidadBodegas(receta);
                forma.Show();
            }
            else
            {
                MessageBox.Show("No ha seleccionado una receta valida!");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
          
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            textBox5.Text = "";
            textBox16.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
        }
        private bool areaSeleccionada()
        {
            if(dataGridView3.RowCount > 0)
            {
                for(int i = 0; i < dataGridView3.RowCount; i++)
                {
                    if (dataGridView3.Rows[i].Cells[2].Value != null)
                    {
                        if (Convert.ToBoolean(dataGridView3.Rows[i].Cells[2].Value.ToString()))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        private bool guardaProcesos(string area, string orden)
        {

            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("INSERT INTO PROCESO_PRODUCCION VALUES ({0}, {1},1,null,null, TRUE);", area, orden);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }
        private string getCodeGenerate()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT MAX(ID_ORDEN_PRODUCCION) FROM ORDENES_PRODUCCION;");
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return "" + reader.GetInt32(0);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION RECETAS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            conexion.Close();
            return null;
        }
        private bool ingresaOrden()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("INSERT INTO ORDENES_PRODUCCION VALUES (null,'{0}','{1}','{2}',null,{3},{4},0, TRUE,{5});",
                     usuario, textBox5.Text, dateTimePicker1.Value.ToString("yyyy-MM-dd"), comboBox1.SelectedIndex+1, textBox6.Text,receta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    string tempX = getCodeGenerate();
                    if (tempX != null)
                    {
                        for(int i = 0; i<dataGridView3.RowCount; i++)
                        {
                            if (dataGridView3.Rows[i].Cells[2].Value != null)
                            {
                                if (Convert.ToBoolean(dataGridView3.Rows[i].Cells[2].Value.ToString()))
                                {
                                    guardaProcesos(dataGridView3.Rows[i].Cells[0].Value.ToString(), tempX);
                                }
                            }
                            
                        }
                        return true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return false;
        }
        private void limpiaOrden()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            textBox5.Text = "";
            textBox16.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if ((dataGridView2.RowCount > 0) && (dataGridView3.RowCount > 0) && textBox5.Text != "" && textBox16.Text != ""
                && textBox4.Text != "" && textBox6.Text != "" && textBox3.Text != "" && comboBox1.SelectedIndex != -1 && areaSeleccionada())
            {
                if (ingresaOrden())
                {
                    var forma = new frm_done();
                    forma.ShowDialog();
                    limpiaOrden();
                }
                else
                {
                    MessageBox.Show("La orden no fue creada exitosamente!");
                }
            } else
            {
                MessageBox.Show("Debe ingresar los datos requeridos!");
            }
        }
        private void cargaOrdenes()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView4.Rows.Clear();
                string sql = string.Format("SELECT * FROM VISTA_ORDENES ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;");
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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if(textBox7.Text != "")
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {
                   
                    string sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE ID_ORDEN_PRODUCCION LIKE '%{0}%' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim());
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
                    } else
                    {
                        sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE USUARIO LIKE '%{0}%' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim());
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
                            sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE ID_MERCADERIA LIKE '%{0}%' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim());
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
                                sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE MERCADERIA LIKE '%{0}%' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim());
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
                                    sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE FECHA_INICIO LIKE '%{0}%' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim());
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
                                        sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE FECHA_FIN LIKE '%{0}%' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim());
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
                                            sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE CANTIDAD_IN LIKE '%{0}%' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim());
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
                                                sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE CANTIDAD_OUT LIKE '%{0}%' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim());
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
                                                    sql = string.Format("SELECT * FROM VISTA_ORDENES WHERE ESTADO LIKE '%{0}%' ORDER BY ID_ORDEN_PRODUCCION DESC LIMIT 300;", textBox7.Text.Trim());
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
            } else 
            {
                cargaOrdenes();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            cargaOrdenes();
        }

        private void dataGridView4_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dataGridView4.RowCount > 0)
            {
                var forma = new vistaOrden(dataGridView4.CurrentRow.Cells[0].Value.ToString(), dataGridView4.CurrentRow.Cells[1].Value.ToString(),
                    dataGridView4.CurrentRow.Cells[2].Value.ToString(), dataGridView4.CurrentRow.Cells[8].Value.ToString(),
                    dataGridView4.CurrentRow.Cells[4].Value.ToString(), dataGridView4.CurrentRow.Cells[5].Value.ToString(),
                    dataGridView4.CurrentRow.Cells[3].Value.ToString(), dataGridView4.CurrentRow.Cells[6].Value.ToString(),
                    dataGridView4.CurrentRow.Cells[9].Value.ToString());
                forma.Show();
            }
        }

        private void dataGridView4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                if (dataGridView4.RowCount > 0)
                {
                    var forma = new vistaOrden(dataGridView4.CurrentRow.Cells[0].Value.ToString(), dataGridView4.CurrentRow.Cells[1].Value.ToString(),
                        dataGridView4.CurrentRow.Cells[2].Value.ToString(), dataGridView4.CurrentRow.Cells[8].Value.ToString(),
                        dataGridView4.CurrentRow.Cells[4].Value.ToString(), dataGridView4.CurrentRow.Cells[5].Value.ToString(),
                        dataGridView4.CurrentRow.Cells[3].Value.ToString(), dataGridView4.CurrentRow.Cells[6].Value.ToString(),
                        dataGridView4.CurrentRow.Cells[9].Value.ToString());
                    forma.Show();
                }
            }
        }

        private void dataGridView4_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView4.RowCount > 0)
            {
                var forma = new vistaOrden(dataGridView4.CurrentRow.Cells[0].Value.ToString(), dataGridView4.CurrentRow.Cells[1].Value.ToString(),
                    dataGridView4.CurrentRow.Cells[2].Value.ToString(), dataGridView4.CurrentRow.Cells[8].Value.ToString(),
                    dataGridView4.CurrentRow.Cells[4].Value.ToString(), dataGridView4.CurrentRow.Cells[5].Value.ToString(),
                    dataGridView4.CurrentRow.Cells[3].Value.ToString(), dataGridView4.CurrentRow.Cells[6].Value.ToString(),
                    dataGridView4.CurrentRow.Cells[9].Value.ToString());
                forma.Show();
            }
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            textBox3.Text = dateTimePicker1.Value.ToShortDateString();
        }
    }
}
