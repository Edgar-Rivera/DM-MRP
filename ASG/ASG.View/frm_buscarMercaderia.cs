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
    public partial class frm_buscarMercaderia : Form
    {

        private frm_recetas frmMercaderia;

        public frm_buscarMercaderia(frm_recetas frm)
        {
            InitializeComponent();
            frmMercaderia = frm;
        }

        private void cargar_mercaderia_search(string busqueda)
        {

            DataTable data = new DataTable();
            data = logicRecetas.Listar_Mercaderia_Search(busqueda);

            if (data != null)
            {
                dgv_mercaderia.DataSource = data;
            }
        }


        private void frm_buscarMercaderia_Load(object sender, EventArgs e)
        {
            cargar_mercaderia_search(txtBusqueda.Text.Trim());
        }


        private void dgv_mercaderia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dgv_mercaderia.CurrentRow.Cells[0].Value.ToString();
            string nombre = dgv_mercaderia.CurrentRow.Cells[1].Value.ToString();


            if (nombre != "")
            {
                frmMercaderia.recibirMercaderia(id, nombre);
                this.Dispose();
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            cargar_mercaderia_search(txtBusqueda.Text.Trim());
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
