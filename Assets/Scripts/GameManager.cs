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
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
   

    private GameExitManager gameExitManager;
    private ServiceHub serviceHub;
    
    private CustomSceneManager customSceneManager;
    private HudManager hudManager;
    private MenuManager menuManager;
    private ShopManager shopManager;

    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            // return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // maintains data
        Debug.Log("🎯 Found and executed servicehub");
        serviceHub = Object.FindFirstObjectByType<ServiceHub>();
        Debug.Log("🎯 Found and executed scenemanager");
        customSceneManager = Object.FindFirstObjectByType<CustomSceneManager>();
        //Debug.Log("🎯 Found and executed game  manager");
        //gameManager = Object.FindFirstObjectByType<GameManager>();
        Debug.Log("🎯 Found and executed interface manager");
        menuManager = Object.FindFirstObjectByType<MenuManager>();
        Debug.Log("🎯 Found and executed game exit maager");
        Debug.Log("🎯 Found and executed interface manager");
        hudManager = Object.FindFirstObjectByType<HudManager>();
        Debug.Log("🎯 Found and executed game exit maager");
        Debug.Log("🎯 Found and executed interface manager");
        shopManager = Object.FindFirstObjectByType<ShopManager>();
        Debug.Log("🎯 Found and executed game exit maager");

        gameExitManager = Object.FindFirstObjectByType<GameExitManager>();
    }
    private void Start()
    {
        ConnectUI();
        hudManager.onResetStats();
    }
    private void Update()
        {
        hudManager.UpdateBagSpace();
    
        hudManager.CalculateHealthPercentage();
        hudManager.UpdateHealthBar();
        menuManager.UpdateCurrentLevelOutput();
    }
    public void ConnectUI()// finds the ui entries after destroyed to re-valuate them
    {
        menuManager.AutoHookupButtonsMenu();
        hudManager.AutoHookupButtonsHUD();
        shopManager.AutoHookupButtonsShop(); 
        
        hudManager.textHealthText = GameObject.Find("HealthText")?.GetComponent<TextMeshProUGUI>();
        hudManager.textInventoryAvaillabilityText = GameObject.Find("InventoryAvaillabilityText")?.GetComponent<TextMeshProUGUI>();
        hudManager.HealthBar = GameObject.Find("HealthBar")?.GetComponent<Slider>();
        hudManager.InventoryCapacity = GameObject.Find("InventoryCapacity")?.GetComponent<Slider>();
        menuManager.textCurrentLevel = GameObject.Find("CurrentLevel")?.GetComponent<TextMeshProUGUI>();

     
    }

}












