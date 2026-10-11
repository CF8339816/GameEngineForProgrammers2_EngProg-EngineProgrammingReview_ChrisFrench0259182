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

    [Header("text output")]
    public TextMeshProUGUI textCurrentLevel;
    public TextMeshProUGUI textHealthText;
    public TextMeshProUGUI textInventoryAvaillabilityText;
    [Header("sliders")]
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider InventoryCapacity;
    [Header("health")]
    public int HealthBarMax = 100;
    private int HealthBarMin = 0;
    [SerializeField] public int healingDone= 12 ;
    [SerializeField] public int DamageTaken= 21;
    private int currentHealthPercentage;
    private int healtBarOutput;
    public float currentHealth;
    [SerializeField] public float maxHealth;
    [Header("inventory")]
    [SerializeField] public int InventoryBarMax = 24;
    private int InventoryBarMin = 0;
    public int InventoryslotsUsed = 0;
    private int InventorySpaceAvailable;
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
        textHealthText = GameObject.Find("HealthText")?.GetComponent<TextMeshProUGUI>();
        textInventoryAvaillabilityText = GameObject.Find("InventoryAvaillabilityText")?.GetComponent<TextMeshProUGUI>();
        HealthBar = GameObject.Find("HealthBar")?.GetComponent<Slider>();
        InventoryCapacity = GameObject.Find("InventoryCapacity")?.GetComponent<Slider>();
        textCurrentLevel = GameObject.Find("CurrentLevel")?.GetComponent<TextMeshProUGUI>();

        menuManager.AutoHookupButtonsMenu();
        hudManager.AutoHookupButtonsHUD();
        shopManager.AutoHookupButtonsShop();
    }

}












