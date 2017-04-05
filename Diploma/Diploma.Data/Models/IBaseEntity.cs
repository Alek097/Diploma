namespace Diploma.Data.Models
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}
