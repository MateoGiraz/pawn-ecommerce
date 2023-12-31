using Service.Exception;
using Service.Filter;
using Service.Filter.ConcreteFilter;
using Service.Product;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test.Service.Filter;

[TestClass]
[ExcludeFromCodeCoverage]
public class NameFilterTest
{
    [TestMethod]
    public void CanCreateFilter_Ok()
    {
        var nameFilter = new NameFilter();
        Assert.IsNotNull(nameFilter);
    }
    
    [TestMethod]
    public void Match_EqualName_Ok()
    {
        const string name = "Potato";
        var filterCriteria = new StringFilterCriteria()
        {
            Value = name
        };
        
        var product = new Product()
        {
            Name = name
        };
        
        var nameFilter = new NameFilter();
        var match = nameFilter.Match(product, filterCriteria);
        Assert.IsTrue(match);
    }
    
    [TestMethod]
    public void Match_IncludesName_Ok()
    {
        const string productName = "Potato";
        const string filterQuery = "Pot";
        var filterCriteria = new StringFilterCriteria()
        {
            Value = filterQuery
        };
        var product = new Product()
        {
            Name = productName
        };
        
        var nameFilter = new NameFilter();
        var match = nameFilter.Match(product, filterCriteria);
        Assert.IsTrue(match);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ModelException))]
    public void Match_WrongFilterCriteria_Throws()
    {
        const string productName = "Potato";
        const string filterQuery = "Pot";
        var filterCriteria = new Category(2)
        {
            Name = filterQuery
        };
        var product = new Product()
        {
            Name = productName
        };
        
        var nameFilter = new NameFilter();
        var match = nameFilter.Match(product, new IdFilterCriteria(){ Value = filterCriteria.Id });
    }
}