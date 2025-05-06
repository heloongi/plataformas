using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Button startButton;
    public Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(() => GameManager.Instance.LoadGameAndGUI());
        quitButton.onClick.AddListener(() => Application.Quit());
    }
}
