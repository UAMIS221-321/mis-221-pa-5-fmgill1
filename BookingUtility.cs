namespace mis_221_pa_5_fmgill1
{
    public class BookingUtility
    {
        private Booking[] bookings;
        private ListingUtility listingUtility;
        private Trainer [] trainers;
        private Listings[] listings;
        private TrainerUtility trainerUtility;

        public BookingUtility() //no arg constructor
        {

        }

        public BookingUtility(Booking[] bookings, ListingUtility listingUtility, Trainer[] trainers, Listings[] listings, TrainerUtility trainerUtility)
        {
            this.bookings = bookings;
            this.listingUtility = listingUtility;
            this.trainers = trainers;
            this.listings = listings;
            this.trainerUtility = trainerUtility;
            GetBookingsFromFile(bookings); //called in constructor to ensure it always occurs 

        }
        public void ViewAllSessions() //used to customer can view the session options rather than entering a random ID
        {
            listingUtility.PrintListings();
        }

        public void GetBookingsFromFile(Booking[] bookings){
            StreamReader inFile = new StreamReader("transactions.txt"); //reads transactions file
            int count = 0;

            Booking.SetCount(0);
            string line = inFile.ReadLine();
            while(line != null) //while there is input in the file
            {
                string[]temp = line.Split("#"); //splits data by delimiter
                count += temp.Length; 
                int sessionID = int.Parse(temp[0]);
                DateOnly trainingDate = DateOnly.Parse(temp[3]);
                int trainerID = int.Parse(temp[4]);
                bookings[Booking.GetCount()]= new Booking(sessionID, temp[1], temp[2], trainingDate, trainerID, temp[5], temp[6]);
                Booking.IncCount();
                line = inFile.ReadLine(); //update read for line
            }
            inFile.Close(); //close file
        }

    
        public void BookASession()
        {
            Booking booking = new Booking();

            ViewAllSessions(); //prints all listings out to user 

            System.Console.WriteLine("Please enter the ID of the session you'd like to book:");
            int choice = int.Parse(Console.ReadLine());
            int found = listingUtility.FindAListing(choice); //finds the listing that matches with user input
            if(found != -1) //if the listing was a match
            {
                booking.SetSessionID();
                System.Console.WriteLine("Please enter your name:");
                booking.SetCustomerName(Console.ReadLine());
                System.Console.WriteLine("Please enter your email address:");
                booking.SetCustomerEmail(Console.ReadLine());

                int toBook = listingUtility.FindAListing(choice); //used to take the data from listings and add to booking 
            
                booking.SetTrainingDate(listings[toBook].GetSessionDate()); //adds listing date to booking
                booking.SetTrainerName(listings[toBook].GetTrainerName()); //adds trainer name booking
                string trainerName = listings[toBook].GetTrainerName(); 

                int trainerBooking = trainerUtility.FindTrainer(trainerName); //finds the index of the trainer based on their name
                booking.SetTrainerID(trainers[trainerBooking].GetTrainerID()); //set trainer ID from name in trainer.txt to the booking
                booking.SetSessionStatus("booked"); //set status to booked 
                listings[toBook].SetSessionIsAvailable(); //changes the availability of the listing
                
            
                bookings[Booking.GetCount()] = booking;
                System.Console.WriteLine("Your session was booked!");
                Booking.IncCount();
                Save();
            }
            else
            {
                System.Console.WriteLine("Sorry! That isn't a valid session ID."); //error handling
            }
            
                    
        }
        public int FindBooking(int sessionPick) //finds booking (or the index for the booking) based on the session ID the user enters 
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
        public void FindBooking(string customerName) //prints out the bookings under a customers name
        {
            for(int i = 0; i < Booking.GetCount(); i++)
            {
                if(bookings[i].GetCustomerName() == customerName)
                {
                    System.Console.WriteLine(bookings[i].ToString());
                }
            }
        }
        public void Save() //saves data to transactions.txt 
        {
            StreamWriter outFile = new StreamWriter("transactions.txt"); //open/write
            for (int i=0; i<Booking.GetCount(); i++)
            {
                outFile.WriteLine(bookings[i].ToFile()); //write to file
            } 
            outFile.Close(); //close file
        }
        public void UpdateSessionStatus()
        {
            System.Console.WriteLine("Enter the name you booked your session under:");
            string name = Console.ReadLine();
            FindBooking(name); //prints bookings for that name, allows customers to view their booked sessions and pick which needs updating

            System.Console.WriteLine("Enter the ID for the session you want to edit:");
            int session = int.Parse(Console.ReadLine());
            int foundIndex = FindBooking(session); //finds index for a booking based on the user input
            
            if(foundIndex != -1 && bookings[foundIndex].GetCustomerName() == name) //if their user input is valid and the customer name matches the name set for the booking
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