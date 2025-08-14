using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    [SerializeField, Header("Вставьте 4 точки для спавна ''яиц''"), Tooltip("Левая нижняя позиция")]
    private Transform leftDTransfrom;
    [SerializeField, Tooltip("Левая верхняя позиция")]
    private Transform leftUTransfrom;
    [SerializeField, Tooltip("Правая нижняя позиция")]
    private Transform rightDTransfrom;
    [SerializeField, Tooltip("Правая верхняя позиция")]
    private Transform rughtUTransfrom;

    private Transform thisTransform;

    [SerializeField, Header("Вставьте префаб ''яйца''")]
    private GameObject prefabEgg;

    [SerializeField, Header("Интервал спавна"), Range(1, 5)]
    private float maxSpawnInterval = 3.0f;
    [SerializeField, Range(1, 5)]
    private float minSpawnInterval = 1.0f;

    [SerializeField, Header("Время ожидания начала игры"), Range(1f, 5f)]
    private float beginTime = 2f;

    //Позиции с трансформов
    private Vector2 leftDPos;
    private Vector2 leftUPos;
    private Vector2 rightUPos;
    private Vector2 rightDPos;

    private List<Vector2> allPos = new List<Vector2>();

    private void OnEnable()
    {
        StartCoroutine(SpawnerEgg());
    }

    private void Awake()
    {
        InitPos();
    }

    private void SpawnEgg(Vector2 currentPos)
    {
        Instantiate(prefabEgg, currentPos, Quaternion.identity, thisTransform);
    }

    private Vector2 RandomPosition(List<Vector2> cuurentListPos)
    {
        Vector2 currentPos = cuurentListPos[Random.Range(0, cuurentListPos.Count)];
        return currentPos;
    }

    IEnumerator SpawnerEgg()
    {
        if (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(beginTime);
            while (true)
            {
                SpawnEgg(RandomPosition(allPos));
                yield return new WaitForSeconds(Random.Range(minSpawnInterval, maxSpawnInterval));
            }
        }
    }

    private void InitPos()
    {
        thisTransform = gameObject.transform;
        leftDPos = leftDTransfrom.position;
        allPos.Add(leftDPos);
        leftUPos = leftUTransfrom.position;
        allPos.Add(leftUPos);
        rightUPos = rughtUTransfrom.position;
        allPos.Add(rightUPos);
        rightDPos = rightDTransfrom.position;
        allPos.Add(rightDPos);
    }


    private void OnValidate()
    {
        if (maxSpawnInterval < minSpawnInterval)
        {
            maxSpawnInterval = minSpawnInterval + 0.1f;
        }
    }
}
