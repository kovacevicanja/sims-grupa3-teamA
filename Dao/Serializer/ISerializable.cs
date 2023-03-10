using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Serializer
{
	internal interface ISerializable
	{
		string[] ToCSV();
		void FromCSV(string[] values);
	}
}
