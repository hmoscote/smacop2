namespace smacop2.Reportes
{
    partial class Reportes02
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Reportes02));
            this.lstentradasmatBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.logicopDataSet = new smacop2.logicopDataSet();
            this.lstsalidasmatBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.lstsalidasmatTableAdapter = new smacop2.logicopDataSetTableAdapters.lstsalidasmatTableAdapter();
            this.lstentradasmatTableAdapter = new smacop2.logicopDataSetTableAdapters.lstentradasmatTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.lstentradasmatBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicopDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstsalidasmatBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // lstentradasmatBindingSource
            // 
            this.lstentradasmatBindingSource.DataMember = "lstentradasmat";
            this.lstentradasmatBindingSource.DataSource = this.logicopDataSet;
            // 
            // logicopDataSet
            // 
            this.logicopDataSet.DataSetName = "logicopDataSet";
            this.logicopDataSet.Locale = new System.Globalization.CultureInfo("");
            this.logicopDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lstsalidasmatBindingSource
            // 
            this.lstsalidasmatBindingSource.DataMember = "lstsalidasmat";
            this.lstsalidasmatBindingSource.DataSource = this.logicopDataSet;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsentradas";
            reportDataSource1.Value = this.lstentradasmatBindingSource;
            reportDataSource2.Name = "dssalidas";
            reportDataSource2.Value = this.lstsalidasmatBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "smacop2.Reportes.R0000007.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(880, 626);
            this.reportViewer1.TabIndex = 1;
            // 
            // lstsalidasmatTableAdapter
            // 
            this.lstsalidasmatTableAdapter.ClearBeforeFill = true;
            // 
            // lstentradasmatTableAdapter
            // 
            this.lstentradasmatTableAdapter.ClearBeforeFill = true;
            // 
            // Reportes02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 626);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Reportes02";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "REPORTE";
            this.Load += new System.EventHandler(this.Reportes02_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lstentradasmatBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicopDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstsalidasmatBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private logicopDataSet logicopDataSet;
        private System.Windows.Forms.BindingSource lstsalidasmatBindingSource;
        private logicopDataSetTableAdapters.lstsalidasmatTableAdapter lstsalidasmatTableAdapter;
        private System.Windows.Forms.BindingSource lstentradasmatBindingSource;
        private logicopDataSetTableAdapters.lstentradasmatTableAdapter lstentradasmatTableAdapter;
    }
}