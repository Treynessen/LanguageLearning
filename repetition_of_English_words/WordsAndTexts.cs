using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

[Serializable]
public class WordsAndTexts
{
    public LinkedList<Dictionary<string, string>>[] Words { get; protected set; } // Ключ - английское слово, значение - русский перевод
    public LinkedList<Dictionary<string, string>>[] Texts { get; protected set; }

    public WordsAndTexts()
    {
        Words = new LinkedList<Dictionary<string, string>>[130];
        Texts = new LinkedList<Dictionary<string, string>>[130];
    }

    public void AddWord(string word, string translation)
    {
        int index = 0;
        for (int i = 0; i < word.Length; ++i) index += word[i];
        index = index % Words.Length;
        Dictionary<string, string> pairs = new Dictionary<string, string>();
        pairs.Add(word, translation);
        Words[index].AddLast(pairs);
    }

    public void AddText(string text, string translation)
    {
        int index = 0;
        for (int i = 0; i < text.Length; ++i) index += text[i];
        index = index % Texts.Length;
        Dictionary<string, string> pairs = new Dictionary<string, string>();
        pairs.Add(text, translation);
        Texts[index].AddLast(pairs);
    }

    public static void SerializeToFile(Stream file_stream, WordsAndTexts data)
    {
        if (file_stream != null && data != null)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file_stream, data);
        }
    }

    public static WordsAndTexts DesirializeFromFile(Stream file_stream)
    {
        if (file_stream != null)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                return (WordsAndTexts)formatter.Deserialize(file_stream);
            }
            catch
            {
                MessageBox.Show("Файл поврежден", "Ошибка");
                return null;
            }
        }
        else return null;
    }
}
