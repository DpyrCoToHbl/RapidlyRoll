using UnityEngine;

public class MainMenuAnimator : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Animator _menuAnimator;

    private const string AnimationDisappearTriggerName = "DisappearMenu";
    
    private void OnEnable()
    {
        _mainMenu.ReturnButtonClicked += OnReturnButtonClicked;
    }

    private void OnDisable()
    {
        _mainMenu.ReturnButtonClicked -= OnReturnButtonClicked;
    }

    private void OnReturnButtonClicked()
    {
        _menuAnimator.SetTrigger(AnimationDisappearTriggerName);
    }
}
