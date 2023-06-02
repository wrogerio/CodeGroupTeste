using CodeGroupTeste.Domain.Base;
using CodeGroupTeste.Domain.Enums;
using CodeGroupTeste.Domain.Extensions;

namespace CodeGroupTeste.Domain.Entities;

public class Jogador : EntityBase
{
    public string Nome { get; set; } = string.Empty;
    public NivelEnum Nivel { get; set; } = NivelEnum.Bom;
    public bool IsGoleiro { get; set; } = false;
    public bool IsConfirmado { get; set; } = false;
    public string Observacao { get; set; } = string.Empty;

    // EF Relation
    public Guid JogoId { get; set; }
    public virtual Jogo? Jogo { get; set; }
}
