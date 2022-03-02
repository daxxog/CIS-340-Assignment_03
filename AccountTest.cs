/* ================================================
 * @author     David Volm aka VOLMINATOR aka daXXog
 * @date       Tue Mar  1 20:39:24 CST 2022
 * @school     UWSP
 * @class      CIS 340
 * @section    01
 * @assignment 03
 * @professor  Hardeep Kaur Dhalla
 * @licence    MIT
 * ===============================================*/

using System;


namespace AccountHierarchy {
    class AccountTest {
        // accounts
        static readonly Account[] accounts = {
            new SavingsAccount(25m, .03m),
            new CheckingAccount(80m, 1m),
            new SavingsAccount(200m, .015m),
            new CheckingAccount(400m, .5m)
        };

        // could also read these from stdin
        // but hardcoded here for simple test automation
        static readonly int[] inputs = {
            20,
            30,
            100,
            90,
            100,
            0,
            100,
            300
        };

        private static void _isCheckingAccountThenPrintFee(Account account) {
            // print some extra info when we perform and action on a CheckingAccount
            if (account is CheckingAccount) {
                CheckingAccount checkingAccount = (CheckingAccount) account;
                System.Console.WriteLine($"${string.Format("{0:0.00}", checkingAccount.txFee)} transaction fee charged.");
            }
        }

        static void Main(string[] args) {
            // test case for polymorphic account thingy
            Action<int, int, int> accountAction = (_accountNum, _withdrawAmount, _depositAmount) => {
                // get a reference to the Account from the array
                Account account = accounts[_accountNum];

                // print balance
                System.Console.WriteLine($"Account {_accountNum + 1} balance: ${string.Format("{0:0.00}", account.Balance)}");
                System.Console.WriteLine();

                // print withdrawal amount and process withdrawal
                System.Console.WriteLine($"Enter an amount to withdraw from Account {_accountNum + 1}: {_withdrawAmount}");
                try {
                    account.Debit(_withdrawAmount);
                    _isCheckingAccountThenPrintFee(account);
                } catch (InsufficientBalanceException) {
                    System.Console.WriteLine("Debit amount exceeded account balance.");
                }
                System.Console.WriteLine();

                // print deposit amount and process deposit
                System.Console.WriteLine($"Enter an amount to deposit into Account {_accountNum + 1}: {_depositAmount}");
                account.Credit(_depositAmount);
                _isCheckingAccountThenPrintFee(account);

                // add interest if this is a SavingsAccount
                if (account is SavingsAccount) {
                    SavingsAccount savingsAccount = (SavingsAccount) account;
                    decimal interest = savingsAccount.CalculateInterest();

                    System.Console.WriteLine($"Adding ${interest} interest to Account {_accountNum + 1} (a SavingsAccount)");
                    savingsAccount.Credit(interest);
                }

                System.Console.WriteLine();
                System.Console.WriteLine($"Updated Account {_accountNum + 1} balance: ${string.Format("{0:0.00}", account.Balance)}");

                // two newlines to match output
                System.Console.WriteLine();
                System.Console.WriteLine();
            };


            // iterate through input data
            int j = 0;
            foreach(int i in inputs) {
                // run action on every other input
                // test function takes withdrawal and
                // deposit info in one go
                if ( (j % 2) == 0 ) {
                    accountAction(j/2, i, inputs[j+1]);
                }

                // increment the index counter
                j++;
            }
        }
    }
}
