
namespace DDDInPractice.Logic
{
    public class Slot : Entity
    {
        public Slot(SnackMachine snackMachine, int position) : this()
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = SnackPile.Empty;
        }

        protected Slot()
        {

        }

        public virtual SnackPile SnackPile { get; set; }
        public virtual SnackMachine SnackMachine { get; protected set; }
        public virtual int Position { get; protected set; }

        public virtual void LoadSnack(SnackPile snackPile)
        {
            SnackPile = snackPile;
        }
    }
}
