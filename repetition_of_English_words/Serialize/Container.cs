using System;
using System.Collections.Generic;
using StringList = System.Collections.Generic.LinkedList<string>;
using Pair = System.Collections.Generic.KeyValuePair<System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.LinkedList<string>>, WordsAndTexts.Type>;

[Serializable]
public sealed class Container
{
    public LinkedList<KeyValuePair<string, StringList>>[] Words { get; private set; }
    public LinkedList<KeyValuePair<string, StringList>>[] Texts { get; private set; }
    public LinkedList<Pair> Incomprehensible { get; private set; }
    public Container(LinkedList<KeyValuePair<string, StringList>>[] words,
        LinkedList<KeyValuePair<string, StringList>>[] texts,
        LinkedList<Pair> incomprehensible)
    {
        Words = words;
        Texts = texts;
        Incomprehensible = incomprehensible;
    }
}