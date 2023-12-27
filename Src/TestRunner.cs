using SS.Challenges.RaceCondition.Bank;

namespace SS.Challenges.RaceCondition;

public class TestRunner
{
    private List<int> GetRandomIntegers(int min, int max, int count)
    {
        List<int> rdmIntList = new List<int>();
        var rdm = new Random();
        for (int i = 0; i <= count; i++)
        {
            rdmIntList.Add(rdm.Next(min, max));
        }

        return rdmIntList;
    }

    private int CalculateBalanceInSingleThread(IBankAccount account, List<int> depositions, List<int> withdrawals)
    {
        depositions.ForEach(d => account.Deposit(d, false));
        withdrawals.ForEach(w => account.Withdraw(w, false));

        return account.Balance;
    }

    private void LogHeader(int run, IBankAccount account, int correctBalance, string logging)
    {
        switch (logging)
        {
            case LoggingOptions.ALL:
                Console.WriteLine("Start performing depositions and withdrawals...");
                Console.WriteLine($"The current account balance is {account.Balance} and the balance at the end should be {correctBalance}");
                Console.WriteLine("-------------------------------------------------");
                break;
            case LoggingOptions.RUNS:
                Console.WriteLine($"Performing run {run}!");
                break;
            default:
                break;
        }
    }

    private void LogFooter(IBankAccount account, string logging)
    {
        switch (logging)
        {
            case LoggingOptions.ALL:
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Finished performing depositions and withdrawals...");
                Console.WriteLine($"The Balance of the account is: {account.Balance}");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("");
                break;
            default:
                break;
        }
    }

    private IBankAccount GetBankAccount(ProgramArgs args)
    {
        switch (args.Solution) {
            case SolutionOptions.LOCKS:
                return new LockedBankAccount(args.InitialBalance);
            default:
                return new BankAccount(args.InitialBalance);
        }
    }

    private async Task<bool> PerformTask(int run, ProgramArgs args)
    {
        // Setup
        var account = this.GetBankAccount(args);
        var bank = new BankService();

        // Generate random depositions and withdrawals
        var rdm = new Random();
        var depositions = GetRandomIntegers(50, 1000, args.TransactionsPerRun);
        var withdrawals = GetRandomIntegers(50, 1000, args.TransactionsPerRun);

        // Calculate what balance should after depositions and withdrawals
        var correctBalance = CalculateBalanceInSingleThread(new BankAccount(account.Balance), depositions, withdrawals);

        // Perform depositions and withdrawals
        this.LogHeader(run, account, correctBalance, args.Logging);
        await Task.WhenAll(new List<Task>() { bank.PerformDepositions(account, depositions, args.Logging == LoggingOptions.ALL), bank.PerformWithdrawals(account, withdrawals, args.Logging == LoggingOptions.ALL) }.ToArray());
        this.LogFooter(account, args.Logging);

        if (correctBalance != account.Balance)
        {
            Console.WriteLine($"Error: A race condition occured at run {run}! The correct balance should be {correctBalance} but the new account balance is {account.Balance}!");
            return false;
        }
        return true;
    }

    public async Task Run(ProgramArgs args)
    {
        // Perform until failure or maxRuns
        int i = 1;
        while (await PerformTask(i, args) && i < args.MaxRuns) { i++; }

        if (i >= args.MaxRuns)
        {
            Console.WriteLine("Success or Luck: No Race Condition occured!");
        }

    }

}
