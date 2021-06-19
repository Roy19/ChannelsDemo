using System;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace ChannelsDemo
{
    public class UnfinishedChannel : IChannelStreaming
    {
        public async Task DoStreaming()
        {
            var channel = Channel.CreateUnbounded<int>();

            // some thread produces
            _ = Task.Factory.StartNew(async () => {
                for(int i = 0;i < 10;i++)
                {
                    await channel.Writer.WriteAsync(i);
                    await Task.Delay(1000);
                }
                // see what happens when the channel completes with an exception
                // myChannel.Writer.Complete(new NullReferenceException());
            });

            // main thread simultaneously consumes
            while(await channel.Reader.WaitToReadAsync())
            {
                var i = channel.Reader.ReadAsync();
                Console.WriteLine($"Received data : {i}");
            }
        }

    }
}