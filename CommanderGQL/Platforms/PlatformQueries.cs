using CommanderGQL.Data;

namespace CommanderGQL.Platforms;

[ExtendObjectType(nameof(Query))]
public class PlatformQueries
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Platform> GetPlatforms(AppDbContext context)
    {
        return context.Platforms;
    }
}