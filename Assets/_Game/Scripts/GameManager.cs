using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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