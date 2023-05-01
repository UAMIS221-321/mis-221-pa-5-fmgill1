namespace mis_221_pa_5_fmgill1
{
    public class Booking
    {
        private int sessionID;
        private string customerName;
        private string customerEmail;
        private DateOnly trainingDate;
        private int trainerID;
        private string trainerName;
        private string sessionStatus;
        static private int count;

        public Booking()
        {
            
        }

        public Booking(int sessionID, string customerName, string customerEmail, DateOnly trainingDate,int trainerID, string trainerName, string sessionStatus)
        {
            this.sessionID = sessionID;
            this.customerName = customerName; 
            this.customerEmail = customerEmail;
            this.trainingDate = trainingDate;
            this.trainerID = trainerID;
            this.trainerName = trainerName;
            this.sessionStatus = sessionStatus;
        }
        public void SetCustomerName(string customerName)
        {
            this.customerName = customerName;
        }
        public void SetCustomerEmail(string customerEmail)
        {
            this.customerEmail = customerEmail;
        }
        public void SetTrainingDate(DateOnly trainingDate)
        {
            this.trainingDate = trainingDate;
        }
        public void SetTrainerID(int trainerID)
        {
            this.trainerID = trainerID;
        }
        public void SetSessionID()
        {
            this.sessionID = count + 1;
        }
        public void SetTrainerName(string trainerName)
        {
            this.trainerName = trainerName;
        }
        public void SetSessionStatus(string sessionStatus)
        {
            this.sessionStatus = sessionStatus;
        }
        public string GetCustomerName()
        {
            return customerName;
        }
        public string GetCustomerEmail()
        {
            return customerEmail;
        }
        public DateOnly GetTrainingDate()
        {
            return trainingDate;
        }
        public int GetTrainerID()
        {
            return trainerID;
        }
        public int GetSessionID()
        {
            return sessionID;
        }
        public string GetTrainerName()
        {
            return trainerName;
        }
        public string GetSessionStatus()
        {
            return sessionStatus;
        }
        static public void SetCount(int trainerCount)
        {
            Booking.count = count;
        }

        static public void IncCount()
        {
            count++;
        }

        static public void DecCount()
        {
            count--;
        }
        static public int GetCount()
        {
            return Booking.count; 
        }
        
       
        public string ToFile()
        {
            return $"{sessionID}#{customerName}#{customerEmail}#{trainingDate}#{trainerID}#{trainerName}#{sessionStatus}";
        }
        public string ToString()
        {
            return $"Session ID:  {sessionID}   {customerName}    {customerEmail}     {trainingDate}   Trainer ID:  {trainerID}    {trainerName}   Session Status:   {sessionStatus}";
        }
    }
}

    
