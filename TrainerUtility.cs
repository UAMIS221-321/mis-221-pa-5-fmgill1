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

            newTrainer.SetTrainerID();
            System.Console.WriteLine("Enter trainer name:");
            newTrainer.SetTrainerName(Console.ReadLine());
            System.Console.WriteLine("Enter mailing address: ");
            newTrainer.SetTrainerMail(Console.ReadLine());
            System.Console.WriteLine("Enter email address:");
            newTrainer.SetTrainerEmail(Console.ReadLine());

            trainers[Trainer.GetCount()] = newTrainer;
            System.Console.WriteLine("Got it! This trainer has been added!");
            Trainer.IncCount();
            Save(trainers);
            
        }

        public void Save(Trainer[] trainers)
        {
            StreamWriter outFile = new StreamWriter("trainers.txt");
            

            for (int i=0; i<Trainer.GetCount(); i++)
            {
                outFile.WriteLine(trainers[i].ToFile());
            } 

            outFile.Close();
        }

        public void GetTrainersFromFile(Trainer[] trainers)
        {
            StreamReader inFile = new StreamReader("trainers.txt");

            int trainerCount = 0;
            Trainer.SetCount(0);

            string line = inFile.ReadLine();
            while(line != null)
            {
                string[]temp = line.Split("#");
                trainerCount += temp.Length;
                int trainerID = int.Parse(temp[0]);
                bool status = bool.Parse(temp[4]);
                trainers[Trainer.GetCount()]= new Trainer(trainerID, temp[1], temp[2], temp[3], status);
                Trainer.IncCount();
                line = inFile.ReadLine();
            }
            inFile.Close();
        }
        public int Find(int searchVal)
        {
            for(int i=0; i<Trainer.GetCount(); i++)
            {
                if(trainers[i].GetTrainerID() == searchVal)
                {
                    return i;
                }
            }
            return -1;
        }
//just changed this, not sure if right
        public int FindTrainer(string trainerName)
        {
            //Trainer trainer = new Trainer();
            for( int i = 0; i<Trainer.GetCount(); i++)
            {
                if(trainers[i].GetTrainerName()==trainerName)
                {
                    //int trainer = trainers[i];
                    return i;
                }
            }
            return -1;
        }

        public void UpdateTrainer()
        {
            System.Console.WriteLine("Please enter the ID of the trainer you would to update");
            int searchVal = int.Parse(Console.ReadLine());
            int foundIndex = Find(searchVal);
            if( foundIndex != -1)
            {
                System.Console.WriteLine("Please enter trainer name");
                trainers[foundIndex].SetTrainerName(Console.ReadLine());
                System.Console.WriteLine("Please enter trainer mailing address");
                trainers[foundIndex].SetTrainerMail(Console.ReadLine());
                System.Console.WriteLine("Please enter trainer email");
                trainers[foundIndex].SetTrainerEmail(Console.ReadLine());
                System.Console.WriteLine("Got it! Your trainer was updated");
                Pause();
                Save(trainers);
            }
            else
            {
                System.Console.WriteLine("Trainer not found");
            }

        }

        public void DeleteTrainer()
        {
            System.Console.WriteLine("Please enter the ID of the trainer you would to delete");
            int searchVal = int.Parse(Console.ReadLine());
            int foundIndex = Find(searchVal);
            if(foundIndex != -1)
            {
                //Trainer test = trainers[foundIndex];
                trainers[foundIndex].SetIsDeleted();
                System.Console.WriteLine("Got it! Your trainer was deleted!");
                Pause();
                Save(trainers);
                Trainer.DecCount();
            }
            else
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
