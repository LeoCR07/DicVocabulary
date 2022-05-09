using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mode4Manager : MonoBehaviour
{

    //Audio de reinicio
    AudioSource audio;
    public Text __UScore, __BestScore;
    private float UScore;

    /* Imputs */
    public GridLayoutGroup __GridBotones, __GridVidas;
    public AudioClip WrongClip;
    [SerializeField] GameObject Juego, GameOver;
    [SerializeField] List<TextAsset> _Datos = new List<TextAsset>();

    public List<Image> __Vidas = new List<Image>();
    public Text __Categoria;
    public Text __Palabra;
    public List<Button> __Botones = new List<Button>();
    public SceneIndex __Index;
    public Image __Img;
    public AudioSource __ASource;

    /* Variables */
    int x1 = 0, x2 = 34;
    string[] Lista = { "Vegetales", "Animales", "Frutas", "Trabajos", "Ropa","Casa","Transportes","Familia"
    ,"Dias","Meses","Cuerpo","Paises","bebidas","Comidas","Nacionalidades","Tecnologia","Numeros","Deportes",
    "Color","Formas","Herramientas","Edificios","Clima","Naturaleza","Espacio","Abecedario","Apariencias","Personalidad",
    "Expresiones","Cosas","Verbos","Viajes","SYD","Preguntas"};

    string path = "";
    private int Categoria;
    private string Palabra;
    private string Answer;
    int IDE;
    int inicio, final;
    List<int> SuperValores;
    AudioClip clip;
    private int ContadorMistakes = 0;
    private List<string> lstTextos;
    private bool CarryOn = false;
    private int OldValue = 0;
    int controlador = 0;
    bool RandomMode = false;
    private float ModeControl;

    public AudioClip ClipRigth;

    void Start()
    {

        /* Normal Mode or Random Mode */
        ModeControl = __Index.GetModeControl();

        InicializarVariables(ModeControl);
        GenerarContenido();
        Size(grid:__GridBotones,x1:0.40,y1:12.5,spc:0.018);
        Size(__GridVidas, x1: 0.07, y1: 40, spc: 0.030);
    }

    void Size(GridLayoutGroup grid,double x1,double y1,double spc)
    {
        double with = Screen.width;
        double high = Screen.height;

        // float space = (float) ((with / 2)-(with*0.019));
        float space = (float)(with * spc);
        float x = (float)(with * x1);
        float y = (float)(high / y1);

        //Vector2 valor = gridLayout.cellSize;
        grid.cellSize = new Vector2(x, y);
        grid.spacing = new Vector2(space,0);

    }



    void InicializarVariables(float mode)
    {
        Answer = "";
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

    public void GenerarContenido()
    {
        /* Generar random para el elemento */
        SuperValores = GenerarElemento(Categoria, 10);
        IDE = SuperValores[0];

        //Obtener Imagenes
        path = Lista[Categoria];

        //Categoria titulo
        CompletarTitulo();

        //Imagen
        CompletarImagen();

        //Obtener las Palabras
        GenerarTextos(Categoria);
       // Palabra = lstTextos[0];

        //Actualizar Palabra natal
        __Palabra.text = Palabra;

        //Rellenar Dropdown
        RandomTextos(lstTextos);

        RellenarBotones();
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

    private void CompletarTitulo()
    {
        int j = 3;

        for (int i = 0; i < __Index.CINuevo; i++)
        {
            __Categoria.text = FindTitulo(j + i);
        }

    }

    private void RellenarBotones()
    {
        Text texto;
        for(int i = 0;i<lstTextos.Count;i++)
        {
            texto = __Botones[i].GetComponentInChildren<Text>();
            texto.text = lstTextos[i];
        }
    }

    public void ClickValidar(int i)
    {
        Text texto = __Botones[i].GetComponentInChildren<Text>();
        Debug.Log("1: " + texto.text + "  2:" + Answer);

        if(texto.text == Answer)
        {
            UScore += 5;
            clip = ClipRigth;
            RefrescarJuego();
          
        }
        else
        {
            // Debug.Log("Diferente");
            clip = WrongClip;
            //Se elimina una vida 
            __Vidas[ContadorMistakes].enabled = false;
            ContadorMistakes++;
        }
        __ASource.PlayOneShot(clip);
    }

    private void RandomTextos(List<string> lst)
    {
        /* Para que el value no caiga en el primer valor cuando se inicia*/
 
        bool valor = false; 

        int randomValue;
        string temp;

        if (controlador == 0)
        {
            string aux = lst[0];
            lst[0] = lst[1];
            lst[1] = aux;

            for (int i = 1; i < lst.Count; i++)
            {
                randomValue = Random.Range(1, lst.Count);
                temp = lst[randomValue];
                lst[randomValue] = lst[i];
                lst[i] = temp;

            }
        }

        if (controlador > 0)
        {
            do
            {
                valor = false;
                for (int i = 0; i < lst.Count; i++)
                {
                    randomValue = Random.Range(0, lst.Count);
                    temp = lst[randomValue];
                    lst[randomValue] = lst[i];
                    lst[i] = temp;
                }

                for (int i = 0; i < lst.Count; i++)
                {
                    if (OldValue == i)
                    {
                        if (Answer == lst[i])
                        {
                            valor = true;
                            Debug.Log("Genero Cambio");

                        }
                    }
                }
            } while (valor);
        }
        /*
        
        */
        controlador++;

    }

    private void GenerarTextos(int i) //Categoria
    {
        // j es el idioma
        List<string> valores = new List<string>();
        int j = 3, P =3;
        string[] data = Encoding.UTF7.GetString(_Datos[i].bytes).Split(new char[] { '\n' });

        for(int m1 = 0; m1 < __Index.CINatal; m1++)
        {
            if(__Index.GetNatal() == m1)
            {
                j = (j + m1);
            }
        }

        for (int m2 = 0; m2 < __Index.CINuevo; m2++)
        {
            if (__Index.GetNuevo() == m2)
            {
                P = (P + m2);
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
                Answer = ei[P]; //Respuesta
                lstTextos.Add(ei[P]);  //Respuesta de todas las opciones
            }
        }

        for (int k = 0; k < data.Length; k++)
        {
            string[] ei = data[k].Split(new char[] { ';' });

            for (int m = 1; m < SuperValores.Count; m++)
            {
                if(ei[2] == SuperValores[m] + "")
                {
                    lstTextos.Add(ei[P]);
                }
            }
        }

    }

    private List<int> GenerarElemento(int i,int cantidad)
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

        for(int j = 1; j < cantidad; j++)
        {
            valor = Random.Range(inicio, final);
            valores.Add(valor);
        }
       

        /*
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

        */
        return valores;

    }

    void RefrescarJuego()
    {
        InicializarVariables(ModeControl);
        GenerarContenido();
    }

    private void Update()
    {

        /* Fin del juego */
        if(ContadorMistakes == 3)
        {
            Juego.gameObject.SetActive(false);
            GameOver.gameObject.SetActive(true);
            ShowScores();
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
        float score = UScore;
        float Max;
        string name;

        if (RandomMode)
        {
            name = "M4";
            Max = PlayerPrefs.GetFloat(name);
        }
        else
        {
            name = "MN4";
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
        UScore = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

  

