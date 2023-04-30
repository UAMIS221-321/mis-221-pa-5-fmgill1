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
                if(bookings[i].GetCustomerEmail() == customerEmail && bookings[i].GetSessionStatus() == "completed")
                {
                    System.Console.WriteLine(bookings[i].ToString());
                    count ++;
                }
            }
            System.Console.WriteLine($"This customer has {count} COMPLETED sessions");
            System.Console.WriteLine("Would you like to save this report? (Yes or No)");
            string userInput = Console.ReadLine().ToUpper();
            string fileName = "";
            if(userInput == "YES")
            {
                System.Console.WriteLine("What would you like to name the file");
                fileName = Console.ReadLine(); 
                StreamWriter outFile = new StreamWriter(fileName);
                for (int i = 0; i < Booking.GetCount(); i++)
                {
                    if(bookings[i].GetCustomerEmail() == customerEmail && bookings[i].GetSessionStatus() == "completed")
                    {
                        outFile.WriteLine(bookings[i].ToFile());
                    }
                }
                outFile.Close();
                System.Console.WriteLine($"Your report was saved to a file called {fileName}.txt");
            }
            //System.Console.WriteLine();
            listingUtility.Pause();

        }
        public void SortByName()
        {
            for (int i= 0; i < Booking.GetCount()-1; i++)
            {
                int min = i;
                for( int j = i + 1; j < Booking.GetCount(); j++)
                {
                    if(bookings[min].GetCustomerName().CompareTo(bookings[j].GetCustomerName())>0)
                    {
                        min = j;
                    }
                    if(min != i)
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

        public void HistoricalCustomerReport()
        {
            int count = 0; 
            SortByName();  //not properly sorting by name or date
            //SortByDate();
            string currName = bookings[0].GetCustomerName();
            for(int i = 0; i < Booking.GetCount(); i++)
            {
                if(bookings[i].GetCustomerName() == currName)
                {
                    count++; //increments the count for the number of sessions per customer
                    //System.Console.WriteLine(bookings[i].ToString());
                    
                }
                else 
                {
                    int min = i;
                    for(int j = i + 1; j < count; j++)
                    {
                        if(bookings[min].GetTrainingDate().CompareTo(bookings[j].GetTrainingDate()) > 0)
                        {
                            min = j;
                        }
                        if(min != i)
                        {
                            Swap(min, i);
                        }
                    }
                    ProcessBreak(ref count, ref currName, i);
                }
                System.Console.WriteLine(bookings[i].ToString());

            
            }
            ProcessBreak(ref count, ref currName, 0);
            //System.Console.WriteLine(bookings);
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
                System.Console.WriteLine($"Your historical customer report was saved to {fileName}.txt");
            }
            listingUtility.Pause();
        }
        public void ProcessBreak(ref int count, ref string currName, int i)
        {
            System.Console.WriteLine($"{currName} had {count} sessions");
            currName = bookings[i].GetCustomerName();
            count = 1;
        }
        
        public void SortByDate()
        {
            for (int i = 0; i < Booking.GetCount() - 1; i++)
            {
                int min = i;
                for( int j = i + 1; j <Booking.GetCount(); j++)
                {
                    if(bookings[min].GetTrainingDate().CompareTo(bookings[j].GetTrainingDate()) > 0)
                    {
                        min = j;
                    }
                    if(min != i)
                    {
                        Swap(min, i);
                    }
                }
            }
        }
        public int FindListingByDate(DateOnly date)
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

        
        public void HistoricalRevenue()
        {
            SortByDate();
            int currentMonth = bookings[0].GetTrainingDate().Month;
            int currentYear = bookings[0].GetTrainingDate().Year;
            double monthRev = listings[FindListingByDate(bookings[0].GetTrainingDate())].GetSessionCost();
            double yearRev = listings[FindListingByDate(bookings[0].GetTrainingDate())].GetSessionCost();
            //double monthRev = listings[FindListingByDate(bookings[0].GetTrainingDate())].GetSessionCost();
            System.Console.WriteLine("Welcome to the Historical Revenue Report! These numbers are based on sessions that have been COMPLETED");
            listingUtility.Pause();
            for(int i = 0; i < Booking.GetCount(); i++)
            {
                //if(bookings[i].GetSessionStatus() != "open")
                //{
                    if(bookings[i].GetTrainingDate().Year == listings[FindListingByDate(bookings[i].GetTrainingDate())].GetYear())
                    {
                        yearRev += listings[FindListingByDate(bookings[i].GetTrainingDate())].GetSessionCost();
                        if(bookings[i].GetTrainingDate().Month == listings[FindListingByDate(bookings[i].GetTrainingDate())].GetMonth())
                        {
                            monthRev += listings[FindListingByDate(bookings[i].GetTrainingDate())].GetSessionCost();
                        }
                        else
                        {
                            System.Console.WriteLine("End of month");
                            ProcessMonthBreak(ref currentMonth, ref monthRev, i);
                        }
                    }
                    else
                    {
                        ProcessYearBreak(ref currentYear, ref yearRev, i);
                    }
                //}

            }
            ProcessMonthBreak(ref currentMonth, ref monthRev, 0);
            ProcessYearBreak(ref currentYear, ref yearRev, 0);
        }
        public void HistoricalRevenueReport()
        {
            SortByDate();
            int currentMonth = bookings[0].GetTrainingDate().Month;
            int currentYear = bookings[0].GetTrainingDate().Year;
            double cost = listings[FindListingByDate(bookings[0].GetTrainingDate())].GetSessionCost();
            //bookingUtility.FindBooking(bookings[0].GetTrainingDate())
            double monthRev = listings[0].GetSessionCost();
            double yearRev = listings[0].GetSessionCost();
            System.Console.WriteLine("Welcome to the Historical Revenue Report! These numbers are based on sessions that have been COMPLETED");
            for(int i = 0; i < Booking.GetCount(); i++)
            {
                if(bookings[i].GetSessionStatus() != "open")
                {
                    if(bookings[i].GetTrainingDate().Year == currentYear)
                    {
                        yearRev += listings[i].GetSessionCost();
                        if(bookings[i].GetTrainingDate().Month == currentMonth)
                        {
                             monthRev += listings[i].GetSessionCost();
                        }
                        else
                        {
                            ProcessMonthBreak(ref currentMonth, ref monthRev, i);
                        }
                    }
                    else
                    {
                        ProcessYearBreak(ref currentYear, ref yearRev, i);
                    }
                }

            }
            ProcessMonthBreak(ref currentMonth, ref monthRev, 0);
            ProcessYearBreak(ref currentYear, ref yearRev, 0);
        }

        public void ProcessMonthBreak(ref int currentMonth, ref double monthRev, int i)
        {
            System.Console.WriteLine($"{currentMonth}  Revenue:  {monthRev}");
            currentMonth = bookings[i].GetTrainingDate().Month;
            monthRev = listings[FindListingByDate(bookings[i].GetTrainingDate())].GetSessionCost();
            listingUtility.Pause();
            
        }
        public void ProcessYearBreak(ref int currentYear, ref double yearRev, int i)
        {
            System.Console.WriteLine($"{currentYear}  Revenue:  {yearRev}");
            currentYear = bookings[i].GetTrainingDate().Year;
            yearRev = listings[FindListingByDate(bookings[0].GetTrainingDate())].GetSessionCost();
            listingUtility.Pause();
        }

    }
}