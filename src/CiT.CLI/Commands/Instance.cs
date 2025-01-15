using System.CommandLine;

namespace CiT.CLI.Commands;

public class Instance
{
    private readonly InstanceApi _apiClient;
    public Instance(IConfigManager configManager, HttpClient client)
    {
        _apiClient = new InstanceApi(configManager, client);
    }
    public Command GetCommand()
    {
        var command = new Command("instance");
        var peersCommand = new Command("peers");
        peersCommand.SetHandler(PeersCommand);

        command.AddCommand(peersCommand);
        return command;
    }
    private void PeersCommand()
    {
        var results = _apiClient.GetInstancePeers().Result;
        results.Sort();
        results.ForEach(Console.WriteLine);
        Console.WriteLine($"Total peers: {results.Count}");
    }
}
