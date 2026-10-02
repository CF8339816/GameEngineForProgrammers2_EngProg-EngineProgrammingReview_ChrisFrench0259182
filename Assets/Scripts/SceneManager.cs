using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

#region coder & project
/// <summary>
///GAME2018 / 4085 / Game Engine for Programmers II(B)/Englehart, Matthew/Robichaud, Sam
///Engine Programming Review 01 Centralized Input Manager
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations:
/// </summary>
#endregion
public class Scenemanager : MonoBehaviour
{
    private GameManager gameManager;
    private GameExitManager gameExitManager;
    private const int MENU_INDEX = 0;
    private const int LEVEL_1_INDEX = 1;
    private const int LEVEL_2_INDEX = 2;
    private const int LEVEL_3_INDEX = 3;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AutoHookupButtons();
    }
    private void AutoHookupButtons()
    {
        Button startButton = GameObject.Find("StartGame")?.GetComponent<Button>();
        if (startButton != null)
        {
            startButton.onClick.RemoveAllListeners();
            startButton.onClick.AddListener(onStart);
        }
        Button nextButton = GameObject.Find("nextLevel")?.GetComponent<Button>();
        if (nextButton != null)
        {
            nextButton.onClick.RemoveAllListeners();
            nextButton.onClick.AddListener(onLoadNextLevel);
        }
        Button menuButton = GameObject.Find("Menu")?.GetComponent<Button>();
        if (menuButton != null)
        {
            menuButton.onClick.RemoveAllListeners();
            menuButton.onClick.AddListener(onMenu);
        }
        Button level1Button = GameObject.Find("Level1")?.GetComponent<Button>();
        if (level1Button != null)
        {
            level1Button.onClick.RemoveAllListeners();
            level1Button.onClick.AddListener(onLevel1);
        }
        Button level2Button = GameObject.Find("level2")?.GetComponent<Button>();
        if (level2Button != null)
        {
            level2Button.onClick.RemoveAllListeners();
            level2Button.onClick.AddListener(onLevel2);
        }
        Button level3Button = GameObject.Find("level3")?.GetComponent<Button>();
        if (level3Button != null)
        {
            level3Button.onClick.RemoveAllListeners();
            level3Button.onClick.AddListener(onLevel3);
        }
        Button QuitButton = GameObject.Find("Quit")?.GetComponent<Button>();
      
        if (QuitButton != null)
        {
            QuitButton.onClick.RemoveAllListeners();

           
            if (GameExitManager.Instance != null)
            {
                QuitButton.onClick.AddListener(GameExitManager.Instance.Ongameexit);
            }
        }
        if (GameManager.Instance != null)
        {
            Button takeDamageButton = GameObject.Find("takeDamage")?.GetComponent<Button>();
            if (takeDamageButton != null)
            {
                takeDamageButton.onClick.RemoveAllListeners();
                takeDamageButton.onClick.AddListener(GameManager.Instance.onTakeDamage);
            }
            Button HealButton = GameObject.Find("Heal")?.GetComponent<Button>();
            if (HealButton != null)
            {
                HealButton.onClick.RemoveAllListeners();
                HealButton.onClick.AddListener(GameManager.Instance.onTakeHealing);
            }
            Button AddItemButton = GameObject.Find("AddItem")?.GetComponent<Button>();
            if (AddItemButton != null)
            {
                AddItemButton.onClick.RemoveAllListeners();
                AddItemButton.onClick.AddListener(GameManager.Instance.onAddItem);
            }
            Button removeItemButton = GameObject.Find("removeItem")?.GetComponent<Button>();
            if (removeItemButton != null)
            {
                removeItemButton.onClick.RemoveAllListeners();
                removeItemButton.onClick.AddListener(GameManager.Instance.onRemoveItem);
            }
        }
    }
    public void LoadLevelByIndex(int index)
    {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)    {   SceneManager.LoadScene(index);   }    
    }
    public void onLevel1()
    {
        LoadLevelByIndex(LEVEL_1_INDEX);
    }
    public void onLevel2()
    {
        LoadLevelByIndex(LEVEL_2_INDEX); 
    }
    public void onLevel3()
    {
        LoadLevelByIndex(LEVEL_3_INDEX); 
    }
    public void onMenu()
    {
        LoadLevelByIndex(MENU_INDEX); 
    }
    public void onStart()
    {
        if (GameManager.Instance != null)  
        {
            GameManager.Instance.onResetStats();
            onLevel1();
        }
    }
    public void onLoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        LoadLevelByIndex(nextIndex);
    }
}

