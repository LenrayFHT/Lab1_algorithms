namespace Lab1.App.Forms
{
    partial class PlotForm
    {
        private System.ComponentModel.IContainer components = null;

        private ScottPlot.WinForms.FormsPlot formsPlot;
        private System.Windows.Forms.ComboBox cmbHistory;
        private System.Windows.Forms.Button btnAddSeries;
        private System.Windows.Forms.Button btnShowHeatmap;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.Panel panelTop;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.formsPlot = new ScottPlot.WinForms.FormsPlot();
            this.cmbHistory = new System.Windows.Forms.ComboBox();
            this.btnAddSeries = new System.Windows.Forms.Button();
            this.btnShowHeatmap = new System.Windows.Forms.Button();
            this.lblHistory = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();

            // Верхняя панель управления
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Height = 50;
            
            this.lblHistory.Text = "История сессий:";
            this.lblHistory.SetBounds(10, 15, 100, 20);

            this.cmbHistory.SetBounds(110, 12, 300, 25);
            this.cmbHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.btnAddSeries.Text = "Добавить на график";
            this.btnAddSeries.SetBounds(420, 10, 150, 30);
            this.btnAddSeries.Click += new System.EventHandler(this.btnAddSeries_Click);

            this.btnShowHeatmap.Text = "Тест Heatmap (Матрицы)";
            this.btnShowHeatmap.SetBounds(580, 10, 170, 30);
            this.btnShowHeatmap.Click += new System.EventHandler(this.btnShowHeatmap_Click);

            this.panelTop.Controls.Add(this.lblHistory);
            this.panelTop.Controls.Add(this.cmbHistory);
            this.panelTop.Controls.Add(this.btnAddSeries);
            this.panelTop.Controls.Add(this.btnShowHeatmap);

            // График ScottPlot
            this.formsPlot.Dock = System.Windows.Forms.DockStyle.Fill;

            // Форма
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.formsPlot);
            this.Controls.Add(this.panelTop);
            this.Name = "PlotForm";
            this.Text = "Графики сложности алгоритмов";
            this.Load += new System.EventHandler(this.PlotForm_Load);

            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}