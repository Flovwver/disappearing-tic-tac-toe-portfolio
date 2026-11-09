using UnityEngine.SceneManagement;

public class BackButton : MyButton
{
    protected override void OnButtonClick()
    {
        SceneManager.LoadScene(ScenesData.Menu);
    }
}
