using System.Runtime.CompilerServices;
using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var isuService = new IsuService();
        var groupName = new GroupName("I23004");
        Group newGroup = isuService.AddGroup(groupName);
        Student newStudent = isuService.AddStudent(newGroup, "Vasiliev Vasiliy Vasilievich");
        Assert.True(newStudent.IsuGroup.Equals(newGroup));
        Assert.Contains(newStudent, newGroup.Students);
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var isuService = new IsuService();
        var groupName = new GroupName("I23004");
        Group newGroup = isuService.AddGroup(groupName);
        for (int i = 0; i < 30; ++i)
        {
            Student newStudent = isuService.AddStudent(newGroup, "Vasiliev Vasiliy Vasilievich");
        }

        GroupIsFullException exception = Assert.Throws<GroupIsFullException>(() => isuService.AddStudent(newGroup, "Vasiliev Vasiliy Vasilievich"));
    }

    [Theory]
    [InlineData("NN203044")]
    [InlineData("N222222")]
    [InlineData("31241")]
    [InlineData("GF2I3G")]
    [InlineData("N12349")]
    [InlineData("E43210")]
    [InlineData("212343")]
    [InlineData("%12343")]
    public void CreateGroupWithInvalidName_ThrowException(string groupName)
    {
        Assert.Throws<GroupNameFormatException>(() => new GroupName(groupName));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var isuService = new IsuService();
        var groupName = new GroupName("I23004");
        var groupName2 = new GroupName("I23005");

        Group oldGroup = isuService.AddGroup(groupName);
        Group newGroup = isuService.AddGroup(groupName2);
        Student newStudent = isuService.AddStudent(oldGroup, "Vasiliev Vasiliy Vasilievich");
        int idStudent = newStudent.Id.Id;
        isuService.ChangeStudentGroup(newStudent, newGroup);

        Assert.True(isuService.GetStudent(idStudent).IsuGroup.Equals(newGroup));
        Assert.DoesNotContain(newStudent, oldGroup.Students);
        Assert.Contains(isuService.GetStudent(idStudent), newGroup.Students);
    }

    [Fact]
    public void TransferStudentToSameGroup_ThrowException()
    {
        var isuService = new IsuService();
        var groupName = new GroupName("I23004");

        Group newGroup = isuService.AddGroup(groupName);
        Student newStudent = isuService.AddStudent(newGroup, "Vasiliev Vasiliy Vasilievich");
        Assert.Throws<TheSameGroupException>(() => isuService.ChangeStudentGroup(newStudent, newGroup));
    }

    [Theory]
    [InlineData("VA DfFv Dsd")]
    [InlineData("wed qwqf ewf ew f")]
    [InlineData("22 gsd")]
    [InlineData("")]
    [InlineData("    ")]
    public void CreateStudentWithInvalidName_ThrowException(string studentName)
    {
        Assert.Throws<NameIsNotCorrectException>(() => new StudentName(studentName));
    }
}