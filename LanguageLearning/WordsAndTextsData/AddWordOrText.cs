using System.Collections.Generic;
using LinkedListWithTranslations = System.Collections.Generic.LinkedList<string>;

public partial class WordsAndTextsData
{
    public void AddWordOrText(WordOrText type, string word_or_text, string translation)
    {
        if (word_or_text != null && translation != null && word_or_text != string.Empty && translation != string.Empty)
        {
            int index = GetHash(word_or_text, type);
            LinkedListWithTranslations word_or_text_translations = GetWordOrTextTranslation(word_or_text, index, type);
            if (word_or_text_translations != null)
            {
                bool have = false;
                foreach (var word_or_text_translation in word_or_text_translations)
                {
                    if (word_or_text_translation == translation)
                    {
                        have = true;
                        break;
                    }
                }
                if (!have) word_or_text_translations.AddLast(translation);
            }
            else
            {
                LinkedListWithTranslations translations_list = new LinkedListWithTranslations();
                translations_list.AddLast(translation);
                KeyValuePair<string, LinkedListWithTranslations> pair = new KeyValuePair<string, LinkedListWithTranslations>(word_or_text, translations_list);
                if (type == WordOrText.Word)
                {
                    if (Words[index] == null) Words[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Words[index].AddLast(pair);
                }
                else
                {
                    if (Texts[index] == null) Texts[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Texts[index].AddLast(pair);
                }
            }
        }
    }

    public void AddWordOrText(WordOrText type, string word_or_text, string translation_1, string translation_2)
    {
        if (word_or_text != null && translation_1 != null && translation_2 != null && word_or_text != string.Empty
            && (translation_1 != string.Empty || translation_2 != string.Empty))
        {
            int index = GetHash(word_or_text, type);
            LinkedListWithTranslations word_or_text_translations = GetWordOrTextTranslation(word_or_text, index, type);
            if (word_or_text_translations != null)
            {
                bool have_1 = false;
                bool have_2 = false;
                foreach (var word_or_text_translation in word_or_text_translations)
                {
                    if (word_or_text_translation == translation_1) have_1 = true;
                    if (word_or_text_translation == translation_2) have_2 = true;
                    if (have_1 && have_2) break;
                }
                if (!have_1 && translation_1 != string.Empty) word_or_text_translations.AddLast(translation_1);
                if (!have_2 && translation_2 != string.Empty) word_or_text_translations.AddLast(translation_2);
            }
            else
            {
                LinkedListWithTranslations translations_list = new LinkedListWithTranslations();
                if (translation_1 != string.Empty) translations_list.AddLast(translation_1);
                if (translation_2 != string.Empty) translations_list.AddLast(translation_2);
                KeyValuePair<string, LinkedListWithTranslations> pair = new KeyValuePair<string, LinkedListWithTranslations>(word_or_text, translations_list);
                if (type == WordOrText.Word)
                {
                    if (Words[index] == null) Words[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Words[index].AddLast(pair);
                }
                else
                {
                    if (Texts[index] == null) Texts[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Texts[index].AddLast(pair);
                }
            }
        }
    }

    public void AddWordOrText(WordOrText type, string word_or_text, string translation_1, string translation_2, string translation_3)
    {
        if (word_or_text != null && translation_1 != null && translation_2 != null && translation_3 != null
            && word_or_text != string.Empty && (translation_1 != string.Empty || translation_2 != string.Empty
            || translation_3 != string.Empty))
        {
            int index = GetHash(word_or_text, type);
            LinkedListWithTranslations word_or_text_translations = GetWordOrTextTranslation(word_or_text, index, type);
            if (word_or_text_translations != null)
            {
                bool have_1 = false;
                bool have_2 = false;
                bool have_3 = false;
                foreach (var word_or_text_translation in word_or_text_translations)
                {
                    if (word_or_text_translation == translation_1) have_1 = true;
                    if (word_or_text_translation == translation_2) have_2 = true;
                    if (word_or_text_translation == translation_3) have_3 = true;
                    if (have_1 && have_2 && have_3) break;
                }
                if (!have_1 && translation_1 != string.Empty) word_or_text_translations.AddLast(translation_1);
                if (!have_2 && translation_2 != string.Empty) word_or_text_translations.AddLast(translation_2);
                if (!have_3 && translation_3 != string.Empty) word_or_text_translations.AddLast(translation_3);
            }
            else
            {
                LinkedListWithTranslations translations_list = new LinkedListWithTranslations();
                if (translation_1 != string.Empty) translations_list.AddLast(translation_1);
                if (translation_2 != string.Empty) translations_list.AddLast(translation_2);
                if (translation_3 != string.Empty) translations_list.AddLast(translation_3);
                KeyValuePair<string, LinkedListWithTranslations> pair = new KeyValuePair<string, LinkedListWithTranslations>(word_or_text, translations_list);
                if (type == WordOrText.Word)
                {
                    if (Words[index] == null) Words[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Words[index].AddLast(pair);
                }
                else
                {
                    if (Texts[index] == null) Texts[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Texts[index].AddLast(pair);
                }
            }
        }
    }

    public void AddWordOrText(WordOrText type, string word_or_text, string translation_1, string translation_2, string translation_3, string translation_4)
    {
        if (word_or_text != null && translation_1 != null && translation_2 != null && translation_3 != null
            && translation_4 != null && word_or_text != string.Empty && (translation_1 != string.Empty
            || translation_2 != string.Empty || translation_3 != string.Empty || translation_4 != string.Empty))
        {
            int index = GetHash(word_or_text, type);
            LinkedListWithTranslations word_or_text_translations = GetWordOrTextTranslation(word_or_text, index, type);
            if (word_or_text_translations != null)
            {
                bool have_1 = false;
                bool have_2 = false;
                bool have_3 = false;
                bool have_4 = false;
                foreach (var word_or_text_translation in word_or_text_translations)
                {
                    if (word_or_text_translation == translation_1) have_1 = true;
                    if (word_or_text_translation == translation_2) have_2 = true;
                    if (word_or_text_translation == translation_3) have_3 = true;
                    if (word_or_text_translation == translation_4) have_4 = true;
                    if (have_1 && have_2 && have_3 && have_4) break;
                }
                if (!have_1 && translation_1 != string.Empty) word_or_text_translations.AddLast(translation_1);
                if (!have_2 && translation_2 != string.Empty) word_or_text_translations.AddLast(translation_2);
                if (!have_3 && translation_3 != string.Empty) word_or_text_translations.AddLast(translation_3);
                if (!have_4 && translation_4 != string.Empty) word_or_text_translations.AddLast(translation_4);
            }
            else
            {
                LinkedListWithTranslations translations_list = new LinkedListWithTranslations();
                if (translation_1 != string.Empty) translations_list.AddLast(translation_1);
                if (translation_2 != string.Empty) translations_list.AddLast(translation_2);
                if (translation_3 != string.Empty) translations_list.AddLast(translation_3);
                if (translation_4 != string.Empty) translations_list.AddLast(translation_4);
                KeyValuePair<string, LinkedListWithTranslations> pair = new KeyValuePair<string, LinkedListWithTranslations>(word_or_text, translations_list);
                if (type == WordOrText.Word)
                {
                    if (Words[index] == null) Words[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Words[index].AddLast(pair);
                }
                else
                {
                    if (Texts[index] == null) Texts[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Texts[index].AddLast(pair);
                }
            }
        }
    }

    public void AddWordOrText(WordOrText type, string word_or_text, string translation_1, string translation_2, string translation_3, string translation_4, string translation_5)
    {
        if (word_or_text != null && translation_1 != null && translation_2 != null && translation_3 != null
            && translation_4 != null && translation_5 != null && word_or_text != string.Empty
            && (translation_1 != string.Empty || translation_2 != string.Empty || translation_3 != string.Empty
            || translation_4 != string.Empty || translation_5 != string.Empty))
        {
            int index = GetHash(word_or_text, type);
            LinkedListWithTranslations word_or_text_translations = GetWordOrTextTranslation(word_or_text, index, type);
            if (word_or_text_translations != null)
            {
                bool have_1 = false;
                bool have_2 = false;
                bool have_3 = false;
                bool have_4 = false;
                bool have_5 = false;
                foreach (var word_or_text_translation in word_or_text_translations)
                {
                    if (word_or_text_translation == translation_1) have_1 = true;
                    if (word_or_text_translation == translation_2) have_2 = true;
                    if (word_or_text_translation == translation_3) have_3 = true;
                    if (word_or_text_translation == translation_4) have_4 = true;
                    if (word_or_text_translation == translation_5) have_5 = true;
                    if (have_1 && have_2 && have_3 && have_4 && have_5) break;
                }
                if (!have_1 && translation_1 != string.Empty) word_or_text_translations.AddLast(translation_1);
                if (!have_2 && translation_2 != string.Empty) word_or_text_translations.AddLast(translation_2);
                if (!have_3 && translation_3 != string.Empty) word_or_text_translations.AddLast(translation_3);
                if (!have_4 && translation_4 != string.Empty) word_or_text_translations.AddLast(translation_4);
                if (!have_5 && translation_5 != string.Empty) word_or_text_translations.AddLast(translation_5);
            }
            else
            {
                LinkedListWithTranslations translations_list = new LinkedListWithTranslations();
                if (translation_1 != string.Empty) translations_list.AddLast(translation_1);
                if (translation_2 != string.Empty) translations_list.AddLast(translation_2);
                if (translation_3 != string.Empty) translations_list.AddLast(translation_3);
                if (translation_4 != string.Empty) translations_list.AddLast(translation_4);
                if (translation_5 != string.Empty) translations_list.AddLast(translation_5);
                KeyValuePair<string, LinkedListWithTranslations> pair = new KeyValuePair<string, LinkedListWithTranslations>(word_or_text, translations_list);
                if (type == WordOrText.Word)
                {
                    if (Words[index] == null) Words[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Words[index].AddLast(pair);
                }
                else
                {
                    if (Texts[index] == null) Texts[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Texts[index].AddLast(pair);
                }
            }
        }
    }

    public void AddWordOrText(WordOrText type, string word_or_text, string[] translations)
    {
        if (word_or_text != null && translations != null && word_or_text != string.Empty)
        {
            int index = GetHash(word_or_text, type);
            LinkedListWithTranslations word_or_text_translations = GetWordOrTextTranslation(word_or_text, index, type);
            if (word_or_text_translations != null)
            {
                bool[] have = new bool[translations.Length];
                foreach (var word_or_text_translation in word_or_text_translations)
                {
                    for (int i = 0; i < translations.Length; ++i)
                        if (translations[i] == word_or_text_translation) have[i] = true;
                }
                for (int i = 0; i < translations.Length; ++i)
                    if (!have[i] && translations[i] != null && translations[i] != string.Empty) word_or_text_translations.AddLast(translations[i]);
            }
            else
            {
                LinkedListWithTranslations translations_list = new LinkedListWithTranslations();
                for (int i = 0; i < translations.Length; ++i)
                    if (translations[i] != null && translations[i] != string.Empty) translations_list.AddLast(translations[i]);
                KeyValuePair<string, LinkedListWithTranslations> pair = new KeyValuePair<string, LinkedListWithTranslations>(word_or_text, translations_list);
                if (type == WordOrText.Word)
                {
                    if (Words[index] == null) Words[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Words[index].AddLast(pair);
                }
                else
                {
                    if (Texts[index] == null) Texts[index] = new LinkedList<KeyValuePair<string, LinkedListWithTranslations>>();
                    Texts[index].AddLast(pair);
                }
            }
        }
    }
}
