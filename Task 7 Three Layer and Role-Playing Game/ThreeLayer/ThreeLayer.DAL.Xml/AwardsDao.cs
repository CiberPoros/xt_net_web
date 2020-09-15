using System;
using System.Collections.Generic;
using System.Linq;
using ThreeLayer.DAL.Contracts;
using ThreeLayer.Common.Entities;
using System.Xml.Linq;
using ThreeLayer.DAL.Xml.Extensions;

namespace ThreeLayer.DAL.Xml
{
    public class AwardsDao : EntityXmlDao<Award>, IAwardsDao
    {
        public AwardsDao()
        {
            StoragePathAppSettingsKey = "AwardsStoragePath";
            DefaultStoragePath = @"Storage\AwardsList.xml";
        }

        protected override string StoragePathAppSettingsKey { get; }
        protected override string DefaultStoragePath { get; }


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
    }
}
