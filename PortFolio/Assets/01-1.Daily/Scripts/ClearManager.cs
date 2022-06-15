using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
    public static ClearManager instance
    {
        get
        {
            if (m_instance == null) m_instance = FindObjectOfType<ClearManager>();
            return m_instance;
        }
    }

    private static ClearManager m_instance;

    public static bool[] stageClear = new bool[4];

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }

        
    
    }

    
    
    
}
