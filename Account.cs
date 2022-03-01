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
using System.Runtime.Serialization;


namespace AccountHierarchy {
    [Serializable()]
    public class InsufficientBalanceException : Exception {
       private decimal insufficientBalance;

       protected InsufficientBalanceException()
          : base()
       { }

       public InsufficientBalanceException(decimal value) :
          base(String.Format("{0} is less that zero, thus an insufficient balance for the requested operation.", value))
       {
          insufficientBalance = value;
       }

       public InsufficientBalanceException(decimal value, string message)
          : base(message)
       {
          insufficientBalance = value;
       }

       public InsufficientBalanceException(decimal value, string message, Exception innerException) :
          base(message, innerException)
       {
          insufficientBalance = value;
       }

       protected InsufficientBalanceException(SerializationInfo info,
                                   StreamingContext context)
          : base(info, context)
       { }

       public decimal InsufficientBalance
       { get { return insufficientBalance; } }
    }

    class Account {
        // constants
        public const decimal DEFAULT_BALANCE = 0.00m;
        public const string DEBIT_ERROR_MESSAGE = "Debit amount exceeded account balance.";

        // fields
        private decimal _balance = DEFAULT_BALANCE;

        // properties
        protected decimal balance {
            get {
                return _balance;
            }

            set {
                Account._validateGreaterThanOrEqualToZero(value);
                _balance = value;
            }

        }

        public decimal Balance {
            get {
                return balance;
            }
        }

        // constructor
        public Account(
            decimal initialBalance = DEFAULT_BALANCE
        ) {
            balance = initialBalance;
        }

        // methods
        protected static void _validateGreaterThanOrEqualToZero(
            decimal amount
        ) {
            // call this function before modifying the balance
            if (amount < 0) {
                throw new InsufficientBalanceException(amount);
            }
        }

        public virtual void Credit(decimal amount) {
            // this wasn't part of the assignment requirements, but it seemed sane to check if the
            // amount is non-negative before performing the operation
            Account._validateGreaterThanOrEqualToZero(amount);

            balance += amount;
        }

        public virtual void Debit(decimal amount) {
            // this wasn't part of the assignment requirements, but it seemed sane to check if the
            // amount is non-negative before performing the operation
            Account._validateGreaterThanOrEqualToZero(amount);

            try {
                balance -= amount;
            } catch (InsufficientBalanceException) {
                // I don't really like this type of error handling
                // (print and continue)
                // but this is what the textbook assignment
                // calls for in this case
                Console.WriteLine(DEBIT_ERROR_MESSAGE);
            }
        }
    }
}
