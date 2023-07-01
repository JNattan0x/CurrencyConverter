namespace App.Interfaces
{
    public interface IFileDataContext<T>
    {
        Task<T> GetContextualData();
    }
}