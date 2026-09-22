namespace Lab1.App.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.ComboBox cmbAlgorithms;
        private System.Windows.Forms.NumericUpDown numNMax;
        private System.Windows.Forms.NumericUpDown numStep;
        private System.Windows.Forms.NumericUpDown numRuns;
        private System.Windows.Forms.CheckBox chkForceRecalc;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnShowGraph;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lblAlgo, lblNMax, lblStep, lblRuns;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmbAlgorithms = new System.Windows.Forms.ComboBox();
            this.numNMax = new System.Windows.Forms.NumericUpDown();
            this.numStep = new System.Windows.Forms.NumericUpDown();
            this.numRuns = new System.Windows.Forms.NumericUpDown();
            this.chkForceRecalc = new System.Windows.Forms.CheckBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnShowGraph = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblAlgo = new System.Windows.Forms.Label();
            this.lblNMax = new System.Windows.Forms.Label();
            this.lblStep = new System.Windows.Forms.Label();
            this.lblRuns = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.numNMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuns)).BeginInit();
            this.SuspendLayout();

            lblAlgo.Text = "Алгоритм:";
            lblAlgo.SetBounds(20, 20, 100, 20);
            cmbAlgorithms.SetBounds(130, 20, 250, 25);
            cmbAlgorithms.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            lblNMax.Text = "N_max:";
            lblNMax.SetBounds(20, 60, 100, 20);
            numNMax.SetBounds(130, 60, 100, 25);
            numNMax.Maximum = 1000000;
            numNMax.Value = 10000;

            lblStep.Text = "Шаг (Step):";
            lblStep.SetBounds(20, 100, 100, 20);
            numStep.SetBounds(130, 100, 100, 25);
            numStep.Maximum = 100000;
            numStep.Value = 1000;

            lblRuns.Text = "Запусков:";
            lblRuns.SetBounds(20, 140, 100, 20);
            numRuns.SetBounds(130, 140, 100, 25);
            numRuns.Maximum = 100;
            numRuns.Value = 5;

            chkForceRecalc.Text = "Принудительный пересчёт";
            chkForceRecalc.SetBounds(20, 180, 200, 20);

            btnRun.Text = "Запустить эксперимент";
            btnRun.SetBounds(20, 220, 180, 40);
            btnRun.Click += new System.EventHandler(this.btnRun_Click);

            btnCancel.Text = "Отмена";
            btnCancel.SetBounds(210, 220, 80, 40);
            btnCancel.Enabled = false;
            btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            progressBar.SetBounds(20, 280, 360, 20);
            progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            progressBar.Visible = false;

            btnShowGraph.Text = "Показать график";
            btnShowGraph.SetBounds(20, 320, 175, 40);
            btnShowGraph.Enabled = false;

            btnCompare.Text = "Сравнить с историей";
            btnCompare.SetBounds(205, 320, 175, 40);
            btnCompare.Enabled = false;

            this.ClientSize = new System.Drawing.Size(400, 390);
            this.Controls.Add(lblAlgo); this.Controls.Add(cmbAlgorithms);
            this.Controls.Add(lblNMax); this.Controls.Add(numNMax);
            this.Controls.Add(lblStep); this.Controls.Add(numStep);
            this.Controls.Add(lblRuns); this.Controls.Add(numRuns);
            this.Controls.Add(chkForceRecalc);
            this.Controls.Add(btnRun); this.Controls.Add(btnCancel);
            this.Controls.Add(progressBar);
            this.Controls.Add(btnShowGraph); this.Controls.Add(btnCompare);
            this.Name = "MainForm";
            this.Text = "Анализ сложности алгоритмов";
            this.Load += new System.EventHandler(this.MainForm_Load);

            ((System.ComponentModel.ISupportInitialize)(this.numNMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRuns)).EndInit();
            this.ResumeLayout(false);
        }
    }
}