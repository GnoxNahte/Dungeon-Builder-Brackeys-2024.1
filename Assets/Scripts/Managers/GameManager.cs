using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManager will always be present in all scenes, not destroyed between scenes
public class GameManager : MonoBehaviour
{
    [SerializeField] private InputManager input;

    public static InputManager Input { get { return instance.input; } }

    // Singleton
    private static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("More than 1 GameManager. Destroying this. Name: " + name);
            return;
        }
    }
}
