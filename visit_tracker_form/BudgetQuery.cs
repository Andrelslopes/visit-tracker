using System;

public class BudgetQuery
{
    public int Id { get; set; }
    public int IdVisit { get; set; }
    public int IdUser { get; set; }
    public double TotalValue { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string StatusId { get; set; }
    public DateTime BudgetDate { get; set; }

    public override string ToString()
    {
        return $"R$ {TotalValue:N2} - {StatusId}";
    }
}   