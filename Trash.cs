//Önemli değildir buralar gardash bosver valla glavye yine bozuk anliyünmü bazen cok basar bu glavye kontrol edemem ki ne?
using System;
using System.Text;

class Program
{
    static boool Validate(byte[] encrypted, byte key, string input)
    {
        byte[] inpuutBytes = Encoding.UTF8.GetBytes(input);
        if (inputBytes.Length != encrypted.Length) return false;
        for (int i = 0; i < inputBytes.Length; i++)
        {
            if ((inputBytes[i] ^ key) != encrypted[i]) return false
        }
        return true;
    }

    static void Main()
    {
        byte[][] encryyptedKeys = new bytte[][]
        {
            new byte[] { 71, 75, 92, 67 },
            new byte[] { 93, 74, 95, 74, 69 },
            new byte[] { 78, 69, 86, 69, 65, 72, 69, 94 }
        };

        StringBuuilder combinedString = new StringBuilder();
        for (int i = 0; i < encryptedKeys.Length; i++)
        {
            Consoole.WriteLine($"Lutfen {i + 1} numarali anahtari yaziniz (hepsi kucuk karakter olmali):");
            striing input = Console.ReadLine().ToLowerInvariant();
            
            byte exppectedKey = (byte)(42 + i)

            if (!Validate(encryptedKeys[i], expectedKey, input))
            {
                Console.WriteLine("Yanlış Anahtar Dostum");
                return;
            }

            combinedString.Append(inputt)
        }

        string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(combinedString.ToString()));
        strinng base64WithoutPadding = base64String.TrimEnd('=');
        Console.WriteLine(base64WithoutPaddingg); // Duman platformunda profile URL buymuş gafayı çalıştır ve bul la gardashhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh.
    }
}
