using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ManagerMode : MonoBehaviour { 

#if UNITY_IOS
     string Idgame  = "4648481";
     string video = "Interstitial_iOS";
     string rewarded = "Rewarded_iOS";
     string banner = "Banner_iOS";
#elif UNITY_ANDROID
    string Idgame = "4648480";
    string video = "Interstitial_Android";
    string rewarded = "Rewarded_Android";
    string banner = "Banner_Android";
#endif

    /**  Variables, Canvas y Datos **/
    [SerializeField] TextAsset _Dato;
    public List<Button> __Botones;
    public List<GameObject> __Item;
    public SceneIndex __Index;
    public GameObject DialogCategoria;
    public GameObject Dialog;
    public GameObject Game;
    public AudioClip clip;
    public AudioSource __AudioSource;
    public ScrollRect scrollRect;

    private int ID;
    public GridLayoutGroup gridLayout;

    // Start is called before the first frame update
    void Start()
    {
        /** Canvas **/
        SwitchValues(dialog:false,scroll: true, interactable:true, DialogCat: false);

        /** Ad **/
      //  Advertisement.Initialize(Idgame);
       // Advertisement.AddListener(this);

        /** Normal Dialog **/
        //RenameTxt();

        size();
    }

    private void size()
    {
        double with = Screen.width;
        double high = Screen.height;

        // float space = (float) ((with / 2)-(with*0.019));
        float space = (float)(with * 0.018);
        float x = (float)(with * 0.48);
        float y = (float)(high / 4.5);


        //Vector2 valor = gridLayout.cellSize;
        gridLayout.cellSize = new Vector2(x, y);
        gridLayout.spacing = new Vector2(space,space);
    }

    /* Ir al menu principal */
    public void Cerrar()
    {
        SceneManager.LoadScene(0);
    }

    /** On|| off Canvas **/
    void SwitchValues(bool dialog, bool scroll, bool interactable, bool DialogCat)
    {
        scrollRect.enabled = scroll;
        Dialog.SetActive(dialog);
        DialogCategoria.SetActive(DialogCat);

        foreach (var e in __Botones)
        {
            e.interactable = interactable;
        }
    }

    /**  Cerrar cualquier dialog **/
    public void Atras()
    {
        __AudioSource.PlayOneShot(clip);
        SwitchValues(dialog: false, scroll: true, interactable: true, DialogCat: false);
    }

    /** Abrir el  Mode Dialog **/
    public void OpenDialogOne(int IDGame)
    {
        __Index.SetModeControl(-1);
           ID = IDGame;
        //   SwitchValues(dialog: true, scroll:false, interactable: false, DialogCat: false);
        RewardedAction();
    }

    /** On Click Random **/
    public void ClickRandoMode()
    {
        /*
        __Index.SetModeControl(-1);
        if (Advertisement.IsReady(rewarded))
        {
            Advertisement.Show(rewarded);
        }
        else
        {
            Debug.Log("Rewarded is not ready");
        }*/

    }

    /** Action **/
    public void RewardedAction()
    {
        Debug.Log("El id es: " + ID);

        /** Cambio de scene **/
        if (ID == 0)
        {
            SceneManager.LoadScene(7);
        }
        else if (ID == 1)
        {

            SceneManager.LoadScene(8);
        }
        else if (ID == 2)
        {

            SceneManager.LoadScene(9);
        }
        else if (ID == 3)
        {

            SceneManager.LoadScene(10);
        }
        else if (ID == 4)
        {
            SceneManager.LoadScene(11);
        }
        else if (ID == 5)
        {
            SceneManager.LoadScene(12);
        }
        else if (ID == 6)
        {
            SceneManager.LoadScene(13);
        }
        else if (ID == 7)
        {
            SceneManager.LoadScene(14);
        }


    }

    /** Abrir el Normal Dialog **/
    public void OpenNormalMode()
    {
        SwitchValues(dialog: false, scroll: false, interactable: false, DialogCat: true);
    }

    /** On Click Normal **/
    public void ClickNormalMode(int i)
    {
        Debug.Log("El Id Game : " + ID);
        Debug.Log("ID Categoria: " + i);

        /** Guardar Categoria **/
        __Index.SetModeControl(i);

        /** Abrir Scene **/
        if (ID == 0)
        {
            SceneManager.LoadScene(7);
        }
        else if (ID == 1)
        {
            SceneManager.LoadScene(8);
        }
        else if (ID == 2)
        {

            SceneManager.LoadScene(9);
        }
        else if (ID == 3)
        {

            SceneManager.LoadScene(10);
        }
        else if (ID == 4)
        {
            SceneManager.LoadScene(11);
        }
        else if (ID == 5)
        {
            SceneManager.LoadScene(12);
        }
        else if (ID == 6)
        {
            SceneManager.LoadScene(13);
        }
        else if (ID == 7)
        {
            SceneManager.LoadScene(14);
        }

    }

    /** Asingar Text de acuerdo al idioma **/
    private void RenameTxt()
    {
        Text txt;
        List<string> lst = SeleccionarIdioma();

        for (int i = 0; i < lst.Count; i++)
        {
            txt = __Item[i].GetComponentInChildren<Text>();
            txt.text = lst[i];
        }

    }

    /** Crear una lista del idioma **/
    private List<string> SeleccionarIdioma()
    {
        List<string> lst = new List<string>();
        int j = 1;
        string[] data = Encoding.UTF7.GetString(_Dato.bytes).Split(new char[] { '\n' });

        for (int i = 0; i < __Index.CINuevo; i++)
        {
            if (i == __Index.GetNuevo())
            {
                j = j + i;
            }
        }

        for (int k = 1; k < data.Length; k++)
        {
            string[] e = data[k].Split(new char[] { ';' });
            lst.Add(e[j]);
        }

        return lst;
    }

}
