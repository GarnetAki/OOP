using Backups.Extra.Exceptions;
using Backups.Interfaces;

namespace Backups.Extra;

public class VirtualClock : IClock
{
    private DateTime _dateTime = DateTime.Today;
    public List<IClean> ObserverList { get; } = new List<IClean>();

    public void AddObserver(IClean backupTask)
    {
        if (ObserverList.Contains(backupTask))
            throw ClockException.AddException();

        ObserverList.Add(backupTask);
    }

    public void RemoveObserver(IClean backupTask)
    {
        if (!ObserverList.Contains(backupTask))
            throw ClockException.RemoveException();

        ObserverList.Remove(backupTask);
    }

    public void NextDay()
    {
        _dateTime = _dateTime.AddDays(1);
        foreach (IClean observer in ObserverList)
        {
            observer.ClockAlarm(_dateTime);
        }
    }
}