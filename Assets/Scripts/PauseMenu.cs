using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private Button resumeButton;
    private Button exitButton;

    void Start()
    {
        if (uiDocument == null)
        {
            Debug.LogError("UI Document not assigned to PauseMenu!");
            return;
        }

        var root = uiDocument.rootVisualElement;

        resumeButton = root.Q<Button>("resumeButton");
        exitButton = root.Q<Button>("exitButton");

        if (resumeButton != null)
            resumeButton.clicked += ResumeGame;
        else
            Debug.LogError("Resume Button not found!");

        if (exitButton != null)
            exitButton.clicked += ExitToMainMenu;
        else
            Debug.LogError("Exit Button not found!");

        // Hide the UI at start
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }


    public void ShowPauseMenu()
    {
        Time.timeScale = 0f; // Pause the game
        uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
    }

    public void HidePauseMenu()
    {
        Time.timeScale = 1f; // Resume the game
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    private void ResumeGame()
    {
        HidePauseMenu();
    }

    private void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Resume time before switching scenes
        SceneManager.LoadScene("MainMenu");
    }
}