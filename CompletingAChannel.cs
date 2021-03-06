using System;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace ChannelsDemo
{
    public class CompletingAChannel : IChannelStreaming
    {
        public async Task DoStreaming()
        {
            var channel = Channel.CreateUnbounded<int>();

            _ = Task.Factory.StartNew(async () => {
                // a very fast producer
                for(int i = 0;i < 10;i++)
                {
                    await channel.Writer.WriteAsync(i);
                }
                // complete the channel to indicate end of sending messages
                channel.Writer.Complete();
            });

            try {
                // a slow consumer
                while(true)
                {
                    var i = await channel.Reader.ReadAsync();
                    Console.WriteLine($"Received : {i}");
                    await Task.Delay(2000);
                }
            } catch(ChannelClosedException) 
            {
                Console.WriteLine("Channel closed as there are no new messages to send.");
            }
        }
    }
}