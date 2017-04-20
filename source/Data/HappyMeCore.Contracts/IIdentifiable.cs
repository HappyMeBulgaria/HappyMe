namespace HappyMe.Data.Contracts
{
    public interface IIdentifiable<TKey>
    {
        TKey Id { get; set; }
    }
}
