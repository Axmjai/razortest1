using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CBEs.Models
{
    public class UserMetadata
    {
        [Display(Name = "ชื่อผู้ใช้" , Order = 15000)]

        public string Username { get; set; }


        [Display(Name = "รหัสผ่าน", Order = 15001)]

        public string Password { get; set; }
    }

    [ModelMetadataType(typeof(UserMetadata))]
    public partial class User
    { 
    }
}
