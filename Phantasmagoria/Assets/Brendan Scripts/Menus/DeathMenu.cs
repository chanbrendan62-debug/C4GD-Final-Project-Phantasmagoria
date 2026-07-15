using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject deathScreenUI;

    private void Start()
    {
        if (deathScreenUI != null)
        {
            deathScreenUI.SetActive(false);
        }
    }

    public void TriggerDeath()
    {
        deathScreenUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("BrendanMainMenu");
    }
}