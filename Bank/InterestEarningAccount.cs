using System;

namespace Bank;

public class InterestEarningAccount : BankAccount
{
    public InterestEarningAccount(string name, decimal initialBalance)
        : base(name, initialBalance)
    { }

    // override позволяет в дочернем классе определить новую реализацию метода
    public override void PerformMonthAndTransitions()
    {
        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "Apply month interest");
        }
    }
}
