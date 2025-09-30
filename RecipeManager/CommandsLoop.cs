using RecipeManager.Commands;

namespace RecipeManager;

public class CommandsLoop(Context context)
{
    public void Run()
    {
        Console.WriteLine("RECIPE MANAGER:)))). Type 'exit' if you " +
                          "want to abandon us or 'help' if you are lost a little bit.");
        while (true)
        {
            Console.Write("* ");
            var line = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            var args = SplitArgs(line);
            var executionResult = context.GetExecutionResult(args, out var error);
            if (executionResult == ExecuteResult.Error
                && error != null) Console.WriteLine(error);
            if(executionResult == ExecuteResult.Break)break;
        }
    }

    private static string[] SplitArgs(string input)
    {
        var result = new List<string>();
        var inQuotes = false;
        var current = "";

        foreach (var ch in input.Trim())
        {
            if (ch == '"')
            {
                inQuotes = !inQuotes;
                continue;
            }

            if (!inQuotes && char.IsWhiteSpace(ch))
            {
                if (current.Length <= 0) continue;
                result.Add(current);
                current = "";
            }
            else current += ch;
        }

        if (current.Length > 0) result.Add(current);
        return result.ToArray();
    }
}