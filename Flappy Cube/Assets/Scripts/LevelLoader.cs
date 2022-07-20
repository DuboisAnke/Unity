using UnityEngine.SceneManagement;

public static class LevelLoader
{
    public static LevelScriptableObject levelToLoad;

    public static void LoadLevel(LevelScriptableObject level)
    {
        levelToLoad = level;
        SceneManager.LoadScene("Level"); // Make sure this name is matching
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}



