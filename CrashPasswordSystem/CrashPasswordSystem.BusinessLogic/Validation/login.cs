using System;
using CrashPasswordSystem.BusinessLogic.Encryption;

namespace CrashPasswordSystem.BusinessLogic.Validation
{

    public class Login : ILogin
    {

        public bool VerifyHash(string plainText, string hashAlgorithm, string hashValue, string saltValue)
        {
            bool isMatch;
          EncryptionClass _Encrypton = new EncryptionClass();

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

