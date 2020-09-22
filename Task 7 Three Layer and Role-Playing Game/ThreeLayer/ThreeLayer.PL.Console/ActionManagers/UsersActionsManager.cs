using System;
using System.Collections.Generic;
using ThreeLayer.BLL.UsersLogicContracts;
using ThreeLayer.Common.Entities;
using ThreeLayer.PL.Console.Extensions;

namespace ThreeLayer.PL.Console.ActionManagers
{
    internal class UsersActionsManager
    {
        private readonly IUsersManager _usersManager;

        public UsersActionsManager(IUsersManager usersManager)
        {
            _usersManager = usersManager ?? throw new ArgumentNullException(nameof(usersManager));
        }

        public void AddUsed()
        {
            _usersManager.AddUser(ReadUserDataFromConsole());
            System.Console.WriteLine("Пользователь успешно добавлен!");
        }

        public void RemoveUserById()
        {
            var resultOperation = _usersManager.RemoveUserById(Utils.ReadNonNegativeIntegerFromConsole());

            System.Console.WriteLine(resultOperation ? "Пользователь успешно удален!" : "Пользователя с таким ID не существует!");
        }

        public void GetAllUsers() => ShowUsersInfoToConsole(_usersManager.GetAllUsers());

        public void AddAwardForUser()
        {
            (int userId, int awardId) = ReadAssociationFromConsole();

            System.Console.WriteLine(_usersManager.BindToAward(
                userId, awardId) ? "Награда успешно добавлена!" : "Такая награда уже имеется у пользователя!");
        }

        public void GetAllUserAwards()
        {
            var userId = Utils.ReadNonNegativeIntegerFromConsole();

            ShowUserAwardsToConsole(_usersManager.GetAwards(userId));
        }

        public void RemoveAwardFromUser()
        {
            (int userId, int awardId) = ReadAssociationFromConsole();

            System.Console.WriteLine(_usersManager.UnBindFromAward(
                userId, awardId) ? "Награда успешно удалена!" : "Ошибка удаления награды!");

            _usersManager.UnBindFromAward(userId, awardId);
        }

        private void ShowUserAwardsToConsole(IEnumerable<Award> awards)
        {
            System.Console.WriteLine("Награды пользователя:");

            foreach (var award in awards)
                System.Console.WriteLine(award);
        }

        private void ShowUsersInfoToConsole(IEnumerable<User> users)
        {
            System.Console.WriteLine("Пользователи:");

            foreach (var user in users)
                System.Console.WriteLine(user);
        }

        private (int userId, int awardId) ReadAssociationFromConsole()
        {
            System.Console.WriteLine("Введите через пробел два целых неотрицательных числа: user_ID award_ID...");

            while (true)
            {

                var input = System.Console.ReadLine().Split(new[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

                if (input.Length != 2)
                {
                    System.Console.WriteLine("Ошибка формата входных данных. Повторите попытку...");
                    continue;
                }

                if (!input[0].TryCastToNonNegativeInt32(out int userId))
                {
                    System.Console.WriteLine("User_ID должен быть целым неотрицательным числом. Повторите попытку...");
                    continue;
                }

                if (!input[1].TryCastToNonNegativeInt32(out int awardId))
                {
                    System.Console.WriteLine("User_ID должен быть целым неотрицательным числом. Повторите попытку...");
                    continue;
                }

                return (userId, awardId);
            }
        }

        private User ReadUserDataFromConsole()
        {
            System.Console.WriteLine("Введите через пробел: имя(не более 30 символов) дата_рождения(MM/dd/yyyy)...");
            var user = new User();

            while (true)
            {
                var inputArray = System.Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (inputArray.Length != 2)
                {
                    System.Console.WriteLine("Неверный формат входных данных. Повторите попытку...");
                    continue;
                }

                if (inputArray[0].Length > 30)
                {
                    System.Console.WriteLine("Длина имени пользователя не может превышать 30 символов. Повторите попытку...");
                    continue;
                }

                if (!DateTime.TryParse(inputArray[1], out DateTime birthDay))
                {
                    System.Console.WriteLine("Неверный форматы даты рождения юзера. Повторите попытку...");
                    continue;
                }

                user.Name = inputArray[0];
                user.DateOfBirth = birthDay;

                return user;
            }
        }
    }
}
