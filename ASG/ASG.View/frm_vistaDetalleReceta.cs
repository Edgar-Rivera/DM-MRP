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
    public partial class frm_vistaDetalleReceta : Form
    {
        Point DragCursor;
        Point DragForm;
        bool Dragging;

        int IdReceta;
        public frm_vistaDetalleReceta()
        {
            InitializeComponent();
        }

        private void ListarDetalleReceta(int idReceta)
        {
            DataTable data = new DataTable();
            data = logicRecetas.Listar_Detalle_Receta(idReceta);

            if (data != null)
            {
                dgv_DetalleReceta.DataSource = data;
            }

        }

        private void FormatoData()
        {
            dgv_DetalleReceta.Columns[1].Visible = false;
            dgv_DetalleReceta.Columns[3].Visible = false;
        }

        private void frm_vistaDetalleReceta_Load(object sender, EventArgs e)
        {
            ListarDetalleReceta(IdReceta);
            FormatoData();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgv_DetalleReceta_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
           

        }

        private void frm_vistaDetalleReceta_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragCursor = Cursor.Position;
            DragForm = this.Location;

        }

        private void frm_vistaDetalleReceta_MouseUp(object sender, MouseEventArgs e)
        {
            Dragging = false;
        }

        private void frm_vistaDetalleReceta_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging == true)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(DragCursor));
                this.Location = Point.Add(DragForm, new Size(dif));
            }
        }

        private void frm_vistaDetalleReceta_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
