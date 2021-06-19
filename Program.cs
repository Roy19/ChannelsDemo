using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChannelsDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var demoList = new List<IChannelStreaming>
            {
                new UningIAsyncEnumerable(),    // same as below but using IAsyncEnumerable
                new CompletingAChannel(),       // channel completes
                new UnfinishedChannel()         // channel left open for messages
            };

            foreach(var demo in demoList)
            {
                await demo.DoStreaming();
            } 

            // var myChannel = Channel.CreateUnbounded<int>();
            // var producer = new Producer(myChannel.Writer);
            // var consumer = new Consumer(myChannel.Reader);
        }
    }
}
