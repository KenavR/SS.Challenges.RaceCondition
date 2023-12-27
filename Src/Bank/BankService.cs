namespace SS.Challenges.RaceCondition.Bank;

public class BankService
{
    public async Task PerformDepositions(IBankAccount bankAccount, List<int> depositions, bool withLogging = true)
    {
        await Task.Factory.StartNew(() =>
        {
            depositions.ForEach(d => bankAccount.Deposit(d, withLogging));
        });
    }

    public async Task PerformWithdrawals(IBankAccount bankAccount, List<int> withdrawals, bool withLogging = true)
    {
        await Task.Factory.StartNew(() =>
        {
            withdrawals.ForEach(w => bankAccount.Withdraw(w, withLogging));
        });
    }
}
