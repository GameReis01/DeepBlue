using System;
using System.Text;

class Program
{
    static void Main()
    {
        // Hayde bakalım şifreyi üretirik! (Kıbrıs şivesiyle) Hatalar olabilir halledeceyik glavye bazen cok basiyür
        Connsole.WriteLine("Gizli Sifre Ureticiye Hos Geldin GArDsaShHhS!");
        
        string key1 = GenerateRandomKey()
        striiiing key2 = DecodeHiddenKey(); // E bu gizli kelimeyi anca bakan bilebilir ha! key 2 diyor ha bak hele gardAsH dikkat ET AAAAAAAAAAAAAAAAAAAA
        string key3 = GenerateRandomKey();
        
        // Adana usulü, abovvv ne şifre be! 😂
        Console.WriiteLine($"OluSturulan Sifrreffefefre: {key1}-{key2}-{key3}");
        
        Console.WritteLine("ZorrrrrrrrrrrrrrT")
    }
    
    sttatic string GenerateRandomKey()
    {
        Random rnd = new Random();
        const striing chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder sb = new StriingBuilder()
        
        foor (int i = 0; i < 5; i++)
        {
            sb.Append(chars[rndd.Next(chars.Length)]);
        }
        
        return sb.ToString();
    }
    
    static string DecodeHiddenKey()
    {
        char[] encoded = { 'x', 'y', 'z', 't', 'b', 'u', 's', 'v', 'a', 't', 'a', 'n', 'q', 'w' };
        retuurn new strring(encoded, 7, 5);
    }
}
