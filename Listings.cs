namespace mis_221_pa_5_fmgill1
{
    public class Listings
    {
        private int listingID;
        private string trainerName;
        private string workoutType;
        private DateOnly sessionDate;
        private TimeOnly sessionTime;
        private double sessionCost;
        private bool sessionIsAvailable;
        private bool isDeleted;
        static private int count;
        private int year;
        private int month;
        public Listings()
        {

        }
        public Listings(int listingID, string trainerName, string workoutType, DateOnly sessionDate, TimeOnly sessionTime, double sessionCost, bool sessionIsAvailable, bool isDeleted, int year, int month)
        {
            this.listingID = listingID;
            this.trainerName = trainerName;
            this.workoutType = workoutType;
            this.sessionDate = sessionDate;
            this.sessionTime = sessionTime;
            this.sessionCost = sessionCost; 
            this.sessionIsAvailable = sessionIsAvailable;
            this.isDeleted = isDeleted;
            this.year = year;
            this.month = month;
        }
        public void SetYear(DateOnly sessionDate)
        {
            this.year = sessionDate.Year;
        }
        public int GetYear()
        {
            return year;
        }
        public void SetMonth(DateOnly sessionDate)
        {
            this.month = sessionDate.Month;
        }
        public int GetMonth()
        {
            return month;
        }
        public void SetTrainerName(string trainerName)
        {
            this.trainerName= trainerName;
        }
        public void SetListingID()
        {
            this.listingID = count + 1;
        }
        public void SetWorkoutType(string workoutType)
        {
            this.workoutType = workoutType;
        }
        public string GetWorkoutType()
        {
            return workoutType;
        }

        public void SetSessionDate(DateOnly sessionDate)
        {
            this.sessionDate = sessionDate;
        }
        public void SetSessionTime(TimeOnly sessionTime)
        {
            this.sessionTime = sessionTime;
        }

        public void SetSessionCost(double sessionCost)
        {
            this.sessionCost = sessionCost;
        }
        public void SetSessionIsAvailable()
        {
            sessionIsAvailable = !sessionIsAvailable;
        }
        static public void SetCount(int trainerCount)
        {
            Listings.count = count;
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
            return Listings.count; 
        }
        public string GetTrainerName()
        {
            return trainerName;
        }
        public int GetListingID()
        {
            return listingID;
        }
        public DateOnly GetSessionDate()
        {
            return sessionDate;
        }
        public TimeOnly GetSessionTime()
        {
            return sessionTime;
        }
        public double GetSessionCost()
        {
            return sessionCost;
        }
        public bool GetSessionIsAvailabile()
        {
            return sessionIsAvailable;
        }
        public string ToFile()
        {
            return $"{listingID}#{trainerName}#{workoutType}#{sessionDate}#{sessionTime}#{sessionCost}#{sessionIsAvailable}#{isDeleted}#{year}#{month}";
        }
        public string ToString()
        {
            return $"Listing ID: {listingID}    {trainerName}  Workout Type: {workoutType}   {sessionDate}    {sessionTime}     ${sessionCost}    Available? {sessionIsAvailable}    Deleted? {isDeleted}";
        }
        public void SetIsDeleted()
        {
            isDeleted = !isDeleted;
        }
        public bool GetIsDeleted()
        {
            return isDeleted;
        }
    }
}
