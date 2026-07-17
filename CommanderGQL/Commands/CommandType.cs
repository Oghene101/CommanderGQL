namespace CommanderGQL.Commands;

public class CommandType : ObjectType<Command>
{
    protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
    {
        descriptor.Description("Represents any executable command");

        descriptor.Field(c => c.Platform)
            .Description("Represents the platform to which the command belongs");
    }
}