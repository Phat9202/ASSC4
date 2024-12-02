using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    public class Service
    {
        MyDb MyDb = new MyDb();
        public List<Sach> Get_ListSach()
        {
            return MyDb.Sachs.ToList();
        }
        public void Add_Sach(Sach sach)
        {
            MyDb.Sachs.Add(sach);
            MyDb.SaveChanges();
        }
        public void Delete_Sach(string MaSach)
        {
            MyDb.Sachs.Remove(MyDb.Sachs.FirstOrDefault(x=>x.Ma==MaSach));
            MyDb.SaveChanges();
        }
    }
}
