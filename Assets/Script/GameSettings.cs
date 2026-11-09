using UnityEngine;

public sealed class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }

    public bool IsBotEnabled;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
