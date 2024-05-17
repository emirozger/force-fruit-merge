using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public enum GameState
{
    Start,
    Playing,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameState gameState;
    private int score = 0;
    [SerializeField] private ParticleSystem explosionFX;
    [SerializeField] private TextMeshProUGUI scoreText;
    ParticleSystem.MainModule mainModule;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    private void Start()
    {
        mainModule = explosionFX.main;
        scoreText.text = score.ToString();
    }

    public void SetGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.Start:
                gameState = newState;
                Debug.Log("Game Started");
                break;
            case GameState.Playing:
                gameState = newState;
                Debug.Log("Game Playing Now");
                break;
            case GameState.GameOver:
                gameState = newState;
                Debug.Log("Game End");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
       
    }
    public void SetScore(int number)
    {
        score += (number *+ 2);
        scoreText.text = score.ToString();
        scoreText.transform.DOPunchScale(Vector3.one * 1.2f, .3f);

    }

    public void PlayExplosionFX(Vector3 position, Color color)
    {
        mainModule.startColor = new ParticleSystem.MinMaxGradient(color);
        explosionFX.transform.position = position;
        explosionFX.Play();
    }
}

public class ManagerEditor : Editor
{
    [MenuItem("Tools/Set GameManager")]
    public static void OpenManager()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        if (manager == null)
        {
            GameObject obj = new GameObject("GameManager");
            obj.AddComponent<GameManager>();
        }
    }

    [MenuItem("Tools/Set GameManager Icon")]
    public static void SetIcon()
    {
        var monoImporter = AssetImporter.GetAtPath("Assets/_Game/Scripts/GameManager.cs") as MonoImporter;
        var icon = (Texture2D)EditorGUIUtility.Load("GameManager Icon");

        monoImporter.SetIcon(icon);
        monoImporter.SaveAndReimport();
    }
}