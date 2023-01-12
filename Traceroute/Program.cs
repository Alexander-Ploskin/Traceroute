using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using CommandLine;

try
{
    Parser.Default.ParseArguments<TracerouteConfig>(args)
        .Validate()
        .WithParsed(Run);
}
catch (ArgumentException ex)
{
    Console.WriteLine(ex.Message);
}


static void Run(TracerouteConfig config)
{
    var ipAddress = GetIPAddress(config.Address!);
    Console.WriteLine($"Tracing route to {config.Address} \nover a maximum of {config.MaxTTL} hops.");
    using var pingSender = new Ping();

    for (var hop = config.MinTTL; hop < config.MaxTTL; ++hop)
    {
        IPAddress? replyAddress = null;

        Console.Write($" {hop}");
        for (var query = 0; query < config.QueriesNumber; ++query)
        {
            var stopwatch = Stopwatch.StartNew();
            var buffer = new byte[config.PacketSize];
            var reply = pingSender.Send(ipAddress, config.WaitTime, buffer, new PingOptions(hop, true));
            stopwatch.Stop();

            if (reply.Status == IPStatus.TimedOut)
            {
                Console.Write(" *");
                continue;
            }
            if (reply.Status == IPStatus.DestinationHostUnreachable)
            {
                Console.WriteLine(" The destination host is unreachable");
                return;
            }
            Console.Write($" {stopwatch.ElapsedMilliseconds} ms");
            if (replyAddress == null)
            {
                replyAddress = reply.Address;
            }
        }

        if (replyAddress == null)
        {
            Console.WriteLine("  Request timed out.");
            continue;
        }
        if (!config.HideDnsNames)
        {
            try
            {
                var dnsName = Dns.GetHostEntry(replyAddress).HostName;
                Console.Write($"  {dnsName} [{replyAddress}]");
            }
            catch (Exception)
            {
                Console.Write($" {replyAddress}");
            }
        }
        else
        {
            Console.Write($" {replyAddress}");
        }
        Console.WriteLine();

        if (replyAddress.Equals(ipAddress))
        {
            break;
        }
    }

    Console.WriteLine();
    Console.WriteLine("Trace complete.");
}

static IPAddress GetIPAddress(string addressName)
{
    IPAddress.TryParse(addressName, out var parsedIpAddress);
    if (parsedIpAddress == null)
    {
        try
        {
            parsedIpAddress = Dns.GetHostAddresses(addressName)[0];
        }
        catch (Exception)
        {
            throw new ArgumentException("Bad value for address");
        }
    }

    return parsedIpAddress != null ? parsedIpAddress : throw new ArgumentException("Bad value for address");
}