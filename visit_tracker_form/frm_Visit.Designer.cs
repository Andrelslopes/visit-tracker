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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.listVisists = new System.Windows.Forms.ListBox();
            this.cbxIdClient = new System.Windows.Forms.ComboBox();
            this.txtDateVisit = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtResponsible = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNameClient = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvClient = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.BtnExit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddVisit
            // 
            this.btnAddVisit.Location = new System.Drawing.Point(288, 22);
            this.btnAddVisit.Name = "btnAddVisit";
            this.btnAddVisit.Size = new System.Drawing.Size(75, 23);
            this.btnAddVisit.TabIndex = 11;
            this.btnAddVisit.Text = "Novo";
            this.btnAddVisit.UseVisualStyleBackColor = true;
            this.btnAddVisit.Click += new System.EventHandler(this.btnAddVisit_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(6, 225);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(364, 116);
            this.txtDescription.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 205);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "Descrição:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtTitle);
            this.groupBox1.Controls.Add(this.listVisists);
            this.groupBox1.Controls.Add(this.cbxIdClient);
            this.groupBox1.Controls.Add(this.txtDateVisit);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtResponsible);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtNameClient);
            this.groupBox1.Controls.Add(this.btnAddVisit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtId);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(612, 347);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Visitas:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(531, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Limpar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(450, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Excluir";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(369, 22);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 19;
            this.btnEdit.Text = "Editar";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 18;
            this.label4.Text = "Titulo:";
            // 
            // txtTitle
            // 
            this.txtTitle.Location = new System.Drawing.Point(6, 179);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(364, 23);
            this.txtTitle.TabIndex = 17;
            // 
            // listVisists
            // 
            this.listVisists.FormattingEnabled = true;
            this.listVisists.ItemHeight = 17;
            this.listVisists.Location = new System.Drawing.Point(376, 133);
            this.listVisists.Name = "listVisists";
            this.listVisists.Size = new System.Drawing.Size(230, 208);
            this.listVisists.TabIndex = 0;
            this.listVisists.SelectedIndexChanged += new System.EventHandler(this.listVisists_SelectedIndexChanged);
            // 
            // cbxIdClient
            // 
            this.cbxIdClient.FormattingEnabled = true;
            this.cbxIdClient.Location = new System.Drawing.Point(6, 85);
            this.cbxIdClient.Name = "cbxIdClient";
            this.cbxIdClient.Size = new System.Drawing.Size(73, 25);
            this.cbxIdClient.TabIndex = 16;
            this.cbxIdClient.SelectedIndexChanged += new System.EventHandler(this.cbxIdClient_SelectedIndexChanged);
            // 
            // txtDateVisit
            // 
            this.txtDateVisit.Location = new System.Drawing.Point(264, 133);
            this.txtDateVisit.Name = "txtDateVisit";
            this.txtDateVisit.Size = new System.Drawing.Size(106, 23);
            this.txtDateVisit.TabIndex = 15;
            this.txtDateVisit.TextChanged += new System.EventHandler(this.txtDateVisit_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(261, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Data Visita:";
            // 
            // txtResponsible
            // 
            this.txtResponsible.Location = new System.Drawing.Point(6, 133);
            this.txtResponsible.Name = "txtResponsible";
            this.txtResponsible.Size = new System.Drawing.Size(252, 23);
            this.txtResponsible.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 113);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "Responsável:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Nome Cliente:";
            // 
            // txtNameClient
            // 
            this.txtNameClient.Location = new System.Drawing.Point(85, 85);
            this.txtNameClient.Name = "txtNameClient";
            this.txtNameClient.Size = new System.Drawing.Size(521, 23);
            this.txtNameClient.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Id Cliente:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(6, 39);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(73, 23);
            this.txtId.TabIndex = 1;
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
            // dgvClient
            // 
            this.dgvClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClient.Location = new System.Drawing.Point(6, 22);
            this.dgvClient.Name = "dgvClient";
            this.dgvClient.Size = new System.Drawing.Size(600, 196);
            this.dgvClient.TabIndex = 16;
            this.dgvClient.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClient_CellContentClick);
            this.dgvClient.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClient_CellContentDoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvClient);
            this.groupBox3.Location = new System.Drawing.Point(12, 365);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(612, 224);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Clientes:";
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(982, 595);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 23);
            this.BtnExit.TabIndex = 18;
            this.BtnExit.Text = "Sair";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(630, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 577);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Proposta:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 221);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(415, 350);
            this.textBox1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 201);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Descrição:";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(88, 21);
            this.radioButton1.TabIndex = 20;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Pendente";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 51);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(92, 21);
            this.radioButton2.TabIndex = 21;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Aprovado";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(12, 80);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(88, 21);
            this.radioButton3.TabIndex = 22;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Rejeitado";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.radioButton3);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Location = new System.Drawing.Point(6, 73);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(129, 125);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Status:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 17);
            this.label8.TabIndex = 24;
            this.label8.Text = "Data da Proposta:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(6, 44);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 23);
            this.textBox2.TabIndex = 25;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.textBox6);
            this.groupBox5.Controls.Add(this.textBox3);
            this.groupBox5.Location = new System.Drawing.Point(141, 22);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(280, 176);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Responsável:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(112, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(92, 17);
            this.label11.TabIndex = 31;
            this.label11.Text = "Responsável:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 17);
            this.label9.TabIndex = 30;
            this.label9.Text = "Data:";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(9, 70);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(265, 23);
            this.textBox6.TabIndex = 28;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(9, 39);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 23);
            this.textBox3.TabIndex = 29;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(511, 58);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(85, 21);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "Proposta";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(115, 39);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(159, 25);
            this.comboBox1.TabIndex = 32;
            // 
            // frm_Visit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 630);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_Visit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Visit";
            this.Load += new System.EventHandler(this.frm_Visit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClient)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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
        private System.Windows.Forms.TextBox txtDateVisit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvClient;
        private System.Windows.Forms.ListBox listVisists;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button BtnExit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}