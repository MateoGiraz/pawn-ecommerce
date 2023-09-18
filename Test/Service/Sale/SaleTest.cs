﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Sale;
using Service.User;
using Moq;
using System;
namespace Test;

[TestClass]
public class SaleTest
{

    [TestMethod]
    public void CanCreateSale_Ok()
    {
        var s = new Sale();
        Assert.IsNotNull(s);
    }

    [TestMethod]
    public void SaleHasUser()
    {

        var userMock = new Mock<IUser>();
        userMock.Setup(user => user.Email).Returns("diegoalmenara@gmail.com");

        var s = new Sale()
        {
            User = (IUser) userMock
        };
        Assert.Equals(s.User.Email, "diegoalmenara@gmail.com");
    }
}