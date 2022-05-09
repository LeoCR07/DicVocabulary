using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetMouseButtonDown(0)&& !IsMouseOverUI())
        {
            Debug.Log("Hola");
        }
        
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void Entrada()
    {
        Debug.Log("Entrada");
    }

    public void Salida()
    {
        

        Debug.Log("Salida");
    }
}
