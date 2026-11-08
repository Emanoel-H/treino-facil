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

        modelBuilder.Entity<Treino>(e =>
        {
            e.Property(t => t.Nome).IsRequired().HasMaxLength(120);
            e.Property(t => t.Descricao).HasMaxLength(300);
            
            e.HasOne(t => t.Aluno)
                .WithMany()
                .HasForeignKey(t => t.AlunoId)
                .OnDelete(DeleteBehavior.Cascade);
            
            
            e.HasMany(t => t.Exercicios)
                .WithOne(x => x.Treino)
                .HasForeignKey(x => x.TreinoId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Exercicio>(e =>
        {
            e.Property(a => a.Nome).IsRequired().HasMaxLength(120);
            e.Property(a => a.Repeticoes).HasMaxLength(60);
        });
    }
}