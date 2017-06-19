using System.Collections.Generic;

namespace SellYourCar.Entities
{
    public class MakeCar
    {
        public int Id {get; set;}
        public string Name {get; set;}

        //Row version for concurreny not to get conflicts between updates and adds from different clients
         
        public byte[] RowVersion {get; set; }
        public virtual ICollection<ModelCar> ModelCars {get; set; } = new List<ModelCar>() {};
    }
}