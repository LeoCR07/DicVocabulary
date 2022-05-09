using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Palabra
{
    public partial class PalabraManager : MonoBehaviour
    {
        /* Imput */
        public List<TextAsset> _dbIdioma;
        public SceneIndex _Index;
       public TextMeshProUGUI _Txt1;
      // public TextMeshProUGUI _Txt2;


        public GameObject _Prefab;



        /* Componentes */
        private TextMeshProUGUI BtnText;
        private UnityEngine.UI.Text BtnSubText;
        private BtnElements BtnID;
        private UnityEngine.UI.Image BtnImg;
        public GridLayoutGroup gridLayout;


        /* Variables */
        List<string> lstID = new List<string>();
        List<string> lstPrimeras = new List<string>();
        List<string> lstSegundas = new List<string>();
        List<Sprite> LstImg = new List<Sprite>();
    }
}