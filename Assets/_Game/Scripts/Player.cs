using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float fruitMaxPosX;
    [Space] [SerializeField] private TouchSlider touchSlider;

    private Fruit mainFruit;
    private bool isPointerDown;
    private Vector3 fruitPos;
    private bool canMove;


    private void Start()
    {
        SpawnFruit();
        canMove = true;
        touchSlider.OnPointerDownAction += OnPointerDown;
        touchSlider.OnPointerDragAction += OnPointerDrag;
        touchSlider.OnPointerUpAction += OnPointerUp;
    }

    private void Update()
    {
        if (isPointerDown)
        {
            mainFruit.transform.position =
                Vector3.Lerp(mainFruit.transform.position, fruitPos, moveSpeed * Time.deltaTime);
        }
    }

    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            isPointerDown = false;
            canMove = false;
        }

        mainFruit.rb.AddForce(Vector3.forward * pushForce, ForceMode.Impulse);

        Invoke(nameof(SpawnNewFruit), .3f);
    }

    private void OnPointerDrag(float xMove)
    {
        if (isPointerDown)
        {
            fruitPos = mainFruit.transform.position;
            fruitPos.x = xMove * fruitMaxPosX;
            GameManager.Instance.SetGameState(GameState.Playing);
        }
    }

    private void OnPointerDown()
    {
        isPointerDown = true;
        GameManager.Instance.SetGameState(GameState.Start);
    }

    private void SpawnNewFruit()
    {
        mainFruit.isMainFruit = false;
        canMove = true;
        SpawnFruit();
    }

    private void SpawnFruit()
    {
        mainFruit = FruitSpawner.Instance.SpawnRandom();
        mainFruit.isMainFruit = true;
        fruitPos = mainFruit.transform.position;
    }

    private void OnDestroy()
    {
        touchSlider.OnPointerDownAction -= OnPointerDown;
        touchSlider.OnPointerDragAction -= OnPointerDrag;
        touchSlider.OnPointerUpAction -= OnPointerUp;
    }
}