using System;
using System.Collections.Generic;
using System.IO;

class GoalFileManager
{
    public List<Base> GoalList { get; set; }

    public GoalFileManager()
    {
        GoalList = new List<Base>();
    }

    public void SaveGoals()
    {
        Console.WriteLine("Please enter the name of the file: ");
        string fileName = Console.ReadLine();
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var goal in GoalList)
                {
                    writer.WriteLine($"{goal.GoalName},{goal.GoalDescription},{goal.PointValue}");
                }
            }

            Console.WriteLine("Goals saved successfully.");
            GoalList.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving goals: {ex.Message}");
        }
    }

    public void LoadGoals()
    {
        Console.WriteLine("Please enter the name of the file you want to load: ");
        string fileName = Console.ReadLine();
        List<Base> loadedGoals = new List<Base>();

        try
        {
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            Base goal = new Base
                            {
                                GoalName = parts[0],
                                GoalDescription = parts[1],
                                PointValue = int.Parse(parts[2])
                            };
                            loadedGoals.Add(goal);
                        }
                    }
                }

                Console.WriteLine("Goals loaded successfully.");
            }
            else
            {
                Console.WriteLine("File not found. No goals loaded.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading goals: {ex.Message}");
        }

        GoalList = loadedGoals;
    }

    public void GoalMenu()
    {
        Console.WriteLine("----------------------");
        Console.WriteLine("1. Create new Simple Goal");
        Console.WriteLine("2. Create new Eternal Goal");
        Console.WriteLine("3. Create new Check List Goal");
        Console.WriteLine("----------------------");
        int userInput = int.Parse(Console.ReadLine());

        if (userInput == 1)
        {
            SimpleGoal simpleGoal = new SimpleGoal();
            simpleGoal.GetValues();
            GoalList.Add(simpleGoal);
        }
        else if (userInput == 2)
        {
            EternalGoal eternalGoal = new EternalGoal();
            eternalGoal.GetValues();
            GoalList.Add(eternalGoal);
        }
        else if (userInput == 3)
        {
            ChecklistGoal checklistGoal = new ChecklistGoal();
            checklistGoal.GetValues();
            checklistGoal.GetBonusPoints();
            GoalList.Add(checklistGoal);
        }
    }
}

class DisplayGoals
{
    public void PrintGoalList(List<Base> GoalList)
    {
        Console.WriteLine("Items in list:");
        foreach (var goal in GoalList)
        {
            Console.WriteLine($"Goal Name: {goal.GoalName}, Description: {goal.GoalDescription}, Point Value: {goal.PointValue}");
        }
    }
}

class Base
{
    public string GoalName { get; set; }
    public string GoalDescription { get; set; }
    public int PointValue { get; set; }
    public int PointTotal { get; set; }

    public Base()
    {

    }

    public void goalMenu(List<Base> GoalList)
    {
        Console.WriteLine("----------------------");
        Console.WriteLine("1. Create new Simple Goal");
        Console.WriteLine("2. Create new Eternal Goal");
        Console.WriteLine("3. Create new Check List Goal");
        Console.WriteLine("----------------------");
        int userInput = int.Parse(Console.ReadLine());

        if (userInput == 1)
        {
            SimpleGoal simpleGoal = new SimpleGoal();
            simpleGoal.GetValues();
            GoalList.Add(simpleGoal);
        }
        else if (userInput == 2)
        {
            EternalGoal eternalGoal = new EternalGoal();
            eternalGoal.GetValues();
            GoalList.Add(eternalGoal);
        }
        else if (userInput == 3)
        {
            ChecklistGoal checklistGoal = new ChecklistGoal();
            checklistGoal.GetValues();
            checklistGoal.GetBonusPoints();
            GoalList.Add(checklistGoal);
        }
    }

    public virtual void GetValues()
    {
        Console.WriteLine("Enter goal name: ");
        GoalName = Console.ReadLine();
        Console.WriteLine("Enter goal description: ");
        GoalDescription = Console.ReadLine();
        Console.WriteLine("Enter point value: ");
        PointValue = int.Parse(Console.ReadLine());
    }
}

class UpdateGoals
{
    public void AddPoints(List<Base> GoalList)
    {
        Console.WriteLine("Which goal did you complete?");
        string userInput = Console.ReadLine();

        Base completedGoal = GoalList.Find(goal => goal.GoalName == userInput);

        if (completedGoal != null)
        {
            completedGoal.PointTotal += completedGoal.PointValue;
            Console.WriteLine($"Your total score is: {completedGoal.PointTotal}");
        }
        else
        {
            Console.WriteLine("Goal not found.");
        }
    }
}

class ChecklistGoal : Base
{
    public int GoalCount { get; set; }
    public int BonusPoints { get; set; }

    public ChecklistGoal()
    {

    }

    public override void GetValues()
    {
        base.GetValues();
    }

    public void GetBonusPoints()
    {
        Console.WriteLine("How many times do you want to complete this goal before receiving bonus points? ");
        GoalCount = int.Parse(Console.ReadLine());
        Console.WriteLine("How many bonus points do you want for completing the goals: ");
        BonusPoints = int.Parse(Console.ReadLine());
    }
}

class EternalGoal : Base
{

    public EternalGoal()
    {

    }

    public override void GetValues()
    {
        base.GetValues();
    }
}

class SimpleGoal : Base
{
    public SimpleGoal()
    {

    }

    public override void GetValues()
    {
        base.GetValues();
    }
}

class Program
{
    static void Main(string[] args)
    {
        GoalFileManager goalManager = new GoalFileManager();
        DisplayGoals displayGoals = new DisplayGoals();
        UpdateGoals updateGoals = new UpdateGoals();

        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Add Goal");
            Console.WriteLine("2. Print Goals");
            Console.WriteLine("3. Add Points");
            Console.WriteLine("4. Save Goals");
            Console.WriteLine("5. Load Goals");
            Console.WriteLine("6. Exit");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    goalManager.GoalMenu();
                    break;
                case 2:
                    displayGoals.PrintGoalList(goalManager.GoalList);
                    break;
                case 3:
                    updateGoals.AddPoints(goalManager.GoalList);
                    break;
                case 4:
                    goalManager.SaveGoals();
                    break;
                case 5:
                    goalManager.LoadGoals();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose again.");
                    break;
            }
        }
    }
}
