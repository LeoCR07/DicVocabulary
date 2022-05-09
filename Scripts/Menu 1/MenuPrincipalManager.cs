using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Android;
using System;

public class MenuPrincipalManager : MonoBehaviour
{
    private Button Btn;
    public GridLayoutGroup gridLayout;
    //  public AdsManager ads;
    public AdsBanner banner;
    public SceneIndex __Index;

    /* Dialog */
    public GameObject __Dialog;

    void Start()
    {
        if (PlayerPrefs.GetInt("PrimeraVez") != 1)
        {
            gridLayout.gameObject.SetActive(false);
            __Dialog.gameObject.SetActive(true);
        }
        else
        {

            gridLayout.gameObject.SetActive(true);
            __Dialog.gameObject.SetActive(false);
        }

        //ads.ShowBanner();
        ValidarAds();
        Size();

       
    }

    private void ValidarAds()
    {
        
        if(__Index.GetAnuncio() )
        {
            banner.HideBannerAd();
         
          // ads.HideBanner();
        }
        else
        {
            banner.ShowBannerAd();
            //ads.ShowBanner();
        }
    }

    IEnumerator RepeatShowBanner()
    {

        yield return new WaitForSeconds(1);
        Debug.Log("Cada segundo");

    }

    /* Layout */
    void Size()
    {

        double with = Screen.width;
        double high = Screen.height;

        // float space = (float) ((with / 2)-(with*0.019));
        float space = (float) (with * 0.018);
        float x = (float)(with * 0.48);
        float y = (float) (high / 3.4);


        //Vector2 valor = gridLayout.cellSize;
        gridLayout.cellSize = new Vector2(x, y);
        gridLayout.spacing = new Vector2(space,space);



    }

    
    public Vector2 GetMainGameViewSize()
    {
        System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
        return (Vector2)Res;
    }
    

    public void ElegirScene(int ID)
    {
        if (ID == 0)
        {
            /* Modo Juego */
            SceneManager.LoadScene(5);
        }
        else if (ID == 1)
        {
            /* Diccionario */
            SceneManager.LoadScene(3);

        }
        else if(ID == 2)
        {
            /* Compras */
            SceneManager.LoadScene(4);

        }
        else if(ID == 3)
        {
            /* Idioma */
            SceneManager.LoadScene(6);

        }
    }

    public void ElegirIdioma()
    {
        __Dialog.gameObject.SetActive(false);
        gridLayout.gameObject.SetActive(true);
        PlayerPrefs.SetInt("PrimeraVez", 1);
    }


}
