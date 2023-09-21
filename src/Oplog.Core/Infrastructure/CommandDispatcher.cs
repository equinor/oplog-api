
namespace Oplog.Core.Infrastructure;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public CommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task Dispatch<T>(T command) where T : ICommand
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<T>)) as ICommandHandler<T>;
        if (handler == null)
        {
            throw new ApplicationException($"No Commandhandler registered for handling {typeof(T)}");
        }
        await handler.Handle(command);
    }

    public async Task<TResult> Dispatch<TCommand, TResult>(TCommand command) where TCommand : ICommand
    {
        var handler = _serviceProvider.GetService(typeof(ICommandHandler<TCommand, TResult>)) as ICommandHandler<TCommand, TResult>;
        if (handler == null)
        {
            throw new ApplicationException($"No Commandhandler registered for handling {typeof(TCommand)}");
        }
        return await handler.Handle(command);
    }
}
