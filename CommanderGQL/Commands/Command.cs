using CommanderGQL.Platforms;

namespace CommanderGQL.Commands;

public class Command
{
    public int Id { get; set; }
    public string HowTo { get; set; }
    public string CommandLine { get; set; }
    public int PlatformId { get; set; }

    //Navigation prop
    public Platform Platform { get; set; }
}