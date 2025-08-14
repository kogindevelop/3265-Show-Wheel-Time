using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class AudioButton : MonoBehaviour
{
    private AudioManager _audioManager;
    private Button _button;

    private void Awake()
    {
        _audioManager = _audioManager = FindObjectOfType<AudioManager>(true);
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
        ProjectContext.Instance.Container.Inject(this);
    }

    private void OnButtonClick()
    {
        _audioManager.PlaySound(AudioClipID.Button_click);
    }
}
