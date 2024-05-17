using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    public static FruitSpawner Instance { get; private set; }

    Queue<Fruit> fruitPool = new Queue<Fruit>();
    [SerializeField] private int poolSize = 20;
    [SerializeField] private bool autoPoolGrow = true;

    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private Color[] fruitColors;

    private Vector3 spawnPos;
    private int maxPower = 12;
    [HideInInspector] public int maxFruitNumber;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        spawnPos = transform.position;
        maxFruitNumber = (int)Mathf.Pow(2, maxPower);
        
        InitializeFruitPool();
    }

    private void InitializeFruitPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            AddFruitToPool();
        }
    }

    private void AddFruitToPool()
    {
        var fruit = Instantiate(fruitPrefab, spawnPos, Quaternion.identity, transform).GetComponent<Fruit>();

        fruit.gameObject.SetActive(false);
        fruit.isMainFruit = false;
        fruitPool.Enqueue(fruit);
    }

    public Fruit SpawnFruit(int number, Vector3 position)
    {
        if (fruitPool.Count == 0)
        {
            if (autoPoolGrow)
            {
                poolSize++;
                AddFruitToPool();
            }
            else
            {
                Debug.LogWarning("Fruit pool is empty!");
                return null;
            }
        }

        var fruit = fruitPool.Dequeue();
        fruit.transform.position = position;
        fruit.SetColor(GetColor(number));
        fruit.SetNumber(number);
        fruit.gameObject.SetActive(true);

        return fruit;
    }

    public Fruit SpawnRandom()
    {
        return SpawnFruit(GenerateRandomNumber(), spawnPos);
    }

    public void DestroyFruit(Fruit fruit)
    {
        fruit.rb.velocity = Vector3.zero;
        fruit.rb.angularVelocity = Vector3.zero;
        fruit.transform.rotation = Quaternion.identity;
        fruit.isMainFruit = false;
        fruit.gameObject.SetActive(false);
        fruitPool.Enqueue(fruit);
    }

    public int GenerateRandomNumber()
    {
        return (int)Mathf.Pow(2, Random.Range(1, 6));
    }

    private Color GetColor(int number)
    {
        return fruitColors[(int)(Mathf.Log(number) / Mathf.Log(2)) - 1];
    }
}