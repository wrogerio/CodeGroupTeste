namespace CodeGroupTeste.Application.DTOs;

public class JogoDto
{
    public Guid Id { get; set; }
    public DateTime DtPartida { get; set; } = DateTime.Now;
    public string Local { get; set; } = string.Empty;
    public string Placar { get; set; } = string.Empty;
    public int QtdPorTime { get; set; } = 0;
    public string Observacao { get; set; } = string.Empty;

    public bool IsRealizado { get; set; } = false;

    // EF Relation
    public virtual IEnumerable<JogadorDto>? Jogadores { get; set; }
}
