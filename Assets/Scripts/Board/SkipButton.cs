using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButton : MonoBehaviour
{
    void Start() {}

    void Update() {}

    private void OnMouseDown()
    {
        GameManager.SkipButtonPressed();
    }
}
