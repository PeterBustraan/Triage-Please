using Complications;

namespace People
{
    public class Patient : Human
    {
        public int TimeToLive { get; private set; }
        public int TimeForProcedure { get; private set; }
        public int LastPossibleMoment { get; private set; }
        public new bool isHealthy = false; //this will need upgrades when hospital staff is expanded
        public int IntakeTime { get; set; } // time entering waiting room
        public int TimeEnteringER { get; set; } // time actually on er table

        public Patient ()
        {
            getAilment();
        }

        public Patient(int ttl, int tableTime)
        {
            TimeToLive = ttl;
            TimeForProcedure = tableTime;
            LastPossibleMoment = -1;
            IntakeTime = -1;
        }

        //Get Diseases

        private void getAilment()
        {
            var SingleTon = DiseaseCoder.SingleTon;
            int[] details = SingleTon.getAilment();

            //TODO Fix before using SQL DB
            TimeToLive = details[0];
            TimeForProcedure = details[1];
            LastPossibleMoment = -1;
            IntakeTime = -1;
        }


        //Death Timer

        public void setFinalOperationTime(int currentTime)
        {
            LastPossibleMoment = currentTime + TimeToLive;
        }
    }
}
