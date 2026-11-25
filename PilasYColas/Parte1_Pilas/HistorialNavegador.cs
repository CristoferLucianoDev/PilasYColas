
namespace PilasYColas.Parte1_Pilas;

/// <summary>
/// Simula el historial de navegacion de un navegador web usando pilas.
///
/// - _historial: pila con las paginas visitadas antes de la actual.
///   La cima siempre es la pagina a la que se regresa con "Atras".
/// - _adelante: pila con las paginas que se dejaron al pulsar "Atras",
///   para poder volver a ellas con "Adelante" (igual que un navegador real).
/// </summary>
public sealed class HistorialNavegador
{
    private readonly Pila<string> _historial = new();
    private readonly Pila<string> _adelante = new();

    /// <summary>Pagina que se esta mostrando en este momento.</summary>
    public string? PaginaActual { get; private set; }

    public bool PuedeRegresar => !_historial.IsEmpty;
    public bool PuedeAvanzar => !_adelante.IsEmpty;

    /// <summary>
    /// Visita una nueva pagina.
    /// La pagina actual se apila en el historial (push) y la nueva pasa a ser la actual.
    /// Al abrir una pagina nueva se pierde el camino "Adelante", como en cualquier navegador.
    /// </summary>
    public void Visitar(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("La URL no puede estar vacia.", nameof(url));

        url = url.Trim();

        // No se apila si se recarga la misma pagina
        if (url.Equals(PaginaActual, StringComparison.OrdinalIgnoreCase))
            return;

        if (PaginaActual is not null)
            _historial.Push(PaginaActual);

        PaginaActual = url;
        _adelante.Clear();
    }

    /// <summary>
    /// Regresa a la pagina anterior.
    /// Se saca la cima del historial (pop) y la pagina actual se guarda en la pila "Adelante".
    /// Devuelve false si no hay paginas anteriores.
    /// </summary>
    public bool Regresar()
    {
        if (_historial.IsEmpty)
            return false;

        _adelante.Push(PaginaActual!);
        PaginaActual = _historial.Pop();
        return true;
    }

    /// <summary>
    /// Avanza a la pagina que se dejo al regresar (operacion inversa de Regresar).
    /// </summary>
    public bool Avanzar()
    {
        if (_adelante.IsEmpty)
            return false;

        _historial.Push(PaginaActual!);
        PaginaActual = _adelante.Pop();
        return true;
    }

    /// <summary>Muestra la pagina actual y el contenido de ambas pilas.</summary>
    public void MostrarHistorial()
    {
        Console.WriteLine($"  Pagina actual : {PaginaActual ?? "(ninguna)"}");

        Console.WriteLine($"  Historial (Atras) - {_historial.Count} pagina(s), de la cima al fondo:");
        ImprimirPila(_historial.ToArray());

        Console.WriteLine($"  Adelante - {_adelante.Count} pagina(s):");
        ImprimirPila(_adelante.ToArray());
    }

    private static void ImprimirPila(string[] paginas)
    {
        if (paginas.Length == 0)
        {
            Console.WriteLine("     (vacia)");
            return;
        }

        for (int i = 0; i < paginas.Length; i++)
        {
            string marca = i == 0 ? "<- cima" : "";
            Console.WriteLine($"     {i + 1}. {paginas[i]} {marca}");
        }
    }
}
