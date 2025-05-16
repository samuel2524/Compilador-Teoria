using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


Token prueba = new Token();
prueba.Clasificacion();
System.Console.WriteLine(prueba.NombreGuardar);
System.Console.WriteLine(prueba.ClaseGuardar);
prueba.MostrarInfo();
prueba.EscribirArchivo();

public class Token
{

    private string[] _clasesValidas = { "GUERRERO", "ARQUERO", "MAGO" };  //Array para verificar que el usuario si ponga clases validas, array de clases validas
    public string[] ClasesValidas
    {
        get { return _clasesValidas; }
        set { _clasesValidas = value; }    
    }

    private string[] _comandosValidos = { "CLASE", "NOMBRE","PERSONAJE" }; //array de comandos validos, para verificar que el usuario si copie en el input bien Los comandos 
    public string[] ComandosValidos
    {
        get { return _comandosValidos; }
        set{ _comandosValidos = value; }
    }


    private string _rutaArchivoInput = "input.txt"; // ruta de archivo
    public string RutaArchivoInput
    {
        get { return _rutaArchivoInput; }
        set { _rutaArchivoInput = value; }
    }
    private string _rutaArchivoOutpot = "output.txt";
    public string RutaArchivoOutpot
    {
        get { return _rutaArchivoOutpot; }
        set{ _rutaArchivoOutpot = value; }
    }

    public string NombreGuardar { get; set; } // propiedad donde se guarda el el nombre del usuario 
    public string ClaseGuardar { get; set; } // propiedad para guardar la clase que ingrese el usuario
    public int ContadorPersonaje { get; set; }
    public int ContadorNombre { get; set; }
    public int ContadorClase { get; set; }


    public Guerrero Guerrero { get; set; } // variable de tipo Guerrero para pasar metodos y atributos de la clase guerrero
    public Mago Mago { get; set; } // variable de tipo mago para pasar metodos y atributos de la clase mago
    public Arquero Arquero { get; set; } // variable de tipo arquero para pasar metodos y atributos de la clase arquero

    public Token()
    {
        // instanciamos las clases
        Guerrero = new Guerrero();
        Mago = new Mago(); 
        Arquero = new Arquero();
    }

    //metodo donde clasificamos el input del usuario y leemos el archivo linea por linea 
    //parte lexica: lo hacemos al recorrer el archivo linea por linea y separar con split
    //sintactico : control de errores 
    public void Clasificacion()
    {

        using (StreamReader sr = new StreamReader(RutaArchivoInput))
        {
            string linea;

            //lee linea por linea del archivo
            while ((linea = sr.ReadLine()) != null)
            {


                //separamos cada linea del archivo con un split :
                string[] Separar = linea.Split(":");
                string clave = Separar[0].Trim();


                // control de errores por si el usuarui en el input pone mas de una vez algun comando
                if (clave.Equals("PERSONAJE"))
                {
                    ContadorPersonaje++;
                }
                else if (clave.Equals("NOMBRE"))
                {
                    ContadorNombre++;
                }
                else if (clave.Equals("CLASE"))
                {
                    ContadorClase++;
                }

                if (ContadorPersonaje > 1)
                {
                    System.Console.WriteLine("pusiste mas de una vez PERSONAJE");
                    Environment.Exit(0);
                }
                if (ContadorNombre > 1)
                {
                    System.Console.WriteLine("pusiste mas de una vez NOMBRE");
                    Environment.Exit(0);
                }
                if (ContadorClase > 1)
                {
                    System.Console.WriteLine("pusiste mas de una vez CLASE");
                    Environment.Exit(0);
                }




                //verificamos que este bien escrito los comandos validos
                if (!ComandosValidos.Contains(clave.Trim()))
                {
                    System.Console.WriteLine("esta mal escrito un comando");
                    Environment.Exit(0);
                }

                //verificamos si en el input hay un NOMBRE y lo guardamos en una variable
                if (clave.Equals("NOMBRE"))
                {
                    NombreGuardar = Separar[1].ToUpper().Trim();
                }   
                //varificamos si en el input hay una CLASE y lo guardamos en una variable
                if (clave.Equals("CLASE"))
                {
                    ClaseGuardar = Separar[1].ToUpper().Trim();
                }
                if (!ComandosValidos.Contains(clave)) //Control de errores por si copian una clase que no existe del array de clases
                {
                    System.Console.WriteLine("Error: clase no validas");
                    Environment.Exit(0);
                }



            }


        }
        




    }

    //outpot

    public void EscribirArchivo()
    {
        using (StreamWriter sw = new StreamWriter(RutaArchivoOutpot))
        {
            sw.WriteLine("PERSONAJE");
            sw.WriteLine("NOMBRE: " + NombreGuardar);
            sw.WriteLine("CLASE: " + ClaseGuardar);
            if (ClaseGuardar.Equals("GUERRERO"))
            {
                sw.WriteLine("ATRIBUTOS: " + "\n" + "Vida: " + Guerrero.Vida + " Inteligencia: " + Guerrero.Inteligencia + " Rabia: " + Guerrero.Rabia + " Fuerza: " + Guerrero.Fuerza);
                sw.WriteLine("INVENTARIO: " + "\n" + Guerrero.Inventario[0] + "\n" + Guerrero.Inventario[1] + "\n" + Guerrero.Inventario[2]);
            }
            else if (ClaseGuardar.Equals("MAGO"))
            {
                sw.WriteLine("ATRIBUTOS: " + "\n" + "Vida: " + Mago.Vida + " Inteligencia: " + Mago.Inteligencia + " Mana: " + Mago.Mana + " Fuerza Magica: " + Mago.FuerzaMagica);
                sw.WriteLine("INVENTARIO: " + "\n" + Mago.Inventario[0] + "\n" + Mago.Inventario[1] + "\n" + Mago.Inventario[2]);
            }
            else if (ClaseGuardar.Equals("ARQUERO"))
            {
                sw.WriteLine("ATRIBUTOS: " + "\n" + "Vida: " + Arquero.Vida + " Inteligencia: " + Arquero.Inteligencia + " Precision: " + Arquero.Precision + " Velocidad: " + Arquero.Velocidad);
                sw.WriteLine("INVENTARIO: " + "\n" + Arquero.Inventario[0] + "\n" + Arquero.Inventario[1] + "\n" + Arquero.Inventario[2]);
            }
        }
    }

    // metodo para mostrar el info segun la clase ingresada por el usuario
    public void MostrarInfo()
    {
        if (ClaseGuardar.Equals("GUERRERO"))
        {
            Guerrero.MostrarAtributos();
            Guerrero.MostrarInventario();
        }
        else if (ClaseGuardar.Equals("MAGO"))
        {
            Mago.MostrarAtributos();
            Mago.MostrarInventario();
        }
        else if (ClaseGuardar.Equals("ARQUERO"))
        {
            Arquero.MostrarAtributos();
            Arquero.MostrarInventario();

        }


    }
}



// clase guerrero con su respectivo inventario y atributos

public class Guerrero
{
    private string[] _inventario = { "espada", "escudo", "pocion de vida" }; // inventario de guerrero

    public string[] Inventario
    {
        get { return _inventario; }
        set { _inventario = value; }
    }

    private static Random _rnd = new Random(); //random para que ingrese valores aleatorios a todos los atributos menos a vida 

    //Atributos de Guerrero
    private int _vida = 100;
    public int Vida
    {
        get { return _vida; }
    }

    private int _inteligencia = _rnd.Next(1, 100);
    public int Inteligencia
    {
        get { return _inteligencia; }
    }

    private int _rabia = _rnd.Next(1, 100);

    public int Rabia
    {
        get { return _rabia; }
    }

    private int _fuerza = _rnd.Next(1, 100);
    public int Fuerza
    {
        get { return _fuerza; }
    }


    // metodo para mostrar atributos de guerrero
    public void MostrarAtributos()
    {
        System.Console.WriteLine("ATRIBUTOS: ");
        System.Console.WriteLine("Vida: " + Vida + " Inteligencia: " + Inteligencia + " Rabia: " + Rabia + " Fuerza: " + Fuerza);
    }

    //metodo para mostrar inventario de guerrero

    public void MostrarInventario()
    {
        System.Console.WriteLine("INVENTARIO: ");
        foreach (string item in Inventario)
        {
            System.Console.WriteLine(item);
        }
    }

}

// clase mago con su respectivo inventario y atributos
public class Mago
{
    private string[] _inventario = { "baston magico", "libro de hechizos", "pocion de mana" }; //inventario de mago

    public string[] Inventario
    {
        get { return _inventario; }
        set { _inventario = value; }
    }

    private static Random _rnd = new Random(); // random para que ingrese valores aleatorios a todos los atributos menos a vida y a mana
    
    //Atributos de Mago
    private int _vida = 100;
    public int Vida
    {
        get { return _vida; }
    }

    private int _inteligencia = _rnd.Next(1, 100);
    public int Inteligencia
    {
        get { return _inteligencia; }
    }

    private int _mana = 100;

    public int Mana
    {
        get { return _mana; }
    }

    private int _fuerzaMagica = _rnd.Next(1, 100);
    public int FuerzaMagica
    {
        get { return _fuerzaMagica; }
    }


    //metodo para mostrar atributos de mago
    public void MostrarAtributos()
    {
        System.Console.WriteLine("ATRIBUTOS: ");
        System.Console.WriteLine("Vida: " + Vida + " Inteligencia: " + Inteligencia + " Mana: " + Mana + " Fuerza Magica: " + FuerzaMagica);
    }

    //metodo para mostrar inventario de mago
    public void MostrarInventario()
    {
        System.Console.WriteLine("INVENTARIO: ");
        foreach (string item in Inventario)
        {
            System.Console.WriteLine(item);
        }
    }

}

// clase Arquero con su respectivo inventario y atributos
public class Arquero
{
    private string[] _inventario = { "arco", "daga", "pocion de resistencia" }; // inventario de arquero

    public string[] Inventario
    {
        get { return _inventario; }
        set { _inventario = value; }
    }

    private static Random _rnd = new Random(); // random para que ingrese valores aleatorios a todos los atributos menos a vida

    //atributos de arquero
    private int _vida = 100;
    public int Vida
    {
        get { return _vida; }
    }

    private int _inteligencia = _rnd.Next(1, 100);
    public int Inteligencia
    {
        get { return _inteligencia; }
    }

    private int _precision = _rnd.Next(1, 100);

    public int Precision
    {
        get { return _precision; }
    }

    private int _velocidad = _rnd.Next(1, 100);
    public int Velocidad
    {
        get { return _velocidad; }
    }


    //metodo para mostrar atributos de arquero
    public void MostrarAtributos()
    {
        System.Console.WriteLine("ATRIBUTOS: ");
        System.Console.WriteLine("Vida: " + Vida + " Inteligencia: " + Inteligencia + " Precision: " + Precision + " Velocidad: " + Velocidad);
    }

    //metodo para mostrar inventario de arquero
    public void MostrarInventario()
    {
        System.Console.WriteLine("INVENTARIO: ");
        foreach (string item in Inventario)
        {
            System.Console.WriteLine(item);
        }
    }

}










    







    





