using DatabaseModels;
using System.Collections.Generic;

namespace LogicModels
{
    /// <summary>
    /// Interface for class Storez.
    /// </summary>
    public interface IStorez
    {
        string Name { get; set; }

        List<Store> ShowStores();
        Store StoreChosen(int chosen);
    }
}