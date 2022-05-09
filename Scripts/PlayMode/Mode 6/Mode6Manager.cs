using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mode6Manager : MonoBehaviour
{
    //Audio de reinicio y puntuaje
    AudioSource audio;
    public Text __UScore, __BestScore;
    private float UScore;

    /* Imputs */
    public AudioClip WrongClip,RigthClip;
    [SerializeField] GameObject Juego, GameOver;
    [SerializeField] List<TextAsset> _Datos = new List<TextAsset>();

    public List<Image> __Vidas = new List<Image>();
    public Text __Categoria;

   
    public List<Button> __Botones = new List<Button>();
    public SceneIndex __Index;
    public AudioSource __ASource, __ASource2;
    public GridLayoutGroup __GridVidas, __GridBotones;

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
    private float ModeControl;
    bool RandomMode = false;

    void Start()
    {
        UScore = 0;

        /* Normal Mode or Random Mode */
        ModeControl = __Index.GetModeControl();

        Debug.Log("Start");
       // __Dropdown.onValueChanged.AddListener(delegate { DropDownIteamSelect(); });
        InicializarVariables(ModeControl);
        GenerarContenido();

        /**/
        Size(__GridVidas, x1: 0.07, y1: 30, spc: 0.030);
        Size(grid: __GridBotones, x1: 0.40, y1: 12.5, spc: 0.018);
    }

    void InicializarVariables(float mode)
    {
        Answer = "";
        SuperValores = new List<int>();
        IDE = 0;
        inicio = 0;
        final = 0;
        lstTextos = new List<string>();
       // __Dropdown.options.Clear();

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
        grid.spacing = new Vector2(space, 0);

    }



    public void GenerarContenido()
    {
        /* Generar random para el elemento */
        SuperValores = GenerarElemento(Categoria, 10);
        IDE = SuperValores[0];

        //Complentar Direccion
        path = Lista[Categoria];

        //Obtener las Palabras
        GenerarTextos(Categoria);

        //Rellenar Dropdown
        RandomTextos(lstTextos);
        RellenarDropDown();

        ImagenSonido();

    }

    private void RellenarDropDown()
    {
        Text txt;
        /*
        foreach (string e in lstTextos)
        {
            __Dropdown.options.Add(new Dropdown.OptionData() { text = e });
        }
        */

        for(int i = 0; i < __Botones.Count; i++)
        {
            txt = __Botones[i].GetComponentInChildren<Text>();
            txt.text = lstTextos[i];
        }
    }

    public void DropDownIteamSelect(int i1)
    {

       // int i = __Dropdown.value;

        if (lstTextos[i1] == Answer)
        {

         //   OldValue = __Dropdown.value;
            clip = RigthClip;

            UScore += 25;

            //Se refresca el juego
            RefrescarJuego();
           
        }
        else
        {
           // Debug.Log("Diferente");
            clip = WrongClip;

            UScore -= 5;
            //Se elimina una vida 
            __Vidas[ContadorMistakes].enabled = false;
            ContadorMistakes++;      
        }

        __ASource2.PlayOneShot(clip);
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


        //fUNCIONA CON DOS PERO SI SE AÑADE MAS IDIOMAS NO CREO QUE FUNCIONE
        for(int m = 0; m < __Index.CINuevo; m++)
        {
            P = P + m;
            __Categoria.text = FindTitulo(P);
        }
        P = __Index.GetNatal()+3;

        //Busca el ID que genero la imagen para guardarlo como la respuesta
        for (int k = 0; k < data.Length; k++)
        {
            string[] ei = data[k].Split(new char[] { ';' });
            if (ei[2] == IDE + "")
            {
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

        // Debug.Log("Inicio:" +inicio);
        //Debug.Log("Fin:" + final);

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

    public void ImagenSonido()
    {
        clip = Resources.Load<AudioClip>("Sonido 1/" + path + "/" + __Index.GetNuevo() + "/" + IDE);
        __ASource.PlayOneShot(clip);
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
            name = "M6";
            Max = PlayerPrefs.GetFloat(name);
        }
        else
        {
            name = "MN6";
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

    public void atras()
    {
        UScore = 0;
        SceneManager.LoadScene(5);
    }
}



