
using System.Text.Json;

public class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("This app was just created to test some stuff while inside a container");
        Console.WriteLine("Type a command (type 'exit' to quit):");

        while (true)
        {
            Console.Write("> "); // Display a prompt
            string? input = Console.ReadLine();

            // Exit condition
            if (input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            // Handle the command
            await ProcessCommand(input);
        }
    }

    static async Task ProcessCommand(string command)
    {
        switch (command)
        {
            case "trivia":
                await FetchTrivia();
                break;
            default:
                Console.WriteLine($"Unknown command: {command}");
                break;
        }
    }

    static async Task FetchTrivia()
    {
        const string url = "https://opentdb.com/api.php?amount=1";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                Console.WriteLine("Fetching trivia questions...");
                string response = await client.GetStringAsync(url);

                // Parse JSON response
                var jsonResponse = JsonDocument.Parse(response);
                var results = jsonResponse.RootElement.GetProperty("results");

                foreach (var question in results.EnumerateArray())
                {
                    string questionText = question.GetProperty("question").GetString();
                    Console.WriteLine($"- {System.Web.HttpUtility.HtmlDecode(questionText)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching trivia: {ex.Message}");
            }
        }
    }
}