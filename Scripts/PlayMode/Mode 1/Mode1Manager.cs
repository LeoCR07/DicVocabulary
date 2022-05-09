using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mode1Manager : MonoBehaviour
{

    /* Views */
    [SerializeField] GameObject Juego, GameOver;

    //Datos
    [SerializeField] List<TextAsset> _Datos = new List<TextAsset>();

    //Componentes
    public Text __UScore,__BestScore;
    public Text __Categoria;
    public List<Button> __Botones = new List<Button>();
    public SceneIndex __Index;
    public Image __Img;
    public AudioSource __ASource;
    public Text __TxtTime;

    /* Variables */
    //Tiempo
    float currentTime = 0f;
    float staringTime = 45f;

    //Audio de reinicio
    AudioSource audio;

    //Sonido, Imagen y datos
    int x1 = 0, x2 = 34;
    string[] Lista = { "Vegetales", "Animales", "Frutas", "Trabajos", "Ropa","Casa","Transportes","Familia"
    ,"Dias","Meses","Cuerpo","Paises","bebidas","Comidas","Nacionalidades","Tecnologia","Numeros","Deportes",
    "Color","Formas","Herramientas","Edificios","Clima","Naturaleza","Espacio","Abecedario","Apariencias","Personalidad",
    "Expresiones","Cosas","Verbos","Viajes","SYD","Preguntas"};


    string path = "";
    private int Categoria;
    int IDE;
    int inicio,final;
    int[] valores = new int[4];
    List<int> SuperValores;
    private float ModeControl;
    bool RandomMode = false;

    /* Propiedades de componentes*/
    //Audio
    AudioClip clip;

    //Botones textos
    private List<string> lstTextos; 


    void Start()
    {
        /* Time */
        currentTime = staringTime;

        /* Normal Mode or Random Mode */
        ModeControl = __Index.GetModeControl();

    }

    void InicializarVariables(float mode)
    {
        SuperValores = new List<int>();
        IDE = 0;
        inicio = 0;
        final = 0;
        lstTextos = new List<string>();

        /*Categoria*/
        if(mode == -1)
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
        /* Generar random para la Categoria */
        //Categoria = Random.Range(x1, x2);
        CompletarTitulo();

        /* Generar random para el elemento */
        SuperValores = GenerarElemento(Categoria);
        IDE = SuperValores[0];

        Debug.Log("El ID: " + IDE);
        //Obtener Imagenes
        path = Lista[Categoria];

        /* Imagen */
        GenerarImagen();
       
        /* Textos */
        GenerarTextos(Categoria);

        /*Botones*/
        GetBtnComponent();
    }
    private string FindTitulo(int i1)
    {
        string valor = "";
        string[] data = Encoding.UTF7.GetString(_Datos[Categoria].bytes).Split(new char[] { '\n' });
        string[] e = data[0].Split(new char[] { ';' });

        valor = e[i1];
        return valor;
    }

    private void GenerarImagen()
    {

        if (Resources.Load<Sprite>("Imagen/" + path + "/" + IDE) == null)
        {
            //Cuando la categoria solo tiene una imagen
            Debug.Log("Imagen solitaria");
            __Img.sprite = Resources.Load<Sprite>("Imagen/" + path + "/" + inicio);
        }
        else
        {
            //Cuando el id tiene su propia imagen
            __Img.sprite = Resources.Load<Sprite>("Imagen/" + path + "/" + IDE);
        }
    }

    private void CompletarTitulo()
    {
        int j = 3;

        for (int i = 0; i < __Index.CINuevo; i++)
        {
            __Categoria.text = FindTitulo(j + i);
        }

    }

    private void GetBtnComponent()
    {
        Text BtnTxt;
        Mode1E model;
        string Respuesta = lstTextos[0];
        RandomTextos(lstTextos);

        for (int i = 0; i < __Botones.Count; i++)
        {
            model = __Botones[i].GetComponent<Mode1E>();
            BtnTxt = __Botones[i].GetComponentInChildren<Text>();

          BtnTxt.text = lstTextos[i];
          model.Answer = Respuesta;
        }
    }

    private void RandomTextos(List<string>lst)
    {
        int randomValue;
        string temp;

        for(int i = 0; i < lst.Count; i++)
        {
            randomValue = Random.Range(0, lst.Count);
            temp = lst[randomValue];
            lst[randomValue] = lst[i];
            lst[i] = temp;
        }
  
    }

    private void GenerarTextos(int i) //Categoria
    {

        int j = 3;  //Idioma
        string[] data = Encoding.UTF7.GetString(_Datos[i].bytes).Split(new char[] { '\n' });

        //Saber cual es el idioma que quiere aprender 
        for(int m = 0; m < __Index.CINuevo; m++)
        {
            if (__Index.GetNuevo() == m)
            {
                j = (j + m);
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

        /* Se tiene que hacer asi porque si no funciona en, dos partes la asiganacion*/

        //2°
        //Asignos los otros ID correspondientes
        for (int k = 0; k < data.Length; k++)
        {
            string[] ei = data[k].Split(new char[] { ';' });

            for(int k2 = 1; k2 < SuperValores.Count; k2++)
            {
                if (ei[2] == SuperValores[k2] + "")
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

        string[] ef = data[data.Length-1].Split(new char[] { ';' });
        final = int.Parse(ef[2]);
        final++;
        valor = Random.Range(inicio, final);  
        valores.Add(valor);


        //Busca un 4 ID todos tienen que ser diferentes y el del campo 0 es la respuesta
        do
        {
            int c = 0;
         
            valor = Random.Range(inicio, final);

            foreach(int e in valores)
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



        } while (valores.Count<4);
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
            Juego.gameObject.SetActive(false);
            GameOver.gameObject.SetActive(true);
            ShowScores();
        }

        /* Refrescar el contenido */
        if (Mode1E.CarryOn)
        {
            //Validar si es Randon or mode normal       
            InicializarVariables(ModeControl);
            GenerarContenido();
            Debug.Log("Mode control: " + ModeControl );


        }

        Mode1E.CarryOn = false;
    }

    private void ShowScores()
    {
        float score = Mode1E.UScore;
        float Max;
        string name;

        if (RandomMode)
        {
            name = "M1";
            Max = PlayerPrefs.GetFloat(name);
        }
        else
        {
            name = "MN1";
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
        Mode1E.UScore = 0;
        SceneManager.LoadScene(5);
    }

}
