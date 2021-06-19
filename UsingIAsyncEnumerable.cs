using System;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace ChannelsDemo
{
    public class UningIAsyncEnumerable : IChannelStreaming
    {
        public async Task DoStreaming()
        {
            var myChannel = Channel.CreateUnbounded<int>();

            _ = Task.Factory.StartNew(async () => {
                // a very fast producer
                for(int i = 0;i < 10;i++)
                {
                    await myChannel.Writer.WriteAsync(i);
                }
                // complete the channel to indicate end of sending messages
                myChannel.Writer.Complete();
            });

            await foreach(var item in myChannel.Reader.ReadAllAsync())
            {
                Console.WriteLine($"Received : {item}");
                await Task.Delay(1000);
            }
        }
    }
}