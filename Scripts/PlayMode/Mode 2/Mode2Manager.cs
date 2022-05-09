using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mode2Manager : MonoBehaviour
{

    //Audio de reinicio
    AudioSource audio;

    /* Imputs */
    public Text __UScore, __BestScore;
    [SerializeField] GameObject Juego, GameOver;
    [SerializeField] List<TextAsset> _Datos = new List<TextAsset>();
    public List<Button> __Botones = new List<Button>();
    public SceneIndex __Index;
    public AudioSource __ASource;
    public Text __TxtTime;

    /* Propiedades de componentes */
    public Text __Categoria;
    AudioClip clip;
    private List<string> lstTextos;

    /* Tiempo */
    float currentTime = 0f;
    float staringTime = 25f;

    /* Sonido, Imagen y datos */
    int x1 = 0, x2 = 34;
    string[] Lista = { "Vegetales", "Animales", "Frutas", "Trabajos", "Ropa","Casa","Transportes","Familia"
    ,"Dias","Meses","Cuerpo","Paises","bebidas","Comidas","Nacionalidades","Tecnologia","Numeros","Deportes",
    "Color","Formas","Herramientas","Edificios","Clima","Naturaleza","Espacio","Abecedario","Apariencias","Personalidad",
    "Expresiones","Cosas","Verbos","Viajes","SYD","Preguntas"};
    string path = "";
    private int Categoria;
    int IDE;
    int inicio, final;
    List<int> SuperValores;
    bool RandomMode = false;
    private float ModeControl;


    // Start is called before the first frame update
    void Start()
    {

        Mode2E.UScore = 0;
        /* Tiempo */
        currentTime = staringTime;

        /* Normal Mode or Random Mode */
        ModeControl = __Index.GetModeControl();
    }

    // Update is called once per frame
    void Update()
    {
        /* Cronometro */
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
        if (Mode2E.CarryOn)
        {
            InicializarVariables(ModeControl);
            GenerarContenido();
        }

        Mode2E.CarryOn = false;
    

}

    void InicializarVariables(float mode)
    {
        SuperValores = new List<int>();
        IDE = 0;
        inicio = 0;
        final = 0;
        lstTextos = new List<string>();

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


    private void GetBtnComponent()
    {
        Text BtnTxt;
        Mode2E model;
        string Respuesta = lstTextos[0];
        
        RandomTextos(lstTextos);

        for (int i = 0; i < __Botones.Count; i++)
        {
            model = __Botones[i].GetComponent<Mode2E>();
            BtnTxt = __Botones[i].GetComponentInChildren<Text>();

            BtnTxt.text = lstTextos[i];
            model.Answer = Respuesta;
        }
    }

    private void RandomTextos(List<string> lst)
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

    private void GenerarTextos(int i) //Categoria
    {
        // j es el idioma
        int j = 3;
        string[] data = Encoding.UTF7.GetString(_Datos[i].bytes).Split(new char[] { '\n' });

        //Busca el idioma natal
      
        for(int v = 0; v < __Index.CINatal; v++)
        {
            if (__Index.GetNatal() == v)
            {
                j = (j + v);
                __Categoria.text = FindTitulo(j);
            }
        }

        //1°
        //Busca el ID que genero la imagen para guardarlo como la respuesta
        for (int k = 0; k < data.Length; k++)
        {
            string[] ei = data[k].Split(new char[] { ';' });
            if (ei[2] == IDE + "")
            {
                //el campo 0 siempre se va guardar la respuesta
                lstTextos.Add(ei[j]);
            }
        }

        //2°
        for (int k = 0; k < data.Length; k++)
        {
            string[] ei = data[k].Split(new char[] { ';' });

            for(int m = 1; m < SuperValores.Count; m++)
            {
                if (ei[2] == SuperValores[m] + "")
                {
                    lstTextos.Add(ei[j]);
                }
            }
        }
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
        valores.Add(valor);  //Valor 0

        //Completa los ID a 4 elementos donde todos sean diferentes en el campo 0 se guarda la respuesta
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
            }

        } while (valores.Count < 4);
        return valores;

    }

    public void GenerarContenido()
    {
        /* Titulo */
        CompletarTitulo();

        /* Generar random para la Categoria */
        //Categoria = Random.Range(x1, x2);

        /* Generar random para el elemento */
        SuperValores = GenerarElemento(Categoria);
        IDE = SuperValores[0];

        path = Lista[Categoria];

        clip = Resources.Load<AudioClip>("Sonido 1/" + path + "/" + __Index.GetNuevo()+ "/" + IDE);
        __ASource.PlayOneShot(clip);

        /* Textos */
        GenerarTextos(Categoria);

        /* Botones */
        GetBtnComponent();
        
    }

    private void CompletarTitulo()
    {
        int j = 3;

        for (int i = 0; i < __Index.CINuevo; i++)
        {
            __Categoria.text = FindTitulo(j + i);
        }

    }


    public void ImagenSonido()
    {
        clip = Resources.Load<AudioClip>("Sonido 1/" + path + "/" + __Index.GetNuevo() + "/" + IDE);
        __ASource.PlayOneShot(clip);
        Debug.Log("Hola");
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
        float Max;
        string name;
        float score = Mode2E.UScore;

        if (RandomMode)
        {
            name = "M2";
            Max = PlayerPrefs.GetFloat(name);
        }
        else
        {
            name = "MN2";
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
        Mode1E.UScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void atras()
    {
        Mode2E.UScore = 0;
        SceneManager.LoadScene(5);
    }
}
