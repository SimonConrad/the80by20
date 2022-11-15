namespace the80by20.Shared.Abstractions.Kernel.Types
{
    public class SolutionToProblemId : TypeId
    {
        public SolutionToProblemId(Guid value) : base(value)
        {
        }

        public static implicit operator SolutionToProblemId(Guid id) => new(id);

        public static implicit operator Guid(SolutionToProblemId id) => id.Value;

        public override string ToString() => Value.ToString("N");
    }
}
