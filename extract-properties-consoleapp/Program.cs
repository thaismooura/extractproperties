using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        // Set the directory and file to read
        var directory = "";
        var fileName = "";

        // Read the file into a string
        var filePath = Path.Combine(directory, fileName);
        var fileText = File.ReadAllText(filePath);

        // Define the regular expression pattern to match properties and their summaries
        var pattern = @"(///\s*<summary>\s*((?:\/\/\/\s*)*.*?)\s*<\/summary>.*?)\s*(public|private|protected)\s+(\w+)\s+(\w+)\s*\{";

        // Find all matches of the pattern in the file text
        var matches = Regex.Matches(fileText, pattern, RegexOptions.Singleline);

        // Create a table to hold the properties and their summaries
        var table = new[]
        {
            new { Visibility = "", Type = "", Description = "" }
        }.ToList();

        // Loop through each match and add it to the table
        foreach (Match match in matches)
        {
            var visibility = match.Groups[4].Value;
            var type = match.Groups[5].Value;
            var summary = match.Groups[2].Value.Replace("///", "").Trim();

            table.Add(new { Visibility = visibility, Type = type,  Description = summary });
        }

        // Output the table in markdown syntax
        Console.WriteLine("| FieldType| FieldName | Description |");
        Console.WriteLine("|------------|------|-------------|");

        foreach (var item in table)
        {
            Console.WriteLine($"| {item.Visibility,-10} | {item.Type,-4} | {item.Description,-11} |");
        }
    }
}