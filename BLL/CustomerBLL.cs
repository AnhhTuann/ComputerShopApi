using System.Collections.Generic;
using DTO;
using DAL;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
	public class CustomerBLL
	{
		private MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
		public List<Person> GetAll()
		{
			return CustomerDAL.GetAll();
		}

		public Person GetById(int id)
		{
			return CustomerDAL.GetById(id);
		}

		public int Create(Person customer)
		{
			byte[] hashedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(customer.Password));
			string hashedPassword = Encoding.ASCII.GetString(hashedBytes);

			customer.Password = hashedPassword;

			return CustomerDAL.Create(customer);
		}

		public Person Login(Person customer)
		{
			byte[] hashedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(customer.Password));
			string hashedPassword = Encoding.ASCII.GetString(hashedBytes);
			Person information = CustomerDAL.GetByEmail(customer.Email);

			if (information.Password == hashedPassword)
			{
				return information;
			}

			return null;
		}

		public void Update(Person customer)
		{
			CustomerDAL.Update(customer);
		}
	}
}