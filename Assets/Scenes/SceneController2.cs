using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController2 : MonoBehaviour
{
    public static int playersInGame;

    public void MoveToScene(int id)
    {
        playersInGame = 3;
        SceneManager.LoadScene(id);
    }

}
