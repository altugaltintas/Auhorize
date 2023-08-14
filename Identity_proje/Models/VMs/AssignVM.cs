using System.Collections.Generic;

namespace Identity_proje.Models.VMs
{
    public class AssignVM
    {

        public string RoleName { get; set; }

        public IEnumerable<AppUser> HasRole { get; set; }  // role sahip olanlar

        public IEnumerable<AppUser> HasNotRole { get; set; }  // role sahip olmayanlar

        public string[] AddIds { get; set; } //Role yeni eklecenker

        public string[] DeleteIds { get; set; }  //Mevcutta bu rolu olup silinecekelr
    }
}
