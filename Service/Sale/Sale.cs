﻿using System;
using Service.User;
using Service.Product;
namespace Service.Sale
{
	public class Sale
	{
        public IUser User { get; set; }
		public List<IProduct> Products { get; set; }
		public DateTime Date { get; set; }
        public Sale()
		{
			Date = DateTime.Now;
        }
	}
}
