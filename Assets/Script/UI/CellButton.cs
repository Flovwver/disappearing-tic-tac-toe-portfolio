using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Cell))]
public class CellButton : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] private Sprite _ex;
    [SerializeField] private Sprite _oh;
    [SerializeField] private Sprite _void;

    [SerializeField] private float _previewSpriteAlfa = 0.5f;
    [SerializeField] private float _minBlinkSpriteAlfa = 0.5f;
    [SerializeField] private float _maxSpriteAlfa = 1f;
    [SerializeField] private float _blinkDuration = 1f;

    [SerializeField] private PlayerSymbol _previewSymbol = PlayerSymbol.Ex;

    [SerializeField] private Image _image;
    [SerializeField] private Game _game;

    private Cell _cell;
    private Tween _blinkTween;

    private void Awake()
    {
        _cell = GetComponent<Cell>();
    }

    private void OnEnable()
    {
        _cell.SymbolSetted += OnSymbolSetted;
        _cell.MarkedForRemoval += OnMarkedForRemoval;
        _cell.UnmarkedForRemoval += OnUnmarkedForRemoval;
        _game.TurnChanged += SetPreviewSymbol;
        _game.WinnerDetermined += OnWinnerDetermined;
    }

    private void OnDisable()
    {
        _cell.SymbolSetted -= OnSymbolSetted;
        _cell.UnmarkedForRemoval -= OnUnmarkedForRemoval;
        _cell.MarkedForRemoval -= OnMarkedForRemoval;
        _game.TurnChanged -= SetPreviewSymbol;
        _game.WinnerDetermined -= OnWinnerDetermined;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_cell.Symbol != PlayerSymbol.Void)
            return;

        _image.sprite = GetSprite(_previewSymbol);
        SetAlpha(_previewSpriteAlfa);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_cell.Symbol != PlayerSymbol.Void)
            return;

        _image.sprite = _void;
        SetAlpha(_maxSpriteAlfa);
    }

    private void OnMarkedForRemoval()
    {
        var endColor = _image.color;
        endColor.a = _minBlinkSpriteAlfa;
        _blinkTween = _image
            .DOColor(endColor, _blinkDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    private void OnUnmarkedForRemoval()
    {
        _blinkTween?.Kill();
        _blinkTween = null;
        SetAlpha(_maxSpriteAlfa);
    }

    private void OnWinnerDetermined(PlayerSymbol symbol)
    {
        OnUnmarkedForRemoval();
    }

    private void OnSymbolSetted(PlayerSymbol symbol)
    {
        _image.sprite = GetSprite(symbol);
        SetAlpha(_maxSpriteAlfa);
    }

    private void SetPreviewSymbol(PlayerSymbol symbol)
    {
        _previewSymbol = symbol;
    }

    private Sprite GetSprite(PlayerSymbol symbol)
    {
        return symbol switch
        {
            PlayerSymbol.Ex => _ex,
            PlayerSymbol.Oh => _oh,
            _ => _void
        };
    }

    private void SetAlpha(float alpha)
    {
        Color color = _image.color;
        color.a = alpha;
        _image.color = color;
    }
}
