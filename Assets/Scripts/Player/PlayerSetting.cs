using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera firstPersonCamera;

    void Update()
    {
        firstPersonCamera.fieldOfView = Mathf.Clamp(PlayerPrefs.GetFloat("FOV"),60,100);
        firstPersonCamera.GetComponent<MouseLook>().mouseSensitivity = (PlayerPrefs.GetFloat("Sensitivity") * 10f);

        foreach (var obj in GameObject.FindGameObjectsWithTag("Audio_SFX"))
        {
            var source = obj.GetComponent<AudioSource>();
            source.volume = PlayerPrefs.GetFloat("Volume");
        }
        
    }
}