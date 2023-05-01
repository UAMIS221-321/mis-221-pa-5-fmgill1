namespace mis_221_pa_5_fmgill1
{
    public class TrainerUtility
    {
        private Trainer[] trainers; 

        public TrainerUtility(Trainer[] trainers)
        {
            this.trainers = trainers;
            GetTrainersFromFile(trainers); 
        }
        public void AddTrainer()
        {

            Trainer newTrainer = new Trainer();

            newTrainer.SetTrainerID(); //set trainer ID for the trainer
            System.Console.WriteLine("Enter trainer name:");
            newTrainer.SetTrainerName(Console.ReadLine());
            //set the trainer name from the input
            System.Console.WriteLine("Enter mailing address: ");
            newTrainer.SetTrainerMail(Console.ReadLine());
            //set mailing address from the input
            System.Console.WriteLine("Enter email address:");
            newTrainer.SetTrainerEmail(Console.ReadLine());
            //set trainer email from the input

            trainers[Trainer.GetCount()] = newTrainer;
            System.Console.WriteLine("Got it! This trainer has been added!");
            Pause();
            Trainer.IncCount();
            Save(trainers);
            
        }

        public void Save(Trainer[] trainers)
        {
            StreamWriter outFile = new StreamWriter("trainers.txt"); //create streamwriter for trainers.txt

            for (int i=0; i<Trainer.GetCount(); i++) //loop through all trainers
            {
                outFile.WriteLine(trainers[i].ToFile()); //write to file
            } 

            outFile.Close(); //close file
        }

        public void GetTrainersFromFile(Trainer[] trainers)
        {
            StreamReader inFile = new StreamReader("trainers.txt"); //create streamreader object

            int trainerCount = 0;
            Trainer.SetCount(0);

            string line = inFile.ReadLine(); //priming read 
            while(line != null) //while there is input
            {
                string[] temp = line.Split("#"); //split the data based on the # delimiter
                trainerCount += temp.Length;
                int trainerID = int.Parse(temp[0]);
                bool status = bool.Parse(temp[4]);
                trainers[Trainer.GetCount()]= new Trainer(trainerID, temp[1], temp[2], temp[3], status); //add temp data to new Trainer object
                Trainer.IncCount();
                line = inFile.ReadLine(); //update read
            }
            inFile.Close(); //close file
        }
        public int Find(int searchVal) //find the index for the trainer 
        {
            for(int i = 0; i < Trainer.GetCount(); i++)
            {
                if(trainers[i].GetTrainerID() == searchVal)
                {
                    return i;
                }
            }
            return -1; //return -1 if not found
        }
        public int FindTrainer(string trainerName) //find trainer index based on the trainer name
        {
            for( int i = 0; i < Trainer.GetCount(); i++)
            {
                if(trainers[i].GetTrainerName() == trainerName)
                {
                    return i;
                }
            }
            return -1; //return -1 if trainer index not found from name
        }

        public void UpdateTrainer() 
        {
            System.Console.WriteLine("Please enter the ID of the trainer you would to update");
            int searchVal = int.Parse(Console.ReadLine());
            int foundIndex = Find(searchVal);
            if( foundIndex != -1) //if trainer ID was found
            {
                System.Console.WriteLine("Please enter trainer name");
                trainers[foundIndex].SetTrainerName(Console.ReadLine()); //set user input to the trainer name
                System.Console.WriteLine("Please enter trainer mailing address");
                trainers[foundIndex].SetTrainerMail(Console.ReadLine()); //set user input to the trainer mail address
                System.Console.WriteLine("Please enter trainer email");
                trainers[foundIndex].SetTrainerEmail(Console.ReadLine()); //set user input to the trainer email address 
                System.Console.WriteLine("Got it! Your trainer was updated");
                Pause();
                Save(trainers);
            }
            else //in case the trainer ID wasnt found
            {
                System.Console.WriteLine("Trainer not found");
            }

        }

        public void DeleteTrainer()
        {
            System.Console.WriteLine("Please enter the ID of the trainer you would to delete");
            int searchVal = int.Parse(Console.ReadLine());
            int foundIndex = Find(searchVal);
            if(foundIndex != -1) //if the trainer ID is found
            {
                trainers[foundIndex].SetIsDeleted(); //set that trainer to deleted 
                System.Console.WriteLine("Got it! Your trainer was deleted!");
                Pause();
                Save(trainers);
                Trainer.DecCount(); //decrement count to account for the deleted trainer
            }
            else //in case the trainer ID was not found
            {
                System.Console.WriteLine("Sorry, this trainer was not found");
            }
        
        }
        public void Pause()
        {
            System.Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
