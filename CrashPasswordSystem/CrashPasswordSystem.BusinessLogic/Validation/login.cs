using System;
using CrashPasswordSystem.BusinessLogic.Encryption;

namespace CrashPasswordSystem.BusinessLogic.Validation
{

    public class Login 
    {

        private readonly EncryptionClass _Encrypton = new EncryptionClass();
        // <summary>
        // Compares a hash of the specified plain text value to a given hash
        // value. Plain text is hashed with the same salt value as the original
        // hash.
        // </summary>
        // <param name="plainText">
        // Plain text to be verified against the specified hash. The function
        // does not check whether this parameter is null.
        // </param>
        // < name="hashAlgorithm">
        // Name of the hash algorithm. Allowed values are: "MD5", "SHA1",
        // "SHA256", "SHA384", and "SHA512" (if any other value is specified
        // MD5 hashing algorithm will be used). This value is case-insensitive.
        // </param>
        // < name="hashValue">
        // Base64-encoded hash value produced by ComputeHash function. This value
        // includes the original salt appended to it.
        // </param>
        // <returns>
        // If computed hash mathes the specified hash the function the return
        // value is true; otherwise, the function returns false.
        // </returns>
        public bool VerifyHash(string plainText, string hashAlgorithm, string hashValue, string saltValue)
        {
            bool isMatch;

            try
            {

                
                // Make sure that hashing algorithm name is specified.
                if ((hashAlgorithm == null))
                    hashAlgorithm = "";

                // Compute a new hash string.
                var expectedHashString = _Encrypton.ComputeHash(plainText, hashAlgorithm, saltValue);

                // If the computed hash matches the specified hash,
                // the plain text value must be correct.
                isMatch = (hashValue == expectedHashString);

            }
            catch (Exception ex)
            {
                return false;
            }

            return isMatch;
        }

    }




}

