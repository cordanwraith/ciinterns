using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 


public class MenuManager : MonoBehaviour {

    public GameObject titleScreenMenu;
    public GameObject levelSelectionMenu;
    public GameObject creditMenu;
    public GameObject mainMenu;

    private GameObject activeMenu;

    // Use this for initialization
    void Start () {
        mainMenu.SetActive(false);
        creditMenu.SetActive(false);
        levelSelectionMenu.SetActive(false);
        titleScreenMenu.SetActive(true);

        activeMenu = titleScreenMenu;

    }

    private void ChangeCanvas(GameObject canvasToActivate)
    {

        if (activeMenu != canvasToActivate)
        {
            activeMenu.SetActive(false);
            activeMenu = canvasToActivate;
            activeMenu.SetActive(true);
        }
    }

    public void ChangeToCredits()
    {
        ChangeCanvas(creditMenu);
    }
    public void ChangeToLevelSelection()
    {
        ChangeCanvas(levelSelectionMenu);
    }
    public void ChangeToMainMenu()
    {
        ChangeCanvas(mainMenu);
    }

    public void Lv1()
    {
        SceneManager.LoadScene(1);
    }
}
