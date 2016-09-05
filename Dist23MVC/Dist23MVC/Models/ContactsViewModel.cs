using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Dist23MVC.Models
{
    public class ContactsViewModel
    {
        [Key]
        public int pKey { get; set; }
        public Contacts contact { get; set; }
        public List<ContactPosition> positions { get; set; }
        public List<PositionViewModel> contactPositions { get; set; }

        public ContactsViewModel(int? ID)
        {
            using(Dist23Data db = new Dist23Data())
            {
                contact = db.Contacts.Where(c => c.pKey == ID).FirstOrDefault();
                positions = db.ContactPosition.Where(p => p.ContactID == ID).ToList();
                contactPositions = new List<PositionViewModel>();
                foreach(ContactPosition pos in positions)
                {
                    PositionViewModel pvm = new PositionViewModel();
                    Positions p = db.Positions.Where(x => x.pKey == pos.PositionID).FirstOrDefault();
                    Groups g = db.Groups.Where(x => x.pKey == pos.GroupID).FirstOrDefault();
                    pvm.positionKey = pos.pKey;
                    pvm.PositionName = p.PositionName;
                    pvm.GroupName = g.GroupName;
                    contactPositions.Add(pvm);
                }
            }
        }

        public ContactsViewModel()
        {

        }
    }

    public class PositionViewModel
    {
        public int positionKey { get; set; }
        public string PositionName { get; set; }
        public string GroupName { get; set; }
    }
}