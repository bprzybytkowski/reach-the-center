using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonHandler : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
