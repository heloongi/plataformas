using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
        private void Awake()
        {
            // Implementação do Singleton
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
    
            Instance = this;
            DontDestroyOnLoad(gameObject);
    
            // Carrega a próxima cena após inicializar
            LoadScene("Splash");
        }
    
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        
}
