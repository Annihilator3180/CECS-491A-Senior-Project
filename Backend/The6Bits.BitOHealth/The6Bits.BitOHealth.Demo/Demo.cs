using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using The6Bits.BitOHealth.ControllerLayer;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.BitOHealth.DAL.Tests;
using The6Bits.BitOHealth.Models;
using The6Bits.BitOHealth.ServiceLayer;
using The6Bits.Logging;
using The6Bits.Logging.Implementations;
using The6Bits.BitOHealth.DAL.Implementations;
using The6Bits.Logging.DAL.Implementations;
using The6Bits.Authorization.Implementations;
using System.Diagnostics;

namespace The6Bits.BitOHealth.Demo
{
    class Demo
    {
        public static void Main(string[] args)
        {
            string connectstring = @"Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;";
            bool loggedin = false;
            string token = "";
            while (!loggedin)
            {


                try
                {
                    AuthenticationController AC = new AuthenticationController(new AuthMsSqlDao(), new DESAuthorizationService());
                    Console.WriteLine("Enter Admin Username : ");
                    String adminUsername = Console.ReadLine();
                    Console.WriteLine("Enter Account Password : ");
                    String password = Console.ReadLine();
                    token = AC.AdminLogin(adminUsername, password);
                    if (token == "Member" || token == "username not found" || token == "incorrect user/pass" || token == "DB Error")
                    {
                        Console.WriteLine(token);
                        Console.WriteLine("Failed To Login");
                    }
                    else
                    {
                        Console.WriteLine("Logged In");
                        loggedin = true;
                    }
                }
                catch (Exception ex)
                {

                }
            }
            ArchivingController ac = new ArchivingController() { connectionstring = connectstring };
            ac.Archive();


            UMController um = new UMController(new MsSqlUMDAO<User>(connectstring), new DESAuthorizationService(), new SQLLogDAO()
            {
                _connectString = connectstring
            }, "boofman2", token);
            bool done = false;
            while (done != true)
            {
                try
                {
                    int choice = 0;
                    Console.WriteLine("1. Create 2. Update 3. Delete\n4. Enable 5. Disable 6. Bulk UM 0.Exit");
                    choice = Int32.Parse(Console.ReadLine());
                    while (choice != 0)
                    {
                        while (choice < 0 || choice > 6)
                        {
                            Console.WriteLine("Incorrect Input\nTry again: ");
                            choice = Int32.Parse(Console.ReadLine());
                        }
                        if (choice == 1)
                        {
                            //create
                            Create(um);

                        }
                        else if (choice == 2)
                        {
                            //update
                            Console.WriteLine("Input username of user to update");
                            String userName = Console.ReadLine();
                            //userName, email, password, firstN, lastN, admin, enabled
                            Console.WriteLine("Pick something to update:  'email' , 'password' ,  'firstN' , 'lastN' , 'admin' , 'enabled' ");
                            String updateChoice = Console.ReadLine();
                            User newUser = new User();
                            newUser.Username = userName;
                            Console.WriteLine("Input new " + updateChoice + ": ");
                            String updater = Console.ReadLine();

                            if (updateChoice == "email")
                            {
                                newUser.Email = updater;
                            }
                            else if (updateChoice == "password")
                            {
                                newUser.Password = updater;
                            }
                            else if (updateChoice == "firstN")
                            {
                                newUser.FirstName = updater;
                            }
                            else if (updateChoice == "lastN")
                            {
                                newUser.LastName = updater;
                            }
                            else if (updateChoice == "admin")
                            {
                                newUser.IsAdmin = Int32.Parse(updater);
                            }
                            else if (updateChoice == "enabled")
                            {
                                newUser.IsEnabled = Int32.Parse(updater);
                            }
                            Console.WriteLine(um.UpdateAccount(newUser));

                        }
                        else if (choice == 3)
                        {
                            //delete
                            Console.WriteLine("Input userName of user to delete");
                            String userName = Console.ReadLine();
                            String userDelete = um.DeleteAccount(userName);
                        }
                        else if (choice == 4)
                        {
                            //enable
                            Console.WriteLine("Enter userName of user to enable");
                            String userName = Console.ReadLine();
                            String userEnable = um.EnableAccount(userName);
                        }
                        else if (choice == 5)
                        {
                            //disable
                            Console.WriteLine("Enter userName of user to disable");
                            String userName = Console.ReadLine();
                            String userDisable = um.DisableAccount(userName);
                            Console.WriteLine(userDisable);

                        }
                        else if (choice == 6)
                        {
                            //bulk um
                            BulkUM(um);
                        }
                        Console.WriteLine("1. Create 2. Update 3. Delete\n4. Enable 5. Disable 6. Bulk UM");
                        choice = Int32.Parse(Console.ReadLine());
                    }
                    done = true;

                }
                catch (Exception ex) { }
            }
        }


        public static void BulkUM(UMController um)
        {

            String userName = "", firstN = "", lastN = "", email = "", password;
            int admin = 0, enabled = 0;

            //C:\Users\reals\OneDrive\Desktop\accounts.txt

            Console.WriteLine("Input file location: ");
            String location = Console.ReadLine();
            Stopwatch stopwatch = Stopwatch.StartNew();

            foreach (string line in File.ReadLines(@location, Encoding.UTF8))
            {
                var a = line.Split(' ');
                if (a[0] == "create")
                {
                    userName = a[1];
                    email = a[2];
                    password = a[3];
                    firstN = a[4];
                    lastN = a[5];
                    admin = Int32.Parse(a[6]);
                    enabled = Int32.Parse(a[7]);
                    User user = new User(userName, email, password, firstN, lastN, admin, enabled);
                    Task.Run(() => um.CreateAccount(user));
                }
                else if (a[0] == "update")
                {
                    userName = a[1];
                    User newUser = new User();
                    newUser.Username = userName;
                    foreach (string i in a.Skip(2))
                    {
                        var x = i.Split(',');
                        if (x[0] == "email")
                        {
                            newUser.Email = x[1];
                        }
                        else if (x[0] == "password")
                        {
                            newUser.Password = x[1];
                        }
                        else if (x[0] == "firstN")
                        {
                            newUser.FirstName = x[1];
                        }
                        else if (x[0] == "lastN")
                        {
                            newUser.LastName = x[1];
                        }
                        else if (x[0] == "admin")
                        {
                            newUser.IsAdmin = Int32.Parse(x[1]);
                        }
                        else if (x[0] == "enabled")
                        {
                            newUser.IsEnabled = Int32.Parse(x[1]);
                        }
                    }
                    Task.Run(() => um.UpdateAccount(newUser));

                }
                else if (a[0] == "delete")
                {
                    userName = a[1];
                    User newUser = new User();
                    Task.Run(() => um.DeleteAccount(userName));


                }
                else if (a[0] == "enable")
                {
                    userName = a[1];
                    User newUser = new User();
                    Task.Run(() => um.EnableAccount(userName));
                }
                else if (a[0] == "disable")
                {
                    userName = a[1];
                    User newUser = new User();
                    Task.Run(() => um.DisableAccount(userName));
                }

            }
            double stopwatchTime = stopwatch.ElapsedMilliseconds;
            Console.WriteLine("Finished in : " + stopwatchTime.ToString() + " ms");
        }


        public static void Create(UMController um)
        {
            //userName, email, password, firstN, lastN, admin, enabled
            Console.WriteLine("Input userName: ");
            String userName = Console.ReadLine();
            Console.WriteLine("email: ");
            String email = Console.ReadLine();
            Console.WriteLine("Password: ");
            String password = Console.ReadLine();
            Console.WriteLine("firstN: ");
            String firstN = Console.ReadLine();
            Console.WriteLine("lastN: ");
            String lastN = Console.ReadLine();
            Console.WriteLine("admin: ");
            int admin = Int32.Parse(Console.ReadLine());
            Console.WriteLine("enabled: ");
            int enabled = Int32.Parse(Console.ReadLine());

            User user = new User(userName, email, password, firstN, lastN, admin, enabled);
            Console.WriteLine(um.CreateAccount(user));
        }

    }
}
