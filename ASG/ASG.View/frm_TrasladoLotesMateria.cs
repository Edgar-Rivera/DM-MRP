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
    public partial class frm_TrasladoLotesMateria : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        bool flagsc;
        string usuario;
        string idSucursal;
        string Traslado;
        ContextMenuStrip mymenuS = new ContextMenuStrip();
        List<string> productoSeleccionados = new List<string>();
        public frm_TrasladoLotesMateria(string user, string sucursal)
        {
            InitializeComponent();
            usuario = user;
            idSucursal = sucursal;
            cargaSucursales();
            stripMenuSC();
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

                    comboBox1.Items.Clear();
                    comboBox1.Items.Add(reader.GetString(0));
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void stripMenuSC()
        {
            mymenuS.Items.Add("Eliminar Producto del Traslado");
            mymenuS.Items[0].Name = "ColHidden";
        }
        private void frm_TrasladoLotesMateria_Load(object sender, EventArgs e)
        {
            dataGridView4.Columns[0].ReadOnly = true;
            dataGridView4.Columns[1].ReadOnly = true;
            dataGridView4.Columns[2].ReadOnly = true;
            dataGridView4.Columns[3].ReadOnly = true;
            dataGridView4.Columns[4].ReadOnly = true;
            dataGridView4.Columns[5].ReadOnly = false;

            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[2].ReadOnly = false;
        }
        internal class CargaProductos
        {
            public List<string> listaCodigo { get; set; }
        }

        private void frm_TrasladoLotesMateria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if ((dataGridView4.RowCount > 0) | (comboBox1.Text != ""))
                {
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR, LOS DATOS DEL TRASLADO SE PERDERAN?", "TRASLADO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                button3.PerformClick();
            }
            else if (Convert.ToInt32(e.KeyData) == Convert.ToInt32(Keys.Control) + Convert.ToInt32(Keys.S))
            {
                button2.PerformClick();
            }
        }

        private void frm_TrasladoLotesMateria_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
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
        private void frm_TrasladoLotesMateria_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_TrasladoLotesMateria_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if ((dataGridView4.RowCount > 0) | (comboBox1.Text != ""))
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR, LOS DATOS DEL TRASLADO SE PERDERAN?", "TRASLADO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void cargaSucursalesDestino()
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT ID_SUCURSAL, NOMBRE_SUCURSAL FROM SUCURSAL WHERE ESTADO_TUPLA = TRUE AND NOMBRE_SUCURSAL <> '{0}';", comboBox1.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), false);
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), false);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                comboBox1.Enabled = false;
                cargaSucursalesDestino();
            }
        }
        private void cargaDatosProucto(string codigo)
        {
            try
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                string sql = string.Format("SELECT DESCRIPCION,  PRECIO, TOTAL_UNIDADES FROM VISTA_MATERIA_PRIMA_FORM  WHERE ID_MATERIA_PRIMA = '{0}' " +
                    "AND SUCURSAL = '{1}';", codigo, comboBox1.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView4.Rows.Add(codigo, comboBox1.Text, reader.GetString(0), reader.GetString(1), reader.GetString(2), 0);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var forma = new frm_seleccionaMateriaTraslado(comboBox1.Text, "ADMINISTRADOR", false);
            if (forma.ShowDialog() == DialogResult.OK)
            {
                productoSeleccionados = forma.currenProductos.listaCodigo;
                foreach (var codigo in productoSeleccionados)
                {
                    cargaDatosProucto(codigo);
                }
            }
        }
        private bool validacionesTraslados()
        {
            for (int i = 0; i < dataGridView4.RowCount; i++)
            {
                if (dataGridView4.Rows[i].Cells[5].Value != null)
                {
                    if (Convert.ToDouble(dataGridView4.Rows[i].Cells[5].Value.ToString()) <= 0)
                    {
                        MessageBox.Show("LA CANTIDAD A TRASLADAR DE: " + "\"" + dataGridView4.Rows[i].Cells[2].Value.ToString() + "\"" + " DEBE SER MAYOR A 0!", "TRASLADOS MERCADERIA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }
            return true;
        }
        private bool creaNuevoTraslado(string codigo, string destino)
        {
            
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("INSERT INTO TRASLADOS_MATERIA_PRIMA VALUES ({0},'{1}', " +
                    "(SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{2}')," +
                    "(SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{3}'), NOW(), TRUE);",
                   codigo, usuario, comboBox1.Text, destino);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                   //adressUser.ingresaBitacora(idSucursal, usuario, "TRASLADO DE MATERIA PRIMA", codigo);
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
        private bool sucursalSeleccionada()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value != null)
                {
                    if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[2].Value.ToString()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool ActualizaCantidadEnSucursalTraslado(string cantidad, string codigo, string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE SUCURSAL_HAS_MATERIA_PRIMA " +
                    "SET TOTAL_UNIDADES = TOTAL_UNIDADES - {0} WHERE " +
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
        private bool ActualizaCantidadEnSucursalTrasladoAd(string cantidad, string codigo, string sucursal)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE SUCURSAL_HAS_MATERIA_PRIMA " +
                    "SET TOTAL_UNIDADES = TOTAL_UNIDADES + {0} WHERE " +
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
                    " ID_MATERIA_PRIMA = '{0}' AND ID_SUCURSAL = " +
                    "(SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{1}');",
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
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView4.RowCount > 0)
            {
                if (sucursalSeleccionada())
                {
                    if (validacionesTraslados())
                    {
                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[2].Value != null)
                            {
                                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[2].Value.ToString()))
                                {
                                    Traslado = numeroTraslado();
                                    if (creaNuevoTraslado(Traslado,dataGridView1.Rows[i].Cells[1].Value.ToString()))
                                    {
                                        for (int j = 0; j<dataGridView4.RowCount; j++)
                                        {
                                            if (ActualizaCantidadEnSucursalTraslado(
                                                dataGridView4.Rows[j].Cells[5].Value.ToString(),
                                                dataGridView4.Rows[j].Cells[0].Value.ToString(),
                                                dataGridView4.Rows[j].Cells[1].Value.ToString()))
                                            {
                                                // se actualiza la cantidad en al sucursal existente
                                                if (existeEnSucursal(dataGridView4.Rows[j].Cells[0].Value.ToString(),
                                                    dataGridView1.Rows[i].Cells[1].Value.ToString()))
                                                {
                                                    if (ActualizaCantidadEnSucursalTrasladoAd(dataGridView4.Rows[j].Cells[5].Value.ToString()
                                                        , dataGridView4.Rows[j].Cells[0].Value.ToString(),
                                                        dataGridView1.Rows[i].Cells[1].Value.ToString()))
                                                    {
                                                        // se agrega este detalle al trastlado ya existente
                                                        if (ingresaTraslado(Traslado, dataGridView4.Rows[j].Cells[0].Value.ToString(),
                                                            dataGridView4.Rows[j].Cells[5].Value.ToString()))
                                                        {
                                                          
                                                        }
                                                    }
                                                } else
                                                {
                                                    // la matria prima no existten en al sucursal
                                                    OdbcConnection conexion = ASG_DB.connectionResult();
                                                    try
                                                    {
                                                        string sql = string.Format("INSERT INTO SUCURSAL_HAS_MATERIA_PRIMA VALUES " +
                                                            "((SELECT ID_SUCURSAL FROM SUCURSAL WHERE NOMBRE_SUCURSAL = '{0}'),'{1}',{2},{3}, TRUE);",
                                                             dataGridView1.Rows[i].Cells[1].Value.ToString(), 
                                                             dataGridView4.Rows[j].Cells[0].Value.ToString(),
                                                             dataGridView4.Rows[j].Cells[5].Value.ToString(),
                                                             dataGridView4.Rows[j].Cells[3].Value.ToString());
                                                        OdbcCommand cmd = new OdbcCommand(sql, conexion);
                                                        if (cmd.ExecuteNonQuery() == 1)
                                                        {
                                                            //MessageBox.Show("se guardo en has");
                                                            if (ingresaTraslado(Traslado, dataGridView4.Rows[j].Cells[0].Value.ToString(),
                                                            dataGridView4.Rows[j].Cells[5].Value.ToString()))
                                                            {
                                                               
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
                            }
                        }
                        var fomra = new frm_done();
                        fomra.ShowDialog();
                        dataGridView1.Rows.Clear();
                        dataGridView4.Rows.Clear();
                        comboBox1.SelectedIndex = -1;
                        comboBox1.Enabled = true;
                        
                    }
                } else
                {
                    MessageBox.Show("NO HA SELECCIONADO UNA SUCURSAL DE DESTINO!", "TRASLADOS MATERIA PRIMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            } else
            {
                MessageBox.Show("DEBE SELECCIONAR AL MENOS UN PRODUCTO PARA TRASLADAR!", "TRASLADOS MATERIA PRIMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool ingresaTraslado(string traslado, string materia, string cantidad)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("CALL INGRESA_PRODUCTOS_TRASLADO_MATERIA ({0},'{1}',{2});", Traslado, materia, cantidad);
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
        private void my_menu_ItemclickedSC(object sender, ToolStripItemClickedEventArgs e)
        {
            if (flagsc == false)
            {
                if (e.ClickedItem.Name == "ColHidden")
                {
                    mymenuS.Visible = false;
                    DialogResult result;
                    result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ELIMINAR EL PRODUCTO DEL TRASLADO?", "TRASLADO DE MERCADERIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        dataGridView4.Rows.RemoveAt(dataGridView4.CurrentRow.Index);
                    }
                    flagsc = true;
                }

            }
        }
        private void dataGridView4_MouseClick(object sender, MouseEventArgs e)
        {
            if (dataGridView4.RowCount > 0)
            {
                if (e.Button == MouseButtons.Right)
                {
                    mymenuS.Show(dataGridView4, new Point(e.X, e.Y));
                    mymenuS.ItemClicked += new ToolStripItemClickedEventHandler(my_menu_ItemclickedSC);
                    mymenuS.Enabled = true;
                    flagsc = false;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" & Convert.ToDouble(textBox1.Text) > 0)
            {
                if (dataGridView4.RowCount > 0)
                {
                    for (int i = 0; i < dataGridView4.RowCount; i++)
                    {
                        dataGridView4.Rows[i].Cells[5].Value = textBox1.Text;
                    }
                }
                else
                {
                    MessageBox.Show("NO HA INGRESADO NINGUN PRODUCTO PARA TRASLADAR!", "TRASLADOS MATERIA PRIMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("INGRESE UNA CANTIDAD VALIDA!", "TRASLADOS MATERIA PRIMA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView4.RowCount > 0)
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?, LOS VALORES SE RESTAURARAN CON 0!", "TRASLADO DE MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    textBox1.Text = "";
                    for (int i = 0; i < dataGridView4.RowCount; i++)
                    {
                        dataGridView4.Rows[i].Cells[5].Value = "0";
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA CANCELAR LA OPERACION?", "TRASLADO DE MATERIA PRIMA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    dataGridView4.Rows.Clear();
                    dataGridView1.Rows.Clear();
                    comboBox1.SelectedIndex = -1;
                    comboBox1.Enabled = true;
                }
            }
        }
    }
}
