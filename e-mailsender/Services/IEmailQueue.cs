using e_mailsender.Models;
using System.Threading.Channels;

namespace e_mailsender.Services
{
    public interface IEmailQueue
    {
        ValueTask QueueAsync(EmailQueueItem item, CancellationToken ct = default);
        ValueTask<EmailQueueItem> DequeueAsync(CancellationToken ct);
    }

    public sealed class EmailQueue : IEmailQueue
    {
        private readonly Channel<EmailQueueItem> _channel = Channel.CreateUnbounded<EmailQueueItem>();

        public ValueTask QueueAsync(EmailQueueItem item, CancellationToken ct = default)
            => _channel.Writer.WriteAsync(item, ct);

        public ValueTask<EmailQueueItem> DequeueAsync(CancellationToken ct)
            => _channel.Reader.ReadAsync(ct);
    }
}
