using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController3 : MonoBehaviour
{
    public static int playersInGame;

    public void MoveToScene(int id)
    {
        playersInGame = 4;
        SceneManager.LoadScene(id);
    }

}
