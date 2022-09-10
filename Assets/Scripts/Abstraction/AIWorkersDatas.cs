namespace Abstraction
{
    public  abstract class  AIWorkersDatas
    {
        public  int Cost;
        public  int PayedAmount;
        public  int BoughtWorkerAmount;
        public  int WorkerTotalLevel;
        public  int WorkerCurrentLevel;

        protected AIWorkersDatas(int cost, int payedAmount, int boughtWorkerAmount, int workerTotalLevel, int workerCurrentLevel)
        {
            Cost = cost;
            PayedAmount = payedAmount;
            BoughtWorkerAmount = boughtWorkerAmount;
            WorkerTotalLevel = workerTotalLevel;
            WorkerCurrentLevel = workerCurrentLevel;
        }
    }
}