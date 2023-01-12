# Traceroute

A clone for Winidows [tracert](https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/tracert) and Linux [traceroute](https://linux.die.net/man/8/traceroute) utilities. It tracks the route packets taken from an IP network on their way to a given host. It utilizes the IP protocol's time to live (TTL) field and attempts to elicit an ICMP TIME_EXCEEDED response from each gateway along the path to the host.

## In action
![cmd_XcexnWROO9](https://user-images.githubusercontent.com/55746901/212147169-205f2134-5d89-477d-b845-b7c38c2a0d53.gif)

## Build and run
1. Ensure you have [.NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed.
2. Clone the repository:
``git clone https://github.com/Alexander-Ploskin/Traceroute.git``
3. Go to project directory:
``cd Traceroute/``
4. Run the command:
``dotnet run -- <host to trace> [<flags>]``

## Available options
 **-m**, **--max_ttl**           (Default: 30) Set the max number of hops traceroute will perform (max ttl).

 **-f**, **--min_ttl**           (Default: 1) Set ttl value for the first hop.

 **-q**, **--queries_number**    (Default: 3) Set the number of querries for a hop.

 **-s**, **--packet_size**       (Default: 5) Set the size of packet to send (in bytes).

 **-n**, **--show_only_ip**      (Default: false) Set to hide dns names.

 **-w**, **--wait_time**         (Default: 120) Set the maximum time to wait for response (in ms).

 **-h**, **--help**              Display help information.

 **-v**, **--version**           Display version information.
