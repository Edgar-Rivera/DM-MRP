using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ASG.ASG.Logic;
using System.Security.Cryptography;
using System.Runtime.InteropServices;

namespace ASG.ASG.View
{
    public partial class frm_AreasProduccion : Form
    {
        string nameUs;
        string rolUs;
        string usuario;
        string idSucursal;
        public frm_AreasProduccion(string nameUser, string rolUser, string user, string sucursal,  string nombre)
        {
            InitializeComponent();
            usuario = user;
            nameUs = nameUser;
            rolUs = rolUser;
            idSucursal = sucursal;
            lbl_usuario.Text = lbl_usuario.Text + "" + nameUs;
        }


        public void Listar_Areas_Produccion(string busqueda)
        {
            DataTable data = new DataTable();
            data = logicAreas.Listar_Areas(busqueda);

            if (data !=null)
            {
                dgv_area.DataSource = data;

                if (dgv_area.Columns.Count > 0)
                {
                    dgv_area.Columns[3].Visible = false;
                }

                
            }

        }


        private void button17_Click(object sender, EventArgs e)
        {
            Listar_Areas_Produccion("");
        }

        private void frm_AreasProduccion_Load(object sender, EventArgs e)
        {
            Listar_Areas_Produccion("");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Comprueba si el indice es >= 0
            var existe = dgv_area.CurrentRow;

            // Elimina la fila del DataGridView
            if (existe != null)
            {

                // Confirmar si desea eliminar la receta
                bool confirmacion = MessageASG.ShowConfirmMessage("¿Esta seguro de eliminar el Área?", "Áreas de Producción");

                if (confirmacion)
                {
                    // Comprueba resultado de marcado (estado_tupla = 0)
                    int idArea = Convert.ToInt32(dgv_area.CurrentRow.Cells[0].Value);
                    bool resp = logicAreas.Cambia_Estado_Area_Delete(idArea);

                    if (resp)
                    {
                        int index = dgv_area.CurrentRow.Index;
                        dgv_area.Rows.RemoveAt(index);
                        Listar_Areas_Produccion("");
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

        private void dgv_area_KeyDown(object sender, KeyEventArgs e)
        {
            //Datos para enviar al preview del área
            string codigo = dgv_area.CurrentRow.Cells[0].Value.ToString();
            string descripcion = dgv_area.CurrentRow.Cells[1].Value.ToString();
            string encargado = dgv_area.CurrentRow.Cells[3].Value.ToString();

            //Validar si presiono enter en alguna área
            if (e.KeyCode == Keys.Enter)
            {
                frm_ViewAreas frmVArea = new frm_ViewAreas();
                frmVArea.setDetalleArea(codigo, descripcion, encargado);
                frmVArea.Show();
            }
        }

        private void dgv_area_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Datos para enviar al preview del área
            string codigo = dgv_area.CurrentRow.Cells[0].Value.ToString();
            string descripcion = dgv_area.CurrentRow.Cells[1].Value.ToString();
            string encargado = dgv_area.CurrentRow.Cells[3].Value.ToString();

            frm_ViewAreas frmVArea = new frm_ViewAreas();
                frmVArea.setDetalleArea(codigo, descripcion, encargado);
                frmVArea.Show();
          
        }

        private void limipiar()
        {
            txtDescripcionArea.Clear();
            txtNombreArea.Clear();
            txtIdEncargado.Clear();
            txtEncargado.Clear();
            btnBuscarEncargado.Focus();
        }

        private void limipiar2()
        {
            txtDArea.Clear();
            txtNArea.Clear();
            txtIdArea.Clear();
            TxtIdEnca.Clear();
            TxtEncargadoA.Clear();
            btnBuscarAreas.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string nombre = txtNombreArea.Text.Trim().ToString();
            string descripcion = txtDescripcionArea.Text.Trim().ToString();
            string encargado = txtEncargado.Text.Trim().ToString();
            string idEncargado = txtIdEncargado.Text.Trim().ToString();

            if (nombre !="" && descripcion !="" && encargado != "" && idEncargado != "") 
            {
                int resp = logicAreas.Crear_Area(idEncargado, nombre, descripcion);

                if (resp == 1)
                {
                    MessageASG.showMessage("Área creada correctamente", "Áreas de producción", 1);
                    limipiar();

                }
                else // FALLO LA INSERCIÓN
                {
                    MessageASG.showMessage("Error, el área no se pudo crear", "Áreas de producción", 0);
                }

            }
            else // NO COMPLETO LOS CAMPOS REQUERIDOS
            {
                MessageASG.showMessage("Error, por favor ingrese todos los datos requeridos", "Áreas de producción", 0);
            }
        }

        private void btnBuscarMercaderia_Click(object sender, EventArgs e)
        {
            frm_BuscarArea frmBArea = new frm_BuscarArea(this);
            frmBArea.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string id = txtIdArea.Text.Trim().ToString();
            string nombre = txtNArea.Text.Trim().ToString();
            string descripcion = txtDArea.Text.Trim().ToString();
            string idEncargado = TxtIdEnca.Text.Trim().ToString();

            if (id!= "" && nombre != "" && descripcion != "" && idEncargado != "")
            {
                int resp = logicAreas.Editar_Area(id, idEncargado, nombre, descripcion);

                if (resp == 1)
                {
                    MessageASG.showMessage("Área editada correctamente", "Áreas de producción", 1);
                    limipiar2();

                }
                else // FALLO LA INSERCIÓN
                {
                    MessageASG.showMessage("Error, el área no se pudo editada", "Áreas de producción", 0);
                }

            }
            else // NO COMPLETO LOS CAMPOS REQUERIDOS
            {
                MessageASG.showMessage("Error, por favor ingrese todos los datos requeridos", "Áreas de producción", 0);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿ESTA SEGURO QUE DESEA SALIR?", "GESTION ÁREAS DE PRODUCCIÓN", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frm_BuscarEncargado frmBEncargado = new frm_BuscarEncargado(this, 1);
            frmBEncargado.Show();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            frm_BuscarEncargado frmBEncargado = new frm_BuscarEncargado(this, 2);
            frmBEncargado.Show();
        }
    }
}
