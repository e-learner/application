using System.Text;

namespace ElearnerApp.Utilities
{
    public class AppTools
    {
        public static string LimitDescription(string description, int limitOfLetters)
        {
            StringBuilder result = new StringBuilder();
            string[] separatewords = description.Split(' ');
            int readedLetters = 0;

            for (int i = 0; i < separatewords.Length; i++)
            {
                if (readedLetters < limitOfLetters)
                {
                    result.Append(separatewords[i] + " ");
                    readedLetters += (separatewords[i] + " ").Length;
                }
                else
                    break;
            }
            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }
    }
}