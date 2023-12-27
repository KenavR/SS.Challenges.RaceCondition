namespace SS.Challenges.RaceCondition.Bank;

public class LockedBankAccount : IBankAccount
{
    public int Balance { get; set; }
    public LockedBankAccount(int balance)
    {
        Balance = balance;
    }

    public void Deposit(int amount, bool logging = true)
    {
        lock (this)
        {
            Balance += amount;
        }
        if (logging) { Console.WriteLine($"{DateTime.UtcNow.ToString("HH:mm:ss.fff")}: ${amount} was added to the account balance!"); }
    }

    public void Withdraw(int amount, bool logging = true)
    {
        lock (this)
        {
            Balance -= amount;
        }
        if (logging) { Console.WriteLine($"{DateTime.UtcNow.ToString("HH:mm:ss.fff")}: ${amount} was withdrawn of the account balance!"); }
    }
}
