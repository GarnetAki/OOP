namespace DataAccess.Models;

public class ReportUserPart
{
    public ReportUserPart(Guid id, Guid userSource, int count)
    {
        Id = id;
        MessageSource = userSource;
        Count = count;
    }

#pragma warning disable CS8618
    public ReportUserPart() { }
#pragma warning restore CS8618

    public Guid Id { get; set; }

    public Guid MessageSource { get; set; }

    public int Count { get; set; }
}