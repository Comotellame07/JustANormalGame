using UnityEngine;

public static class LanguageManager
{
    private const string KEY = "Language";

    public static bool IsSpanish()
    {
        return PlayerPrefs.GetString(KEY, "es") == "es";
    }

    public static void SetSpanish()
    {
        PlayerPrefs.SetString(KEY, "es");
        PlayerPrefs.Save();
    }

    public static void SetEnglish()
    {
        PlayerPrefs.SetString(KEY, "en");
        PlayerPrefs.Save();
    }
}