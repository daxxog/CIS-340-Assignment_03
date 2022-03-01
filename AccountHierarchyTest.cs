/* ================================================
 * @author     David Volm aka VOLMINATOR aka daXXog
 * @date       Sun Feb 27 17:10:06 CST 2022
 * @school     UWSP
 * @class      CIS 340
 * @section    01
 * @assignment 03
 * @professor  Hardeep Kaur Dhalla
 * @licence    MIT
 * ===============================================*/

using System;


namespace AccountHierarchy {
    class AccountHierarchyTest {
        // accounts
        static readonly Account[] accounts = {
            new Account(50.00m),
            new SavingsAccount(25.00m, 0.03m),
            new CheckingAccount(80.00m, 1.00m)
        };

        private static void _isCheckingAccountThenPrintFee(Account account) {
            // print some extra info when we perform and action on a CheckingAccount
            if (account is CheckingAccount) {
                CheckingAccount checkingAccount = (CheckingAccount) account;
                System.Console.WriteLine($"${checkingAccount.txFee} transaction fee charged.");
            }
        }

        static void Main(string[] args) {
            // some formatting lambdas / test functions
            Action<int> printBalance = _accountNum =>
                System.Console.WriteLine($"account{_accountNum} balance: ${accounts[_accountNum - 1].Balance}");

            Action<int, decimal> attemptDebit = (_accountNum, _amount) => {
                System.Console.WriteLine($"Attempting to debit account{_accountNum} by ${_amount}.");
                Account account = accounts[_accountNum - 1];
                account.Debit(_amount);
                AccountHierarchyTest._isCheckingAccountThenPrintFee(account);
            };

            Action<int, decimal> attemptCredit = (_accountNum, _amount) => {
                System.Console.WriteLine($"Crediting ${_amount} to account{_accountNum}.");
                Account account = accounts[_accountNum - 1];
                account.Credit(_amount);
                AccountHierarchyTest._isCheckingAccountThenPrintFee(account);
            };

            Action<int> applyInterest = (_accountNum) => {
                Account account = accounts[_accountNum - 1];

                if (account is SavingsAccount) {
                    SavingsAccount savingsAccount = (SavingsAccount) account;
                    decimal interest = savingsAccount.CalculateInterest();

                    System.Console.WriteLine($"Adding ${interest} interest to account{_accountNum}.");
                    savingsAccount.Credit(interest);
                    System.Console.WriteLine($"New account{_accountNum} balance: ${account.Balance}");
                } else {
                    // could have a custom exception type here instead of the generic "Exception"
                    throw new Exception($"account{_accountNum} is not a SavingsAccount!");
                }
            };


            // tests
            printBalance(1);
            printBalance(2);
            printBalance(3);

            System.Console.WriteLine();
            attemptDebit(1, 25.00m);
            printBalance(1);

            System.Console.WriteLine();
            attemptDebit(2, 30.00m);
            printBalance(2);

            System.Console.WriteLine();
            attemptDebit(3, 40.00m);
            printBalance(3);

            System.Console.WriteLine();
            attemptCredit(1, 40.00m);
            printBalance(1);

            System.Console.WriteLine();
            attemptCredit(2, 65.00m);
            printBalance(2);

            System.Console.WriteLine();
            attemptCredit(3, 20.00m);
            printBalance(3);

            System.Console.WriteLine();
            applyInterest(2);
        }
    }
}
