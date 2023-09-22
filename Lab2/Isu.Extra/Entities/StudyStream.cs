using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class StudyStream
{
    private List<ElectiveCourseGroup> _electiveCourseGroups;
    public StudyStream(int streamNumber, Course course)
    {
        if (course == null)
            throw new ArgumentNullException();

        StreamNumber = streamNumber;
        course.AddStudyStream(this);
        _electiveCourseGroups = new List<ElectiveCourseGroup>();
    }

    public IReadOnlyList<ElectiveCourseGroup> ElectiveCourseGroups => _electiveCourseGroups;

    public int StreamNumber { get; }

    public void AddGroup(ElectiveCourseGroup group)
    {
        if (group == null)
            throw new ArgumentNullException();

        ValidateStream(group);
        _electiveCourseGroups.Add(group);
    }

    private void ValidateStream(ElectiveCourseGroup group)
    {
        if (group == null)
            throw new ArgumentNullException();

        if (ElectiveCourseGroups.Any(gr => gr.ElectiveCourseGroupName.Stream != StreamNumber))
            throw StudyStreamException.CreateStudyStreamsIsEmpty();

        if (ElectiveCourseGroups.Any(gr => ElectiveCourseGroups.Where(cGroup => !cGroup.Equals(group)).
                All(cGr => cGr.ElectiveCourseGroupName.Number != group.ElectiveCourseGroupName.Number)))
            throw StudyStreamException.CreateStudyStreamNumbersRepeats();
    }
}