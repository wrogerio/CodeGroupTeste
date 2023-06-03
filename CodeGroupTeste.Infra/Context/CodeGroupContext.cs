using CodeGroupTeste.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeGroupTeste.Infra.Context;

public class CodeGroupContext: DbContext
{
    public CodeGroupContext()
    {
    }

    public CodeGroupContext(DbContextOptions<CodeGroupContext> options): base(options)
    {
    }

    public DbSet<Jogo> Jogos { get; set; }
    public DbSet<Jogador> Jogadores { get; set; }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CodeGroupContext).Assembly);
    }
}
