namespace Banks.Models;

public class Transfer
{
    private List<TransferPart> _parts;

    public Transfer(Guid id, List<TransferPart> parts)
    {
        Id = id;
        _parts = parts;
    }

    public Guid Id { get; }

    public IReadOnlyList<TransferPart> Parts => _parts;
}