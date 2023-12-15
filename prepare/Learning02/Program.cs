using System;
using System.Collections.Generic;
using System.Threading;

public class MainMenu
{
    int BreathingActivityCount = 0;
    int ReflectingActivityCount = 0;
    int ListingActivityCount = 0;

    public string OptionsDisplay()
    {
        string input = "";
        do
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("    1. Start Breathing Activity");
            Console.WriteLine("    2. Start Reflecting Activity");
            Console.WriteLine("    3. Start Listing Activity");
            Console.WriteLine("    4. Count The Number Of Activities Completed");
            Console.WriteLine("    5. Quit");
            Console.Write("Select a choice from the menu: ");
            input = Console.ReadLine();
            Console.Clear();
        } while (input != "1" && input != "2" && input != "3" && input != "4" && input != "5");

        return input;
    }

    public FullActivity PickActivity(string input)
    {
        FullActivity activity = new FullActivity();

        if (input == "1")
        {
            activity = new BreathingActivity();
            BreathingActivityCount += 1;
        }
        else if (input == "2")
        {
            activity = new ReflectingActivity();
            ReflectingActivityCount += 1;
        }
        else if (input == "3")
        {
            activity = new ListingActivity();
            ListingActivityCount += 1;
        }
        else if (input == "4")
        {
            Console.WriteLine("These are number of activities completed: ");
            Console.WriteLine();
            Console.WriteLine($"Breathing Activity: {BreathingActivityCount}");
            Console.WriteLine($"Reflecting Activity: {ReflectingActivityCount}");
            Console.WriteLine($"Listing Activity: {ListingActivityCount}");
            Console.WriteLine();
        }
        return activity;
    }

    public void ExecuteActivity(FullActivity activity)
    {
        activity.ActivitySetup();
        activity.StartActivity();
        activity.FinishActivity();
    }
}

class Program
{
    static void Main(string[] args)
    {
        MainMenu main = new MainMenu();
        string input = main.OptionsDisplay();

        while (input != "5")
        {
            if (input == "1" || input == "2" || input == "3")
            {
                FullActivity activity = main.PickActivity(input);
                main.ExecuteActivity(activity);
                input = main.OptionsDisplay();
            }
            else if (input == "4")
            {
                FullActivity activity = main.PickActivity(input);
                input = main.OptionsDisplay();
            }
        }
    }
}

public class ReflectingActivity : FullActivity
{
    private Random _random = new Random();
    private List<string> Prompts = new List<string>()
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> QuestionPrompt = new List<string>()
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectingActivity()
    {
        SetActivityName("Reflecting Activity");
        SetDescription("This activity will help you reflect on questions that will be provided to you.");
    }

    public override void DisplayPrompt()
    {
        Console.WriteLine("Take time to ponder on the questions that will be provided to you: ");
        Console.WriteLine($"--{Prompts[_random.Next(Prompts.Count)]}");
        Console.WriteLine("You can start in: ");
        UseTimer().CountDownFrom(5);
        Questions();
    }

    private void Questions()
    {
        int numQuestions = (GetDuration() / 10) + 1;

        for (int i = 0; i < numQuestions; i++)
        {
            Console.Write(">" + QuestionPrompt[_random.Next(QuestionPrompt.Count)]);
            UseTimer().LoadingScreen();
            Console.WriteLine();
        }
    }

    public override void StartActivity()
    {
        DisplayPrompt();
    }
}

public class Timer
{
    public Timer()
    {
    }

    public void LoadingScreen()
    {
        List<string> animationStrings = new List<string>() { "|", "/", "-", "\\", "|", "/", "-", "\\" };
        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(5);

        while (DateTime.Now < endTime)
        {
            foreach (string s in animationStrings)
            {
                Console.Write(s);
                Thread.Sleep(500);
                Console.Write("\b \b");
            }
        }
    }

    public void CountDownFrom(int num)
    {
        for (int i = num; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    public DateTime GetFutureTime(int seconds)
    {
        DateTime startTime = DateTime.Now;
        DateTime futureTime = startTime.AddSeconds(seconds);

        return futureTime;
    }

    public bool TimesUp(DateTime futureTime)
    {
        DateTime currentTime = DateTime.Now;

        if (currentTime > futureTime)
        {
            return true;
        }
        return false;
    }
}

public class ListingActivity : FullActivity
{
    private Random _random = new Random();
    private List<string> Prompts = new List<string>()
    {
        "Who is someone you looked up to?",
        "What is your biggest fear?",
        "Who are your closest friends?",
        "How have you improved this year?"
    };

    public ListingActivity()
    {
        SetActivityName("Listening Activity");
        SetDescription("This activity will help you practice listening.");
    }

    public override void DisplayPrompt()
    {
        Console.WriteLine("List as many responses to the question as you can: ");
        Console.WriteLine($"--{Prompts[_random.Next(Prompts.Count)]}");
        Console.WriteLine("You can start in: ");
        UseTimer().CountDownFrom(5);
        Console.WriteLine();
    }

    private void ListOfThoughts()
    {
        DateTime future = UseTimer().GetFutureTime(GetDuration());
        int items = 0;

        while (!UseTimer().TimesUp(future))
        {
            Console.WriteLine("> ");
            Console.ReadLine();
            items++;
        }

        Console.WriteLine($"You wrote a total of {items} thoughts");
        UseTimer().LoadingScreen();
        Console.WriteLine();
    }

    public override void StartActivity()
    {
        DisplayPrompt();
        ListOfThoughts();
    }
}

public class FullActivity
{
    private string _activityName;
    private string _description;
    private int _duration;
    private Timer _time;

    public FullActivity()
    {
        _time = new Timer();
    }

    public virtual void DisplayPrompt() { }
    public virtual void StartActivity() { }

    public void SetActivityName(string activityName)
    {
        _activityName = activityName;
    }

    public string GetActivityName()
    {
        return _activityName;
    }

    public void SetDescription(string description)
    {
        _description = description;
    }

    public string GetDescription()
    {
        return _description;
    }

    public void SetDuration(int duration)
    {
        _duration = duration;
    }

    public int GetDuration()
    {
        return _duration;
    }

    public void ActivitySetup()
    {
        Console.Write($"Welcome to the {_activityName}.\n");
        _time.LoadingScreen();
        Console.WriteLine(_description);
        Console.WriteLine("How long, in seconds, would you like for your session?");

        int duration;
        while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid, positive number for the duration.");
        }

        _duration = duration;

        Console.Clear();

        Console.Write("Get ready...");
        _time.CountDownFrom(4);
        Console.Clear();
        Thread.Sleep(500);
    }

    public void FinishActivity()
    {
        Console.WriteLine("Well Done!!");
        Console.WriteLine($"You have finished {_duration} seconds of the {_activityName}");
        _time.LoadingScreen();
        Console.Clear();
    }

    public Timer UseTimer()
    {
        return _time;
    }
}

public class BreathingActivity : FullActivity
{
    public BreathingActivity()
    {
        SetActivityName("Breathing Activity");
        SetDescription("This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.");
    }

    public override void StartActivity()
    {
        BreathingExercise();
    }

    public void BreathingExercise()
    {
        int breaths = (GetDuration() / 20) + 1;

        for (int i = 0; i < breaths; i++)
        {
            Console.Write($"Breath in...");
            UseTimer().CountDownFrom(4);
            Thread.Sleep(500);
            Console.WriteLine();
            Console.Write("Now breath out...");
            UseTimer().CountDownFrom(6);
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
