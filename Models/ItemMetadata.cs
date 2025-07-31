using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyApp.Models
{
    public class ItemMetadata
    {
    }
    [MetadataType(typeof(ItemMetadata))]
    public partial class Item
    { 
        public bool Create(MyappDatabaseContext dbcontext)
        {
            IsDeleted = false;
            CreatedBy = "John";
            CreatedDate = DateTime.Now;
            Updatedby = "John";
            UpdatedDate = DateTime.Now;
            
            dbcontext.Items.Add(this); //  เพิ่มเข้าฐานข้อมูล
            dbcontext.SaveChanges(); //  บันทึกเข้าฐานข้อมูล
            return true;
        }

        public Item Update(MyappDatabaseContext dbContext)
        {      
            Updatedby = "John";
            UpdatedDate = DateTime.Now;
            dbContext.Items.Update(this);
            dbContext.SaveChanges();
            return this;
        }

        public bool Delete(MyappDatabaseContext dbContext)
        {
            IsDeleted = true;
            Updatedby = "John";
            UpdatedDate = DateTime.Now;
            dbContext.Items.Update(this);
            dbContext.SaveChanges();
            return true;
        }


        public List<Item> GetAll(MyappDatabaseContext dbContext)
        {
            return dbContext.Items.Where(q => q.IsDeleted != true) //  เข้าถึง ฐานข้อมูลใน items เเละกรอกข้อมูล
                                             .Include(i => i.Category) // การนำข้อมูลจากตารางอื่นมา (join)
                                             .Include(s => s.SerialNumbers)
                                             .Include(ic => ic.ItemClient)
                                             .Include(c => c.Client)
                                             .ToList(); // แปลงให้เป็น List<>
        }
     
        public Item? GetById(MyappDatabaseContext dbContext, int id)
        { 
            Item? item = dbContext.Items.Include(i => i.Category)
                                        .Include(s => s.SerialNumbers)
                                        .Include(ic => ic.ItemClient)
                                        .Include(c => c.Client)
                                        .FirstOrDefault(q => q.IsDeleted != true && q.Id == id);//ถ้าเจอ → คืนค่าเเรกออกมานั้น //  ถ้าไม่เจอ → คืนค่า null(หรือ default)
                                                             // ยังไม่ถูกลบ เเละ ID เเละ Id ที่ส่งมาเท่ากัน

           return item;
        }
    }


}
