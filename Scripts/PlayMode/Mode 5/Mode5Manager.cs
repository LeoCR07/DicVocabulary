using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mode5Manager : MonoBehaviour
{
    /* Imputs */
    public AudioClip RightClip;
    [SerializeField] GameObject Juego, GameOver, WinGame;
    [SerializeField] List<TextAsset> _Datos = new List<TextAsset>();
    public GridLayoutGroup gridLayout;

    public Slider __Slider;
    public Text __Categoria;
    public Text __Palabra;
    public AudioSource audio;

    public List<Mode5E> __Botones = new List<Mode5E>();
    public SceneIndex __Index;
    public Image __Img;
    //public AudioSource __ASource;

    /* Variables */
    int x1 = 0, x2 = 34;
    string[] Lista = { "Vegetales", "Animales", "Frutas", "Trabajos", "Ropa","Casa","Transportes","Familia"
    ,"Dias","Meses","Cuerpo","Paises","bebidas","Comidas","Nacionalidades","Tecnologia","Numeros","Deportes",
    "Color","Formas","Herramientas","Edificios","Clima","Naturaleza","Espacio","Abecedario","Apariencias","Personalidad",
    "Expresiones","Cosas","Verbos","Viajes","SYD","Preguntas"};
    string path = "";
    private int Categoria;
    private string Palabra;
    private string Dir = "";
    int IDE;
    int inicio, final;
    List<int> SuperValores;
    public static int Answer;
    private int ValorSlider = 1;
    private float ModeControl;
    bool RandomMode = false;


    void Start()
    {
        /* Normal Mode or Random Mode */
        ModeControl = __Index.GetModeControl();

        InicializarVariables(ModeControl);
        GenerarContenido();

        /* layout */
        Size();
    }

    void Size()
    {
        double with = Screen.width;
        double high = Screen.height;

        // float space = (float) ((with / 2)-(with*0.019));
        float space = (float)(with * 0.050);
        float x = (float)(with * 0.18);
        float y = (float)(high / 16.3);


        //Vector2 valor = gridLayout.cellSize;
        gridLayout.cellSize = new Vector2(x, y);
        //double aux = (float)(with * 0.030);
        gridLayout.spacing = new Vector2(space, space);

    }

    private string FindTitulo(int i1)
    {
        string valor = "";
        string[] data = Encoding.UTF7.GetString(_Datos[Categoria].bytes).Split(new char[] { '\n' });
        string[] e = data[0].Split(new char[] { ';' });

        valor = e[i1];
        return valor;
    }


    private void Update()
    {
        if (ValorSlider == 10)
        {
            Juego.gameObject.SetActive(false);
            GameOver.gameObject.SetActive(true);
            WinGame.gameObject.SetActive(true);

        }
    }

    void InicializarVariables(float mode)
    {
        SuperValores = new List<int>();
        IDE = 0;
        inicio = 0;
        final = 0;

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
        /* Scrollbar */
        __Slider.value = ValorSlider;

        /* Generar random para el elemento */
        SuperValores = GenerarElemento(Categoria, 6);
        IDE = SuperValores[0];

        //Obtener Imagenes
        path = Lista[Categoria];
        //__Img.sprite = Resources.Load<Sprite>("Imagen/" + path + "/" + IDE);

        //Imagen
        CompletarImagen();

        //Obtener las Palabras
        GenerarTexto(Categoria);

        //Actualizar palabra
        __Palabra.text = Palabra;

        RellanarSonido();

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

    private void RellanarSonido()
    {
        Dir = ("Sonido 1/" + path + "/" + __Index.GetNuevo() + "/");
        RandomTextos(SuperValores);

        for (int i = 0; i < __Botones.Count; i++)
        {
            __Botones[i].ID = SuperValores[i];
            __Botones[i].Direccion = Dir;
        }
    }

    private void RandomTextos(List<int> lst)
    {
        /* Para que el value no caiga en el primer valor cuando se inicia*/
        int randomValue;
        int temp;

        for (int i = 0; i < lst.Count; i++)
        {
            randomValue = Random.Range(0, lst.Count);
            temp = lst[randomValue];
            lst[randomValue] = lst[i];
            lst[i] = temp;
        }

    }

    private List<int> GenerarElemento(int i, int cantidad)
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
            }

        } while (valores.Count < cantidad);
        return valores;

    }

    private void GenerarTexto(int i) //Categoria
    {
        // j es el idioma
        List<string> valores = new List<string>();
        int j = 3;
        string[] data = Encoding.UTF7.GetString(_Datos[i].bytes).Split(new char[] { '\n' });


        for(int m = 0; m < __Index.CINatal; m++)
        {
            if(m == __Index.GetNatal())
            {
                j = 3 + m;
            }
        }

        for (int m = 0; m<__Index.CINuevo; m++)
        {
            if(m == __Index.GetNuevo())
            {
                __Categoria.text = FindTitulo(m+3);
            }
        }

  
        //Busca el ID que genero la imagen para guardarlo como la respuesta
        for (int k = 0; k < data.Length; k++)
        {
            string[] ei = data[k].Split(new char[] { ';' });
            if (ei[2] == IDE + "")
            {
                //lstTextos.Add(ei[j]);    //Palabra natal
                //valores.Add(ei[P]);    //Respuesta
                Palabra = ei[j]; // Palabra natal
                //Answer = ei[P]; //Respuesta
              //  lstTextos.Add(ei[P]);  //Respuesta de todas las opciones
            }
        }
    }

    public void Validar()
    {    
        if(Answer == IDE)
        {

            ValorSlider++;
            audio.PlayOneShot(RightClip);

            InicializarVariables(ModeControl);
            GenerarContenido();
       
            Debug.Log(ValorSlider+"::");
        }
        else
        {
            /* Fin del juego */
            ValorSlider = 1;
            Juego.gameObject.SetActive(false);
            GameOver.gameObject.SetActive(true);
        }

        Answer = -1;
    }

    public void atras()
    {
        SceneManager.LoadScene(5);
    }
}
