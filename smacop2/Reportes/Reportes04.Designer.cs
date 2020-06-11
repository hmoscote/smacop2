namespace smacop2.Reportes
{
    partial class Reportes04
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reportes04));
            this.vistaviajesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.logicopDataSet = new smacop2.logicopDataSet();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.vista_viajesTableAdapter = new smacop2.logicopDataSetTableAdapters.vista_viajesTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.vistaviajesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicopDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // vistaviajesBindingSource
            // 
            this.vistaviajesBindingSource.DataMember = "vista_viajes";
            this.vistaviajesBindingSource.DataSource = this.logicopDataSet;
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
            reportDataSource1.Name = "dsproduccion";
            reportDataSource1.Value = this.vistaviajesBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000015.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(788, 545);
            this.reportViewer1.TabIndex = 0;
            // 
            // vista_viajesTableAdapter
            // 
            this.vista_viajesTableAdapter.ClearBeforeFill = true;
            // 
            // Reportes04
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 545);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Reportes04";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REPORTE";
            this.Load += new System.EventHandler(this.Reportes04_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vistaviajesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicopDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private logicopDataSet logicopDataSet;
        private System.Windows.Forms.BindingSource vistaviajesBindingSource;
        private logicopDataSetTableAdapters.vista_viajesTableAdapter vista_viajesTableAdapter;
    }
}