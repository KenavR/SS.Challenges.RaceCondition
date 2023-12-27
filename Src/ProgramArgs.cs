namespace SS.Challenges.RaceCondition;

class LoggingOptions
{
    public const string ALL = "all";
    public const string RUNS = "runs";
    public const string NONE = "none";
}

class SolutionOptions
{
    public const string NONE = "none";
    public const string LOCKS = "locks";
}
public class ProgramArgs
{
    const string TransactionArgName = "transactions";
    const string LoggingArgName = "logging";
    const string BalanceArgName = "balance";
    const string SolutionArgName = "solution";
    const string MaxRunsArgName = "maxRuns";

    public int TransactionsPerRun { get; set; } = 15;
    public string Logging { get; set; } = LoggingOptions.ALL;
    public int InitialBalance { get; set; } = 1000;
    public string Solution { get; set; } = SolutionOptions.NONE;
    public int MaxRuns { get; set; } = 1000;

    public static ProgramArgs CreateFromArgsArray(string[] args)
    {
        var arguments = new Dictionary<string, string>();

        foreach (string argument in args)
        {
            string[] splitted = argument.Split('=');

            if (splitted.Length == 2)
            {
                arguments[splitted[0]] = splitted[1];

            }
        }

        return new ProgramArgs()
        {
            TransactionsPerRun = arguments.Keys.Contains(TransactionArgName) ? Int32.Parse(arguments[TransactionArgName]) : 15,
            Logging = arguments.Keys.Contains(LoggingArgName) ? arguments[LoggingArgName] : LoggingOptions.RUNS,
            InitialBalance = arguments.Keys.Contains(BalanceArgName) ? Int32.Parse(arguments[BalanceArgName]) : 1000,
            Solution = arguments.Keys.Contains(SolutionArgName) ? arguments[SolutionArgName] : SolutionOptions.NONE,
            MaxRuns = arguments.Keys.Contains(MaxRunsArgName) ? Int32.Parse(arguments[MaxRunsArgName]) : 1000
        };
    }
}
