using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static System.Net.Mime.MediaTypeNames;

namespace Palabra
{


    public partial class PalabraManager : MonoBehaviour
    {


        void Start()
        {
            Validar();
            size();
        }

        private void size()
        {
            double with = Screen.width;
            double high = Screen.height;

            // float space = (float) ((with / 2)-(with*0.019));
            float space = (float)(with * 0.018);
            float x = (float)(with * 0.48);
            float y = (float)(high / 5.5);


            //Vector2 valor = gridLayout.cellSize;
            gridLayout.cellSize = new Vector2(x, y);
            gridLayout.spacing = new Vector2(space,0);
        }

        private void Validar()
        {
            //ID = 2
            //{3}{4} Español,Ingles - Tablas de Palabras

            /* Validar idiomas */
            ValidarIdiomaPrincipal();
            ValidarIdiomaSegundario();

            /* Buscar el ID */
            lstID = FindList(2);

            /* Creacion */
            Instaciar();
        }

        private void ValidarIdiomaSegundario()
        {
            for(int i = 0; i < _Index.CINatal; i++)
            {
                if(_Index.GetNatal() == i)
                {
                    lstSegundas = FindList(3+i);
                }
            }
        }

        private void ValidarIdiomaPrincipal()
        {

            for(int i= 0; i< _Index.CINuevo; i++)
            {
                if(_Index.GetNuevo() == i)
                {
                    lstPrimeras = FindList(3+i);
                    _Txt1.text = FindTitulo(3+i);
                }

            }



        }

        private void Instaciar()
        {
            BtnText = _Prefab.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            BtnSubText = _Prefab.gameObject.GetComponentInChildren<UnityEngine.UI.Text>();
            BtnID = _Prefab.gameObject.GetComponent<BtnElements>();

            BtnImg = _Prefab.gameObject.GetComponentInChildren<UnityEngine.UI.Image>();
            
            LstImg.AddRange(FindImgenes());
            //Debug.Log("Cantidad :" + LstImg.Count);


            for (int i = 0; i < lstPrimeras.Count; i++)
            {
                /* Texto */
                BtnText.text = lstPrimeras[i];
                BtnSubText.text = lstSegundas[i];
                BtnID.ID = lstID[i];

                /* Imagenes */
                BtnImg.sprite = LstImg[i];

                GameObject aux = Instantiate(_Prefab);
                aux.transform.SetParent(GameObject.FindGameObjectWithTag("GameController").transform, false);
            }
        }

    }

}