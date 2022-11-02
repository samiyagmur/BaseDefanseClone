using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Interfaces;
using Signals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers.AIManagers
{
    public class EnemySpawnController : MonoBehaviour, IGetPoolObject
    {
        #region Self Variables

        #region Serialized Variables

        private List<GameObject> enemyList = new List<GameObject>();

        [SerializeField]
        private List<Transform> randomTargetTransform;

        [SerializeField]
        private ThrowEventController throwEventController;

        [SerializeField]
        private Transform spawnTransform;

        [SerializeField]
        private EnemySpawnData enemySpawnData;

        [SerializeField]
        private Transform bossSpawnPos;

        [SerializeField]
        private GameObject spriteTarget;

        #endregion Serialized Variables

        #region Private Variables

        private const string _dataPath = "Data/CD_AIData";

        private bool _isSpawning;

        #endregion Private Variables

        #endregion Self Variables

        private void Awake()
        {
            enemySpawnData = GetData();
        }

        private void Start()
        {
            SpawnBoss();
            StartCoroutine(SpawnEnemies());
        }

        private EnemySpawnData GetData() => Resources.Load<CD_AIData>(_dataPath).EnemyAIData.enemySpawnData;

        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            AISignals.Instance.getSpawnTransform += SetSpawnTransform;
            AISignals.Instance.getRandomTransform += SetRandomTransform;
            AISignals.Instance.onReleaseObjectUpdate += OnReleasedObjectCount;
        }

        private void UnsubscribeEvents()
        {
            AISignals.Instance.getSpawnTransform -= SetSpawnTransform;
            AISignals.Instance.getRandomTransform -= SetRandomTransform;
            AISignals.Instance.onReleaseObjectUpdate -= OnReleasedObjectCount;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subscription

        private Transform SetSpawnTransform()
        {
            return spawnTransform;
        }

        private Transform SetRandomTransform()
        {
            return randomTargetTransform[Random.Range(0, randomTargetTransform.Count)];
        }

        private IEnumerator SpawnEnemies()
        {
            WaitForSeconds wait = new WaitForSeconds(enemySpawnData.SpawnDelay);

            int spawnedEnemies = 0;

            _isSpawning = true;

            while (spawnedEnemies < enemySpawnData.NumberOfEnemiesToSpawn)
            {
                DoSpawnEnemy();
                spawnedEnemies++;
                yield return wait;
            }

            _isSpawning = false;
        }

        private void OnReleasedObjectCount(GameObject releasedObj)
        {
            enemyList.Remove(releasedObj);
            CheckEnemyCount();
        }

        private void CheckEnemyCount()
        {
            if (enemyList.Count > enemySpawnData.NumberOfEnemiesToSpawn / 2) return;
            if (_isSpawning) return;
            // StartCoroutine(SpawnEnemies());
        }

        private void DoSpawnEnemy()
        {
            int randomType = Random.Range(0, System.Enum.GetNames(typeof(EnemyType)).Length);
            int randomPercentage = Random.Range(0, enemySpawnData.RandomMaxRange);

            if (randomType == (int)EnemyType.Red)
            {
                if (randomPercentage < enemySpawnData.MinPossibilityToSpawnEnemy)
                {
                    randomType = (int)EnemyType.Orange;
                }
            }

            var poolType = (PoolType)System.Enum.Parse(typeof(PoolType), ((EnemyType)randomType).ToString());

            var enemyObj = GetObject(poolType);

            enemyList.Add(enemyObj);
        }

        public GameObject GetObject(PoolType poolName)
        {
            return PoolSignals.Instance.onGetObjectFromPool(poolName);
        }

        private void SpawnBoss()
        {
            var bossObj = GetObject(PoolType.BossEnemy);
            bossObj.GetComponentInChildren<ThrowEventController>().SpriteTarget = spriteTarget;
            bossObj.transform.position = bossSpawnPos.position;
        }
    }
}