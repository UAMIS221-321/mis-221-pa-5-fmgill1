namespace mis_221_pa_5_fmgill1
{
    public class BookingUtility
    {
        private Booking[] bookings;
        private ListingUtility listingUtility;
        private Trainer [] trainers;
        private Listings[] listings;
        private TrainerUtility trainerUtility;

        public BookingUtility()
        {

        }

        public BookingUtility(Booking[] bookings, ListingUtility listingUtility, Trainer[] trainers, Listings[] listings, TrainerUtility trainerUtility)
        {
            this.bookings = bookings;
            this.listingUtility = listingUtility;
            this.trainers = trainers;
            this.listings = listings;
            this.trainerUtility = trainerUtility;
            GetBookingsFromFile(bookings);

        }
        public void ViewAllSessions()
        {
            listingUtility.PrintListings();
        }

        public void GetBookingsFromFile(Booking[] bookings){
            StreamReader inFile = new StreamReader("transactions.txt");
            int count = 0;

            Booking.SetCount(0);
            string line = inFile.ReadLine();
            while(line != null)
            {
                string[]temp = line.Split("#");
                count += temp.Length;
                int sessionID = int.Parse(temp[0]);
                DateOnly trainingDate = DateOnly.Parse(temp[3]);
                int trainerID = int.Parse(temp[4]);
                bookings[Booking.GetCount()]= new Booking(sessionID, temp[1], temp[2], trainingDate, trainerID, temp[5], temp[6]);
                Booking.IncCount();
                line = inFile.ReadLine();
            }
            inFile.Close();
        }

    
        public void BookASession()
        {
            Booking booking = new Booking();
            //Listings listing = new Listings();

            listingUtility.PrintListings();
            //choose the listing by ID

            System.Console.WriteLine("Please enter the ID of the session you'd like to book:");
            int choice = int.Parse(Console.ReadLine());
            int found = listingUtility.FindAListing(choice);
            if(found != -1)
            {
                booking.SetSessionID();
                System.Console.WriteLine("Please enter your name:");
                booking.SetCustomerName(Console.ReadLine());
                System.Console.WriteLine("Please enter your email address:");
                booking.SetCustomerEmail(Console.ReadLine());

                int toBook = listingUtility.FindAListing(choice);
            
                booking.SetTrainingDate(listings[toBook].GetSessionDate());
                booking.SetTrainerName(listings[toBook].GetTrainerName());
                string trainerName = listings[toBook].GetTrainerName();

                int trainerBooking = trainerUtility.FindTrainer(trainerName);
                booking.SetTrainerID(trainers[trainerBooking].GetTrainerID());
                booking.SetSessionStatus("booked");
                listings[toBook].SetSessionIsAvailable();
                
            
                bookings[Booking.GetCount()] = booking;
                System.Console.WriteLine("Your session was booked!");
                Booking.IncCount();
                Save();
            }
            else
            {
                System.Console.WriteLine("Sorry! That isn't a valid session ID.");
            }
            
                    
        }
        public int FindBooking(int sessionPick)
        {
            for( int i = 0; i < Booking.GetCount(); i++)
            {
                if(bookings[i].GetSessionID() == sessionPick)
                {
                    return i;
                }
            }
            return -1;
        }
        public void FindBooking(string customerName)
        {
            for(int i = 0; i < Booking.GetCount(); i++)
            {
                if(bookings[i].GetCustomerName() == customerName)
                {
                    System.Console.WriteLine(bookings[i].ToString());
                    //return i;
                }
            }
           // return -1;
        }
        public void Save()
        {
            StreamWriter outFile = new StreamWriter("transactions.txt");
            for (int i=0; i<Booking.GetCount(); i++)
            {
                outFile.WriteLine(bookings[i].ToFile());
            } 
            outFile.Close();
        }
        public void UpdateSessionStatus()
        {
            System.Console.WriteLine("Enter the name you booked your session under:");
            string name = Console.ReadLine();
            FindBooking(name);

            System.Console.WriteLine("Enter the ID for the session you want to edit:");
            int session = int.Parse(Console.ReadLine());
            int foundIndex = FindBooking(session);
            
            if(foundIndex != -1 && bookings[foundIndex].GetCustomerName() == name)
            {
            
                System.Console.WriteLine("Would you like to cancel your session?");
                string userInput = Console.ReadLine().ToLower();
                if(userInput == "yes")
                {
                    bookings[foundIndex].SetSessionStatus("open");
                }
                System.Console.WriteLine("Did you complete your session?");
                string otherInput = Console.ReadLine().ToLower();
                if(otherInput == "yes")
                {
                    bookings[foundIndex].SetSessionStatus("completed");
                }
                System.Console.WriteLine("Your changes have been made!");
                Save();
            }
            else
            {
                System.Console.WriteLine("Sorry! That session was not found. Please try again!");
            }

        }
        public void Pause()
        {
            System.Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

    }
}