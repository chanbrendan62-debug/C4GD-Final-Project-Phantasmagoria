using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button startButton;
    public Button quitButton;
    void Start()
    {
        startButton.onClick.AddListener(StartGameButton);
        quitButton.onClick.AddListener(QuitGameButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene("BrendanScene");
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }
}
