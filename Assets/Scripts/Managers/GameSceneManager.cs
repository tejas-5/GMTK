using BBS.Assets.Scripts.Utilities;
using BBS.Utilities;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : Singleton<GameSceneManager>
{
    protected override bool dontDestroyOnLoad => true;

    protected override void OnSingletonAwake()
    {
        Debug.Log("SceneManager initialized.");
        SceneManager.sceneLoaded += OnSceneLoaded;
        // FIXED: Correct method call
        StartCoroutine(LoadSceneRoutine(SceneName.MainMenu, null));
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != SceneName.Manager)
            SceneManager.SetActiveScene(scene);
    }

    public void LoadScene(string sceneToLoad, string sceneToUnload)
    {
        StartCoroutine(LoadSceneRoutine(sceneToLoad, sceneToUnload));
    }

    private IEnumerator LoadSceneRoutine(string sceneToLoad, string sceneToUnload)
    {
        // Load the new scene additively
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        while (!loadOp.isDone)
            yield return null;

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
        Debug.Log($"Loaded scene: {sceneToLoad}");

        // Unload the previous scene if valid and not the Manager
        if (!string.IsNullOrEmpty(sceneToUnload) && sceneToUnload != SceneName.Manager)
        {
            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneToUnload);
            while (!unloadOp.isDone)
                yield return null;

            Debug.Log($"Unloaded scene: {sceneToUnload}");
        }
    }
}
