using CodeGroupTeste.Infra.Context;
using CodeGroupTeste.Infra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeGroupTeste.Infra.Repository;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    private readonly CodeGroupContext _context;

    public RepositoryBase(CodeGroupContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAll()
    {
        var result = await _context.Set<T>().ToListAsync();
        return result;
    }

    public async Task<T> GetById(Guid id)
    {
        var result = await _context.Set<T>().FindAsync(id);
        if (result == null)
        {
            return result!;
        }
        return result;
    }

    public async Task<T> Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update(T entity)
    {
        var result = _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        _ = _context.Set<T>().Remove(entity!);

        var r = await _context.SaveChangesAsync();

        try
        {
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
