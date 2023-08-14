using System.ComponentModel.DataAnnotations;

namespace Identity_proje.Models.DTOs
{
    public class RegisterDTO
    {





        //dataannotations kütüphanesi  istediğimiz hata mesajlarını yazabirliz  required altındaki  ile ilişkilidir yani user name ile 

        [Required(ErrorMessage ="Kullanıcı adı yazınız")]
        [MinLength(3,ErrorMessage ="En az 3 karakter yazmalısınız")]
        public string UserName {get; set; }




        [Required(ErrorMessage ="Şirenizi yazınız")]
        [MaxLength(4,ErrorMessage ="Max 4 karakter yazmalısınız")]
        [DataType(DataType.Password)]
        public string Password { get; set; }





        [Required(ErrorMessage ="Mailinizi yazınız")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }



    }
}
