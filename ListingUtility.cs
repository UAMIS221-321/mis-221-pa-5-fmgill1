namespace mis_221_pa_5_fmgill1
{
    public class ListingUtility
    {
        private Listings[] listings;
        private Trainer[] trainers;

        public ListingUtility(Listings[] listings, Trainer[] trainers)
        {
            this.listings = listings;
            this.trainers = trainers;
            GetListingsFromFile(listings); //ensures the listings are always opened from file 
        }

        public void AddListing()
        {
            
            System.Console.WriteLine("Please enter trainer name:");
            string name = Console.ReadLine();
            int found = FindATrainer(name); //finds trainer index based on their name
            if(found != -1 && trainers[found].GetIsDeleted() == false) //if trainer index is found and the trainer isnt deleted makes it so a trainer must be in the system to add a listing
            {
                Listings addListing = new Listings();
                addListing.SetListingID(); //creates listingID
                addListing.SetTrainerName(name); //adds trainers name to listing
                System.Console.WriteLine("Enter the session date:");
                addListing.SetSessionDate(DateOnly.Parse(Console.ReadLine())); //adds date to listing
                System.Console.WriteLine("What type of workout will this listing be?");
                addListing.SetWorkoutType(Console.ReadLine()); //allows trainer to choose what workout class they want to do
                System.Console.WriteLine("Enter the session time:");
                addListing.SetSessionTime(TimeOnly.Parse(Console.ReadLine())); // allows trainer to enter preferred time
                System.Console.WriteLine("Enter the session cost:");
                addListing.SetSessionCost(double.Parse(Console.ReadLine())); //sets cost to listing
                System.Console.WriteLine("Is the session available? ");
                if(Console.ReadLine().ToUpper() == "YES")
                {
                    addListing.SetSessionIsAvailable();
                }
                listings[Listings.GetCount()] = addListing;
                System.Console.WriteLine("Got it! Your new listing was made :)");
                Pause();
                Listings.IncCount();
                Save();
            }
            else
            {
                System.Console.WriteLine("Sorry! This trainer is not in the system. You can go back to the main menu and add them!"); //catches if a nonexistent trainer attempts to add listing
                Pause();
            }

            
            
        }
        public void Save() //saves to listings.txt
        {
            StreamWriter outFile = new StreamWriter("listings.txt"); //open
            for (int i = 0; i < Listings.GetCount(); i++)
            {
                outFile.WriteLine(listings[i].ToFile());//write to file
            } 
            outFile.Close(); //close
        }
        public void GetListingsFromFile(Listings[] listings)
        {
            StreamReader inFile = new StreamReader("listings.txt"); //open
            int count = 0;
            Listings.SetCount(0);

            string line = inFile.ReadLine();//priming read
            while(line != null)
            {
                string[]temp = line.Split("#");
                count += temp.Length;
                int listingID = int.Parse(temp[0]);
                string name = temp[1];
                string workoutType = temp[2];
                DateOnly sessionDate = DateOnly.Parse(temp[3]); //parse for dateonly
                double sessionCost = double.Parse(temp[5]);
                bool sessionIsAvailable = bool.Parse(temp[6]); //parse string for bool
                bool isDeleted = bool.Parse(temp[7]);
                listings[Listings.GetCount()]= new Listings(listingID, name, workoutType, sessionDate, TimeOnly.Parse(temp[4]), sessionCost, sessionIsAvailable, isDeleted, int.Parse(temp[8]), int.Parse(temp[9]));
                //creates new Listing based on constructor and all the data needed for the file
                Listings.IncCount();
                line = inFile.ReadLine(); //update read
            }
            inFile.Close(); //close
        }

        public int FindAListing(int sessionChoice) //finds a listing index based on user input, returns -1 if not found
        {
            for(int i = 0; i < Listings.GetCount(); i++)
            {
                if(listings[i].GetListingID() == sessionChoice)
                {
                    return i;
                }
            }
            return -1;
        }
        

        public int FindAListing(string trainerName) //finds a listing index based on the trainers name inputted, returns -1 if not found
        {
            for(int i = 0; i < Listings.GetCount(); i++)
            {
                if(listings[i].GetTrainerName() == trainerName)
                {
                    return i;
                }
            }
            return -1;
        }
        public int FindATrainer(string trainerName) //finds a trainer index based on the trainers name, returns -1 if not found
        {
            for(int i = 0; i < Trainer.GetCount(); i++)
            {
                if(trainers[i].GetTrainerName() == trainerName)
                {
                    return i;
                }
            }
            return -1;
        }
        public void PrintTrainerListings(string trainerName) //prints out all listings by a trainers name
        {
            for (int i = 0; i < Listings.GetCount(); i++)
            {
                if(listings[i].GetIsDeleted() == false && listings[i].GetTrainerName() == trainerName) //checks if listing is deleted and trainer on listing matches input
                {
                    System.Console.WriteLine(listings[i].ToString()); //prints each occurence in for loop
                }
            }
        }

        public void UpdateListing()
        {
            System.Console.WriteLine("Please enter trainer name:");
            string name = Console.ReadLine();
            int found = FindATrainer(name); //finds trainer by name
            if(found != -1 && trainers[found].GetIsDeleted() == false) //if trainer name was found and trainer isn't deleted 
            {
                PrintTrainerListings(name); //print all listings under that trainer
                System.Console.WriteLine("Please enter the ID for the listing you want to update:");
                int listingID = int.Parse(Console.ReadLine());
                int foundIndex = FindAListing(listingID); //checks that user input is valid 
                if(foundIndex == -1) //if listing ID is not valid
                {
                    System.Console.WriteLine("Sorry, that isn't a valid listing ID to update.");
                    Pause();
                }
                else if(foundIndex != -1) //if listing is found
                {
                    //update any info needed 
                    System.Console.WriteLine("Please enter session date:");
                    listings[foundIndex].SetSessionDate(DateOnly.Parse(Console.ReadLine()));
                    System.Console.WriteLine("Please enter the workout type:");
                    listings[foundIndex].SetWorkoutType(Console.ReadLine());
                    System.Console.WriteLine("Please enter session time:");
                    listings[foundIndex].SetSessionTime(TimeOnly.Parse(Console.ReadLine()));
                    System.Console.WriteLine("Please enter a session cost:");
                    listings[foundIndex].SetSessionCost(double.Parse(Console.ReadLine()));
                    System.Console.WriteLine("Is the session taken?");
                    if(Console.ReadLine().ToUpper() == "YES") 
                    {
                        listings[foundIndex].SetSessionIsAvailable(); //changes session availability 
                    }
                    Save();
                    System.Console.WriteLine("Got it! Your session has been updated :)");
                    Pause();
                }
                
            }
            else if(found == -1) //if no listings were found under the trainers name 
            {
                System.Console.WriteLine("Sorry! You don't have any listings at the moment. You can add one though!");
                Pause();
            }
            else //if trainer isnt found in search 
            {
                System.Console.WriteLine("Sorry! This trainer doens't exist or is deleted.");
                Pause();
            }
        }
        public void DeleteListing()
        {
            System.Console.WriteLine("Hi Trainer! Please enter your name:");
            string name = Console.ReadLine();
            int found = FindAListing(name); //find listing index by trainers name input
            if(found != -1 && listings[found].GetIsDeleted() == false) //if name is found and the listing isnt deleted 
            {
                PrintTrainerListings(name); //print all the trainers listings 
                System.Console.WriteLine("Please enter the ID of the session want to delete:");
                int listingID = int.Parse(Console.ReadLine());
                int foundIndex = FindAListing(listingID); //finds index based on input
                if(foundIndex != -1 && listings[foundIndex].GetIsDeleted() == false) //if index is found in listings and the listing isnt deleted already
                {
                    System.Console.WriteLine("Got it! Your session has been deleted.");
                    Listings test = listings[foundIndex];
                    listings[foundIndex].SetIsDeleted(); //sets that listing to be deleted 
                    Save();
                    Pause();
                }
                else
                {
                    System.Console.WriteLine("Sorry! Listing was not found."); //in case listing was not found from ID input
                    Pause();
                }
            }
            else //in case there are no listings found under that trainers name
            {
                System.Console.WriteLine("Sorry! You don't have any listings at the moment. You can add one though!"); 
                Pause();
            }
            
        
        }
        public void PrintListings() //prints all listings that are available and are not deleted 
        {
            for (int i = 0; i < Listings.GetCount(); i++)
            {
                if(listings[i].GetSessionIsAvailabile() == true && listings[i].GetIsDeleted() == false)
                {
                    System.Console.WriteLine(listings[i].ToString());
                }
            }
        }
        public void Pause()
        {
            System.Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

