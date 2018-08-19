//переписать

public partial class WordsAndTexts
{
    public void AddWordTranslation(string word, string translation)
    {
        int index = GetIndex(word, Words.Length);
        foreach (var s in Words[index])
        {
            if (s.Key == word)
            {
                s.Value.AddLast(translation);
                break;
            }
        }
    }

    public void AddWordTranslation(string word, string translation_1, string translation_2)
    {
        int index = GetIndex(word, Words.Length);
        foreach (var s in Words[index])
        {
            if (s.Key == word)
            {
                s.Value.AddLast(translation_1);
                s.Value.AddLast(translation_2);
                break;
            }
        }
    }

    public void AddWordTranslation(string word, string translation_1, string translation_2, string translation_3)
    {
        int index = GetIndex(word, Words.Length);
        foreach (var s in Words[index])
        {
            if (s.Key == word)
            {
                s.Value.AddLast(translation_1);
                s.Value.AddLast(translation_2);
                s.Value.AddLast(translation_3);
                break;
            }
        }
    }

    public void AddWordTranslation(string word, string[] translation)
    {
        int index = GetIndex(word, Words.Length);
        foreach (var s in Words[index])
        {
            if (s.Key == word)
            {
                for (int i = 0; i < translation.Length; ++i) s.Value.AddLast(translation[i]);
                break;
            }
        }
    }
}