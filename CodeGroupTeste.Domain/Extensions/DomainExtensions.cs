using CodeGroupTeste.Domain.Base;
using System.Security.Principal;

namespace CodeGroupTeste.Domain.Extensions;

public static class DomainExtensions
{
    public static void BeautifyProperties<T>(this T entidade) where T : class, IEntity
    {
        foreach (var property in entidade!.GetType().GetProperties())
        {
            if (property.PropertyType == typeof(string))
            {
                var valor = property.GetValue(entidade)?.ToString();
                if (!string.IsNullOrWhiteSpace(valor))
                {
                    property.SetValue(entidade, valor.Trim());
                }
            }
        }
    }
}
