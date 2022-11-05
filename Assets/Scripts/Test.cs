using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Test : MonoBehaviour
{
    float x;

    void Dooooo() 
    {
        x = gameObject.transform.position.x;

        decimal decX = (decimal)Mathf.Round(x * 10f) * 0.1m;
        Debug.Log(string.Format("decimalX : {0}, x : {1}, same? : {2}", decX, x, decX == (decimal)x));
        
        if (x > 9)
        {
            Debug.Log(string.Format("Mathf.Round(x * 10f) = {0}", Mathf.Round(x * 10f) ));                                  // 99
            Debug.Log(string.Format("Mathf.Round(x * 10f) * 0.1f = {0}", Mathf.Round(x * 10f) * 0.1f ));                    // 9.900001
            Debug.Log(string.Format("(decimal)Mathf.Round(x * 10f) * 0.1m = {0}", (decimal)Mathf.Round(x * 10f) * 0.1m ));  // 9.9
        }
    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.Translate(new Vector2(1.1f, 0f));
            Dooooo();
        }
    }
    
}
