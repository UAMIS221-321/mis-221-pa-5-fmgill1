using mis_221_pa_5_fmgill1;
using System.Diagnostics;
using System.Net;

Trainer[] trainer = new Trainer[1000];
Listings[] listings = new Listings[1000];
Booking[] bookings = new Booking[1000];

TrainerUtility utility = new TrainerUtility(trainer);
ListingUtility utility1 = new ListingUtility(listings, trainer);
BookingUtility utility2 = new BookingUtility(bookings, utility1, trainer, listings, utility);
Report report = new Report(utility2, bookings, utility1, listings);

MainMenu(utility, utility1, utility2, report, bookings);

static void MainMenu(TrainerUtility utility, ListingUtility utility1, BookingUtility utility2, Report report, Booking[] bookings)
{
    string prompt = @" _  _  ____  __     ___  __   _  _  ____    ____  __     ____  _  _  ____     
/ )( \(  __)(  )   / __)/  \ ( \/ )(  __)  (_  _)/  \   (_  _)/ )( \(  __)   / __)( \/ )( \/ )/ \  
\ /\ / ) _) / (_/\( (__(  O )/ \/ \ ) _)     )( (  O )    )(  ) __ ( ) _)   ( (_ \ )  / / \/ \\_/  
(_/\_)(____)\____/ \___)\__/ \_)(_/(____)   (__) \__/    (__) \_)(_/(____)   \___/(__/  \_)(_/(_)  
Is it New Years??? Use the arrow keys to cycle through options and press enter to select an option!";
    string[] options = {"Manage Trainer Data", "Manage Listing Data", "Manage Customer Booking Data", "Run Reports", "Exit"};
    MenuSelector mainMenu = new MenuSelector(prompt, options);
    int selectedIndex = mainMenu.Run();
    while(selectedIndex != 5)
    {
        switch(selectedIndex)
        {
            case 0:
                RunTrainerMenu(utility, utility1, utility2, report, bookings);
                break;
            case 1:
                RunListingMenu(utility, utility1, utility2, report);
                break;
            case 2: 
                RunCustomerMenu(utility, utility1, utility2, report, bookings);
                break;
            case 3: 
                RunReportsMenu(utility, utility1, utility2, report);
                break;
            case 4:
                System.Console.WriteLine("Bye bye! Hope you got those gains!");
                ExitMenu();
                break;
        }
        selectedIndex = mainMenu.Run();
    }
}

static void RunTrainerMenu(TrainerUtility utility, ListingUtility utility1, BookingUtility utility2, Report report, Booking[] bookings)
{
    string prompt = @" ____  ____   __   __  __ _  ____  ____    _ 
(_  _)(  _ \ / _\ (  )(  ( \(  __)(  _ \  ( \/ )(  __)(  ( \/ )( \
  )(   )   //    \ )( /    / ) _)  )   /  / \/ \ ) _) /    /) \/ (
 (__) (__\_)\_/\_/(__)\_)__)(____)(__\_)  \_)(_/(____)\_)__)\____/";
    string[] options = {"Add", "Update", "Delete", "Return to main menu"};
    MenuSelector mainMenu = new MenuSelector(prompt, options);
    int selectedIndex = mainMenu.Run();
    while(selectedIndex != 4)
    {
        switch(selectedIndex)
        {
            case 0:
                utility.AddTrainer();
                break;
            case 1:
                utility.UpdateTrainer();
                break;
            case 2: 
                utility.DeleteTrainer();
                break;
            case 3:
                MainMenu(utility, utility1, utility2, report, bookings);
                break;
        }
        selectedIndex = mainMenu.Run();
    }
  
}

static void RunListingMenu(TrainerUtility utility, ListingUtility utility1, BookingUtility utility2, Report report)
{
    string prompt = @" __    __  ____  ____  __  __ _   ___    _  
(  )  (  )/ ___)(_  _)(  )(  ( \ / __)  ( \/ )(  __)(  ( \/ )( \
/ (_/\ )( \___ \  )(   )( /    /( (_ \  / \/ \ ) _) /    /) \/ (
\____/(__)(____/ (__) (__)\_)__) \___/  \_)(_/(____)\_)__)\____/";
    string[] options = {"Add", "Update", "Delete", "Return to Main Menu"};
    MenuSelector mainMenu = new MenuSelector(prompt, options);
    int selectedIndex = mainMenu.Run();
    while(selectedIndex != 3)
    {
        switch(selectedIndex)
        {
            case 0:
                utility1.AddListing();
                break;
            case 1:
                utility1.UpdateListing();
                break;
            case 2: 
                utility1.DeleteListing();
                break;
        }
        selectedIndex = mainMenu.Run();
    }
   
}

static void RunCustomerMenu(TrainerUtility utility, ListingUtility utility1, BookingUtility utility2, Report report, Booking[] bookings)
{
    string prompt = @" 
   __  _  _  ____  ____  __   _  _  ____  ____    ____   __    __  __ _  __  __ _   ___   
 / __)/ )( \/ ___)(_  _)/  \ ( \/ )(  __)(  _ \  (  _ \ /  \  /  \(  / )(  )(  ( \ / __)      
( (__ ) \/ (\___ \  )( (  O )/ \/ \ ) _)  )   /   ) _ ((  O )(  O ))  (  )( /    /( (_ \      
 \___)\____/(____/ (__) \__/ \_)(_/(____)(__\_)  (____/ \__/  \__/(__\_)(__)\_)__) \___/";
    string[] options = {"View Available Training Sessions", "Book a session", "Update Booking Status", "Return to Main Menu"};
    MenuSelector mainMenu = new MenuSelector(prompt, options);
    int selectedIndex = mainMenu.Run();
    while(selectedIndex != 3)
    {
        switch(selectedIndex)
        {
            case 0:
                utility2.ViewAllSessions();
                break;
            case 1:
                utility2.BookASession();
                break;
            case 2: 
                utility2.UpdateSessionStatus();
                break;
        }
        System.Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        selectedIndex = mainMenu.Run();
    }


}

static void RunReportsMenu(TrainerUtility utility, ListingUtility utility1, BookingUtility utility2, Report report)
{
    string prompt = @" ____  _  _  __ _    ____  ____  ____  
(  _ \/ )( \(  ( \  (  _ \(  __)(  _ \ /  \(  _ \(_  _)/ ___)
 )   /) \/ (/    /   )   / ) _)  ) __/(  O ))   /  )(  \___ \
(__\_)\____/\_)__)  (__\_)(____)(__)   \__/(__\_) (__) (____/";
    string[] options = {"Individual Customer Sessions", "Historical Customer Sessions", "Historical Revenue Report", "Return to Main Menu"};
    MenuSelector mainMenu = new MenuSelector(prompt, options);
    int selectedIndex = mainMenu.Run();
    while(selectedIndex != 3)
    {
        switch(selectedIndex)
        {
            case 0:
                report.IndividualCustomerReport();
                break;
            case 1:
                report.HistoricalCustomerReport();
                break;
            case 2: 
                report.HistoricalRevenueReport();
                break;
        }
        selectedIndex = mainMenu.Run();
    }
    

}

static void ExitMenu()
{
    Environment.Exit(0);
}



