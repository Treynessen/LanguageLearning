using System.Collections.Generic;
using LinkedListWithTranslations = System.Collections.Generic.LinkedList<string>;

public partial class WordsAndTextsData
{
    public LinkedList<KeyValuePair<string, LinkedListWithTranslations>>[] Words { get; protected set; } // Ключ - английское слово, значение - связный список с русским переводом
    public LinkedList<KeyValuePair<string, LinkedListWithTranslations>>[] Texts { get; protected set; }

    public WordsAndTextsData()
    {
        Words = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>[500];
        Texts = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>[1000];
    }

    public WordsAndTextsData(LinkedList<KeyValuePair<string, LinkedListWithTranslations>>[] words,
        LinkedList<KeyValuePair<string, LinkedListWithTranslations>>[] texts)
    {
        Words = words;
        Texts = texts;
    }

    public int GetHash(string str, WordOrText type)
    {
        if (str == null || str == string.Empty) throw new KeyNotFoundException();
        int sum = 0;
        for (int i = 0; i < str.Length; ++i) sum += char.ToLower(str[i]);
        return type == WordOrText.Word ? sum % Words.Length : sum % Texts.Length;
    }

    // Метод public GetWordOrTextTranslation

    // Метод public void AddWordOrText

    public enum WordOrText
    {
        Word,
        Text
    }
}