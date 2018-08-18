using System;
using System.Collections.Generic;
using StringList = System.Collections.Generic.LinkedList<string>;

[Serializable]
public partial class WordsAndTexts
{
    public LinkedList<KeyValuePair<string, StringList>>[] Words { get; protected set; } // Ключ - английское слово, значение - связный список с русским переводом
    public LinkedList<KeyValuePair<string, StringList>>[] Texts { get; protected set; }

    public WordsAndTexts()
    {
        Words = new LinkedList<KeyValuePair<string, StringList>>[500];
        Texts = new LinkedList<KeyValuePair<string, StringList>>[100];
    }

    int GetIndex(string str, int arr_length)
    {
        int index = 0;
        for (int i = 0; i < str.Length; ++i) index += str[i];
        return index = index % arr_length;
    }

    // Метод public void AddWord

    // Метод public void AddText

    // Метод public void AddWordTranslation

    // Метод public void AddTextTranslation
}
