using Abstraction;
using Datas.ValueObject;

namespace Datas.UnityObject
{
    public class MoneyWorkerDatas : AIWorkersDatas
    {
        public MoneyWorkerDatas(int cost, int payedAmount, int boughtWorkerAmount, int workerTotalLevel, int workerCurrentLevel) : base(cost, payedAmount, boughtWorkerAmount, workerTotalLevel, workerCurrentLevel)
        {
        }
    }
}