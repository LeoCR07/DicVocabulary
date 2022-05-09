using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mode8Manger : MonoBehaviour
{
    /* Puntaje */
    public Text __UScore, __BestScore;
    AudioSource audio;
    public GridLayoutGroup __GridBotones;

    public Text __TxtTime;
    public static int Contador;
    public GridLayoutGroup __GridImagens;

    /* Tiempo */
    float currentTime;
    float staringTime;


    /* Imputs */
    public AudioClip WrongClip;
    public SceneIndex __Index;
    [SerializeField] List<TextAsset> _Datos = new List<TextAsset>();
    public GameObject GameOver, Juego;

    public List<GameObject> __LstButtons = new List<GameObject>();
    public List<GameObject> __LstImg = new List<GameObject>();
    public AudioSource __ASource;

    /* Sonido, Imagen y datos */
    int x1 = 0, x2 = 34;
    string[] Lista = { "Vegetales", "Animales", "Frutas", "Trabajos", "Ropa","Casa","Transportes","Familia"
    ,"Dias","Meses","Cuerpo","Paises","bebidas","Comidas","Nacionalidades","Tecnologia","Numeros","Deportes",
    "Color","Formas","Herramientas","Edificios","Clima","Naturaleza","Espacio","Abecedario","Apariencias","Personalidad",
    "Expresiones","Cosas","Verbos","Viajes","SYD","Preguntas"};


    string path;
    int inicio, final;
    private string Palabra;
    private List<string> lstPalabra;
    private List<int> lstCategoria;
    private List<int> lstID;
    private float ModeControl;
    List<int> LstInicio;
    bool RandomMode = false;

    void Start()
    {
        /* Normal Mode or Random Mode */
        ModeControl = __Index.GetModeControl();

        InicializarVariables();
        GenerarContenido();

        size(__GridImagens, 0.48,7,0.018);
        size(__GridBotones,0.30,12,0.030);
    }

    private void size(GridLayoutGroup grid,double ancho,double altura,double spc)

    {
        double with = Screen.width;
        double high = Screen.height;

        // float space = (float) ((with / 2)-(with*0.019));
        float space = (float)(with * spc);
        float x = (float)(with *ancho);
        float y = (float)(high / altura);


        //Vector2 valor = gridLayout.cellSize;
        grid.cellSize = new Vector2(x, y);
        grid.spacing = new Vector2(space, space * 2);;
    }
    void InicializarVariables()
    {
        //Mode7E.Reinicio = false;
        staringTime = 25;
        currentTime = 0;
        Contador = 0;
        inicio = 0;
        final = 0;
        path = "";
        lstCategoria = new List<int>();
        lstPalabra = new List<string>();
        lstID = new List<int>();
        LstInicio = new List<int>();

    }

    void GenerarContenido()
    {
        /* Tiempo */
        currentTime = staringTime;

        /* Generar random para la Categoria */
        lstCategoria = GenerarCategoria();

        /* Generar random para el elemento */
        if (RandomMode)
        {
            lstID = GenerarElementos(lstCategoria);
        }
        else
        {
            lstID = GenerarElementosUnicos(lstCategoria[0]);
        }


        /* Obtencion de los textos */
        lstPalabra = GenerarTextos(lstCategoria, lstID);

        /* Obtencion de la imagen */
        GenerarImagnes(lstCategoria,lstID);

        /* Random */
        RandomTextos(lstPalabra);

        /* Botones */
        Instanciar();

    }

    private void Instanciar()
    {
        Text txt;
        ModeE8 model;
        ModeImg8 MImg;
        string word;

        for(int i = 0; i < 6; i++)
        {
            txt = __LstButtons[i].GetComponentInChildren<Text>();
            model = __LstButtons[i].GetComponent<ModeE8>();

            word = lstPalabra[i];
            txt.text = word;
            model.valor = word;
        }
    }

    private void GenerarImagnes(List<int>categoria, List<int> ID)
    {
        string direccion = "";
        Image img;
        ModeImg8 model;

        for (int i = 0; i < 6; i++)
        {
            img = __LstImg[i].GetComponent<Image>();
            model = __LstImg[i].GetComponent<ModeImg8>();

            path = Lista[categoria[i]];
            direccion = ("Imagen/" + path + "/" + ID[i]);

            if (Resources.Load<Sprite>("Imagen/" + path + "/" + ID[i]) == null)
            {
               img.sprite = Resources.Load<Sprite>("Imagen/" + path + "/" + LstInicio[i]);
            }
            else
            {
                img.sprite = Resources.Load<Sprite>(direccion);
            }

               
            model.valor = lstPalabra[i];

            //Sonido
            model.Sonido = Resources.Load<AudioClip>("Sonido 1/"+path + "/"+__Index.GetNuevo() +"/"+ ID[i]);
            //Debug.Log(("Imagen/" + path + "/" + ID[i]));
        }

    
    }

    private List<int> GenerarCategoria()
    {
        int valor = 0;
        List<int> lst = new List<int>();


        if (ModeControl == -1)
        {
            RandomMode = true;

            for (int i = 0; i < 6; i++)
            {
                valor = Random.Range(x1, x2);
                lst.Add(valor);
               // Debug.Log("ID categoria:" + valor + " P:" + i);
            }
        }
        else
        {
            RandomMode = false;
            int aux = (int)ModeControl;
            for (int i = 0; i < 6; i++)
            {
                lst.Add(aux);
            }

            
        }

      

        return lst;
    }

    public void ImagenSonido()
    {
       // clip = Resources.Load<AudioClip>("Sonido 1/" + path + "/" + __Index.IdiomaNext + "/" + IDE);
       // __ASource.PlayOneShot(clip);
    }


    void RandomTextos(List<string> lst)
    {
        int randomValue;
        string temp;

        for (int i = 0; i < lst.Count; i++)
        {
            randomValue = Random.Range(0, lst.Count);
            temp = lst[randomValue];
            lst[randomValue] = lst[i];
            lst[i] = temp;

        }
    }

    private List<int> GenerarElementosUnicos(int i)
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



        } while (valores.Count < 6);
        return valores;

    }

    private List<int> GenerarElementos(List<int> lst)
    {
        List<int> Valores = new List<int>();

        for(int i = 0; i < 6; i++)
        {
            int valor = 0;
            string[] data = Encoding.UTF7.GetString(_Datos[lst[i]].bytes).Split(new char[] { '\n' });

            string[] ei = data[1].Split(new char[] { ';' });
            inicio = int.Parse(ei[2]);
            LstInicio.Add(inicio);

            string[] ef = data[data.Length - 1].Split(new char[] { ';' });
            final = int.Parse(ef[2]);
            final++;
            valor = Random.Range(inicio, final);

            Valores.Add(valor);
            Debug.Log("ID e: " + valor + " P:" + i);
        }

        return Valores;
    }

    private List<string> GenerarTextos(List<int> catergoria, List<int> lstID) //Categoria
    {
        List<string> lst = new List<string>();
        string valor = "";
        int j = 3, random;
       
        for(int m = 0; m < __Index.CINuevo; m++)
        {
            j = j + m;
        }

        for(int i = 0; i < 6; i++)
        {
            string[] data = Encoding.UTF7.GetString(_Datos[catergoria[i]].bytes).Split(new char[] { '\n' });

            for (int k = 0; k < data.Length; k++)
            {
                string[] ei = data[k].Split(new char[] { ';' });
                if (ei[2] == lstID[i] + "")
                {
                    //el campo 0 siempre se va guardar la respuesta
                    //valor = (ei[j]);
                    lst.Add(ei[j]);
                 //   Debug.Log(ei[j]);
                }
            }

        }

       

        return lst;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        __TxtTime.text = currentTime.ToString("0");


        /* Fin del juego */
        if (currentTime <= 0f)
        {
            Juego.gameObject.SetActive(false);
            GameOver.gameObject.SetActive(true);
            ShowScores();
        }

        //Refrescar 
        if(Contador == 6)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        //Cuando hay un error
        if (ModeImg8.activado && ModeE8.activado)
        {
            if (ModeE8.ValorBotones != ModeImg8.ValorImagen)
            {
                ModeImg8.ValorImagen = "111";
                ModeImg8.activado = false;
                ModeE8.activado = false;

                ModeE8.ValorBotones = "000";
                __ASource.PlayOneShot(WrongClip);
                
               
                __Index.ScoreM8 -= 2;
                Debug.Log("Incorrecto : " + __Index.ScoreM8);
            }
        }
    }

    private void ShowScores()
    {
        float score = __Index.ScoreM8;
        float Max;
        string name;

        if (RandomMode)
        {
            name = "M8";
            Max = PlayerPrefs.GetFloat(name);
        }
        else
        {
            name = "MN8";
            Max = PlayerPrefs.GetFloat(name);
        }

        __BestScore.text = "Best Score: \n" + Max;
        __UScore.text = "You Score: \n" + score;

        if (score >= Max)
        {
            PlayerPrefs.SetFloat(name, score);
            __UScore.text = "New Record: \n" + score;
        }

    }

    public void Reincio()
    {
        audio = GetComponent<AudioSource>();
        audio.PlayOneShot(audio.clip);
        __Index.ScoreM8 = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void atras()
    {
        __Index.ScoreM8 = 0;
        SceneManager.LoadScene(5);
    }
}
