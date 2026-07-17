using CommanderGQL.Data;

namespace CommanderGQL.Commands;

[ExtendObjectType(nameof(Query))]
public class CommandQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Command> GetCommands(AppDbContext context)
    {
        return context.Commands;
    }
}