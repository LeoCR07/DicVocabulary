using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 En este modo no aparecen las categorias en la cuales solo se tiene una imagen ya que hace el juego imposible
 */

public class Mode3Manager : MonoBehaviour
{
    //Audio de reinicio
    AudioSource audio;
    public Text __UScore, __BestScore;


    /* Imputs */
    /* Views */
    [SerializeField] GameObject Juego, GameOver;


    //Datos
    [SerializeField] List<TextAsset> _Datos = new List<TextAsset>();

    //Componentes
    public Text __Categoria;
    public List<Mode3E> __Botones = new List<Mode3E>();
    public SceneIndex __Index;
    public Image __Img;
    public AudioSource __ASource;
    public Text __TxtTime;

    /* Variables */
    //Tiempo
    float currentTime = 0f;
    float staringTime = 30f;

    //Sonido, Imagen y datos
    int x1 = 0, x2 = 26;
    string[] Lista = { "Vegetales", "Animales", "Frutas", "Trabajos", "Ropa","Casa","Transportes"
    ,"Cuerpo","Paises","bebidas","Comidas","Nacionalidades","Tecnologia","Numeros","Deportes",
    "Color","Formas","Herramientas","Edificios","Clima","Naturaleza","Espacio","Apariencias","Cosas",
    "Viajes"};
    string path = "";
    private int Categoria;
    int IDE;
    int inicio, final;
    //int[] valores = new int[4];
    List<int> SuperValores;
    bool RandomMode = false;
    private float ModeControl;

    /* Propiedades de componentes*/
    //Audio
    AudioClip clip;

    //Botones textos
    private List<string> lstTextos;


    void Start()
    {
        Mode3E.UScore = 0;

        /* Time */
        currentTime = staringTime;

        /* Normal Mode or Random Mode */
        ModeControl = __Index.GetModeControl();
    }

    private void CompletarTitulo()
    {
        int j = 3;

        for (int i = 0; i < __Index.CINuevo; i++)
        {
            __Categoria.text = FindTitulo(j + i);
        }

    }


    private string FindTitulo(int i1)
    {
        string valor = "";
        string[] data = Encoding.UTF7.GetString(_Datos[Categoria].bytes).Split(new char[] { '\n' });
        string[] e = data[0].Split(new char[] { ';' });

        valor = e[i1];
        return valor;
    }

    void InicializarVariables(float mode)
    {
        SuperValores = new List<int>();
        IDE = 0;
        inicio = 0;
        final = 0;
        lstTextos = new List<string>();
        __Botones[0].Answer = false;
        __Botones[1].Answer = false;

        /*Categoria*/
        if (mode == -1)
        {
            Categoria = Random.Range(x1, x2);
            RandomMode = true;
        }
        else
        {
            Categoria = (int)mode;
            RandomMode = false;
        }
    }

    public void GenerarContenido()
    {
        /* Categoria */
        CompletarTitulo();

        /* Generar random para el elemento */
        SuperValores = GenerarElemento(Categoria);
        IDE = SuperValores[0];


        /** Generar Palabra **/
        //string aux = GenerarTexto(IDE);
        //Txt.text = aux;


        //Obtener Imagenes y Palabra
        path = Lista[Categoria];
        CompletarImagen();
        //__Img.sprite = Resources.Load<Sprite>("Imagen/" + path + "/" + IDE);


        /* Fake o Real respuesta */
        int r = Random.Range(0, 2);

        Debug.Log("" + r);

        //Respuesta incorrecta 
        if (r == 1)
        {
            IDE = SuperValores[1];
        }


        /* Generar sonido */
        clip = Resources.Load<AudioClip>("Sonido 1/" + path + "/" + __Index.GetNuevo() + "/" + IDE);
        __ASource.PlayOneShot(clip);

        /*Botones*/
        GetBtnComponent(r);
    }

    private void CompletarImagen()
    {
        if (Resources.Load<Sprite>("Imagen/" + path + "/" + IDE) == null)
        {
            __Img.sprite = Resources.Load<Sprite>("Imagen/" + path + "/" + inicio);
        }
        else
        {
            //Cuando no hay error
            __Img.sprite = Resources.Load<Sprite>("Imagen/" + path + "/" + IDE);
        }

    }

    private string GenerarTexto(int i) //Categoria
    {
        // j es el idioma
        string valor = "";
        int j = 3;
        string[] data = Encoding.UTF7.GetString(_Datos[Categoria].bytes).Split(new char[] { '\n' });


        for (int m2 = 0; m2 < __Index.CINuevo; m2++)
        {
            if (__Index.GetNuevo() == m2)
            {
                j = (j + m2);
            }
        }

        //Busca el ID que genero la imagen para guardarlo como la respuesta
        for (int k = 0; k < data.Length; k++)
        {
            string[] ei = data[k].Split(new char[] { ';' });
            if (ei[2] == IDE + "")
            {
                valor = ei[j]; //Respuesta

            }
        }

        return valor;
    }

    private void GetBtnComponent(int valor)
    {
        Mode3E model;
        //string Respuesta = lstTextos[0];
        //int Respuesta = SuperValores[0];
        //RandomTextos(SuperValores);

        if(valor == 0)
        {
            __Botones[0].Answer = true;
        }
        else if(valor == 1)
        {
            __Botones[1].Answer = true;
        }

        /*
        for (int i = 0; i < __Botones.Count; i++)
        {
            model = __Botones[i].GetComponent<Mode3E>();

            //BtnTxt.text = lstTextos[i];
            //model.Texto = lstTextos[i];
            model.Texto = SuperValores[i];
            model.Answer = Respuesta;
        }*/
    }

    private List<int> GenerarElemento(int i)
    {
        List<int> valores = new List<int>();
        int valor = 0;
        string[] data = Encoding.UTF7.GetString(_Datos[i].bytes).Split(new char[] { '\n' });

        string[] ei = data[1].Split(new char[] { ';' });
        inicio = int.Parse(ei[2]);

        string[] ef = data[data.Length - 1].Split(new char[] { ';' });
        final = int.Parse(ef[2]);
        final++;
        valor = Random.Range(inicio, final);
        valores.Add(valor);


        do
        {
            int c = 0;

            valor = Random.Range(inicio, final);

            foreach (int e in valores)
            {
                if (e != valor)
                {
                    c++;
                }
            }


            if (c == valores.Count)
            {
                valores.Add(valor);
                // Debug.Log(valor + "");
            }



        } while (valores.Count < 2);
        return valores;

    }

    public void ImagenSonido()
    {
        clip = Resources.Load<AudioClip>("Sonido 1/" + path + "/" + __Index.GetNuevo() + "/" + IDE);
        __ASource.PlayOneShot(clip);
    }

    private void Update()
    {

        /* Tiempo */
        currentTime -= 1 * Time.deltaTime;
        __TxtTime.text = currentTime.ToString("0");

        /* Fin del juego */
        if (currentTime <= 0f)
        {
            Juego.active = false;
            GameOver.active = true;
            ShowScores();

        }

        /* Refrescar el contenido */        
        if (Mode3E.CarryOn)
        {
            InicializarVariables(ModeControl);
            GenerarContenido();
        }

        Mode3E.CarryOn = false;
    
    }

    private void ShowScores()
    {
        float score = Mode3E.UScore;
        float Max;
        string name;

        if (RandomMode)
        {
            name = "M3";
            Max = PlayerPrefs.GetFloat(name);
        }
        else
        {
            name = "MN3";
            Max = PlayerPrefs.GetFloat(name);
        }

        __BestScore.text = "Best Score: \n" + Max;
        __UScore.text = "You Score: \n" + score;

        if (score >= Max)
        {
            PlayerPrefs.SetFloat(name,score);
            __UScore.text = "New Record: \n" + score;
        }


    }

    public void Reincio()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
        Mode3E.UScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void atras()
    {
        Mode3E.UScore = 0;
        SceneManager.LoadScene(5);
    }
}
