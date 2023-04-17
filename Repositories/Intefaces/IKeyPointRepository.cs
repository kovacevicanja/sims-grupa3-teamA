using BookingProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingProject.Repositories.Intefaces
{
    public interface IKeyPointRepository
    {
       void Create(KeyPoint keyPoint);

       List<KeyPoint> GetAll();

       KeyPoint GetByID(int id);
    }
}
