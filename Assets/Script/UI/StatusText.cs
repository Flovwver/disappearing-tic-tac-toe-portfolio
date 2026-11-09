using TMPro;
using UnityEngine;
using YG;

public class StatusText : MonoBehaviour
{
    [SerializeField] private Game _game;

    [SerializeField] private string _ruExWin = "Игрок X победил!";
    [SerializeField] private string _enExWin = "Player X wins!";

    [SerializeField] private string _ruOhWin = "Игрок O победил!";
    [SerializeField] private string _enOhWin = "Player O wins!";

    [SerializeField] private string _ruDraw = "Ничья!";
    [SerializeField] private string _enDraw = "Draw!";

    private string _drawMessage;
    private string _ohWinsMessage;
    private string _exWinsMessage;
    private TMP_Text _statusText;

    private void Awake()
    {
        _statusText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _game.WinnerDetermined += HandleWinnerDetermined;
        _game.Restarted += Restart;
        YG2.onSwitchLang += SwitchLanguage;
        SwitchLanguage(YG2.lang);
    }

    private void OnDisable()
    {
        _game.WinnerDetermined -= HandleWinnerDetermined;
        _game.Restarted -= Restart;
        YG2.onSwitchLang -= SwitchLanguage;
    }

    private void Restart()
    {
        _statusText.text = string.Empty;
    }

    private void HandleWinnerDetermined(PlayerSymbol winner)
    {
        switch (winner)
        {
            case PlayerSymbol.Ex:
                _statusText.text = _exWinsMessage;
                break;
            case PlayerSymbol.Oh:
                _statusText.text = _ohWinsMessage;
                break;
            case PlayerSymbol.Void:
                _statusText.text = _drawMessage;
                break;
        }
    }

    private void SwitchLanguage(string lang)
    {
        switch (lang)
        {
            case "ru":
                _drawMessage = _ruDraw;
                _exWinsMessage = _ruExWin;
                _ohWinsMessage = _ruOhWin;
                break;
            default:
                _drawMessage = _enDraw;
                _exWinsMessage = _enExWin;
                _ohWinsMessage = _enOhWin;
                break;
        }
    }
}
