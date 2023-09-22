using Backups.Interfaces;

namespace Backups.Extra.Logging;

public class FileLogger : ILogger, IDisposable
{
    private IRepository _repository;
    private Stream _stream;
    private StreamWriter _streamWriter;

    public FileLogger(IRepository repository, string path)
    {
        _repository = repository;
        _stream = _repository.OpenWrite(path);
        _streamWriter = new StreamWriter(_stream);
    }

    public void WriteLine(string line)
    {
        _streamWriter.Write(line + '\n');
        _streamWriter.Flush();
    }

    public void Dispose()
    {
        _streamWriter.Dispose();
    }
}