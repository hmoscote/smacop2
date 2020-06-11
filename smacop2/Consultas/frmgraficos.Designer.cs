namespace smacop2.Consultas
{
    partial class frmgraficos
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lstentradasxcanterasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.logicopDataSet1 = new smacop2.logicopDataSet();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lstsalidasxccosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lstprovsalidasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lstentradas_x_canterasTableAdapter1 = new smacop2.logicopDataSetTableAdapters.lstentradas_x_canterasTableAdapter();
            this.lstsalidas_x_ccosTableAdapter = new smacop2.logicopDataSetTableAdapters.lstsalidas_x_ccosTableAdapter();
            this.lstprov_salidasTableAdapter = new smacop2.logicopDataSetTableAdapters.lstprov_salidasTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstentradasxcanterasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicopDataSet1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstsalidasxccosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstprovsalidasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.Transparent;
            this.chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            this.chart1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chart1.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.DarkHorizontal;
            this.chart1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.TopRight;
            this.chart1.BackImageTransparentColor = System.Drawing.Color.Silver;
            this.chart1.BackSecondaryColor = System.Drawing.Color.Gainsboro;
            this.chart1.BorderlineColor = System.Drawing.Color.Transparent;
            this.chart1.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            this.chart1.BorderlineWidth = 0;
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.DataSource = this.lstentradasxcanterasBindingSource;
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 51);
            this.chart1.Name = "chart1";
            this.chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Entradas";
            series1.XValueMember = "mes";
            series1.YValueMembers = "total";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(671, 296);
            this.chart1.TabIndex = 175;
            this.chart1.Text = "chart1";
            // 
            // lstentradasxcanterasBindingSource
            // 
            this.lstentradasxcanterasBindingSource.DataMember = "lstentradas_x_canteras";
            this.lstentradasxcanterasBindingSource.DataSource = this.logicopDataSet1;
            // 
            // logicopDataSet1
            // 
            this.logicopDataSet1.DataSetName = "logicopDataSet";
            this.logicopDataSet1.Locale = new System.Globalization.CultureInfo("");
            this.logicopDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::smacop2.Properties.Resources.bg_boxsmallfoothover;
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(671, 51);
            this.panel1.TabIndex = 176;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 14);
            this.label5.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(560, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Últimos 5 Meses";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(548, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 179;
            // 
            // chart2
            // 
            this.chart2.BackColor = System.Drawing.Color.Transparent;
            this.chart2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            this.chart2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chart2.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.DarkHorizontal;
            this.chart2.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.TopRight;
            this.chart2.BackImageTransparentColor = System.Drawing.Color.Silver;
            this.chart2.BackSecondaryColor = System.Drawing.Color.Gainsboro;
            this.chart2.BorderlineColor = System.Drawing.Color.Transparent;
            this.chart2.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            this.chart2.BorderlineWidth = 0;
            chartArea2.Area3DStyle.Enable3D = true;
            chartArea2.Name = "ChartArea1";
            this.chart2.ChartAreas.Add(chartArea2);
            this.chart2.DataSource = this.lstsalidasxccosBindingSource;
            this.chart2.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart2.Legends.Add(legend2);
            this.chart2.Location = new System.Drawing.Point(0, 0);
            this.chart2.Name = "chart2";
            this.chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Salidas";
            series2.XValueMember = "mes";
            series2.YValueMembers = "total";
            this.chart2.Series.Add(series2);
            this.chart2.Size = new System.Drawing.Size(671, 347);
            this.chart2.TabIndex = 180;
            this.chart2.Text = "chart2";
            // 
            // lstsalidasxccosBindingSource
            // 
            this.lstsalidasxccosBindingSource.DataMember = "lstsalidas_x_ccos";
            this.lstsalidasxccosBindingSource.DataSource = this.logicopDataSet1;
            // 
            // chart3
            // 
            this.chart3.BackColor = System.Drawing.Color.Transparent;
            this.chart3.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.Center;
            this.chart3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chart3.BackHatchStyle = System.Windows.Forms.DataVisualization.Charting.ChartHatchStyle.DarkHorizontal;
            this.chart3.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.TopRight;
            this.chart3.BackImageTransparentColor = System.Drawing.Color.Silver;
            this.chart3.BackSecondaryColor = System.Drawing.Color.Gainsboro;
            this.chart3.BorderlineColor = System.Drawing.Color.Transparent;
            this.chart3.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            this.chart3.BorderlineWidth = 0;
            chartArea3.Area3DStyle.Enable3D = true;
            chartArea3.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea3);
            this.chart3.DataSource = this.lstprovsalidasBindingSource;
            this.chart3.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chart3.Legends.Add(legend3);
            this.chart3.Location = new System.Drawing.Point(0, 0);
            this.chart3.Name = "chart3";
            this.chart3.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Fire;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Salidas";
            series3.XValueMember = "mes";
            series3.YValueMembers = "total";
            this.chart3.Series.Add(series3);
            this.chart3.Size = new System.Drawing.Size(671, 347);
            this.chart3.TabIndex = 181;
            this.chart3.Text = "chart3";
            // 
            // lstprovsalidasBindingSource
            // 
            this.lstprovsalidasBindingSource.DataMember = "lstprov_salidas";
            this.lstprovsalidasBindingSource.DataSource = this.logicopDataSet1;
            // 
            // lstentradas_x_canterasTableAdapter1
            // 
            this.lstentradas_x_canterasTableAdapter1.ClearBeforeFill = true;
            // 
            // lstsalidas_x_ccosTableAdapter
            // 
            this.lstsalidas_x_ccosTableAdapter.ClearBeforeFill = true;
            // 
            // lstprov_salidasTableAdapter
            // 
            this.lstprov_salidasTableAdapter.ClearBeforeFill = true;
            // 
            // frmgraficos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::smacop2.Properties.Resources.bg_boxsmall;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(671, 347);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart3);
            this.Controls.Add(this.chart2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmgraficos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gráfico de Historico";
            this.Load += new System.EventHandler(this.frmgraficos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstentradasxcanterasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logicopDataSet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstsalidasxccosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lstprovsalidasBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart2;

        private System.Windows.Forms.Label label5;
       
        private logicopDataSet logicopDataSet1;
        private System.Windows.Forms.BindingSource lstentradasxcanterasBindingSource;
        private logicopDataSetTableAdapters.lstentradas_x_canterasTableAdapter lstentradas_x_canterasTableAdapter1;
        private System.Windows.Forms.BindingSource lstsalidasxccosBindingSource;
        private logicopDataSetTableAdapters.lstsalidas_x_ccosTableAdapter lstsalidas_x_ccosTableAdapter;
        public System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.Windows.Forms.BindingSource lstprovsalidasBindingSource;
        private logicopDataSetTableAdapters.lstprov_salidasTableAdapter lstprov_salidasTableAdapter;
    }
}