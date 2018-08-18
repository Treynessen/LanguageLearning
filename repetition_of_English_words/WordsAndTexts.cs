using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using StringList = System.Collections.Generic.LinkedList<string>;

[Serializable]
public class WordsAndTexts
{
    public LinkedList<KeyValuePair<string, StringList>>[] Words { get; protected set; } // Ключ - английское слово, значение - связный список с русским переводом
    public LinkedList<KeyValuePair<string, StringList>>[] Texts { get; protected set; }

    public WordsAndTexts()
    {
        Words = new LinkedList<KeyValuePair<string, StringList>>[500];
        Texts = new LinkedList<KeyValuePair<string, StringList>>[100];
    }

    public void AddWord(string word, params string[] translation)
    {
        int index = 0;
        for (int i = 0; i < word.Length; ++i) index += word[i];
        index = index % Words.Length;
        StringList list = new StringList();
        for (int i = 0; i < translation.Length; ++i) list.AddLast(translation[i]);
        KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(word, list);
        Words[index].AddLast(pair);
    }

    public void AddText(string text, params string[] translation)
    {
        int index = 0;
        for (int i = 0; i < text.Length; ++i) index += text[i];
        index = index % Texts.Length;
        StringList list = new StringList();
        for (int i = 0; i < translation.Length; ++i) list.AddLast(translation[i]);
        KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(text, list);
        Texts[index].AddLast(pair);
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
