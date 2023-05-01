namespace mis_221_pa_5_fmgill1
{
    public class Report
    {
        private BookingUtility bookingUtility;
        private Booking[] bookings;
        private ListingUtility listingUtility;
        private Listings[] listings;
     

        public Report()
        {

        }
        public Report(BookingUtility bookingUtility, Booking[] bookings, ListingUtility listingUtility, Listings[] listings)
        {
            this.bookingUtility = bookingUtility;
            this.bookings = bookings;
            this.listingUtility = listingUtility;
            this.listings = listings;
        }

        public void IndividualCustomerReport()
        {
            System.Console.WriteLine("Please enter the customer email address:"); 
            string customerEmail = Console.ReadLine();
            int count = 0;
            for (int i = 0; i < Booking.GetCount(); i++)
            {
                if(bookings[i].GetCustomerEmail() == customerEmail && bookings[i].GetSessionStatus() == "completed") //if email is found and session is completed 
                {
                    System.Console.WriteLine(bookings[i].ToString()); //print all bookings under that email 
                    count ++;
                }
            }
            System.Console.WriteLine($"This customer has {count} COMPLETED sessions"); //only counts the ones actually completed, not just booked 
            System.Console.WriteLine("Would you like to save this report? (Yes or No)");
            string userInput = Console.ReadLine().ToUpper();
            string fileName = "";
            if(userInput == "YES")
            {
                System.Console.WriteLine("What would you like to name the file");
                fileName = Console.ReadLine(); 
                StreamWriter outFile = new StreamWriter(fileName); //creates streamwriter object
                for (int i = 0; i < Booking.GetCount(); i++)
                {
                    if(bookings[i].GetCustomerEmail() == customerEmail && bookings[i].GetSessionStatus() == "completed") //if email is found and session is completed
                    {
                        outFile.WriteLine(bookings[i].ToFile()); //write those bookings to the file
                    }
                }
                outFile.Close(); //close file
                System.Console.WriteLine($"Your report was saved to a file called {fileName}.txt");
            }
            listingUtility.Pause();

        }

        public void IndividualTrainerReport()
        {
            
            System.Console.WriteLine("Please enter the trainer name:"); 
            string name = Console.ReadLine();
            int completed = 0;
            int cancelled = 0;
            int booked = 0;
            for (int i = 0; i < Booking.GetCount(); i++)
            {
                if(bookings[i].GetTrainerName() == name) //if trainer name is found
                {
                    if(bookings[i].GetSessionStatus() == "completed" ) //if name is found and session is completed
                    {
                        System.Console.WriteLine(bookings[i].ToString()); //print all bookings completed
                        completed ++; //inc completed #
                    }
                    else if(bookings[i].GetSessionStatus()== "booked")// if the name is found and session is booked
                    {
                        System.Console.WriteLine(bookings[i].ToString());//print all bookings booked
                        booked++; //inc booked #
                    }
                    else if(bookings[i].GetSessionStatus() == "cancelled")//if the name is found and the session was cancelled
                    {
                        System.Console.WriteLine(bookings[i].ToString()); //print cancelled sessions
                        cancelled++; //inc cancelled #
                    }
                }
                
            }
            System.Console.WriteLine($"This trainer has {completed} completed sessions, {booked} booked sessions, and {cancelled} cancelled sessions"); //counts for booked, completed and cancelled  
            System.Console.WriteLine();
            System.Console.WriteLine("Would you like to save this report? (Yes or No)");
            string userInput = Console.ReadLine().ToUpper();
            string fileName = "";
            if(userInput == "YES")
            {
                System.Console.WriteLine("What would you like to name the file");
                fileName = Console.ReadLine(); 
                StreamWriter outFile = new StreamWriter(fileName); //creates streamwriter object
                for (int i = 0; i < Booking.GetCount(); i++)
                {
                    if(bookings[i].GetTrainerName() == name && (bookings[i].GetSessionStatus() == "completed" || bookings[i].GetSessionStatus() == "booked" || bookings[i].GetSessionStatus() == "cancelled")) //if name is found and session is completed/booked
                    {
                        outFile.WriteLine(bookings[i].ToFile()); //write those bookings to the file
                    }
                }
                outFile.Close(); //close file
                System.Console.WriteLine($"Your report was saved to a file called {fileName}.txt");
            }
            listingUtility.Pause();
        }
        public void SortByName()
        {
            for (int i= 0; i < Booking.GetCount()-1; i++) //starts at 0, ends at the second to last booking
            {
                int min = i;
                for( int j = i + 1; j < Booking.GetCount(); j++) //starts at 1, ends at last booking
                {
                    if(bookings[min].GetCustomerName().CompareTo(bookings[j].GetCustomerName())>0) //compare two names 
                    {
                        min = j; //reset min if in the wrong spot
                    }
                    if(min != i) //if min was reset, swap the bookings 
                    {
                        Swap(min, i);
                    }
                }
            }
        }
        public void Swap(int x, int y)
        {
            Booking temp = bookings[x];
            bookings[x] = bookings[y];
            bookings[y] = temp;
        }
        public int FindListingByDate(DateOnly date) //finds a listing index based on the shared booking date 
        {
            for(int i = 0; i < Listings.GetCount(); i++)
            {
                if(listings[i].GetSessionDate() == date)
                {
                    return i;
                }
            }
            return -1;
        }

        public void HistoricalCustomerReport()
        {
            int count = 0; 
            SortByName();  //sort the customers by name
            string currName = bookings[0].GetCustomerName();//current name initially set to the first name 
            for(int i = 0; i < Booking.GetCount(); i++)
            {
                if(bookings[i].GetCustomerName() == currName) //if the booking on current index has the current name
                {
                    count++; //increments the count for the number of sessions per customer                    
                }
                else 
                {
                    for (int j = i + 1; j < count + 1; j++)//sort by date (attempted.. was working but now isnt and I cant figure out why :( )
                    {
                        if (bookings[j].GetTrainingDate()<bookings[i].GetTrainingDate()) //compare training date from inner and outer for loop position
                        {
                            Booking temp = bookings[j];//swap them
                            bookings[j] = bookings[i];
                            bookings[i] = temp;
                        }
                    }
                   
                    ProcessBreak(ref count, ref currName, i);//process break because we're no longer on the same name
                }
                System.Console.WriteLine(bookings[i].ToString()); //print out the bookings 
            }
            ProcessBreak(ref count, ref currName, 0); //one more process break because of fencepost 
            //System.Console.WriteLine(bookings);
            System.Console.WriteLine("Would you like to save the report?");
            string userInput = Console.ReadLine().ToUpper();
            if(userInput == "YES")
            {
                System.Console.WriteLine("What is the name of the file you would like to save the report to");
                string fileName = Console.ReadLine();
                StreamWriter outFile = new StreamWriter(fileName); //creates streamwriter object 
                for(int j = 0; j < Booking.GetCount(); j++)
                {
                    outFile.WriteLine(bookings[j].ToFile()); //writes to the file
                }
                outFile.Close(); //close the file
                System.Console.WriteLine($"Your historical customer report was saved to {fileName}.txt");
            }
            listingUtility.Pause();
        }
        public void ProcessBreak(ref int count, ref string currName, int i) //gives total sessions per name and resets the values currName and count
        {
            System.Console.WriteLine($"{currName} had {count} sessions");
            currName = bookings[i].GetCustomerName();
            count = 1;
        }
        
        public void SortByDate()
        {
            for (int i = 0; i < Booking.GetCount()-1; i++) //start on first booking, end on second to last
            {
                for (int j = i + 1; j < Booking.GetCount(); j++) //start on second booking, end on the last 
                {
                    if (bookings[j].GetTrainingDate()<bookings[i].GetTrainingDate()) //compare the two next to eachother
                    {
                        //swap
                        Booking temp = bookings[j];
                        bookings[j] = bookings[i];
                        bookings[i] = temp;
                    }
                }
            }
        }
        public void HistoricalRevenueReport() 
        {
            SortByDate();   //sort the bookings by date
            int currentMonth = bookings[0].GetTrainingDate().Month; //creates an int for the month # based on the first training date in bookings 
            int currentYear = bookings[0].GetTrainingDate().Year; //creates an int for the first year # based on the training date in bookings
            double monthRev = 0;
            double yearRev = 0;
            System.Console.WriteLine("Welcome to the Historical Revenue Report! These numbers are based on sessions that have been COMPLETED");
            System.Console.WriteLine("Here are the revenues for the months in which training sessions exist:");
            listingUtility.Pause();

            for (int i = 0; i < Booking.GetCount(); i++){ // iterating through all bookings
                if(bookings[i].GetSessionStatus() == "completed")
                { // only using completed sessions
                    if(bookings[i].GetTrainingDate().Year == currentYear)
                    {
                        if(bookings[i].GetTrainingDate().Month == currentMonth)
                        { //checks if the month in the booking is the same as the month in thw listing
                            monthRev += listings[FindListingByDate(bookings[i].GetTrainingDate())].GetSessionCost(); //add session cost for that listing (in the same year) to yearRev
                        }
                        else
                        {
                            ProcessMonthBreak(ref currentMonth, ref monthRev, i, ref currentYear);
                        }
                        yearRev += listings[FindListingByDate(bookings[i].GetTrainingDate())].GetSessionCost(); //add session cost for that listing (in the same year) to yearRev
                    }
                    else
                    {
                        ProcessMonthBreak(ref currentMonth, ref monthRev, i, ref currentYear);
                        ProcessYearBreak(ref currentYear, ref yearRev, i);
                    }
                }
            }
            ProcessYearBreak(ref currentYear, ref yearRev, 0);

            System.Console.WriteLine("Would you like to save the report?");
            string userInput = Console.ReadLine().ToUpper();
            if(userInput == "YES")
            {
                System.Console.WriteLine("What is the name of the file you would like to save the report to");
                string fileName = Console.ReadLine();
                StreamWriter outFile = new StreamWriter(fileName);
                for(int j = 0; j < Booking.GetCount(); j++)
                {
                    outFile.WriteLine(bookings[j].ToFile());
                }
                outFile.Close();
                System.Console.WriteLine($"Your historical revenue report was saved to {fileName}.txt");
            }
            listingUtility.Pause();
        }
        public void ProcessMonthBreak(ref int currentMonth, ref double monthRev, int i, ref int currentYear)
        {
            System.Console.WriteLine($"{currentMonth}/{currentYear}  Revenue:  {monthRev}"); //print current month/year and total revenue for the month
            currentMonth = bookings[i].GetTrainingDate().Month; //reset current month
            monthRev = listings[FindListingByDate(bookings[i].GetTrainingDate())].GetSessionCost(); //reset monthly rev
        }
        public void ProcessYearBreak(ref int currentYear, ref double yearRev, int i)
        {
            System.Console.WriteLine($"{currentYear}    Revenue:  {yearRev}"); //print current year and the total revenue for the year
            currentYear = bookings[i].GetTrainingDate().Year; //reset current year
            yearRev = listings[FindListingByDate(bookings[i].GetTrainingDate())].GetSessionCost(); //reset the yearly rev
        }

    }
}