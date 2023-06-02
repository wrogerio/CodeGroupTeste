using CodeGroupTeste.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGroupTeste.Infra.Configuration;

public class JogoConfig : IEntityTypeConfiguration<Jogo>
{
    public void Configure(EntityTypeBuilder<Jogo> builder)
    {
        // Key
        builder.HasKey(x => x.Id);

        // Properties
        builder.Property(x => x.Placar).HasColumnType("varchar").HasMaxLength(5).HasDefaultValue("");
        builder.Property(x => x.Observacao).HasColumnType("varchar").HasMaxLength(250).HasDefaultValue("");
        builder.Property(x => x.Local).HasColumnType("varchar").HasMaxLength(100).HasDefaultValue("");
        builder.Property(x => x.QtdPorTime).HasColumnType("int").HasDefaultValue(10);
        builder.Property(x => x.DtPartida).HasColumnType("datetime").HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.IsRealizado).HasColumnType("bit").HasDefaultValue(false);

        // EF Relation
        builder.HasMany(x => x.Jogadores).WithOne(x => x.Jogo).HasForeignKey(x => x.JogoId).OnDelete(DeleteBehavior.Cascade);
    }
}
