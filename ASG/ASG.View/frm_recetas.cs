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
using ASG.ASG.View;
using ASG.ASG.Logic;
using ASG.ASG.Entity;
using System.Runtime.Remoting.Messaging;

namespace ASG
{
    public partial class frm_recetas : Form
    {

        //Variables Globales
        List<int> eliminados = new List<int> { };
        List<StructDetalle.SDetalleReceta> insertados = new List<StructDetalle.SDetalleReceta> { };

        public frm_recetas()
        {
            InitializeComponent();

        }


        private void Cargar_Detalle_Receta(int idReceta)
        {
            DataTable data = new DataTable();
            data = logicRecetas.Listar_Detalle_Receta(idReceta);

            if (data != null)
            {
                limpiarGrid2();

                List<string> items = new List<string> { };

                foreach (DataRow row in data.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        //Se carga la fila a la lista
                        items.Add(item.ToString());
                    }
                    //Se inserta fila en grid
                    dgv_detalleReceta.Rows.Add(items[0], items[1], items[2], items[3], items[4]);
                    items.Clear();
                }

            }

        }


        private void Cargar_Detalle_Receta2(int idReceta)
        {
            DataTable data = new DataTable();
            data = logicRecetas.Listar_Detalle_Receta(idReceta);

            if (data != null)
            {
                limpiarGrid2();

                List<string> items = new List<string> { };

                foreach (DataRow row in data.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        //Se carga la fila a la lista
                        items.Add(item.ToString());
                    }
                    //Se inserta fila en grid
                    dgv_materia.Rows.Add(items[0], items[1], items[2], items[3], items[4]);
                    items.Clear();
                }

            }

            txtDescripcion.Focus();

        }


        private void Listar_Receta_Search(string busqueda)
        {

            DataTable data = new DataTable();
            data = logicRecetas.Listar_Receta_Search(busqueda);

            if (data != null)
            {
                dgv_receta.DataSource = data;
            }


        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if ((dgv_detalleReceta.RowCount > 0) | (dgv_materia.RowCount > 0))
            {
                DialogResult result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR?", "GESTION RECETAS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void cmb_materia_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_materia_TextChanged(object sender, EventArgs e)
        {

        }

        private bool MateriaRepetida(string idMateria)
        {
            if (dgv_materia.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in dgv_materia.Rows)
                {
                    string idRow = row.Cells[3].Value.ToString();

                    if (idRow == idMateria)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        private bool MateriaRepetida2(string idMateria)
        {
            if (dgv_detalleReceta.Rows.Count > 0)
            {

                foreach (DataGridViewRow row in dgv_detalleReceta.Rows)
                {
                    string idRow = row.Cells[3].Value.ToString();

                    if (idRow == idMateria)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool cambioMercaderia(string mercaderia)
        {
            if (txtMercaderia.Text != "")
            {
                return true;
            }

            return false;
        }

        private void limpiarDetalle()
        {
            txtCantidad.Clear();
            txtIdMateria.Clear();
            txtMateria.Clear();
            txtIdMedida.Clear();
            txtMedida.Clear();
            btnSearchMateria.Focus();
        }

        private void limpiarDetalle2()
        {
            txtMcantidad.Clear();
            txtMaId.Clear();
            txtMprima.Clear();
            txtMeId.Clear();
            txtMunidad.Clear();
            btnSearchMat.Focus();
        }

        private void limpiarEncabezado()
        {
            txtMercaderia.Clear();
            txtIdMercaderia.Clear();
            txtDescripcion.Clear();
            btnBuscarMercaderia.Focus();
        }

        public void limpiarGrid()
        {
            dgv_materia.Rows.Clear();
        }

        public void limpiarGrid2()
        {
            dgv_detalleReceta.Rows.Clear();
        }

        public bool verificarDatos(string idMercaderia, string descripcion)
        {
            int nItemsGrid = dgv_materia.Rows.Count;

            if (idMercaderia != "" && descripcion != "" && nItemsGrid > 0)
            {
                return true;
            }

            return false;
        }

        private void button9_Click(object sender, EventArgs e)
        {

            string cantidad = txtCantidad.Text.ToString();
            string idMateria = txtIdMateria.Text.ToString();
            string materia = txtMateria.Text.ToString();
            string medida = txtMedida.Text.ToString();
            string idMedida = txtIdMedida.Text.ToString();


            if (cantidad != "" && materia != "" && medida != "")
            {

                if (MateriaRepetida(idMateria) == false)
                {
                    dgv_materia.Rows.Add(cantidad, idMedida, medida, idMateria, materia);
                    limpiarDetalle();
                }
                else
                {
                    MessageASG.showMessage("Error, la materia prima ya ha sido ingresada", "Recetas", 0);
                    limpiarDetalle();
                }

            }
            else
            {
                MessageASG.showMessage("Error, debe ingresar todos los campos del detalle de receta", "Recetas", 0);
            }
        }

        private void txt_cantidad_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

            var existe = dgv_materia.CurrentRow;

            if (existe != null)
            {
                int index = dgv_materia.CurrentRow.Index;
                dgv_materia.Rows.RemoveAt(index);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {

            // Datos para encabezado de recta
            string idMercaderia = txtIdMercaderia.Text.ToString();
            string descripcion = txtDescripcion.Text.ToString();


            if (verificarDatos(idMercaderia, descripcion))
            {

                // Datos para detalle de receta
                List<StructDetalle.SDetalleReceta> listaDetalle = new List<StructDetalle.SDetalleReceta> { };

                foreach (DataGridViewRow row in dgv_materia.Rows)
                {
                    StructDetalle.SDetalleReceta item = new StructDetalle.SDetalleReceta();

                    item.cantidad = Convert.ToDouble(row.Cells[0].Value.ToString());
                    item.id_materia_prima = row.Cells[3].Value.ToString();
                    listaDetalle.Add(item);
                }

                // Validación y llamada al proceso de crear receta
                int respuesta = logicRecetas.Crear_Receta(idMercaderia, descripcion, listaDetalle);

                if (respuesta != 0) // La insercion fue exitosa
                {
                    limpiarDetalle();
                    limpiarGrid();
                    limpiarEncabezado();
                    MessageASG.showMessage("Receta creada correctamente", "Recetas", 1);
                }
                else // La insercion fallo
                {
                    MessageASG.showMessage("Error, la receta no se pudo crear", "Recetas", 0);
                }

            }
            else
            {
                MessageASG.showMessage("Error, por favor ingrese todos los datos requeridos", "Recetas", 0);
            }


        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void dgv_receta_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_receta_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void dgv_receta_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void tabControl1_Click_1(object sender, EventArgs e)
        {

        }

        private void frm_recetas_Load(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim().ToString();
            Listar_Receta_Search(busqueda);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            string mercaderia = txtMercaderia.ToString();

            // Comprueba si ya hay una mercaderia seleccionada
            if (cambioMercaderia(mercaderia))
            {
                if (MessageASG.ShowConfirmMessage("¿Esta seguro de cambiar la mercaderia actual y perder los datos ingresados?", "Recetas"))
                {

                    limpiarEncabezado();
                    limpiarDetalle();
                    limpiarGrid();

                    frm_buscarMercaderia frm = new frm_buscarMercaderia(this);
                    frm.Show();

                }
            }
            else // No hay mercaderia seleccionada
            {
                frm_buscarMercaderia frmMerc = new frm_buscarMercaderia(this);
                frmMerc.Show();
            }

        }

        private void button13_Click(object sender, EventArgs e)
        {
            frm_buscarMateria frmMateria = new frm_buscarMateria(this, 1);
            frmMateria.Show();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim().ToString();
            Listar_Receta_Search(busqueda);
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void cmbTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            txtBusqueda.Focus();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Listar_Receta_Search("");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            //Comprueba si el indice es >= 0
            var existe = dgv_receta.CurrentRow;

            // Elimina la fila del DataGridView
            if (existe != null)
            {

                // Confirmar si desea eliminar la receta
                bool confirmacion = MessageASG.ShowConfirmMessage("¿Esta seguro de eliminar la receta de forma permanente?", "Receta");

                if (confirmacion)
                {
                    // Comprueba resultado de marcado (estado_tupla = 0)
                    int idReceta = Convert.ToInt32(dgv_receta.CurrentRow.Cells[0].Value);
                    bool resp = logicRecetas.Cambia_Estado_Receta_Delete(idReceta);

                    if (resp)
                    {
                        int index = dgv_receta.CurrentRow.Index;
                        dgv_receta.Rows.RemoveAt(index);
                        Listar_Receta_Search("");
                        txtBusqueda.Clear();
                        txtBusqueda.Focus();
                    }
                    else
                    {
                        MessageASG.showMessage("Error al eliminar receta", "Receta", 0);
                    }

                }
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            string cantidad = txtMcantidad.Text.ToString();
            string idMateria = txtMaId.Text.ToString();
            string materia = txtMprima.Text.ToString();
            string medida = txtMunidad.Text.ToString();
            string idMedida = txtMeId.Text.ToString();


            if (cantidad != "" && materia != "" && medida != "")
            {

                if (MateriaRepetida2(idMateria) == false)
                {
                    dgv_detalleReceta.Rows.Add(cantidad, idMedida, medida, idMateria, materia);

                    StructDetalle.SDetalleReceta structura = new StructDetalle.SDetalleReceta();
                    structura.id_materia_prima = idMateria;
                    structura.cantidad = Convert.ToDouble(cantidad);


                    //Verifica que la lista no este vacia
                    if (insertados.Count > 0)
                    {
                        int current = 0;
                        int index = 0;
                        bool find = false;

                        //Verifica si el elemento ya existe
                        foreach (var item in insertados)
                        {
                            if (item.id_materia_prima == structura.id_materia_prima)
                            {
                                index = current;
                                find = true;
                            }
                            current++;
                        }

                        if (find) // Si ya existe lo elimina para ser remplazado por el nuevo
                        {
                            insertados.RemoveAt(index);
                        }

                    }

                    insertados.Add(structura);
                    limpiarDetalle2();
                }
                else
                {
                    MessageASG.showMessage("Error, la materia prima ya ha sido ingresada", "Recetas", 0);
                    limpiarDetalle2();
                }

            }
        }



        private void button4_Click_1(object sender, EventArgs e)
        {
            //frm_buscarReceta frmBReceta = new frm_buscarReceta(this);
            //frmBReceta.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frm_buscarMateria frmMateria = new frm_buscarMateria(this, 2);
            frmMateria.Show();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {

            var existe = dgv_detalleReceta.CurrentRow;

            if (existe != null)
            {
                bool encontrado = false;
                int index = dgv_detalleReceta.CurrentRow.Index;
                int valor = Convert.ToInt32(dgv_detalleReceta.CurrentRow.Cells[3].Value.ToString());


                foreach (var itemI in eliminados)
                {
                    if (itemI == valor)
                    {
                        encontrado = true;
                    }
                }

                if (encontrado == false)
                {
                    eliminados.Add(valor);
                }
                dgv_detalleReceta.Rows.RemoveAt(index);
            }
        }

        private void LimpiarEditReceta()
        {
            txtIdReceta.Clear();
            txtMercReceta.Clear();
            txtDescripRecta.Clear();
            txtMprima.Clear();
            txtMunidad.Clear();
            txtMcantidad.Clear();
            dgv_detalleReceta.Rows.Clear();
            btnBuscarReceta.Focus();

        }


        private void button7_Click(object sender, EventArgs e)
        {
            int idReceta = 0;
            string descripcion = "";

            if (txtIdReceta.Text.Trim() != "" && txtDescripRecta.Text.Trim() != "")
            {
                idReceta = Convert.ToInt32(txtIdReceta.Text.Trim());
                descripcion = txtDescripRecta.Text.Trim();
            }

        


            if (idReceta !=0 && descripcion !="")
            {
                int result = logicRecetas.Editar_Encabezado_Receta(idReceta, descripcion);

                if (result == 1)
                {
                    int result2 = logicRecetas.Editar_Detalle_Delete(idReceta, eliminados);

                    if (result2 == 1)
                    {
                        int result3 = logicRecetas.Editar_Detalle_Insert(idReceta, insertados);

                        if (result3 == 1)
                        {
                            MessageASG.showMessage("Receta editada exitosamente", "Recetas", 1);
                            LimpiarEditReceta();
                        }
                    }
                }
            }
            else
            {
                MessageASG.showMessage("Error, Debe completar los campos requeridos", "Recetas", 0);
            }







        }

        private void dgv_receta_KeyDown(object sender, KeyEventArgs e)
        {
            //Datos para enviar al preview de la receta
            string codigo = dgv_receta.CurrentRow.Cells[0].Value.ToString();
            string mercaderia = dgv_receta.CurrentRow.Cells[1].Value.ToString();
            string descripcion = dgv_receta.CurrentRow.Cells[2].Value.ToString();
            string fecha = dgv_receta.CurrentRow.Cells[3].Value.ToString();
            string creadoPor = dgv_receta.CurrentRow.Cells[4].Value.ToString();

            //Validar si presiono enter en alguna receta
            if (e.KeyCode == Keys.Enter)
            {
                frm_vistaReceta frmVReceta = new frm_vistaReceta();
                frmVReceta.setDatosReceta(codigo, mercaderia, descripcion, fecha, creadoPor);
                frmVReceta.Show();
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Listar_Receta_Search("");
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            //Comprueba si el indice es >= 0
            var existe = dgv_receta.CurrentRow;

            // Elimina la fila del DataGridView
            if (existe != null)
            {

                // Confirmar si desea eliminar la receta
                bool confirmacion = MessageASG.ShowConfirmMessage("¿Esta seguro de eliminar la receta?", "Receta");

                if (confirmacion)
                {
                    // Comprueba resultado de marcado (estado_tupla = 0)
                    int idReceta = Convert.ToInt32(dgv_receta.CurrentRow.Cells[0].Value);
                    bool resp = logicRecetas.Cambia_Estado_Receta_Delete(idReceta);

                    if (resp)
                    {
                        int index = dgv_receta.CurrentRow.Index;
                        dgv_receta.Rows.RemoveAt(index);
                        Listar_Receta_Search("");
                        txtBusqueda.Clear();
                        txtBusqueda.Focus();
                    }
                    else
                    {
                        MessageASG.showMessage("Error al eliminar receta", "Receta", 0);
                    }

                }
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            string mercaderia = txtMercaderia.ToString();

            // Comprueba si ya hay una mercaderia seleccionada
            if (cambioMercaderia(mercaderia))
            {
                if (MessageASG.ShowConfirmMessage("¿Esta seguro de cambiar la mercaderia actual y perder los datos ingresados?", "Recetas"))
                {

                    limpiarEncabezado();
                    limpiarDetalle();
                    limpiarGrid();

                    frm_buscarMercaderia frm = new frm_buscarMercaderia(this);
                    frm.Show();

                }
            }
            else // No hay mercaderia seleccionada
            {
                frm_buscarMercaderia frmMerc = new frm_buscarMercaderia(this);
                frmMerc.Show();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            frm_buscarMateria frmMateria = new frm_buscarMateria(this, 1);
            frmMateria.Show();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            frm_buscarMateria frmMateria = new frm_buscarMateria(this, 2);
            frmMateria.Show();
        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            frm_buscarReceta frmBReceta = new frm_buscarReceta(this , 1);
            frmBReceta.Show();
        }

        private void dgv_receta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var existe = dgv_receta.CurrentRow;

            if (existe != null)
            {

                //Datos para enviar al preview de la receta
                string codigo = dgv_receta.CurrentRow.Cells[0].Value.ToString();
                string mercaderia = dgv_receta.CurrentRow.Cells[1].Value.ToString();
                string descripcion = dgv_receta.CurrentRow.Cells[2].Value.ToString();
                string fecha = dgv_receta.CurrentRow.Cells[3].Value.ToString();
                string creadoPor = dgv_receta.CurrentRow.Cells[4].Value.ToString();

                //Ver preview de receta
                frm_vistaReceta frmVReceta = new frm_vistaReceta();
                frmVReceta.setDatosReceta(codigo, mercaderia, descripcion, fecha, creadoPor);
                frmVReceta.Show();
            }
        }

        private void dgv_receta_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var existe = dgv_receta.CurrentRow;

            if (existe != null)
            {

                //Datos para enviar al preview de la receta
                string codigo = dgv_receta.CurrentRow.Cells[0].Value.ToString();
                string mercaderia = dgv_receta.CurrentRow.Cells[1].Value.ToString();
                string descripcion = dgv_receta.CurrentRow.Cells[2].Value.ToString();
                string fecha = dgv_receta.CurrentRow.Cells[3].Value.ToString();
                string creadoPor = dgv_receta.CurrentRow.Cells[4].Value.ToString();

                //Ver preview de receta
                frm_vistaReceta frmVReceta = new frm_vistaReceta();
                frmVReceta.setDatosReceta(codigo, mercaderia, descripcion, fecha, creadoPor);

            }
        }

        private void button1_Click_4(object sender, EventArgs e)
        {
            frm_buscarReceta frmBReceta = new frm_buscarReceta(this, 2);
            frmBReceta.Show();
        }

        private void frm_recetas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                if ((dgv_detalleReceta.RowCount > 0) | (dgv_materia.RowCount > 0))
                {
                    DialogResult result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR?", "GESTION RECETAS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
        }
    }
}
