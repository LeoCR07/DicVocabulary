using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Palabra
{
    public partial class PalabraManager : MonoBehaviour
    {
       
        private List<string> FindList(int i)
        {
            List<string> lst = new List<string>();

            int ID = Int32.Parse(_Index.Categoria);

            String[] data = Encoding.UTF7.GetString(_dbIdioma[ID].bytes).Split(new char[] { '\n' });

            for (int j = 1; j < data.Length; j++)
            {

                string[] e = data[j].Split(new char[] { ';' });

                if (_Index.SubCategoria == e[1])
                {
                    lst.Add(e[i]);
                    Debug.Log(e[i]);
                }
            }
            return lst;
        }

        private string FindTitulo(int i1)
        {
            string valor = "";
            int ID = Int32.Parse(_Index.Categoria);
            String[] data = Encoding.UTF7.GetString(_dbIdioma[ID].bytes).Split(new char[] { '\n' });
            string[] e = data[0].Split(new char[] { ';' });

            valor = e[i1];
            return valor;
        }
       

        private List<Sprite> FindImgenes()
        {
            List<Sprite> lst = new List<Sprite>();
            List<Sprite> lstAux = new List<Sprite>();
            List<string> lstName = new List<string>();
            List<int> lstNumber = new List<int>();

            //lst.AddRange(Resources.LoadAll<Sprite>("Imagenes/Vegetales"));
            //Traer Todas las ID de las imagenes segun su CATEGORIA
            lst.AddRange(FindLista());

            //Una List<Sprite> la propiedad de nombre se le asigana a una List<int>
            for (int i = 0; i < lst.Count; i++)
            {
                lstNumber.Add(Int32.Parse(lst[i].name));
            }

            //Para poder lo ordenan de Menor a Mayor
            lstNumber.Sort();

            //Esa List<int> ya ordenada la guardo en una List<string>
            foreach (int e in lstNumber)
            {
                lstName.Add(""+e);
               
            }

            //Se ordena la lista de Sprite en base a lstNumber
            //filtrando solo las ID que contiene la categoria     
            for(int k = 0; k < lstID.Count; k++)
            {
                for (int i = 0; i < lstNumber.Count; i++)
                {
                    if (lstID[k] == lstName[i])
                    {
                        for (int j = 0; j < lstNumber.Count; j++)
                        {
                            if (lstName[i] == lst[j].name)
                            {
                               // Debug.Log("Id:: "+lst[j]);
                                lstAux.Add(lst[j]);
                            }
                        }
                    }
                }      
            }


            /* Excepción */
            if(lstAux.Count == 1)
            {
                
               for(int i = 1; i < lstID.Count; i++)
                {
                    lstAux.Add(lstAux[0]);
                }
            }

            return lstAux;
        }

        private List<Sprite> FindLista()
        {
            List<Sprite> lst = new List<Sprite>();
            string ID = (_Index.Categoria);

            if (ID == "0")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Vegetales"));
            }
            else if (ID == "1")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Animales"));


            }
            else if (ID == "2")
            {
                // lst.AddRange(_Img.Frutas);
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Frutas"));
            }
            else if (ID == "3")
            {
                // lst.AddRange(_Img.Trabajos);
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Trabajos"));

            }
            else if (ID == "4")
            {
                //  lst.AddRange(_Img.Ropa);
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Ropa"));

            }
            else if (ID == "5")
            {

                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Casa"));
            }
            else if (ID == "6")
            {

                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Transportes"));
            }
            else if (ID == "7")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Familia"));

            }
            else if (ID == "8")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Dias"));

            }
            else if (ID == "9")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Meses"));

            }
            else if (ID == "10")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Cuerpo"));
            }
            else if (ID == "11")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Paises"));
            }
            else if (ID == "12")
            {
                //lst.AddRange(_Img.Bebidas);
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Bebidas"));
            }
            else if (ID == "13")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Comidas"));
            }
            else if (ID == "14")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Nacionalidades"));
            }
            else if (ID == "15")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Tecnologia"));
            }
            else if (ID == "16")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Numeros"));
            }
            else if (ID == "17")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Deportes"));
            }
            else if (ID == "18")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Color"));
            }
            else if (ID == "19")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Formas"));
            }
            else if (ID == "20")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Herramientas"));
            }
            else if (ID == "21")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Edificios"));
            }
            else if (ID == "22")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Clima"));
            }
            else if (ID == "23")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Naturaleza"));
            }
            else if (ID == "24")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Espacio"));
            }
            else if (ID == "25")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Abecedario"));
            }
            else if (ID == "26")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Apariencias"));
            }
            else if (ID == "27")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Personalidad"));
            }       
            else if (ID == "28")
            {
               lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Expresiones"));
            }
            else if (ID == "29")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Cosas"));
            }
            else if (ID == "30")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Verbos"));
            }

            else if (ID == "31")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Viajes"));
            }
            else if (ID == "32")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/SYD"));
            }
         
            else if (ID == "33")
            {
                lst.AddRange(Resources.LoadAll<Sprite>("Imagen/Preguntas"));
            }
           

                return lst;
        }

    }
}