using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _aboutCreatorsPanel;

    public event UnityAction ReturnButtonClicked;

    private IEnumerator DeactivateMenu()
    {
        const float delaySeconds = 1.2f; 
        
        yield return new WaitForSeconds(delaySeconds);
        _aboutCreatorsPanel.SetActive(false);
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(ScenesHolder.GameScene);
    }

    public void OnAboutCreatorsButtonClick()
    {
        _aboutCreatorsPanel.SetActive(true);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnReturnButtonClick()
    {
        ReturnButtonClicked?.Invoke();
        StartCoroutine(DeactivateMenu());
    }
}