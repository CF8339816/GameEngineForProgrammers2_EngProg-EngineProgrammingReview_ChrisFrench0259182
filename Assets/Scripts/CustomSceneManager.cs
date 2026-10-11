using System.Collections;
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
public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager Instance { get; private set; }

    private GameManager gameManager;
    private GameExitManager gameExitManager;
    private ServiceHub serviceHub;
    private CustomSceneManager customSceneManager;
    private HudManager hudManager;
    private MenuManager menuManager;
    private ShopManager shopManager;
    private InterfaceManager interfaceManager;

    // 2. Removed 'const' because compile-time constants cannot be serialized in the Inspector
    [SerializeField] private int MAIN_INDEX = 0;
    [SerializeField] private int MENU_INDEX = 1;
    [SerializeField] private int LEVEL_1_INDEX = 2;
    [SerializeField] private int LEVEL_2_INDEX = 3;
    [SerializeField] private int LEVEL_3_INDEX = 4;
    [SerializeField] private int SHOP_INDEX = 5;
    [SerializeField] private int PLAYER_INDEX = 6;
    [SerializeField] private int HUD_INDEX = 7;
    [SerializeField] private int ENVIRONMENT_INDEX = 8;

    private int RETURNLEVEL_INDEX;
    public int ReturnIndex;

    void Awake()
    {
        Debug.Log("CustomSceneManager awakening...");
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // keeps script accessible across scene changes
        SceneManager.LoadScene(ENVIRONMENT_INDEX, LoadSceneMode.Additive);
        Debug.Log(" Menu  awakening...");
        SceneManager.LoadScene(MENU_INDEX, LoadSceneMode.Additive);
        Debug.Log(" scene HUD  awakening...");
        SceneManager.LoadScene(HUD_INDEX, LoadSceneMode.Additive);
        SceneManager.LoadScene(PLAYER_INDEX, LoadSceneMode.Additive);
        Debug.Log("Scenemanager  done.");
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
        if (scene.buildIndex == MAIN_INDEX)
        {
            interfaceManager = FindFirstObjectByType<InterfaceManager>();// finds interface manager
            if (interfaceManager != null)
            {
                interfaceManager.AutoHookupButtons();
            }
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
        StartCoroutine(AddSceneLayers(LEVEL_1_INDEX));
    }
    public void onLevel2()
    {
        StartCoroutine(AddSceneLayers(LEVEL_2_INDEX));
    }
    public void onLevel3()
    {
        StartCoroutine(AddSceneLayers(LEVEL_3_INDEX));
    }
    public void onMenu()
    {
        SceneManager.LoadScene(MAIN_INDEX, LoadSceneMode.Single);
        SceneManager.LoadScene(MENU_INDEX, LoadSceneMode.Additive);
    }
    public void onShop()
    {
        ReturnIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(AddSceneLayers(SHOP_INDEX));
    }
    public void onExitShop()
    {
        RETURNLEVEL_INDEX = ReturnIndex;
        StartCoroutine(AddSceneLayers(RETURNLEVEL_INDEX));
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
        if (nextIndex <= LEVEL_3_INDEX)
        {
            StartCoroutine(AddSceneLayers(nextIndex));
        }
        else
        {
            onMenu();
        }
    }
    public IEnumerator AddSceneLayers(int baseLevelIndex)
    {
        yield return SceneManager.LoadSceneAsync(baseLevelIndex, LoadSceneMode.Single);// load active level and wait till complete        
        yield return SceneManager.LoadSceneAsync(MENU_INDEX, LoadSceneMode.Additive);// load scene layer fully then on to next
        yield return SceneManager.LoadSceneAsync(HUD_INDEX, LoadSceneMode.Additive);// load scene layer fully then on to next
        yield return SceneManager.LoadSceneAsync(PLAYER_INDEX, LoadSceneMode.Additive);// load scene layer fully then on to next
        yield return SceneManager.LoadSceneAsync(ENVIRONMENT_INDEX, LoadSceneMode.Additive);// load scene layer fully 
        Scene baseScene = SceneManager.GetSceneByBuildIndex(baseLevelIndex);// makse sure added objects  owned by base layer
        if (baseScene.IsValid())
        {
            SceneManager.SetActiveScene(baseScene);
        }
        yield return null;      
    }
}

    