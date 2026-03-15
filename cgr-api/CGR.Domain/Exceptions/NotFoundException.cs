namespace CGR.Domain.Exceptions;

/// <summary>
/// Exceção lançada quando uma entidade não é encontrada no repositório.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string entityName, Guid id)
        : base($"{entityName} com ID '{id}' não foi encontrado(a).") { }
}
