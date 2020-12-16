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

			if (CustomerDAL.GetByEmail(customer.Email) != null)
			{
				return -1;
			}

			return CustomerDAL.Create(customer);
		}

		public Person Login(Person customer)
		{
			byte[] hashedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(customer.Password));
			string hashedPassword = Encoding.ASCII.GetString(hashedBytes);
			Person information = CustomerDAL.GetByEmail(customer.Email);

			if (information != null && information.Password == hashedPassword)
			{
				return information;
			}

			return null;
		}

		public void Update(Person customer)
		{
			Person oldCustomer = CustomerDAL.GetById(customer.Id);
			customer.Password = oldCustomer.Password;
			CustomerDAL.Update(customer);
		}

		public bool ChagePassword(int id, string oldPassword, string newPassword)
		{
			Person customer = CustomerDAL.GetById(id);
			byte[] hashedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(oldPassword));
			string hashedPassword = Encoding.ASCII.GetString(hashedBytes);

			if (hashedPassword == customer.Password)
			{
				byte[] newHashedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(newPassword));
				string newHashedPassword = Encoding.ASCII.GetString(newHashedBytes);
				customer.Password = newHashedPassword;
				CustomerDAL.Update(customer);
				return true;
			}

			return false;
		}
	}
}