using CodeGroupTeste.Domain.Entities;
using CodeGroupTeste.Infra.Context;
using CodeGroupTeste.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeGroupTeste.Infra.Repository
{
    public class JogoRepository : IJogoRepository
    {
        private readonly CodeGroupContext _context;

        public JogoRepository(CodeGroupContext context)
        {
            _context = context;
        }

        public async Task<Jogo> Create(Jogo entity)
        {
            await _context.Jogos.AddAsync(entity);
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.Jogos.FirstOrDefaultAsync(x => x.Id == id);
            try
            {
                _context.Jogos.Remove(entity!);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Jogo>> GetAll()
        {
            var result = await _context.Jogos.Include(x => x.Jogadores).ToListAsync();
            return result;
        }

        public async Task<Jogo> GetById(Guid id)
        {
            var result = await _context.Jogos.Include(x => x.Jogadores).FirstOrDefaultAsync(x => x.Id == id);
            return result!;
        }

        public async Task<Jogo> Update(Jogo entity)
        {
            _ = _context.Jogos.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
