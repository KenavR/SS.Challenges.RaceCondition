namespace SS.Challenges.RaceCondition.Bank;

public interface IBankAccount
{
    int Balance { get; set; }
    void Deposit(int amount, bool logging = true);
    void Withdraw(int amount, bool logging = true);
}

public class BankAccount : IBankAccount
{
    public int Balance { get; set; }


    public BankAccount(int balance)
    {
        Balance = balance;
    }

    public void Deposit(int amount, bool logging = true)
    {
        Balance += amount;
        if (logging) { Console.WriteLine($"{DateTime.UtcNow.ToString("HH:mm:ss.fff")}: ${amount} was added to the account balance!"); }
    }

    public void Withdraw(int amount, bool logging = true)
    {
        Balance -= amount;
        if (logging) { Console.WriteLine($"{DateTime.UtcNow.ToString("HH:mm:ss.fff")}: ${amount} was withdrawn of the account balance!"); }
    }
}
