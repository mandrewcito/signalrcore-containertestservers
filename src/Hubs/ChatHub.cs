namespace SignalRSample.Hubs
{
	using System;
	using System.Threading;
	using System.Threading.Channels;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.SignalR;

	public class ChatHub : Hub
    {
        public ChannelReader<int> Counter(
        int count,
        int delay,
        CancellationToken cancellationToken)
        {
            var channel = Channel.CreateUnbounded<int>();

            // We don't want to await WriteItemsAsync, otherwise we'd end up waiting 
            // for all the items to be written before returning the channel back to
            // the client.
            _ = WriteItemsAsync(channel.Writer, count, delay, cancellationToken);

            return channel.Reader;
        }
        private async Task WriteItemsAsync(
            ChannelWriter<int> writer,
            int count,
            int delay,
            CancellationToken cancellationToken)
        {
            Exception localException = null;
            try
            {
                for (var i = 0; i < count; i++)
                {
                    await writer.WriteAsync(i, cancellationToken);

                    // Use the cancellationToken in other APIs that accept cancellation
                    // tokens so the cancellation can flow down to them.
                    await Task.Delay(delay, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                localException = ex;
            }

            writer.Complete(localException);
        }
        public async Task UploadStream(ChannelReader<string> stream)
        {
            while (await stream.WaitToReadAsync())
            {
                while (stream.TryRead(out var item))
                {
                    // do something with the stream item
                    Console.WriteLine(item);
                }
            }
        }
        
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public async Task ThrowException(string message)
        {
            await Clients.All.SendAsync("ThrowExceptionCall", message);
            throw new Exception();                 
        }
        
        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine(exception);
            return base.OnDisconnectedAsync(exception);
        }

        public async void DisconnectMe()
        {
	        await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} Disconnected");
            Context.Abort();
        }
    }
}