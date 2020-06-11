namespace smacop2.Reportes
{
    partial class Reportes05
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reportes05));
            this.redimientoporequipoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.logicopDataSet = new smacop2.logicopDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.redimiento_por_equipoTableAdapter = new smacop2.logicopDataSetTableAdapters.redimiento_por_equipoTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.redimientoporequipoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicopDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // redimientoporequipoBindingSource
            // 
            this.redimientoporequipoBindingSource.DataMember = "redimiento_por_equipo";
            this.redimientoporequipoBindingSource.DataSource = this.logicopDataSet;
            // 
            // logicopDataSet
            // 
            this.logicopDataSet.DataSetName = "logicopDataSet";
            this.logicopDataSet.Locale = new System.Globalization.CultureInfo("");
            this.logicopDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsprod";
            reportDataSource1.Value = this.redimientoporequipoBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000017.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(849, 479);
            this.reportViewer1.TabIndex = 0;
            // 
            // redimiento_por_equipoTableAdapter
            // 
            this.redimiento_por_equipoTableAdapter.ClearBeforeFill = true;
            // 
            // Reportes05
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 479);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Reportes05";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REPORTE";
            this.Load += new System.EventHandler(this.Reportes05_Load);
            ((System.ComponentModel.ISupportInitialize)(this.redimientoporequipoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicopDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private logicopDataSet logicopDataSet;
        private System.Windows.Forms.BindingSource redimientoporequipoBindingSource;
        private logicopDataSetTableAdapters.redimiento_por_equipoTableAdapter redimiento_por_equipoTableAdapter;
    }
}