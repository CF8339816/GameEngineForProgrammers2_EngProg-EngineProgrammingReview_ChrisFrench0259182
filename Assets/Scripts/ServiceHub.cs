using UnityEngine;
#region coder & project
/// <summary>
///GAME2018 / 4085 / Game Engine for Programmers II(B)/Englehart, Matthew/Robichaud, Sam
///Engine Programming Review 01 Centralized Input Manager
/// Coder current script: Chris French Second Year NSCC Game Programming 
/// Additions / annotations:
/// </summary>
#endregion
public class ServiceHub : MonoBehaviour
{    public static ServiceHub Instance { get; private set; }// creates szervice hub instance 
    [Header("System References")]// defines other scripts
    public Scenemanager customSceneManager;
    public GameManager gameManager;
    public GameExitManager gameExitManager;
    public camLookControler CameraController;
    public playercontroler _playerControler;
    public DensityManager densityManager;
    public DephaseBoxScript dephaseBoxScript;
    public inputcontroler _inputcontroler;


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
       
        if (CameraController == null) CameraController = GetComponent<camLookControler>();
        if (_playerControler == null) _playerControler = GetComponent<playercontroler>();
        if (_inputcontroler == null) _inputcontroler = GetComponent<inputcontroler>();
       
     



    }
}