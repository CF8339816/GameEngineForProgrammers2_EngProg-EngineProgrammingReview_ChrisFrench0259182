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
public class InterfaceManager : MonoBehaviour
{

    public static InterfaceManager Instance { get; private set; }
    private GameManager gameManager;
    private GameExitManager gameExitManager;
    //private Scenemanager sceneManager;
    [SerializeField] private Button Buy;
    [SerializeField] private Button Heal;
    [SerializeField] private Button takeDamage;
    [SerializeField] private Button removeItem;
    [SerializeField] private Button Level1;
    [SerializeField] private Button level2;
    [SerializeField] private Button level3;
    [SerializeField] private Button Sell;
    [SerializeField] private Button Quit;
    [SerializeField] private Button AddItem;
    [SerializeField] private Button ExitShop;
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
    //private Button ButtonHunter(string buttonName)
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
    public void AutoHookupButtons()//)
    {
        if (Scenemanager.Instance == null)
        {
            Debug.LogError("InterfaceManager: Scenemanager.Instance is null! Cannot hook up buttons.");
            return;
        }

        Scenemanager sceneManager = Scenemanager.Instance;// reinitalize the scene manager


       // Button startButton = ButtonHunter("StartGame");
        if (StartGame != null)
        {
            StartGame.onClick.RemoveAllListeners();
            StartGame.onClick.AddListener(sceneManager.onStart);
            Debug.Log("🎯 Found and linked: StartGame");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: StartGame");
        }

       // Button nextButton = ButtonHunter("nextLevel");
        if (NextLevel != null)
        {
            NextLevel.onClick.RemoveAllListeners();
            NextLevel.onClick.AddListener(sceneManager.onLoadNextLevel);
            Debug.Log("🎯 Found and linked: nextLevel");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: nextLevel");
        }

      //  Button menuButton = ButtonHunter("Menu");
        if (Menu != null)
        {
            Menu.onClick.RemoveAllListeners();
            Menu.onClick.AddListener(sceneManager.onMenu);
            Debug.Log("🎯 Found and linked: Menu");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Menu");
        }

       // Button level1Button = ButtonHunter("Level1");
        if (Level1 != null)
        {
            Level1.onClick.RemoveAllListeners();
            Level1.onClick.AddListener(sceneManager.onLevel1);
            Debug.Log("🎯 Found and linked: Level1");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Level1");
        }

       // Button level2Button = ButtonHunter("level2");
        if (level2 != null)
        {
            level2.onClick.RemoveAllListeners();
            level2.onClick.AddListener(sceneManager.onLevel2);
            Debug.Log("🎯 Found and linked: level2");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: level2");
        }

       // Button level3Button = ButtonHunter("level3");
        if (level3 != null)
        {
            level3.onClick.RemoveAllListeners();
            level3.onClick.AddListener(sceneManager.onLevel3);
            Debug.Log("🎯 Found and linked: level3");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: level3");
        }

       // Button ShopButton = ButtonHunter("Shop");
        if (Shop != null)
        {
            Shop.onClick.RemoveAllListeners();
            Shop.onClick.AddListener(sceneManager.onShop);
            Debug.Log("🎯 Found and linked: Shop");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Shop");
        }

       // Button ExitShopButton = ButtonHunter("ExitShop");
        if (ExitShop != null)
        {
            ExitShop.onClick.RemoveAllListeners();
            ExitShop.onClick.AddListener(sceneManager.onExitShop);
            Debug.Log("🎯 Found and linked: ExitShop");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: ExitShop");
        }

       // Button QuitButton = ButtonHunter("Quit");
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
       //     Button takeDamageButton = ButtonHunter("takeDamage");
            if (takeDamage != null)
            {
                takeDamage.onClick.RemoveAllListeners();
                takeDamage.onClick.AddListener(GameManager.Instance.onTakeDamage);
                Debug.Log("🎯 Found and linked: takeDamage");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: takeDamage");
            }

         //   Button HealButton = ButtonHunter("Heal");
            if (Heal != null)
            {
                Heal.onClick.RemoveAllListeners();
                Heal.onClick.AddListener(GameManager.Instance.onTakeHealing);
                Debug.Log("🎯 Found and linked: Heal");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: Heal");
            }

         //   Button AddItemButton = ButtonHunter("AddItem");
            if (AddItem != null)
            {
                //AddItem.onClick.RemoveAllListeners();
                AddItem.onClick.AddListener(GameManager.Instance.onAddItem);
                Debug.Log("🎯 Found and linked: AddItem");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: AddItem");
            }

         //   Button removeItemButton = ButtonHunter("removeItem");
            if (removeItem != null)
            {
                removeItem.onClick.RemoveAllListeners();
                removeItem.onClick.AddListener(GameManager.Instance.onRemoveItem);
                Debug.Log("🎯 Found and linked: removeItem");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: removeItem");
            }

         //   Button BuyButton = ButtonHunter("Buy");
            if (Buy != null)
            {
                Buy.onClick.RemoveAllListeners();
                Buy.onClick.AddListener(GameManager.Instance.onAddItem);
                Debug.Log("🎯 Found and linked: Buy");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: Buy");
            }

          //  Button SellButton = ButtonHunter("Sell");
            if (Sell != null)
            {
                Sell.onClick.RemoveAllListeners();
                Sell.onClick.AddListener(GameManager.Instance.onRemoveItem);
                Debug.Log("🎯 Found and linked: Sell");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: Sell");
            }





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

