using System;

public sealed class Changes : EventArgs
{
    public TypeChanges Type { get; private set; }
    public string WordOrText { get; private set; }

    public Changes(string word_or_text, TypeChanges type)
    {
        WordOrText = word_or_text;
        Type = type;
    }
}

public enum TypeChanges
{
    WordDeleted,
    TextDeleted,
    WordAdded,
    TextAdded
}

public sealed class DictionaryChanges
{
    public event EventHandler<Changes> WordOrTextAddedEvent;
    public event EventHandler<Changes> WordOrTextDeletedEvent;

    public DictionaryChanges Add_AddWordsOrTextForms(AddWordsOrTextForms form)
    {
        if (form != null) form.AddWordOrTextEvent += WordOrTextAddedEvent;
        return this;
    }

    public DictionaryChanges Add_DictionaryEditForm(DictionaryEditForm form)
    {
        if (form != null) form.WordOrTextDeleteEvent += WordOrTextDeletedEvent;
        return this;
    }
}
