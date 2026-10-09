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
public class Scenemanager : MonoBehaviour
{
    public static Scenemanager Instance { get; private set; }
    private GameManager gameManager;
    private GameExitManager gameExitManager;
    private InterfaceManager interfaceManager;
    private const int MAIN_INDEX = 0;
    private const int MENU_INDEX = 1;
    private const int LEVEL_1_INDEX = 2;
    private const int LEVEL_2_INDEX = 3;
    private const int LEVEL_3_INDEX = 4;
    private const int SHOP_INDEX = 5;
    private const int PLAYER_INDEX = 6;
    private const int HUD_INDEX = 7;
    private const int ENVIRONMENT_INDEX = 8;
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
        SceneManager.LoadScene(ENVIRONMENT_INDEX, LoadSceneMode.Additive);
        SceneManager.LoadScene(MENU_INDEX, LoadSceneMode.Additive);
        SceneManager.LoadScene(HUD_INDEX, LoadSceneMode.Additive); 
        SceneManager.LoadScene(PLAYER_INDEX, LoadSceneMode.Additive);

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

        CleanDuplicateSceneElements();

        // 5. Direct link to our real global singleton instance to bind the buttons
        if (InterfaceManager.Instance != null)
        {
            InterfaceManager.Instance.AutoHookupButtons();
        }
        else
        {
            interfaceManager = FindFirstObjectByType<InterfaceManager>();
            if (interfaceManager != null) interfaceManager.AutoHookupButtons();
        }
    }

    private void CleanDuplicateSceneElements()
    {
        Debug.Log("Scenemanager: Starting additive scene cleanup routine...");

        // 1. Clean up duplicate Cameras (Very common cause for broken UI clicks)
        Camera[] allCameras = FindObjectsByType<Camera>(FindObjectsSortMode.None);
        if (allCameras.Length > 1)
        {
            // Keep the first camera (index 0), destroy the rest
            for (int i = 1; i < allCameras.Length; i++)
            {
                Debug.Log($"[CLEANUP] Destroyed duplicate Camera on GameObject: {allCameras[i].gameObject.name}");
                Destroy(allCameras[i].gameObject);
            }
        }

        // Also clean up duplicate AudioListeners, as Unity flags warnings for multiples
        AudioListener[] allListeners = FindObjectsByType<AudioListener>(FindObjectsSortMode.None);
        if (allListeners.Length > 1)
        {
            for (int i = 1; i < allListeners.Length; i++)
            {
                Destroy(allListeners[i]); // Just remove the extra listener component
            }
        }

        // 2. Clean up duplicate Directional Lights
        Light[] allLights = FindObjectsByType<Light>(FindObjectsSortMode.None);
        bool foundMainDirectionalLight = false;
        foreach (Light light in allLights)
        {
            if (light.type == LightType.Directional)
            {
                if (!foundMainDirectionalLight)
                {
                    foundMainDirectionalLight = true; // Keep the first directional light we see
                }
                else
                {
                    Debug.Log($"[CLEANUP] Destroyed duplicate Directional Light: {light.gameObject.name}");
                    Destroy(light.gameObject);
                }
            }
        }

        GameObject[] allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        bool foundMainVolume = false;
        foreach (GameObject go in allObjects)
        {
            // Checks if the object is named Global Volume or contains a Volume component
            if (go.name.Contains("Global Volume") || go.name.Contains("PostProcess") || go.GetComponent("Volume") != null)
            {
                if (!foundMainVolume)
                {
                    foundMainVolume = true; // Keep the first one
                }
                else
                {
                    Debug.Log($"[CLEANUP] Destroyed duplicate Global Volume GameObject: {go.name}");
                    Destroy(go);
                }
            }
        }
    }
}