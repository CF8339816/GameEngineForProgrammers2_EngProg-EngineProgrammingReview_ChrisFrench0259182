using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#region coder & project
/// <summary>
/// NSCC GAME2025 / 4086 / Game Programming III(B)/ Doucette,Matthew
/// Unity: Game Manager & Persistence
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations:
/// 
/// </summary>
#endregion
public class Scenemanager : MonoBehaviour
{
    public static Scenemanager Instance { get; private set; }
    private GameManager gameManager;
    private GameExitManager gameExitManager;
    private InterfaceManager interfaceManager;
    private const int MAIN_INDEX = 0;
    private const int MENU_INDEX = 1;
    private const int LEVEL_1_INDEX = 2;
    private const int LEVEL_2_INDEX = 3;
    private const int LEVEL_3_INDEX = 4;
    private const int SHOP_INDEX = 5;
    private const int PLAYER_INDEX = 6;
    private const int HUD_INDEX = 7;
    private const int ENVIRONMENT_INDEX = 8;
    private int RETURNLEVEL_INDEX;
    public int ReturnIndex;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // keeps script accessable accross scene changes
       
        SceneManager.LoadScene(MENU_INDEX, LoadSceneMode.Additive);
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        interfaceManager = FindFirstObjectByType<InterfaceManager>();// looks for interface manager in scene
        if (interfaceManager != null)
        {
            interfaceManager.AutoHookupButtons();
        }
    }
    public void LoadLevelByIndex(int index)
    {
      if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        { 
            SceneManager.LoadScene(index);
        }      
    }
    public void onLevel1()
    {
        SceneManager.LoadScene(LEVEL_1_INDEX, LoadSceneMode.Single);
        AddMePls();
    }
    public void onLevel2()
    {
        SceneManager.LoadScene(LEVEL_2_INDEX, LoadSceneMode.Single);
        AddMePls();
    }
    public void onLevel3()
    {
        SceneManager.LoadScene(LEVEL_3_INDEX, LoadSceneMode.Single);
        AddMePls();
    }
    public void onMenu()
    {
        SceneManager.LoadScene(MAIN_INDEX, LoadSceneMode.Single);
        SceneManager.LoadScene(MENU_INDEX, LoadSceneMode.Additive);
    }
    public void onShop()
    {
        ReturnIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(SHOP_INDEX, LoadSceneMode.Single);
        AddMePls(); 
    }
    public void onExitShop()
    {
        RETURNLEVEL_INDEX = ReturnIndex;
        SceneManager.LoadScene(RETURNLEVEL_INDEX, LoadSceneMode.Single);
        AddMePls();
    }
    public void onStart()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onResetStats();
        }
        onLevel1();      
    }
    public void onLoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        LoadLevelByIndex(nextIndex);
        AddMePls();
    }

    public void AddMePls()
    {
        SceneManager.LoadScene(MENU_INDEX, LoadSceneMode.Additive);
        SceneManager.LoadScene(HUD_INDEX, LoadSceneMode.Additive);
        SceneManager.LoadScene(PLAYER_INDEX, LoadSceneMode.Additive);
        SceneManager.LoadScene(ENVIRONMENT_INDEX, LoadSceneMode.Additive);
    }
}


