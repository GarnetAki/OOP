using System.ComponentModel.DataAnnotations.Schema;
using MPSExceptions;

namespace DataAccess.Models;

public class EmailMessage : Message
{
    public EmailMessage(Guid id, MessageSource messageSource, string title, string text, string sender)
        : base(id, messageSource)
    {
        Title = title;
        Text = text;
        Sender = sender;
    }

#pragma warning disable CS8618
    public EmailMessage() { }
#pragma warning restore CS8618

    public string Title { get; protected init; }

    public string Text { get; protected init; }

    public string Sender { get; protected init; }
}