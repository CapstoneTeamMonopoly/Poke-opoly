using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController1 : MonoBehaviour
{
    public static int playersInGame;

    public void MoveToScene(int id)
    {
        playersInGame = 2;
        SceneManager.LoadScene(id);
    }

}
