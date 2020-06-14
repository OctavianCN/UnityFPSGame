using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingScene : MonoBehaviour
{
    
    [SerializeField] private Slider slider;
    public static int sceneToLoad;
   
    void Start()
    {
        if(slider!=null)
            StartCoroutine(this.SceneLoad());
    }

    private IEnumerator SceneLoad()
    {
        
        yield return new WaitForSeconds(0.1f);
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(sceneToLoad);

        while(gameLevel.progress < 1)
        {
            slider.value = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
        
    }
  
}
