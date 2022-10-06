using Enums;
using Interfaces;
using Signals;
using System.Collections;
using UnityEngine;


namespace Controllers
{
    public class EnemySpawnController : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Serialized Variables

        #endregion

        #region Public Variables

        public int NumberOfEnemiesToSpawn = 50;

        public float SpawnDelay = 2;

        #endregion

        #region Private Variables


        #endregion
        #endregion

        private void Awake()
        {
           
            StartCoroutine(SpawnEnemies());

        }
       
        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(SpawnDelay);

            int spawnedEnemies = 0;

            while (spawnedEnemies < NumberOfEnemiesToSpawn)
            {
                DoSpawnEnemy();

                spawnedEnemies++;
                yield return wait;
            }
        }

        private void DoSpawnEnemy()
        {
            int randomType;
            int randomPercentage = UnityEngine.Random.Range(0, 101);

            if (randomPercentage<=15)
            {
                randomType = (int)PoolType.Orange;
            }
            else if (15< randomPercentage && randomPercentage <=50)
            {
                randomType = (int)PoolType.Red;
            }
            else
            {
                randomType = (int)PoolType.Yellow;

            }

           var obj= GetObject(((PoolType)randomType));
        }

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool?.Invoke(poolName);
        }
    }
}