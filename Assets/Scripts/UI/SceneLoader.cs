using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoading : MonoBehaviour
{
    public Animator Fade;

    //Loads scene transition
    public void SceneTransition(string Scene)
    {
        if (Scene == "MainMenu") //Go back to MainMenu from Hiring Scene by unloading Hiring scene
			StartCoroutine(LoadingScene(Scene, "Black"));
		else if (Scene == "Game") //idk if being used tbh might delete
			StartCoroutine(LoadingScene(Scene, "Black"));
	}
    //Loads scene non-additively
    IEnumerator LoadingScene(string Scene, string Transition)
    {
        Debug.Log("Transitioning to Scene: " + Scene);
        switch (Transition)
        {
            case "White":
                Fade.SetTrigger("FadeToWhite");
                break;
            case "Black":
                Fade.SetTrigger("FadeToBlack");
                break;
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(Scene);
    }
	public void ReloadScene()
	{
		Scene currentScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(currentScene.name);
	}

}
