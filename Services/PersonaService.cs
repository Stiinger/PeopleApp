using Microsoft.EntityFrameworkCore;
using PeopleApp.DAL;
using PeopleApp.Models;
using System.Linq.Expressions;

namespace PeopleApp.Services;

public class PersonaService
{
    private readonly Contexto _contexto;

    public PersonaService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Guardar(Persona persona)
    {
        if (!await Existe(persona.PersonaId))
            return await Insertar(persona);
        else
            return await Modificar(persona);
    }

    private async Task<bool> Modificar(Persona persona)
    {
        _contexto.Personas.Update(persona);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Insertar(Persona persona)
    {
        _contexto.Personas.Add(persona);
        return await _contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Existe(int personaId)
    {
        return await _contexto.Personas
            .AnyAsync(p => p.PersonaId == personaId);
    }

    public async Task<bool> Eliminar(Persona persona)
    {
        var cuantity = await _contexto.Personas
            .Where(p => p.PersonaId == persona.PersonaId)
            .ExecuteDeleteAsync();
        return cuantity > 0;
    }

    public async Task<Persona?> Buscar(int personaId)
    {
        return await _contexto.Personas
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PersonaId == personaId);
    }
	public async Task<Persona?> BuscarNombre(string nombre)
	{
		return await _contexto.Personas
			.AsNoTracking()
			.FirstOrDefaultAsync(p => p.Nombre == nombre);
	}

	public List<Persona> Listar(Expression<Func<Persona, bool>> criterio)
    {
        return _contexto.Personas
            .AsNoTracking()
            .Where(criterio)
            .ToList();
    }
}