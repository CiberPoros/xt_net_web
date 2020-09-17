using System;
using System.Collections.Generic;
using System.Linq;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.Common.Entities;
using System.Xml.Linq;
using ThreeLayer.DAL.Xml.Extensions;
using ThreeLayer.DAL.Xml.Abstract;

namespace ThreeLayer.DAL.Xml
{
    public class AwardsDao : EntityWithIdXmlDao<Award>, IAwardsDao
    {
        public event EventHandler<Award> AwardRemoved;

        public void AddAward(Award award) => Add(award);

        public IEnumerable<Award> GetAllAwards() => GetAll();

        public void RemoveAwardById(int id)
        {
            var document = XDocument.Load(_storageFileInfo.FullName);
            var match = document.Root.Elements()
                                     .FirstOrDefault(xElement => xElement.FromXElement<Award>().Id == id);

            if (match == null)
                return;

            match.Remove();
            document.Save(_storageFileInfo.FullName);

            AwardRemoved(this, match.FromXElement<Award>());
        }

        protected override string GetEntityName() => nameof(Award);
    }
}
