using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos => Set<Aluno>();
    
    public DbSet<Treino> Treinos => Set<Treino>();
    
    public DbSet<Exercicio> Exercicios => Set<Exercicio>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aluno>(e =>
        {
            e.Property(a => a.Nome).IsRequired().HasMaxLength(120);
            e.Property(a => a.Telefone).HasMaxLength(20);
            e.Property(a => a.Objetivo).HasMaxLength(200);
        });
    }
}