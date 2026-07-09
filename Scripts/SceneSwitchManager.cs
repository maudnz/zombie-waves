using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchManager : MonoBehaviour
{


    public AudioSource audioSource;
    public AudioClip click;


    public void RunGame()
    {
        audioSource.PlayOneShot(click);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(click);
        Application.Quit();
    }

    public void MenuGame()
    {
        audioSource.PlayOneShot(click);
        SceneManager.LoadScene(0);
    }

}