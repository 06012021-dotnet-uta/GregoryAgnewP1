using DatabaseModels;
using System.Collections.Generic;

namespace LogicModels
{
    /// <summary>
    /// Interface for class Cart.
    /// </summary>
    public interface ICart
    {
        Dictionary<string, int> Cartstuff { get; }

        List<string> Cartadd(List<Item> item, int itemamount, int whichitem);
        string FinalCart(Dictionary<string, int> endcart, string total, int currentstore, string userfirstname, string userlastname);
        void Resetcart();
    }
}