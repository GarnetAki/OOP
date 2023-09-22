using System.ComponentModel.DataAnnotations.Schema;
using MPSExceptions;

namespace DataAccess.Models;

public class PhoneMessage : Message
{
    public PhoneMessage(Guid id, MessageSource messageSource, string text, string sender)
        : base(id, messageSource)
    {
        Text = text;
        Sender = sender;
    }

#pragma warning disable CS8618
    public PhoneMessage() { }
#pragma warning restore CS8618

    public string Text { get; protected init; }

    public string Sender { get; protected init; }
}