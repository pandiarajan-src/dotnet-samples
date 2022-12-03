namespace ExtensionMethodLib;
public class Product
{
    public Product() => ProductName = "*******";
    public string ProductName { get; set; }
    public double DiscountPercentage { get; set; }
    public double Price{get; set;}
}

public class OnlineProduct : Product
{
    public string OnlineVendor { get; set; }
    public double OnlineCommision{get; set;}
}
