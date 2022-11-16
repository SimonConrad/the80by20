using the80by20.Shared.Abstractions.Kernel;
using the80by20.Shared.Abstractions.Messaging;

namespace the80by20.Modules.Solution.App.Solution.Services
{
    public class EventMapper : IEventMapper
    {
        public IMessage Map(IDomainEvent @event)
            => @event switch
            {
                Domain.Solution.Events.SolutionFinished e => new Events.SolutionFinished(e.solution.Id), // todo add rest of data
                //SubmissionStatusChanged
                //{ Status: SubmissionStatus.Approved } e => new SubmissionApproved(e.Submission.Id),
                //SubmissionStatusChanged
                //{ Status: SubmissionStatus.Rejected } e => new SubmissionRejected(e.Submission.Id),
                _ => null
            };

        public IEnumerable<IMessage> MapAll(IEnumerable<IDomainEvent> events)
            => events.Select(Map);
    }
}
