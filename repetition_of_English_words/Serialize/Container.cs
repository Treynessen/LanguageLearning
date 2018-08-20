using System;
using System.Collections.Generic;
using StringList = System.Collections.Generic.LinkedList<string>;

[Serializable]
public sealed class Container
{
    public LinkedList<KeyValuePair<string, StringList>>[] Words { get; private set; }
    public LinkedList<KeyValuePair<string, StringList>>[] Texts { get; private set; }
    public LinkedList<KeyValuePair<string, WordsAndTexts.Type>> Incomprehensible { get; private set; }
    public Container(LinkedList<KeyValuePair<string, StringList>>[] words,
        LinkedList<KeyValuePair<string, StringList>>[] texts,
        LinkedList<KeyValuePair<string, WordsAndTexts.Type>> incomprehensible)
    {
        Words = words;
        Texts = texts;
        Incomprehensible = incomprehensible;
    }
}