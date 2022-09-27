using ATM_Simulator.App.UI;
using ATM_Simulator.Domain.Entities;
using ATM_Simulator.Domain.Interfaces;

namespace ATM_Simulator;


class ATM_simulator : IUserLogin
{
    private List<UserAccount> userAccountList;
    private UserAccount selectedAccount;


    public void InitializeData()
    {
        userAccountList = new List<UserAccount>
        {
            new UserAccount{Id=1, FullName="Patryk Walendziuk",AccountNumber=123456, CardNumber=321321, CardPin=123123, AccountBalance=50000.00m, IsLocked=false},
            new UserAccount{Id=1, FullName="Barack Obama",AccountNumber=456789, CardNumber=654654, CardPin=456456, AccountBalance=100000.00m, IsLocked=false},
            new UserAccount{Id=1, FullName="Andrzej Duda",AccountNumber=123555, CardNumber=987987, CardPin=789789, AccountBalance=2900000.00m, IsLocked=true},
        };
    }

    public void ChcekUserCardNumAndPassword()
    {
        bool isCorrectLogin = false;
        UserAccount inputAccount = AppScreen.UserLoginForm();
        AppScreen.LoginProgress();
    }

    
}

