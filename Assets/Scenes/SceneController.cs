using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static int playersInGame;

    public void MoveToScene(int id)
    {
        playersInGame = 1;
        SceneManager.LoadScene(id);
    }

}
