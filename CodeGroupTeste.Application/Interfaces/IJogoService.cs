using CodeGroupTeste.Application.DTOs;

namespace CodeGroupTeste.Application.Interfaces;

public interface IJogoService
{
    Task<List<JogoDto>> GetAll();
    Task<JogoDto> GetById(Guid id);
    Task<JogoDto> Create(JogoDto entityDto);
    Task<JogoDto> Update(JogoDto entityDto);
    Task<bool> Delete(Guid id);
}
