using System.Collections.Generic;
using StringList = System.Collections.Generic.LinkedList<string>;

public partial class WordsAndTexts
{
    public void AddText(string text, string translation)
    {
        int index = GetIndex(text, Texts.Length);
        StringList sl = CheckKeysInTexts(index, text);
        if (sl != null)
        {
            bool have = false;
            foreach (var s in sl)
                if (s == translation) have = true;
            if (!have) sl.AddLast(translation);
        }
        else
        {
            StringList list = new StringList();
            list.AddLast(translation);
            KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(text, list);
            Texts[index].AddLast(pair);
        }
    }

    public void AddText(string text, string translation_1, string translation_2)
    {
        int index = GetIndex(text, Texts.Length);
        StringList sl = CheckKeysInTexts(index, text);
        if (sl != null)
        {
            bool have_1 = false;
            bool have_2 = false;
            foreach (var s in sl)
            {
                if (s == translation_1) have_1 = true;
                if (s == translation_2) have_2 = true;
            }
            if (!have_1) sl.AddLast(translation_1);
            if (!have_2) sl.AddLast(translation_2);
        }
        else
        {
            StringList list = new StringList();
            list.AddLast(translation_1);
            list.AddLast(translation_2);
            KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(text, list);
            Texts[index].AddLast(pair);
        }
    }

    public void AddText(string text, string translation_1, string translation_2, string translation_3)
    {
        int index = GetIndex(text, Texts.Length);
        StringList sl = CheckKeysInTexts(index, text);
        if (sl != null)
        {
            bool have_1 = false;
            bool have_2 = false;
            bool have_3 = false;
            foreach (var s in sl)
            {
                if (s == translation_1) have_1 = true;
                if (s == translation_2) have_2 = true;
                if (s == translation_3) have_3 = true;
            }
            if (!have_1) sl.AddLast(translation_1);
            if (!have_2) sl.AddLast(translation_2);
            if (!have_3) sl.AddLast(translation_3);
        }
        else
        {
            StringList list = new StringList();
            list.AddLast(translation_1);
            list.AddLast(translation_2);
            list.AddLast(translation_3);
            KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(text, list);
            Texts[index].AddLast(pair);
        }
    }

    public void AddText(string text, string translation_1, string translation_2, string translation_3, string translation_4)
    {
        int index = GetIndex(text, Texts.Length);
        StringList sl = CheckKeysInTexts(index, text);
        if (sl != null)
        {
            bool have_1 = false;
            bool have_2 = false;
            bool have_3 = false;
            bool have_4 = false;
            foreach (var s in sl)
            {
                if (s == translation_1) have_1 = true;
                if (s == translation_2) have_2 = true;
                if (s == translation_3) have_3 = true;
                if (s == translation_4) have_4 = true;
            }
            if (!have_1) sl.AddLast(translation_1);
            if (!have_2) sl.AddLast(translation_2);
            if (!have_3) sl.AddLast(translation_3);
            if (!have_4) sl.AddLast(translation_4);
        }
        else
        {
            StringList list = new StringList();
            list.AddLast(translation_1);
            list.AddLast(translation_2);
            list.AddLast(translation_3);
            list.AddLast(translation_4);
            KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(text, list);
            Texts[index].AddLast(pair);
        }
    }

    public void AddText(string text, string translation_1, string translation_2, string translation_3, string translation_4, string translation_5)
    {
        int index = GetIndex(text, Texts.Length);
        StringList sl = CheckKeysInTexts(index, text);
        if (sl != null)
        {
            bool have_1 = false;
            bool have_2 = false;
            bool have_3 = false;
            bool have_4 = false;
            bool have_5 = false;
            foreach (var s in sl)
            {
                if (s == translation_1) have_1 = true;
                if (s == translation_2) have_2 = true;
                if (s == translation_3) have_3 = true;
                if (s == translation_4) have_4 = true;
                if (s == translation_5) have_5 = true;
            }
            if (!have_1) sl.AddLast(translation_1);
            if (!have_2) sl.AddLast(translation_2);
            if (!have_3) sl.AddLast(translation_3);
            if (!have_4) sl.AddLast(translation_4);
            if (!have_5) sl.AddLast(translation_5);
        }
        else
        {
            StringList list = new StringList();
            list.AddLast(translation_1);
            list.AddLast(translation_2);
            list.AddLast(translation_3);
            list.AddLast(translation_4);
            list.AddLast(translation_5);
            KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(text, list);
            Texts[index].AddLast(pair);
        }
    }

    public void AddText(string text, string[] translation)
    {
        int index = GetIndex(text, Texts.Length);
        StringList sl = CheckKeysInTexts(index, text);
        if (sl != null)
        {
            bool[] have = new bool[translation.Length];
            foreach (var s in sl)
            {
                for (int i = 0; i < translation.Length; ++i)
                {
                    if (s == translation[i]) have[i] = true;
                }
            }
            for (int i = 0; i < have.Length; ++i)
            {
                if (!have[i]) sl.AddLast(translation[i]);
            }
        }
        else
        {
            StringList list = new StringList();
            for (int i = 0; i < translation.Length; ++i) list.AddLast(translation[i]);
            KeyValuePair<string, StringList> pair = new KeyValuePair<string, StringList>(text, list);
            Texts[index].AddLast(pair);
        }
    }
}
