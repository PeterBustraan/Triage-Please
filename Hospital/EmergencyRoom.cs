using System;
using System.Collections.Generic;
using PriorityQueue;
using People;

namespace Hospital
{
    class Hospital
    {
        static public int CurrentTime { get; set; }
    }

    class ERTable
    {
        // estimated time of completion
        public int ETC { get; set; }
        public Patient Patient { get; private set; }

        public ERTable(Patient p)
        {
            Patient = p;
            ETC = p.TimeForProcedure + Hospital.CurrentTime;
        }
    }

    class TriageUnit
    {
        private readonly int seed = 23;
        private readonly Random rand;
        private patientFactory patientFactory =  new patientFactory();

        public TriageUnit()
        {
            rand = new Random(seed);
        }

        public List<Patient> getNewPatients()
        {
            List<Patient> newPatients = new List<Patient>();
            int numPatients = rand.Next(12);
            for (int i = 0; i < numPatients; i++)
            {
                // two hours max, average about 1 hour
                int expiryTime = rand.Next(100);
                expiryTime += 20; // At least 20 minutes to get to ER

                // up to 35 minutes on ER table
                int operationTime = rand.Next(20);
                operationTime += 5; // no less than 10.

                newPatients.Add(patientFactory.makePaitent());
                //newPatients.Add(patientFactory.makePaitent(expiryTime, operationTime));
            }
            return newPatients;
        }
    }

    class EmergencyRoom
    {
        public const int NUM_TABLES = 10;
        public const int numMinutes = 12*60; // one shift
        private TriageUnit triage = new TriageUnit();
        private IQueue<Patient> waitingQueue;
        private List<ERTable> ERtables = new List<ERTable>();
        string lr = Environment.NewLine;

        public void processPatients(bool usePriority)
        {
            Hospital.CurrentTime = 0;
            List<Patient> patientList = new List<Patient>();
            List<Patient> fatalities = new List<Patient>();
            // TODO:  You'll need some counters and accumulators
            int totalPatients  = 0;
            int totalAdmited   = 0;
            int maxWaiting     = 0;
            int totalWaiting   = 0;
            int averageWaiting = 0;
            int averageStay    = 0;
            int totalStay      = 0;

            // this is for free
            if (usePriority)
                waitingQueue = new PriorityQueue<Patient>();
            else
                waitingQueue = new SimpleQueue<Patient>();

            while (Hospital.CurrentTime < numMinutes)
            {
                //Pack The Loby
                foreach (Patient current in triage.getNewPatients())
                {
                    current.setFinalOperationTime(Hospital.CurrentTime);
                    current.IntakeTime = Hospital.CurrentTime;
                    current.setFinalOperationTime(Hospital.CurrentTime);
                    waitingQueue.Add(current.TimeToLive, current);
                    totalPatients++;
                }

                //If Tables are emtpy then add people from the waiting list
                while (ERtables.Count < NUM_TABLES)
                {
                    if (waitingQueue.Count > 0)
                    {
                        Patient Current = waitingQueue.Remove();
                        Current.TimeEnteringER = Hospital.CurrentTime;
                        if (Hospital.CurrentTime <= Current.LastPossibleMoment)
                        {
                            ERtables.Add(new ERTable(Current));
                            totalStay += Current.TimeForProcedure;
                            totalAdmited++;
                        }
                        else
                            fatalities.Add(Current);
                    }
                    else
                        break;
                }

                //Process Existing Tables
                var index = 0;
                while (index < ERtables.Count)
                {
                    ERTable current = ERtables[index];
                        
                    Patient subject = current.Patient;
                    current.ETC = current.ETC-1; //Because all operations are smooth and painless
                                    //TODO dice rolls for operation progression and success
                    if (Hospital.CurrentTime > subject.LastPossibleMoment)
                    {
                        fatalities.Add(subject);
                        ERtables.RemoveAt(index);
                    }
                    else if (Hospital.CurrentTime == current.ETC)
                    {
                        patientList.Add(subject);
                        ERtables.RemoveAt(index); // I know but if I'm not taking them off the tables then they stay till death
                    }
                    else
                    {
                        index++;
                    }
                }

                //Counting the Highest WaitingRoom Population
                if (waitingQueue.Count > 0)
                {
                    if (waitingQueue.Count > maxWaiting)
                    {
                        maxWaiting = waitingQueue.Count;
                    }
                    totalWaiting += 1;
                }


                Hospital.CurrentTime++;
            }

            averageWaiting = maxWaiting / totalWaiting;

            averageStay = totalStay/totalAdmited;

            // print time, total patients, max waiting, average waiting, expired patients
            Console.WriteLine("time : {0}" + lr +
                              "total patients : {1}" + lr +
                              "max waiting : {2}" + lr +
                              "average waiting : {3}" + lr +
                              "expired patients: {4}" + lr +
                              "Average Stay: {5}" + lr,
                              Hospital.CurrentTime,totalPatients,maxWaiting, averageWaiting, fatalities.Count,averageStay);
        }
    }
}
