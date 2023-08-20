namespace Domain
{
    public interface IAuditableEntity : IEntity
    {
        DateTime CreatedAt { get; set; }
        DateTime? ModifiedAt { get; set; }
    }
}