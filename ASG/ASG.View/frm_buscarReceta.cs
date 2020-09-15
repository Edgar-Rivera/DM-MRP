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
    public partial class frm_buscarReceta : Form
    {
        frm_recetas frmRecetas;
        int modo = 0;
        Point DragCursor;
        Point DragForm;
        bool Dragging;
        public frm_buscarReceta(frm_recetas frm, int mode)
        {
            InitializeComponent();
            frmRecetas = frm;
            modo = mode;
        }



        private void Listar_Receta_Search(string busqueda)
        {
            DataTable data = new DataTable();
            data = logicRecetas.Listar_Receta_Search(busqueda);

            if (data != null)
            {
                dgv_receta.DataSource = data;
                //dgv_receta.Columns[6].Visible = false;
            }
        }

        private void cmbTipoBusqueda_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            txtBusqueda.Focus();
        }

        private void frm_buscarReceta_Load(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim().ToString();
            Listar_Receta_Search(busqueda);
        }



        private void txtBusqueda_TextChanged_1(object sender, EventArgs e)
        {
            string busqueda = txtBusqueda.Text.Trim().ToString();
            Listar_Receta_Search(busqueda);
        }

        private void dgv_receta_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int codigoReceta = Convert.ToInt32(dgv_receta.CurrentRow.Cells[0].Value.ToString());
            string mercaderiaReceta = dgv_receta.CurrentRow.Cells[1].Value.ToString();
            string descripcionReceta = dgv_receta.CurrentRow.Cells[2].Value.ToString();
            string idMercaderia = dgv_receta.CurrentRow.Cells[6].Value.ToString();

            if (codigoReceta != 0 && mercaderiaReceta != "" && descripcionReceta != "")
            {
                if (modo == 1)
                {
                    frmRecetas.recibirReceta(codigoReceta, mercaderiaReceta, descripcionReceta);
                    this.Dispose();
                }
                else if (modo == 2)
                {
                    frmRecetas.recibirReceta2(codigoReceta, descripcionReceta, mercaderiaReceta, idMercaderia);
                    this.Dispose();
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_buscarReceta_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;
        }

        private void frm_buscarReceta_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_buscarReceta_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void frm_buscarReceta_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
