using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public FadeScreen fadeScreen;
    
    public void GoToAsyncScene(int sceneIndex)
    {
        StartCoroutine(GoToSceneAsyncRoutine(sceneIndex));
    }
     IEnumerator GoToSceneAsyncRoutine(int sceneIndex)
    {
        fadeScreen.FadeOut();

        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameData.instance.mainSceneLoadPosition = transform.position;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        Debug.LogWarning($"scene transition progrssion: {operation.progress.ToString()}");
        operation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;

            yield return null;
        }
        operation.allowSceneActivation = true;
    }
}
