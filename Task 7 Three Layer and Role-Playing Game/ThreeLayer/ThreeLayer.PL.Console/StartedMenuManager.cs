using System;
using System.Collections.Generic;
using ThreeLayer.PL.Console.ActionManagers;

namespace ThreeLayer.PL.Console
{
    internal class StartedMenuManager
    {
        private readonly UsersActionsManager _userActionsManager;
        private readonly AwardsActionsManager _awardActionsManager;

        private readonly Dictionary<UIActionType, string> _discriprionsOfActions;

        public StartedMenuManager(UsersActionsManager userActionsManager, AwardsActionsManager awardActionsManager)
        {
            _userActionsManager = userActionsManager;
            _awardActionsManager = awardActionsManager;

            _discriprionsOfActions = new Dictionary<UIActionType, string>
            {
                { UIActionType.ADD_USER, "Добавить пользователя;" },
                { UIActionType.GET_ALL_USERS, "Показать всех пользователей;" },
                { UIActionType.REMOVE_USER_BY_ID, "Удалить пользователя по ID;" },
                { UIActionType.ADD_AWARD, "Добавить награду;" },
                { UIActionType.GET_ALL_AWARDS, "Показать все возможные награды;" },
                { UIActionType.REMOVE_AWARD_BY_ID, "Удалить награду по ID;" },
                { UIActionType.ADD_AWARD_TO_USER, "Добавить награду для пользователя;" },
                { UIActionType.GET_USER_AWARDS, "Показать награды пользователя по его ID;" },
                { UIActionType.REMOVE_AWARD_FROM_USER, "Удалить награду у пользователя..." }
            };
        }

        public void StartShowingMenu()
        {
            while (true)
            {
                ShowMenu();
            }
        }

        private void ShowMenu()
        {
            OutActionTypes();
            var at = ReadActionType();

            HandleChouseAction(at);
        }

        private void HandleChouseAction(UIActionType actionType)
        {
            switch (actionType)
            {
                case UIActionType.NONE:
                    break;
                case UIActionType.ADD_USER:
                    _userActionsManager.AddUsed();
                    break;
                case UIActionType.GET_ALL_USERS:
                    _userActionsManager.GetAllUsers();
                    break;
                case UIActionType.REMOVE_USER_BY_ID:
                    _userActionsManager.RemoveUserById();
                    break;
                case UIActionType.ADD_AWARD:
                    _awardActionsManager.AddAward();
                    break;
                case UIActionType.GET_ALL_AWARDS:
                    _awardActionsManager.GetAllAwards();
                    break;
                case UIActionType.REMOVE_AWARD_BY_ID:
                    _awardActionsManager.RemoveAwardById();
                    break;
                case UIActionType.ADD_AWARD_TO_USER:
                    _userActionsManager.AddAwardForUser();
                    break;
                case UIActionType.GET_USER_AWARDS:
                    _userActionsManager.GetAllUserAwards();
                    break;
                case UIActionType.REMOVE_AWARD_FROM_USER:
                    _userActionsManager.RemoveAwardFromUser();
                    break;
                default:
                    break;
            }
        }

        private void OutActionTypes()
        {
            foreach (var kvp in _discriprionsOfActions)
            {
                System.Console.WriteLine($"{(int)kvp.Key} - {kvp.Value}");
            }
        }

        private UIActionType ReadActionType()
        {
            var res = UIActionType.NONE;

            while (res == UIActionType.NONE)
            {
                var input = System.Console.ReadKey().Key;
                var keyValue = input >= ConsoleKey.D1 && input <= ConsoleKey.D9 ? input - ConsoleKey.D0 : 0;
                if (keyValue == 0)
                    keyValue = input >= ConsoleKey.NumPad1 && input <= ConsoleKey.NumPad9 ? input - ConsoleKey.NumPad0 : 0;

                res = (UIActionType)keyValue;

                if (res == UIActionType.NONE)
                    ClearLastSymbolInConsole();
            }

            System.Console.WriteLine();
            return res;
        }

        private void ClearLastSymbolInConsole()
        {
            System.Console.SetCursorPosition(System.Console.CursorLeft - 1, System.Console.CursorTop);
            System.Console.Write(" ");
            System.Console.SetCursorPosition(System.Console.CursorLeft - 1, System.Console.CursorTop);
        }
    }
}
