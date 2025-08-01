using BBS.Assets.Scripts.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BBS.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button playerButton;
        [SerializeField] private CanvasGroup mainMenuCanvasGroup;
        [SerializeField] private CanvasGroup introCanvasGroup;
        [SerializeField] private float fadeDuration = 1f;
        [SerializeField] private float introDuration = 3f;

        private void Start()
        {
            introCanvasGroup.alpha = 0;
            introCanvasGroup.blocksRaycasts = false;
            introCanvasGroup.gameObject.SetActive(false);

            mainMenuCanvasGroup.alpha = 1;
            mainMenuCanvasGroup.blocksRaycasts = true;

            playerButton.onClick.AddListener(OnPlayerClicked);
        }

        private void OnPlayerClicked()
        {
            playerButton.interactable = false;
            StartCoroutine(PlayIntroThenLoadGame());
        }

        private IEnumerator PlayIntroThenLoadGame()
        {
            yield return Fade(mainMenuCanvasGroup, 1f, 0f);
            mainMenuCanvasGroup.gameObject.SetActive(false);

            // Show intro
            introCanvasGroup.gameObject.SetActive(true);
            yield return Fade(introCanvasGroup, 0f, 1f);

            yield return new WaitForSeconds(introDuration);

            // Fade out intro
            yield return Fade(introCanvasGroup, 1f, 0f);

            // Load Game scene via GameSceneManager
            GameSceneManager.Instance.LoadScene(SceneName.GameScene, SceneManager.GetActiveScene().name);
        }

        private IEnumerator Fade(CanvasGroup canvasGroup, float from, float to)
        {
            float time = 0f;
            canvasGroup.alpha = from;
            canvasGroup.blocksRaycasts = true;

            while (time < fadeDuration)
            {
                time += Time.unscaledDeltaTime;
                canvasGroup.alpha = Mathf.Lerp(from, to, time / fadeDuration);
                yield return null;
            }

            canvasGroup.alpha = to;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
