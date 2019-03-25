namespace CrashPasswordSystem.BusinessLogic.Encryption
{
    public interface IEncryption
    {
        string GenerateSalt();
        string ComputeHash(string plainText, string hashAlgorithm, string bytes);

    }
}