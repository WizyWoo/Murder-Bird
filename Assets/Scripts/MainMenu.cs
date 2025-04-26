using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private Button startButton;
    private Button exitButton;

    void Start()
    {
        var root = uiDocument.rootVisualElement;

        startButton = root.Q<Button>("startButton");
        exitButton = root.Q<Button>("exitButton");

        startButton.clicked += StartGame;
        exitButton.clicked += ExitGame;
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    private void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting...");
    }
}
