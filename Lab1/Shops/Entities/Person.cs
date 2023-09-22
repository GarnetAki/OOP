using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Person
{
    private List<ShoppingListPosition> _shoppingList;

    public Person(PersonName name, decimal money)
    {
        Name = name;
        Money = money;
        _shoppingList = new List<ShoppingListPosition>();
    }

    public PersonName Name { get; }

    public decimal Money { get; private set; }

    public IReadOnlyList<ShoppingListPosition> ShoppingList => _shoppingList;

    public void ReduceMoney(decimal subtrahend)
    {
        if (Money < subtrahend) throw MoneyException.CreateNotEnoughMoney();
        Money -= subtrahend;
    }

    public void ShoppingListClear()
    {
        _shoppingList.Clear();
    }

    public void AddPosition(Product newProduct, int amount)
    {
        if (_shoppingList.Find(productPos => productPos.Product.Id == newProduct.Id) == null)
        {
            _shoppingList.Add(new ShoppingListPosition(newProduct, amount));
        }
        else
        {
            _shoppingList.Single(productPos => productPos.Product.Id == newProduct.Id).Replenishment(amount);
        }
    }
}