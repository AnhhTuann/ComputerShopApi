using System.Collections.Generic;
using DTO;
using DAL;
using System.Security.Cryptography;
using System.Text;

namespace BLL
{
	public class StaffBLL
	{
		private MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
		public List<Staff> GetAll()
		{
			return StaffDAL.GetAll();
		}

		public Staff GetById(int id)
		{
			return StaffDAL.GetById(id);
		}

		public int Create(Staff staff)
		{
			byte[] hashedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(staff.Password));
			string hashedPassword = Encoding.ASCII.GetString(hashedBytes);

			staff.Password = hashedPassword;

			if (StaffDAL.GetByEmail(staff.Email) != null)
			{
				return -1;
			}

			return StaffDAL.Create(staff);
		}

		public Staff Login(Staff staff)
		{
			byte[] hashedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(staff.Password));
			string hashedPassword = Encoding.ASCII.GetString(hashedBytes);
			Staff information = StaffDAL.GetByEmail(staff.Email);

			if (information != null && information.Password == hashedPassword)
			{
				return information;
			}

			return null;
		}

		public void Update(Staff staff)
		{
			StaffDAL.Update(staff);
		}
	}
}