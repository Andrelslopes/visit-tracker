using System;

public class Visits
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public string Responsavel { get; set; }
    public int IdResponsavel { get; set; }
    public DateTime DataVisita { get; set; }

    public override string ToString()
    {
        return $"{DataVisita:dd/MM/yyyy} - {Titulo}";
    }
}