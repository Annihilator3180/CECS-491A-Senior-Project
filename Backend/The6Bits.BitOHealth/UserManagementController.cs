using System;
using UserManagementManager;

public class UserManagementController
{

    UserManagementManager UMM = new UserManagementManager();



    public bool CreateAccount()
    {
        UMM.CreateAccount();
        return true;
    }

}