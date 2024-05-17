using System;
using System.Data;
using UnityEngine;
using Random = UnityEngine.Random;


public class FruitCollision : MonoBehaviour
{
    private Fruit fruit;

    private void Awake()
    {
        fruit = GetComponent<Fruit>();
    }

    private void OnCollisionEnter(Collision other)
    {
        var otherFruit = other.gameObject.GetComponent<Fruit>();

        if (otherFruit == null) return;
        if (fruit.fruitID <= otherFruit.fruitID) return;
        if (fruit.fruitNumber != otherFruit.fruitNumber) return;

        var collisionPoint = other.contacts[0].point;

        if (otherFruit.fruitNumber < FruitSpawner.Instance.maxFruitNumber)
        {
            Fruit newFruit =
                FruitSpawner.Instance.SpawnFruit(fruit.fruitNumber * 2, collisionPoint + Vector3.up * 1.6f);
            float pushForce = 2.5f;
            newFruit.rb.AddForce(new Vector3(0, .3f, 1f) * pushForce, ForceMode.Impulse);

            var random = Random.Range(-20, 20);
            var randomDirection = Vector3.one * random;
            newFruit.rb.AddTorque(randomDirection);
        }

        var overlapColliders = Physics.OverlapSphere(collisionPoint, 2f);
        var explosionForce = 400f;
        var explosionRadius = 1.5f;

        foreach (var col in overlapColliders)
        {
            if (col.attachedRigidbody != null)
            {
                col.attachedRigidbody.AddExplosionForce(explosionForce, collisionPoint, explosionRadius);
            }
        }

        GameManager.Instance.PlayExplosionFX(collisionPoint, fruit.fruitColor);
        GameManager.Instance.SetScore(fruit.fruitNumber);
        FruitSpawner.Instance.DestroyFruit(fruit);
        FruitSpawner.Instance.DestroyFruit(otherFruit);
    }
}