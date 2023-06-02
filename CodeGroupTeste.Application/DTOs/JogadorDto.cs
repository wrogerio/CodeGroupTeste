using CodeGroupTeste.Domain.Enums;

namespace CodeGroupTeste.Application.DTOs;

public class JogadorDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public NivelEnum Nivel { get; set; } = NivelEnum.Bom;
    public bool IsGoleiro { get; set; } = false;
    public bool IsConfirmado { get; set; } = false;
    public string Observacao { get; set; } = string.Empty;

    // EF Relation
    public Guid JogoId { get; set; }
    public virtual JogoDto? Jogo { get; set; }
}
