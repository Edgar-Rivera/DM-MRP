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
    public partial class frm_buscarMateria : Form
    {
        frm_recetas formularioRecetas;
        byte mode;

        public frm_buscarMateria(frm_recetas frm, byte mod)
        {
            InitializeComponent();
            formularioRecetas = frm;
            mode = mod;
        }

        private void formatoGrid()
        {
            dgv_materia.Columns[2].Visible = false; 
        }

        private void Listar_Materia_Search()
        {
            DataTable data = new DataTable();
            data = logicRecetas.Listar_Materia_Search(txtBusqueda.Text.Trim());

            if (data != null)
            {
                dgv_materia.DataSource = data;
            }

        }

        private void frm_buscarMateria_Load(object sender, EventArgs e)
        {
            Listar_Materia_Search();
            formatoGrid();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            Listar_Materia_Search();
        }

        private void dgv_materia_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string nombre = dgv_materia.CurrentRow.Cells[1].Value.ToString();
            string id = dgv_materia.CurrentRow.Cells[0].Value.ToString();
            string idMedida = dgv_materia.CurrentRow.Cells[2].Value.ToString();
            string medida = dgv_materia.CurrentRow.Cells[3].Value.ToString();

            if (nombre != null)
            {
                if (mode == 1)
                {
                    formularioRecetas.recibirMateria(id, nombre, idMedida, medida);
                
                }else if (mode == 2)
                {
                    formularioRecetas.recibirMateria2(id, nombre, idMedida, medida);
                }

                this.Dispose();
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
