using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "IndexScrip", menuName = "ScriptableObjects/Index")]
public class SceneIndex : ScriptableObject
{
    /* Control de Game */
    public int ControlGame;

    /* Control de idiomas */
    public int CINatal = 7;
    public int CINuevo = 2;

    /* Idiomas */
    public int Spain = 0;
    public int English = 1;
    public int Germany = 2;
    public int Portugees = 6;
    public int Nederlands = 3;
    public int France = 4;
    public int Italy = 5;

    /* Scores */
    public float BestScoreM2;
    public float BestScoreM3;
    public float BestScoreM4;
    public float BestScoreM5;
    public float BestScoreM6;
    public float BestScoreM7;
    public float BestScoreM8;


    /* Variables de ayuda */
    public float ScoreM8;
    public float ScoreM7;
    public float ScoreM6;


    //public int IdiomaNatal { get; set; } = 0; 
   //  public int IdiomaNext { get; set; } = 1;

    //public int IdiomaNext = PlayerPrefs.GetInt("Nuevo");
    //[SerializeField] public int IdiomaNatal = PlayerPrefs.GetInt("Natal");
   

    public string Menu { get; set; } = "0";
    
    
    /* Diccionario */
    public string Categoria { get; set; } = "0";
    public string SubCategoria { get; set; } = "0";
    public string Elemento { get; set; }






    /***********  Idioma Natal *************/
    public void SetNatal(int ID)
    {
        PlayerPrefs.SetInt("Natal", ID);
    }

    public int GetNatal()
    {
        return PlayerPrefs.GetInt("Natal");
    }

    /***********  Idioma Nuevo *************/
    public void SetNuevo(int ID)
    {
        PlayerPrefs.SetInt("Nuevo", ID);
    }

    public int GetNuevo()
    {
        return PlayerPrefs.GetInt("Nuevo");
    }

    /********* Categoria juego *****************/
    public float GetModeControl()
    {
        return PlayerPrefs.GetFloat("NormalMode");
    }

    public void SetModeControl(float valor)
    {
        PlayerPrefs.SetFloat("NormalMode",valor);
    }

    /* Compras */
    public void ComprarContenido()
    {
        // 0 false
        // 1 true
        PlayerPrefs.SetInt("Compras",1);
    }

    public void QuitarAnuncio()
    {
        // 0 false
        // 1 true
        PlayerPrefs.SetInt("ads", 1);
    }

    public bool GetComprar()
    {
        bool temp = false;

        if(PlayerPrefs.GetInt("Compras") == 1)
        {
            temp = true;
            
        }
        else
        {
            temp = false;
        }

        return temp;
    }

    public bool GetAnuncio()
    {
        bool temp = false;

        if (PlayerPrefs.GetInt("ads") == 1)
        {
            temp = true;
        }
        else
        {
            temp = false;
        }

        return temp;
    }

}


