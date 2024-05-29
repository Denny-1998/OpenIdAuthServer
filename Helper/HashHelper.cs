using System;
using System.Security.Cryptography;
using System.Text;

public class HashHelper
{

    public string ComputeHash_SHA512(string input)
    {
        if (input == null)
            throw new ArgumentNullException("input");

        using (SHA512 sha512 = SHA512.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = sha512.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }


    public string ComputeHashWithSalt_SHA512(string input, string salt)
    {
        if (input == null)
            throw new ArgumentNullException("input");

        if (salt == null)
            throw new ArgumentNullException("salt");

        using (SHA512 sha512 = SHA512.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input + salt);
            byte[] hashBytes = sha512.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }

    public string IterateHash(string input, string salt, int iterations = 50)
    {
        return RecursiveHashing(input, salt, iterations);
    }

    private string RecursiveHashing(string input, string salt, int iterations)
    {
        if (iterations == 0)
            return input; // Base case: return the input string when iterations reach 0

        string hashedResult = ComputeHashWithSalt_SHA512(input, salt);
        // Recursive call with decremented iterations
        return RecursiveHashing(hashedResult, salt, iterations - 1);
    }


}