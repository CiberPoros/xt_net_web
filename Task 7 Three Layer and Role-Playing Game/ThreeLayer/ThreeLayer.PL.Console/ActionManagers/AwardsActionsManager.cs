using System;
using System.Collections.Generic;
using ThreeLayer.BLL.UsersLogicContracts;
using ThreeLayer.Common.Entities;

namespace ThreeLayer.PL.Console.ActionManagers
{
    internal class AwardsActionsManager
    {
        private readonly IAwardsManager _awardManager;

        public AwardsActionsManager(IAwardsManager awardsManager)
        {
            _awardManager = awardsManager ?? throw new ArgumentNullException(nameof(awardsManager));
        }

        public void AddAward()
        {
            _awardManager.AddAward(ReadAwardDataFromConsole());
            System.Console.WriteLine("Награда успешно добавлена!");
        }

        public void RemoveAwardById()
        {
            var resultOperation = _awardManager.RemoveAwardById(Utils.ReadNonNegativeIntegerFromConsole());

            System.Console.WriteLine(resultOperation ? "Награда успешно добавлена!" : "Наградой с таким ID не существует!");
        }

        public void GetAllAwards() => ShowAwardInfoToConsole(_awardManager.GetAllAwards());

        private void ShowAwardInfoToConsole(IEnumerable<Award> awards)
        {
            System.Console.WriteLine("Награды:");

            foreach (var award in awards)
                System.Console.WriteLine(award);
        }

        private Award ReadAwardDataFromConsole()
        {
            System.Console.WriteLine("Введите через пробел: наименование_награды(не более 30 символов)...");

            while (true)
            {
                var input = System.Console.ReadLine().Trim(' ');

                if (string.IsNullOrEmpty(input) || input.Length > 30)
                {
                    System.Console.WriteLine("Длина наименования награды не может превышать 30 символов. Повторите попытку...");
                    continue;
                }

                return new Award() { Title = input };
            }
        }
    }
}
