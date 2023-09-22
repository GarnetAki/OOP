using Shops.Exceptions;
using Shops.Models;

namespace Shops.Entities;

public class Shop
{
    private List<ProductPosition> _productPositions;

    public Shop(ShopName shopName, Guid shopId, ShopAddress shopAddress)
    {
        ShopName = shopName;
        ShopId = shopId;
        ShopAddress = shopAddress;
        _productPositions = new List<ProductPosition>();
    }

    public ShopName ShopName { get; }
    public Guid ShopId { get; }
    public ShopAddress ShopAddress { get; }

    public void ProductsReplenishment(List<ProductPosition> newProducts)
    {
        foreach (ProductPosition newProductPosition in newProducts)
        {
            AddPosition(newProductPosition);
        }
    }

    public bool CheckProductAvailability(ShoppingListPosition product)
    {
        return _productPositions.Any(position => position.Product.Id.Equals(product.Product.Id))
               && _productPositions.Single(position => position.Product.Id.Equals(product.Product.Id)).CheckAvailable(product.Amount);
    }

    public bool CheckProductsAvailability(IReadOnlyList<ShoppingListPosition> products)
    {
        return products.All(CheckProductAvailability);
    }

    public decimal PriceOfProducts(IReadOnlyList<ShoppingListPosition> products)
    {
        if (!CheckProductsAvailability(products))
            throw ProductPositionException.CreateProductNotEnough();

        return products.Sum(product => _productPositions.Single(position => position.Product.Id.Equals(product.Product.Id)).Price * product.Amount);
    }

    public void SellProducts(IReadOnlyList<ShoppingListPosition> products, Person person)
    {
        if (!CheckProductsAvailability(products))
            throw ProductPositionException.CreateProductNotEnough();
        person.ReduceMoney(PriceOfProducts(products));
        foreach (ShoppingListPosition product in products)
        {
            _productPositions.Single(position => position.Product.Id.Equals(product.Product.Id)).Sell(product.Amount);
        }

        person.ShoppingListClear();
    }

    public void ChangePrice(Product product, decimal newPrice)
    {
        var newProductPosition = new ProductPosition(product, 0, newPrice);
        AddPosition(newProductPosition);
    }

    public int ProductAmount(Product product)
    {
        return CheckProductAvailability(new ShoppingListPosition(product, 1))
            ? _productPositions.Single(prod => prod.Product.Id.Equals(product.Id)).Amount : 0;
    }

    public decimal ProductPrice(Product product)
    {
        return CheckProductAvailability(new ShoppingListPosition(product, 1))
            ? _productPositions.Single(prod => prod.Product.Id.Equals(product.Id)).Price : 0;
    }

    private void AddPosition(ProductPosition newProduct)
    {
        if (_productPositions.Any(position => position.Product.Id.Equals(newProduct.Product.Id)))
        {
            _productPositions.Single(position => position.Product.Id.Equals(newProduct.Product.Id)).Replenishment(newProduct.Product.Name, newProduct.Amount, newProduct.Price);
        }
        else
        {
            _productPositions.Add(newProduct);
        }
    }
}