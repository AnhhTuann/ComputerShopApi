using DAL;
using System.Collections.Generic;
using DTO;

namespace BLL
{
	public class StorageBLL
	{
		public int Create(Storage storage)
		{
			return StorageDAL.Create(storage);
		}

		public List<Storage> GetAll()
		{
			return StorageDAL.GetAll();
		}
	}
}