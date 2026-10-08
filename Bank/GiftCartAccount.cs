namespace Bank;

public class GiftCardAccount: BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;

        //monthlyDeposit - параметр по умолчания (принимает 0),
        // при создании new GiftCardAccount("Yana"


        public GiftCardAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0)
        : base(name, initialBalance)
        => _monthlyDeposit = monthlyDeposit;

    public override void PerformMonthAndTransitions()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add monthlydeposit");
        }

    }

    public override string ToString() => base.ToString() + $"monthly deposit: {_monthlyDeposit}";
}