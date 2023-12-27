# Race Condition Callenge (Group 3)
by Günter Arzberger, Thomas Leber and Rene Vanek

## Challenge Description
A race condition occurs in a program when the behavior depends on the relative timing of events, such as
the order of thread execution. This challenge focuses on creating a simple scenario where a race condition
can occur.

## Scenario
Imagine a shared bank account managed by two functions: deposit and withdraw. Both functions access
and modify the account balance. Your task is to implement these functions in a way that exposes a race
condition.

## Instructions
Implement the `BankAccount` class with the following methods:
```typescript
interface BankAccount {
  _init_(self, balance: int); // Initializes the bank account with the given balance.
  deposit(self, amount: int): None; // Deposits the specified amount into the
  withdraw(self, amount: int): None; // Withdraws the specified amount from the account
}
```

Create two threads, one for deposit and one for withdrawal, both operating on the same BankAccount instance.

Demonstrate the race condition by showing how the final balance can be incorrect due to the interleaved execution of deposit and withdrawal operations.

## Constraints
The initial account balance is a positive integer.
Both deposit and withdrawal amounts are positive integers.
The threads should perform multiple operations (deposits and withdrawals) to increase the chances of a race condition.

**Note:**
The initial account balance is a positive integer.
Both deposit and withdrawal amounts are positive integers.
The threads should perform multiple operations (deposits and withdrawals) to increase the chances of a
race condition.

## Implementation
The `BankAccount` class is implemented as described above. The `BankService` performs the depositions and withdrawals in two threads concurrently.

The Application runs multiple scenarios until it fails with a race condition. The program provides the following optional arguments:

```
int balance: The initial balance of the account. Default: 1000
int transactions: Amount of depositions and withdrawals made in one test scenario. Default: 15
string logging: Defines which information should be shown in the console. Default: runs, Options: all | runs | none
string solution: Defines if the program should run with a solution. Default: none, MaxRuns: 1000, Options: none | locks
int maxRuns: Maximum number of test scenarios. Default: 1000
```

Run the program
```sh
racecondition.exe balance=1000 transactions=15 logging=runs solution=none
```

Example output with the above arguments
```sh
...
Performing run 24!
Performing run 25!
Performing run 26!
Performing run 27!
Error: A race condition occured at run 27! The correct balance should be 682 but the new account balance is 8743!
```

## Solution 1: Locks
In this solution the account balance is locked before performing a deposition or withdrawal

```csharp
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
        ...
    }

    public void Withdraw(int amount, bool logging = true)
    {
        lock (this)
        {
            Balance -= amount;
        }
        ...
    }
}
```

Run the Program with solution

```sh
racecondition.exe maxRuns=10000 solution=locks
```

Example output with above arguments
```sh
...
Performing run 9995!
Performing run 9996!
Performing run 9997!
Performing run 9998!
Performing run 9999!
Performing run 10000!
Success or Luck: No Race Condition occured!
```