﻿using Service;
using Service.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Test;

[TestClass]
[ExcludeFromCodeCoverage]
public class CategoryTest
{
    Category aCategory = new Category(1)
    {
        Name = "Casual"
    };
    [TestMethod]
    public void CategoryIsNotNull()
    {
        Assert.IsNotNull(aCategory);
    }
    [TestMethod]
    [ExpectedException(typeof(ServiceException))]
    public void CategoryNameIsEmpty()
    {
        Category otherCategory = new Category(2)
        {
            Name = ""
        };
    }
    [TestMethod]
    public void CategoryNameIsOk()
    {
        Assert.AreEqual(aCategory.Name,"Casual");
    }

    public void CategoryNameFails()
    {
        Assert.AreNotEqual(aCategory.Name, "Outwear");
    }
}
