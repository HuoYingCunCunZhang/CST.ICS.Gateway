using CST.ICS.Gateway.Model;
using CST.ICS.Gateway.Pressure.IDevice;
using GDZ9.Model.ICS.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.ICS.Gateway.Pressure.Command
{
    /// <summary>
    /// 被检设备指令
    /// </summary>
    public class UUTCommand : IUUTCommand
    {
        public List<DeviceUnit<UUTLiveData>> Devices { get; set; }
        public CommandReturnData<string> AD(string parameter)
        {
            UUTCommandData.ADResultPairs.Clear();
            UUTCommandData.ADState.Clear();
            ADParam strings = new ADParam();
            strings = JsonConvert.DeserializeObject<ADParam>(parameter);

            List<Task> tasks = new List<Task>();
            // 异常序列号
            string errorSerialNumber = string.Empty;
            foreach (var item in Devices)
            {
                if (item.DeviceItem == null)
                {
                    continue;
                }
                if (!item.DeviceItem.IsEnable || !item.DeviceItem.IsOnline)
                {
                    continue;
                }
                if (UUTCommandData.ADState.ContainsKey(item.LiveData.SN))
                {
                    UUTCommandData.ADState[item.LiveData.SN] = CommandRunState.Running;
                }
                else
                {
                    UUTCommandData.ADState.Add(item.LiveData.SN, CommandRunState.Running);
                }
                Task task = Task.Run(() =>
                {
                    var pressureGauge = item.DeviceItem as IPressureStdDevice;
                    List<int> testedADList = new List<int>();
                    //1、设备开启AD标定模式
                    pressureGauge.StartCalibrationAD();


                    for (int i = 0; i < strings.ADList.Count; i++)
                    {
                        int adValue = strings.ADList[i];
                        if (UUTCommandData.ADResultPairs.ContainsKey(item.LiveData.SN))
                        {
                            UUTCommandData.ADResultPairs[item.LiveData.SN] = adValue;
                        }
                        else
                        {
                            UUTCommandData.ADResultPairs.Add(item.LiveData.SN, adValue);
                        }
                        //2、设备写入AD值
                        var result = pressureGauge.SetADValue(adValue);
                        if (!result)
                        {
                            if (string.IsNullOrEmpty(errorSerialNumber))
                            {
                                errorSerialNumber += pressureGauge.SerialNumber;
                            }
                            else
                            {
                                errorSerialNumber += $"\r\n{pressureGauge.SerialNumber}";
                            }
                            break;
                        }
                        System.Threading.Thread.Sleep(1000);

                        //3、设备读取放大倍数原始标识值
                        double orgADValue = pressureGauge.GetADOriginalFlagValue();

                        //4、判断原始标识值是否大于8000000
                        if (orgADValue > 8000000)
                        {
                            break;
                        }
                        testedADList.Add(adValue);
                    }
                    //5、取遍历过的上一个AD值即最优AD
                    if (UUTCommandData.ADResultPairs.ContainsKey(item.LiveData.SN))
                    {
                        UUTCommandData.ADResultPairs[item.LiveData.SN] = testedADList.LastOrDefault();
                    }
                    else
                    {
                        UUTCommandData.ADResultPairs.Add(item.LiveData.SN, testedADList.LastOrDefault());
                    }
                    if (UUTCommandData.ADState.ContainsKey(item.LiveData.SN))
                    {
                        UUTCommandData.ADState[item.LiveData.SN] = CommandRunState.Finish;
                    }
                    else
                    {
                        UUTCommandData.ADState.Add(item.LiveData.SN, CommandRunState.Finish);
                    }
                });
                tasks.Add(task);
            }
            CommandReturnData<string> commandReturnData = new CommandReturnData<string>();
            commandReturnData.Success = true;
            commandReturnData.ErrorCode = CommandErrorCode.Code_0000;

            return commandReturnData;
        }

        public CommandReturnData<ADResult> ADQuery()
        {
            
            throw new NotImplementedException();
        }

        public CommandReturnData<Dictionary<string, Xmas11.Domain.Mechanics.Pressure>> CurrentValueQuery()
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<Dictionary<string, bool>> Disable(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> LiveData(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> OffSet(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> ResumeCalData(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<ResumeCalDataResult> ResumeCalDataQuery()
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> RS(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<RSResult> RSQuery()
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> ScanDevices(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> SentencedToSteady(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<BalanceResult<Xmas11.Domain.Mechanics.Pressure>> SentencedToSteadyQuery()
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> WorkMode(string parameter)
        {
            throw new NotImplementedException();
        }

        public CommandReturnData<string> Zero()
        {
            throw new NotImplementedException();
        }
    }
}
