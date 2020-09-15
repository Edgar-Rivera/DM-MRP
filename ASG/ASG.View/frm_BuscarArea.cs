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

namespace ASG.ASG.View
{
    public partial class frm_BuscarArea : Form
    {
        frm_AreasProduccion FormularioAreas;

        public frm_BuscarArea(frm_AreasProduccion frm)
        {
            InitializeComponent();
            FormularioAreas = frm;
        }


        private void Listar_Areas(string busqueda)
        {
            DataTable data = new DataTable();
            data = logicAreas.Listar_Areas(busqueda);

            if (data != null)
            {
                dgv_area.DataSource = data;
                dgv_area.Columns[3].Visible = false;
            }

        }

        private void frm_BuscarArea_Load(object sender, EventArgs e)
        {
            Listar_Areas("");
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim().ToString();
            Listar_Areas(busqueda);
        }

        private void dgv_area_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dgv_area.CurrentRow.Cells[0].Value.ToString();
            string nombre = dgv_area.CurrentRow.Cells[1].Value.ToString();
            string descripcion = dgv_area.CurrentRow.Cells[2].Value.ToString();
            string idEncargado = dgv_area.CurrentRow.Cells[3].Value.ToString();
            string encargado = dgv_area.CurrentRow.Cells[4].Value.ToString();


            if (id != null && nombre != null && descripcion != null && idEncargado != null && encargado != null)
            {
                FormularioAreas.setDetallesArea(id, nombre, descripcion, idEncargado, encargado);
                this.Dispose();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_area_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dgv_area_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
