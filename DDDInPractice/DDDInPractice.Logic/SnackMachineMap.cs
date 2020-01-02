using FluentNHibernate;
using FluentNHibernate.Mapping;

namespace DDDInPractice.Logic
{
    public class SnackMachineMap : ClassMap<SnackMachine>
    {
        public SnackMachineMap()
        {
            Id(x => x.Id);

            Component(x => x.MoneyInside, y =>
            {
                y.Map(x => x.OneCentCount);
                y.Map(x => x.TenCentCount);
                y.Map(x => x.QuarterCount);
                y.Map(x => x.OneDollarCount);
                y.Map(x => x.FiveDollarCount);
                y.Map(x => x.TwentyDollarCount);
            });

            // Reveal por conta da prop ser definida como "protected".
            // Ao salvar, atualiza os slots que foram alterados com Cascade.SaveUpdate().

            HasMany<Slot>(Reveal.Member<SnackMachine>("Slots"))
                .Cascade.SaveUpdate()
                .Not.LazyLoad();
        }
    }
}
