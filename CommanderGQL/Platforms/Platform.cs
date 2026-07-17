using CommanderGQL.Commands;

namespace CommanderGQL.Platforms;

public class Platform
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LicenseKey { get; set; }

    public ICollection<Command> Commands { get; set; } = [];
}