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
	public class OrderDetailManager : IOrderDetailService
	{
		private readonly IOrderDetailDal _orderDetail;

		public OrderDetailManager(IOrderDetailDal orderDetail)
		{
			_orderDetail = orderDetail;
		}

		public void TAdd(OrderDetail entity)
		{
			throw new NotImplementedException();
		}

		public void TDelete(OrderDetail entity)
		{
			throw new NotImplementedException();
		}

		public OrderDetail TGetById(int id)
		{
			throw new NotImplementedException();
		}

		public List<OrderDetail> TGetListAll()
		{
			throw new NotImplementedException();
		}

		public void TUpdate(OrderDetail entity)
		{
			throw new NotImplementedException();
		}
	}
}
