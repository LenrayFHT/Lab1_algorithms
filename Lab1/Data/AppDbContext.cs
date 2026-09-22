using Lab1.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Data
{
    /// <summary>
    /// Контекст базы данных Entity Framework Core для PostgreSQL.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Algorithm> Algorithms { get; set; }
        public DbSet<ExperimentSession> ExperimentSessions { get; set; }
        public DbSet<Experiment> Experiments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связей через Fluent API

            modelBuilder.Entity<Algorithm>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<ExperimentSession>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Algorithm)
                      .WithMany(a => a.Sessions)
                      .HasForeignKey(e => e.AlgorithmId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Experiment>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                // Связь Эксперимента с Алгоритмом
                entity.HasOne(e => e.Algorithm)
                      .WithMany()
                      .HasForeignKey(e => e.AlgorithmId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Связь Эксперимента с Сессией (серией)
                entity.HasOne(e => e.Session)
                      .WithMany(s => s.Experiments)
                      .HasForeignKey(e => e.SessionId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Seed Data: Добавим пару базовых алгоритмов при создании БД
            modelBuilder.Entity<Algorithm>().HasData(
                new Algorithm { Id = 1, Name = "Сортировка пузырьком", BigONotation = "O(n^2)", Description = "Простая сортировка обменом" },
                new Algorithm { Id = 2, Name = "Быстрая сортировка", BigONotation = "O(n log n)", Description = "Сортировка Хоара (QuickSort)" },
                new Algorithm { Id = 3, Name = "Константная функция", BigONotation = "O(1)" },
                new Algorithm { Id = 4, Name = "Сумма элементов", BigONotation = "O(n)" },
                new Algorithm { Id = 5, Name = "Произведение элементов", BigONotation = "O(n)" },
                new Algorithm { Id = 6, Name = "Полином (в лоб)", BigONotation = "O(n^2)" },
                new Algorithm { Id = 7, Name = "Полином (Горнер)", BigONotation = "O(n)" },
                new Algorithm { Id = 8, Name = "Встроенная сортировка (Array.Sort)", BigONotation = "O(n log n)" },
                new Algorithm { Id = 9, Name = "Умножение матриц", BigONotation = "O(n^3)" },
                new Algorithm { Id = 10, Name = "Степень (Наивный)", BigONotation = "O(n)" },
                new Algorithm { Id = 11, Name = "Степень (Рекурсивный)", BigONotation = "O(n)" },
                new Algorithm { Id = 12, Name = "Степень (Быстрый/Бинарный)", BigONotation = "O(log n)" },
                new Algorithm { Id = 13, Name = "Radix Sort (LSD, base 10)", BigONotation = "O(d·n)", Description = "Поразрядная сортировка, LSD, основание 10. Несравнительная, устойчивая, O(d·n)." }
            );
        }
    }
}