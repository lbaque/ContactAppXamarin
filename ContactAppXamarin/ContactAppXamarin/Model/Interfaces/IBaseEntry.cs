namespace ContactAppXamarin.Model.Interfaces
{
    public interface IBaseEntry<TKey> 
    {
        TKey Id { get; set; }
    }
}
