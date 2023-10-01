﻿using System;
namespace Service.Product
{
	public interface IColorRepository
	{
		public List<Color> GetAll();

        public Color GetById(int id);
    }
}

