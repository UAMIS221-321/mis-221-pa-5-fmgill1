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
            GetListingsFromFile(listings);
        }

        public void AddListing()
        {
            
            System.Console.WriteLine("Please enter trainer name:");
            string name = Console.ReadLine();
            int found = FindATrainer(name);
            if(found != -1 && trainers[found].GetIsDeleted() == false)
            {
                Listings addListing = new Listings();
                addListing.SetListingID();
                addListing.SetTrainerName(name);
                System.Console.WriteLine("Enter the session date:");
                addListing.SetSessionDate(DateOnly.Parse(Console.ReadLine()));
                System.Console.WriteLine("What type of workout will this listing be?");
                addListing.SetWorkoutType(Console.ReadLine());
                System.Console.WriteLine("Enter the session time:");
                addListing.SetSessionTime(TimeOnly.Parse(Console.ReadLine()));
                System.Console.WriteLine("Enter the session cost:");
                addListing.SetSessionCost(double.Parse(Console.ReadLine()));
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
                System.Console.WriteLine("Sorry! This trainer is not in the system. You can go back to the main menu and add them!");
                Pause();
            }

            
            
        }
        public void Save()
        {
            StreamWriter outFile = new StreamWriter("listings.txt");
            for (int i = 0; i < Listings.GetCount(); i++)
            {
                outFile.WriteLine(listings[i].ToFile());
            } 
            outFile.Close();
        }
        public void GetListingsFromFile(Listings[] listings)
        {
            StreamReader inFile = new StreamReader("listings.txt");
            int count = 0;
            Listings.SetCount(0);

            string line = inFile.ReadLine();
            while(line != null)
            {
                string[]temp = line.Split("#");
                count += temp.Length;
                int listingID = int.Parse(temp[0]);
                string name = temp[1];
                string workoutType = temp[2];
                DateOnly sessionDate = DateOnly.Parse(temp[3]);
                double sessionCost = double.Parse(temp[5]);
                bool sessionIsAvailable = bool.Parse(temp[6]);
                bool isDeleted = bool.Parse(temp[7]);
                listings[Listings.GetCount()]= new Listings(listingID, name, workoutType, sessionDate, TimeOnly.Parse(temp[4]), sessionCost, sessionIsAvailable, isDeleted, int.Parse(temp[8]), int.Parse(temp[9]));
                Listings.IncCount();
                line = inFile.ReadLine();
            }
            inFile.Close();
        }

        public int FindAListing(int sessionChoice)
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
        

        public int FindAListing(string trainerName)
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
        public int FindATrainer(string trainerName)
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
        public void PrintTrainerListings(string trainerName)
        {
            for (int i = 0; i < Listings.GetCount(); i++)
            {
                if(listings[i].GetIsDeleted() == false && listings[i].GetTrainerName() == trainerName)
                {
                    System.Console.WriteLine(listings[i].ToString());
                }
            }
        }

        public void UpdateListing()
        {
            System.Console.WriteLine("Please enter trainer name:");
            string name = Console.ReadLine();
            int found = FindATrainer(name);
            if(found != -1 && trainers[found].GetIsDeleted() == false)
            {
                PrintTrainerListings(name);
                System.Console.WriteLine("Please enter the ID for the listing you want to update:");
                int listingID = int.Parse(Console.ReadLine());
                int foundIndex = FindAListing(listingID);
                if(foundIndex == -1)
                {
                    System.Console.WriteLine("Sorry, that isn't a valid listing ID to update.");
                    Pause();
                }
                else if(foundIndex != -1)
                {
                    System.Console.WriteLine("Please enter session date:");
                    listings[foundIndex].SetSessionDate(DateOnly.Parse(Console.ReadLine()));
                    System.Console.WriteLine("Please enter the workout type:");
                    listings[foundIndex].SetWorkoutType(Console.ReadLine());
                    System.Console.WriteLine("Please enter session time:");
                    listings[foundIndex].SetSessionTime(TimeOnly.Parse(Console.ReadLine()));
                    System.Console.WriteLine("Please enter a session cost:");
                    listings[foundIndex].SetSessionCost(double.Parse(Console.ReadLine()));
                    System.Console.WriteLine("Is the session taken?");
                    if(Console.ReadLine() == "Yes")
                    {
                        listings[foundIndex].SetSessionIsAvailable();
                    }
                    Save();
                    System.Console.WriteLine("Got it! Your session has been updated :)");
                    Pause();
                }
                
            }
            else if(found == -1)
            {
                System.Console.WriteLine("Sorry! You don't have any listings at the moment. You can add one though!");
                Pause();
            }
            else
            {
                System.Console.WriteLine("Sorry! This trainer doens't exist or is deleted.");
                Pause();
            }
        }
        public void DeleteListing()
        {
            System.Console.WriteLine("Hi Trainer! Please enter your name:");
            string name = Console.ReadLine();
            int found = FindAListing(name);
            if(found != -1 && listings[found].GetIsDeleted() == false)
            {
                PrintTrainerListings(name);
                System.Console.WriteLine("Please enter the ID of the session want to delete:");
                int listingID = int.Parse(Console.ReadLine());
                int foundIndex = FindAListing(listingID);
                if(foundIndex != -1 && listings[foundIndex].GetIsDeleted() == false)
                {
                    System.Console.WriteLine("Got it! Your session has been deleted.");
                    Listings test = listings[foundIndex];
                    listings[foundIndex].SetIsDeleted();
                    Save();
                    Pause();
                }
                else
                {
                    System.Console.WriteLine("Sorry! Listing was not found.");
                    Pause();
                }
            }
            else
            {
                System.Console.WriteLine("Sorry! You don't have any listings at the moment. You can add one though!");
                Pause();
            }
            
        
        }
        public void PrintListings()
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

