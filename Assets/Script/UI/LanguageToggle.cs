using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(Toggle))]
public class LanguageToggle : MonoBehaviour
{
    [SerializeField] private string _languageCode = "ru";

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();

        _toggle.SetIsOnWithoutNotify(YG2.lang == _languageCode);
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(ChangeLanguage);
        YG2.onSwitchLang += OnSwitchLang;
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(ChangeLanguage);
        YG2.onSwitchLang -= OnSwitchLang;
    }

    private void OnSwitchLang(string lang)
    {
        if (lang != _languageCode)
        {
            OffToggle();
        }
    }

    private void ChangeLanguage(bool isOn)
    {
        if (isOn) 
            YG2.SwitchLanguage(_languageCode);
    } 

    private void OffToggle()
    {
        _toggle.isOn = false;
    }
}
