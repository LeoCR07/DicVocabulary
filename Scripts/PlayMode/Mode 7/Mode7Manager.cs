using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Mode7Manager : MonoBehaviour
{
    //lOCAL Audio
    AudioSource audio;

    /* Imputs */
    public GridLayoutGroup __Grid;
    public SceneIndex __Index;
    public Text __UScore, __BestScore;
    [SerializeField] List<TextAsset> _Datos = new List<TextAsset>();
    public GameObject GameOver,Juego;
    public GameObject __Prefab;
    public static char Letra;
    public Image __Img;
    public Text __Categoria;
    public Text __TxtTime;
    public AudioSource __ASource;
    AudioClip clip;

    /* Variables */
    List<char> lista = new List<char>();

    /* Tiempo */
    float currentTime;
    float staringTime;

    /* Sonido, Imagen y datos */
    int x1 = 0, x2 = 30;
    string[] Lista = { "Vegetales", "Animales", "Frutas", "Trabajos", "Ropa","Casa","Transportes","Familia"
    ,"Dias","Meses","Cuerpo","Paises","bebidas","Comidas","Nacionalidades","Tecnologia","Numeros","Deportes",
    "Color","Formas","Herramientas","Edificios","Clima","Naturaleza","Espacio","Apariencias","Personalidad"
            ,"Cosas","Verbos","Viajes"};

    string path;
    private int Categoria;
    int IDE;
    int inicio, final;
    private string Palabra;
    private float ModeControl;
    bool RandomMode = false;

    void Start()
    {
        /* Normal Mode or Random Mode */
        ModeControl = __Index.GetModeControl();

        InicializarVariables(ModeControl);
        GenerarContenido();
        Size(__Grid, x1: 0.16, y1:14, spc: 0.018);
    }

    void Size(GridLayoutGroup grid, double x1, double y1, double spc)
    {
        double with = Screen.width;
        double high = Screen.height;

        // float space = (float) ((with / 2)-(with*0.019));
        float space = (float)(with * spc);
        float x = (float)(with * x1);
        float y = (float)(high / y1);

        //Vector2 valor = gridLayout.cellSize;
        grid.cellSize = new Vector2(x, y);
        grid.spacing = new Vector2(space, space);

    }



    void GenerarContenido()
    {  
        /* Tiempo */
        currentTime = staringTime;

        /* Generar random para el elemento */
        IDE = GenerarElemento(Categoria);

        /* Obtencion de la imagen */
        path = Lista[Categoria];

        //Imagen
        CompletarImagen();

        /* Sonido */
        ImagenSonido();

        /* Obtencion del texto */
        Palabra = GenerarTexto(Categoria, IDE);

        /* Alterar el orden de la palabra */
        DividirPalabra(Palabra.Trim());
        RandomTextos(lista);

        /* Creacion */
        Instanciar();

    }

    private void CompletarImagen()
    {

        if (Resources.Load<Sprite>("Imagen/" + path + "/" + IDE) == null)
        {
            //Cuando la categoria solo tiene una imagen
            Debug.Log("Imagen solitaria");
            __Img.sprite = Resources.Load<Sprite>("Imagen/" + path + "/" + inicio);
        }
        else
        {
            //Cuando no hay error
            __Img.sprite = Resources.Load<Sprite>("Imagen/" + path + "/" + IDE);
        }
    }

    public void ImagenSonido()
    {
        clip = Resources.Load<AudioClip>("Sonido 1/" + path + "/" + __Index.GetNuevo() + "/" + IDE);
        __ASource.PlayOneShot(clip);
    }

    private void DividirPalabra(string palabra)
    {
        for (int i = 0; i < palabra.Length; i++)
        {
            lista.Add(palabra[i]);
        }
    }

    private void Instanciar()
    {
        /* Propiedades */
        Text texto = __Prefab.gameObject.GetComponentInChildren<Text>();
        Mode7E.PalabraCompleta = this.Palabra;

        /* Parent */
        Transform parent = GameObject.FindGameObjectWithTag("GameController").transform;

        for(int i = 0; i < this.lista.Count; i++)
        {
            texto.text =lista[i].ToString();
            Instantiate(__Prefab, parent, false);
        }
        
    }

    void RandomTextos(List<char> lst)
    {
        int randomValue;
        char temp;

        for (int i = 0; i < lst.Count; i++)
        {
            randomValue = Random.Range(0, lst.Count);
            temp = lst[randomValue];
            lst[randomValue] = lst[i];
            lst[i] = temp;

        }
    }


    private int GenerarElemento(int i)
    {
        int valor = 0;
        string[] data = Encoding.UTF7.GetString(_Datos[i].bytes).Split(new char[] { '\n' });

        string[] ei = data[1].Split(new char[] { ';' });
        inicio = int.Parse(ei[2]);

        string[] ef = data[data.Length - 1].Split(new char[] { ';' });
        final = int.Parse(ef[2]);
        final++;
        valor = Random.Range(inicio, final);

        return valor;

    }


    private string GenerarTexto(int i,int ID) //Categoria
    {
        // j es el idioma
        string valor = "";
        int j = 3, random;
        string[] data = Encoding.UTF7.GetString(_Datos[i].bytes).Split(new char[] { '\n' });

        for(int m = 0; m < __Index.CINuevo; m++)
        {
            j = j + m;
            __Categoria.text = FindTitulo(j);
        }

        //Busca el ID que genero la imagen para guardarlo como la respuesta
        for (int k = 0; k < data.Length; k++)
        {
            string[] ei = data[k].Split(new char[] { ';' });
            if (ei[2] == ID + "")
            {
                //el campo 0 siempre se va guardar la respuesta
                valor = (ei[j]);
            }
        }

        return valor;
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

        if (Mode7E.Reinicio)
        {
            InicializarVariables(ModeControl);
            GenerarContenido();
        }


    }


    void InicializarVariables(float mode)
    {
        //Mode7E.Reinicio = false;
        IDE = 0;
        inicio = 0;
        final = 0;
        Palabra = "";
        path = "";
        staringTime = 30;
        currentTime = 0;

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

    private string FindTitulo(int i1)
    {
        string valor = "";
        string[] data = Encoding.UTF7.GetString(_Datos[Categoria].bytes).Split(new char[] { '\n' });
        string[] e = data[0].Split(new char[] { ';' });

        valor = e[i1];
        return valor;
    }

    private void ShowScores()
    {
        float score = __Index.ScoreM7;

        float Max;
        string name;

        if (RandomMode)
        {
            name = "M7";
            Max = PlayerPrefs.GetFloat(name);
        }
        else
        {
            name = "MN7";
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
        __Index.ScoreM7 = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void atras()
    {
        __Index.ScoreM7 = 0;
        SceneManager.LoadScene(5);
    }
}
