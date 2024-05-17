using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var fruit = other.gameObject.GetComponent<Fruit>();
        if(fruit == null) return;
        if(!fruit.isMainFruit && fruit.rb.velocity.magnitude<.1f)
        {
            GameManager.Instance.SetGameState(GameState.GameOver);
        }
        
    }
}
