using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour
{
    public GameObject SettingsCanvas;

    public GameObject volume;

    public GameObject credits;

    public GameObject teamCreds;

    public GameObject photoCreds;

    public GameObject makeYourOwnGame;
    public GameObject getInvolved;

    public GameObject backBtn;

    public GameObject quitBtn;

    public TextMeshProUGUI topText;

    void Awake()
    {
        SettingsCanvas.SetActive(false);
        CloseAllCreds();
    }
    public void OnOpenSettings()
    {
        SettingsCanvas.SetActive(true);
    }

    public void OnCloseSettings()
    {
        SettingsCanvas.SetActive(false);
    }

    void CloseAllCreds()
    {
        topText.text = "<u>SETTINGS</u>";
        photoCreds.SetActive(false);
        teamCreds.SetActive(false);
        makeYourOwnGame.SetActive(false);
        getInvolved.SetActive(false);
        backBtn.SetActive(false);
        credits.SetActive(true);
        volume.SetActive(true);
        quitBtn.SetActive(true);
    }

    public void OnBackButton()
    {
        CloseAllCreds();
    }

    public void OnOpenCredit(int cred)
    {
        if(cred == 0)
        {
            topText.text = "Developer Credits";
            teamCreds.SetActive(true);
        }
        else if( cred == 1){
            topText.text = "Photo Credits";
            photoCreds.SetActive(true);
        }
        else if( cred == 2)
        {
            topText.text = "Make Your Own Game";
            makeYourOwnGame.SetActive(true);
        }
        else if( cred == 3)
        {
            topText.text = "Get Involved";
            getInvolved.SetActive(true);
        }
        backBtn.SetActive(true);
        credits.SetActive(false);
        volume.SetActive(false);
        quitBtn.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
