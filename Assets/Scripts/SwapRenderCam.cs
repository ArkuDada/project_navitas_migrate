using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapRenderCam : MonoBehaviour
{
    private Canvas _canvas;
    [SerializeField] private GameObject es;
    [SerializeField] private Camera main;
    [SerializeField] private Camera render;

    private bool _swap = true;
    
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        
        Time.timeScale = 1;
        _swap = true;
        main.gameObject.SetActive(false);
        es.SetActive(false);
        _canvas.worldCamera = render;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Swap()
    {
        if (_swap)
        {
            main.gameObject.SetActive(true);
            es.SetActive(true);
            _canvas.worldCamera = main;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            main.gameObject.SetActive(false);
            es.SetActive(false);
            _canvas.worldCamera = render;
            Cursor.lockState = CursorLockMode.Locked;
        }

        _swap = !_swap;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Swap();
        }
    }
}