using System;
using System.IO;
namespace SocialNetworkMovies.FileHandler
{
    static public class FileHandler
    {
        static private string fileName = Path.Combine(Directory.GetCurrentDirectory(), "../apikey.txt");

        static public string readFile()
        {
            if (File.Exists(fileName))
            {
                string key = File.ReadAllText(fileName);
                return key;
            }
            else
            {
                throw new Exception("File not found");
            }
        }
    }
}
