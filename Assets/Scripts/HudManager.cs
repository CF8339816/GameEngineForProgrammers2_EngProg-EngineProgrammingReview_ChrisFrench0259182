using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using static UnityEditor.Timeline.Actions.MenuPriority;

#region coder & project
/// <summary>
/// NSCC GAME2025 / 4086 / Game Programming III(B)/ Doucette,Matthew
/// Unity: Game Manager & Persistence
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations:
/// 
/// </summary>
#endregion
public class HudManager : MonoBehaviour
{
    public static HudManager Instance { get; private set; }
    private GameManager gameManager;
    private GameExitManager gameExitManager;
    private ServiceHub serviceHub;
    // private SceneManager sceneManager;
    private CustomSceneManager customSceneManager;
    // private HudManager hudManager;
    private MenuManager menuManager;
    private ShopManager shopManager;


    [Header("text output")]
    //public TextMeshProUGUI textCurrentLevel;
    public TextMeshProUGUI textHealthText;
    public TextMeshProUGUI textInventoryAvaillabilityText;
    [Header("sliders")]
    [SerializeField] private Slider HealthBar;
    [SerializeField] private Slider InventoryCapacity;
    [Header("health")]
    public int HealthBarMax = 100;
    private int HealthBarMin = 0;
    [SerializeField] public int healingDone = 12;
    [SerializeField] public int DamageTaken = 21;
    private int currentHealthPercentage;
    private int healtBarOutput;
    public float currentHealth;
    [SerializeField] public float maxHealth;
    [Header("inventory")]
    [SerializeField] public int InventoryBarMax = 24;
    private int InventoryBarMin = 0;
    public int InventoryslotsUsed = 0;
    private int InventorySpaceAvailable;

    [SerializeField] private Button AddItem;
    [SerializeField] private Button Heal;
    [SerializeField] private Button takeDamage;
    [SerializeField] private Button removeItem;

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
  
    public void UpdateBagSpace()
    {
        if (InventoryCapacity == null) { return; }
        InventorySpaceAvailable = InventoryBarMax - InventoryslotsUsed;
        InventoryCapacity.value = InventoryslotsUsed;
        textInventoryAvaillabilityText.text = "Slots available" + InventorySpaceAvailable.ToString() + "\nInventory used: " + InventoryslotsUsed.ToString() + "/" + InventoryBarMax.ToString();
    }
    public void UpdateHealthBar()
    {
        if (HealthBar == null) { return; }
        healtBarOutput = currentHealthPercentage;
        HealthBar.value = healtBarOutput;
        textHealthText.text = "Health %: " + currentHealthPercentage.ToString() + "\nHealth: " + currentHealth.ToString() + "/" + maxHealth.ToString();
        Debug.Log($"🎯 health percentage = {currentHealthPercentage}");
        Debug.Log($"🎯 health  = {healtBarOutput}");
    }
    public void onTakeDamage()
    {
        currentHealth -= DamageTaken;
        if (currentHealth <= 0) { currentHealth = 0; }
        CalculateHealthPercentage();
        Debug.Log($"🎯 damagee = {DamageTaken}");
    }
    public void onTakeHealing()
    {
        currentHealth += healingDone;
        if (currentHealth >= maxHealth) { currentHealth = maxHealth; }
        CalculateHealthPercentage();
        Debug.Log($"🎯 Healing  = {healingDone}");
    }
    public void CalculateHealthPercentage()
    {
        currentHealthPercentage = Mathf.RoundToInt((currentHealth / maxHealth) * 100);
        Debug.Log($"🎯 health percentage = {currentHealthPercentage}");

    }
    public void onAddItem()
    {
        Debug.Log("🎯 Found and executed add");
        if (InventoryslotsUsed < InventoryBarMax) { InventoryslotsUsed++; }


        else
        {
            Debug.Log("❌ Could NOTaddiecakes");
        }
    }
    public void onRemoveItem()
    {
        if (InventoryslotsUsed > 0) { InventoryslotsUsed--; }
    }
    public void onResetStats()
    {
        currentHealth = maxHealth;
        InventoryslotsUsed = 0;
        CalculateHealthPercentage();
        if (HealthBar != null) { HealthBar.value = currentHealthPercentage; }
        if (InventoryCapacity != null) { InventoryCapacity.value = InventoryslotsUsed; }
    }


    public void AutoHookupButtonsHUD()
    {


        if (HudManager.Instance == null)
        {
            Debug.LogError("InterfaceManager: Scenemanager.Instance is null! Cannot hook up buttons.");
            return;
        }

       HudManager hudManager = HudManager.Instance;// reinitalize the scene manager


        if (takeDamage != null)
        {
            takeDamage.onClick.RemoveAllListeners();
            takeDamage.onClick.AddListener(onTakeDamage);
            Debug.Log("🎯 Found and linked: takeDamage");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: takeDamage");
        }
        if (Heal != null)
        {
             Heal.onClick.RemoveAllListeners();
            Heal.onClick.AddListener(onTakeHealing);
            Debug.Log("🎯 Found and linked: Heal");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Heal");
        }
        if (AddItem != null)
        {
            AddItem.onClick.RemoveAllListeners();
            AddItem.onClick.AddListener(onAddItem);
            Debug.Log("🎯 Found and linked: AddItem");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: AddItem");
        }
        if (removeItem != null)
        {
            removeItem.onClick.RemoveAllListeners();
            removeItem.onClick.AddListener(onRemoveItem);
            Debug.Log("🎯 Found and linked: removeItem");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: removeItem");
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

