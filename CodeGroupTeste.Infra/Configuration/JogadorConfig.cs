using CodeGroupTeste.Domain.Entities;
using CodeGroupTeste.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGroupTeste.Infra.Configuration;

public class JogadorConfig : IEntityTypeConfiguration<Jogador>
{
    public void Configure(EntityTypeBuilder<Jogador> builder)
    {
        // Key
        builder.HasKey(x => x.Id);

        // Properties
        builder.Property(x => x.Nome).HasColumnType("varchar").HasMaxLength(70).HasDefaultValue("");
        builder.Property(x => x.Observacao).HasColumnType("varchar").HasMaxLength(250).HasDefaultValue("");
        builder.Property(x => x.IsGoleiro).HasDefaultValue(false);
        builder.Property(x => x.IsConfirmado).HasDefaultValue(false);
        builder.Property(x => x.Nivel).HasDefaultValue(NivelEnum.Bom);
    }
}
