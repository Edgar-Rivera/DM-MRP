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
    public partial class produccion : Form
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
        string areaSeguimiento;
        public produccion(string nameUser, string rolUser, string user, string sucursal, string nombre)
        {
            InitializeComponent();
            usuario = user;
            nameUs = nameUser;
            rolUs = rolUser;
            idSucursal = sucursal;
            nombreSucursal = nombre;
            label19.Text = label19.Text + "" + nameUs;
            
        }

        private void produccion_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }

        private void produccion_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void produccion_MouseMove(object sender, MouseEventArgs e)
        {

            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void produccion_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        internal class Orden
        {
            public string getOrden { get; set; }
            public string getMercaderia { get; set; }
            public string getDescripcion { get; set; }
            public string getCantidad { get; set; }
            public string getReceta { get; set; }

        }
        private void cargaDetalleReceta(string receta)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM VISTA_DETALLE_RECETA WHERE ID_RECETA = {0};", receta);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView5.Rows.Clear();
                    dataGridView5.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), ' ', reader.GetString(4));
                    while (reader.Read())
                    {
                        dataGridView5.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), ' ', reader.GetString(4));
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
        private void cargaAreas(string orden)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT id_proceso_produccion, " +
                    "(select nombre_area from area_produccion where id_area = id_proceso_produccion) FROM proceso_produccion WHERE id_orden_produccion = {0} and estado_tupla = true;",orden);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView4.Rows.Clear();
                    dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), true);
                    while (reader.Read())
                    {
                        dataGridView4.Rows.Add(reader.GetString(0), reader.GetString(1), true);
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
        private void cargaTotales()
        {
            if (dataGridView5.RowCount > 0)
            {
                for (int i = 0; i < dataGridView5.RowCount; i++)
                {
                    dataGridView5.Rows[i].Cells[4].Value = Convert.ToDouble(dataGridView5.Rows[i].Cells[3].Value.ToString()) * Convert.ToDouble(textBox10.Text);
                }
            }
        }
        private void cargaAreas()
        {

        }
        private void button7_Click(object sender, EventArgs e)
        {
            var forma = new obtieneOrden("ABIERTA");
            if(forma.ShowDialog() == DialogResult.OK)
            {
                textBox7.Text = forma.CurrentOrden.getOrden;
                textBox11.Text = forma.CurrentOrden.getMercaderia;
                textBox12.Text = forma.CurrentOrden.getDescripcion;
                textBox10.Text = forma.CurrentOrden.getCantidad;
                textBox8.Text = dateTimePicker2.Value.ToShortDateString();
                comboBox2.SelectedIndex = 0;
                cargaDetalleReceta(forma.CurrentOrden.getReceta);
                cargaAreas(forma.CurrentOrden.getOrden);
                cargaTotales();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            textBox8.Text = dateTimePicker2.Value.ToShortDateString();
        }
        private void limpiaOrden()
        {
       
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox2.SelectedIndex = -1;
        }
        private bool iniciaProduccion(string orden)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE ordenes_produccion SET ESTADO_ORDEN = 2 , FECHA_INICIO = '{0}' where id_orden_produccion = {1};", dateTimePicker2.Value.ToString("yyyy-MM-dd"),orden);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;               
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
            return false;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if ((dataGridView4.RowCount > 0) && (dataGridView5.RowCount > 0) && textBox11.Text != "" && textBox12.Text != ""
              && textBox10.Text != "" && textBox7.Text != "" && textBox8.Text != "" && comboBox2.SelectedIndex != -1 )
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA INICIAR EL PROCESO DE PRODUCCION?, ESTE PROCESO ES IRREVERSIBLE", "ORDEN DE PRODUCCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (iniciaProduccion(textBox7.Text))
                    {
                        var forma = new frm_done();
                        forma.ShowDialog();
                        limpiaOrden();
                    }
                    else
                    {
                        MessageBox.Show("La orden no fue creada exitosamente!");
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Debe ingresar los datos requeridos!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "")
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR, LOS DATOS DE LA ORDEN SE PERDERAN?", "ORDEN DE PRODUCCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    limpiaOrden();
                }
            }
        }
        private void cargaDetalleRecetaArea(string orden)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM vista_asignacion_area WHERE ID_ORDEN_PRODUCCION = {0};", orden);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView2.Rows.Clear();
                    dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    while (reader.Read())
                    {
                        dataGridView2.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
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
        private void cargaAreasSinProcesos(string orden)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM vista_areas_dispnibles WHERE ID_ORDEN_PRODUCCION = {0};", orden);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox1.Items.Clear();
                    comboBox1.Items.Add(reader.GetString(1));
                    while (reader.Read())
                    {
                        comboBox1.Items.Add(reader.GetString(1));
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
        private void cargaTotalesArea()
        {
            if (dataGridView2.RowCount > 0)
            {
                for (int i = 0; i < dataGridView2.RowCount; i++)
                {
                    dataGridView2.Rows[i].Cells[4].Value = Convert.ToDouble(dataGridView2.Rows[i].Cells[3].Value.ToString()) * Convert.ToDouble(textBox3.Text);
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            var forma = new obtieneOrden("EN PROCESO");
            if (forma.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = forma.CurrentOrden.getOrden;
                textBox4.Text = forma.CurrentOrden.getDescripcion;
                textBox3.Text = forma.CurrentOrden.getCantidad;
                receta = forma.CurrentOrden.getReceta;
                cargaDetalleRecetaArea(forma.CurrentOrden.getOrden);
                cargaAreasSinProcesos(forma.CurrentOrden.getOrden);
       
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridView2.RowCount > 0)
            {
                var forma = new disponibilidadBodegas(receta);
                forma.Show();
            }
            else
            {
                MessageBox.Show("No ha seleccionado una receta valida!");
            }
        }

        private void produccion_Load(object sender, EventArgs e)
        {
            dataGridView3.Columns[0].ReadOnly = true;
            dataGridView3.Columns[1].ReadOnly = true;
            dataGridView3.Columns[2].ReadOnly = false;
        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(dataGridView2.RowCount > 0)
            {
                if (comboBox1.SelectedIndex != -1)
                {
                    dataGridView3.Rows.Add(dataGridView2.CurrentRow.Cells[0].Value, dataGridView2.CurrentRow.Cells[1].Value, dataGridView2.CurrentRow.Cells[4].Value);
                }
                else
                {
                    MessageBox.Show("No ha seleccionado un area de produccion!");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(dataGridView3.RowCount > 0)
            {
                MessageBox.Show("Para cambiar el area debe de cancelar el proceso actual");
            }
            if(comboBox1.SelectedIndex != -1)
            {
                comboBox1.Enabled = false;
            }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if(dataGridView2.RowCount > 0)
            {
                if (e.KeyData == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    if (comboBox1.SelectedIndex != -1)
                    {
                        dataGridView3.Rows.Add(dataGridView2.CurrentRow.Cells[0].Value, dataGridView2.CurrentRow.Cells[1].Value, dataGridView2.CurrentRow.Cells[4].Value);
                    }
                    else
                    {
                        MessageBox.Show("No ha seleccionado un area de produccion!");
                    }
                }
            }
        }
        private bool cambiaEstado()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE proceso_produccion SET ESTADO = 2 , FECHA_INICIO = now() where id_orden_produccion = {0} and id_proceso_produccion = " +
                    "(select id_area from area_produccion where nombre_area = '{1}' limit 1);", textBox2.Text, comboBox1.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("NO SE PUDO GUARDAR EL PROCESO EN EL AREA!", "GESTION ORDENES PRODUCCION", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
            return false;
        }
        private void guardaCantidades(string area, string ordenes, string merca, string cantidad)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("INSERT INTO proceso_produccion_has_materia_prima VALUES ((SELECT id_area FROM area_produccion WHERE nombre_area = '{0}' LIMIT 1),{1},'{2}',{3},0,0);", area, ordenes, merca, cantidad);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                   
                  

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "" && comboBox1.SelectedIndex != -1 && dataGridView3.RowCount > 0)
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA ASIGNAR LA MATERIA A EL AREA DE PRODUCCION? TENGA EN CUENTA QUE ESTA ACCION ES IRREVERSIBLE", "ORDEN DE PRODUCCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (cambiaEstado())
                    {
                        for (int i = 0; i < dataGridView3.RowCount; i++)
                        {
                            guardaCantidades(comboBox1.Text, textBox2.Text, dataGridView3.Rows[i].Cells[0].Value.ToString(), dataGridView3.Rows[i].Cells[2].Value.ToString());
                        }
                        var forma = new frm_done();
                        forma.ShowDialog();
                        limpiaIniciaAreas();
                    }
                }
                else
                {
                    MessageBox.Show("Es necesario que llene todas las areas requeridas!");
                }
            }
               
        }
        private void limpiaIniciaAreas()
        {
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            comboBox1.Items.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox1.Enabled = true;
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }
        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "" | comboBox1.SelectedIndex != -1)
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR, LOS DATOS DE LA ORDEN SE PERDERAN?", "ORDEN DE PRODUCCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    limpiaIniciaAreas();
                }
            }
        }
        private void cargaAreasEnProcesos(string orden)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM vista_areas_procesos WHERE ID_ORDEN_PRODUCCION = {0};", orden);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    comboBox3.Items.Clear();
                    comboBox3.Items.Add(reader.GetString(1));
                    while (reader.Read())
                    {
                        comboBox3.Items.Add(reader.GetString(1));
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
        private void button10_Click(object sender, EventArgs e)
        {
            var forma = new obtieneOrden("EN PROCESO");
            if (forma.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = forma.CurrentOrden.getOrden;
               
                textBox1.Text = forma.CurrentOrden.getDescripcion;
                textBox5.Text = forma.CurrentOrden.getCantidad;
                cargaAreasEnProcesos(forma.CurrentOrden.getOrden);
               

            }
        }
        private void cargaDetalleSeguimiento(string ordenS, string areaS)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                dataGridView6.Rows.Clear();
                string sql = string.Format("SELECT * FROM vista_materia_area WHERE ID_ORDEN_PRODUCCION = {0} and id_proceso_produccion = {1};", ordenS, areaS);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    
                    dataGridView6.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
                    while (reader.Read())
                    {
                        dataGridView6.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
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
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox3.SelectedIndex != -1)
            {
                OdbcConnection conexion = ASG_DB.connectionResult();
                try
                {
                    string sql = string.Format("SELECT * FROM VISTA_AREA WHERE NOMBRE_AREA = '{0}';", comboBox3.Text);
                    OdbcCommand cmd = new OdbcCommand(sql, conexion);
                    OdbcDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        areaSeguimiento = reader.GetString(0);
                        textBox13.Text = reader.GetString(1);
                        textBox9.Text = reader.GetString(3);
                        cargaDetalleSeguimiento(textBox6.Text,areaSeguimiento );
                    }
                    else
                    {
                        // MessageBox.Show("NO EXISTEN CLIENTES!", "GESTION CLIENTES", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("FALLO LA CONEXION CON LA BASE DE DATOS!" + "\n" + ex.ToString(), "GESTION ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                conexion.Close();
            }
        }
        private bool actualizaProceso()
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("UPDATE proceso_produccion SET ESTADO = 3, FECHA_INICIO = now() where id_orden_produccion = {0} and id_proceso_produccion = " +
                    "(select id_area from area_produccion where nombre_area = '{1}' limit 1);", textBox6.Text, comboBox3.Text);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("NO SE PUDO GUARDAR EL PROCESO EN EL AREA!", "GESTION ORDENES PRODUCCION", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            conexion.Close();
            return false;
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != "" && comboBox3.SelectedIndex != -1)
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA TERMINAR EL PROCESO EN EL AREA? TENGA EN CUENTA QUE ESTA ACCION ES IRREVERSIBLE", "ORDEN DE PRODUCCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    if (actualizaProceso())
                    {
                        var forma = new frm_done();
                        forma.ShowDialog();
                        limpiaSeguimiento();
                    }
                }
            }
        }
        private void limpiaSeguimiento()
        {
            dataGridView6.Rows.Clear();
            comboBox3.Items.Clear();
            comboBox3.SelectedIndex = -1;
            comboBox3.Enabled = true;
            textBox6.Text = "";
            textBox1.Text = "";
            textBox5.Text = "";
            textBox9.Text = "";
            textBox13.Text = "";

        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" | comboBox1.SelectedIndex != -1)
            {
                DialogResult result;
                result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR, LOS DATOS DE LA ORDEN SE PERDERAN?", "ORDEN DE PRODUCCION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    limpiaSeguimiento();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView6.RowCount > 0)
            {
                var forma = new requestArea(textBox6.Text, areaSeguimiento, dataGridView6.CurrentRow.Cells[0].Value.ToString(), dataGridView6.CurrentRow.Cells[1].Value.ToString(), true);
                if(forma.ShowDialog() == DialogResult.OK)
                {
                    cargaDetalleSeguimiento(textBox6.Text, areaSeguimiento);
                }
            } else
            {
                MessageBox.Show("No hay materia prima en el area para solicitar mas cantidad!");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView6.RowCount > 0)
            {
                var forma = new requestArea(textBox6.Text, areaSeguimiento, dataGridView6.CurrentRow.Cells[0].Value.ToString(), dataGridView6.CurrentRow.Cells[1].Value.ToString(), false);
                if (forma.ShowDialog() == DialogResult.OK)
                {
                    cargaDetalleSeguimiento(textBox6.Text, areaSeguimiento);
                }
            }
            else
            {
                MessageBox.Show("No hay materia prima en el area para ingresar materia prima como perdida!");
            }
        }
        private void cargaProduccion(string ordenesE)
        {
            OdbcConnection conexion = ASG_DB.connectionResult();
            try
            {
                string sql = string.Format("SELECT * FROM vista_datos_maestros WHERE id_orden_produccion = {0};", ordenesE);
                OdbcCommand cmd = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                    while (reader.Read())
                    {
                        dataGridView1.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
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
        private void button12_Click(object sender, EventArgs e)
        {
            var forma = new obtieneOrden("EN PROCESO");
            if (forma.ShowDialog() == DialogResult.OK)
            {
                textBox18.Text = forma.CurrentOrden.getOrden;
                textBox14.Text = forma.CurrentOrden.getDescripcion;
                textBox15.Text = forma.CurrentOrden.getCantidad;
                cargaProduccion(forma.CurrentOrden.getOrden);
            }
        }
    }
}
