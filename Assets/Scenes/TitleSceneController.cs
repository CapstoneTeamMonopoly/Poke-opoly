using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    public void MoveToScene(int id)
    {
        SceneManager.LoadScene(id);
    }
}