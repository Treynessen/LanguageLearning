using System.Collections.Generic;
using StringList = System.Collections.Generic.LinkedList<string>;

public partial class WordsAndTexts
{
    public void AddWord(string word, string translation)
    {
        StringList list = new StringList();
        list.AddLast(translation);
        KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(word, list);
        Words[GetIndex(word, Words.Length)].AddLast(pair);
    }

    public void AddWord(string word, string translation_1, string translation_2)
    {
        StringList list = new StringList();
        list.AddLast(translation_1);
        list.AddLast(translation_2);
        KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(word, list);
        Words[GetIndex(word, Words.Length)].AddLast(pair);
    }

    public void AddWord(string word, string translation_1, string translation_2, string translation_3)
    {
        StringList list = new StringList();
        list.AddLast(translation_1);
        list.AddLast(translation_2);
        list.AddLast(translation_3);
        KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(word, list);
        Words[GetIndex(word, Words.Length)].AddLast(pair);
    }

    public void AddWord(string word, string translation_1, string translation_2, string translation_3, string translation_4)
    {
        StringList list = new StringList();
        list.AddLast(translation_1);
        list.AddLast(translation_2);
        list.AddLast(translation_3);
        list.AddLast(translation_4);
        KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(word, list);
        Words[GetIndex(word, Words.Length)].AddLast(pair);
    }

    public void AddWord(string word, string translation_1, string translation_2, string translation_3, string translation_4, string translation_5)
    {
        StringList list = new StringList();
        list.AddLast(translation_1);
        list.AddLast(translation_2);
        list.AddLast(translation_3);
        list.AddLast(translation_4);
        list.AddLast(translation_5);
        KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(word, list);
        Words[GetIndex(word, Words.Length)].AddLast(pair);
    }

    public void AddWord(string word, params string[] translation)
    {
        StringList list = new StringList();
        for (int i = 0; i < translation.Length; ++i) list.AddLast(translation[i]);
        KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(word, list);
        Words[GetIndex(word, Words.Length)].AddLast(pair);
    }
}