using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadScene(int level)
	{
		Application.LoadLevel(level);
	}
}
