using Backups.Entities;
using Backups.Interfaces;

namespace Backups.Extra;

public interface IClean
{
    void ClockAlarm(DateTime dateTime);
}