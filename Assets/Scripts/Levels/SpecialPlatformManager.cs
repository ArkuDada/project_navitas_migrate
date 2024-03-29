using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpecialPlatformManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<MovingPlatform> mplist = new List<MovingPlatform>();
    [SerializeField] private List<FlagilePlatform> fplist = new List<FlagilePlatform>();

    public void ResetPlatforms()
    {
        if (mplist.Count > 0)
        {
            foreach (var mp in mplist)
            {
                mp.SetDirection(0);
                mp.playerContact = false;
                print(mp.gameObject.name + mp.playerContact);
            }
        }
        if (fplist.Count > 0)
        {
            foreach (var fp in fplist)
            {
                fp.Reappear();
                fp.playerContact = false;
                print(fp.gameObject.name + fp.playerContact);
            }
        }
    }
}