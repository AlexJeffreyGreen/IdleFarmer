using UnityEngine;
using System;


public class Loader : MonoBehaviour
{
    //public Camera mainCamera;
    public GameObject gameManager;
    private void Awake()
    {
        if (GameManager.instance == null)
            Instantiate(gameManager);
        //GameManager.instance._mainCamera = Camera.main;
    }
}