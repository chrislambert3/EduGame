using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour 
{
    public GameObject NextLevScreen;
    public GameObject GameUI;
    public TMP_InputField TextInput;
    public Slider Slider;
    public void GoToScene(string sceneName)
    {
        Debug.Log($"going to: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game has been slain(quit game)");
    }
    public void ActivateLevelFinish()
    {
        Debug.Log("ActivateLevelFinish has been called");
        GameUI.SetActive(false);
        NextLevScreen.SetActive(true);
    }

    public void SetTextBox()
    {
        TextInput.text = Slider.value.ToString("F2");
    }
}
