using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private GameObject AdUI;
    [SerializeField]
    private GameObject PauseUI;
    [SerializeField]
    private GameObject QuitUI;
    [SerializeField]
    private GameObject PlayerLoseUI;
    [SerializeField]
    private GameObject PlayerWinUI;
     [SerializeField]
    private GameObject Levelcomplete;

    

    void HideAll()
    {
        
        AdUI.gameObject.SetActive(false);
        PauseUI.gameObject.SetActive(false);
        QuitUI.gameObject.SetActive(false);
        PlayerLoseUI.gameObject.SetActive(false);
        PlayerWinUI.gameObject.SetActive(false);
        Levelcomplete.gameObject.SetActive(false);
        
    }

    public void ShowADsUI()
    {
        HideAll();
        AdUI.gameObject.SetActive(true);
    }

    public void ShowPauseUI()
    {
        Debug.Log(PauseUI.gameObject);
        HideAll();
        PauseUI.gameObject.SetActive(true);
        
    }
    
    public void ShowQuitUI()
    {
        HideAll();
        QuitUI.gameObject.SetActive(true);
    }

    public void ShowPlayerLoseUI()
    {
        HideAll();
        PlayerLoseUI.gameObject.SetActive(true);
    }

    public void ShowPlayerWinUI()
    {
        HideAll();
        PlayerWinUI.gameObject.SetActive(true);
    }

    public void PauseGame ()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame ()
    {
        Time.timeScale = 1;
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    public void ShowLevelcomplete()
    {
        HideAll();
        Levelcomplete.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
