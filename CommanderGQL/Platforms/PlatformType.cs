namespace CommanderGQL.Platforms;

public class PlatformType : ObjectType<Platform>
{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Field(x => x.LicenseKey).Ignore();
        
        descriptor.Field(p => p.Commands)
            .Description("This is the list of available commands for the platform");
    }
}