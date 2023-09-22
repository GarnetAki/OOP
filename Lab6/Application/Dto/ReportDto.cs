using DataAccess.Models;

namespace Application.Dto;

public record ReportDto(Guid id, Guid userId, IReadOnlyCollection<ReportSourcePart> sourceParts, IReadOnlyCollection<ReportUserPart> userParts);