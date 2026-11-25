namespace PilasYColas.Parte2_Colas;

/// <summary>
/// Cliente que llega al banco.
/// El turno se asigna en orden de llegada y la hora se registra al entrar a la cola.
/// </summary>
public sealed record Cliente(int Turno, string Nombre, string Operacion, TimeOnly HoraLlegada)
{
    public string Codigo => $"T-{Turno:D3}";

    public override string ToString() =>
        $"{Codigo}  {Nombre,-20} {Operacion,-20} llego {HoraLlegada:HH:mm}";
}
