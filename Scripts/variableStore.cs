using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class variableStore : MonoBehaviour
{
    /// <summary>
    /// WW - carries over swing value from main to game
    /// </summary>
    public static variableStore instance;
    public static float swing;

    public void setSwing(float value)
    {
        swing = value;
    }

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
