using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    private void Start()
    {
        Invoke("GoToMainMenu", 2f); // espera 2 segundos
    }

    void GoToMainMenu()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }
}
