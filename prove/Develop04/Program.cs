using System;
// I might have to extend more
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness Activities Menu:");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");

            Console.Write("Choose an activity (1-4): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    MindfulnessActivity breathingActivity = new BreathingActivity();
                    breathingActivity.StartActivity(GetDuration());
                    break;

                case "2":
                    MindfulnessActivity reflectionActivity = new ReflectionActivity();
                    reflectionActivity.StartActivity(GetDuration());
                    break;

                case "3":
                    MindfulnessActivity listingActivity = new ListingActivity();
                    listingActivity.StartActivity(GetDuration());
                    break;

                case "4":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }

    static int GetDuration()
    {
        Console.Write("Enter the duration in seconds: ");
        int duration;
        while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a positive integer.");
            Console.Write("Enter the duration in seconds: ");
        }
        return duration;
    }
}


public class MindfulnessActivity
{
    protected string name;
    protected string description;

    public MindfulnessActivity(string name, string description)
    {
        this.name = name;
        this.description = description;
    }

    public void StartActivity(int duration)
    {
        DisplayStartingMessage();
        PauseForPreparation();

        PerformActivity(duration);

        DisplayEndingMessage(duration);
        PauseBeforeFinishing();
    }

    protected virtual void DisplayStartingMessage()
    {
        Console.WriteLine($"Starting {name} activity. {description}");
    }

    protected virtual void PerformActivity(int duration)
    {
        
    }

    protected virtual void DisplayEndingMessage(int duration)
    {
        Console.WriteLine($"Good job! You completed the {name} activity for {duration} seconds.");
    }

    protected void PauseForPreparation()
    {
        Console.WriteLine("Get ready...");
        CountdownTimer(5); 
    }

    protected void PauseBeforeFinishing()
    {
        CountdownTimer(5); 
    }

    protected void CountdownTimer(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($"{i} ");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }

    protected void SpinnerAnimation(int seconds)
    {
        Console.Write("Preparing ");
        for (int i = 0; i < seconds; i++)
        {
            Console.Write(".");
            Thread.Sleep(1000);
        }
        Console.WriteLine();
    }
}


public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing", "This activity will help you relax by walking you through breathing in and out slowly.")
    {
    }

    protected override void PerformActivity(int duration)
    {
        Console.WriteLine("Clear your mind and focus on your breathing.");

        for (int i = 0; i < duration; i++)
        {
            Console.WriteLine("Breathe in...");
            SpinnerAnimation(2); 
            Console.WriteLine("Breathe out...");
            SpinnerAnimation(2); 
        }
    }
}


public class ReflectionActivity : MindfulnessActivity
{
    private string[] prompts = 
    {
        "Think of a time when you did something really difficult.",
        "Think of a time when you did something truly selfless."
    };

    private string[] reflectionQuestions =
     {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "What did you learn about yourself through this experience?",
     };    
        public ReflectionActivity() : base("Reflection", "This activity will help you reflect on times in your life when you have shown strength and resilience.")
    {

    }

    protected override void PerformActivity(int duration)
    {
        Random random = new Random();

        for (int i = 0; i < duration; i++)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);

            foreach (var question in reflectionQuestions)
            {
                Console.WriteLine(question);
                SpinnerAnimation(3); 
            }
        }
    }
}

public class ListingActivity : MindfulnessActivity
{
    private string[] listingPrompts = {
        "Who are people that you appreciate?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
    }

    protected override void PerformActivity(int duration)
    {
        Random random = new Random();
        string prompt = listingPrompts[random.Next(listingPrompts.Length)];

        Console.WriteLine(prompt);
        SpinnerAnimation(3); 
        Console.WriteLine("Start listing...");

      for (int i = 0; i < duration; i++)
        {
            
            Console.WriteLine($"Item {i + 1}");
            SpinnerAnimation(3); 
        }

        Console.WriteLine($"You listed {duration} items.");
    }
}