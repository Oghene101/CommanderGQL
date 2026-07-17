using CommanderGQL.Data;

namespace CommanderGQL.Commands;

[ExtendObjectType(nameof(Mutation))]
public class CommandMutations
{
    public async Task<AddCommandPayload> AddCommandAsync(AppDbContext context, CommandInput input)
    {
        var command = new Command
        {
            HowTo = input.HowTo,
            CommandLine = input.CommandLine,
            PlatformId = input.PlatformId,
        };
        context.Commands.Add(command);

        await context.SaveChangesAsync();

        return new AddCommandPayload(command);
    }
}