using Isu.Extra.Models;
using Isu.Models;

namespace Isu.Extra.Entities;

public class IsuGroupProector
{
    public IsuGroupProector(GroupName groupName, Schedule schedule)
    {
        GroupName = groupName ?? throw new ArgumentNullException();
        Schedule = schedule ?? throw new ArgumentNullException();
    }

    public GroupName GroupName { get; }

    public Schedule Schedule { get; set; }
}