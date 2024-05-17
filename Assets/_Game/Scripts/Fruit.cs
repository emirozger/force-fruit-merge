using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private static int staticID = 0;
    [SerializeField] private TMP_Text[] numberTexts;

    [HideInInspector] public int fruitID;
    [HideInInspector] public Color fruitColor;
    [HideInInspector] public int fruitNumber;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool isMainFruit;
    
    private MeshRenderer meshRenderer; 

    private void Awake()
    {
        fruitID = staticID++;
        meshRenderer=GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        numberTexts = this.GetComponentsInChildren<TMP_Text>();
    }

    public void SetColor(Color color)
    {
        fruitColor = color;
        meshRenderer.material.color = color;
    }
    public void SetNumber(int number)
    {
        fruitNumber = number;
        foreach (var text in numberTexts)
        {
            text.text = number.ToString();
        }
    }
}