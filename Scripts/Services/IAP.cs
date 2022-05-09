using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAP : MonoBehaviour
{
    public SceneIndex __Index;
    private string extra = "com.overwrittengame.dicvocabulary.extrawords";
    private string noads = "com.overwrittengame.dicvocabulary.noads";

    public void OnPurchaseComplete(Product product)
    {
        if(product.definition.id == extra)
        {
            Debug.Log("Compra de contenido extra ha sido exitoso");
            __Index.ComprarContenido();
        }


        if(product.definition.id == noads)
        {
            __Index.QuitarAnuncio();
            Debug.Log("Compra de Anuncios exitoso");
        }
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("compra fallida");
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

