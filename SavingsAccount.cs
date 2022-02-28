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
    class SavingsAccount : Account {
        // constants
        public const decimal DEFAULT_INTEREST_RATE = 0.00m;

        // fields
        private decimal _interestRate = DEFAULT_INTEREST_RATE;

        public SavingsAccount(
            decimal initialBalance = DEFAULT_BALANCE,
            decimal initialInterestRate = DEFAULT_INTEREST_RATE
        ) {
            balance = initialBalance;
            _interestRate = initialInterestRate;
        }

        public decimal CalculateInterest() {
            return balance * _interestRate;
        }
    }
}
