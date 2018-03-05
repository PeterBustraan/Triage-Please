

namespace People
{
    class patientFactory
    {
        public void paiteintFactory()
        {
            //This will house some tools for thread safe related functions later
        }

        public Patient makePaitent()
        {
            return new Patient();
        }

        public Patient makePaitent(int TimeToLive, int tableTime)
        {
            return new Patient(TimeToLive, tableTime);
        }
    }
}
