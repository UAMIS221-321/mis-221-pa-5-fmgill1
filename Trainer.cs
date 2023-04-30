namespace mis_221_pa_5_fmgill1
{
    public class Trainer
    {
         private int trainerID;

        private string trainerName;

        private string trainerMailingAddress;

        private string trainerEmail;

        private bool isDeleted;

        static private int count;

        public Trainer(){
    
        }
        public Trainer(int trainerID, string trainerName, string trainerMailingAddress, string trainerEmail, bool isDeleted)
        {
            this.trainerID = trainerID;
            this.trainerName = trainerName;
            this.trainerMailingAddress = trainerMailingAddress;
            this.trainerEmail = trainerEmail;
            this.isDeleted = isDeleted;
        }
        public void SetTrainerID(){
            this.trainerID = count + 1;
        }

        public int GetTrainerID(){
            return trainerID;
        }
       
        public void SetTrainerName(string trainerName)
        {
            this.trainerName = trainerName;
        }
        public string GetTrainerName(){
            return trainerName;
        }

        public void SetTrainerMail(string trainerMailingAddress)
        {
            this.trainerMailingAddress = trainerMailingAddress;
        }
        public string GetTrainerMail()
        { 
            return trainerMailingAddress;
        }
        
        public void SetTrainerEmail(string trainerEmail){
            this.trainerEmail = trainerEmail;
        }
        public string GetTrainerEmail(){
            return trainerEmail;
        }

        static public void SetCount(int trainerCount)
        {
            Trainer.count = count;
        }

        static public void IncCount(){
            count ++;
        }

        static public void DecCount(){
            count --;
        }
        static public int GetCount(){
            return Trainer.count; 
        }

        public string ToFile()
        {
            return $"{trainerID}#{trainerName}#{trainerMailingAddress}#{trainerEmail}#{isDeleted}";
        }
        

        public void SetIsDeleted(){
            isDeleted = !isDeleted;
        }

        public bool GetIsDeleted(){
            return isDeleted;
        }

   }
}