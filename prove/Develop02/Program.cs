using System;
using System.Collections.Generic;



class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        while (true)
        {
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Console.Write("Enter your responses: ");
                    String response = Console.ReadLine();
                    String prompt = GetRandomPrompt();
                    String date = DateTime.Now.ToString("YYYY-MM-dd HH:mm:ss");

                    JournalEntry newEntry = new JournalEntry(prompt, response, date );
                    journal.AddEntry(newEntry);
                    break;

                case 2:
                    Console.Clear();
                    journal.DisplayEntries();
                    break;
                case 3:
                    Console.Clear();
                    Console.Write("Enter the file to save: ");
                    String saveFileName = Console.ReadLine();
                    journal.SaveToFile(saveFileName);
                    break;
                case 4:
                    Console.Clear();
                    Console.Write("Enter the file name to load: ");
                    string loadFileName = Console.ReadLine();
                    journal.LoadFromFile(loadFileName);
                    break;
                case 5:
                    //Enviroment.Exit(O);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again. ");
                    break;
            }
        }
        
    }

    static string GetRandomPrompt()
    {
        List<string> prompts = new List<string>
        {
            "Who was the most intresting person I interacted with today? ",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If i had one thing I could do over today, what would it be?"

        };

        Random random = new Random ();
        return prompts[random.Next(prompts.Count)];
    }

    class JournalEntry 
    {
        public string Prompt {get; set; }
        public string Response { get; set; }
        public string Date { get; set; }

        public JournalEntry(string prompt, string response, string date )
        {
            Prompt = prompt;
            Response = response;
            Date = date;
        }

        public override string ToString()
        {
            return $"{Date}\nPrompt: {Prompt}\nResponse: {Response}\n";
        }
    }
    class Journal
    {
        private List<JournalEntry> entries;

        public Journal()
        {
            entries = new List<JournalEntry>();
        }
        public void AddEntry(JournalEntry entry)
        {
            entries.Add(entry);
        }
        public void DisplayEntries()
        {
            foreach ( var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }

        public void SaveToFile(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                foreach (var entry in entries)
                {
                    sw.WriteLine($"{entry.Date}, {entry.Prompt}, {entry.Response}");
                }
            }
        }

        public void LoadFromFile(string fileName)
        {
            entries.Clear();

            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    while (!sr.EndOfStream)
                    {
                        var line = sr.ReadLine();
                        var parts = line.Split(',');

                        if(parts.Length == 3)
                        {
                            var entry = new JournalEntry(parts[1], parts[2], parts[0]);
                            entries.Add(entry);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. Creating a new journal.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading journal: {ex.Message}");
            }
        }
    }
}