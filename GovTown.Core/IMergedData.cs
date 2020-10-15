using System.Collections.Generic;

namespace GovTown.Core
{
	public interface IMergedData
	{
		bool MergedDataIgnore { get; set; }
		Dictionary<string, object> MergedDataValues { get; }
	}
}
