namespace visit_tracker
{
    partial class frm_Visit
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAddVisit = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDate = new System.Windows.Forms.Button();
            this.cbxIdClient = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNameClient = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.listVisits = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtResponsible = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.btnCadUser = new System.Windows.Forms.Button();
            this.btnCadLogin = new System.Windows.Forms.Button();
            this.btnCadClient = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.listProp = new System.Windows.Forms.ListView();
            this.btnNewProp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnCadProduct = new System.Windows.Forms.Button();
            this.dtpDateTime = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddVisit
            // 
            this.btnAddVisit.Location = new System.Drawing.Point(122, 633);
            this.btnAddVisit.Name = "btnAddVisit";
            this.btnAddVisit.Size = new System.Drawing.Size(90, 30);
            this.btnAddVisit.TabIndex = 11;
            this.btnAddVisit.Text = "Novo";
            this.btnAddVisit.UseVisualStyleBackColor = true;
            this.btnAddVisit.Click += new System.EventHandler(this.btnAddVisit_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(6, 277);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(482, 228);
            this.txtDescription.TabIndex = 10;
            this.txtDescription.TextChanged += new System.EventHandler(this.txtDescription_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 257);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "Descrição:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDate);
            this.groupBox1.Controls.Add(this.cbxIdClient);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNameClient);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(494, 74);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Clientes:";
            // 
            // btnDate
            // 
            this.btnDate.Location = new System.Drawing.Point(1019, 347);
            this.btnDate.Name = "btnDate";
            this.btnDate.Size = new System.Drawing.Size(23, 23);
            this.btnDate.TabIndex = 23;
            this.btnDate.UseVisualStyleBackColor = true;
            this.btnDate.Click += new System.EventHandler(this.btnDate_Click);
            // 
            // cbxIdClient
            // 
            this.cbxIdClient.FormattingEnabled = true;
            this.cbxIdClient.Location = new System.Drawing.Point(6, 39);
            this.cbxIdClient.Name = "cbxIdClient";
            this.cbxIdClient.Size = new System.Drawing.Size(83, 25);
            this.cbxIdClient.TabIndex = 16;
            this.cbxIdClient.SelectedIndexChanged += new System.EventHandler(this.cbxIdClient_SelectedIndexChanged);
            this.cbxIdClient.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxIdClient_KeyDown);
            this.cbxIdClient.Leave += new System.EventHandler(this.cbxIdClient_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(92, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Cliente:";
            // 
            // txtNameClient
            // 
            this.txtNameClient.Location = new System.Drawing.Point(95, 39);
            this.txtNameClient.Name = "txtNameClient";
            this.txtNameClient.Size = new System.Drawing.Size(393, 23);
            this.txtNameClient.TabIndex = 4;
            this.txtNameClient.TextChanged += new System.EventHandler(this.txtNameClient_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Id:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 211);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Titulo:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(6, 231);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(482, 23);
            this.txtTitle.TabIndex = 17;
            this.txtTitle.TextChanged += new System.EventHandler(this.txtTitle_TextChanged);
            // 
            // listVisits
            // 
            this.listVisits.FormattingEnabled = true;
            this.listVisits.ItemHeight = 17;
            this.listVisits.Location = new System.Drawing.Point(6, 68);
            this.listVisits.Name = "listVisits";
            this.listVisits.Size = new System.Drawing.Size(482, 140);
            this.listVisits.TabIndex = 0;
            this.listVisits.SelectedIndexChanged += new System.EventHandler(this.listVisits_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(90, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Data Visita:";
            // 
            // txtResponsible
            // 
            this.txtResponsible.Location = new System.Drawing.Point(216, 39);
            this.txtResponsible.Name = "txtResponsible";
            this.txtResponsible.Size = new System.Drawing.Size(272, 23);
            this.txtResponsible.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(213, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Responsável:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(6, 39);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(81, 23);
            this.txtId.TabIndex = 1;
            this.txtId.TextChanged += new System.EventHandler(this.txtId_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Id Visita:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(410, 633);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(90, 30);
            this.btnClear.TabIndex = 21;
            this.btnClear.Text = "Limpar";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(314, 633);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 30);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Text = "Excluir";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(218, 633);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(90, 30);
            this.btnEdit.TabIndex = 19;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(857, 633);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(90, 30);
            this.BtnExit.TabIndex = 18;
            this.BtnExit.Text = "Sair";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // btnCadUser
            // 
            this.btnCadUser.Location = new System.Drawing.Point(857, 96);
            this.btnCadUser.Name = "btnCadUser";
            this.btnCadUser.Size = new System.Drawing.Size(90, 43);
            this.btnCadUser.TabIndex = 25;
            this.btnCadUser.Text = "Usuários";
            this.btnCadUser.UseVisualStyleBackColor = true;
            this.btnCadUser.Click += new System.EventHandler(this.btnCadUser_Click);
            // 
            // btnCadLogin
            // 
            this.btnCadLogin.Location = new System.Drawing.Point(857, 47);
            this.btnCadLogin.Name = "btnCadLogin";
            this.btnCadLogin.Size = new System.Drawing.Size(90, 43);
            this.btnCadLogin.TabIndex = 24;
            this.btnCadLogin.Text = "Login";
            this.btnCadLogin.UseVisualStyleBackColor = true;
            this.btnCadLogin.Click += new System.EventHandler(this.btnCadLogin_Click);
            // 
            // btnCadClient
            // 
            this.btnCadClient.Location = new System.Drawing.Point(857, 145);
            this.btnCadClient.Name = "btnCadClient";
            this.btnCadClient.Size = new System.Drawing.Size(90, 43);
            this.btnCadClient.TabIndex = 23;
            this.btnCadClient.Text = "Clientes";
            this.btnCadClient.UseVisualStyleBackColor = true;
            this.btnCadClient.Click += new System.EventHandler(this.btnCadClient_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Margin = new System.Windows.Forms.Padding(3, 0, 9, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(935, 24);
            this.label9.TabIndex = 26;
            this.label9.Text = "label9";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listProp
            // 
            this.listProp.HideSelection = false;
            this.listProp.Location = new System.Drawing.Point(9, 22);
            this.listProp.Name = "listProp";
            this.listProp.Size = new System.Drawing.Size(324, 266);
            this.listProp.TabIndex = 27;
            this.listProp.UseCompatibleStateImageBehavior = false;
            // 
            // btnNewProp
            // 
            this.btnNewProp.Location = new System.Drawing.Point(243, 294);
            this.btnNewProp.Name = "btnNewProp";
            this.btnNewProp.Size = new System.Drawing.Size(90, 30);
            this.btnNewProp.TabIndex = 24;
            this.btnNewProp.Text = "Nova";
            this.btnNewProp.UseVisualStyleBackColor = true;
            this.btnNewProp.Click += new System.EventHandler(this.btnNewProp_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtpDateTime);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtResponsible);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtTitle);
            this.groupBox2.Controls.Add(this.listVisits);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtId);
            this.groupBox2.Location = new System.Drawing.Point(12, 116);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(494, 511);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Visitas:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listProp);
            this.groupBox3.Controls.Add(this.btnNewProp);
            this.groupBox3.Location = new System.Drawing.Point(512, 36);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(339, 591);
            this.groupBox3.TabIndex = 30;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Proposta e Orçamentos:";
            // 
            // btnCadProduct
            // 
            this.btnCadProduct.Location = new System.Drawing.Point(857, 194);
            this.btnCadProduct.Name = "btnCadProduct";
            this.btnCadProduct.Size = new System.Drawing.Size(90, 43);
            this.btnCadProduct.TabIndex = 31;
            this.btnCadProduct.Text = "Produtos";
            this.btnCadProduct.UseVisualStyleBackColor = true;
            this.btnCadProduct.Click += new System.EventHandler(this.btnCadProduct_Click);
            // 
            // dtpDateTime
            // 
            this.dtpDateTime.Location = new System.Drawing.Point(93, 39);
            this.dtpDateTime.Name = "dtpDateTime";
            this.dtpDateTime.Size = new System.Drawing.Size(117, 23);
            this.dtpDateTime.TabIndex = 29;
            // 
            // frm_Visit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 675);
            this.Controls.Add(this.btnCadProduct);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCadUser);
            this.Controls.Add(this.btnCadClient);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.btnCadLogin);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnAddVisit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_Visit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visitas";
            this.Load += new System.EventHandler(this.frm_Visit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnAddVisit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtResponsible;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNameClient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxIdClient;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox listVisits;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnDate;
        private System.Windows.Forms.Button btnCadUser;
        private System.Windows.Forms.Button btnCadLogin;
        private System.Windows.Forms.Button btnCadClient;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ListView listProp;
        private System.Windows.Forms.Button btnNewProp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCadProduct;
        private System.Windows.Forms.DateTimePicker dtpDateTime;
    }
}