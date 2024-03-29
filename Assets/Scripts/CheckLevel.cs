using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckLevel : MonoBehaviour
{
    [SerializeField] private Button[] levels;
    private LevelTracker lt;
    private LevelCondition lc;

    void Start()
    {
        lt = LevelTracker.Instance;
        LoadStage();
    }

    public void LoadStage()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            Button currentB = levels[i];

            if (i + 1 == levels.Length)
            {
                break;
            }

            Button nextB = levels[i + 1];

            lc = lt.GetCondition(currentB.gameObject.name);

            nextB.enabled = lc.levelPass;
            nextB.gameObject.GetComponent<Image>().color = lc.levelPass ? new Color(1, 1, 1) : new Color(0.5f, 0.5f, 0.5f);
        }
    }
}