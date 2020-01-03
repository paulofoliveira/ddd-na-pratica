using DDDInPractice.Logic.Utils;

namespace DDDInPractice.Logic.Common
{
    public abstract class Repository<T>
        where T : AggregateRoot
    {
        public T GetById(long id)
        {
            using (var session = SessionFactory.OpenSession())
            {
                return session.Get<T>(id);
            }
        }

        public void Save(T aggregateRoot)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var tr = session.BeginTransaction())
                {
                    session.SaveOrUpdate(aggregateRoot);
                    tr.Commit();
                }
            }
        }
    }
}
