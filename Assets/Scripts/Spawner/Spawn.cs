using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private Bonus _bonus;
    [SerializeField] private Vector2 _positionRange;
    [SerializeField] private float _spawnObstacleTime;
    [SerializeField] private float _spawnBonusTime;

    private void Start()
    {
        StartCoroutine(SpawnObject(_obstacle.gameObject, _spawnObstacleTime, true));
        StartCoroutine(SpawnObject(_bonus.gameObject, _spawnBonusTime, false));
    }

    private IEnumerator SpawnObject(GameObject gameObject, float spawnTime, bool isObstacele)
    {
        while (true)
        {

            if (isObstacele)
            {
                SpawnObstacle(gameObject);
            }
            else
            {
                SpawnBonus(gameObject);
            }

            yield return new WaitForSeconds(spawnTime);
        }

    }

    private void SpawnBonus(GameObject gameObject)
    {
        GameObject Object = Instantiate(gameObject);
        Object.transform.position = GetRandomPosition();
    }

    private void SpawnObstacle(GameObject gameObject)
    {
        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            GameObject Object = Instantiate(gameObject);
            Object.transform.position = GetRandomPosition();
        }
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(_positionRange.x, _positionRange.y), transform.position.y, transform.position.z);
    }

}
