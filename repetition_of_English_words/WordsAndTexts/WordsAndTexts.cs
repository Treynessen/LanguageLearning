using System.Collections.Generic;
using StringList = System.Collections.Generic.LinkedList<string>;

public partial class WordsAndTexts
{
    public LinkedList<KeyValuePair<string, StringList>>[] Words { get; protected set; } // Ключ - английское слово, значение - связный список с русским переводом
    public LinkedList<KeyValuePair<string, StringList>>[] Texts { get; protected set; }

    public WordsAndTexts()
    {
        Words = new LinkedList<KeyValuePair<string, StringList>>[500];
        Texts = new LinkedList<KeyValuePair<string, StringList>>[100];

    }

    public WordsAndTexts(LinkedList<KeyValuePair<string, StringList>>[] words, LinkedList<KeyValuePair<string, StringList>>[] texts)
    {
        Words = words;
        Texts = texts;
    }

    public int GetIndex(string str, int arr_length)
    {
        int index = 0;
        for (int i = 0; i < str.Length; ++i) index += str[i];
        return index = index % arr_length;
    }

    StringList CheckKeysInWords(int index, string word)
    {
        if (Words[index] == null)
        {
            Words[index] = new LinkedList<KeyValuePair<string, StringList>>();
            return null;
        }
        foreach (var k in Words[index])
        {
            if (k.Key == word) return k.Value;
        }
        return null;
    }

    StringList CheckKeysInTexts(int index, string text)
    {
        if (Texts[index] == null)
        {
            Texts[index] = new LinkedList<KeyValuePair<string, StringList>>();
            return null;
        }
        foreach (var k in Texts[index])
        {
            if (k.Key == text) return k.Value;
        }
        return null;
    }

    // Метод public void AddWord

    // Метод public void AddText

    // Метод public void AddWordTranslation

    // Метод public void AddTextTranslation

    public enum Type
    {
        Word,
        Text,
        None
    }
}
