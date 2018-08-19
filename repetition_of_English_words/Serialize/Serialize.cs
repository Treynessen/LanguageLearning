using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Collections.Generic;

[Serializable]
public sealed class Serialize
{
    public WordsAndTexts WordsAndText_DATA { get; private set; }
    public LinkedList<string> Incomprehensible { get; private set; }

    public Serialize(WordsAndTexts data, LinkedList<string> incomprehensible)
    {
        WordsAndText_DATA = data;
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
