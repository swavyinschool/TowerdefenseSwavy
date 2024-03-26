using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class IntroMenu : MonoBehaviour
{
    private Button playButton;
    private Button quitButton;
    private TextField nameField;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        playButton = root.Q<Button>("PlayButton");
        quitButton = root.Q<Button>("QuitButton");
        nameField = root.Q<TextField>("NameField");

        playButton.clicked += OnPlayButtonClicked;
        quitButton.clicked += Application.Quit;

        playButton.SetEnabled(false);

       
    }

    void OnDestroy()
    {
        playButton.clicked -= OnPlayButtonClicked;
    }

    void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }

    void OnNameValueChanged(string newName)
    {
        if (newName.Length >= 3)
        {
            playButton.SetEnabled(true);
        }
        else
        {
            playButton.SetEnabled(false);
        }
    }
}
