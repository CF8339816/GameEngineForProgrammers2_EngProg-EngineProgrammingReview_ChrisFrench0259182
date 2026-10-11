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
    private SceneManager sceneManager;
    private CustomSceneManager customSceneManager;
    private HudManager hudManager;
    private MenuManager menuManager;
    //private ShopManager shopManager;
    private InterfaceManager interfaceManager;


    //private Button ButtonHunterShop(string buttonName)
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
