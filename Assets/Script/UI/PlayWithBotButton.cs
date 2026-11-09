using UnityEngine.SceneManagement;
using YG;

public class PlayWithBotButton : MyButton
{
    protected override void OnButtonClick()
    {
        YG2.InterstitialAdvShow();
        GameSettings.Instance.IsBotEnabled = true;
        SceneManager.LoadScene(ScenesData.Play);
    }
}
