namespace AllureViewerPortable
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Button btnVisualizar;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lblEstado;
        private System.Windows.Forms.Button btnGuardarLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtZip = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.btnVisualizar = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();
            this.btnGuardarLog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtZip
            // 
            this.txtZip.Location = new System.Drawing.Point(12, 12);
            this.txtZip.Name = "txtZip";
            this.txtZip.ReadOnly = true;
            this.txtZip.Size = new System.Drawing.Size(420, 23);
            this.txtZip.TabIndex = 0;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(438, 12);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(90, 23);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Seleccionar...";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // btnVisualizar
            // 
            this.btnVisualizar.Location = new System.Drawing.Point(12, 50);
            this.btnVisualizar.Name = "btnVisualizar";
            this.btnVisualizar.Size = new System.Drawing.Size(140, 30);
            this.btnVisualizar.TabIndex = 2;
            this.btnVisualizar.Text = "Visualizar Reporte";
            this.btnVisualizar.UseVisualStyleBackColor = true;
            this.btnVisualizar.Click += new System.EventHandler(this.btnVisualizar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(438, 50);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(90, 30);
            this.btnCerrar.TabIndex = 3;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblEstado
            // 
            this.lblEstado.AutoEllipsis = true;
            this.lblEstado.Location = new System.Drawing.Point(12, 93);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Size = new System.Drawing.Size(516, 40);
            this.lblEstado.TabIndex = 4;
            this.lblEstado.Text = "Estado: listo.";
            // 
            // btnGuardarLog
            // 
            this.btnGuardarLog.Location = new System.Drawing.Point(170, 50);
            this.btnGuardarLog.Name = "btnGuardarLog";
            this.btnGuardarLog.Size = new System.Drawing.Size(120, 30);
            this.btnGuardarLog.TabIndex = 5;
            this.btnGuardarLog.Text = "Guardar log...";
            this.btnGuardarLog.UseVisualStyleBackColor = true;
            this.btnGuardarLog.Enabled = false;
            this.btnGuardarLog.Click += new System.EventHandler(this.btnGuardarLog_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(540, 145);
            this.Controls.Add(this.btnGuardarLog);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnVisualizar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtZip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Allure Viewer Portable";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
