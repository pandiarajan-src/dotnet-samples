// See https://aka.ms/new-console-template for more information
using ExtensionMethodLib;

public static class ProductExtensions
{
    public static double CalculateDiscountRate(this Product obj)
    {
        return (obj.DiscountPercentage * obj.Price)/100;
    }

    public static double CalculateOnlineCommission(this OnlineProduct obj)
    {
        return (obj.Price * obj.OnlineCommision)/100;
    }

}

public static class Program
{
    public static void Main()
    {
        Product prod = new Product {ProductName = "First", Price = 20, DiscountPercentage = 10};
        System.Console.WriteLine(prod?.CalculateDiscountRate());

        OnlineProduct oProd = new OnlineProduct{ ProductName = "First", Price = 10, DiscountPercentage = 10, OnlineCommision = 2};
        System.Console.WriteLine(oProd?.CalculateOnlineCommission());
    }
}