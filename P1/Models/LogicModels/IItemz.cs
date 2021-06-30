using DatabaseModels;
using System.Collections.Generic;

namespace LogicModels
{
    public interface IItemz
    {
        int ShowItemid(string userinput);
        decimal ShowItemPrice(string chosenitem);
        List<Item> ShowItems(string userinput);
    }
}