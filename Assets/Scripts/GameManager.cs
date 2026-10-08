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
    private Scenemanager sceneManager;
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
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // maintains data              
        serviceHub = Object.FindFirstObjectByType<ServiceHub>();
        sceneManager = Object.FindFirstObjectByType<Scenemanager>();
        gameExitManager = Object.FindFirstObjectByType<GameExitManager>();



        onResetStats();
    }
    private void Update()
    {
        FindUIElementsInNewScene(); 
        UpdateBagSpace();
        UpdateHealthBar();
        UpdateCurrentLevelOutput();
    }
    private void FindUIElementsInNewScene()// finds the ui entries after destroyed to re-valuate them
    {        
        if (textHealthText == null) textHealthText = GameObject.Find("HealthText")?.GetComponent<TextMeshProUGUI>();
        if (textInventoryAvaillabilityText == null) textInventoryAvaillabilityText = GameObject.Find("InventoryAvaillabilityText")?.GetComponent<TextMeshProUGUI>();
        if (HealthBar == null) HealthBar = GameObject.Find("HealthBar")?.GetComponent<Slider>();
        if (InventoryCapacity == null) InventoryCapacity = GameObject.Find("InventoryCapacity")?.GetComponent<Slider>();
        if (textCurrentLevel == null) textCurrentLevel = GameObject.Find("CurrentLevel")?.GetComponent<TextMeshProUGUI>();

    }
    public void UpdateCurrentLevelOutput()
    {
        if (textCurrentLevel != null)
        {
            string CurrentScene = SceneManager.GetActiveScene().name;
            textCurrentLevel.text = "Current Level Loaded: " +CurrentScene;
        }
    }
    public void UpdateBagSpace()
    {
        InventorySpaceAvailable = InventoryBarMax - InventoryslotsUsed;
        InventoryCapacity.value = InventoryslotsUsed;
        textInventoryAvaillabilityText.text = "Slots available" + InventorySpaceAvailable.ToString() + "\nInventory used: " + InventoryslotsUsed.ToString() + "/" + InventoryBarMax.ToString();
    }
    public void UpdateHealthBar()
    {
        healtBarOutput = currentHealthPercentage;
        HealthBar.value = healtBarOutput;
        textHealthText.text = "Health %: " + currentHealthPercentage.ToString() + "\nHealth: " + currentHealth.ToString()  +"/" + maxHealth.ToString();
    }
    public void onTakeDamage()
    {
        currentHealth -= DamageTaken;
        if (currentHealth <= 0)  {   currentHealth = 0;  }
        CalculateHealthPercentage();
    }
    public void onTakeHealing()
    {        
        currentHealth += healingDone;
       if (currentHealth >= maxHealth)  {  currentHealth = maxHealth;   }
        CalculateHealthPercentage(); 
    }
    public void CalculateHealthPercentage()
    {
        currentHealthPercentage = Mathf.RoundToInt((currentHealth / maxHealth) * 100);
    }
    public void onAddItem()
    {
        if (InventoryslotsUsed < InventoryBarMax) { InventoryslotsUsed++; }
    }
    public void onRemoveItem()
    {
        if (InventoryslotsUsed > 0) {   InventoryslotsUsed--;  }
    }
    public void onResetStats()
    {
        currentHealth = maxHealth;
        InventoryslotsUsed = 0;
        CalculateHealthPercentage();
        if (HealthBar != null) { HealthBar.value = currentHealthPercentage; }
        if (InventoryCapacity != null) { InventoryCapacity.value = InventoryslotsUsed; }
    }
}












