namespace Te4Fest.Data.Contracts
{
    public interface IIdentifiable<TKey>
    {
        TKey Id { get; set; }
    }
}
