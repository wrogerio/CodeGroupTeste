using CodeGroupTeste.Application.DTOs;
namespace CodeGroupTeste.UI.Models;
public class JogoViewModel
{
    public JogoDto Jogo { get; set; }
    public List<JogadorDto> Jogadores { get; set; }
    public JogadorDto Jogador { get; set; }
}
