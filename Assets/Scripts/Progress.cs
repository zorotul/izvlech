using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public int Coins;
    public int Width;
    public int Height;
    public int PriceW; 
    public int PriceH;

    public static Progress Instance;
    
    private void Awake()
    {
        if (PriceW == 0 || PriceW == 10)
        {
            PriceW = 10;
        }
        if (PriceH == 0 || PriceH == 20)
        {
            PriceH = 20;
        }

        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
 
}
