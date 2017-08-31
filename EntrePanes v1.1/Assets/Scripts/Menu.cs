using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    #region Statics
    /*
    public static void ExitS()
    {
        Application.Quit();
    }
    public static void PrincipalS()
    {
        SceneManager.LoadScene(0);
    }
    public static void PlayS()
    {        
        SceneManager.LoadScene(1);
    }
    public static void ConfigS()
    {
        SceneManager.LoadScene(2);
    }
    */
    public static void WinS()
    {
        SceneManager.LoadScene(3);
    }
    #endregion
    #region NonStatics
    public void Exit()
    {
        Application.Quit();
    }
    public void Principal()
    {
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        PanActions.panY = 0f;
        SceneManager.LoadScene(1);
    }
    public void Config()
    {
        SceneManager.LoadScene(2);
    }
    public void Win()
    {
        SceneManager.LoadScene(3);
    }
    #endregion

}
