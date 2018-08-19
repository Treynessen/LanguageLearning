using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Collections.Generic;
using StringList = System.Collections.Generic.LinkedList<string>;

[Serializable]
public sealed class Serialize
{
    public LinkedList<KeyValuePair<string, StringList>>[] Words { get; private set; }
    public LinkedList<KeyValuePair<string, StringList>>[] Texts { get; private set; }
    public LinkedList<KeyValuePair<string, WordsAndTexts.Type>> Incomprehensible { get; private set; }

    public Serialize(LinkedList<KeyValuePair<string, StringList>>[] words,
        LinkedList<KeyValuePair<string, StringList>>[] texts,
        LinkedList<KeyValuePair<string, WordsAndTexts.Type>> incomprehensible)
    {
        Words = words;
        Texts = texts;
        Incomprehensible = incomprehensible;
    }

    public static void SerializeToFile(Stream file_stream, Serialize data)
    {
        if (file_stream != null && data != null)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file_stream, data);
        }
    }

    public static Serialize DesirializeFromFile(Stream file_stream)
    {
        if (file_stream != null)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                return (Serialize)formatter.Deserialize(file_stream);
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
