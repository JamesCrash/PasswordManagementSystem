namespace CrashPasswordSystem.BusinessLogic.Validation
{
    public interface ILogin
    {
        bool VerifyHash(string plainText, string hashAlgorithm, string hashValue, string saltValue);
    }
}