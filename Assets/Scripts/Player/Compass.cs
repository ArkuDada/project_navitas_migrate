using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public static Compass Instance;

    [SerializeField] private RawImage compassImage;
    [SerializeField] private Transform player;
    [SerializeField] private Goal goal;

    [SerializeField] private GameObject markerPrefab;
    [SerializeField] private Sprite exitIcon;
    [SerializeField] private Sprite objIcon;

    private List<Marker> markerList = new List<Marker>();

    private float compassUnit;
    private float maxDis;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        goal = PlayerController.Instance.goal;
        compassUnit = compassImage.rectTransform.rect.width / 360f;
        ResetMarker();
    }

    void Update()
    {
        compassImage.uvRect = new Rect(player.localEulerAngles.y / 360, 0, 1, 1);

        foreach (Marker m in markerList)
        {
            m.image.rectTransform.anchoredPosition = GetPosOnCompass(m);

            if (m.affectByScale)
            {
                float dest = Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z),
                    m.position);
                float scale = 0;

                if (dest < maxDis)
                {
                    scale = 1f - (dest / maxDis);
                }

                m.image.rectTransform.localScale = Vector3.one
                                                   * scale;
            }
           
        }
    }

    private void AddNewMarker(Marker m)
    {
        GameObject newMarker = Instantiate(markerPrefab, compassImage.transform);
        m.image = newMarker.GetComponent<Image>();
        m.image.sprite = m.icon;

        markerList.Add(m);
    }

    Vector2 GetPosOnCompass(Marker m)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerForward = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(m.position - playerPos, playerForward);

        return new Vector2(compassUnit * angle, 0f);
    }

    public void ResetMarker()
    {
        maxDis = Vector3.Distance(player.position, goal.gameObject.transform.position);

        if (markerList.Count > 0)
        {
            foreach (Marker m in markerList)
            {
                Destroy(m.image.gameObject);
            }

            markerList.Clear();
        }

        foreach (var g in GameObject.FindGameObjectsWithTag("Objective"))
        {
            var m = g.GetComponent<Marker>();
            AddNewMarker(m);
        }
    }

    public void RemoveMarker(Marker m)
    {
        Destroy(m.image.gameObject);
        markerList.Remove(m);
    }
}