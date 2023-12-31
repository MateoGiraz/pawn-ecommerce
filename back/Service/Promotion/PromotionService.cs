﻿using System;
using Service.Product;
using Service.Exception;
namespace Service.Promotion
{
	public class PromotionService : IPromotionService
	{
		private PromotionSelector Selector { get; set; }

		public PromotionService()
		{
			Selector = new PromotionSelector();
		}

		public IPromotionStrategy GetPromotion(List<Service.Product.Product> products)
		{
			if (products is null || products.Count == 0)
			{
				throw new ServiceException("Can not get promotion of empty list of products");
			}
			
			return Selector.GetBestPromotion(products);
		}
	}
}

