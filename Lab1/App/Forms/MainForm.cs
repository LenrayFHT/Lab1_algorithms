using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Lab1.Core.Algorithms;
using Lab1.Core.Interfaces;
using Lab1.Core.Models;
using Lab1.Data;

namespace Lab1.App.Forms
{
    public partial class MainForm : Form
    {
        // Поля для хранения всех наших сервисов
        private readonly IExperimentService _experimentService;
        private readonly AppDbContext _dbContext;
        private readonly IApproximationService _approximationService;
        private readonly IExperimentRepository _repository;
        
        private CancellationTokenSource? _cts;
        private ExperimentResult? _lastResult;

        // DI-контейнер автоматически передаст сюда все 4 зависимости
        public MainForm(
            IExperimentService experimentService, 
            AppDbContext dbContext,
            IApproximationService approximationService,
            IExperimentRepository repository)
        {
            InitializeComponent();
            
            // Сохраняем сервисы в приватные поля
            _experimentService = experimentService;
            _dbContext = dbContext;
            _approximationService = approximationService;
            _repository = repository;

            // Привязываем обработчик клика для кнопки графика
            this.btnShowGraph.Click += new System.EventHandler(this.btnShowGraph_Click);
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                var algorithms = await _dbContext.Algorithms.AsNoTracking().ToListAsync();
                
                cmbAlgorithms.DataSource = algorithms;
                cmbAlgorithms.DisplayMember = "Name"; 
                cmbAlgorithms.ValueMember = "Id";     
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка подключения к БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            if (cmbAlgorithms.SelectedValue == null) return;

            int algorithmId = (int)cmbAlgorithms.SelectedValue;
            int nMax = (int)numNMax.Value;
            int step = (int)numStep.Value;
            int runs = (int)numRuns.Value;
            bool forceRecalc = chkForceRecalc.Checked;

            ToggleUI(isRunning: true);
            _cts = new CancellationTokenSource();

            try
            {
                _lastResult = await _experimentService.RunExperimentAsync(
                    algorithmId, nMax, step, runs, forceRecalc, _cts.Token);

                MessageBox.Show("Эксперимент успешно завершен и сохранен в БД!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                btnShowGraph.Enabled = true;
                btnCompare.Enabled = true;
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Эксперимент был отменен пользователем.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при выполнении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ToggleUI(isRunning: false);
                _cts?.Dispose();
                _cts = null;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cts?.Cancel(); 
            btnCancel.Enabled = false; 
        }

        // Обработчик для кнопки "Показать график"
        private void btnShowGraph_Click(object? sender, EventArgs e)
        {
            if (_lastResult == null || !_lastResult.Points.Any()) return;

            // 1. Формируем точки для аппроксимации из результатов эксперимента
            bool useSteps = _lastResult.AlgorithmId >= 10 && _lastResult.AlgorithmId <= 12;
            var points = _lastResult.Points
                .Select(p => (p.N, useSteps ? (double)p.AverageSteps : p.AverageTimeMs))
                .ToList();

            // 2. Считаем математику аппроксимации
            var approxResult = _approximationService.Approximate(points);
            string algoName = cmbAlgorithms.Text;

            // 3. Создаем форму графика и передаем в нее проброшенные сервисы и данные
            var plotForm = new PlotForm(
                _repository, 
                _approximationService, 
                _lastResult, 
                approxResult, 
                algoName);
                
            plotForm.Show(); // Открываем график не блокируя главное окно
        }

        private void ToggleUI(bool isRunning)
        {
            cmbAlgorithms.Enabled = !isRunning;
            numNMax.Enabled = !isRunning;
            numStep.Enabled = !isRunning;
            numRuns.Enabled = !isRunning;
            chkForceRecalc.Enabled = !isRunning;
            btnRun.Enabled = !isRunning;
            
            btnCancel.Enabled = isRunning;
            progressBar.Visible = isRunning;
        }
        private void btnHeatmap_Click(object sender, EventArgs e)
        {
            int maxN = 100; // Строки матрицы A
            int maxM = 100; // Столбцы матрицы A (и строки B)
            int step = 10;
            int fixedP = 50; // Фиксируем P (столбцы B) для чистоты 2D-эксперимента

            int rows = maxN / step;
            int cols = maxM / step;
            double[,] heatmapData = new double[rows, cols];

            var matrixAlgo = new MatrixMultiplicationAlgorithm();
            matrixAlgo.CustomP = fixedP; 

            // Блокируем UI на время расчета
            ToggleUI(true);

            try
            {
                // Двумерный цикл для снятия замеров
                for (int r = 0; r < rows; r++)
                {
                    int currentN = (r + 1) * step;
                    for (int c = 0; c < cols; c++)
                    {
                        int currentM = (c + 1) * step;
                
                        matrixAlgo.CustomM = currentM;

                        var sw = Stopwatch.StartNew();
                        matrixAlgo.Run(currentN);
                        sw.Stop();

                        heatmapData[r, c] = sw.Elapsed.TotalMilliseconds;
                    }
                }

                // Создаем форму графика без начальных данных для стандартной отрисовки
                var plotForm = new PlotForm(
                    _repository, 
                    _approximationService, 
                    new ExperimentResult(), // Пустой результат
                    new ApproximationResult(), 
                    "Умножение матриц");

                // Вызываем метод отрисовки Heatmap
                plotForm.RenderHeatmap(heatmapData, step, step);
                plotForm.Show();
            }
            finally
            {
                ToggleUI(false);
            }
        }
    }
}