﻿using the80by20.Modules.Solution.App.Solution.Events;
using the80by20.Modules.Solution.Domain.Solution.Events;
using the80by20.Shared.Abstractions.Kernel;
using the80by20.Shared.Abstractions.Messaging;

namespace the80by20.Modules.Solution.App.Solution.Services
{
    public class EventMapper : IEventMapper
    {
        public IMessage Map(IDomainEvent @event)
            => @event switch
            {
                SolutionFinished e => new SolutionToProblemFinished(e.solution.Id, Guid.Empty, "", "", 0), // todo add rest of data
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