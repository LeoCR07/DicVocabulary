using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackManager : MonoBehaviour
{
    public SceneIndex _ControlIndex;

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Diccionario()
    {
        SceneManager.LoadScene(3);
    }

    public void CategoriaOrDiccionario()
    {
        if (_ControlIndex.SubCategoria == "0")
        {
            Diccionario();
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
