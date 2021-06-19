using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChannelsDemo
{
    public class BoundedChannel : IChannelStreaming
    {
        public async Task DoStreaming()
        {
            var channelOptions = new BoundedChannelOptions(5)
            {
                // wait for writing if the channel is full
                FullMode = BoundedChannelFullMode.Wait
            };

            var channel = Channel.CreateBounded<int>(channelOptions);

            _ = Task.Factory.StartNew(async () => {
                // a very fast writer
                for(int i = 0;i < 10;i++)
                {
                    // Console.WriteLine("Writing to channel");
                    await channel.Writer.WriteAsync(i);
                }
                channel.Writer.Complete();
            });

            // a very slow reader
            await foreach(var item in channel.Reader.ReadAllAsync())
            {
                Console.WriteLine($"Received : {item}");
                await Task.Delay(1000);
            }
        }
    }
}