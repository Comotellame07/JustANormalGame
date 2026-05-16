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

Diseño de assets y animaciones
Pixel Adventure 1
por Pixel Frog
(assets utilizados bajo licencia de uso libre
con atribución — itch.io)

Gracias por jugar.
";

    private string GetCreditsEN() => @"
JUST A NORMAL GAME

Full development
Darío Moreno

Asset design & animations
Pixel Adventure 1
by Pixel Frog
(assets used under free license
with attribution — itch.io)

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