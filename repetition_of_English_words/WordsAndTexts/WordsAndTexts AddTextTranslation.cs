public partial class WordsAndTexts
{
    public void AddTextTranslation(string text, string translation)
    {
        int index = GetIndex(text, Texts.Length);
        foreach (var s in Texts[index])
        {
            if (s.Key == text)
            {
                s.Value.AddLast(translation);
                break;
            }
        }
    }

    public void AddTextTranslation(string text, string translation_1, string translation_2)
    {
        int index = GetIndex(text, Texts.Length);
        foreach (var s in Texts[index])
        {
            if (s.Key == text)
            {
                s.Value.AddLast(translation_1);
                s.Value.AddLast(translation_2);
                break;
            }
        }
    }

    public void AddTextTranslation(string text, string translation_1, string translation_2, string translation_3)
    {
        int index = GetIndex(text, Texts.Length);
        foreach (var s in Texts[index])
        {
            if (s.Key == text)
            {
                s.Value.AddLast(translation_1);
                s.Value.AddLast(translation_2);
                s.Value.AddLast(translation_3);
                break;
            }
        }
    }

    public void AddTextTranslation(string text, params string[] translation)
    {
        int index = GetIndex(text, Texts.Length);
        foreach (var s in Texts[index])
        {
            if (s.Key == text)
            {
                for (int i = 0; i < translation.Length; ++i) s.Value.AddLast(translation[i]);
                break;
            }
        }
    }
}