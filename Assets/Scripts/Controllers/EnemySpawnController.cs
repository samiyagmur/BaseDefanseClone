using AIBrain;
using Enums;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Controllers
{
    public class EnemySpawnController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private List<GameObject> enemies = new List<GameObject>();

       // [SerializeField] private List<Transform> targetList = new List<Transform>();


        #endregion

        #region Public Variables

       // public Transform Player;

        public int NumberOfEnemiesToSpawn = 50;

        public float SpawnDelay = 2;

       // public GameObject EnemyPrefab;

       // public Transform SpawnPos;

        #endregion

        #region Private Variables

        private EnemyType enemyType;

        private List<EnemyBrain> enemyScripts = new List<EnemyBrain>();

        private NavMeshTriangulation triangulation;

        private GameObject _enemyAI;

        private EnemyBrain _enemyBrain;

        #endregion
        #endregion

        private void Awake()
        {
            InitEnemyPool();
            StartCoroutine(SpawnEnemies());

        }
        private void InitEnemyPool()
        {
            for (int i = 0; i < enemies.Count; i++)
                ObjectPoolManager.Instance.AddObjectPool(() => Instantiate(enemies[i]), TurnOnEnemyAI, TurnOffEnemyAI, ((EnemyType)i).ToString(), 50, true);
        }

        private void Start()
        {
           // triangulation = NavMesh.CalculateTriangulation();
        }
        private void TurnOnEnemyAI(GameObject enemy)
        {
            enemy.SetActive(true);
        }

        private void TurnOffEnemyAI(GameObject enemy)
        {
            enemy.SetActive(false);

        }

        private void ReleaseEnemyObject(GameObject go, Type t)
        {
            ObjectPoolManager.Instance.ReturnObject(go, t.ToString());
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
                randomType = (int)EnemyType.Red;
            }
            else if (15< randomPercentage && randomPercentage <=50)
            {
                randomType = (int)EnemyType.Yellow;
            }
            else
            {
                randomType = (int)EnemyType.Orange;

            }
           
             ObjectPoolManager.Instance.GetObject<GameObject>(((EnemyType)randomType).ToString());
         

        }
    }
}