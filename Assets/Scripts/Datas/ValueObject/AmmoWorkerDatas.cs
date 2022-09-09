using Abstraction;
using Datas.ValueObject;

namespace Datas.UnityObject
{
    internal class ammoWorkerDatas : AIWorkersDatas
    {
        public ammoWorkerDatas(int cost, int payedAmount, int boughtWorkerAmount, int workerTotalLevel, int workerCurrentLevel) : base(cost, payedAmount, boughtWorkerAmount, workerTotalLevel, workerCurrentLevel)
        {
        }
    }
}

