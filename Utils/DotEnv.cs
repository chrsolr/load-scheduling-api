public class DotEnv
{
    public static void Load(string filepath = ".env")
    {
        if (!File.Exists(filepath))
        {
            return;
        }

        foreach (var line in File.ReadAllLines(filepath))
        {
            var parts = line.Split('=', 2);

            if (parts.Length != 2)
            {
                continue;
            }

            Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}
