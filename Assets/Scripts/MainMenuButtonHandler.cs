using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    public void OpenMainMenu() {
        SceneManager.LoadScene(0);
    }
}
