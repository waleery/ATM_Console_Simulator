using ATM_Simulator.App.UI;
using ATM_Simulator.Domain.Entities;
using ATM_Simulator.Domain.Enum;
using ATM_Simulator.Domain.Interfaces;

namespace ATM_Simulator;


public class ATM_simulator : IUserLogin, IUserAccountActions, ITransaction
{
    private List<UserAccount> userAccountList;
    private UserAccount selectedAccount;
    private List<Transaction> _listOfTransactions;
    private const decimal minimumKeptAmount = 50;
    private readonly AppScreen screen;

    //create constructor
    public ATM_simulator()
    {
        screen = new AppScreen();
    }

    public void Run()
    {
        AppScreen.Welcome();
        ChcekUserCardNumAndPassword();
        AppScreen.WelcomeCustomer(selectedAccount.FullName);
        AppScreen.DispalyAppMenu();
        ProcessMenuOption();
    }


    public void InitializeData()
    {
        userAccountList = new List<UserAccount>
        {
            new UserAccount{Id=1, FullName="Patryk Walendziuk",AccountNumber=123456, CardNumber=321321, CardPin=123123, AccountBalance=50000.00m, IsLocked=false},
            new UserAccount{Id=2, FullName="Barack Obama",AccountNumber=456789, CardNumber=654654, CardPin=456456, AccountBalance=100000.00m, IsLocked=false},
            new UserAccount{Id=3, FullName="Andrzej Duda",AccountNumber=123555, CardNumber=987987, CardPin=789789, AccountBalance=2900000.00m, IsLocked=true},
        };

        _listOfTransactions = new List<Transaction>();
    }

    public void ChcekUserCardNumAndPassword()
    {
        bool isCorrectLogin = false;

        while (isCorrectLogin == false)
        {
        restart:
            UserAccount inputAccount = AppScreen.UserLoginForm();
            AppScreen.LoginProgress();


            foreach (UserAccount account in userAccountList)
            {

                selectedAccount = account;

                //if typed card number is in database
                if (inputAccount.CardNumber.Equals(selectedAccount.CardNumber))
                {
                    selectedAccount.TotalLogin++;

                    if (inputAccount.CardPin.Equals(selectedAccount.CardPin))
                    {
                        //selectedAccount = account;

                        if (selectedAccount.IsLocked)
                        {
                            AppScreen.PrintLockScreen();
                            break;
                        }
                        else
                        {
                            selectedAccount.TotalLogin = 0;
                            isCorrectLogin = true;
                            break;
                        }
                    }

                }

                //if login or PIN was incorrect
                if (isCorrectLogin == false)
                {
                    Utility.PrintMessage("\nInvalid card number or PIN.", false);

                    //if user typed correct card number and wrong pin 3 times, account get blocked
                    selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;

                    if (selectedAccount.IsLocked)
                    {
                        AppScreen.PrintLockScreen();
                    }
                }
                Console.Clear();
                goto restart;
            }
        }


    }

    private void ProcessMenuOption()
    {
        switch (Validator.Convert<int>("an option:"))
        {
            case (int)AppMenu.CheckBalance:
                CheckBalance();
                break;
            case (int)AppMenu.PlaceDeposit:
                PlaceDeposit();
                break;
            case (int)AppMenu.MakeWithdrawl:
                MakeWithDrawal();
                break;
            case (int)AppMenu.InterlanTransfer:
                var internalTransfer = screen.InternalTransferForm();
                ProcessInternalTransfer(internalTransfer);
                break;
            case (int)AppMenu.ViewTransaction:
                ViewTransaction();
                break;
            case (int)AppMenu.Logout:
                AppScreen.LogoutProgress();
                Utility.PrintMessage("You have successfully logged out. Please collect your ATM card.");
                Run();
                break;
            default:
                Utility.PrintMessage("Invalid option.", false);
                break;
        }
    }

    public void CheckBalance()
    {
        Utility.PrintMessage($"Your account balance is: {Utility.FormatAmount(selectedAccount.AccountBalance)}.");
    }

    public void PlaceDeposit()
    {
        Console.WriteLine("\nOnly multiples of 100 and 50 polish zloty allowed");
        var transaction_amount = Validator.Convert<int>($"amount{AppScreen.cur}");

        //simulate checking
        Console.WriteLine("\nChecking and counting bills.");
        Utility.PrintDotAnimation();
        Console.WriteLine("");

        //some guard clause
        if (transaction_amount <= 0)
        {
            Utility.PrintMessage("Amount needs to be greater than 0. Try again.", false);
            return;
        }
        if (transaction_amount % 50 != 0)
        {
            Utility.PrintMessage($"Enter deposit amount in multiples of 50 or 100. Try again.");
            return;
        }

        if (PreviewBankZlotysCount(transaction_amount) == false)
        {
            Utility.PrintMessage($"You have cancelled your action.", false);
            return;
        }

        //bind transaction details to transaction object
        InsertTransaction(selectedAccount.Id, TransactionType.Deposit, transaction_amount, "");

        //update account balance
        selectedAccount.AccountBalance += transaction_amount;

        //print success message
        Utility.PrintMessage($"Your deposit of {Utility.FormatAmount(transaction_amount)} was successful.");
    }

    public void MakeWithDrawal()
    {
        var transaction_amount = 0;
        int selectedAmount = AppScreen.SelectAmount();

        if (selectedAmount == -1)
        {
            selectedAmount = AppScreen.SelectAmount();
        }
        else if (selectedAmount != 0)
        {
            transaction_amount = selectedAmount;
        }
        else
        {
            transaction_amount = Validator.Convert<int>($"amount {AppScreen.cur}");
        }

        //input validation
        if (transaction_amount <= 0)
        {
            Utility.PrintMessage("Amount needs to be greater than zero. Try again", false);
            return;
        }
        if(transaction_amount % 50 != 0)
        {
            Utility.PrintMessage("You can only withdraw amount in multiples of 50 or 100 zł. Try again.", false);
            return;
        }

        //Business logic validations
        if(transaction_amount > selectedAccount.AccountBalance)
        {
            Utility.PrintMessage($"Withdrawak failed. Your balance is to loow to withdraw {Utility.FormatAmount(transaction_amount)}", false);
            return;
        }
        if((selectedAccount.AccountBalance - transaction_amount) < minimumKeptAmount)
        {
            Utility.PrintMessage($"Withdrawal failed. Your account needs to have minimum {Utility.FormatAmount(minimumKeptAmount)}", false);
            return;
        }

        //Bind withdrawal details to transaction object
        InsertTransaction(selectedAccount.Id, TransactionType.Withdrawal, -transaction_amount, "");

        //update account balance
        selectedAccount.AccountBalance -= transaction_amount;

        //success message
        Utility.PrintMessage($"You have successfully withdrawn {Utility.FormatAmount(transaction_amount)}", true);
    }

    private bool PreviewBankZlotysCount(int amount)
    {
        int hundredsZlotyCount = amount / 100;
        int fiftiesZlotyCount = (amount % 100) / 50;

        Console.WriteLine("\nSummary");
        Console.WriteLine("---------");
        Console.WriteLine($"100{AppScreen.cur} X {hundredsZlotyCount} = {100 * hundredsZlotyCount}");
        Console.WriteLine($"50{AppScreen.cur} X {fiftiesZlotyCount} = {50 * fiftiesZlotyCount}");
        Console.WriteLine($"Total amount: {Utility.FormatAmount(amount)}\n\n");

        int opt = Validator.Convert<int>("1 to confirm");

        return opt.Equals(1); //if one return true, else return false
    }

    public void InsertTransaction(long _UserBankAccountID, TransactionType _tranType, decimal _tranAmount, string _desc)
    {
        //create a new transaction object
        var transaction = new Transaction()
        {
            TransactionId = Utility.GetTransactionId(),
            UserBankAccountId = _UserBankAccountID,
            TransactionDate = DateTime.Now,
            TransactionType = _tranType,
            TransactionAmount = _tranAmount,
            Description = _desc
        };

        //add transaction object to he list
        _listOfTransactions.Add(transaction);
    }

    public void ViewTransaction()
    {
        //searching list of transactions logged user
        var filteredTransactionList = _listOfTransactions.Where(t => t.UserBankAccountId == selectedAccount.Id).ToList();

        //check if there is a transaction
        if(filteredTransactionList.Count == 0)
        {
            Utility.PrintMessage("You have no transaction yet.", true);
        }
        else
        {

        }
    }

    private void ProcessInternalTransfer(InternalTransfer internalTrasfer)
    {
        if(internalTrasfer.TransferAmount <= 0)
        {
            Utility.PrintMessage("Amount needs to be more than 0. Try again.", false);
            return;
        }

        //check sender's account balance
        if(internalTrasfer.TransferAmount > selectedAccount.AccountBalance)
        {
            Utility.PrintMessage($"Transfer failed. You do not have enough balance to transfer {Utility.FormatAmount(internalTrasfer.TransferAmount)}", false);
            return;

        }

        //check the minium kept amount
        if ((selectedAccount.AccountBalance - internalTrasfer.TransferAmount) < minimumKeptAmount)
        {
            Utility.PrintMessage($"Transfer failed. Your account needs to have minimum {Utility.FormatAmount(minimumKeptAmount)}", false);
            return;
        }

        //check reciever's account number is valid
        var selectedBankAccountReceiver = (from userAccount in userAccountList
                                           where userAccount.AccountNumber == internalTrasfer.ReciepeintBankAccountNumber
                                           select userAccount).FirstOrDefault();

        //nothing was selected => invalid bank number
        if(selectedBankAccountReceiver== null)
        {
            Utility.PrintMessage("Transfer failed. Receiver bank account number is invalid", false);
            return;
        }

        //check receiver's name
        if(selectedBankAccountReceiver.FullName != internalTrasfer.RecipientBankAccountName)
        {
            Utility.PrintMessage("Transfer failed. Recipient's bank account name doesn't match.", false);
            return;
        }

        //add transaction to transaction record - sender
        InsertTransaction(selectedAccount.Id, TransactionType.transfer, -internalTrasfer.TransferAmount, $"Transfer form {selectedAccount.FullName} to" +
            $" {selectedBankAccountReceiver.FullName} -  receiver card number: ({selectedBankAccountReceiver.CardNumber})");

        //update sender's account balance
        selectedAccount.AccountBalance -= internalTrasfer.TransferAmount;

        //add transaction to transaction record - receiver
        InsertTransaction(selectedBankAccountReceiver.Id, TransactionType.transfer, internalTrasfer.TransferAmount, $"Transfer form {selectedAccount.FullName} to" +
            $" {selectedBankAccountReceiver.FullName} - sender card number: ({selectedAccount.CardNumber})");

        //update receiver's account balance
        selectedBankAccountReceiver.AccountBalance += internalTrasfer.TransferAmount;

        //print success message
        Utility.PrintMessage($"You have successfully transfered {Utility.FormatAmount(internalTrasfer.TransferAmount)} to " +
            $"{internalTrasfer.RecipientBankAccountName}", true);
    }
}

