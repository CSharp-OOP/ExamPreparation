using SantaWorkshop.Models.Dwarfs.Contracts;
using SantaWorkshop.Models.Instruments.Contracts;
using SantaWorkshop.Models.Presents.Contracts;
using SantaWorkshop.Models.Workshops.Contracts;
using System.Linq;

namespace SantaWorkshop.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public Workshop()
        {

        }
        public void Craft(IPresent present, IDwarf dwarf)
        {
            while (dwarf.Energy>0 && dwarf.Instruments.Any())
            {
                IInstrument currentInstrument = dwarf.Instruments.First();

                while (!present.IsDone() && dwarf.Energy>0 && !currentInstrument.IsBroken())
                {
                    dwarf.Work();
                    present.GetCrafted();
                    currentInstrument.Use();
                }

                if (currentInstrument.IsBroken())
                {
                    dwarf.Instruments.Remove(currentInstrument);
                }

                if (present.IsDone())
                {
                    break;
                }
            }
        }
    }
}
