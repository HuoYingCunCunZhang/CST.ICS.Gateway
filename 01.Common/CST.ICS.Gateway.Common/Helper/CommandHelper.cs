using GDZ9.Model.ICS.Business;

namespace CST.ICS.Gateway.Common
{
    public static class CommandHelper
    {
        /// <summary>
        /// 指令解析内容
        /// </summary>
        /// <param name="command">指令</param>
        /// <returns></returns>
        public static bool ResolveCommandPayload(string command, out CommandModel commandModel)
        {
            commandModel = new CommandModel();
            if (command.Contains("|"))
            {
                commandModel.ReturnValue = command.Split('|')[1];
            }
            else
            {
                if (command.Contains(" "))
                {
                    string[] foreStr = command.Split(' ');
                    commandModel.Param = foreStr[1];
                    commandModel.Role = (RoleType)Enum.Parse(typeof(RoleType), foreStr[0].Split(':')[0]);
                    commandModel.Main = foreStr[0].Split(':')[1];

                }
                else
                {
                    if (command.Contains("?"))
                    {
                        commandModel.Param = null;
                        commandModel.Role = (RoleType)Enum.Parse(typeof(RoleType), command.Split(':')[0]);
                        commandModel.Main = command.Split(':')[1].Replace("?", "Query");
                    }
                    else
                    {
                        commandModel.Param = null;
                        commandModel.Role = (RoleType)Enum.Parse(typeof(RoleType), command.Split(':')[0]);
                        commandModel.Main = command.Split(':')[1];
                    }
                }

            }
            return true;
        }
    }
}