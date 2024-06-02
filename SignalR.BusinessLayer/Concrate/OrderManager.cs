using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.BusinessLayer.Concrate
{
	public class OrderManager : IOrderService
	{
		private readonly IOrderDal _orderdal;

		public OrderManager(IOrderDal orderdal)
		{
			_orderdal = orderdal;
		}

		public int TActiveOrderCount()
		{
			return _orderdal.ActiveOrderCount();
		}

		public void TAdd(Order entity)
		{
			throw new NotImplementedException();
		}

		public void TDelete(Order entity)
		{
			throw new NotImplementedException();
		}

		public Order TGetById(int id)
		{
			throw new NotImplementedException();
		}

		public List<Order> TGetListAll()
		{
			throw new NotImplementedException();
		}

		public decimal TLastOrderPrice()
		{
			return _orderdal.LastOrderPrice();
		}

		public int TTotalOrderCount()
		{
			return _orderdal.TotalOrderCount();
		}

		public void TUpdate(Order entity)
		{
			throw new NotImplementedException();
		}
	}
}
