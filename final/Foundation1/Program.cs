using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        Video video1 = new Video("My First Video", "John Doe", 60);
        video1.AddComment("Alice", "Great video! You will make amazing content in the months to come!");
        video1.AddComment("Noah", "Thanks for sharing Bro.");
        videos.Add(video1);

        Video video2 = new Video("How to Create sick tracks for hot wheels", "Jake christensen", 120);
        video2.AddComment("Felix", "This was very helpful! Super rad video");
        video2.AddComment("Caden", "I'm going to try this out.");
        videos.Add(video2);

        Video video3 = new Video("Cute Cat Compilation", "Emma Lee", 180);
        video3.AddComment("Frank", "This made my day!");
        video3.AddComment("Grace", "So adorable!");
        video3.AddComment("Henry", "I can't stop watching.");
        videos.Add(video3);

        foreach (Video video in videos)
        {
            Console.WriteLine("Title: {0}", video.Title);
            Console.WriteLine("Author: {0}", video.Author); // Fix here
            Console.WriteLine("Length: {0} seconds", video.Length);
            Console.WriteLine("Number of comments: {0}", video.GetNumberOfComments());

            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine("{0}: {1}", comment.Name, comment.Text); // Fix here
            }

            Console.WriteLine();
        }
    }
}

class Video
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int Length { get; private set; }
    public List<Comment> Comments { get; private set; }

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        Length = length;
        Comments = new List<Comment>();
    }

    public void AddComment(string name, string text)
    {
        Comment comment = new Comment(name, text);
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }
}

class Comment
{
    public string Name { get; private set; }
    public string Text { get; private set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }
}
