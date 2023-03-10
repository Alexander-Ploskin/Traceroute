using CommandLine;

public class TracerouteConfig
{
    [Option('m', "max_ttl", Default = 30, HelpText = "Set the max number of hops traceroute will perform (max ttl).")]
    public int MaxTTL { get; set; }

    [Option('f', "min_ttl", Default = 1, HelpText = "Set ttl value for the first hop.")]
    public int MinTTL { get; set; }

    [Option('q', "queries_number", Default = 3, HelpText = "Set the number of querries for a hop.")]
    public int QueriesNumber { get; set; }

    [Option('s', "packet_size", Default = 5, HelpText = "Set the size of packet to send (in bytes).")]
    public int PacketSize { get; set; }

    [Option('n', "show_only_ip", Default = false, HelpText = "Set to hide dns names.")]
    public bool HideDnsNames { get; set; }

    [Option('w', "wait_time", Default = 120, HelpText = "Set the maximum time to wait for response (in ms).")]
    public int WaitTime { get; set; }

    [Value(0, Required = true, HelpText = "IP address or DNS name to traceroute.")]
    public string? Address { get; set; }
}

public static class TracerouteConfigExtenstions
{
    public static ParserResult<TracerouteConfig> Validate(this ParserResult<TracerouteConfig> result)
    {
        var config = result.Value;

        if (config == null)
        {
            return result;
        }

        if (config.MaxTTL < 1 || config.MaxTTL < config.MinTTL)
        {
            throw new ArgumentException("Bad value for max_ttl");
        }
        if (config.MinTTL < 1)
        {
            throw new ArgumentException("Bad value for min_ttl");
        }
        if (config.QueriesNumber < 1)
        {
            throw new ArgumentException("Bad value for max_querries");
        }
        if (config.PacketSize < 1)
        {
            throw new ArgumentException("Bad value for packet_size");
        }
        if (config.WaitTime < 1)
        {
            throw new ArgumentException("Bad value for wait_time");
        }

        return result;
    }
}