using System;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace ChannelsDemo
{
    public class UnfinishedChannel : IChannelStreaming
    {
        public async Task DoStreaming()
        {
            var myChannel = Channel.CreateUnbounded<int>();

            // some thread produces
            _ = Task.Factory.StartNew(async () => {
                for(int i = 0;i < 10;i++)
                {
                    await myChannel.Writer.WriteAsync(i);
                    await Task.Delay(1000);
                }
                // see what happens when the channel completes with an exception
                // myChannel.Writer.Complete(new NullReferenceException());
            });

            // main thread simultaneously consumes
            while(await myChannel.Reader.WaitToReadAsync())
            {
                var i = myChannel.Reader.ReadAsync();
                Console.WriteLine($"Received data : {i}");
            }
        }

    }
}