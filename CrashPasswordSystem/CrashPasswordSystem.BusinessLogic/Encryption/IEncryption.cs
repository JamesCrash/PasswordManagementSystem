namespace CrashPasswordSystem.BusinessLogic.Encryption
{
    public interface IEncryption
    {
        string GenerateSalt();
        string ComputeHash(string plainText, string hashAlgorithm, string bytes);
        bool VerifyHash(string plainText, string hashAlgorithm, string hashValue, string saltValue);
        bool Test(string plainstring);
    }
}