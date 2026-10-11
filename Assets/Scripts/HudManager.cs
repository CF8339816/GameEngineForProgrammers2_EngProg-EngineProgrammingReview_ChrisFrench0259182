using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    private SceneManager sceneManager;
    private CustomSceneManager customSceneManager;
    // private HudManager hudManager;
    private MenuManager menuManager;
    private ShopManager shopManager;
    private InterfaceManager interfaceManager;

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


    //private Button ButtonHunterHUD(string buttonName)
    //{
    //    Debug.Log("🎯 Found  BH found");
    //    Button[] allActiveAndInactiveButtons = Resources.FindObjectsOfTypeAll<Button>();

    //    foreach (Button btn in allActiveAndInactiveButtons)
    //    {
    //        if (btn.gameObject.name == buttonName)
    //        {

    //            if (btn.gameObject.scene.isLoaded)
    //            {
    //                return btn;
    //            }
    //        }
    //    }       
    //    return null;
    //}


}
