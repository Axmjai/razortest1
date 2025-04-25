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
        [NotMapped]
        public List<string> Serial { get; set; }
        public async Task<bool> Create(MyAppContext dbcontext)
        {
            IsDeleted = false;
            CreatedBy = "John";
            CreatedDate = DateTime.Now;
            Updatedby = "John";
            UpdatedDate = DateTime.Now;
            
            dbcontext.Items.Add(this);
            await dbcontext.SaveChangesAsync();

            return true;
        }
        public async Task<Item> Update(MyAppContext dbContext)
        {
            IsDeleted = false;
            Updatedby = "John";
            UpdatedDate = DateTime.Now;
            dbContext.Items.Update(this);
            await dbContext.SaveChangesAsync();
            return this;
        }

        public async Task<bool> Delete(MyAppContext dbContext)
        {
            IsDeleted = true;
            Updatedby = "John";
            UpdatedDate = DateTime.Now;
            dbContext.Items.Update(this);
            await dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<List<Item>> GetAll(MyAppContext dbContext)
        {
            return await dbContext.Items.Where(q => q.IsDeleted != true)
                                             .Include(i => i.Category)
                                             .Include(s => s.SerialNumbers)
                                             .Include(ic => ic.ItemClient)
                                             .Include(c => c.Client)
                                             .ToListAsync();
        }
     
        public async Task<Item?> GetById(MyAppContext dbContext, int id)
        { 
            Item? item = await dbContext.Items.Include(i => i.Category)
                                             .Include(s => s.SerialNumbers)
                                             .Include(ic => ic.ItemClient)
                                             .Include(c => c.Client)
                                             .FirstOrDefaultAsync(q => q.IsDeleted != true && q.Id == id);
            return item;
        }

        public async Task<bool> Createserial(MyAppContext dbContext, List<string>Serial)
        {
            foreach (var see in Serial)
            {
                if (!string.IsNullOrWhiteSpace(see)) {
                    dbContext.SerialNumbers.Add(new SerialNumber {Name = see,ItemId = this.Id});
                }
                
            }
            await dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateSerial(MyAppContext dbContext, List<string> upSerial) 
        {
            var isee = dbContext.SerialNumbers.Where(e => e.ItemId == Id);
            //var item = await dbContext.SerialNumbers.FindAsync(Id); 
            dbContext.SerialNumbers.RemoveRange(isee);
            foreach (var see in upSerial)
            {
                if (!string.IsNullOrWhiteSpace(see))
                {
                    dbContext.SerialNumbers.Add(new SerialNumber { Name = see, ItemId = this.Id });
                }
            }
            await dbContext.SaveChangesAsync();
            return true;
        }

    }


}
