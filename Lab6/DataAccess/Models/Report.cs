using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public class Report
{
    public Report(Guid id, User user, List<ReportSourcePart> sourceParts, List<ReportUserPart> userParts)
    {
        Id = id;
        User = user;
        SourceParts = sourceParts;
        UserParts = userParts;
    }

#pragma warning disable CS8618
    public Report() { }
#pragma warning restore CS8618

    public Guid Id { get; set; }

    public virtual List<ReportSourcePart> SourceParts { get; set; }

    public virtual List<ReportUserPart> UserParts { get; set; }

    public virtual User User { get; set; }
}