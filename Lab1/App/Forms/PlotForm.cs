using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using ScottPlot;
using Lab1.Core.Interfaces;
using Lab1.Core.Models;
using System.Collections.Generic;

namespace Lab1.App.Forms
{
    public partial class PlotForm : Form
    {
        private readonly IExperimentRepository _repository;
        private readonly IApproximationService _approximationService;
        
        private readonly ExperimentResult _initialResult;
        private readonly ApproximationResult _initialApproximation;
        private readonly string _algorithmName;

        private int _seriesCounter = 1;

        public PlotForm(
            IExperimentRepository repository, 
            IApproximationService approximationService,
            ExperimentResult initialResult,
            ApproximationResult initialApproximation,
            string algorithmName)
        {
            InitializeComponent();
            _repository = repository;
            _approximationService = approximationService;
            _initialResult = initialResult;
            _initialApproximation = initialApproximation;
            _algorithmName = algorithmName;
        }

        private async void PlotForm_Load(object sender, EventArgs e)
        {
            // 1. Отрисовка текущего эксперимента
            PlotExperiment(_initialResult, _initialApproximation, _algorithmName, isMain: true);

            // 2. Настройка осей и заголовка
            formsPlot.Plot.Axes.Bottom.Label.Text = "Размерность N";
            formsPlot.Plot.Axes.Left.Label.Text = "Время (мс)";
            formsPlot.Plot.ShowLegend();
            
            // 3. Загрузка истории в ComboBox (выбираем сессии для этого же алгоритма)
            var sessions = await _repository.GetSessionsAsync();
            var targetSessions = sessions
                .Where(s => s.AlgorithmId == _initialResult.AlgorithmId)
                .Select(s => new { 
                    s.Id, 
                    DisplayName = $"Сессия #{s.Id} (NMax: {s.NMax}, От: {s.CreatedAt:HH:mm})" 
                })
                .ToList();

            cmbHistory.DataSource = targetSessions;
            cmbHistory.DisplayMember = "DisplayName";
            cmbHistory.ValueMember = "Id";
        }

        private void PlotExperiment(ExperimentResult expResult, ApproximationResult approxResult, string algoName, bool isMain)
        {
            // Проверяем, нужно ли использовать шаги вместо времени (для алгоритмов 10, 11, 12)
            bool useSteps = expResult.AlgorithmId >= 10 && expResult.AlgorithmId <= 12;

            // Подготовка массивов X и Y для эмпирических точек
            double[] xs = expResult.Points.Select(p => (double)p.N).ToArray();
            double[] ys = useSteps 
                ? expResult.Points.Select(p => (double)p.AverageSteps).ToArray() 
                : expResult.Points.Select(p => p.AverageTimeMs).ToArray();

            // Подготовка массивов для идеальной кривой аппроксимации
            double[] curveXs = approxResult.Curve.Select(p => (double)p.n).ToArray();
            double[] curveYs = approxResult.Curve.Select(p => p.expectedTime).ToArray();

            // Генерируем уникальный цвет для новой серии
            var color = isMain ? Colors.Blue : GetRandomColor();

            // 1. Добавляем эмпирические точки (Scatter plot)
            var scatter = formsPlot.Plot.Add.Scatter(xs, ys);
            scatter.LineStyle.Width = 0; // Скрываем линию, оставляем только маркеры
            scatter.MarkerStyle.FillColor = color;
            scatter.MarkerStyle.Size = 8;
            scatter.Label = isMain ? "Эмпирика (Текущая)" : $"Эмпирика (История {_seriesCounter})";

            // 2. Добавляем линию аппроксимации (Line plot)
            var line = formsPlot.Plot.Add.Scatter(curveXs, curveYs);
            line.MarkerStyle.Size = 0; // Скрываем маркеры, оставляем только линию
            line.LineStyle.Width = 2;
            line.LineStyle.Color = color.WithOpacity(0.5); // Полупрозрачная линия
            line.Label = isMain ? $"Теор: {approxResult.Function}" : $"Теор (История {_seriesCounter})";

            // Настраиваем заголовки и оси только при отрисовке основного (первого) графика
            if (isMain)
            {
                string title = $"{algoName}\nЛучшая аппроксимация: {approxResult.Function} (MSE = {approxResult.MSE:E2})";
                formsPlot.Plot.Title(title);
                
                formsPlot.Plot.Axes.Left.Label.Text = useSteps ? "Количество операций (шаги)" : "Время (мс)";
            }
            
            formsPlot.Refresh();
        }

        private async void btnAddSeries_Click(object sender, EventArgs e)
        {
            if (cmbHistory.SelectedValue == null) return;
            int sessionId = (int)cmbHistory.SelectedValue;

            // Загружаем данные экспериментов по ID сессии
            // (В репозитории должен быть метод GetBySessionAsync, либо фильтруем все)
            var allData = await _repository.GetByAlgorithmAsync(_initialResult.AlgorithmId);
            var sessionData = allData.Where(x => x.SessionId == sessionId).ToList();

            if (!sessionData.Any()) return;

            // Группируем по N и считаем среднее (собираем ExperimentResult)
            var historyExpResult = new ExperimentResult { AlgorithmId = _initialResult.AlgorithmId };
            var grouped = sessionData.GroupBy(x => x.N);
            
            var pointsForApproximation = new List<(int n, double t)>();

            foreach (var g in grouped)
            {
                var avgTime = g.Average(x => x.ElapsedMs);
                historyExpResult.Points.Add(new ExperimentPoint { N = g.Key, AverageTimeMs = avgTime });
                pointsForApproximation.Add((g.Key, avgTime));
            }

            // Вычисляем аппроксимацию для исторических данных
            var historyApprox = _approximationService.Approximate(pointsForApproximation);

            // Отрисовываем
            PlotExperiment(historyExpResult, historyApprox, _algorithmName, isMain: false);
            _seriesCounter++;
        }

        // --- ДЕМОНСТРАЦИЯ HEATMAP ДЛЯ МАТРИЦ (Часть IV) ---
        private void btnShowHeatmap_Click(object sender, EventArgs e)
        {
            formsPlot.Plot.Clear();

            // Создаем фейковую матрицу зависимостей времени от (N, M)
            int rows = 10; // Например, N
            int cols = 10; // Например, M
            double[,] data = new double[rows, cols];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < cols; c++)
                {
                    // Имитируем рост времени при увеличении матриц O(N*M*K)
                    data[r, c] = (r + 1) * (c + 1) * 1.5; 
                }
            }

            // Добавляем тепловую карту
            var hm = formsPlot.Plot.Add.Heatmap(data);
            
            formsPlot.Plot.Title("Зависимость времени умножения матриц (N x M)");
            formsPlot.Plot.Axes.Bottom.Label.Text = "Размерность M";
            formsPlot.Plot.Axes.Left.Label.Text = "Размерность N";
            
            // Скрываем легенду от прошлых графиков
            formsPlot.Plot.HideLegend();
            formsPlot.Refresh();
        }

        private ScottPlot.Color GetRandomColor()
        {
            var rand = new Random();
            return new ScottPlot.Color(rand.Next(50, 200), rand.Next(50, 200), rand.Next(50, 200));
        }
        public void RenderHeatmap(double[,] timeData, int stepN, int stepM)
        {
            formsPlot.Plot.Clear();

            // Добавляем тепловую карту
            var hm = formsPlot.Plot.Add.Heatmap(timeData);
    
            // Настраиваем оси, чтобы они показывали реальные значения N и M (с учетом шага)
            double left = stepM; // Смещение по X
            double right = left + (timeData.GetLength(1) * stepM); // Конец по X
            double bottom = stepN; // Смещение по Y
            double top = bottom + (timeData.GetLength(0) * stepN); // Конец по Y

            hm.Extent = new ScottPlot.CoordinateRect(left, right, bottom, top);

            formsPlot.Plot.Title("Heatmap: Зависимость времени умножения (N x M)");
            formsPlot.Plot.Axes.Bottom.Label.Text = "Размерность M (столбцы A)";
            formsPlot.Plot.Axes.Left.Label.Text = "Размерность N (строки A)";
    
            formsPlot.Plot.HideLegend();
            formsPlot.Refresh();
        }
    }
}