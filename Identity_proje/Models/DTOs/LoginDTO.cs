using System.ComponentModel.DataAnnotations;

namespace Identity_proje.Models.DTOs
{
    public class LoginDTO
    {



        [Required(ErrorMessage ="Bu alan boş bırakılamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Şifrenizi yazınız")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

      
        //login actiona x controllerin y actionundan gitme isteyen yada direkt olarak account/login gelmek isteyen bir kişi olabilir. Kişi logini başarılır bir şekilde bitirdikten sonra doğduran istediği adrese gidebilir yada ana sayfaya yönlendirilebilir

        // Örnek veriyoruz. kişi categorty kontroller / update actiona gitmek isteyebilir anca biz o controllera authorize verdiysek önce kişi kendini tanıtması için logine yönlendirilir ve  gitmek istediği adres returnURl propunda category/update diye tutulur. Login başarılır olursa kayıtlı adresi göderilir. direk account/login geldiyse reutrnURL null olacağından anasayfaya yönlenir. Bu yuüzden  bu property required diyenen illa dolu olma zorunda değildir

        //
        public string ReturnUrl { get; set; }

    }
}
