namespace CommanderGQL.Platforms;

[ExtendObjectType(nameof(Subscription))]
public class PlatformSubscriptions
{
    [Subscribe]
    [Topic]
    public Platform OnPlatformAdded([EventMessage] Platform platform)
    {
        return platform;
    }
}