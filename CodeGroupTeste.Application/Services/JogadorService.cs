using AutoMapper;
using CodeGroupTeste.Application.DTOs;
using CodeGroupTeste.Application.Interfaces;
using CodeGroupTeste.Domain.Entities;
using CodeGroupTeste.Infra.Interfaces;

namespace CodeGroupTeste.Application.Services;

public class JogadorService : IJogadorService
{
    private readonly IJogadorRepository _repository;
    private readonly IMapper _mapper;

    public JogadorService(IJogadorRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<JogadorDto>> GetAll()
    {
        var jogadores = await _repository.GetAll();
        return _mapper.Map<List<JogadorDto>>(jogadores);
    }

    public async Task<JogadorDto> GetById(Guid id)
    {
        var jogador = await _repository.GetById(id);
        return _mapper.Map<JogadorDto>(jogador);
    }

    public async Task<JogadorDto> Create(JogadorDto entityDto)
    {
        var jogadorEntity = _mapper.Map<Jogador>(entityDto);
        await _repository.Create(jogadorEntity);
        return _mapper.Map<JogadorDto>(jogadorEntity);
    }

    public async Task<JogadorDto> Update(JogadorDto entityDto)
    {
        var jogadorEntity = _mapper.Map<Jogador>(entityDto);
        await _repository.Update(jogadorEntity);
        return _mapper.Map<JogadorDto>(jogadorEntity);
    }

    public async Task<bool> Delete(Guid id)
    {
        var jogador = await _repository.GetById(id);

        if (jogador == null)
            return false;

        await _repository.Delete(jogador.Id);
        return true;
    }
}
