using System.Collections;
using System.Collections.Generic;
using TMPro;
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
public class MenuManager : MonoBehaviour
{

    public static MenuManager Instance { get; private set; }
    private GameManager gameManager;
    private GameExitManager gameExitManager;
    private ServiceHub serviceHub;
    //private SceneManager sceneManager;
    private CustomSceneManager customSceneManager;
    private HudManager hudManager;
    // private MenuManager menuManager;
    private ShopManager shopManager;


     private int MAIN_INDEX = 0;
     private int MENU_INDEX = 1;
     private int LEVEL_1_INDEX = 2;
     private int LEVEL_2_INDEX = 3;
     private int LEVEL_3_INDEX = 4;
     private int SHOP_INDEX = 5;
     private int PLAYER_INDEX = 6;
     private int HUD_INDEX = 7;
     private int ENVIRONMENT_INDEX = 8;

    private int RETURNLEVEL_INDEX;
    public int ReturnIndex;

    [Header("text output")]
    public TextMeshProUGUI textCurrentLevel;

    [SerializeField] private Button Level1;
    [SerializeField] private Button level2;
    [SerializeField] private Button level3;
    [SerializeField] private Button Quit;
    [SerializeField] private Button Shop;
    [SerializeField] private Button Menu;
    [SerializeField] private Button StartGame;
    [SerializeField] private Button NextLevel;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // keeps script accessable accross scene changes

    }

    public void UpdateCurrentLevelOutput()
    {
        if (textCurrentLevel != null)
        {
            string CurrentScene = SceneManager.GetActiveScene().name;
            textCurrentLevel.text = "Current Level Loaded: " + CurrentScene;
        }
    }
    public void AutoHookupButtonsMenu()

    {

        if (MenuManager.Instance == null)
        {
            Debug.LogError("InterfaceManager: Scenemanager.Instance is null! Cannot hook up buttons.");
            return;
        }

        MenuManager menuManager = MenuManager.Instance;// reinitalize the scene manager


        if (StartGame != null)
        {
            StartGame.onClick.RemoveAllListeners();
            StartGame.onClick.AddListener(customSceneManager.onStart);
            Debug.Log("🎯 Found and linked: StartGame");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: StartGame");
        }
        if (NextLevel != null)
        {
            NextLevel.onClick.RemoveAllListeners();
            NextLevel.onClick.AddListener(customSceneManager.onLoadNextLevel);
            Debug.Log("🎯 Found and linked: nextLevel");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: nextLevel");
        }
        if (Menu != null)
        {
            Menu.onClick.RemoveAllListeners();
            Menu.onClick.AddListener(customSceneManager.onMenu);
            Debug.Log("🎯 Found and linked: Menu");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Menu");
        }
        if (Level1 != null)
        {
            Level1.onClick.RemoveAllListeners();
            Level1.onClick.AddListener(customSceneManager.onLevel1);
            Debug.Log("🎯 Found and linked: Level1");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Level1");
        }
        if (level2 != null)
        {
            level2.onClick.RemoveAllListeners();
            level2.onClick.AddListener(customSceneManager.onLevel2);
            Debug.Log("🎯 Found and linked: level2");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: level2");
        }
        if (level3 != null)
        {
            level3.onClick.RemoveAllListeners();
            level3.onClick.AddListener(customSceneManager.onLevel3);
            Debug.Log("🎯 Found and linked: level3");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: level3");
        }
        if (Shop != null)
        {
            Shop.onClick.RemoveAllListeners();
            Shop.onClick.AddListener(customSceneManager.onShop);
            Debug.Log("🎯 Found and linked: Shop");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Shop");
        }
 
        if (Quit != null)
        {
            Quit.onClick.RemoveAllListeners();
            if (GameExitManager.Instance != null)
            {
                Quit.onClick.AddListener(GameExitManager.Instance.Ongameexit);
            }
            Debug.Log("🎯 Found and linked: Quit");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Quit");
        }



        if (GameManager.Instance != null)
        {
            GameManager.Instance.ConnectUI();
        }
        if (GameExitManager.Instance != null)
        {
            GameExitManager.Instance.ConnectExitUI();
        }
    }
}

    

