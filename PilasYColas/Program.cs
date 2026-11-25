using System.Text;
using PilasYColas.Parte1_Pilas;
using PilasYColas.Parte2_Colas;

namespace PilasYColas;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Modo no interactivo (para generar la salida de las capturas):
        //   dotnet run -- 1      demo del navegador (Pila)
        //   dotnet run -- 2      demo del banco (Cola)
        //   dotnet run -- todos  ambas demos
        if (args.Length > 0)
        {
            string modo = args[0].ToLowerInvariant();
            if (modo is "1" or "todos") DemoNavegador();
            if (modo is "2" or "todos") DemoBanco();
            return;
        }

        string opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("====== PILAS Y COLAS ======\n");
            Console.WriteLine("1. Navegador web - Historial de navegacion (Pila)");
            Console.WriteLine("2. Banco - Atencion de clientes (Cola)");
            Console.WriteLine("3. Ver demostracion de ambos ejercicios");
            Console.WriteLine("0. Salir");
            Console.Write("\nSeleccione una opcion: ");
            opcion = Console.ReadLine()?.Trim() ?? "0";

            switch (opcion)
            {
                case "1": MenuNavegador(); break;
                case "2": MenuBanco(); break;
                case "3":
                    Console.Clear();
                    DemoNavegador();
                    DemoBanco();
                    Pausa();
                    break;
            }
        } while (opcion != "0");
    }

    // ---------------------------------------------------------------
    // PARTE I - PILAS: menu interactivo del navegador
    // ---------------------------------------------------------------
    static void MenuNavegador()
    {
        var navegador = new HistorialNavegador();
        string opcion;

        do
        {
            Console.Clear();
            Titulo("NAVEGADOR WEB - HISTORIAL (PILA)");
            Console.WriteLine($"  Pagina actual: {navegador.PaginaActual ?? "(ninguna)"}\n");
            Console.WriteLine("1. Visitar nueva pagina");
            Console.WriteLine("2. Regresar a la pagina anterior");
            Console.WriteLine("3. Avanzar");
            Console.WriteLine("4. Mostrar historial");
            Console.WriteLine("0. Volver");
            Console.Write("\nOpcion: ");
            opcion = Console.ReadLine()?.Trim() ?? "0";

            switch (opcion)
            {
                case "1":
                    Console.Write("URL: ");
                    string url = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(url))
                        Console.WriteLine("  Debe escribir una URL.");
                    else
                    {
                        navegador.Visitar(url);
                        Console.WriteLine($"  Visitando: {navegador.PaginaActual}");
                    }
                    Pausa();
                    break;
                case "2":
                    Console.WriteLine(navegador.Regresar()
                        ? $"  Regreso a: {navegador.PaginaActual}"
                        : "  No hay paginas anteriores en el historial.");
                    Pausa();
                    break;
                case "3":
                    Console.WriteLine(navegador.Avanzar()
                        ? $"  Avanzo a: {navegador.PaginaActual}"
                        : "  No hay paginas para avanzar.");
                    Pausa();
                    break;
                case "4":
                    navegador.MostrarHistorial();
                    Pausa();
                    break;
            }
        } while (opcion != "0");
    }

    // ---------------------------------------------------------------
    // PARTE II - COLAS: menu interactivo del banco
    // ---------------------------------------------------------------
    static void MenuBanco()
    {
        var banco = new SistemaAtencion();
        string opcion;

        do
        {
            Console.Clear();
            Titulo("BANCO - ATENCION DE CLIENTES (COLA)");
            banco.MostrarEnAtencion();
            Console.WriteLine($"  En espera: {banco.EnEspera}   Atendidos: {banco.Atendidos}\n");
            Console.WriteLine("1. Llega un cliente (enqueue)");
            Console.WriteLine("2. Atender siguiente cliente (dequeue)");
            Console.WriteLine("3. Finalizar atencion actual");
            Console.WriteLine("4. Listar clientes en espera");
            Console.WriteLine("5. Mostrar cliente en atencion");
            Console.WriteLine("0. Volver");
            Console.Write("\nOpcion: ");
            opcion = Console.ReadLine()?.Trim() ?? "0";

            switch (opcion)
            {
                case "1":
                    Console.Write("Nombre del cliente: ");
                    string nombre = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(nombre))
                    {
                        Console.WriteLine("  El nombre es obligatorio.");
                        Pausa();
                        break;
                    }
                    Console.Write("Operacion (deposito, retiro, prestamo...): ");
                    string operacion = Console.ReadLine() ?? string.Empty;
                    var cliente = banco.LlegaCliente(nombre,
                        string.IsNullOrWhiteSpace(operacion) ? "Consulta" : operacion);
                    Console.WriteLine($"  Turno asignado: {cliente.Codigo}");
                    Pausa();
                    break;
                case "2":
                    var siguiente = banco.AtenderSiguiente();
                    Console.WriteLine(siguiente is null
                        ? "  No hay clientes esperando."
                        : $"  Pasa a caja: {siguiente}");
                    Pausa();
                    break;
                case "3":
                    var terminado = banco.FinalizarAtencion();
                    Console.WriteLine(terminado is null
                        ? "  La caja ya estaba libre."
                        : $"  Atencion finalizada: {terminado.Codigo} {terminado.Nombre}");
                    Pausa();
                    break;
                case "4":
                    banco.ListarEspera();
                    Pausa();
                    break;
                case "5":
                    banco.MostrarEnAtencion();
                    Pausa();
                    break;
            }
        } while (opcion != "0");
    }

    // ---------------------------------------------------------------
    // Demostraciones con datos fijos
    // ---------------------------------------------------------------
    static void DemoNavegador()
    {
        Titulo("EJERCICIO 1 - NAVEGACION EN UN NAVEGADOR WEB (PILA)");
        var navegador = new HistorialNavegador();

        string[] paginas =
        [
            "https://www.google.com",
            "https://www.itla.edu.do",
            "https://www.itla.edu.do/carreras",
            "https://learn.microsoft.com/dotnet",
            "https://github.com"
        ];

        Paso("Se visitan 5 paginas (cada visita hace push de la pagina anterior)");
        foreach (var url in paginas)
        {
            navegador.Visitar(url);
            Console.WriteLine($"  Visitar -> {url}");
        }
        Console.WriteLine();
        navegador.MostrarHistorial();

        Paso("Regresar dos veces (pop del historial)");
        for (int i = 0; i < 2; i++)
        {
            navegador.Regresar();
            Console.WriteLine($"  Atras  -> {navegador.PaginaActual}");
        }
        Console.WriteLine();
        navegador.MostrarHistorial();

        Paso("Avanzar una vez (se recupera la pagina de la pila Adelante)");
        navegador.Avanzar();
        Console.WriteLine($"  Adelante -> {navegador.PaginaActual}\n");
        navegador.MostrarHistorial();

        Paso("Visitar una pagina nueva (se descarta la pila Adelante)");
        navegador.Visitar("https://stackoverflow.com");
        Console.WriteLine("  Visitar -> https://stackoverflow.com\n");
        navegador.MostrarHistorial();

        Paso("Regresar hasta el inicio y un intento extra con el historial vacio");
        while (navegador.Regresar())
            Console.WriteLine($"  Atras  -> {navegador.PaginaActual}");
        Console.WriteLine("  Atras  -> No hay paginas anteriores en el historial.\n");
        navegador.MostrarHistorial();
        Console.WriteLine();
    }

    static void DemoBanco()
    {
        Titulo("EJERCICIO 2 - ATENCION DE CLIENTES EN UN BANCO (COLA)");

        // Reloj simulado: empieza a las 09:00 y avanza 3 minutos por llegada
        var hora = new TimeOnly(9, 0);
        var banco = new SistemaAtencion(() => { var h = hora; hora = hora.AddMinutes(3); return h; });

        (string Nombre, string Operacion)[] llegadas =
        [
            ("Maria Rodriguez", "Deposito"),
            ("Jose Martinez", "Retiro"),
            ("Ana Perez", "Apertura de cuenta"),
            ("Luis Gomez", "Pago de prestamo"),
            ("Carmen Diaz", "Cambio de divisas")
        ];

        Paso("Llegan 5 clientes al banco (enqueue)");
        foreach (var (nombre, operacion) in llegadas)
        {
            var c = banco.LlegaCliente(nombre, operacion);
            Console.WriteLine($"  Llega  -> {c}");
        }
        Console.WriteLine();
        banco.MostrarEnAtencion();
        banco.ListarEspera();

        Paso("Se atiende al primer cliente (dequeue)");
        banco.AtenderSiguiente();
        banco.MostrarEnAtencion();
        banco.ListarEspera();

        Paso("Se atiende al siguiente y llega un cliente nuevo");
        banco.AtenderSiguiente();
        var nuevo = banco.LlegaCliente("Pedro Sanchez", "Consulta de saldo");
        Console.WriteLine($"  Llega  -> {nuevo}\n");
        banco.MostrarEnAtencion();
        banco.ListarEspera();

        Paso("Se atiende a todos los clientes restantes en orden de llegada");
        while (banco.AtenderSiguiente() is { } cliente)
            Console.WriteLine($"  Atendiendo -> {cliente.Codigo} {cliente.Nombre}");

        Console.WriteLine("  Atendiendo -> No hay clientes esperando.\n");
        banco.MostrarEnAtencion();
        banco.ListarEspera();
        Console.WriteLine($"  Total de clientes atendidos: {banco.Atendidos}\n");
    }

    // ---------------------------------------------------------------
    // Utilidades de consola
    // ---------------------------------------------------------------
    static void Titulo(string texto)
    {
        Console.WriteLine(new string('=', 70));
        Console.WriteLine(texto);
        Console.WriteLine(new string('=', 70));
    }

    static void Paso(string texto)
    {
        Console.WriteLine($"\n--- {texto} ---");
    }

    static void Pausa()
    {
        Console.WriteLine("\nPresione una tecla para continuar...");
        Console.ReadKey(true);
    }
}
