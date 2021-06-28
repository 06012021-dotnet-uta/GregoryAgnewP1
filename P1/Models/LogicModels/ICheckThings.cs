namespace LogicModels
{
    /// <summary>
    /// Interface for class CheckThings.
    /// </summary>
    public interface ICheckThings
    {
        bool CheckPassword(string password);
        bool CheckUser(string email);
    }
}