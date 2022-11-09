using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string single;
    public string multi;
    public string ai;
    public GameObject settingsScreen;
    

    // Start is called before the first frame update
    void Start()
    {
        settingsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startSingle()
    {
        SceneManager.LoadScene(single);
    } 
    public void startMulti()
    {
       SceneManager.LoadScene(multi); 
    } 
    public void startAI()
    {
        SceneManager.LoadScene(ai);
    } 
    public void startSettings()
    {
        settingsScreen.SetActive(true);
    } 
    public void closeSettings()
    {
        settingsScreen.SetActive(false);
    }   
    public void QuitGame()
    {
       Application.Quit();
       Debug.Log("Quitting"); 
    } 
}
