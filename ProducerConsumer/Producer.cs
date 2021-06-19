using System.Threading.Channels;

namespace ChannelsDemo
{
    public class Producer
    {
        private readonly ChannelWriter<int> _channelWriter;

        public Producer(ChannelWriter<int> channelWriter)
        {
            _channelWriter = channelWriter;
        }
    }
}