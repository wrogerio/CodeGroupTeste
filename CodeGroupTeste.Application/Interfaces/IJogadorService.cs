using CodeGroupTeste.Application.DTOs;

namespace CodeGroupTeste.Application.Interfaces;

public interface IJogadorService
{
    Task<List<JogadorDto>> GetAll();
    Task<JogadorDto> GetById(Guid id);
    Task<JogadorDto> Create(JogadorDto entityDto);
    Task<JogadorDto> Update(JogadorDto entityentityDto);
    Task<bool> Delete(Guid id);
}
