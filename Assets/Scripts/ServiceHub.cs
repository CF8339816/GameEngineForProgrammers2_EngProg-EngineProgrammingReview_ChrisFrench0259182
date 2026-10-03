using UnityEngine;
#region coder & project
/// <summary>
/// NSCC GAME2025 / 4086 / Game Programming III(B)/ Doucette,Matthew
/// Unity: Game Manager & Persistence
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations: Fixed naming conflict with Unity's built-in SceneManager.
/// </summary>
#endregion
public class ServiceHub : MonoBehaviour
{    public static ServiceHub Instance { get; private set; }// creates szervice hub instance 
    [Header("System References")]// defines other scripts
    public Scenemanager customSceneManager;
    public GameManager gameManager;
    public GameExitManager gameExitManager;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); //destroy whole object
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); //maintaains gameobject accross scenes
        if (customSceneManager == null) customSceneManager = GetComponent<Scenemanager>();
        if (gameManager == null) gameManager = GetComponent<GameManager>();
        if (gameExitManager == null) gameExitManager = GetComponent<GameExitManager>();
    }
}