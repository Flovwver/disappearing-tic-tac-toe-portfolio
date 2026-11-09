using UnityEngine.SceneManagement;
using UnityEngine;
using YG;


public class PlayWithFriendButton : MyButton
{
    protected override void OnButtonClick()
    {
        YG2.InterstitialAdvShow();
        GameSettings.Instance.IsBotEnabled = false;
        SceneManager.LoadScene(ScenesData.Play);
    }
}
