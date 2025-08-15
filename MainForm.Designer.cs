using System.Windows.Forms;

namespace AllureViewerPortable
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtZip;
        private Button btnBuscar;
        private Button btnVisualizar;
        private Button btnCerrar;
        private Label lblEstado;
        private Button btnGuardarLog;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblFirma;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtZip = new TextBox();
            this.btnBuscar = new Button();
            this.btnVisualizar = new Button();
            this.btnCerrar = new Button();
            this.lblEstado = new Label();
            this.btnGuardarLog = new Button();
            this.statusStrip1 = new StatusStrip();
            this.lblFirma = new ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
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
            // statusStrip1
            // 
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.Dock = DockStyle.Bottom;
            this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.lblFirma });
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.TabIndex = 6;
            // 
            // lblFirma
            // 
            this.lblFirma.Name = "lblFirma";
            this.lblFirma.IsLink = true;
            this.lblFirma.LinkBehavior = LinkBehavior.NeverUnderline;
            this.lblFirma.ToolTipText = "Abrir LinkedIn del desarrollador";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(540, 170);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnGuardarLog);
            this.Controls.Add(this.lblEstado);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnVisualizar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtZip);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Allure Viewer Portable";
            this.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
