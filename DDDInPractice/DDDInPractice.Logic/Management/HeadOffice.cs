using DDDInPractice.Logic.Common;
using DDDInPractice.Logic.SharedKernel;

namespace DDDInPractice.Logic.Management
{
    public class HeadOffice : AggregateRoot
    {
        public HeadOffice()
        {
        }

        public virtual decimal Balance { get; protected set; }
        public virtual Money Cash { get; protected set; } = Money.None;

        public virtual void ChangeBalance(decimal delta)
        {
            Balance += delta;
        }
    }
}
