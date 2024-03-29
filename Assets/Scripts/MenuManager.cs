using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<Button> appButtons;
    [SerializeField] private List<GameObject> appPanel;

    private GameObject _currentApp;

    public void OpenApp()
    {
        if (_currentApp)
        {
            _currentApp.SetActive(false);
        }
        _currentApp = appPanel[appButtons.IndexOf(EventSystem.current.currentSelectedGameObject.GetComponent<Button>())];
        _currentApp.SetActive(true);
    }

    public void CloseApp()
    {
        _currentApp.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}