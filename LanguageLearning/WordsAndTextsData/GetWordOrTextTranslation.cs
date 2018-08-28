using LinkedListWithTranslations = System.Collections.Generic.LinkedList<string>;

public partial class WordsAndTextsData
{
    public LinkedListWithTranslations GetWordOrTextTranslation(string word_or_text, WordOrText type)
    {
        if (word_or_text != string.Empty)
        {
            int index = GetHash(word_or_text, type);
            if (type == WordOrText.Word)
            {
                if (Words[index] == null) return null;
                foreach (var pair in Words[index])
                {
                    if (pair.Key.Equals(word_or_text, System.StringComparison.CurrentCultureIgnoreCase)) return pair.Value;
                }
            }
            else
            {
                if (Texts[index] == null) return null;
                foreach (var pair in Texts[index])
                {
                    if (pair.Key.Equals(word_or_text, System.StringComparison.CurrentCultureIgnoreCase)) return pair.Value;
                }
            }
        }
        return null;
    }

    public LinkedListWithTranslations GetWordOrTextTranslation(string word_or_text, int index, WordOrText type)
    {
        if (word_or_text != string.Empty)
        {
            if (type == WordOrText.Word)
            {
                if (Words[index] == null) return null;
                foreach (var pair in Words[index])
                {
                    if (pair.Key.Equals(word_or_text, System.StringComparison.CurrentCultureIgnoreCase)) return pair.Value;
                }
            }
            else
            {
                if (Texts[index] == null) return null;
                foreach (var pair in Texts[index])
                {
                    if (pair.Key.Equals(word_or_text, System.StringComparison.CurrentCultureIgnoreCase)) return pair.Value;
                }
            }
        }
        return null;
    }
}