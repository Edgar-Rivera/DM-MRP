namespace ASG
{
    partial class frm_recetas
    {
     

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        // CUSTOM

        public void recibirMercaderia(string id, string nombre)
        {
            txtIdMercaderia.Text = id;
            txtMercaderia.Text = nombre;
        }

        public void recibirMateria(string id, string nombre, string idMedad, string medida)
        {
            txtIdMateria.Text = id;
            txtMateria.Text = nombre;
            txtMedida.Text = medida;
            txtIdMedida.Text = idMedad;
        }

        public void recibirMateria2(string id, string nombre, string idMedad, string medida)
        {
            txtMaId.Text = id;
            txtMprima.Text = nombre;
            txtMeId.Text = idMedad;
            txtMunidad.Text = medida;
        }

        public void recibirReceta(int idReceta, string mercaderiaReceta, string descripcionReceta)
        {
            txtIdReceta.Text = idReceta.ToString();
            txtMercReceta.Text = mercaderiaReceta;
            txtDescripRecta.Text = descripcionReceta;

            Cargar_Detalle_Receta(idReceta);

        }

        public void recibirReceta2( int idReceta, string descripcion, string mercaderiaReceta , string idMercaderia)
        {
            txtIdMercaderia.Text = idMercaderia;
            txtDescripcion.Text = descripcion;
            txtMercaderia.Text = mercaderiaReceta;

            Cargar_Detalle_Receta2(idReceta);

        }




        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_recetas));
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_usuario = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dgv_receta = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnBuscarMercaderia = new System.Windows.Forms.Button();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdMedida = new System.Windows.Forms.TextBox();
            this.txtIdMateria = new System.Windows.Forms.TextBox();
            this.txtIdMercaderia = new System.Windows.Forms.TextBox();
            this.txtMercaderia = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSearchMateria = new System.Windows.Forms.Button();
            this.txtMedida = new System.Windows.Forms.TextBox();
            this.txtMateria = new System.Windows.Forms.TextBox();
            this.dgv_materia = new System.Windows.Forms.DataGridView();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id_materia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBuscarReceta = new System.Windows.Forms.Button();
            this.txtDescripRecta = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtMeId = new System.Windows.Forms.TextBox();
            this.txtMaId = new System.Windows.Forms.TextBox();
            this.txtIdReceta = new System.Windows.Forms.TextBox();
            this.txtMercReceta = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnSearchMat = new System.Windows.Forms.Button();
            this.txtMunidad = new System.Windows.Forms.TextBox();
            this.txtMprima = new System.Windows.Forms.TextBox();
            this.dgv_detalleReceta = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMcantidad = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_receta)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_materia)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detalleReceta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(101)))), ((int)(((byte)(36)))));
            this.panel2.Location = new System.Drawing.Point(18, 50);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1045, 3);
            this.panel2.TabIndex = 27;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbl_usuario);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 593);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1074, 28);
            this.panel1.TabIndex = 26;
            // 
            // lbl_usuario
            // 
            this.lbl_usuario.BackColor = System.Drawing.Color.Transparent;
            this.lbl_usuario.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_usuario.ForeColor = System.Drawing.Color.White;
            this.lbl_usuario.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbl_usuario.Location = new System.Drawing.Point(891, 1);
            this.lbl_usuario.Name = "lbl_usuario";
            this.lbl_usuario.Size = new System.Drawing.Size(163, 18);
            this.lbl_usuario.TabIndex = 22;
            this.lbl_usuario.Text = "Usuario: ";
            this.lbl_usuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(18, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 18);
            this.label1.TabIndex = 21;
            this.label1.Text = "Multivelas";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(17, 67);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1045, 513);
            this.tabControl1.TabIndex = 32;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click_1);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.ImageIndex = 2;
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1037, 485);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "CONSULTAR / ELIMINAR";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button8);
            this.groupBox1.Controls.Add(this.button11);
            this.groupBox1.Controls.Add(this.button17);
            this.groupBox1.Controls.Add(this.txtBusqueda);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1028, 479);
            this.groupBox1.TabIndex = 80;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recetas Existentes";
            // 
            // button8
            // 
            this.button8.BackColor = System.Drawing.Color.Transparent;
            this.button8.Image = global::ASG.Properties.Resources.research;
            this.button8.Location = new System.Drawing.Point(722, 19);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(33, 31);
            this.button8.TabIndex = 196;
            this.toolTip1.SetToolTip(this.button8, "Buscar Materia Prima");
            this.button8.UseVisualStyleBackColor = false;
            // 
            // button11
            // 
            this.button11.BackColor = System.Drawing.Color.Transparent;
            this.button11.Image = global::ASG.Properties.Resources.delete__1_;
            this.button11.Location = new System.Drawing.Point(800, 19);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(32, 31);
            this.button11.TabIndex = 194;
            this.toolTip1.SetToolTip(this.button11, "Activar / Desactivar Materia Prima");
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click_1);
            // 
            // button17
            // 
            this.button17.BackColor = System.Drawing.Color.Transparent;
            this.button17.Image = global::ASG.Properties.Resources.refresh;
            this.button17.Location = new System.Drawing.Point(761, 19);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(33, 31);
            this.button17.TabIndex = 195;
            this.toolTip1.SetToolTip(this.button17, "Refrescar Datos");
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.BackColor = System.Drawing.Color.White;
            this.txtBusqueda.Location = new System.Drawing.Point(173, 25);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(538, 23);
            this.txtBusqueda.TabIndex = 72;
            this.txtBusqueda.TextChanged += new System.EventHandler(this.txtBusqueda_TextChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dgv_receta);
            this.groupBox6.Location = new System.Drawing.Point(1, 52);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(1024, 424);
            this.groupBox6.TabIndex = 80;
            this.groupBox6.TabStop = false;
            // 
            // dgv_receta
            // 
            this.dgv_receta.AllowUserToAddRows = false;
            this.dgv_receta.AllowUserToDeleteRows = false;
            this.dgv_receta.AllowUserToResizeColumns = false;
            this.dgv_receta.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.dgv_receta.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_receta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_receta.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgv_receta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_receta.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_receta.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(174)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_receta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_receta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(82)))), ((int)(((byte)(120)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_receta.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_receta.EnableHeadersVisualStyles = false;
            this.dgv_receta.Location = new System.Drawing.Point(19, 28);
            this.dgv_receta.MultiSelect = false;
            this.dgv_receta.Name = "dgv_receta";
            this.dgv_receta.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_receta.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_receta.RowHeadersVisible = false;
            this.dgv_receta.RowHeadersWidth = 51;
            this.dgv_receta.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_receta.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_receta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_receta.Size = new System.Drawing.Size(988, 379);
            this.dgv_receta.TabIndex = 77;
            this.dgv_receta.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_receta_CellClick);
            this.dgv_receta.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_receta_CellContentClick);
            this.dgv_receta.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_receta_CellDoubleClick);
            this.dgv_receta.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_receta_CellMouseDoubleClick);
            this.dgv_receta.SelectionChanged += new System.EventHandler(this.dgv_receta_SelectionChanged);
            this.dgv_receta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_receta_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(73, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 17);
            this.label6.TabIndex = 73;
            this.label6.Text = "Buscar Receta: ";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1037, 485);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "INGRESAR RECETA";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnBuscarMercaderia);
            this.groupBox2.Controls.Add(this.txtDescripcion);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtIdMedida);
            this.groupBox2.Controls.Add(this.txtIdMateria);
            this.groupBox2.Controls.Add(this.txtIdMercaderia);
            this.groupBox2.Controls.Add(this.txtMercaderia);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1025, 479);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.button1.Image = global::ASG.Properties.Resources.bill;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(803, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(140, 33);
            this.button1.TabIndex = 298;
            this.button1.Text = "Duplicar Receta";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.button1, "Buscar Materia Pirma (Ctrl +B)");
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_4);
            // 
            // btnBuscarMercaderia
            // 
            this.btnBuscarMercaderia.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscarMercaderia.FlatAppearance.BorderSize = 0;
            this.btnBuscarMercaderia.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarMercaderia.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBuscarMercaderia.Image = global::ASG.Properties.Resources.research;
            this.btnBuscarMercaderia.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarMercaderia.Location = new System.Drawing.Point(803, 55);
            this.btnBuscarMercaderia.Name = "btnBuscarMercaderia";
            this.btnBuscarMercaderia.Size = new System.Drawing.Size(140, 33);
            this.btnBuscarMercaderia.TabIndex = 297;
            this.btnBuscarMercaderia.Text = "Buscar Mercaderia";
            this.btnBuscarMercaderia.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnBuscarMercaderia, "Buscar Materia Pirma (Ctrl +B)");
            this.btnBuscarMercaderia.UseVisualStyleBackColor = false;
            this.btnBuscarMercaderia.Click += new System.EventHandler(this.button22_Click);
            // 
            // txtDescripcion
            // 
            this.txtDescripcion.Location = new System.Drawing.Point(300, 105);
            this.txtDescripcion.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(480, 23);
            this.txtDescripcion.TabIndex = 121;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(201, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 120;
            this.label2.Text = "Descripción:";
            // 
            // txtIdMedida
            // 
            this.txtIdMedida.BackColor = System.Drawing.Color.White;
            this.txtIdMedida.Location = new System.Drawing.Point(258, 448);
            this.txtIdMedida.MaxLength = 10;
            this.txtIdMedida.Name = "txtIdMedida";
            this.txtIdMedida.Size = new System.Drawing.Size(86, 23);
            this.txtIdMedida.TabIndex = 119;
            this.txtIdMedida.Visible = false;
            // 
            // txtIdMateria
            // 
            this.txtIdMateria.BackColor = System.Drawing.Color.White;
            this.txtIdMateria.Location = new System.Drawing.Point(155, 448);
            this.txtIdMateria.MaxLength = 10;
            this.txtIdMateria.Name = "txtIdMateria";
            this.txtIdMateria.Size = new System.Drawing.Size(86, 23);
            this.txtIdMateria.TabIndex = 118;
            this.txtIdMateria.Visible = false;
            // 
            // txtIdMercaderia
            // 
            this.txtIdMercaderia.BackColor = System.Drawing.Color.White;
            this.txtIdMercaderia.Location = new System.Drawing.Point(54, 448);
            this.txtIdMercaderia.MaxLength = 10;
            this.txtIdMercaderia.Name = "txtIdMercaderia";
            this.txtIdMercaderia.Size = new System.Drawing.Size(86, 23);
            this.txtIdMercaderia.TabIndex = 117;
            this.txtIdMercaderia.Visible = false;
            // 
            // txtMercaderia
            // 
            this.txtMercaderia.Location = new System.Drawing.Point(300, 61);
            this.txtMercaderia.Margin = new System.Windows.Forms.Padding(2);
            this.txtMercaderia.Name = "txtMercaderia";
            this.txtMercaderia.ReadOnly = true;
            this.txtMercaderia.Size = new System.Drawing.Size(480, 23);
            this.txtMercaderia.TabIndex = 113;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.LightGray;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Image = global::ASG.Properties.Resources.arrow;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(926, 441);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(81, 34);
            this.button5.TabIndex = 104;
            this.button5.Text = "Cancelar";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.LightGray;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::ASG.Properties.Resources.floppy_disk_;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(803, 441);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 34);
            this.button2.TabIndex = 66;
            this.button2.Text = "Guardar Receta";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.btnSearchMateria);
            this.groupBox4.Controls.Add(this.txtMedida);
            this.groupBox4.Controls.Add(this.txtMateria);
            this.groupBox4.Controls.Add(this.dgv_materia);
            this.groupBox4.Controls.Add(this.btnBorrar);
            this.groupBox4.Controls.Add(this.btnAgregar);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.txtCantidad);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox4.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(19, 137);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(984, 297);
            this.groupBox4.TabIndex = 107;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Detalle de Receta";
            // 
            // btnSearchMateria
            // 
            this.btnSearchMateria.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchMateria.FlatAppearance.BorderSize = 0;
            this.btnSearchMateria.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchMateria.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSearchMateria.Image = global::ASG.Properties.Resources.research;
            this.btnSearchMateria.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchMateria.Location = new System.Drawing.Point(620, 32);
            this.btnSearchMateria.Name = "btnSearchMateria";
            this.btnSearchMateria.Size = new System.Drawing.Size(154, 33);
            this.btnSearchMateria.TabIndex = 298;
            this.btnSearchMateria.Text = "Buscar Materia Prima";
            this.btnSearchMateria.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnSearchMateria, "Buscar Materia Pirma (Ctrl +B)");
            this.btnSearchMateria.UseVisualStyleBackColor = false;
            this.btnSearchMateria.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // txtMedida
            // 
            this.txtMedida.Location = new System.Drawing.Point(178, 85);
            this.txtMedida.Margin = new System.Windows.Forms.Padding(2);
            this.txtMedida.Name = "txtMedida";
            this.txtMedida.ReadOnly = true;
            this.txtMedida.Size = new System.Drawing.Size(158, 25);
            this.txtMedida.TabIndex = 125;
            // 
            // txtMateria
            // 
            this.txtMateria.Location = new System.Drawing.Point(178, 37);
            this.txtMateria.Margin = new System.Windows.Forms.Padding(2);
            this.txtMateria.Name = "txtMateria";
            this.txtMateria.ReadOnly = true;
            this.txtMateria.Size = new System.Drawing.Size(425, 25);
            this.txtMateria.TabIndex = 124;
            // 
            // dgv_materia
            // 
            this.dgv_materia.AllowUserToAddRows = false;
            this.dgv_materia.AllowUserToDeleteRows = false;
            this.dgv_materia.AllowUserToResizeColumns = false;
            this.dgv_materia.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightGray;
            this.dgv_materia.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_materia.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_materia.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgv_materia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_materia.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_materia.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(174)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_materia.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_materia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_materia.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column10,
            this.Column11,
            this.Column12,
            this.id_materia,
            this.materia});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(82)))), ((int)(((byte)(120)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_materia.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_materia.EnableHeadersVisualStyles = false;
            this.dgv_materia.Location = new System.Drawing.Point(17, 123);
            this.dgv_materia.MultiSelect = false;
            this.dgv_materia.Name = "dgv_materia";
            this.dgv_materia.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_materia.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgv_materia.RowHeadersVisible = false;
            this.dgv_materia.RowHeadersWidth = 51;
            this.dgv_materia.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_materia.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_materia.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_materia.Size = new System.Drawing.Size(943, 165);
            this.dgv_materia.TabIndex = 123;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "CANTIDAD";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "ID UNIDAD";
            this.Column11.MinimumWidth = 6;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.Visible = false;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "UNIDAD MEDIDA";
            this.Column12.MinimumWidth = 6;
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            // 
            // id_materia
            // 
            this.id_materia.HeaderText = "ID MATERIA";
            this.id_materia.MinimumWidth = 6;
            this.id_materia.Name = "id_materia";
            this.id_materia.ReadOnly = true;
            this.id_materia.Visible = false;
            // 
            // materia
            // 
            this.materia.HeaderText = "MATERIA PRIMA";
            this.materia.MinimumWidth = 6;
            this.materia.Name = "materia";
            this.materia.ReadOnly = true;
            // 
            // btnBorrar
            // 
            this.btnBorrar.BackColor = System.Drawing.Color.LightGray;
            this.btnBorrar.FlatAppearance.BorderSize = 0;
            this.btnBorrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBorrar.Image = global::ASG.Properties.Resources.multiply__1_;
            this.btnBorrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBorrar.Location = new System.Drawing.Point(884, 81);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(76, 34);
            this.btnBorrar.TabIndex = 122;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBorrar.UseVisualStyleBackColor = false;
            this.btnBorrar.Click += new System.EventHandler(this.button11_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.LightGray;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.Image = global::ASG.Properties.Resources.add__1_;
            this.btnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregar.Location = new System.Drawing.Point(789, 81);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(89, 34);
            this.btnAgregar.TabIndex = 113;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.button9_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.Black;
            this.label21.Location = new System.Drawing.Point(61, 85);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(103, 17);
            this.label21.TabIndex = 120;
            this.label21.Text = "Unidad Medida:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(369, 85);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 17);
            this.label9.TabIndex = 117;
            this.label9.Text = "Cantidad:";
            // 
            // txtCantidad
            // 
            this.txtCantidad.BackColor = System.Drawing.Color.White;
            this.txtCantidad.Location = new System.Drawing.Point(445, 85);
            this.txtCantidad.MaxLength = 10;
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(158, 25);
            this.txtCantidad.TabIndex = 116;
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_cantidad_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(69, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 17);
            this.label7.TabIndex = 114;
            this.label7.Text = "Materia Prima:";
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.Black;
            this.label24.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label24.Location = new System.Drawing.Point(14, 20);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(226, 26);
            this.label24.TabIndex = 39;
            this.label24.Text = "Ingrese Nueva Receta";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(201, 63);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 17);
            this.label8.TabIndex = 67;
            this.label8.Text = "Mercaderia:";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.ImageIndex = 0;
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1037, 485);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "EDITAR RECETA";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBuscarReceta);
            this.groupBox3.Controls.Add(this.txtDescripRecta);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtMeId);
            this.groupBox3.Controls.Add(this.txtMaId);
            this.groupBox3.Controls.Add(this.txtIdReceta);
            this.groupBox3.Controls.Add(this.txtMercReceta);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.button7);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(7, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1025, 479);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            // 
            // btnBuscarReceta
            // 
            this.btnBuscarReceta.BackColor = System.Drawing.Color.Transparent;
            this.btnBuscarReceta.FlatAppearance.BorderSize = 0;
            this.btnBuscarReceta.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarReceta.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnBuscarReceta.Image = global::ASG.Properties.Resources.research;
            this.btnBuscarReceta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBuscarReceta.Location = new System.Drawing.Point(803, 55);
            this.btnBuscarReceta.Name = "btnBuscarReceta";
            this.btnBuscarReceta.Size = new System.Drawing.Size(117, 33);
            this.btnBuscarReceta.TabIndex = 298;
            this.btnBuscarReceta.Text = "Buscar Receta";
            this.btnBuscarReceta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnBuscarReceta, "Buscar Materia Pirma (Ctrl +B)");
            this.btnBuscarReceta.UseVisualStyleBackColor = false;
            this.btnBuscarReceta.Click += new System.EventHandler(this.button1_Click_3);
            // 
            // txtDescripRecta
            // 
            this.txtDescripRecta.Location = new System.Drawing.Point(300, 105);
            this.txtDescripRecta.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescripRecta.Name = "txtDescripRecta";
            this.txtDescripRecta.Size = new System.Drawing.Size(480, 23);
            this.txtDescripRecta.TabIndex = 121;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(201, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 120;
            this.label3.Text = "Descripción:";
            // 
            // txtMeId
            // 
            this.txtMeId.BackColor = System.Drawing.Color.White;
            this.txtMeId.Location = new System.Drawing.Point(258, 448);
            this.txtMeId.MaxLength = 10;
            this.txtMeId.Name = "txtMeId";
            this.txtMeId.Size = new System.Drawing.Size(86, 23);
            this.txtMeId.TabIndex = 119;
            this.txtMeId.Visible = false;
            // 
            // txtMaId
            // 
            this.txtMaId.BackColor = System.Drawing.Color.White;
            this.txtMaId.Location = new System.Drawing.Point(155, 448);
            this.txtMaId.MaxLength = 10;
            this.txtMaId.Name = "txtMaId";
            this.txtMaId.Size = new System.Drawing.Size(86, 23);
            this.txtMaId.TabIndex = 118;
            this.txtMaId.Visible = false;
            // 
            // txtIdReceta
            // 
            this.txtIdReceta.BackColor = System.Drawing.Color.White;
            this.txtIdReceta.Location = new System.Drawing.Point(54, 448);
            this.txtIdReceta.MaxLength = 10;
            this.txtIdReceta.Name = "txtIdReceta";
            this.txtIdReceta.Size = new System.Drawing.Size(86, 23);
            this.txtIdReceta.TabIndex = 117;
            this.txtIdReceta.Visible = false;
            // 
            // txtMercReceta
            // 
            this.txtMercReceta.Location = new System.Drawing.Point(300, 61);
            this.txtMercReceta.Margin = new System.Windows.Forms.Padding(2);
            this.txtMercReceta.Name = "txtMercReceta";
            this.txtMercReceta.ReadOnly = true;
            this.txtMercReceta.Size = new System.Drawing.Size(480, 23);
            this.txtMercReceta.TabIndex = 113;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.LightGray;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Image = global::ASG.Properties.Resources.arrow;
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(926, 441);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(81, 34);
            this.button6.TabIndex = 104;
            this.button6.Text = "Cancelar";
            this.button6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button6.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.LightGray;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Image = global::ASG.Properties.Resources.floppy_disk_;
            this.button7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button7.Location = new System.Drawing.Point(803, 441);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(117, 34);
            this.button7.TabIndex = 66;
            this.button7.Text = "Guardar Receta";
            this.button7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.Transparent;
            this.groupBox5.Controls.Add(this.btnSearchMat);
            this.groupBox5.Controls.Add(this.txtMunidad);
            this.groupBox5.Controls.Add(this.txtMprima);
            this.groupBox5.Controls.Add(this.dgv_detalleReceta);
            this.groupBox5.Controls.Add(this.button9);
            this.groupBox5.Controls.Add(this.button10);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txtMcantidad);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(19, 140);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(984, 295);
            this.groupBox5.TabIndex = 107;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "receta detalle";
            this.groupBox5.Enter += new System.EventHandler(this.groupBox5_Enter);
            // 
            // btnSearchMat
            // 
            this.btnSearchMat.BackColor = System.Drawing.Color.Transparent;
            this.btnSearchMat.FlatAppearance.BorderSize = 0;
            this.btnSearchMat.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchMat.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnSearchMat.Image = global::ASG.Properties.Resources.research;
            this.btnSearchMat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearchMat.Location = new System.Drawing.Point(606, 26);
            this.btnSearchMat.Name = "btnSearchMat";
            this.btnSearchMat.Size = new System.Drawing.Size(154, 33);
            this.btnSearchMat.TabIndex = 299;
            this.btnSearchMat.Text = "Buscar Materia Prima";
            this.btnSearchMat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.btnSearchMat, "Buscar Materia Pirma (Ctrl +B)");
            this.btnSearchMat.UseVisualStyleBackColor = false;
            this.btnSearchMat.Click += new System.EventHandler(this.button3_Click_2);
            // 
            // txtMunidad
            // 
            this.txtMunidad.Location = new System.Drawing.Point(165, 81);
            this.txtMunidad.Margin = new System.Windows.Forms.Padding(2);
            this.txtMunidad.Name = "txtMunidad";
            this.txtMunidad.ReadOnly = true;
            this.txtMunidad.Size = new System.Drawing.Size(158, 25);
            this.txtMunidad.TabIndex = 125;
            // 
            // txtMprima
            // 
            this.txtMprima.Location = new System.Drawing.Point(165, 33);
            this.txtMprima.Margin = new System.Windows.Forms.Padding(2);
            this.txtMprima.Name = "txtMprima";
            this.txtMprima.ReadOnly = true;
            this.txtMprima.Size = new System.Drawing.Size(425, 25);
            this.txtMprima.TabIndex = 124;
            // 
            // dgv_detalleReceta
            // 
            this.dgv_detalleReceta.AllowUserToAddRows = false;
            this.dgv_detalleReceta.AllowUserToDeleteRows = false;
            this.dgv_detalleReceta.AllowUserToResizeColumns = false;
            this.dgv_detalleReceta.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.LightGray;
            this.dgv_detalleReceta.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgv_detalleReceta.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_detalleReceta.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dgv_detalleReceta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgv_detalleReceta.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgv_detalleReceta.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(174)))), ((int)(((byte)(48)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_detalleReceta.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgv_detalleReceta.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_detalleReceta.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(82)))), ((int)(((byte)(120)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_detalleReceta.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgv_detalleReceta.EnableHeadersVisualStyles = false;
            this.dgv_detalleReceta.Location = new System.Drawing.Point(17, 126);
            this.dgv_detalleReceta.MultiSelect = false;
            this.dgv_detalleReceta.Name = "dgv_detalleReceta";
            this.dgv_detalleReceta.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_detalleReceta.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgv_detalleReceta.RowHeadersVisible = false;
            this.dgv_detalleReceta.RowHeadersWidth = 51;
            this.dgv_detalleReceta.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_detalleReceta.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_detalleReceta.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_detalleReceta.Size = new System.Drawing.Size(943, 162);
            this.dgv_detalleReceta.TabIndex = 123;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "CANTIDAD";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "ID UNIDAD";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "UNIDAD MEDIDA";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "ID MATERIA";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "MATERIA PRIMA";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // button9
            // 
            this.button9.BackColor = System.Drawing.Color.LightGray;
            this.button9.FlatAppearance.BorderSize = 0;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button9.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button9.Image = global::ASG.Properties.Resources.multiply__1_;
            this.button9.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button9.Location = new System.Drawing.Point(884, 81);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(76, 34);
            this.button9.TabIndex = 122;
            this.button9.Text = "Borrar";
            this.button9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button9_Click_1);
            // 
            // button10
            // 
            this.button10.BackColor = System.Drawing.Color.LightGray;
            this.button10.FlatAppearance.BorderSize = 0;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button10.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button10.Image = global::ASG.Properties.Resources.add__1_;
            this.button10.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button10.Location = new System.Drawing.Point(789, 81);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(89, 34);
            this.button10.TabIndex = 113;
            this.button10.Text = "Agregar";
            this.button10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(49, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 120;
            this.label5.Text = "Unidad Medida:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(356, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 17);
            this.label10.TabIndex = 117;
            this.label10.Text = "Cantidad:";
            // 
            // txtMcantidad
            // 
            this.txtMcantidad.BackColor = System.Drawing.Color.White;
            this.txtMcantidad.Location = new System.Drawing.Point(432, 81);
            this.txtMcantidad.MaxLength = 10;
            this.txtMcantidad.Name = "txtMcantidad";
            this.txtMcantidad.Size = new System.Drawing.Size(158, 25);
            this.txtMcantidad.TabIndex = 116;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(56, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 17);
            this.label11.TabIndex = 114;
            this.label11.Text = "Materia Prima:";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label12.Location = new System.Drawing.Point(14, 20);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(124, 26);
            this.label12.TabIndex = 39;
            this.label12.Text = "Editar Receta";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(201, 63);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 17);
            this.label13.TabIndex = 67;
            this.label13.Text = "Mercaderia:";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "clipboard (1).png");
            this.imageList1.Images.SetKeyName(1, "clipboard (2).png");
            this.imageList1.Images.SetKeyName(2, "clipboard (3).png");
            this.imageList1.Images.SetKeyName(3, "settings.png");
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::ASG.Properties.Resources.substract__2_;
            this.pictureBox4.Location = new System.Drawing.Point(1015, 14);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(14, 16);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 31;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::ASG.Properties.Resources.cancel__3_;
            this.pictureBox3.Location = new System.Drawing.Point(1041, 14);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(14, 16);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 30;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Image = global::ASG.Properties.Resources.bullet_list;
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(24, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(208, 38);
            this.label4.TabIndex = 29;
            this.label4.Text = "Gestión Recetas";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // frm_recetas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(47)))), ((int)(((byte)(63)))));
            this.ClientSize = new System.Drawing.Size(1074, 621);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frm_recetas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_recetas";
            this.Load += new System.EventHandler(this.frm_recetas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_recetas_KeyDown);
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_receta)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_materia)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_detalleReceta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_usuario;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgv_receta;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgv_materia;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtMercaderia;
        private System.Windows.Forms.TextBox txtMateria;
        private System.Windows.Forms.TextBox txtIdMedida;
        private System.Windows.Forms.TextBox txtIdMateria;
        private System.Windows.Forms.TextBox txtIdMercaderia;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMedida;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_materia;
        private System.Windows.Forms.DataGridViewTextBoxColumn materia;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtDescripRecta;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMeId;
        private System.Windows.Forms.TextBox txtMaId;
        private System.Windows.Forms.TextBox txtIdReceta;
        private System.Windows.Forms.TextBox txtMercReceta;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtMunidad;
        private System.Windows.Forms.TextBox txtMprima;
        private System.Windows.Forms.DataGridView dgv_detalleReceta;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtMcantidad;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.Button btnBuscarMercaderia;
        private System.Windows.Forms.Button btnSearchMateria;
        private System.Windows.Forms.Button btnBuscarReceta;
        private System.Windows.Forms.Button btnSearchMat;
        private System.Windows.Forms.Button button1;
    }
}