using System;
using UnityEngine;
using YG;

public class RestartButton : MyButton
{
    [SerializeField] private Game _game;

    protected override void OnButtonClick()
    {
        _game.Restart();
        YG2.InterstitialAdvShow();
    }
}
