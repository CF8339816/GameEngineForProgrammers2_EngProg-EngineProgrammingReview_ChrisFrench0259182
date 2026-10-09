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
    private Button ButtonHunter(string buttonName)
    {
        Debug.Log("🎯 Found  BH found");
        Button[] allActiveAndInactiveButtons = Resources.FindObjectsOfTypeAll<Button>();
       
        foreach (Button btn in allActiveAndInactiveButtons)
        {
            if (btn.gameObject.name == buttonName)
            {
               
                if (btn.gameObject.scene.isLoaded)
                {
                    return btn;
                }
            }
        }       
        return null;
    }
    public void AutoHookupButtons()
    {
        if (Scenemanager.Instance == null)
        {
            Debug.LogError("InterfaceManager: Scenemanager.Instance is null! Cannot hook up buttons.");
            return;
        }

        Scenemanager sceneManager = Scenemanager.Instance;// reinitalize the scene manager


        Button startButton = ButtonHunter("StartGame");
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(sceneManager.onStart);
            Debug.Log("🎯 Found and linked: StartGame");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: StartGame");
        }

        Button nextButton = ButtonHunter("nextLevel");
        if (nextButton != null)
        {
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(sceneManager.onLoadNextLevel);
            Debug.Log("🎯 Found and linked: nextLevel");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: nextLevel");
        }

        Button menuButton = ButtonHunter("Menu");
        if (menuButton != null)
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(sceneManager.onMenu);
            Debug.Log("🎯 Found and linked: Menu");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Menu");
        }

        Button level1Button = ButtonHunter("Level1");
        if (level1Button != null)
        {
            level1Button.onClick.RemoveAllListeners();
            level1Button.onClick.AddListener(sceneManager.onLevel1);
            Debug.Log("🎯 Found and linked: Level1");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Level1");
        }

        Button level2Button = ButtonHunter("level2");
        if (level2Button != null)
        {
            level2Button.onClick.RemoveAllListeners();
            level2Button.onClick.AddListener(sceneManager.onLevel2);
            Debug.Log("🎯 Found and linked: level2");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: level2");
        }

        Button level3Button = ButtonHunter("level3");
        if (level3Button != null)
        {
            level3Button.onClick.RemoveAllListeners();
            level3Button.onClick.AddListener(sceneManager.onLevel3);
            Debug.Log("🎯 Found and linked: level3");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: level3");
        }

        Button ShopButton = ButtonHunter("Shop");
        if (ShopButton != null)
        {
            ShopButton.onClick.RemoveAllListeners();
            ShopButton.onClick.AddListener(sceneManager.onShop);
            Debug.Log("🎯 Found and linked: Shop");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Shop");
        }

        Button ExitShopButton = ButtonHunter("ExitShop");
        if (ExitShopButton != null)
        {
            ExitShopButton.onClick.RemoveAllListeners();
            ExitShopButton.onClick.AddListener(sceneManager.onExitShop);
            Debug.Log("🎯 Found and linked: ExitShop");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: ExitShop");
        }

        Button QuitButton = ButtonHunter("Quit");
        if (QuitButton != null)
        {
            QuitButton.onClick.RemoveAllListeners();
            if (GameExitManager.Instance != null)
            {
                QuitButton.onClick.AddListener(GameExitManager.Instance.Ongameexit);
            }
            Debug.Log("🎯 Found and linked: Quit");
        }
        else
        {
            Debug.LogWarning("❌ Could NOT find Button GameObject named: Quit");
        }

        if (GameManager.Instance != null)
        {
            Button takeDamageButton = ButtonHunter("takeDamage");
            if (takeDamageButton != null)
            {
                takeDamageButton.onClick.RemoveAllListeners();
                takeDamageButton.onClick.AddListener(GameManager.Instance.onTakeDamage);
                Debug.Log("🎯 Found and linked: takeDamage");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: takeDamage");
            }

            Button HealButton = ButtonHunter("Heal");
            if (HealButton != null)
            {
                HealButton.onClick.RemoveAllListeners();
                HealButton.onClick.AddListener(GameManager.Instance.onTakeHealing);
                Debug.Log("🎯 Found and linked: Heal");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: Heal");
            }

            Button AddItemButton = ButtonHunter("AddItem");
            if (AddItemButton != null)
            {
                AddItemButton.onClick.RemoveAllListeners();
                AddItemButton.onClick.AddListener(GameManager.Instance.onAddItem);
                Debug.Log("🎯 Found and linked: AddItem");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: AddItem");
            }

            Button removeItemButton = ButtonHunter("removeItem");
            if (removeItemButton != null)
            {
                removeItemButton.onClick.RemoveAllListeners();
                removeItemButton.onClick.AddListener(GameManager.Instance.onRemoveItem);
                Debug.Log("🎯 Found and linked: removeItem");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: removeItem");
            }

            Button BuyButton = ButtonHunter("Buy");
            if (AddItemButton != null)
            {
                AddItemButton.onClick.RemoveAllListeners();
                AddItemButton.onClick.AddListener(GameManager.Instance.onAddItem);
                Debug.Log("🎯 Found and linked: Buy");
            }
            else
            {
                Debug.LogWarning("❌ Could NOT find Button GameObject named: Buy");
            }

            Button SellButton = ButtonHunter("Sell");
            if (removeItemButton != null)
            {
                removeItemButton.onClick.RemoveAllListeners();
                removeItemButton.onClick.AddListener(GameManager.Instance.onRemoveItem);
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

