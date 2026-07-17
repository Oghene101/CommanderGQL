using CommanderGQL.Data;
using HotChocolate.Subscriptions;

namespace CommanderGQL.Platforms;

[ExtendObjectType(nameof(Mutation))]
public class PlatformMutations
{
    public async Task<AddPlatformPayload> AddPlatformAsync(
        AppDbContext context,
        ITopicEventSender topicEventSender,
        PlatformInput input,
        CancellationToken cancellationToken = default)
    {
        var platform = new Platform
        {
            Name = input.Name,
            LicenseKey = string.Empty,
        };
        context.Platforms.Add(platform);

        await context.SaveChangesAsync(cancellationToken);

        await topicEventSender.SendAsync(nameof(PlatformSubscriptions.OnPlatformAdded), platform, cancellationToken);

        return new AddPlatformPayload(platform);
    }
}