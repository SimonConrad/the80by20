// TODO make tests working with real database work, also on ci/cd sqllite ine memory has problems with applying migrations if multiple contexts and schemas

using Xunit;

namespace the80by20.Tests.Integration.Common;

[CollectionDefinition(nameof(SystemTestCollectionDefinition), DisableParallelization = true)]
public class SystemTestCollectionDefinition { }