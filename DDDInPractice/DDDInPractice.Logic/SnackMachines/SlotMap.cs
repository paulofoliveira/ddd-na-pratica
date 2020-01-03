using FluentNHibernate.Mapping;

namespace DDDInPractice.Logic.SnackMachines
{
    public class SlotMap : ClassMap<Slot>
    {
        public SlotMap()
        {
            Id(x => x.Id);
            Map(x => x.Position);

            Component(x => x.SnackPile, m =>
             {
                 m.Map(x => x.Quantity);
                 m.Map(x => x.Price);
                 m.Map(x => x.Snack).Not.LazyLoad();
             });

            References(x => x.SnackMachine);
        }
    }
}
