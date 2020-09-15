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
    public partial class frm_BuscarEncargado : Form
    {

        frm_AreasProduccion FormularioAreas;
        int mode = 0;
        public frm_BuscarEncargado(frm_AreasProduccion frm, int mod)
        {
            InitializeComponent();
            FormularioAreas = frm;
            mode = mod;
        }

        private void Listar_Encargados(string busqueda)
        {
            DataTable data;
            data = logicAreas.Listar_Encargados(busqueda);

            if (data !=null)
            {
                dgv_encargados.DataSource = data;

                if (dgv_encargados.Rows.Count > 0)
                {
                    dgv_encargados.Columns[0].Visible = false;
                }
            }

        }

        private void frm_ViewEncargado_Load(object sender, EventArgs e)
        {
            Listar_Encargados("");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dgv_encargados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dgv_encargados.CurrentRow.Cells[0].Value.ToString();
            string nombre = dgv_encargados.CurrentRow.Cells[1].Value.ToString();
            


            if (nombre != null && id != null)
            {

                if (mode == 1)
                {
                    FormularioAreas.setEncargado(id, nombre);
                    this.Dispose();
                }else if (mode == 2)
                {
                    FormularioAreas.setEncargado2(id, nombre);
                    this.Dispose();
                }

            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim().ToString();
            Listar_Encargados(busqueda);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
