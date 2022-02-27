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
        static void Main(string[] args) {
            Console.WriteLine("test");
            Account a = new Account(0.00m);
            Console.WriteLine(a);
            a.Credit(-1);
            a.Credit(2);
            Console.WriteLine(a.Balance);
            a.Debit(1.1m);
            a.Debit(5.1m);
        }
    }
}
