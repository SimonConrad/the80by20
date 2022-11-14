//using MediatR;
//using the80by20.Modules.Solution.Infrastructure.EF;
//using the80by20.Shared.Abstractions.ArchitectureBuildingBlocks.MarkerAttributes;

//namespace the80by20.Modules.Solution.Infrastructure.Behaviors
//{
//    // todo test 
//    // https://github.com/jbogard/MediatR/wiki/Behaviors todo check if instead of IPipelineBehavior use preprocessor and posprocessobehavior
//    [HandlerDecorator]
//    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
//        where TRequest : IRequest<TResponse>
//    {
//        private readonly SolutionDbContext _dbContext;

//        public UnitOfWorkBehavior(SolutionDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
//        {
//            await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

//            try
//            {
//                var result = await next();
//                await _dbContext.SaveChangesAsync(cancellationToken);
//                await transaction.CommitAsync(cancellationToken);

//                return result;
//            }
//            catch (Exception)
//            {
//                await transaction.RollbackAsync(cancellationToken);
//                throw;
//            }
//        }

//    }
//}