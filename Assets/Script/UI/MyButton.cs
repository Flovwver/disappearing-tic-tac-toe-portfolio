using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Button))]
public abstract class MyButton : MonoBehaviour
{
    [SerializeField] private string _ru;
    [SerializeField] private string _en;
    [SerializeField] private TMP_Text _text;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        YG2.onSwitchLang += SwitchLanguage;
        SwitchLanguage(YG2.lang);
    }


    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        YG2.onSwitchLang -= SwitchLanguage;
    }

    private void SwitchLanguage(string lang)
    {
        if (_text == null)
            return;

        switch (lang)
        {
            case "ru":
                _text.text = _ru;
                break;
            default:
                _text.text = _en;
                break;
        }
    }

    protected abstract void OnButtonClick();
}
