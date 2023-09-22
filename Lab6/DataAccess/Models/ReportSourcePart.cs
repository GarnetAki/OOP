namespace DataAccess.Models;

public class ReportSourcePart
{
    public ReportSourcePart(Guid id, Guid messageSource, int count)
    {
        Id = id;
        MessageSource = messageSource;
        Count = count;
    }

#pragma warning disable CS8618
    public ReportSourcePart() { }
#pragma warning restore CS8618

    public Guid Id { get; set; }

    public Guid MessageSource { get; set; }

    public int Count { get; set; }
}