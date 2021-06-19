using System.Threading.Channels;

namespace ChannelsDemo
{
    public class Consumer
    {
        private readonly ChannelReader<int> _channelReader;

        public Consumer(ChannelReader<int> channelReader)
        {
            _channelReader = channelReader;
        }
    }
}