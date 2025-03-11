using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using YourTask.Domain;
using YourTask.Domain.Models;

namespace YourTask.Infrastructure
{
    public class YourTaskContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }

        public YourTaskContext(DbContextOptions<YourTaskContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>().HasData(TarefasDemo.CriarTarefasDemo());

            // Configure Tarefa entity
            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.ToTable("Tarefas");

                entity.HasKey(t => t.Id);

                entity.Property(t => t.Titulo)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(t => t.Descricao);

                entity.Property(t => t.DataCriacao)
                      .HasDefaultValueSql("GETDATE()");
            });
        }
    }   
}
