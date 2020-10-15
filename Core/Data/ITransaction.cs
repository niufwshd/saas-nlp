using System;

namespace GovTown.Core.Data
{
	public interface ITransaction : IDisposable
	{
		void Commit();
		void Rollback();
	}
}
