namespace the80by20.Shared.Abstractions.Kernel.Types
{
    public class ProblemId : TypeId
    {
        public ProblemId(Guid value) : base(value)
        {
        }

        public static implicit operator ProblemId(Guid id) => new(id);

        public static implicit operator Guid(ProblemId id) => id.Value;

        public override string ToString() => Value.ToString("N");
    }
}
