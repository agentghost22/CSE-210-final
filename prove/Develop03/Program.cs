using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world, that he gave his only Son...");

        while (!scripture.AllWordsHidden)
        {
            Console.Clear();
            Console.WriteLine(scripture.Display());
            Console.WriteLine("Press Enter to hide more words or type 'quit' to exit.");

            string userInput = Console.ReadLine();

            if (userInput.ToLower() == "quit")
                break;

            scripture.HideRandomWords();
        }

        Console.WriteLine("Program ended. Press any key to exit.");
        Console.ReadKey();
    }
}

class Scripture
{
    private List<Word> words = new List<Word>();
    private Random random = new Random();

    public ScriptureReference Reference { get; }

    public bool AllWordsHidden => words.All(word => word.Hidden);

    public Scripture(string reference, string text)
    {
        Reference = new ScriptureReference(reference);
        words.AddRange(text.Split(' ').Select(word => new Word(word)));
    }

    public void HideRandomWords()
    {
        int wordsToHide = random.Next(1, 4); // You can adjust the number of words to hide
        List<Word> visibleWords = words.Where(word => !word.Hidden).ToList();

        for (int i = 0; i < wordsToHide && i < visibleWords.Count; i++)
        {
            int indexToHide = random.Next(visibleWords.Count);
            visibleWords[indexToHide].Hide();
        }
    }

    public string Display()
    {
        return $"{Reference.Display()} {string.Join(" ", words.Select(word => word.Display()))}";
    }
}

class ScriptureReference
{
    public string Reference { get; }

    public ScriptureReference(string reference)
    {
        Reference = reference;
    }

    public string Display()
    {
        return $"[{Reference}]";
    }
}

class Word
{
    public string Text { get; }
    public bool Hidden { get; private set; }

    public Word(string text)
    {
        Text = text;
    }

    public void Hide()
    {
        Hidden = true;
    }

    public string Display()
    {
        return Hidden ? "___" : Text;
    }
}
