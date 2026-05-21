using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CreditsUI : MonoBehaviour
{
    [SerializeField] private TMP_Text creditsText;
    [SerializeField] private float autoReturnSeconds = 12f;

    private void Start()
    {
        bool es = LanguageManager.IsSpanish();
        creditsText.text = es ? GetCreditsES() : GetCreditsEN();
        StartCoroutine(AutoReturn());
    }

    private string GetCreditsES() => @"
JUST A NORMAL GAME

Desarrollo completo
Darío Moreno

Personaje y animaciones
Creados por Darío Moreno

Resto de assets
Generados con inteligencia artificial

Gracias por jugar.
";

    private string GetCreditsEN() => @"
JUST A NORMAL GAME

Full development
Darío Moreno

Character & animations
Created by Darío Moreno

Other assets
Generated with artificial intelligence

Thanks for playing.
";

    public void OnReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator AutoReturn()
    {
        yield return new WaitForSeconds(autoReturnSeconds);
        SceneManager.LoadScene("MainMenu");
    }
}