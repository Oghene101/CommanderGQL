using CommanderGQL.Commands;
using CommanderGQL.Platforms;
using HotChocolate.Execution.Configuration;

namespace CommanderGQL.Extensions;

public static class Type
{
    public static IRequestExecutorBuilder AddTypes(
        this IRequestExecutorBuilder builder)
    {
        return builder
            .AddQueryType()
            .AddTypeExtension<PlatformQueries>()
            .AddTypeExtension<CommandQueries>()
            //
            .AddMutationType()
            .AddTypeExtension<PlatformMutations>()
            .AddTypeExtension<CommandMutations>()
            //
            .AddSubscriptionType()
            .AddTypeExtension<PlatformSubscriptions>()
            //
            .AddType<PlatformType>()
            .AddType<CommandType>();
    }
}