using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSwitcher : MonoBehaviour
{
    public Camera[] effectCameras;

    private void Start()
    {
        Camera.main.tag = "Untagged";

        SetActiveCamera(0);
    }

    void OnGUI()
    {
        int i = 0;
        foreach (Camera c in effectCameras)
        {
            if (GUI.Button(new Rect(20f, i*20f*1.5f + 20f, 200f, 20f), c.name))
            {
                SetActiveCamera(i);
            }
            i++;
        }
    }

    private void SetActiveCamera(int i)
    {
        foreach (Camera c in effectCameras)
        {
            c.gameObject.tag = "Untagged";
            c.gameObject.SetActive(false);
        }

        effectCameras[i].gameObject.tag = "MainCamera";
        effectCameras[i].gameObject.SetActive(true);
    }
}
