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


namespace AccountHierarchy {
    class CheckingAccount : Account {
        // constants
        public const decimal DEFAULT_TX_FEE = 0.00m;

        // fields
        private decimal _txFee = DEFAULT_TX_FEE;

        // properties
        public decimal txFee {
            get {
                return _txFee;
            }

            set {
                Account._validateGreaterThanOrEqualToZero(value);
                _txFee = value;
            }

        }

        public CheckingAccount(
            decimal initialBalance = DEFAULT_BALANCE,
            decimal initialTxFee = DEFAULT_TX_FEE
        ) {
            balance = initialBalance;
            txFee = initialTxFee;
        }

        public override void Credit(decimal amount) {
            // this wasn't part of the assignment requirements, but it seemed sane to check if the
            // amount is non-negative before performing the operation
            Account._validateGreaterThanOrEqualToZero(amount);

            balance += amount - txFee;
        }

        public override void Debit(decimal amount) {
            // this wasn't part of the assignment requirements, but it seemed sane to check if the
            // amount is non-negative before performing the operation
            Account._validateGreaterThanOrEqualToZero(amount);

            // does not factor in concurrency / multi-threading
            // could cause race conditions . .
            // a mutex lock or transactional SQL could solve this
            decimal beforeTx = balance;

            try {
                balance -= txFee;
                balance -= amount;
            } catch (InsufficientBalanceException ibe) {
                balance = beforeTx;
                throw ibe; // throw the original exception after resetting the balance
            }
        }
    }
}
