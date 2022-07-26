using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.SolutionToProblem.Operations;
using MediatR;

namespace Core.App.SolutionToProblem.Events;

public sealed record ProblemCreated(SolutionToProblemId SolutionToProblemId, string Category) : INotification;
