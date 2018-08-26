using System;
using System.Collections.Generic;
using LinkedListWithTranslations = System.Collections.Generic.LinkedList<string>;
using IncomprehensiblePair = System.Collections.Generic.KeyValuePair<string, WordsAndTextsData.WordOrText>;

[Serializable]
public sealed class Container
{
    public LinkedList<KeyValuePair<string, LinkedListWithTranslations>>[] Words { get; private set; }
    public LinkedList<KeyValuePair<string, LinkedListWithTranslations>>[] Texts { get; private set; }
    public LinkedList<IncomprehensiblePair> Incomprehensible { get; private set; }
    public Container(LinkedList<KeyValuePair<string, LinkedListWithTranslations>>[] words,
        LinkedList<KeyValuePair<string, LinkedListWithTranslations>>[] texts,
        LinkedList<IncomprehensiblePair> incomprehensible)
    {
        Words = words;
        Texts = texts;
        Incomprehensible = incomprehensible;
    }
}