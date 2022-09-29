using Enums;
using UnityEngine;
using DG.Tweening;
using Managers;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

namespace Controllers
{
 
    public class AmmoWorkerStackController
    {
        private PlayerAmmaStackStatus _playerAmmaStackStatus;

        private float yPos=-0.5f;//passed false
        private float zPos;
        private Sequence ammoSeq = DOTween.Sequence();
        private List<GameObject> ammoStackObjectList = new List<GameObject>();

        public  void AddStack(Transform startPoint,Transform ammoWorker,GameObject bullets)
        {
            
            bullets.transform.position = startPoint.position;
            bullets.transform.SetParent(ammoWorker);


                ammoSeq.Append(bullets.transform.DOScale(new Vector3(1, 1, 1), 0.8f));


                ammoSeq.Join(bullets.transform.DOLocalMove(new Vector3(Random.Range(0, 2), bullets.transform.localPosition.y +

                       Random.Range(2, 3), bullets.transform.localPosition.z- Random.Range(3,4)), 0.4f).OnComplete(()

                       => bullets.transform.DOLocalMove(new Vector3(0, ammoWorker.localPosition.y+yPos+1.5f, -ammoWorker.localScale.z - zPos), 0.4f)));


                ammoSeq.Join(bullets.transform.DOLocalRotate(new Vector3(Random.Range(-179, 179),Random.Range(-179, 179), Random.Range(-90, 90)), 0.3f).

                    OnComplete(()=> bullets.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.4f)));


            //


    
            ammoSeq.Play();

            yPos += 0.5f;

            if (yPos >= 5)
            { 
           
                zPos += 0.5f;
                yPos = 0;
            }
         
            ammoStackObjectList.Add(bullets);

        }
        public void RemoveStack()
        {   
            
        }

      
        public PlayerAmmaStackStatus GetStackStatus()
        {   
            return _playerAmmaStackStatus;
        }

        public List<GameObject> SendAmmoStack()
        {
            return ammoStackObjectList;
        }
    }
}