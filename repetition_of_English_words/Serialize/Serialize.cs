using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

public static class Serialize
{
    public static void SerializeToFile(Stream file_stream, Container data)
    {
        if (file_stream != null && data != null)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file_stream, data);
        }
    }

    public static Container DesirializeFromFile(Stream file_stream)
    {
        if (file_stream != null)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                return (Container)formatter.Deserialize(file_stream);
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
