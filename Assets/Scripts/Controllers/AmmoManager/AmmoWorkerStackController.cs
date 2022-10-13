using Enums;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using Datas.ValueObject;
using Sirenix.OdinInspector;
using Controllers.Player;
using Data.UnityObject;
using Abstraction;

namespace Controllers
{
 
    public class AmmoWorkerStackController: AStack
    {
        #region Self Variables

        #region Serialized Variables

      //  [SerializeField] private Transform moneyTransformParent;

        [SerializeField] private StackingSystem stackingSystem;

        [ShowIf("stackingSystem", StackingSystem.Static)]
        [SerializeField] private StackAreaType stackAreaType;


        [ShowIf("stackingSystem", StackingSystem.Static)]
        [SerializeField] private GridData stackAreaGridData;

        [ShowIf("stackingSystem", StackingSystem.Dynamic)]
        [SerializeField] private StackerType stackerType;


        [ShowIf("stackingSystem", StackingSystem.Dynamic)]
        [SerializeField] private GridData stackerGridData;

        [SerializeField] private AmmoStackerController ammoStackerController;

        #endregion

        #region Private Variables

        [ShowInInspector] private List<Vector3> gridPositionsData = new List<Vector3>();

        private Vector3 _gridPositions;

        private GridData _gridData;

        #endregion

        #region Public Variables

        #endregion

        #endregion

        private void Awake()
        {
            GetData();
            SetGrid();
            SendGridDataToStacker();
        }

        private void GetData()
        {
            if (stackingSystem == StackingSystem.Dynamic)
            {
                stackerGridData = GetStackerGridData();
            }
            else
            {
                stackAreaGridData = GetAreaStackGridData();
            }
        }

        private GridData GetStackerGridData()
        {
            return Resources.Load<CD_Stack>("Data/CD_Stack").StackDatas[(int)stackingSystem].DynamicGridDatas[(int)stackerType];
        }

        private GridData GetAreaStackGridData()
        {
            return Resources.Load<CD_Stack>("Data/CD_Stack").StackDatas[(int)stackingSystem].StaticGridDatas[(int)stackAreaType];
        }


        public override void SetStackHolder(GameObject gameObject)
        {
            gameObject.transform.SetParent(transform);
        }


        public override void SetGrid()
        {
            if (stackingSystem == StackingSystem.Static)
            {
                _gridData = stackAreaGridData;
            }
            else
            {
                _gridData = stackerGridData;
            }
            var gridCount = _gridData.GridSize.x * _gridData.GridSize.y * _gridData.GridSize.z;
            for (int i = 0; i < gridCount; i++)
            {
                var modX = (int)(i % _gridData.GridSize.x);
                var divideZ = (int)(i / _gridData.GridSize.x);
                var modZ = (int)(divideZ % _gridData.GridSize.z);
                var divideXZ = (int)(i / (_gridData.GridSize.x * _gridData.GridSize.z));
                if (_gridData.isDynamic)
                {
                    _gridPositions = new Vector3(modX * _gridData.Offset.x,
                        modZ * _gridData.Offset.z, divideXZ * _gridData.Offset.y);
                }
                else
                {
                    _gridPositions = new Vector3(modX * _gridData.Offset.x, divideXZ * _gridData.Offset.y,
                        modZ * _gridData.Offset.z);

                }
                gridPositionsData.Add(_gridPositions);

            }
        }
        public override void SendGridDataToStacker()
        {
            ammoStackerController.GetStackPositions(gridPositionsData);
        }

    }
}
//private float yPos = -0.5f;//passed false
//private float zPos = -0.25f;
//private Sequence ammoSeq;

//[SerializeField]
//private List<GameObject> ammoStackObjectList = new List<GameObject>();

//public void AddStack(Transform startPoint, Transform ammoWorker, GameObject bullets)
//{

//    //ammoSeq = DOTween.Sequence();
//    //bullets.transform.position = startPoint.position;
//    //bullets.transform.SetParent(ammoWorker);

//    //ammoSeq.Append(bullets.transform.DOScale(new Vector3(1, 1, 1), 0.8f));


//    //ammoSeq.Join(bullets.transform.DOLocalMove(new Vector3(Random.Range(0, 2), bullets.transform.localPosition.y +

//    //Random.Range(4, 5), bullets.transform.localPosition.z - Random.Range(3, 4)), 0.4f).
//    //OnComplete(()
//    //=> {

//    //         bullets.transform.DOLocalMove(new Vector3(0, ammoWorker.localPosition.y + yPos + 1.5f, -ammoWorker.localScale.z - zPos), 0.4f);

//    //         yPos += 0.5f;
//    //   }));
//    //ammoSeq.Join(bullets.transform.DOLocalRotate(new Vector3(Random.Range(-179, 179),Random.Range(-179, 179), Random.Range(-90, 90)), 0.3f).

//    //        OnComplete(()=> bullets.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.4f)));

//    //ammoSeq.Play();

//    //if (yPos >= 5)
//    //{ 

//    //    zPos += 0.5f;
//    //    yPos = 0;
//    //}

//    //ammoStackObjectList.Add(bullets);

//}
//public List<GameObject> SendAmmoStack()
//{
//    return ammoStackObjectList;
//}
//public void SetClearWorkerStackList()
//{
//    zPos = -0.25f;

//    ammoStackObjectList.Clear();
//    ammoStackObjectList.TrimExcess();
//}