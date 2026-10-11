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
public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }
    private GameManager gameManager;
    private GameExitManager gameExitManager;
    private ServiceHub serviceHub;
    //private SceneManager sceneManager;
    private CustomSceneManager customSceneManager;
    private HudManager hudManager;
    private MenuManager menuManager;
    //private ShopManager shopManager;

    [SerializeField] private Button Buy;
    [SerializeField] private Button Sell;
    [SerializeField] private Button ExitShop;


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

    }
    public void AutoHookupButtonsShop()
    {
        if (ShopManager.Instance == null)
        {
            Debug.LogError("InterfaceManager: Scenemanager.Instance is null! Cannot hook up buttons.");
            return;
        }

        ShopManager shopManager = ShopManager.Instance;// reinitalize the scene manager




        if (Buy != null)
        {
            Buy.onClick.RemoveAllListeners();
            Buy.onClick.AddListener(HudManager.Instance.onAddItem);
            Debug.Log("🎯 Found and linked: Buy");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Buy");
        }
        if (Sell != null)
        {
            Sell.onClick.RemoveAllListeners();
            Sell.onClick.AddListener(HudManager.Instance.onRemoveItem);
            Debug.Log("🎯 Found and linked: Sell");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Sell");
        }
        if (ExitShop != null)
        {
            ExitShop.onClick.RemoveAllListeners();
            ExitShop.onClick.AddListener(customSceneManager.onExitShop);
            Debug.Log("🎯 Found and linked: ExitShop");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: ExitShop");
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