using CodeGroupTeste.Domain.Extensions;

namespace CodeGroupTeste.Domain.Base;

public class EntityBase: IEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public void CleanProperties() => this.BeautifyProperties();
}
