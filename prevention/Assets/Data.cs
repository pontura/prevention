using UnityEngine;
using System.Collections;

public class Data : MonoBehaviour {
    
    static Data mInstance = null;
    public Settings settings;
    public UserData userData;
    public LocallizationManager locallizationManager;
    public Animation fade;
    string sceneName;
    public int timesScenesChanged;

    public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                Debug.LogError("Algo llama a DATA antes de inicializarse");
            }
            return mInstance;
        }
    }
	void Awake () {       
        
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
		DontDestroyOnLoad(this);
        fade.gameObject.SetActive(false);
    }
	public void LoadScene(string sceneName)
    {
        timesScenesChanged++;
        this.sceneName = sceneName;
        StartCoroutine(LoadFade());
    }
    IEnumerator LoadFade()
    {
        fade.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.35f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        yield return new WaitForSeconds(0.25f);
        fade.Play("off");
        yield return new WaitForSeconds(1);
        fade.gameObject.SetActive(false);
    }
}
